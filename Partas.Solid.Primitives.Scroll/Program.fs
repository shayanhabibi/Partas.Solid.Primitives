namespace Partas.Solid.Primitives

open Partas.Solid
open Fable.Core

[<Erase; AutoOpen>]
module private ScrollSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/scroll"

        [<Literal>]
        let version = "2.1.0"

open Spec

[<AllowNullLiteral; Interface>]
type ScrollPosition =
    abstract member x: int with get
    abstract member y: int with get

[<Erase>]
type Scroll =
    /// Default target of window
    [<ImportMember(path)>]
    static member createScrollPosition() : Accessor<ScrollPosition> = jsNative

    /// Element ref target or Accessor<#HtmlElement>
    [<ImportMember(path)>]
    static member createScrollPosition(element: unit -> #HtmlElement) : Accessor<ScrollPosition> = jsNative

    /// Returns reactive object with current window scroll position; signals and event-listeners are shared
    /// between dependendents making it more optimised to use in multiple places at once
    [<ImportMember(path)>]
    static member useWindowScrollPosition() : ScrollPosition = jsNative

    /// <summary>
    /// Gets a <c>ScrollPosition</c> element/window scroll position
    /// </summary>
    [<ImportMember(path)>]
    static member getScrollPosition() : ScrollPosition = jsNative
