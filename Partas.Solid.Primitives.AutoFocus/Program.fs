namespace Partas.Solid.Primitives.AutoFocus

open Partas.Solid
open Fable.Core
open Browser.Types
open Partas.Solid.Primitives

[<Erase>]
module Spec =
    let [<Literal>] path = Common.path + "autofocus"
    let [<Literal>] version = ""

open Spec

[<Erase; AutoOpen>]
type AutoFocus =
    /// <summary>
    /// The <c>autofocus</c> directive uses the native <c>autofocus</c> attribute to determine it should focus
    /// the element or not. Using this directive without <c>autofocus={true}</c> (or the shorthand, <c>autofocus</c>)
    /// will not perform anything.
    /// </summary>
    /// <example>
    /// <code>
    /// // As a directive
    /// button(autofocus = true).use(autofocus)
    /// // As a ref
    /// button(autofocus = true).ref(autofocus)
    /// </code>
    /// </example>
    [<ImportMember(path)>]
    static member autofocus = jsNative
    
    /// <summary>
    /// Reactively autofocuses an element passid in as a signal
    /// </summary>
    /// <example><code>
    /// import { createAutofocus } from "@solid-primitives/autofocus";
    /// // Using ref
    /// let ref!: HTMLButtonElement;
    /// createAutofocus(() => ref);
    /// 
    /// /button ref={ref}>Autofocused /button>;
    /// 
    /// // Using ref signal
    /// const [ref, setRef] = createSignal /HTMLButtonElement>();
    /// createAutofocus(ref);
    /// 
    /// /button ref={setRef}>Autofocused /button>;
    /// </code></example>
    [<ImportMember(path)>]
    static member createAutofocus (ref: unit -> #HtmlElement): unit = jsNative