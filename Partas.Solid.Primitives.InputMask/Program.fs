namespace Partas.Solid.Primitives

open System.Text.RegularExpressions
open Partas.Solid
open Fable.Core
open Browser.Types

[<Erase; AutoOpen>]
module private InputMaskSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/input-mask"
        [<Literal>]
        let version = "0.3.1"

[<Erase>]
module InputMask =
    [<Erase>]
    type Selection = Selection of start: float * finish: float
    [<Erase>]
    type InputMaskFn = delegate of value: string * selection: Selection -> value: string * selection: Selection
    [<Erase>]
    type InputMaskArray = InputMaskArray of U2<string, Regex>[]
    [<Erase>]
    type InputMaskRegex = InputMaskRegex of regex: Regex * replacer: (obj -> string)
    [<Erase; RequireQualifiedAccess>]
    type InputMask =
        | Fn of InputMaskFn
        | Array of InputMaskArray
        | Regex of InputMaskRegex
        | String of string
    [<ImportMember(Spec.path)>]
    let stringMaskRegExp: Map<string, Regex> = jsNative
    
[<AutoOpen; Erase>]
type InputMask =
    [<ImportMember(Spec.path)>]
    static member stringMaskToArray(mask: string, ?regexps: Map<string, Regex>): InputMask.InputMaskArray = jsNative
    [<ImportMember(Spec.path)>]
    static member regexMaskToFn(regex: Regex, replacer: obj): InputMask.InputMaskFn = jsNative
    [<ImportMember(Spec.path)>]
    static member maskArrayToFn(maskArray: InputMask.InputMaskArray): InputMask.InputMaskFn = jsNative
    [<ImportMember(Spec.path)>]
    static member anyMaskToFn(mask: InputMask.InputMask, ?regexps: Map<string, Regex>): InputMask.InputMaskFn = jsNative
    [<ImportMember(Spec.path)>]
    static member createInputMask<'MaskEvent>(mask: InputMask.InputMask, ?regexps: Map<string, Regex>): 'MaskEvent -> string = jsNative
    static member inline createKeyboardInputMask(mask: InputMask.InputMask, ?regexps: Map<string, Regex>) =
        InputMask.createInputMask<KeyboardEvent>(mask, ?regexps = regexps)
    static member inline createInputInputMask(mask: InputMask.InputMask, ?regexps: Map<string, Regex>) =
        InputMask.createInputMask<InputEvent>(mask, ?regexps = regexps)
    static member inline createClipboardInputMask(mask: InputMask.InputMask, ?regexps: Map<string, Regex>) =
        InputMask.createInputMask<ClipboardEvent>(mask, ?regexps = regexps)
    [<ImportMember(Spec.path)>]
    static member createMaskPattern<'MaskEvent>(inputMask: 'MaskEvent -> string, pattern: string -> string): 'MaskEvent -> string = jsNative
    static member inline createKeyboardMaskPattern(inputMask: KeyboardEvent -> string, pattern: string -> string) =
        InputMask.createMaskPattern<KeyboardEvent>(inputMask,pattern)
    static member inline createInputMaskPattern(inputMask: InputEvent -> string, pattern: string -> string) =
        InputMask.createMaskPattern<InputEvent>(inputMask,pattern)
    static member inline createClipboardMaskPattern(inputMask: ClipboardEvent -> string, pattern: string -> string) =
        InputMask.createMaskPattern<ClipboardEvent>(inputMask,pattern)
