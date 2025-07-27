[<AutoOpen>]
module Partas.Solid.Primitives.Common

open Fable.Core

[<Erase>]
module Spec =
    [<Literal>]
    let primitives = "@solid-primitives/"

    [<Literal>]
    let utils = primitives + "utils"

type DisposeCallback = unit -> unit
