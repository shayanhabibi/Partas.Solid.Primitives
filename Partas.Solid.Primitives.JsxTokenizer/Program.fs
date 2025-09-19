namespace Partas.Solid.Primitives
open Partas.Solid
open Fable.Core
open Fable.Core.JS

#nowarn 1182

[<Erase; AutoOpen>]
module JsxTokenizerSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/jsx-tokenizer"
        [<Literal>]
        let version = "1.1.1"

/// <summary>
/// A tokenizer can be used to create multiple token components with the same
/// id and resolve their data from the JSX element structure.
/// </summary>
type JSXTokenizer<'Data> = interface end
/// <summary>
/// Resolved token returned by <c>resolveTokens</c>. Access data using the named property.
/// </summary>
type TokenElement<'Data> =
    abstract data: 'Data with get,set
    abstract ``$TOKENIZER``: obj with get,set
    
/// <summary>
/// Type of the component returned by <c>createToken</c>
/// </summary>
type TokenComponent<'Props, 'Data> =
    inherit HtmlElement
    inherit JSXTokenizer<'Data>
/// <summary>
/// Type of the component returned by <c>createToken</c>
/// </summary>
type TokenComponent<'Props> = TokenComponent<'Props, 'Props>

[<AutoOpen; Erase>]
module AutoOpenJsxTokenizerExtensions =
    type JSXTokenizer<'Data> with
        member _.``$TOKENIZER``
            with get(): obj = unbox null
            and set(value: obj) = ()
        member _.``$TYPE``
            with get(): 'Data = unbox null
            and set(value: 'Data) = ()

[<AutoOpen; Erase>]
type JsxTokenizer =
    /// <summary>
    /// Creates a JSX Tokenizer that can be used to create multiple token components
    /// with the same id and resolve their data from the JSX Element structure.
    /// </summary>
    /// <param name="name">The name of the parser used for debugging.</param>
    [<ImportMember(Spec.path); ParamObject(0)>]
    static member createTokenizer<'Data>(name: string): JSXTokenizer<'Data> = jsNative
    /// <summary>
    /// Creates a JSX Tokenizer that can be used to create multiple token components
    /// with the same id and resolve their data from the JSX Element structure.
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createTokenizer<'Data>(): JSXTokenizer<'Data> = jsNative
    /// <summary>
    /// Creates a token component for passing custom data through JSX structure.
    /// The token component can be used as a normal component in JSX. When resolved
    /// by <c>resolveTokens</c> it will return the data passed to it. But when resolved
    /// normally (eg using the <c>children()</c> helper) it will return the fallback JSX Element.
    /// </summary>
    /// <remarks>
    /// From a <c>Partas.Solid</c> perspective, the advantage here is leveraging the
    /// plugins robust tag construction mechanisms to improve bindings. This is primarily
    /// because we do not have to concern ourselves with writing POJOs with parameters in
    /// both the constructor and as properties.<br/><br/>
    /// Further to this, we can also use interfaces more intuitively.
    /// </remarks>
    /// <param name="tokenizer">Identity object returned by <c>createTokenizer</c> or other
    /// <c>TokenComponent</c> (if not passed, a new tokenizer id will be created)</param>
    /// <param name="tokenData">Function that returns the data of the token. (if one isn't passed, props will be used as data)</param>
    /// <param name="render">Function that returns the fallback JSX Element to render. (if not passed, the token will
    /// render nothing and warn in development)</param>
    /// <returns>A <c>TokenComponent</c> that can be used as a normal component in JSX.</returns>
    [<ImportMember(Spec.path)>]
    static member createToken<'Props, 'Data>(tokenizer: JSXTokenizer<'Data>, tokenData: 'Props -> 'Data, ?render: 'Props -> HtmlElement): TokenComponent<'Props, 'Data> = jsNative
    /// <summary>
    /// Creates a token component for passing custom data through JSX structure.
    /// The token component can be used as a normal component in JSX. When resolved
    /// by <c>resolveTokens</c> it will return the data passed to it. But when resolved
    /// normally (eg using the <c>children()</c> helper) it will return the fallback JSX Element.
    /// </summary>
    /// <remarks>
    /// From a <c>Partas.Solid</c> perspective, the advantage here is leveraging the
    /// plugins robust tag construction mechanisms to improve bindings. This is primarily
    /// because we do not have to concern ourselves with writing POJOs with parameters in
    /// both the constructor and as properties.<br/><br/>
    /// Further to this, we can also use interfaces more intuitively.
    /// </remarks>
    /// <param name="tokenizer">Identity object returned by <c>createTokenizer</c> or other
    /// <c>TokenComponent</c> (if not passed, a new tokenizer id will be created)</param>
    /// <param name="render">Function that returns the fallback JSX Element to render. (if not passed, the token will
    /// render nothing and warn in development)</param>
    /// <returns>A <c>TokenComponent</c> that can be used as a normal component in JSX.</returns>
    static member inline createToken<'Data>(tokenizer: JSXTokenizer<'Data>, ?render: 'Data -> HtmlElement): TokenComponent<'Data> =
        JsxTokenizer.createToken<'Data, 'Data>(tokenizer, undefined, ?render = render)
    /// <summary>
    /// Creates a token component for passing custom data through JSX structure.
    /// The token component can be used as a normal component in JSX. When resolved
    /// by <c>resolveTokens</c> it will return the data passed to it. But when resolved
    /// normally (eg using the <c>children()</c> helper) it will return the fallback JSX Element.
    /// </summary>
    /// <remarks>
    /// From a <c>Partas.Solid</c> perspective, the advantage here is leveraging the
    /// plugins robust tag construction mechanisms to improve bindings. This is primarily
    /// because we do not have to concern ourselves with writing POJOs with parameters in
    /// both the constructor and as properties.<br/><br/>
    /// Further to this, we can also use interfaces more intuitively.
    /// </remarks>
    /// <param name="tokenData">Function that returns the data of the token. (if one isn't passed, props will be used as data)</param>
    /// <param name="render">Function that returns the fallback JSX Element to render. (if not passed, the token will
    /// render nothing and warn in development)</param>
    /// <returns>A <c>TokenComponent</c> that can be used as a normal component in JSX.</returns>
    static member inline createToken<'Props, 'Data>(tokenData: 'Props -> 'Data, ?render: 'Props -> HtmlElement): TokenComponent<'Props, 'Data> =
        JsxTokenizer.createToken<'Props, 'Data>(undefined, tokenData, ?render = render)
    /// <summary>
    /// Creates a token component for passing custom data through JSX structure.
    /// The token component can be used as a normal component in JSX. When resolved
    /// by <c>resolveTokens</c> it will return the data passed to it. But when resolved
    /// normally (eg using the <c>children()</c> helper) it will return the fallback JSX Element.
    /// </summary>
    /// <remarks>
    /// From a <c>Partas.Solid</c> perspective, the advantage here is leveraging the
    /// plugins robust tag construction mechanisms to improve bindings. This is primarily
    /// because we do not have to concern ourselves with writing POJOs with parameters in
    /// both the constructor and as properties.<br/><br/>
    /// Further to this, we can also use interfaces more intuitively.
    /// </remarks>
    /// <param name="render">Function that returns the fallback JSX Element to render. (if not passed, the token will
    /// render nothing and warn in development)</param>
    /// <returns>A <c>TokenComponent</c> that can be used as a normal component in JSX.</returns>
    static member inline createToken<'Data>(?render: 'Data -> HtmlElement): TokenComponent<'Data, 'Data> =
        JsxTokenizer.createToken<'Data, 'Data>(undefined, ?render = render)
    /// <summary>
    /// A function similar to Solid's <c>children()</c>. Resolves passed JSX structure, searching for tokens
    /// with the given tokenizer id.
    /// </summary>
    /// <param name="tokenizer">identity object returned by <c>createTokenizer</c> or a <c>TokenComponent</c>. An array of multiple tokenizers can be passed.</param>
    /// <param name="fn">Accessor that returns a JSX element structure (eg: <c>fun () -> props.children</c>)</param>
    /// <param name="includeJSXElements">If <c>true</c>, other JSX Elements will be included in the result array (default: <c>false</c>)</param>
    /// <returns>Accessor that returns an array of resolved tokens (and JSX Elements if option is enabled)</returns>
    [<ImportMember(Spec.path); ParamObject(2)>]
    static member resolveTokens<'Data>(tokenizer: JSXTokenizer<'Data>, fn: Accessor<HtmlElement>, ?includeJSXElements: bool): Accessor<TokenElement<'Data>[]> = jsNative
    /// <summary>
    /// A function similar to Solid's <c>children()</c>. Resolves passed JSX structure, searching for tokens
    /// with the given tokenizer id.
    /// </summary>
    /// <param name="tokenizer">identity object returned by <c>createTokenizer</c> or a <c>TokenComponent</c>. An array of multiple tokenizers can be passed.</param>
    /// <param name="fn">Accessor that returns a JSX element structure (eg: <c>fun () -> props.children</c>)</param>
    /// <param name="includeJSXElements">If <c>true</c>, other JSX Elements will be included in the result array (default: <c>false</c>)</param>
    /// <returns>Accessor that returns an array of resolved tokens (and JSX Elements if option is enabled)</returns>
    [<ImportMember(Spec.path); ParamObject(2)>]
    static member resolveTokens<'Data>(tokenizer: JSXTokenizer<'Data>[], fn: Accessor<HtmlElement>, ?includeJSXElements: bool): Accessor<TokenElement<'Data>[]> = jsNative
    /// <summary>
    /// Checks if passed value is a <c>TokenElement</c> created by the corresponding jsx-tokenizer.
    /// </summary>
    /// <param name="tokenizer">Identity object returned by <c>createTokenizer</c> or a <c>TokenComponent</c>. An
    /// array of multiple tokenizers can be passed.</param>
    /// <param name="value">Value to check</param>
    /// <returns><c>true</c> if value is a <c>TokenElement</c></returns>
    [<ImportMember(Spec.path)>]
    static member isToken<'Data>(tokenizer: JSXTokenizer<'Data>, value: obj): bool = jsNative
    /// <summary>
    /// Checks if passed value is a <c>TokenElement</c> created by the corresponding jsx-tokenizer.
    /// </summary>
    /// <param name="tokenizer">Identity object returned by <c>createTokenizer</c> or a <c>TokenComponent</c>. An
    /// array of multiple tokenizers can be passed.</param>
    /// <param name="value">Value to check</param>
    /// <returns><c>true</c> if value is a <c>TokenElement</c></returns>
    [<ImportMember(Spec.path)>]
    static member isToken<'Data>(tokenizer: JSXTokenizer<'Data>[], value: obj): bool = jsNative
