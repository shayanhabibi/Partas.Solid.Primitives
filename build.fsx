#load "./buildUtils.fsx"
#r "nuget: EasyBuild.FileSystemProvider"
#r "nuget: Fake.Core.Target"
#r "nuget: Fake.Core.Process"
#r "nuget: Fake.Core.ReleaseNotes"
#r "nuget: Fake.IO.FileSystem"
#r "nuget: Fake.DotNet.Cli"
#r "nuget: Fake.DotNet.MSBuild"
#r "nuget: Fake.DotNet.AssemblyInfoFile"
#r "nuget: Fake.Tools.Git"
#r "nuget: Fake.Api.GitHub"
#r "nuget: Fake.DotNet.Testing.Expecto"
#r "nuget: Partas.Fake.Tools.GitCliff"

open System.Threading.Tasks
open BuildUtils

[<Literal>]
let baseName = "Partas.Solid.Primitives"

type Project =
    { Path: string
      Name: PackageName
      Description: string
      NpmPackage: string option
      NpmPackageVersion: string option
      Tags: string
      InitialVersion: string option }

open EasyBuild.FileSystemProvider
type Repo = AbsoluteFileSystem<__SOURCE_DIRECTORY__>

type BuildRepo =
    VirtualFileSystem<
        __SOURCE_DIRECTORY__,
        """
docs
    RELEASE_NOTES.md
Common
    AssemblyFile.fs
"""
     >

