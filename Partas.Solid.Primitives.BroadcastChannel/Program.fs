﻿namespace rec Partas.Solid.Primitives.BroadcastChannel

open Partas.Solid.Primitives
open Fable.Core
open Partas.Solid
open Browser.Types

[<Fable.Core.Erase>]
module Spec =
    let [<Literal>] path = Common.path + "broadcast-channel"
    let [<Literal>] version = "0.1.0"

[<AllowNullLiteral; Interface>]
type MessageEvent<'T> =
    inherit MessageEvent
    abstract member data: 'T with get
    

[<AllowNullLiteral; Interface>]
type BroadcastChannelResult = interface end


[<AllowNullLiteral; Interface>]
type MakeBroadcastChannelResult<'T> =
    inherit BroadcastChannelResult
    /// A function to subscribe to messages from other tabs on the same channel
    abstract member onMessage: event: MessageEvent<'T> -> unit with get
    /// A function to send messages to other tabs
    abstract member postMessage: 'T -> unit with get
    /// A function to close the channel
    abstract member close: unit -> unit with get
    /// The name of the channel
    abstract member channelName: string with get
    /// The underlying BroadcastChannel instance
    abstract member instance: BroadcastChannel<'T> with get
[<AllowNullLiteral; Interface>]
type CreateBroadcastChannelResult<'T> =
    inherit BroadcastChannelResult
    /// An accessor that updates when postMessage is fired from other contexts
    abstract member message: Accessor<'T> -> unit with get
    /// A function to send messages to other tabs
    abstract member postMessage: 'T -> unit with get
    /// A function to close the channel
    abstract member close: unit -> unit with get
    /// The name of the channel
    abstract member channelName: string with get
    /// The underlying BroadcastChannel instance
    abstract member instance: BroadcastChannel<'T> with get

[<Erase>]
type BroadcastChannel<'T> =
    /// <summary>
    /// Creates a new BroadcastChannel instance for cross-tab communication.<br/>
    /// The channel name is used to identify the channel. If a channel with the same name already exists, it will
    /// be returned instead of creating a new one.<br/>
    /// Channel attempt closing the channel when the on owner cleanup. If there are multiple connected instances, the
    /// channel will not be closed until the last owner is removed.
    /// </summary>
    /// <param name="name">Channel name to listen/broadcast on</param>
    /// <returns>An object with the following properties<br/>
    /// onMessage, postMessage, close, channelName, instance</returns>
    [<ImportMember("@solid-primitives/broadcast-channel")>]
    static member makeBroadcastChannel<'T> (name: string): MakeBroadcastChannelResult<'T> = jsNative
    /// <summary>
    /// Provides the same functionality as <c>makeBroadcastChannel</c> but instead of returning <c>onMessage</c>, it
    /// returns a <c>message</c> signal accessor that updates when postMessage is fired from other contexts.
    /// </summary>
    /// <param name="name">Channel name to listen/broadcast on</param>
    /// <returns>An object with the following properties<br/>
    /// message, postMessage, close, channelName, instance</returns>
    [<ImportMember("@solid-primitives/broadcast-channel")>]
    static member createBroadcastChannel<'T> (name: string): CreateBroadcastChannelResult<'T> = jsNative