module Build.Build

open System.IO
open Fake.Core
open Fake.IO
open Fake.IO.Globbing.Operators
open Fake.Tools
open Partas.GitNet.RepoCracker
open Spec
open Projects
open Partas.GitNet.ConfigTypes
open Partas.GitNet

Helpers.initializeContext()
// GitNet Config
let gitnetConfig =
    let ignorePath = fun (str: string) -> Path.GetFileNameWithoutExtension(str)
    {
        GitNetConfig.initFSharp with
            ProjectType =
                {
                    ProjectFSharpConfig.init with
                        IgnoredProjects = List.map ignorePath [
                            Files.Root.Devgrounds.``Devgrounds.fsproj``
                            Files.Root.``Build.fsproj``
                        ]
                }
                |> Some
                |> ProjectType.FSharp
    }
// GitNet Runtime
let runtime =
    lazy
    new GitNetRuntime(gitnetConfig)
// GitNet Projects
let crackedProjects =
    lazy
    runtime.Value
    |> _.CrackRepo
    |> Seq.choose (CrackedProject.getFSharp >> ValueOption.toOption)
    |> Seq.toList

// Setup repo
Target.create Ops.Prelude <| fun args ->
    task {
        crackedProjects.Value
        |> ignore
    }
    |> ignore
    if not Args.local then
        [ $"config --local user.email \"{githubEmail}\""
          $"config --local user.name \"{githubUsername}\"" ]
        |> List.iter (Git.CommandHelper.directRunGitCommandAndFail Files.Root.``.``)

// Cleanup dirs
Target.create Ops.Clean <| fun args ->
    !! "**/**/bin"
    -- "bin"
    ++ "temp/"
    |> Shell.cleanDirs

Target.create Ops.GitNet <| fun args ->
    let runtime = runtime.Value
    if Args.local ||
       args.Context.AllExecutingTargets
       |> List.map _.Name
       |> List.exists ((=) Ops.PublishLocal)
    then
        let bumps,content = runtime.DryRun()
        bumps
        |> Seq.map (fun keyVal ->
            (keyVal.Key, keyVal.Value.SemVer.ToString(), keyVal.Value.ToString())
            |||> sprintf "Scope: %s | Next: %s | SepochSemver: %s")
        |> Trace.logItems "GitNet"
        runtime.WriteToOutput content
        |> ignore
    else
        runtime.Run(githubUsername, githubEmail)
        |> ignore

Target.create Ops.RestoreTools <| fun _ ->
    Helpers.run
        Helpers.dotnet
        [ "tool"; "restore" ]
        Files.Root.``.``

open Fake.DotNet

Target.create Ops.Build <| fun _ ->
    crackedProjects.Value
    |> List.iter (fun project ->
        DotNet.build
            (fun p ->
                {
                    p with
                        Configuration = DotNet.BuildConfiguration.Release
                        DotNet.BuildOptions.MSBuildParams.DisableInternalBinLog = true
                }
            )
            project.ProjectFileName
        )

Target.create Ops.Pack <| fun _ ->
    crackedProjects.Value
    |> List.iter (fun project ->
        project.ProjectFileName
        |> DotNet.pack (fun p ->
            {
                p with
                    NoRestore = true
                    OutputPath = Some "bin"
                    DotNet.PackOptions.MSBuildParams.DisableInternalBinLog = true
            }
            )
        )

Target.create Ops.GitPush <| fun _ ->
    Git.Branches.push Files.Root.``.``
    Git.CommandHelper.directRunGitCommandAndFail Files.Root.``.`` "push --tags origin"

let pushLocal path =
    path
    |> DotNet.nugetPush (fun p ->
        { p with
            DotNet.NuGetPushOptions.PushParams.Source = Some "local"
            DotNet.NuGetPushOptions.Common.CustomParams = Some "--skip-duplicate" }
        )

Target.create Ops.PublishLocal <| fun _ ->
    !! "bin/*.nupkg"
    |> Seq.iter pushLocal

Target.create Ops.Publish <| fun _ ->
    !!"bin/*.nupkg"
    |> Seq.iter (fun path ->
        path
        |> DotNet.nugetPush (fun p ->
            { p with
                DotNet.NuGetPushOptions.PushParams.Source = Some "https://api.nuget.org/v3/index.json"
                DotNet.NuGetPushOptions.Common.CustomParams = Some "--skip-duplicate"
                DotNet.NuGetPushOptions.PushParams.ApiKey = Args.apiKey }
            )
        if Args.local then
            pushLocal path
        )

open Fake.Core.TargetOperators

let dependencies = [
    Ops.Prelude
    ==> Ops.RestoreTools
    ==> Ops.Clean
    ==> Ops.GitNet
    ==> Ops.Pack
    ==> Ops.Publish
    ==> Ops.GitPush
    
    Ops.Pack
    ==> Ops.PublishLocal
]

[<EntryPoint>]
let main args =
    args |> Args.setArgs
    try
        args[0] |> Target.runOrDefaultWithArguments
        0
    with e ->
        printfn $"%A{e}"
        1
        
        
