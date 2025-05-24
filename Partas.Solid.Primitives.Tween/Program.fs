namespace Partas.Solid.Primitives.Tween

open Partas.Solid.Primitives
open Partas.Solid
open Fable.Core
open Fable.Core.JS

[<Erase>]
module Spec =
    let [<Literal>] path = Common.path + "tween"
    let [<Literal>] version = "1.4.0"

open Spec

type Easing = (float -> float)
/// <summary>
/// Suggested to use easing functions that are already prepared:
/// <code lang="fsharp">
/// let tweenedValue = createTween(value, CreateTweenOptions(500, Easing.easeInSine))
/// </code>
/// </summary>
[<Pojo>]
type CreateTweenOptions(?duration: int, ?easing: Easing) =
    member val duration = duration |> Option.defaultValue 0
    member val easing = easing |> Option.defaultValue id
/// Contains typical easing functions
[<Erase; AutoOpen>]
module Easing =
    let easeInSine: Easing = fun x -> 1. - Math.cos((x * Math.PI) / 2.)
    let easeOutSine: Easing = fun x  -> Math.sin((x * Math.PI) / 2.)
    let easeInOutSine: Easing = fun x -> -(Math.cos(Math.PI  * x) - 1.) / 2.
    let easeInQuad: Easing = fun x -> x * x
    let easeOutQuad: Easing = fun x -> 1. - (1. - x) * (1. - x)
    let easeInOutQuad: Easing = fun x -> if x < 0.5 then 2. * x * x else 1. - (-2. * x + 2.) * (-2. * x + 2.) / 2.
    let easeInCubic: Easing = fun x -> x * x * x
    let easeOutCubic: Easing = fun x -> 1. - (1. - x) * (1. - x) * (1. - x)
    let easeInOutCubic: Easing = fun x -> if x < 0.5 then 4. * x * x * x else 1. - (-2. * x + 2.) ** 3. / 2.
    let easeInQuart: Easing = fun x -> x * x * x * x
    let easeOutQuart: Easing = fun x -> 1. - (1. - x) ** 4.
    let easeInOutQuart: Easing = fun x -> if x < 0.5 then 8. * x * x * x * x else 1. - (-2. * x + 2.) ** 4. / 2.
    let easeInQuint: Easing = fun x -> x * x * x * x * x
    let easeOutQuint: Easing = fun x -> 1. - (1. - x) ** 5.
    let easeInOutQuint: Easing = fun x -> if x < 0.5 then 16. * x * x * x * x * x else 1. - (-2. * x + 2.) ** 5. / 2.
    let easeInExpo: Easing = fun x -> if x = 0. then 0. else 2. ** (10. * x - 10.)
    let easeOutExpo: Easing = fun x -> if x = 1. then 1. else 1. - 2. ** (-10. * x)
    let easeInOutExpo: Easing = fun x -> 
        if x = 0. then 0.
        elif x = 1. then 1.
        else if x < 0.5 then 2. ** (20. * x - 10.) / 2. else (2. - 2. ** (-20. * x + 10.)) / 2.

    let easeInCirc: Easing = fun x -> 1. - Math.sqrt(1. - x * x)
    let easeOutCirc: Easing = fun x -> Math.sqrt(1. - (x - 1.) * (x - 1.))
    let easeInOutCirc: Easing = fun x -> 
        if x < 0.5 then (1. - Math.sqrt(1. - (2. * x) ** 2.)) / 2. else (Math.sqrt(1. - (-2. * x + 2.) ** 2.) + 1.) / 2.

    let easeInBack: Easing = fun x -> let c1 = 1.70158 in c1 * x * x * x - c1 * x * x
    let easeOutBack: Easing = fun x -> let c1 = 1.70158 in 1. + c1 * (x - 1.) * (x - 1.) * (x - 1.) + c1 * (x - 1.) * (x - 1.)
    let easeInOutBack: Easing = fun x -> 
        let c2 = 2.5949095
        if x < 0.5 then (c2 * (2. * x) * (2. * x) * (2. * x) - c2 * (2. * x) * (2. * x)) / 2. 
        else (c2 * (-2. * x + 2.) * (-2. * x + 2.) * (-2. * x + 2.) + c2 * (-2. * x + 2.) * (-2. * x + 2.) + 2.) / 2.

    let easeInElastic: Easing = fun x -> 
        if x = 0. then 0.
        elif x = 1. then 1.
        else -2. ** (10. * x - 10.) * Math.sin((x * 10. - 10.75) * (2. * Math.PI) / 3.)

    let easeOutElastic: Easing = fun x -> 
        if x = 0. then 0.
        elif x = 1. then 1.
        else 2. ** (-10. * x) * Math.sin((x * 10. - 0.75) * (2. * Math.PI) / 3.) + 1.

    let easeInOutElastic: Easing = fun x -> 
        if x = 0. then 0.
        elif x = 1. then 1.
        else if x < 0.5 then (-2. ** (20. * x - 10.) * Math.sin((20. * x - 11.125) * (2. * Math.PI) / 4.5)) / 2.
        else (2. ** (-20. * x + 10.) * Math.sin((20. * x - 11.125) * (2. * Math.PI) / 4.5)) / 2. + 1.

    let easeOutBounce: Easing = fun x -> 
        if x < 1. / 2.75 then 7.5625 * x * x
        elif x < 2. / 2.75 then 7.5625 * (x - 1.5 / 2.75) * (x - 1.5 / 2.75) + 0.75
        elif x < 2.5 / 2.75 then 7.5625 * (x - 2.25 / 2.75) * (x - 2.25 / 2.75) + 0.9375
        else 7.5625 * (x - 2.625 / 2.75) * (x - 2.625 / 2.75) + 0.984375
    let easeInBounce: Easing = fun x -> 1. - easeOutBounce(1. - x)

    let easeInOutBounce: Easing = fun x -> 
        if x < 0.5 then (1. - easeOutBounce(1. - 2. * x)) / 2.
        else (1. + easeOutBounce(2. * x - 1.)) / 2.
    let easeLinear:  Easing = id
