namespace Partas.Solid.Primitives

open Partas.Solid
open Fable.Core
open Browser.Types

[<Erase; AutoOpen>]
module private AudioSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/audio"

[<Global>]
type MediaSource = interface end
[<Erase>]
type AudioSource = U3<string, HTMLAudioElement, MediaSource>
[<RequireQualifiedAccess; StringEnum>]
type AudioState =
    | Loading
    | Playing
    | Paused
    | Complete
    | Stopped
    | Ready
    | Error
[<AllowNullLiteral; Interface>]
type AudioControls =
    /// Start playing
    abstract member play: unit -> unit
    /// Pause playing
    abstract member pause: unit -> unit
    /// Seeks to a location in the playhead
    abstract member seek: time: float -> unit
    /// Sets the volume of the player
    abstract member setVolume: volume: float -> unit


[<AllowNullLiteral; Interface>]
type AudioPlayer =
    inherit AudioControls
    /// Raw player instance
    abstract member player: HTMLAudioElement

[<AllowNullLiteral; Interface>]
type ReactiveAudioPlayer =
    abstract member player: HTMLAudioElement
    abstract member state: AudioState
    abstract member currentTime: float
    abstract member duration: float
    abstract member volume: float

[<Erase; AutoOpen>]
type Audio =
    /// <summary>
    /// A foundational primitive with no player controls but exposes the raw player
    /// object.
    /// </summary>
    /// <param name="src">Audio file path or MediaSource to be played</param>
    /// <param name="handlers">An array of handlers to bind against the player.</param>
    [<ImportMember(Spec.path)>]
    static member makeAudio(src: AudioSource, ?handlers: (*AudioEventHandlers*) obj): HTMLAudioElement = jsNative
    /// <summary>
    /// Provides a very basic interface for wrapping listeners to a supplied or default
    /// audio player.
    /// </summary>
    /// <remarks>
    /// The <c>seek</c> function falls back to <c>fastSeek</c> when on supporting browsers.
    /// </remarks>
    /// <param name="src"></param>
    /// <param name="handlers">Array of handlers to bind against the player</param>
    [<ImportMember(Spec.path)>]
    static member makeAudioPlayer(src: AudioSource, ?handlers: (*AudioEventHandlers*) obj): AudioPlayer = jsNative
    /// <summary>
    /// Creates a very basic audio/sound player with reactive properties to control
    /// the audio. Be careful not to destructure the value properties provided
    /// by the primitive as it will likely break reactivity.
    /// </summary>
    /// <remarks>
    /// The audio primitive exports reactive properties that provide you access
    /// to state, duration and current time.<br/><br/>
    /// Intialising the primitive with <c>playing</c> as true works, however
    /// note that the user has to interact with the page first (on a fresh
    /// page load).
    /// </remarks>
    /// <param name="src">Can be a reactive signal as well as a media source.</param>
    /// <param name="playing"></param>
    /// <param name="volume"></param>
    [<ImportMember(Spec.path)>]
    static member createAudio(
            src: AudioSource,
            ?playing: Accessor<bool>,
            ?volume: Accessor<float>
        ): ReactiveAudioPlayer * AudioControls = jsNative
    /// <summary>
    /// Creates a very basic audio/sound player with reactive properties to control
    /// the audio. Be careful not to destructure the value properties provided
    /// by the primitive as it will likely break reactivity.
    /// </summary>
    /// <remarks>
    /// The audio primitive exports reactive properties that provide you access
    /// to state, duration and current time.<br/><br/>
    /// Intialising the primitive with <c>playing</c> as true works, however
    /// note that the user has to interact with the page first (on a fresh
    /// page load).
    /// </remarks>
    /// <param name="src">Can be a reactive signal as well as a media source.</param>
    /// <param name="playing"></param>
    /// <param name="volume"></param>
    [<ImportMember(Spec.path)>]
    static member createAudio(
            src: Accessor<AudioSource>,
            ?playing: Accessor<bool>,
            ?volume: Accessor<float>
        ): ReactiveAudioPlayer * AudioControls = jsNative
