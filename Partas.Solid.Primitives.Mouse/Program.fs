namespace Partas.Solid.Primitives.Mouse

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Partas.Solid.Primitives
open Partas.Solid
open Partas.Solid.Experimental.U

[<Erase>]
module Spec =
    let [<Erase; Literal>] path = "@solid-primitives/mouse"
    let [<Erase; Literal>] version = "2.1.2"

open Spec

[<StringEnum>]
type MouseSourceType =
    | Mouse
    | Touch

[<JS.Pojo>]
type Position(?x:int,?y:int) =
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set

[<JS.Pojo>]
type MousePosition(
        ?x: int
        ,?y: int
        ,?sourceType: MouseSourceType
    ) =
    inherit Position(?x=x,?y=y)
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set
    /// Can be null
    member val sourceType: MouseSourceType option = sourceType with get,set

[<JS.Pojo>]
type MousePositionInside(
        ?x: int
        ,?y: int
        ,?isInside: bool
        ,?sourceType: MouseSourceType
    ) =
    inherit MousePosition(?x=x, ?y=y, ?sourceType = sourceType)
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set
    member val isInside: bool = JS.undefined with get,set
    /// Can be null
    member val sourceType: MouseSourceType option = sourceType with get,set
    
[<JS.Pojo>]
type PositionRelativeToElement(
        ?x: int
        ,?y: int
        ,?top: int
        ,?left: int
        ,?width: int
        ,?height: int
        ,?isInside: bool
    ) =
    member val x: int = JS.undefined with get,set
    member val y: int = JS.undefined with get,set
    member val top: int = JS.undefined with get,set
    member val left: int = JS.undefined with get,set
    member val width: int = JS.undefined with get,set
    member val height: int = JS.undefined with get,set
    member val isInside: bool = JS.undefined with get,set

