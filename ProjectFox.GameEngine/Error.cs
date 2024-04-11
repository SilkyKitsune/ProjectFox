namespace ProjectFox.GameEngine;

/// <summary> Types of errors handled by the engine </summary>
public enum ErrorCodes
{
    /// <summary> No existing implementation available </summary>
    NotImplemented,

    /// <summary> Invalid Argument </summary>
    BadArgument,
    /// <summary> Enum value outside of intended values </summary>
    BadEnumValue,
    MinGreaterThanMax,

    /// <summary> Argument was null </summary>
    NullArgument,
    /// <summary> CompoundObject contained a pet that was null </summary>
    NullPet,
    /// <summary> PhysicsObject's PhysicsSpace was null </summary>
    NullPhysicsSpace,
    /// <summary> RasterObject's VisualLayer was null </summary>
    NullVisualLayer,
    /// <summary> AudioSource's AudioChannel was null </summary>
    NullAudioChannel,
    /// <summary> RasterObject attempted to draw a PalettizedTexture with a null palette </summary>
    NullPalette,
    /// <summary> RasterObject attempted to draw with a null texture </summary>
    NullTexture,
    NullAnimation,
    /// <summary> AudioSource attempted to draw with a null wave shape </summary>
    NullWaveShape,

    EmptyPalette,
    MissingAnimation,
    EmptyAnimation,
    PlaybackError,

    /// <summary> Scene list is empty </summary>
    NoScenes,
    /// <summary> Scene list has no active scene </summary>
    NoActiveScene,

    /// <summary> Object already has an owner </summary>
    AlreadyOwnedOrInScene,

    PhysicsShapeNotInScene,
    VisualLayerNotInScene,
    AudioChannelNotInScene,

    SelfRegistration,//recursive?
}

/// <summary> Contains information about an error that was caught by the engine </summary>
/// <remarks> readonly type </remarks>
public class ErrorMessage
{
    internal enum ErrorSeverity
    {
        None,
        Warning,
        Error
    }

    internal readonly ErrorSeverity severity;

    /// <summary> type of the error </summary>
    public readonly ErrorCodes error;

    /// <summary> frame the error was caught on </summary>
    public readonly uint frame;

    /// <summary> additional information about the error </summary>
    public readonly string message;

    internal ErrorMessage(ErrorCodes error, uint frame, string message)
    {
        this.error = error;
        this.frame = frame;
        this.message = message;
        severity = error switch
        {
            ErrorCodes.NotImplemented => ErrorSeverity.Error,

            ErrorCodes.BadArgument => ErrorSeverity.Error,
            ErrorCodes.BadEnumValue => ErrorSeverity.Error,
            ErrorCodes.MinGreaterThanMax => ErrorSeverity.Error,

            ErrorCodes.NullArgument => ErrorSeverity.Error,
            ErrorCodes.NullPet => ErrorSeverity.Error,
            ErrorCodes.NullPhysicsSpace => ErrorSeverity.Warning,
            ErrorCodes.NullVisualLayer => ErrorSeverity.Warning,
            ErrorCodes.NullAudioChannel => ErrorSeverity.Warning,
            ErrorCodes.NullPalette => ErrorSeverity.Warning,
            ErrorCodes.NullTexture => ErrorSeverity.Warning,
            ErrorCodes.NullAnimation => ErrorSeverity.Warning,
            ErrorCodes.NullWaveShape => ErrorSeverity.Warning,

            ErrorCodes.EmptyPalette => ErrorSeverity.Warning,
            ErrorCodes.MissingAnimation => ErrorSeverity.Warning,
            ErrorCodes.EmptyAnimation => ErrorSeverity.Warning,
            ErrorCodes.PlaybackError => ErrorSeverity.Warning,

            ErrorCodes.NoScenes => ErrorSeverity.None,
            ErrorCodes.NoActiveScene => ErrorSeverity.None,

            ErrorCodes.AlreadyOwnedOrInScene => ErrorSeverity.Warning,

            ErrorCodes.PhysicsShapeNotInScene => ErrorSeverity.Warning,
            ErrorCodes.VisualLayerNotInScene => ErrorSeverity.Warning,
            ErrorCodes.AudioChannelNotInScene => ErrorSeverity.Warning,

            ErrorCodes.SelfRegistration => ErrorSeverity.Error,

            _ => ErrorSeverity.None
        };
    }
}