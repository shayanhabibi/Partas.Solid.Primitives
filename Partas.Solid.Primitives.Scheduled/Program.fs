namespace Partas.Solid.Primitives

open Fable.Core

[<Erase; AutoOpen>]
module private ScheduledSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/scheduled"

        [<Literal>]
        let version = "1.5.0"

open Spec


[<AllowNullLiteral; Interface>]
type Schedule<'T> =
    [<Emit("$0($1)")>]
    abstract member exec: 'T -> unit

    abstract member clear: unit -> unit with get

type DebounceOrThrottle<'T> = ('T -> unit) * int -> Schedule<'T>

[<Erase>]
type Scheduled =
    [<ImportMember(path)>]
    static member debounce(callback: 'T -> unit, timespan: int) : Schedule<'T> = jsNative

    [<ImportMember(path)>]
    static member throttle(callback: 'T -> unit, timespan: int) : Schedule<'T> = jsNative

    [<ImportMember(path)>]
    static member scheduleIdle(callback: 'T -> unit, timespan: int) : Schedule<'T> = jsNative

    [<ImportMember(path)>]
    static member leading(debOrThrot: DebounceOrThrottle<'T>, callback: 'T -> unit, timespan: int) : Schedule<'T> =
        jsNative

    [<ImportMember(path)>]
    static member leadingAndTrailing
        (debOrThrot: DebounceOrThrottle<'T>, callback: 'T -> unit, timespan: int)
        : Schedule<'T> =
        jsNative
// [<ImportMember(path)>]
// static member createScheduled (schedule: ('T -> unit) -> ()