[<Erase; AutoOpen>]
type Mouse =
    /// <summary>
    /// Attaches event listeners to provided targat, listeneing for changes to the mouse/touch position.
    /// </summary>
    /// <param name="target">
    /// <code lang="ts">
    /// SVGSVGElement | HTMLElement | Window | Document
    /// </code>
    /// </param>
    /// <param name="callback">
    /// function fired on every position change
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// function removing all event listeners
    /// </returns>
    /// <seealso cref="UseTouchOptions">UseTouchOptions</seealso>
    /// <seealso cref="FollowTouchOptions">FollowTouchOptions</seealso>
    [<Import("makeMousePositionListener", path); ParamObject(2)>]
    static member makeMousePositionListener (target: U4<#HtmlElement, #Element, Document, Window>, callback: (MousePosition -> unit), ?touch: bool, ?followTouch: bool) : DisposeCallback = nativeOnly
    /// <summary>
    /// Attaches event listeners to provided targat, listening for mouse/touch entering/leaving the element.
    /// </summary>
    /// <param name="target">
    /// <code lang="ts">
    /// SVGSVGElement | HTMLElement | Window | Document
    /// </code>
    /// </param>
    /// <param name="callback">
    /// function fired on mouse leaving or entering the element
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <returns>
    /// function removing all event listeners
    /// </returns>
    [<Import("makeMouseInsideListener", path); ParamObject(2)>]
    static member makeMouseInsideListener (target: U4<#HtmlElement, #Element, Document, Window>, callback: (bool -> unit), ?touch: bool) : DisposeCallback = nativeOnly
    /// <summary>
    /// Turn position relative to the page, into position relative to an element.
    /// </summary>
    [<Import("getPositionToElement", path)>]
    static member getPositionToElement(pageX: int, pageY: int, el: U4<#HtmlElement, #Element, Document, Window>): PositionRelativeToElement = nativeOnly
    /// <summary>
    /// Turn position relative to the page, into position relative to an element. Clamped to the element bounds.
    /// </summary>
    [<Import("getPositionInElement", path)>]
    static member getPositionInElement(pageX: int, pageY: int, el: U4<#HtmlElement, #Element, Document, Window>): PositionRelativeToElement = nativeOnly
    /// <summary>
    /// Turn position relative to the page, into position relative to the screen.
    /// </summary>
    [<Import("getPositionToScreen", path)>]
    static member getPositionToScreen(pageX: int, pageY: int): Position = nativeOnly
    /// <summary>
    /// Attaches event listeners to <see href="target">target</see> element to provide a reactive object of current mouse position on the page.
    /// </summary>
    /// <example>
    /// const [el, setEl] = createSignal(ref)
    /// const pos = createMousePosition(el, { touch: false })
    /// createEffect(() => {
    ///   console.log(pos.x, pos.y)
    /// })
    /// </example>
    /// <param name="target">
    /// (Defaults to <c>window</c>) element to attach the listeners to – can be a reactive function
    /// </param>
    /// <param name="initialValues">
    /// Initial values
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// reactive object of current mouse position on the page
    /// <code lang="ts">
    /// { x: number, y: number, sourceType: MouseSourceType, isInside: boolean }
    /// </code>
    /// </returns>
    [<Import("createMousePosition", path)>]
    static member createMousePosition (?target: U4<#HtmlElement, #Element, Document, Window>, ?initialValues: MousePositionInside, ?touch: bool, ?followTouch: bool) : MousePositionInside = nativeOnly
    /// <summary>
    /// Attaches event listeners to <see href="target">target</see> element to provide a reactive object of current mouse position on the page.
    /// </summary>
    /// <example>
    /// const [el, setEl] = createSignal(ref)
    /// const pos = createMousePosition(el, { touch: false })
    /// createEffect(() => {
    ///   console.log(pos.x, pos.y)
    /// })
    /// </example>
    /// <param name="target">
    /// (Defaults to <c>window</c>) element to attach the listeners to – can be a reactive function
    /// </param>
    /// <param name="initialValues">
    /// Initial values
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// reactive object of current mouse position on the page
    /// <code lang="ts">
    /// { x: number, y: number, sourceType: MouseSourceType, isInside: boolean }
    /// </code>
    /// </returns>
    [<Import("createMousePosition", path); ParamObject(1)>]
    static member createMousePosition (?target: U4<Accessor<#HtmlElement>, Accessor<#Element>, Accessor<Document>, Accessor<Window>>, ?initialValues: MousePositionInside, ?touch: bool, ?followTouch: bool) : MousePositionInside = nativeOnly
    /// <summary>
    /// Attaches event listeners to <c>window</c> to provide a reactive object of current mouse position on the page.
    ///
    /// This is a [singleton root primitive](https://github.com/solidjs-community/solid-primitives/tree/main/packages/rootless#createSingletonRoot).
    /// </summary>
    /// <example>
    /// const pos = useMousePosition()
    /// createEffect(() => {
    ///   console.log(pos.x, pos.y)
    /// })
    /// </example>
    /// <returns>
    /// reactive object of current mouse position on the page
    /// <code lang="ts">
    /// { x: number, y: number, sourceType: MouseSourceType, isInside: boolean }
    /// </code>
    /// </returns>
    [<Import("useMousePosition", path)>]
    static member inline useMousePosition: (unit -> MousePositionInside) = nativeOnly
    /// <summary>
    /// Provides an autoupdating position relative to an element based on provided page position.
    /// </summary>
    /// <example>
    /// const [el, setEl] = createSignal(ref)
    /// const pos = useMousePosition()
    /// const relative = createPositionToElement(el, () => pos)
    /// createEffect(() => {
    ///   console.log(relative.x, relative.y)
    /// })
    /// </example>
    /// <param name="element">
    /// target <c>Element</c> used in calculations
    /// </param>
    /// <param name="pos">
    /// reactive function returning page position *(relative to the page not window)*
    /// </param>
    /// <param name="initialValues">
    /// Initial values
    /// </param>
    /// <param name="touch">
    /// Listen to touch events. If enabled, position will be updated on <c>touchstart</c> event.
    /// </param>
    /// <param name="followTouch">
    /// If enabled, position will be updated on <c>touchmove</c> event.
    /// </param>
    /// <returns>
    /// Autoupdating position relative to top-left of the target + current bounds of the element.
    /// </returns>
    [<Import("createPositionToElement", path); ParamObject(2)>]
    static member createPositionToElement (element: U4<#HtmlElement, #Element, Accessor<#HtmlElement>, Accessor<#Element>>, pos: Accessor<Position>, ?initialValues: PositionRelativeToElement, ?touch: bool, ?followTouch: bool) : PositionRelativeToElement = nativeOnly