module Projects =
    let private tags =
        "Partas;Oxpecker;F#;FSharp;Fable;fable-javascript;Web;Framework;Solid;Solidjs;Bindings;Primitives"

    let private makeProject name path =
        { Path = path
          Name =
              match name with
              | "" -> baseName
              | _ -> (baseName, name) ||> sprintf "%s.%s"
              |> PackageName.create
          Description = "Bindings for Solid-Primitives " + name
          Tags = tags
          NpmPackageVersion = None
          NpmPackage = None
          InitialVersion = None }

    let private withPackage package project =
        { project with
            NpmPackage = Some package }

    let private withPackageVersion version project =
        { project with
            NpmPackageVersion = "@solid-primitives/" + version |> Some }

    let private withDescription description project =
        { project with
            Description = description }

    let private withInitialVersion version project =
        { project with
            InitialVersion = Some version }

    let common =
        Repo.``Partas.Solid.Primitives.Common``.ToString()
        |> makeProject "Common"
        |> withDescription "Common methods, functions, types and bindings for the Partas.Solid.Primitives bindings"
        |> withInitialVersion "0.1.0"

    let activeElement =
        Repo.``Partas.Solid.Primitives.ActiveElement``.ToString()
        |> makeProject "ActiveElement"
        |> withPackageVersion "0.1.0"
        |> withPackage "active-element"
        |> withInitialVersion "0.2.0"

    let audio =
        Repo.``Partas.Solid.Primitives.Audio``.``.``
        |> makeProject "Audio"
        |> withPackageVersion "1.4.1"
        |> withPackage "audio"
        |> withInitialVersion "0.1.0"

    let autoFocus =
        Repo.``Partas.Solid.Primitives.AutoFocus``.``.``
        |> makeProject "AutoFocus"
        |> withPackageVersion "0.1.0"
        |> withPackage "autofocus"
        |> withInitialVersion "0.2.0"

    let bounds =
        Repo.``Partas.Solid.Primitives.Bounds``.``.``
        |> makeProject "Bounds"
        |> withPackageVersion "0.1.0"
        |> withPackage "bounds"
        |> withInitialVersion "0.2.0"

    let broadcastChannel =
        Repo.``Partas.Solid.Primitives.BroadcastChannel``.``.``
        |> makeProject "BroadcastChannel"
        |> withPackageVersion "0.1.0"
        |> withPackage "broadcast-channel"
        |> withInitialVersion "0.2.0"

    let clipboard =
        Repo.``Partas.Solid.Primitives.Clipboard``.``.``
        |> makeProject "Clipboard"
        |> withPackage "clipboard"
        |> withPackageVersion "0.1.0"
        |> withInitialVersion "0.2.0"

    let devices =
        Repo.``Partas.Solid.Primitives.Devices``.``.``
        |> makeProject "Devices"
        |> withPackage "devices"
        |> withPackageVersion "1.3.1"
        |> withInitialVersion "0.1.0"

    let eventBus =
        Repo.``Partas.Solid.Primitives.EventBus``.``.``
        |> makeProject "EventBus"
        |> withPackage "event-bus"
        |> withPackageVersion "0.1.0"
        |> withInitialVersion "0.2.0"

    let eventListener =
        Repo.``Partas.Solid.Primitives.EventListener``.``.``
        |> makeProject "EventListener"
        |> withPackage "event-listener"
        |> withPackageVersion "2.4.1"
        |> withInitialVersion "0.1.0"

    let idle =
        Repo.``Partas.Solid.Primitives.Idle``.``.``
        |> makeProject "Idle"
        |> withPackageVersion "0.2.0"
        |> withPackage "idle"
        |> withInitialVersion "0.2.0"

    let keyboard =
        Repo.``Partas.Solid.Primitives.Keyboard``.``.``
        |> makeProject "Keyboard"
        |> withPackage "keyboard"
        |> withPackageVersion "0.1.0"
        |> withInitialVersion "0.2.0"

    let media =
        Repo.``Partas.Solid.Primitives.Media``.``.``
        |> makeProject "Media"
        |> withPackage "media"
        |> withPackageVersion "0.1.0"
        |> withInitialVersion "0.2.0"

    let mouse =
        Repo.``Partas.Solid.Primitives.Mouse``.``.``
        |> makeProject "Mouse"
        |> withPackage "mouse"
        |> withPackageVersion "2.1.2"
        |> withInitialVersion "0.2.0"

    let raf =
        Repo.``Partas.Solid.Primitives.Raf``.``.``
        |> makeProject "Raf"
        |> withPackage "raf"
        |> withPackageVersion "2.3.1"
        |> withInitialVersion "0.2.0"

    let scheduled =
        Repo.``Partas.Solid.Primitives.Scheduled``.``.``
        |> makeProject "Scheduled"
        |> withPackage "scheduled"
        |> withPackageVersion "1.5.0"
        |> withInitialVersion "0.2.0"

    let scroll =
        Repo.``Partas.Solid.Primitives.Scroll``.``.``
        |> makeProject "Scroll"
        |> withPackage "scroll"
        |> withPackageVersion "2.1.0"
        |> withInitialVersion "0.2.0"

    let spring =
        Repo.``Partas.Solid.Primitives.Spring``.``.``
        |> makeProject "Spring"
        |> withPackage "spring"
        |> withPackageVersion "0.1.1"
        |> withInitialVersion "0.2.0"

    let timer =
        Repo.``Partas.Solid.Primitives.Timer``.``.``
        |> makeProject "Timer"
        |> withPackage "timer"
        |> withPackageVersion "1.4.0"
        |> withInitialVersion "0.2.0"

    let trigger =
        Repo.``Partas.Solid.Primitives.Trigger``.``.``
        |> makeProject "Trigger"
        |> withPackage "trigger"
        |> withPackageVersion "0.1.0"
        |> withInitialVersion "0.2.0"

    let tween =
        Repo.``Partas.Solid.Primitives.Tween``.``.``
        |> makeProject "Tween"
        |> withPackage "tween"
        |> withPackageVersion "1.4.0"
        |> withInitialVersion "0.2.0"

    let projectPrimitives =
        Repo.``.``
        |> makeProject ""
        |> withInitialVersion "0.3.0"

let projectTargetProjects =
    [ Projects.common
      Projects.activeElement
      Projects.audio
      Projects.autoFocus
      Projects.bounds
      Projects.broadcastChannel
      Projects.clipboard
      Projects.devices
      Projects.eventBus
      Projects.eventListener
      Projects.idle
      Projects.keyboard
      Projects.media
      Projects.mouse
      Projects.raf
      Projects.scheduled
      Projects.scroll
      Projects.spring
      Projects.timer
      Projects.trigger
      Projects.tween
      Projects.projectPrimitives ]

System.Environment.GetCommandLineArgs()
|> Array.skip 2
|> Array.toList
|> Fake.Core.Context.FakeExecutionContext.Create false __SOURCE_FILE__
|> Fake.Core.Context.RuntimeContext.Fake
|> Fake.Core.Context.setExecutionContext

open Fake
open Fake.Core.TargetOperators
open Fake.Core
open Fake.IO
open Fake.IO.FileSystemOperators
open Fake.IO.Globbing.Operators
open Fake.DotNet
open Fake.DotNet.Testing
open Fake.Tools
open System.IO

Target.initEnvironment ()

open Fake.Tools.GitCliff

