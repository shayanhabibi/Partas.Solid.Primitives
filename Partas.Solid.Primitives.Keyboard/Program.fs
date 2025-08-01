﻿namespace Partas.Solid.Primitives

open Partas.Solid
open Fable.Core
open Browser.Types

[<Erase; AutoOpen>]
module private KeyboardSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/keyboard"

        [<Literal>]
        let version = ""

open Spec

[<Erase; AutoOpen>]
type Keyboard =
    [<ImportMember(path)>]
    static member useKeyDownEvent() : Accessor<KeyboardEvent> = jsNative

    [<ImportMember(path)>]
    static member useKeyDownList() : Accessor<string[]> = jsNative

    [<ImportMember(path)>]
    static member useCurrentlyHeldKey() : Accessor<string | null> = jsNative

    [<ImportMember(path)>]
    static member useKeyDownSequence() : Accessor<string[][]> = jsNative

    [<ImportMember(path)>]
    static member createKeyHold(key: string) : Accessor<bool> = jsNative

    [<ImportMember(path); ParamObject(1)>]
    static member createKeyHold(key: string, preventDefault: bool) : Accessor<bool> = jsNative

    [<ImportMember(path)>]
    static member createShortcut(keys: string[], handler: unit -> unit) : unit = jsNative

    [<ImportMember(path); ParamObject(2)>]
    static member createShortcut
        (keys: string[], handler: unit -> unit, ?preventDefault: bool, ?requireReset: bool)
        : unit =
        jsNative
