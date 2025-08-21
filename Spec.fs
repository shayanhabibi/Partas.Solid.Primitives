module Spec

open Fake.Core
open EasyBuild.FileSystemProvider

module Files =
    type Root = AbsoluteFileSystem<__SOURCE_DIRECTORY__>

module Ops =
    [<Literal>]
    let Prelude = "Prelude"
    [<Literal>]
    let Clean = "Clean"
    [<Literal>]
    let GitNet = "GitNet"
    [<Literal>]
    let RestoreTools = "RestoreTools"
    [<Literal>]
    let Build = "Build"
    [<Literal>]
    let Pack = "Pack"
    [<Literal>]
    let GitPush = "GitPush"
    [<Literal>]
    let Publish = "Publish"
    [<Literal>]
    let PublishLocal = "PublishLocal"
    // [<Literal>]
    // let Test = "Test"
    // [<Literal>]
    // let Format = "Format"
    // [<Literal>]
    // let CheckFormat = "CheckFormat"

module Args =
    let mutable apiKey: string option = None
    let mutable local: bool = false
    let mutable parallelise: bool = false
    let setArgs args =
        let containsArg arg =
            args |> Array.contains arg
        let getArgValue arg =
            args
            |> Array.tryFindIndex ((=) arg)
            |> Option.map ((+) 1)
            |> Option.bind(fun idx ->
                Array.tryItem idx args
                )
        parallelise <- containsArg "--parallel"
        apiKey <- getArgValue "--nuget-api-key"
        local <- containsArg "--local"
            
open Fake.IO.Globbing.Operators
let sourceFiles =
    !! "**/*.fs"
    -- "packages/**/*.*"
    -- "paket-files/**/*.*"
    -- ".fake/**/*.*"
    -- "**/obj/**/*.*"
    -- "**/AssemblyInfo.fs"

let githubUsername = "GitHub Action"
let githubEmail = "41898282+github-actions[bot]@users.noreply.github.com"

