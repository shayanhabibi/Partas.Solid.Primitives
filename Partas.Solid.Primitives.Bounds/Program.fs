namespace Partas.Solid.Primitives.Bounds
open Partas.Solid.Primitives
open Fable.Core
open Partas.Solid
open Browser.Types

[<Erase>]
module Spec =
    let [<Literal>] path = Common.path + "bounds"
    let [<Literal>] version = "0.1.0"

open Spec

[<Interface; AllowNullLiteral>]
type ElementBounds =
    abstract width: int with get
    abstract height: int with get
    abstract top: int with get
    abstract left: int with get
    abstract right: int with get
    abstract bottom: int with get
[<Erase; AutoOpen>]
type Bounds =
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    /// <param name="trackScroll">Listen to window scroll events</param>
    /// <param name="trackMutation">Listen to changes to the dom structure/styles</param>
    /// <param name="trackResize">Listen to changes to the element's resize events</param>
    /// <remarks>All options are 'truthy' by default</remarks>
    [<ImportMember(path); ParamObject(1)>]
    static member createElementBounds(target: #HTMLElement,
                                      ?trackScroll: bool,
                                      ?trackMutation: bool,
                                      ?trackResize: bool): ElementBounds = jsNative
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    [<ImportMember(path)>]
    static member createElementBounds(target: #HTMLElement): ElementBounds = jsNative
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    /// <param name="trackScroll">Listen to window scroll events</param>
    /// <param name="trackMutation">Listen to changes to the dom structure/styles</param>
    /// <param name="trackResize">Listen to changes to the element's resize events</param>
    /// <remarks>All options are 'truthy' by default</remarks>
    [<ImportMember(path); ParamObject(1)>]
    static member createElementBounds(target: Accessor<#HTMLElement>,
                                      ?trackScroll: bool,
                                      ?trackMutation: bool,
                                      ?trackResize: bool): ElementBounds = jsNative
    /// <summary>
    /// Creates a reactive store-like object of current element bounds — position on the screen, and size dimensions. Bounds will be automatically updated on scroll, resize events and updates to the DOM.
    /// </summary>
    /// <param name="target">Ref or reactive ref element</param>
    [<ImportMember(path)>]
    static member createElementBounds(target: Accessor<#HTMLElement>): ElementBounds = jsNative
    