module Ops =
    [<Literal>]
    let Clean = "Clean"

    [<Literal>]
    let Prelude = "Prelude"

    [<Literal>]
    let RestoreTools = "RestoreTools"

    [<Literal>]
    let Nuget = "NuGet"

    [<Literal>]
    let Publish = "Publish"

    [<Literal>]
    let Build = "Build"

    [<Literal>]
    let AssemblyInfo = "AssemblyInfo"

    [<Literal>]
    let Test = "RunTests"

    [<Literal>]
    let GitCliff = "GitCliff"

    [<Literal>]
    let PublishLocal = "PublishLocal"

    [<Literal>]
    let Format = "Format"

    [<Literal>]
    let CheckFormat = "CheckFormat"

    [<Literal>]
    let ReleaseNotes = "ReleaseNotes"

let description =
    "F# Fable front-end framework; derived from Oxpecker.Solid; built on top of Solid.js"

let gitOwner = "shayanhabibi"
let gitName = "Partas.Solid"

let releases =
    lazy
        projectTargetProjects
        |> List.map (fun project -> project.Name, $"{project.Path}/RELEASE_NOTES.md")
        |> List.map (fun keyVal -> fst keyVal, snd keyVal |> ReleaseNotes.load)
        |> dict


let apiKey =
    Target.getArguments ()
    |> Option.bind (fun args ->
        let idx = args |> (Array.tryFindIndex ((=) "--nuget-api-key") >> Option.map ((+) 1))

        idx |> Option.map (Array.get args))

let sourceFiles =
    !!"**/*.fs" ++ "**/*.fsx"
    -- "packages/**/*.*"
    -- "paket-files/**/*.*"
    -- ".fake/**/*.*"
    -- "**/obj/**/*.*"
    -- "**/AssemblyInfo.fs"
    -- "**/IndexAccess/IndexAccess.fs"
    -- "Partas.Solid.FablePlugin/Plugin.fs"

// Check each project directory has a changelog config et al
Target.create Ops.Prelude (fun _ ->
    [ "config --local user.email \"41898282+github-actions[bot]@users.noreply.github.com\""
      "config --local user.name \"GitHub Action\"" ]
    |> List.iter (Git.CommandHelper.directRunGitCommandAndFail Repo.``.``)
)

Target.create Ops.Format (fun _ ->
    let result =
        sourceFiles
        |> Seq.map (sprintf "\"%s\"")
        |> String.concat " "
        |> DotNet.exec id "fantomas"

    if not result.OK then
        Trace.log $"Errors while formatting all files: %A{result.Messages}")


Target.create Ops.CheckFormat (fun _ ->
    let errorAction =
        if Git.Information.getBranchName "." = "master" then
            Trace.traceImportant
        else
            failwith

    let result =
        sourceFiles
        |> Seq.map (sprintf "\"%s\"")
        |> String.concat " "
        |> sprintf "%s --check"
        |> DotNet.exec id "fantomas"

    if result.ExitCode = 0 then
        Trace.log "No files need formatting"
    elif result.ExitCode = 99 then
        errorAction "Some files need formatting, run `dotnet fsi build.fsx target Format` to format them."
    else
        Trace.logf $"Errors while formatting: %A{result.Errors}"
        errorAction "Unknown errors while formatting")

Target.create Ops.GitCliff (fun _ ->
    projectTargetProjects
    |> List.iter (function
        | { Path = path
            Name = packageName
            InitialVersion = initVers } -> bumpWithModifiedContext packageName initVers path)

    Git.Commit.execExtended __SOURCE_DIRECTORY__ "[skip ci]" "Release notes")
// Generate assembly info file with versioning and up-to-date info
Target.create Ops.AssemblyInfo (fun _ ->
    let projects = releases.Value

    let paths =
        dict
            [ for (KeyValue(key, _)) in projects do
                  key,
                  Path.combine
                      (projectTargetProjects
                       |> List.find (_.Name >> (=) key)
                       |> _.Path)
                      (if key = Projects.projectPrimitives.Name
                       then Path.combine Projects.projectPrimitives.Name.Value "AssemblyInfo.fs"
                       else "AssemblyInfo.fs") ]

    projectTargetProjects
    |> List.iter (fun project ->
        AssemblyInfoFile.createFSharp
            paths[project.Name]
            [ AssemblyInfo.Title project.Name.Value
              AssemblyInfo.Product project.Name.Value
              AssemblyInfo.Version projects[project.Name].AssemblyVersion
              AssemblyInfo.FileVersion projects[project.Name].AssemblyVersion ]))


