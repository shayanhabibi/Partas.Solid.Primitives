namespace Partas.Solid.Primitives.Clipboard

open Partas.Solid.Primitives
open Fable.Core

[<Erase>]
module Spec =
    let [<Literal>] path = "@solid-primitives/clipboard"
    let [<Literal>] version = ""

open Spec

   
[<Erase; AutoOpen>]
type Clipboard =
    [<ImportMember(path)>]
    static member readClipboard () = jsNative
    
    [<ImportMember(path)>]
    static member writeClipboard (input: string): unit = jsNative
    
    [<ImportMember(path)>]
    static member createClipboard () = jsNative
    
    [<ImportMember(path)>]
    static member copyToClipboard () = jsNative
    
    [<ImportMember(path)>]
    static member newItem () = jsNative