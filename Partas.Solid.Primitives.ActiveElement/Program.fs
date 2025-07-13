namespace Partas.Solid.Primitives

open Partas.Solid
open Fable.Core

[<Erase>]
module private ActiveElementSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/active-element"
        let [<Literal>] version = ""

open ActiveElementSpec

[<Erase; AutoOpen>]
type ActiveElement =
    /// <summary>
    /// Listen for changes to the <c>document.activeElement</c>
    /// </summary>
    /// <remarks>
    /// non reactive
    /// </remarks>
    [<ImportMember(Spec.path)>]
    static member makeActiveElementListener (handler: #HtmlElement -> unit): DisposeCallback = jsNative
    
    /// <summary>
    /// Attaches "blur" and "focus" event listeners to the element
    /// </summary>
    /// <param name="target"></param>
    /// <param name="callBack"></param>
    /// <param name="useCapture"></param>
    [<ImportMember(Spec.path)>]
    static member makeFocusListener (target: #HtmlElement, callBack: bool -> unit, ?useCapture: bool): DisposeCallback = jsNative
    
    /// <summary>
    /// Provides a reactive signal of <c>document.activeElement</c>. Check which element is currently focused.
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createActiveElement (): Accessor<#HtmlElement> = jsNative
    
    /// <summary>
    /// Provides a signal representing element's focus state
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createFocusSignal(target: #HtmlElement): Accessor<bool> = jsNative
    
    /// <summary>
    /// Provides a signal representing element's focus state
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createFocusSignal(target: Accessor<#HtmlElement>): Accessor<bool> = jsNative
    
    // /// <summary>
    // /// A directive that notifies you when the element becomes active or inactive
    // /// </summary>
    // [<ImportMember(Spec.path)>]
    // static member focus: string = jsNative
