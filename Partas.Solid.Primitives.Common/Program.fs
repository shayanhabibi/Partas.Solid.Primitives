[<AutoOpen>]
module Partas.Solid.Primitives.Common

open Fable.Core

[<Erase>]
module Spec =
    let [<Literal>] primitives = "@solid-primitives/"
    let [<Literal>] utils = primitives + "utils"

type DisposeCallback = unit -> unit
