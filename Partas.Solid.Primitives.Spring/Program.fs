namespace Partas.Solid.Primitives

open System.Runtime.CompilerServices
open Partas.Solid
open Fable.Core
open Fable.Core.JsInterop
open Partas.Solid.Experimental.U

[<Erase; AutoOpen>]
module private SpringSpec =
    [<Erase>]
    module Spec =
        let [<Erase; Literal>] path = "@solid-primitives/spring"
        let [<Erase; Literal>] version = "0.1.1"

open Spec

[<JS.Pojo>]
type SpringSetterOptions(?hard: bool, ?soft: U2<bool, float>) =
    member val hard = hard with get,set
    member val soft = soft with get,set

module SetterExtensions =
    [<AutoOpen; Erase>]
    type SetterExtensions =
        [<Extension>]
        static member inline Invoke(setter: float -> unit, newValue: float, options: SpringSetterOptions) = setter !!(newValue, options)
        
[<Erase; AutoOpen>]
type Spring =
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: float, ?stiffness: float, ?damping: float): Signal<float> = jsNative
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: int, ?stiffness: float, ?damping: float): Signal<int> = jsNative
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: float seq, ?stiffness: float, ?damping: float): Signal<float seq> = jsNative
    /// <summary>
    /// Creates a signal and a setter that uses spring physics when interpolating from
    /// one value to another. This means when the value changes, instead of
    /// transitioning at a steady rate, it "bounces" like a spring would,
    /// depending on the physics parameters provided. This adds a level of realism to
    /// the transitions and can enhance the user experience.
    ///
    /// <c>T</c> - The type of the signal. It works for the basic data types that can be
    /// interpolated: <c>number</c>, a <c>Date</c>, <c>Array&lt;T></c> or a nested object of T.
    /// </summary>
    /// <example>
    /// const [progress, setProgress] = createSpring(0, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="initialValue">
    /// The initial value of the signal.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value and a setter. The setter can be provided new Spring options by opening
    /// the <c>SetterExtensions</c> submodule and <c>setter.Invoke</c>-ing the setter.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createSpring(initialValue: int seq, ?stiffness: float, ?damping: float): Signal<int seq> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<int>, ?stiffness: float, ?damping: float): Accessor<int> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<float>, ?stiffness: float, ?damping: float): Accessor<float> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<int seq>, ?stiffness: float, ?damping: float): Accessor<int seq> = jsNative
    /// <summary>
    /// Creates a spring value that interpolates based on changes on a passed signal.
    /// Works similar to the <c>@solid-primitives/tween</c>
    /// </summary>
    /// <example>
    /// const percent = createMemo(() => current() / total() * 100);
    ///
    /// const springedPercent = createDerivedSignal(percent, { stiffness: 0.15, damping: 0.8 });
    /// </example>
    /// <param name="target">
    /// Target to be modified.
    /// </param>
    /// <param name="stiffness">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <param name="damping">
    /// Options to configure the physics of the spring.
    /// </param>
    /// <returns>
    /// Returns the spring value only.
    /// </returns>
    [<ImportMember(path); ParamObject(1)>]
    static member createDerivedSpring(target: Accessor<float seq>, ?stiffness: float, ?damping: float): Accessor<float seq> = jsNative
