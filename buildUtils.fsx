#r "nuget: Partas.Fake.Tools.GitCliff"
#r "nuget: EasyBuild.FileSystemProvider"
#r "nuget: Str"
#r "nuget: Fake.Tools.Git"

open Fake.Tools
open EasyBuild.FileSystemProvider

type Files =
    VirtualFileSystem<
        ".",
        """
cliff.toml
RELEASE_NOTES.md
gitcliff-context.json
"""
     >

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
            Git.TagPattern = "*" + PackageName.toTagSuffix packageName
            Bump.InitialTag = initialVersion |> Option.defaultValue "0.1.0"
            Changelog.Body =
                $$$"""{%- macro remote_url() -%}
    https://github.com/{{ remote.github.owner }}/{{ remote.github.repo }}
{%- endmacro -%}

{% if version -%}
    ## [{{ version | trim_start_matches(pat="v") | {{{PackageName.configVersionTrimmer packageName}}} }}] - {{ timestamp | date(format="%Y-%m-%d") }}
{% else -%}
    <h2>

        [Unreleased]
    
    </h2>
{% endif -%}

{% for group, commits in commits | group_by(attribute="group") %}
    <h3>{{ group | upper_first }}</h3>
    {%- for commit in commits %}
        - {{ commit.message | split(pat="\n") | first | upper_first | trim }}\
            {% if commit.remote.username %} by @{{ commit.remote.username }}{%- endif -%}
            {% if commit.remote.pr_number %} in \
            [#{{ commit.remote.pr_number }}]({{ self::remote_url() }}/pull/{{ commit.remote.pr_number }}) \
            {%- endif -%}
    {% endfor %}
{% endfor %}

{%- if github.contributors | filter(attribute="is_first_time", value=true) | length != 0 %}
  <h2>New Contributors</h2>
{%- endif -%}

{% for contributor in github.contributors | filter(attribute="is_first_time", value=true) %}
  * @{{ contributor.username }} made their first contribution
    {%- if contributor.pr_number %} in \
      [#{{ contributor.pr_number }}]({{ self::remote_url() }}/pull/{{ contributor.pr_number }}) \
    {%- endif %}
{%- endfor %}\n

"""
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

    let private validateContext packageName =
        File.readAsString Files.``gitcliff-context.json``
        |> Json.deserialize
        |> trimVersions packageName
        |> fun (content, lastVersion) ->
            content
            |> Json.serialize
            |> File.writeString false Files.``gitcliff-context.json``

            lastVersion

    let private getModifiedContext packageName dir =
        GitCliff.run
            (fun p ->
                { p with
                    Output = Files.``gitcliff-context.json``
                    Flags = [ GitCliff.CliFlags.Context ]
                    Config = Files.``cliff.toml`` })
            dir

        validateContext packageName,
        fun ctx ->
            { ctx with
                GitCliff.CliParams.FromContext = Files.``gitcliff-context.json`` }


    let runWithModifiedContext cliParams packageName initialVersion dir =
        if not <| File.exists Files.``cliff.toml`` then
            writeConfiguration (fun _ -> createConfig packageName initialVersion) Files.``cliff.toml``

        let _, cliModifier = getModifiedContext packageName dir
        GitCliff.run (cliParams >> cliModifier) dir
        File.delete Files.``gitcliff-context.json``

    let bumpWithModifiedContext packageName initialVersion dir =
        if not <| File.exists Files.``cliff.toml`` then
            writeConfiguration (fun _ -> createConfig packageName initialVersion) Files.``cliff.toml``

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

        let newVersion =
            File.readAsString Files.``gitcliff-context.json``
            |> Json.deserialize
            |> List.map _.Version
            |> List.tryFind _.IsSome
            |> Option.flatten

        GitCliff.run
            cliModifier
            dir

        File.delete Files.``gitcliff-context.json``

        let files = [ Files.``cliff.toml``; Files.``RELEASE_NOTES.md`` ]
        files
        |> List.iter (Git.Staging.stageFile "" >> ignore)
        Git.Commit.exec "" $"[skip ci]\n\nchore: update release notes for {packageName.Value}"
        
        match previousVersion, newVersion with
        | None, _ -> newVersion
        | Some v1, Some v2 when v1 <> v2 -> newVersion
        | _ -> None
        |> function
            | Some value -> value + PackageName.toTagSuffix packageName |> Git.Branches.tag ""
            | _ -> ()



let createNpmDependency npmPackageName version =
    "NpmDependencies", $"""<NpmPackage Name="{npmPackageName}" Version=">= {version}" />"""
