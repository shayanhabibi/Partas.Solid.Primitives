namespace Partas.Solid.Primitives.Trigger

open Partas.Solid.Primitives
open Fable.Core
open Fable.Core.JS
open System.Runtime.CompilerServices

[<Erase>]
module Spec =
    let [<Literal>] path = "@solid-primitives/trigger"
    let [<Literal>] version = ""
    
open Spec

type [<Erase>] Track<'T> = 'T -> unit
type [<Erase>] Dirty<'T> = 'T -> unit
type [<Erase>] DirtyAll = unit -> unit
type [<Erase>] TriggerSignal<'T> = Track<'T> * Dirty<'T>
type [<Erase>] TriggerCacheSignal<'T> = Track<'T> * Dirty<'T> * DirtyAll

[<AutoOpen; Erase>]
type Extensions =
    /// <summary>
    /// Will track the trigger of the given key for the cache
    /// </summary>
    /// <param name="triggerCache"></param>
    /// <param name="key">the key</param>
    [<Erase; Extension>]
    static member track (triggerCache: TriggerCacheSignal<'T>, key: 'T): unit = undefined
    /// <summary>
    /// Will trigger the tracker for the given key of the cache
    /// </summary>
    /// <param name="triggerCache"></param>
    /// <param name="key">the key</param>
    [<Erase; Extension>]
    static member dirty (triggerCache: TriggerCacheSignal<'T>, key: 'T): unit = undefined

[<Erase; AutoOpen>]
type Trigger =
    /// <summary>
    /// Set listeners in reactive computations and then trigger them when you want.
    /// </summary>
    /// <example><code>
    /// let track,dirty = createTrigger()
    /// createEffect(fun() ->
    ///     track() // 'read' track
    ///     // ...
    /// )
    /// // later
    /// dirty()
    /// </code></example>
    [<ImportMember(path)>]
    static member createTrigger (): TriggerSignal<unit> = jsNative
    /// <summary>
    /// Creates a cache of triggers that can be used to mark dirty only specific keys.
    /// <br/><br/>Cache is a Map or WeakMap depending on the mapConstructor argument. (default: Map)
    /// <br/><br/>If mapConstructor is WeakMap then the cache will be weak and the keys will be garbage collected when they are no longer referenced.
    /// <br/><br/>Trigger signals added to the cache only when tracked under a computation, and get deleted from the cache when they are no longer tracked.
    /// </summary>
    /// <example><code>
    /// let map = createTriggerCache[int]()
    /// createEffect(fun() ->
    ///     map.track(1) // 'read' track
    ///     // ...
    /// )
    /// // later
    /// map.dirty(1)
    /// </code></example>
    [<ImportMember(path)>]
    static member createTriggerCache<'T> (): TriggerCacheSignal<'T> = jsNative