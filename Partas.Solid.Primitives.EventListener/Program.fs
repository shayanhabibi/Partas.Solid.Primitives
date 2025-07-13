namespace Partas.Solid.Primitives

open Partas.Solid
open Fable.Core
open Browser.Types

[<Erase; AutoOpen>]
module private EventListenerSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/event-listener"

[<Erase; AutoOpen>]
type EventListener =
    /// <summary>
    /// Creates an event listener, that will be automatically disposed on cleanup.
    /// </summary>
    /// <example>
    /// const clear = makeEventListener(element, 'click', e => { ... }, { passive: true })
    /// // remove listener (will also happen on cleanup)
    /// clear()
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    /// <returns>
    /// Function clearing all event listeners form targets
    /// </returns>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member makeEventListener(
        target: #Element,
        ``type``: string,
        handler: Event -> unit,
        ?capture: bool, ?once: bool, ?passive: bool): DisposeCallback = jsNative
    /// <summary>
    /// Creates an event listener, that will be automatically disposed on cleanup.
    /// </summary>
    /// <example>
    /// const clear = makeEventListener(element, 'click', e => { ... }, { passive: true })
    /// // remove listener (will also happen on cleanup)
    /// clear()
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="options">
    /// addEventListener options
    /// </param>
    /// <returns>
    /// Function clearing all event listeners form targets
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member makeEventListener(
        target: #Element,
        ``type``: string,
        handler: Event -> unit,
        options: AddEventListenerOptions): DisposeCallback = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(
        target: #Element,
        ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>,
        handler: Event -> unit,
        ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(
        target: #Element,
        ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>,
        handler: Event -> unit,
        options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(target: #Element[], ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(target: #Element[], ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(target: Accessor<#Element>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(target: Accessor<#Element>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Creates a reactive event listener, that will be automatically disposed on cleanup,
    /// and can take reactive arguments to attach listeners to new targets once changed.
    /// </summary>
    /// <example>
    /// <code>
    /// const [targets, setTargets] = createSignal([element])
    /// createEventListener(targets, 'click', e => { ... }, { passive: true })
    /// setTargets([]) // &lt;- removes listeners from previous target
    /// setTargets([element, button]) // &lt;- adds listeners to new targets
    /// </code>
    /// </example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="handler">
    /// event handler
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    [<ImportMember(Spec.path); ParamObject(3)>]
    static member createEventListener(target: Accessor<#Element[]>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, ?capture: bool, ?once: bool, ?passive: bool): unit = jsNative
    [<ImportMember(Spec.path)>]
    static member createEventListener(target: Accessor<#Element[]>, ``type``: U4<string, string[], Accessor<string>, Accessor<string[]>>, handler: Event -> unit, options: AddEventListenerOptions): unit = jsNative
    /// <summary>
    /// Provides an reactive signal of last captured event.
    /// </summary>
    /// <example><code>
    /// const lastEvent = createEventSignal(el, 'click', { passive: true })
    ///
    /// createEffect(() => {
    ///    console.log(lastEvent())
    /// })
    /// </code></example>
    /// <param name="target">
    /// ref to HTMLElement, EventTarget or Array thereof
    /// </param>
    /// <param name="type">
    /// name of the handled event
    /// </param>
    /// <param name="capture">
    /// addEventListener options
    /// </param>
    /// <param name="once">
    /// addEventListener options
    /// </param>
    /// <param name="passive">
    /// addEventListener options
    /// </param>
    /// <returns>
    /// Signal of last captured event and function clearing all event listeners
    /// </returns>
    [<ImportMember(Spec.path); System.Obsolete("Unimplemented")>]
    static member createEventSignal([<System.ParamArray>] arguments: obj[]): obj = jsNative
