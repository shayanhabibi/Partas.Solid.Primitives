namespace Partas.Solid.Primitives

open Partas.Solid
open Browser.Types
open Fable.Core

[<Erase; AutoOpen>]
module private IdleSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/idle"
        let [<Literal>] version = "0.2.0"

open Spec

[<AllowNullLiteral; Interface>]
type IdleTimer =
    /// Report user status.
    abstract member isIdle: bool Accessor with get
    /// Report user status.
    abstract member isPrompted: bool Accessor with get
    /// Start timer
    abstract member start: unit -> unit with get
    /// Stop timer
    abstract member stop: unit -> unit with get
    /// Reset timer
    abstract member reset: unit -> unit with get
    /// Manually sets isIdle to true and triggers onIdle callback (with custom manualidle event).
    abstract member triggerIdle: unit -> unit with get

[<Erase; AutoOpen>]
type Idle =
    /// Provides different accessors and methods to observe the user's idle status and react to its changing.
    [<ImportMember(path); ParamObject>]
    static member createIdleTimer (
            ?idleTimeout: int,
            ?promptTimeout: int,
            ?onIdle: (Event -> unit),
            ?onPrompt: (Event -> unit),
            ?onActive: (Event -> unit),
            ?startManually: bool,
            ?events: Event[],
            ?element: HtmlElement
        ): IdleTimer = jsNative
