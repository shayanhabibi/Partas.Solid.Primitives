namespace Partas.Solid.Primitives.Timer

open Partas.Solid.Primitives
open Partas.Solid
open Fable.Core
open Browser.Types

[<Erase>]
module Spec =
    let [<Literal>] path = Common.path + "timer"
    let [<Literal>] version = "1.4.0"

open Spec

[<Erase>]
type IntervalOrTimeout = (unit -> unit) -> int -> int

[<Erase; AutoOpen>]
type Timer =
    /// Makes an automatically cleaned up timer. Takes a callback, the timespan, and then either the
    /// the function `setInterval` or `setTimeout`
    [<ImportMember(path)>]
    static member makeTimer (callback: unit -> unit, timespan: int, policy: IntervalOrTimeout): DisposeCallback = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: int, policy: IntervalOrTimeout): unit = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: unit -> U2<bool, int>, policy: IntervalOrTimeout): unit = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: unit -> int, policy: IntervalOrTimeout): unit = jsNative
    /// makeTimer but with a fully reactive delay. The delay can also be set to 'false' in which case the timer is disabled.
    [<ImportMember(path)>]
    static member createTimer (callback: unit -> unit, timespan: unit -> bool, policy: IntervalOrTimeout): unit = jsNative
    /// <summary>
    /// Similar to an interval created with createTimer, but the delay does not update until the callback is executed
    /// </summary>
    /// <example><code>
    /// let cb = fun () -> ()
    /// let delay,setDelay = createSignal(1000)
    /// createTimeoutLoop(cb, delay)
    /// //...
    /// 500 |> setDelay
    /// </code></example>
    [<ImportMember(path)>]
    static member createTimeoutLoop (callback: unit -> unit, timespan: int): unit = jsNative
    [<ImportMember(path)>]
    static member createTimeoutLoop (callback: unit -> unit, timespan: Accessor<int>): unit = jsNative
    /// <summary>
    /// Periodically polls a function, returning an accessor to its last return value.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createPolled(callback: unit -> 'T, timespan: int): Accessor<'T> = jsNative
    /// <summary>
    /// Periodically polls a function, returning an accessor to its last return value.
    /// </summary>
    /// <param name="callback"></param>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createPolled(callback: unit -> 'T, timespan: Accessor<int>): Accessor<'T> = jsNative
    /// <summary>
    /// A counter which increments periodically on the delay.
    /// </summary>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createIntervalCounter(timespan: int): Accessor<int> = jsNative
    /// <summary>
    /// A counter which increments periodically on the delay.
    /// </summary>
    /// <param name="timespan"></param>
    [<ImportMember(path)>]
    static member createIntervalCounter(timespan: Accessor<int>): Accessor<int> = jsNative