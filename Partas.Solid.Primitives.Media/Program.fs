namespace Partas.Solid.Primitives.Media

open Partas.Solid
open Partas.Solid.Primitives
open Fable.Core

[<Erase>]
module Spec =
    let [<Literal>] path = "@solid-primitives/media"
    let [<Literal>] version = ""

open Spec

[<Erase>]
type BreakpointMonitor<'T> =
    member inline this.obj = unbox<'T> this
    member _.key with get(): string = jsNative

[<Erase>]
type MediaQuery = (unit -> bool)

[<Interface; AllowNullLiteral>]
type MediaQueryEvent =
    abstract matches: bool
    abstract media: string

[<Erase; AutoOpen>]
type Media =
    [<ImportMember(path)>]
    static member makeMediaQueryListener(query: string) (handler: MediaQueryEvent -> unit): DisposeCallback = jsNative
    [<ImportMember(path)>]
    static member createMediaQuery (query: string, ?serverFallback: bool): MediaQuery = jsNative
    [<ImportMember(path)>]
    static member createBreakpoints (queryMonitor: 'T): BreakpointMonitor<'T> = jsNative
    [<ImportMember(path)>]
    static member sortBreakpoints (breakpoints: 'T): 'T = jsNative
    [<ImportMember(path)>]
    static member createPrefersDark (?fallback: bool): MediaQuery = jsNative
    [<ImportMember(path)>]
    static member usePrefersDark (): MediaQuery = jsNative

