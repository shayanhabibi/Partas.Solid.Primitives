namespace Partas.Solid.Primitives
open Fable.Core
open Fable.Core.JsInterop


[<Erase;AutoOpen>]
module PermissionSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/permission"
        [<Literal>]
        let version = "1.3.1"
    
[<StringEnum(CaseRules.KebabCase)>]
type PermissionName =
    | Geolocation
    | Midi
    | Notifications
    | PersistentStorage
    | Push
    | ScreenWakeLock
    | StorageAccess
    | Microphone
    | Camera

[<StringEnum>]
type PermissionState =
    | Granted
    | Denied
    | Prompt
    | Unknown

[<AllowNullLiteral>]
type PermissionDescriptor =
    abstract name: PermissionName with get,set

[<Erase; AutoOpen>]
type Permission =
    /// <summary>
    /// Queries the permission API
    /// </summary>
    /// <param name="name"></param>
    [<ImportMember(Spec.path)>]
    static member createPermission(name: U2<PermissionDescriptor, PermissionName>): unit -> PermissionState = jsNative
    
    
