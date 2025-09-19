namespace Partas.Solid.Primitives

open System
open Fable.Core
open Fable.Core.JS
open Fable.Core.JsInterop
open Partas.Solid
[<Erase; AutoOpen>]
module StorageSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/storage"
        [<Literal>]
        let version = "4.3.2"

type SyncStorage =
    abstract getItem: key: string -> string option
    abstract setItem: key: string * value: string -> unit
    abstract removeItem: key: string -> unit
    [<EmitIndexer>]
    abstract Item: string -> obj
type AsyncStorage =
    abstract getItem: key: string -> Promise<string option>
    abstract setItem: key: string * value: string -> Promise<obj>
    abstract removeItem: key: string -> Promise<unit>
    [<EmitIndexer>]
    abstract Item: string -> obj
type SyncStorageWithOptions<'Options> =
    abstract getItem: key: string * ?options: 'Options -> string option
    abstract setItem: key: string * value: string * ?options: 'Options -> unit
    abstract removeItem: key: string * ?options: 'Options -> unit
    [<EmitIndexer>]
    abstract Item: string -> obj
type AsyncStorageWithOptions<'Options> =
    abstract getItem: key: string * ?options: 'Options -> Promise<string option>
    abstract setItem: key: string * value: string * ?options: 'Options -> Promise<obj>
    abstract removeItem: key: string * ?options: 'Options -> Promise<unit>
    [<EmitIndexer>]
    abstract Item: string -> obj

type PersistenceSyncData =
    abstract key: string with get,set
    abstract newValue: string option with get,set
    abstract timeStamp: float with get,set
    abstract url: string option with get,set
type PersistenceSyncCallback = PersistenceSyncData -> unit
type PersistenceSyncSubscribe = PersistenceSyncCallback -> unit
type PersistenceSyncUpdate = string * string option -> unit
type PersistenceSyncAPI = PersistenceSyncSubscribe * PersistenceSyncUpdate
[<Pojo>]
type PersistenceOptions<'T, 'Options>(
    ?name: string
    ,?serialize: 'T -> string
    ,?deserialize: string -> 'T
    ,?sync: PersistenceSyncAPI
    ,?storage: U4<SyncStorage, AsyncStorage, SyncStorageWithOptions<'Options>, AsyncStorageWithOptions<'Options>>
    ,?storageOptions: 'Options
    ) =
    [<DefaultValue>]
    val mutable name: string
    [<DefaultValue>]
    val mutable serialize: 'T -> string
    [<DefaultValue>]
    val mutable deserialize: string -> 'T
    [<DefaultValue>]
    val mutable sync: PersistenceSyncAPI
    [<DefaultValue>]
    val mutable storage: U4<SyncStorage, AsyncStorage, SyncStorageWithOptions<'Options>, AsyncStorageWithOptions<'Options>>
    [<DefaultValue>]
    val mutable storageOptions: 'Options

type PersistedState<'T> = Accessor<'T> * Setter<'T> * obj


[<Erase; AutoOpen>]
type Storage =
    [<ImportMember(Spec.path)>]
    static member makePersisted<'T>(signal: Signal<'T>, ?options: PersistenceOptions<'T, _>): PersistedState<'T> = jsNative
    [<ImportMember(Spec.path)>]
    static member storageSync: PersistenceSyncAPI = jsNative
    // TODO broadcast channel overload
    [<ImportMember(Spec.path)>]
    static member messageSync(?channel: Browser.Types.Window): PersistenceSyncAPI = jsNative
    // TODO
    // [<ImportMember(Spec.path)>]
    // static member wsSync(ws: WebSocket, ?warnOnError: bool): PersistenceSyncAPI = jsNative
    [<ImportMember(Spec.path)>]
    static member multiplexSync([<ParamArray>] syncAPIs: PersistenceSyncAPI[]): PersistenceSyncAPI = jsNative
