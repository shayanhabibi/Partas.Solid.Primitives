#r "nuget: Partas.Fake.Tools.GitCliff, 0.2.3"
#r "nuget: Str"
#r "nuget: Fake.Tools.Git"

open Fake.Tools

type Files =
    static member ``cliff.toml`` = "cliff.toml"
    static member ``RELEASE_NOTES.md`` = "RELEASE_NOTES.md"
    static member ``gitcliff-context.json`` = "gitcliff-context.json"

type PackageName =
    private
    | PackageName of string

    static member Create input = PackageName input
    member this.Value = let (PackageName value) = this in value

module PackageName =
    open Str
    let create = PackageName.Create
    let get: PackageName -> string = _.Value
    let toTagSuffix = get >> (+) "-"
    let tagRegex = toTagSuffix >> sprintf @"^[0-9]+\.[0-9]+\.[0-9]*(?:%s})$"
    let configVersionTrimmer = toTagSuffix >> sprintf "trim_end_matches(pat=\"%s\")"

    let trimVersion packageName =
        packageName |> toTagSuffix |> Str.delete

[<AutoOpen>]
module GitCliff =
    open Fake.Tools.GitCliff.ConfigHelper

    let createConfig packageName (initialVersion: string option) : Config =
        { Config.Default with
            Git.CommitParsers = Config.Default.Git.CommitParsers |> Array.append [|
                CommitParser.Create(message = "^\[skip ci\]", skip = true)
            |]
            Changelog.RenderAlways = Some true
            Bump.InitialTag = initialVersion |> Option.defaultValue "0.1.0"
            Changelog.Body = $"{{%%- macro remote_url() -%%}}
    https://github.com/{{{{ remote.github.owner }}}}/{{{{ remote.github.repo }}}}
{{%%- endmacro -%%}}

{{%% if version -%%}}
    ## [{{{{ version | trim_start_matches(pat=\"v\") | {PackageName.configVersionTrimmer packageName} }}}}] - {{{{ timestamp | date(format=\"%%Y-%%m-%%d\") }}}}
{{%% else -%%}}
    <h2><a href=\"{{{{ self::remote_url() }}}}/compare/{{{{ release.previous.version }}}}..HEAD\">Unreleased</a></h2>
{{%% endif -%%}}

{{%% for group, commits in commits | group_by(attribute=\"group\") %%}}
    <h3>{{{{ group | upper_first }}}}</h3>
    {{%%- for commit in commits %%}}
        - {{{{ commit.message | split(pat=\"\n\") | first | upper_first | trim }}}} \
            {{%% if commit.remote.username %%}} by @{{{{ commit.remote.username }}}}{{%%- endif -%%}}
            {{%% if commit.remote.pr_number %%}} in \
            [#{{{{ commit.remote.pr_number }}}}]({{{{ self::remote_url() }}}}/pull/{{{{ commit.remote.pr_number }}}}) \
            {{%%- endif -%%}}
    {{%% endfor %%}}
{{%% endfor %%}}

{{%%- if github.contributors | filter(attribute=\"is_first_time\", value=true) | length != 0 %%}}
  <h2>New Contributors</h2>
{{%%- endif -%%}}

{{%% for contributor in github.contributors | filter(attribute=\"is_first_time\", value=true) %%}}
  * @{{{{ contributor.username }}}} made their first contribution
    {{%%- if contributor.pr_number %%}} in \
      [#{{{{ contributor.pr_number }}}}]({{{{ self::remote_url() }}}}/pull/{{{{ contributor.pr_number }}}}) \
    {{%%- endif %%}}
{{%%- endfor %%}}
"
            Changelog.Output = Files.``RELEASE_NOTES.md`` }

    open GitCliffContext
    open Fake.IO

    let private trimVersions packageName (context: JsonContent) =
        let mutable lastVersion = None

        let trimmer value =
            lastVersion <- lastVersion |> Option.orElse value
            value |> Option.map (PackageName.trimVersion packageName)

        let rec modifyContext: Context -> Context =
            function
            | { Version = version
                Previous = previous } as ctx ->
                { ctx with
                    Version = version |> trimmer
                    Previous = previous |> Option.map modifyContext }

        context |> List.map modifyContext: JsonContent,
        lastVersion

    let private validateContext packageName dir =
        Path.combine dir Files.``gitcliff-context.json``
        |> File.readAsString
        |> Json.deserialize
        |> trimVersions packageName
        |> fun (content, lastVersion) ->
            content
            |> Json.serialize
            |> File.writeString false (Path.combine dir Files.``gitcliff-context.json``)
            lastVersion

    let private getModifiedContext packageName dir =
        GitCliff.run
            (fun p ->
                { p with
                    WorkDir = dir
                    Output = Files.``gitcliff-context.json``
                    Flags = [ GitCliff.CliFlags.Context ]
                    Config = Files.``cliff.toml``
                    TagPattern = packageName |> PackageName.tagRegex })
            dir

        validateContext packageName dir,
        fun ctx ->
            { ctx with
                GitCliff.CliParams.WorkDir = dir
                GitCliff.CliParams.FromContext = Files.``gitcliff-context.json`` }


    let runWithModifiedContext cliParams packageName initialVersion dir =
        let cliffPath = Path.combine dir Files.``cliff.toml``
        if not <| File.exists cliffPath then
            writeConfiguration (fun _ -> createConfig packageName initialVersion) cliffPath 

        let _, cliModifier = getModifiedContext packageName dir
        GitCliff.run (cliParams >> cliModifier) dir
        File.delete cliffPath

    let bumpWithModifiedContext packageName initialVersion dir =
        let cliffPath = Path.combine dir Files.``cliff.toml``
        cliffPath |> File.delete
        cliffPath |> writeConfiguration (fun _ -> createConfig packageName initialVersion)

        let previousVersion, cliModifier = getModifiedContext packageName dir

        let bumpedContextCliParams: GitCliff.CliParams -> _ =
            fun p ->
                { p with
                    Bump = Some GitCliff.BumpStrategy.Auto
                    Flags = [ GitCliff.Context ]
                    Output = Files.``gitcliff-context.json``
                    Config = Files.``cliff.toml`` }
            >> cliModifier

        GitCliff.run bumpedContextCliParams dir
        let contextPath = Path.combine dir Files.``gitcliff-context.json``
        let newVersion =
            File.readAsString contextPath
            |> Json.deserialize
            |> List.map _.Version
            |> List.tryFind _.IsSome
            |> Option.flatten

        GitCliff.run
            cliModifier
            dir

        File.delete contextPath

        let files = [ cliffPath; Path.combine dir Files.``RELEASE_NOTES.md`` ]
        files
        |> List.iter (Git.Staging.stageFile "" >> ignore)
        Git.Commit.exec "" $"[skip ci]\n\nchore: update release notes for {packageName.Value}"
        
        match previousVersion, newVersion with
        | None, _ -> newVersion
        | Some v1, Some v2 when v1 <> v2 -> newVersion
        | _ -> None
        |> function
            | Some value ->
                let tag = value + PackageName.toTagSuffix packageName
                tag |> Git.Branches.tag ""
            | _ -> ()



let createNpmDependency npmPackageName version =
    "NpmDependencies", $"""<NpmPackage Name="{npmPackageName}" Version=">= {version}" />"""
