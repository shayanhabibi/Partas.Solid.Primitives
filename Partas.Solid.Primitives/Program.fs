namespace Partas.Solid.Primitives

open Fable.Core
// Stub
module internal __NOOP =
    [<Global>]
    let mutable x: int = 5

    [<Global>]
    let noop = if x = 5 then () else ()
