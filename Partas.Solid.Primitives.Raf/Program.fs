namespace Partas.Solid.Primitives.Raf

open Fable.Core
open Partas.Solid
open Partas.Solid.Experimental.U

[<Erase>]
module Spec =
    let [<Erase; Literal>] path = "@solid-primitives/raf"
    let [<Erase; Literal>] version = "2.3.1"

open Spec

type FrameRequestCallback = float -> unit
[<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
type StartVoidFunction = (unit -> unit)
[<System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)>]
type StopVoidFunction = (unit -> unit)

[<AllowNullLiteral>]
[<Interface>]
type MsCounter =
    [<Emit("$0()")>]
    abstract member current: int with get
    abstract member reset: (unit -> unit) with get
    abstract member running: (unit -> bool) with get
    abstract member start: (unit -> unit) with get
    abstract member stop: (unit -> unit) with get


[<Erase; AutoOpen>]
type Raf =
    /// <summary>
    /// A primitive creating reactive <c>window.requestAnimationFrame</c>, that is automatically disposed onCleanup.
    /// </summary>
    /// <example><code>
    /// const [running, start, stop] = createRAF((timestamp) => {
    ///    el.style.transform = "translateX(...)"
    /// });
    /// </code></example>
    /// <param name="callback">
    /// The callback to run each frame
    /// </param>
    /// <returns>
    /// Returns a signal if currently running as well as start and stop methods
    /// <code lang="ts">
    /// [running: Accessor&lt;boolean>, start: VoidFunction, stop: VoidFunction]
    /// </code>
    /// </returns>
    [<Import("createRAF", path)>]
    static member createRAF (callback: FrameRequestCallback) : Accessor<bool> * StartVoidFunction * StopVoidFunction = nativeOnly
    /// <summary>
    /// A primitive for wrapping <c>window.requestAnimationFrame</c> callback function to limit the execution of the callback to specified number of FPS.
    ///
    /// Keep in mind that limiting FPS is achieved by not executing a callback if the frames are above defined limit. This can lead to not consistant frame duration.
    /// </summary>
    /// <example><code>
    /// const [running, start, stop] = createRAF(
    ///   targetFPS(() => {...}, 60)
    /// );
    /// </code></example>
    /// <param name="callback">
    /// The callback to run each *allowed* frame
    /// </param>
    /// <param name="fps">
    /// The target FPS limit
    /// </param>
    /// <returns>
    /// Wrapped RAF callback
    /// </returns>
    [<Import("targetFPS", path)>]
    static member targetFPS (callback: FrameRequestCallback, fps: float) : FrameRequestCallback = nativeOnly
    /// <summary>
    /// A primitive for wrapping <c>window.requestAnimationFrame</c> callback function to limit the execution of the callback to specified number of FPS.
    ///
    /// Keep in mind that limiting FPS is achieved by not executing a callback if the frames are above defined limit. This can lead to not consistant frame duration.
    /// </summary>
    /// <example><code>
    /// const [running, start, stop] = createRAF(
    ///   targetFPS(() => {...}, 60)
    /// );
    /// </code></example>
    /// <param name="callback">
    /// The callback to run each *allowed* frame
    /// </param>
    /// <param name="fps">
    /// The target FPS limit
    /// </param>
    /// <returns>
    /// Wrapped RAF callback
    /// </returns>
    [<Import("targetFPS", path)>]
    static member targetFPS (callback: FrameRequestCallback, fps: Accessor<float>) : FrameRequestCallback = nativeOnly
    /// <summary>
    /// A primitive that creates a signal counting up milliseconds with a given frame rate to base your animations on.
    /// </summary>
    /// <param name="fps">
    /// the frame rate, either as Accessor or number
    /// </param>
    /// <param name="limit">
    /// an optional limit, either as Accessor or number, after which the counter is reset
    /// </param>
    /// <returns>
    /// an Accessor returning the current number of milliseconds and the following methods:
    /// - <c>reset()</c>: manually resetting the counter
    /// - <c>running()</c>: returns if the counter is currently setRunning
    /// - <c>start()</c>: restarts the counter if stopped
    /// - <c>stop()</c>: stops the counter if running
    ///
    /// <code lang="ts">
    /// const ms = createMs(60);
    /// createEffect(() => ms() > 500000 ? ms.stop());
    /// return &lt;rect x="0" y="0" height="10" width={Math.min(100, ms() / 5000)} />
    /// </code>
    /// </returns>
    /// <remarks>
    /// Contrary to the original implementation, the binding accesses the current value using <c>ms.current</c> instead of <c>ms()</c>
    /// </remarks>
    [<Import("createMs", path)>]
    static member createMs (fps: U4<float, Accessor<float>, int, Accessor<int>>, ?limit: U4<float, Accessor<float>, int, Accessor<int>>): MsCounter = nativeOnly
