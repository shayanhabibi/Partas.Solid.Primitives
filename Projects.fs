module Projects
open System
open System.IO
open Fake.IO
open Fake.Core
open Spec

[<Literal>]
let private primitiveBase = "Partas.Solid.Primitives"

[<Struct>]
type PackageName = PackageName of string
    
module PackageName =
    let create = PackageName
    let get = function PackageName value -> value
    let map func = function PackageName value -> func value |> create
    let createWithBase = create >> map (sprintf "%s.%s" primitiveBase)

[<Struct>]
type NpmPackage =
    | Versioned of name: string * vers: Semver.SemVersion
    | Basic of name: string
    | NotProvided
module Semver =
    let create (major: int) (minor: int) (patch: int) = Semver.SemVersion(major,minor,patch)
    
module NpmPackage =
    let createSemVersioned (major,minor,patch) name=
        ( name,
          (major,minor,patch)
          |||> Semver.create )
        |> NpmPackage.Versioned
    let createVersioned vers name =
        (name,vers)
        |> NpmPackage.Versioned
    let create = NpmPackage.Basic
    let none = NpmPackage.NotProvided

type Project = {
    Path: string
    Name: PackageName
    NpmPackage: NpmPackage voption
}

type private ProjectBuilder() =
    member inline _.Yield(_: unit) = ()
    member inline _.Yield(value: Project) = value
    member inline _.Delay(y) = y()
    [<CustomOperation "path">]
    member inline _.ProjectPath(_: unit, value: string) =
        {
            Path = value
            Name =
                value
                |> Path.GetFileNameWithoutExtension
                |> PackageName.create
            NpmPackage = ValueNone
        }
    [<CustomOperation "projectWithoutBase">]
    member inline _.ProjectOp(this: Project, value: string) =
        { this with
            Name = value |> PackageName.create  }
    [<CustomOperation "project">]
    member inline _.ProjectWithBaseOp(this: Project, value: string) =
        { this with
            Name = value |> PackageName.createWithBase }
    [<CustomOperation "npm">]
    member inline _.NpmPackageName(this: Project, value: string) =
        match this with
        | { NpmPackage = ValueSome(NpmPackage.Versioned(name,semVer)) } ->
            { this with
                NpmPackage =
                    value
                    |> sprintf "@solid-primitives/%s"
                    |> NpmPackage.createVersioned semVer
                    |> ValueSome }
        | { NpmPackage = _ } ->
            { this with
                NpmPackage =
                    value
                    |> sprintf "@solid-primitives/%s"
                    |> NpmPackage.create
                    |> ValueSome }
    [<CustomOperation "npmWithSemver">]
    member inline _.NpmWithSemVer(this: Project, name, major: int, ?minor: int, ?patch: int) =
        let minor = defaultArg minor 0
        let patch = defaultArg patch 0
        { this with NpmPackage = NpmPackage.createSemVersioned (major, minor, patch) name |> ValueSome }
    member inline _.Run(state: Project) =
        state
let private project = ProjectBuilder()


module Projects =
    let common = project {
        path Files.Root.``Partas.Solid.Primitives.Common``.``.``
    }
    let activeElement = project {
        path Files.Root.``Partas.Solid.Primitives.ActiveElement``.``.``
        npmWithSemver "active-element" 0 1 0
    }
    let audio = project {
        path Files.Root.``Partas.Solid.Primitives.Audio``.``.``
        npmWithSemver "audio" 1 4 1
    }
    let autoFocus = project {
        path Files.Root.``Partas.Solid.Primitives.AutoFocus``.``.``
        npmWithSemver "autofocus" 0 1 0
    }
    let bounds = project {
        path Files.Root.``Partas.Solid.Primitives.Bounds``.``.``
        npmWithSemver "bounds" 0 1 0
    }
    let broadcastChannel = project {
        path Files.Root.``Partas.Solid.Primitives.BroadcastChannel``.``.``
        npmWithSemver "broadcast-channel" 0 1 0
    }
    let clipboard = project {
        path Files.Root.``Partas.Solid.Primitives.Clipboard``.``.``
        npmWithSemver "clipboard" 0 1 0
    }
    let devices = project {
        path Files.Root.``Partas.Solid.Primitives.Devices``.``.``
        npmWithSemver "devices" 1 3 1
    }
    let eventBus = project {
        path Files.Root.``Partas.Solid.Primitives.EventBus``.``.``
        npmWithSemver "event-bus" 0 1 0
    }
    let eventListener = project {
        path Files.Root.``Partas.Solid.Primitives.EventListener``.``.``
        npmWithSemver "event-listener" 2 4 1
    }
    let idle = project {
        path Files.Root.``Partas.Solid.Primitives.Idle``.``.``
        npmWithSemver "idle" 0 2 0
    }
    let keyboard = project {
        path Files.Root.``Partas.Solid.Primitives.Keyboard``.``.``
        npmWithSemver "keyboard" 0 1 0
    }
    let media = project {
        path Files.Root.``Partas.Solid.Primitives.Media``.``.``
        npmWithSemver "media" 0 1 0
    }
    let mouse = project {
        path Files.Root.``Partas.Solid.Primitives.Mouse``.``.``
        npmWithSemver "mouse" 2 1 2
    }
    let raf = project {
        path Files.Root.``Partas.Solid.Primitives.Raf``.``.``
        npmWithSemver "raf" 2 3 1
    }
    let scheduled = project {
        path Files.Root.``Partas.Solid.Primitives.Scheduled``.``.``
        npmWithSemver "scheduled" 1 5 0
    }
    let scroll = project {
        path Files.Root.``Partas.Solid.Primitives.Scroll``.``.``
        npmWithSemver "scroll" 2 1 0
    }
    let spring = project {
        path Files.Root.``Partas.Solid.Primitives.Spring``.``.``
        npmWithSemver "spring" 0 1 1
    }
    let timer = project {
        path Files.Root.``Partas.Solid.Primitives.Timer``.``.``
        npmWithSemver "timer" 1 4 0
    }
    let trigger = project {
        path Files.Root.``Partas.Solid.Primitives.Trigger``.``.``
        npmWithSemver "trigger" 0 1 0
    }
    let tween = project {
        path Files.Root.``Partas.Solid.Primitives.Tween``.``.``
        npmWithSemver "tween" 1 4 0
    }
    let projectPrimitives = project {
        path Files.Root.``Partas.Solid.Primitives``.``.``
        projectWithoutBase "Partas.Solid.Primitives"
    }

let projects = [
    Projects.activeElement
    Projects.audio
    Projects.autoFocus
    Projects.bounds
    Projects.broadcastChannel
    Projects.clipboard
    Projects.common
    Projects.devices
    Projects.eventBus
    Projects.eventListener
    Projects.idle
    Projects.keyboard
    Projects.media
    Projects.mouse
    Projects.projectPrimitives
    Projects.raf
    Projects.scheduled
    Projects.scroll
    Projects.spring
    Projects.timer
    Projects.trigger
    Projects.tween
]
