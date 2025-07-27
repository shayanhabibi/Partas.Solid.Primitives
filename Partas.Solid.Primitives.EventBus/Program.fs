namespace Partas.Solid.Primitives

open Fable.Core

[<Erase; AutoOpen>]
module private EventBusSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/event-bus"

/// <summary>
/// Provides all the base functions of an event-emitter, plus additional functions for managing listeners, it's behavior could be customized with an config object. Good for advanced usage.
/// </summary>
/// <code>
/// import { createEventBus } from "@solid-primitives/event-bus";
///
/// const bus = createEventBus:string:();
///
/// // can be used without payload type, if you don't want to send any
/// createEventBus();
///
/// // bus can be destructured:
/// const { listen, emit, clear } = bus;
///
/// const unsub = bus.listen(a => console.log(a));
///
/// bus.emit("foo");
///
/// // unsub gets called automatically on cleanup
/// unsub();
/// </code>
[<Erase>]
type EventBus<'T> =
    abstract listen: ('T -> unit) -> DisposeCallback
    abstract emit: 'T -> unit
    abstract clear: unit -> unit

/// <summary>
/// An emitter which you can listen to and emit various events.
/// </summary>
/// <code>
/// import { createEmitter } from "@solid-primitives/event-bus";
///
/// const emitter = createEmitter:{
///   foo: number;
///   bar: string;
/// }:();
/// // can be destructured
/// const { on, emit, clear } = emitter;
///
/// emitter.on("foo", e => {});
/// emitter.on("bar", e => {});
///
/// emitter.emit("foo", 0);
/// emitter.emit("bar", "hello");
///
/// // unsub gets called automatically on cleanup
/// unsub();
/// </code>
[<Erase>]
type Emitter<'MessageTyper> =
    abstract on: (string * (obj -> unit)) -> DisposeCallback
    abstract emit: (string * obj) -> unit
    abstract clear: unit -> unit

/// <summary>
/// Typesafe version of the Emitter made for F#/Fable.
/// It uses the path from the type to its member as the key of the emission. Because we provide a typed path, we
/// get the benefit of having the member being typed as the message type.
/// That is to say, there is nothing stopping you (on the js side) from sending the wrong type through.
/// </summary>
[<Erase>]
type MappedEmitter<'MessageMapper> =
    [<Erase; Emit("$0.on($1, $2)")>]
    member this.zzz_onImplementation(key: string, callback: obj) : DisposeCallback = jsNative

    member inline this.on (mapping: 'MessageMapper -> 'MessageType) (callback: 'MessageType -> unit) : DisposeCallback =
        this.zzz_onImplementation (
            JsInterop.emitJsExpr (Experimental.namesofLambda (mapping)) "$0.join('.')",
            unbox callback
        )

    [<Erase; Emit("$0.emit($1, $2)")>]
    member this.zzz_emitImplementation(key: string, value: obj) : unit = jsNative

    member inline this.emit (mapping: 'MessageMapper -> 'MessageType) (message: 'MessageType) : unit =
        this.zzz_emitImplementation (
            JsInterop.emitJsExpr (Experimental.namesofLambda (mapping)) "$0.join('.')",
            unbox message
        )

    [<Erase>]
    member this.clear() : unit = ()

/// <summary>
/// Wrapper around createEmitter.<br/><br/>
/// Creates an emitter with which you can listen to and emit various events. With this emitter you can also listen to all events.
/// </summary>
/// <code>
/// import { createGlobalEmitter } from "@solid-primitives/event-bus";
///
/// const emitter = createGlobalEmitter:{
///   foo: number;
///   bar: string;
/// }:();
/// // can be destructured
/// const { on, emit, clear, listen } = emitter;
///
/// emitter.on("foo", e => {});
/// emitter.on("bar", e => {});
///
/// emitter.emit("foo", 0);
/// emitter.emit("bar", "hello");
///
/// // global listener - listens to all channels
/// emitter.listen(e => {
///   switch (e.name) {
///     case "foo": {
///       e.details;
///       break;
///     }
///     case "bar": {
///       e.details;
///       break;
///     }
///   }
/// });
/// </code>
[<Erase>]
type GlobalEmitter<'T> =
    inherit Emitter<'T>
    abstract listen: (obj -> unit) -> DisposeCallback

/// <summary>
/// Provides helpers for using a group of event buses.<br/><br/>
/// Can be used with createEventBus, createEventStack or any emitter that has the same api.
/// </summary>
/// <code>
/// How to use it
/// /// Creating EventHub
/// import { createEventHub } from "@solid-primitives/event-bus";
///
/// // by passing an record of Channels
/// const hub = createEventHub({
///   busA: createEventBus(),
///   busB: createEventBus:string:(),
///   busC: createEventStack:{ text: string }:(),
/// });
///
/// // by passing a function
/// const hub = createEventHub(bus =@ ({
///   busA: bus:number:(),
///   busB: bus:string:(),
///   busC: createEventStack:{ text: string }:(),
/// }));
///
/// // hub can be destructured
/// const { busA, busB, on, emit, listen, value } = hub;
/// /// Listening  Emitting
/// const hub = createEventHub({
///   busA: createEventBus:void:(),
///   busB: createEventBus:string:(),
///   busC: createEventStack:{ text: string }:(),
/// });
/// // can be destructured
/// const { busA, busB, on, listen, emit } = hub;
///
/// hub.on("busA", e =@ {});
/// hub.on("busB", e =@ {});
///
/// hub.emit("busA", 0);
/// hub.emit("busB", "foo");
///
/// // global listener - listens to all channels
/// hub.listen(e =@ {
///   switch (e.name) {
///     case "busA": {
///       e.details;
///       break;
///     }
///     case "busB": {
///       e.details;
///       break;
///     }
///   }
/// });
/// /// Accessing values
/// // If a emitter returns an accessor value, it will be available in a .value store.
///
/// hub.value.myBus;
/// // same as
/// hub.myBus.value();
/// </code>
[<Erase>]
type EventHub<'T> =
    inherit GlobalEmitter<'T>
//
// [<Erase>]
// type EventStackParameters<'Message> =
//     abstract event: Event
// [<Erase>]
// type EventStackListener<'Message> = EventStackParameters<'Message> -> unit
// [<Erase>]
// type EventStack<'Event, 'PackagedEvent> =
//     inherit GlobalEmitter<'Event>
//
// type EventStackSimple<'Event> = EventStack<'Event, 'Event>

[<Erase; AutoOpen>]
type EventBus =
    [<ImportMember(Spec.path)>]
    static member createEventBus<'T>() : EventBus<'T> = jsNative

    [<ImportMember(Spec.path)>]
    static member createEmitter<'T>() : Emitter<'T> = jsNative

    [<Import("createEmitter", Spec.path)>]
    static member createMappedEmitter<'MessageSchema>() : MappedEmitter<'MessageSchema> = jsNative
    //todo createEventStack & utils
    [<ImportMember(Spec.path)>]
    static member createEventHub<'T>([<OptionalArgument>] channels: 'T) : EventHub<'T> = jsNative
