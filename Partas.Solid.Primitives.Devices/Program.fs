namespace Partas.Solid.Primitives

open Fable.Core
open Partas.Solid
open Browser.Types

[<Erase; AutoOpen>]
module private DevicesSpec =
    [<Erase>]
    module Spec =
        let [<Literal>] path = "@solid-primitives/devices"

[<AllowNullLiteral; Interface>]
type GyroscopeValue =
    abstract member alpha: float with get,set
    abstract member beta: float with get,set
    abstract member gamma: float with get,set

[<Erase; AutoOpen>]
type Devices =
    /// <summary>
    /// Creates a list of all media devices
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the permissions to use the media devices are not granted, you'll get a single device of that kind with empty ids and label to show that devices are available at all.
    ///
    /// If the array does not contain a device of a certain kind, you cannot get permissions, as requesting permissions requires requesting a stream on any device of the kind.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createDevices(): Accessor<MediaDeviceInfo[]> = jsNative
    /// <summary>
    /// Creates a list of all media devices that are microphones
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the microphone permissions are not granted, you'll get a single device with empty ids and label to show that devices are available at all.
    ///
    /// Without a device, you cannot get permissions, as requesting permissions requires requesting a stream on any device of the kind.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createMicrophones(): Accessor<MediaDeviceInfo[]> = jsNative
    /// <summary>
    /// Creates a list of all media devices that are speakers
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the speaker permissions are not granted, you'll get a single device with empty ids and label to show that devices are available at all.
    ///
    /// Microphone permissions automatically include speaker permissions. You can use the device id of the speaker to use the setSinkId-API of any audio tag.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createSpeakers(): Accessor<MediaDeviceInfo[]> = jsNative
    /// <summary>
    /// Creates a list of all media devices that are cameras
    /// </summary>
    /// <returns>
    /// () => MediaDeviceInfo[]
    ///
    /// If the camera permissions are not granted, you'll get a single device with empty ids and label to show that devices are available at all.
    ///
    /// Without a device, you cannot get permissions, as requesting permissions requires requesting a stream on any device of the kind.
    /// </returns>
    [<ImportMember(Spec.path)>]
    static member createCameras(): Accessor<MediaDeviceInfo> = jsNative
    
    /// <summary>
    /// Creates a reactive wrapper to get device acceleration
    /// </summary>
    /// <param name="includeGravity">
    /// boolean. default value false
    /// </param>
    /// <param name="interval">
    /// number as ms. default value 100
    /// </param>
    [<ImportMember(Spec.path)>]
    static member createAccelerometer(?includeGravity: bool, ?interval: float): Accessor<DeviceAcceleration option> = jsNative
    
    /// <summary>
    /// Creates a reactive wrapper to get device orientation
    /// </summary>
    /// <param name="interval">
    /// number as ms. default value 100
    /// </param>
    [<ImportMember(Spec.path)>]
    static member createGyroscope(?interval: float): Accessor<GyroscopeValue> = jsNative