[<Erase; AutoOpen>]
type Tween =
    /// <summary>
    /// Creates an efficient tweening derived signal that smoothly transitions a given signal from its previous value to its next value whenever it changes.
    /// <br/>
    /// target can be any reactive value (signal, memo, or function that calls such). For example, to use a component prop, specify <c>fun () -> props.value</c>.<br/><br/>
    /// You can provide two options:
    /// <br/>
    /// - duration is the number of milliseconds to perform the transition from the previous value to the next value. Defaults to 100.
    /// <br/>- easing is a function that maps a number between 0 and 1 to a number between 0 and 1, to speed up or slow down different parts of the transition. The default easing function (t) => t is linear (no easing). A common choice is (t) => 0.5 - Math.cos(Math.PI * t) / 2.
    /// <br/><br/>Internally, createTween uses requestAnimationFrame to smoothly update the tweened value at the display refresh rate. After the tweened value reaches the underlying signal value, it will stop updating via requestAnimationFrame for efficiency.
    /// </summary>
    [<ImportMember(path)>]
    static member createTween(target: Accessor<float>, ?options: CreateTweenOptions): Accessor<float> = jsNative
    /// <summary>
    /// Creates an efficient tweening derived signal that smoothly transitions a given signal from its previous value to its next value whenever it changes.
    /// <br/>
    /// target can be any reactive value (signal, memo, or function that calls such). For example, to use a component prop, specify <c>fun () -> props.value</c>.<br/><br/>
    /// You can provide two options:
    /// <br/>
    /// - duration is the number of milliseconds to perform the transition from the previous value to the next value. Defaults to 100.
    /// <br/>- easing is a function that maps a number between 0 and 1 to a number between 0 and 1, to speed up or slow down different parts of the transition. The default easing function (t) => t is linear (no easing). A common choice is (t) => 0.5 - Math.cos(Math.PI * t) / 2.
    /// <br/><br/>Internally, createTween uses requestAnimationFrame to smoothly update the tweened value at the display refresh rate. After the tweened value reaches the underlying signal value, it will stop updating via requestAnimationFrame for efficiency.
    /// </summary>
    [<ImportMember(path)>]
    static member createTween(target: Accessor<int>, ?options: CreateTweenOptions): Accessor<int> = jsNative
    static member inline createTween(target: Accessor<int>, duration: int, easing: Easing): Accessor<int> =
        Tween.createTween(target, options = CreateTweenOptions(duration = duration, easing = easing))
    static member inline createTween(target: Accessor<float>, duration: int, easing: Easing): Accessor<float> =
        Tween.createTween(target, options = CreateTweenOptions(duration = duration, easing = easing))
