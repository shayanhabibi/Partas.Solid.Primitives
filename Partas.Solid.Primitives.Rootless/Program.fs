﻿namespace Partas.Solid.Primitives

open System
open Partas.Solid
open Fable.Core
open Fable.Core.JsInterop

[<Erase; AutoOpen>]
module RootlessSpec =
    [<Erase>]
    module Spec =
        [<Literal>]
        let path = "@solid-primitives/rootless"
        [<Literal>]
        let version = "1.5.1"

[<Erase>]
module Rootless =
    /// <summary>
    /// Callback function for <c>createRootPool</c>. Called when a new root is created.
    /// </summary>
    /// <param name="arg">An accessor that returns the argument passed to the <c>RootPoolFunction</c></param>
    /// <param name="active">An accessor that returns the active state of the root. When <c>false</c>, root is not being used
    /// and is waiting in the pool to be reused</param>
    /// <param name="dispose">A function that disposes the root and prevents it from being reused.</param>
    type RootPoolFactory<'Arg, 'Result> = delegate of arg: Accessor<'Arg> * active: Accessor<bool> * dispose: DisposeCallback -> 'Result

[<AutoOpen; Erase>]
type Rootless =
    /// <summary>
    /// Creates a reactive sub root that will be automatically disposed when its owner does.
    /// </summary>
    /// <param name="fn">A function in which the reactive state is scoped</param>
    /// <param name="owners">Reactive root dependencies - cleanup of ANY of them will trigger sub-root disposal. Defaults to <c>getOwner()</c></param>
    [<ImportMember(Spec.path)>]
    static member createSubRoot<'T>(fn: DisposeCallback -> 'T, [<ParamArray>] owners: obj): 'T = jsNative
    [<ImportMember(Spec.path)>]
    static member createCallback<'T>(callback: 'T, ?owner: obj): 'T = jsNative
    [<ImportMember(Spec.path)>]
    static member createDisposable(fn: DisposeCallback -> unit, [<ParamArray>] owners: obj): DisposeCallback = jsNative
    [<ImportMember(Spec.path)>]
    static member createSingletonRoot<'T>(
        factory: DisposeCallback -> 'T,
        ?detachedOwner: obj
        ): Accessor<'T> = jsNative
    [<ImportMember(Spec.path)>]
    static member createHydratableSingletonRoot<'T>(factory: DisposeCallback -> 'T): Accessor<'T> = jsNative
    /// <summary>
    ///  Creates a pool fo roots, that can be reused. Useful for creating components
    /// that are mounted and unmounted frequently. When the root is created, it will call
    /// the <c>factory</c> function with a <c>RootPoolFactory</c> callback. Roots are created
    /// by calling the returned function, after cleanup they won't be disposed but instead
    /// put back into the pool to be reused. Next time the function is called, it will
    /// reuse the root from the pool and update it with the new <c>arg</c>
    /// </summary>
    /// <param name="factory">A function that will be called when a new root is created.</param>
    /// <param name="limit">Defaults to 100. Size of the root pool.</param>
    [<ImportMember(Spec.path); ParamObject(1)>]
    static member createRootPool<'Arg, 'Result>(
            factory: Rootless.RootPoolFactory<'Arg, 'Result>,
            limit: int
        ): 'Arg -> 'Result = jsNative
    [<ImportMember(Spec.path)>]
    static member createRootPool<'Arg, 'Result>(
            factory: Rootless.RootPoolFactory<'Arg, 'Result>
        ): 'Arg -> 'Result = jsNative
