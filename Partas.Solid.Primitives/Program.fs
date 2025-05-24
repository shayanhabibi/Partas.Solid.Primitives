namespace Partas.Solid.Primitives

open Partas.Solid
open Fable.Core

[<Erase; AutoOpen>]
module private Internal =
    let [<Literal>] solidPrimitives = "@solid-primitives/" 
    
[<Erase>]
module Utils =
    let [<Literal>] private path' = solidPrimitives + "utils"
    let [<Literal>] path = path' + "/"

[<Erase>]
module Common =
    let [<Literal>] path = solidPrimitives 
    
/// Call this function to cleanup the resources of its originator
type DisposeCallback = (unit -> unit)