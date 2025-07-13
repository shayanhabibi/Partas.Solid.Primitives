namespace Partas.Solid.Primitives

open Fable.Core
open Partas.Solid
open Browser.Types

[<AutoOpen; Erase>]
module private ClipboardSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/clipboard"
        let [<Literal>] version = ""

[<StringEnum>]
type PresentationStyle =
    | Attachment
    | Inline
    | Unspecified

[<AllowNullLiteral>]
[<Interface>]
type ClipboardResourceItem =
    abstract member ``type``: string with get
    abstract member text: string option with get
    abstract member blob: Blob with get

/// <summary>
/// Returned from the Solid Primitive <c>clipboard</c> library.
/// <br/>
/// Represents the array result of <c>createClipboard</c>. You can skip interacting with
/// this interface by using the apostraphised version of the method and destructuring it
/// using F# language features.
/// </summary>
[<AllowNullLiteral; Interface>]
type ClipboardResult =
    [<Emit("$0[0]")>]
    abstract member resourceItems: SolidResource<ClipboardResourceItem[]> with get
    [<Emit("$0[1]")>]
    abstract member refetch: (unit -> unit) with get
    [<Emit("$0[2]")>]
    abstract member write: (string -> JS.Promise<unit>) with get

[<AllowNullLiteral; Interface>]
type ClipboardItem =
    abstract member presentationStyle: PresentationStyle with get
    abstract member types: string[] with get
    // The getType() method of ClipboardItem interface returns a Promise that resolves with a Blob of the requested MIME type or an error if the MIME type is not found.
    /// <summary>
    /// <a href="https://developer.mozilla.org/docs/Web/API/ClipboardItem/getType">MDN Reference</a>
    /// </summary>
    abstract member getType: ``type``: string -> JS.Promise<Blob>

[<Erase; AutoOpen>]
type Clipboard =
    /// <summary>
    /// newClipboardItem is a wrapper method around creating new ClipboardItems.
    /// </summary>
    /// <param name="type">
    /// The MIME type of the item to create
    /// </param>
    /// <param name="data">
    /// Data to populate the item with
    /// </param>
    /// <returns>
    /// Provides a ClipboardItem object
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member newClipboardItem(``type``: string, data: obj): ClipboardItem = jsNative
    /// <summary>
    /// Async read from the clipboard
    /// </summary>
    /// <returns>
    /// Promise of ClipboardItem array
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member readClipboard (): JS.Promise<ClipboardItem[]> = jsNative
    
    /// <summary>
    /// Async write to the clipboard.<br/>The apostraphised version of the method will
    /// return the <c>Promise</c>.
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<ImportMember(Spec.path)>]
    static member writeClipboard (data: string): unit = jsNative
    /// <summary>
    /// Async write to the clipboard.<br/>The apostraphised version of the method will
    /// return the <c>Promise</c>.
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<ImportMember(Spec.path)>]
    static member writeClipboard (data: ClipboardItem[]): unit = jsNative
    /// <summary>
    /// Async write to the clipboard. <br/>The unapostraphised version discards the
    /// <c>Promise</c> for cleaner source
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<Import("writeClipboard", Spec.path)>]
    static member writeClipboard'(data: string): JS.Promise<unit> = jsNative
    /// <summary>
    /// Async write to the clipboard. <br/>The unapostraphised version discards the
    /// <c>Promise</c> for cleaner source
    /// </summary>
    /// <param name="data">
    /// Data to write to the clipboard - either a string or ClipboardItem array.
    /// </param>
    [<Import("writeClipboard", Spec.path)>]
    static member writeClipboard'(data: ClipboardItem[]): JS.Promise<unit> = jsNative 
    /// <summary>
    /// Creates a new reactive primitive for managing the clipboard.
    /// </summary>
    /// <example>
    /// const [data, setData] = createSignal('Foo bar');
    /// const [ clipboard, read ] = createClipboard(data);
    /// </example>
    /// <param name="data">
    /// Data signal to write to the clipboard.
    /// </param>
    /// <param name="deferInitial">
    /// Sets the value of the clipboard from the signal. defaults to false.
    /// </param>
    /// <returns>
    /// Returns a resource representing the clipboard elements and children.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createClipboard (?data: Accessor<string>, ?deferInitial: bool): ClipboardResult = jsNative
    /// <summary>
    /// Creates a new reactive primitive for managing the clipboard.
    /// </summary>
    /// <example>
    /// const [data, setData] = createSignal('Foo bar');
    /// const [ clipboard, read ] = createClipboard(data);
    /// </example>
    /// <param name="data">
    /// Data signal to write to the clipboard.
    /// </param>
    /// <param name="deferInitial">
    /// Sets the value of the clipboard from the signal. defaults to false.
    /// </param>
    /// <returns>
    /// Returns a resource representing the clipboard elements and children.
    /// </returns>
    static member createClipboard (?data: Accessor<ClipboardItem[]>, ?deferInitial: bool): ClipboardResult = jsNative
    /// <summary>
    /// Same as <c>createClipboard</c> except it returns the result ready for
    /// destructuring in F#
    /// </summary>
    [<ImportMember(Spec.path)>]
    static member createClipboard' (?data: Accessor<string>, ?deferInitial: bool): SolidResource<ClipboardResourceItem[]> * (unit -> unit) * (string -> JS.Promise<unit>) = jsNative
    /// <summary>
    /// Same as <c>createClipboard</c> except it returns the result ready for
    /// destructuring in F#
    /// </summary>
    static member createClipboard' (?data: Accessor<ClipboardItem[]>, ?deferInitial: bool): SolidResource<ClipboardResourceItem[]> * (unit -> unit) * (string -> JS.Promise<unit>) = jsNative
    // [<ImportMember(Spec.path)>] // Directive
    // static member copyToClipboard () = jsNative