Target.create Ops.Clean (fun _ ->
    !!"**/**/bin" |> Shell.cleanDirs

    Shell.cleanDirs [ "bin"; "temp" ])

let makeArgs: string seq -> string = String.concat " "

let dotnet cmd args =
    match DotNet.exec id cmd (makeArgs args) with
    | result when not result.OK -> failwith $"Failed: {result.Errors}"
    | _ -> ()

open FSharp.Core
open FSharp.Collections

Target.create Ops.Build (fun _ ->

    projectTargetProjects
    |> List.iter (fun project ->
        (
        if project.Name = Projects.projectPrimitives.Name
        then Path.combine project.Path project.Name.Value
        else project.Path
        , project.Name.Value + ".fsproj"
        )
        ||> Path.combine
        |> DotNet.build (fun p ->
            { p with
                Configuration = DotNet.BuildConfiguration.Release
                DotNet.BuildOptions.MSBuildParams.DisableInternalBinLog = true
                DotNet.BuildOptions.MSBuildParams.Properties =
                    [ "PackageVersion", releases.Value.[project.Name].AssemblyVersion
                      "Version", releases.Value.[project.Name].AssemblyVersion
                      if project.NpmPackage.IsSome && project.NpmPackageVersion.IsSome then
                          createNpmDependency project.NpmPackage.Value project.NpmPackageVersion.Value ] })))


Target.create Ops.Test (fun _ ->
    !!"**/bin/**/*.Tests.Plugin.dll"
    |> Testing.Expecto.run (fun p ->
        { p with
            Summary = true
            CustomArgs = [ "--colours 256" ] @ p.CustomArgs }))

Target.create Ops.RestoreTools (fun _ ->
    let result = DotNet.exec id "tool" "restore"

    result.Messages |> Trace.logItems "Tool Restore"

    if not result.OK then
        failwith "Failed to restore dotnet tools")

Target.create Ops.Nuget (fun _ ->
    projectTargetProjects
    |> List.iter (fun project ->
        (
        if project.Name = Projects.projectPrimitives.Name
        then Path.combine project.Path project.Name.Value
        else project.Path
        , project.Name.Value + ".fsproj"
        )
        ||> Path.combine
        |> DotNet.pack (fun p ->
            { p with
                NoRestore = true
                OutputPath = Some "bin"
                DotNet.PackOptions.MSBuildParams.DisableInternalBinLog = true
                DotNet.PackOptions.MSBuildParams.Properties =
                    [ "PackageVersion", releases.Value[project.Name].AssemblyVersion
                      "Version", releases.Value[project.Name].AssemblyVersion
                      if project.NpmPackage.IsSome && project.NpmPackageVersion.IsSome then
                          createNpmDependency project.NpmPackage.Value project.NpmPackageVersion.Value ] }))
    Git.Branches.push Repo.``.``
    Git.CommandHelper.directRunGitCommandAndFail "" "push --tags origin"
    )

Target.create Ops.Publish (fun _ ->
    !!"bin/*.nupkg"
    |> Seq.iter (
        DotNet.nugetPush (fun p ->
            { p with
                DotNet.NuGetPushOptions.PushParams.ApiKey = apiKey
                DotNet.NuGetPushOptions.PushParams.Source = Some "https://api.nuget.org/v3/index.json"
                DotNet.NuGetPushOptions.Common.CustomParams = Some "--skip-duplicate" })
    ))

Target.create Ops.PublishLocal (fun _ ->
    !!"bin/*.nupkg"
    |> Seq.iter (
        DotNet.nugetPush (fun p ->
            { p with
                DotNet.NuGetPushOptions.PushParams.Source = Some "local"
                DotNet.NuGetPushOptions.PushParams.PushTrials = 1 })
    ))

Target.create Ops.ReleaseNotes (fun _ -> Git.Branches.push Repo.``.``)

Ops.Prelude ==> Ops.GitCliff ==> Ops.AssemblyInfo ?=> Ops.Build

Ops.AssemblyInfo ==> Ops.Nuget

Ops.Test ==> Ops.Nuget ==> Ops.Publish

Ops.GitCliff ==> Ops.ReleaseNotes

Ops.Test ==> Ops.Nuget ==> Ops.PublishLocal

Ops.Clean ==> Ops.Build ==> Ops.Test

Ops.RestoreTools ==> Ops.Test

Ops.CheckFormat ==> Ops.Build

Target.runOrDefaultWithArguments Ops.Clean
