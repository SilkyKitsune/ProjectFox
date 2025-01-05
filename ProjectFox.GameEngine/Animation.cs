using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine;

public abstract class Animation : ICopy<Animation>
{
    public enum PlaybackMode
    {
        Normal,
        Reverse,
        PingPong,
        ReversePingPong
    }

    public abstract class Frame
    {
        public int delay = 0;
    }

    private static readonly NameID name = new("Anmtion", 0);

    private int time = 0, playbackIndex = 0;

    internal int frameIndex = 0;

    public bool play = false, loop = false;

    public PlaybackMode playbackMode = PlaybackMode.Normal;

    public abstract int FrameCount { get; }

    public int FrameIndex
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => frameIndex;
    }

    public int PlaybackLength
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]//?
        get
        {
            switch (playbackMode)
            {
                case PlaybackMode.Normal:
                case PlaybackMode.Reverse:
                    return FrameCount;
                case PlaybackMode.PingPong:
                case PlaybackMode.ReversePingPong:
                    return FrameCount * 2 - 1;
                default:
                    Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(playbackMode));
                    playbackMode = PlaybackMode.Normal;
                    goto case PlaybackMode.Normal;
            }
        }
    }

    public int PlaybackIndex
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => playbackIndex;
        set
        {
            int length = FrameCount, lastIndex = length - 1, lastIndexP = lastIndex * 2 + 1;
            switch (playbackMode)
            {
                case PlaybackMode.Normal:
                    frameIndex = playbackIndex = Math.Wrap(value, 0, lastIndex);
                    break;
                case PlaybackMode.Reverse:
                    frameIndex = lastIndex - (playbackIndex = Math.Wrap(value, 0, lastIndex));
                    break;
                case PlaybackMode.PingPong:
                    frameIndex = (playbackIndex = Math.Wrap(value, 0, lastIndexP)) >= length ? lastIndexP - playbackIndex : playbackIndex;
                    break;
                case PlaybackMode.ReversePingPong:
                    frameIndex = (playbackIndex = Math.Wrap(value, 0, lastIndexP)) >= length ? playbackIndex - lastIndex : lastIndex - playbackIndex;
                    break;
                default:
                    Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(playbackMode));
                    playbackMode = PlaybackMode.Normal;
                    break;
    }
        }
    }

    private protected abstract void GetFrame(out Frame frame, out int frameCount);

    internal void _animate()
    {
        if (play)
        {
            GetFrame(out Frame frame, out int length);

            if (length <= 0)
            {
                Engine.SendError(ErrorCodes.PlaybackError, name, null,
                    $"Animation cannot play when {nameof(FrameCount)} == 0");
                return;
            }
            
            if (++time >= frame.delay)
            {
                time = 0;
                switch (playbackMode)
                {
                    case PlaybackMode.Normal:
                        if (++playbackIndex >= length) playbackIndex = loop ? 0 : length - 1;
                        frameIndex = playbackIndex;
                        break;
                    case PlaybackMode.Reverse:
                        if (++playbackIndex >= length) playbackIndex = loop ? 0 : length - 1;
                        frameIndex = length - playbackIndex - 1;
                        break;
                    case PlaybackMode.PingPong:
                        {
                            int playbackLength = length * 2 - 1;
                            if (++playbackIndex >= playbackLength) playbackIndex = loop ? 1 : playbackLength - 1;
                            frameIndex = playbackIndex >= length ? playbackLength - playbackIndex - 1 : playbackIndex;
                            break;
                        }
                    case PlaybackMode.ReversePingPong:
                        {
                            int playbackLength = length * 2 - 1, lastIndex = length - 1;
                            if (++playbackIndex >= playbackLength) playbackIndex = loop ? 1 : playbackLength - 1;
                            frameIndex = playbackIndex >= length ? playbackIndex - lastIndex : lastIndex - playbackIndex;
                            break;
                        }
                    default:
                        Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(playbackMode));
                        playbackMode = PlaybackMode.Normal;
                        break;
                }
            }
        }
    }

    public abstract void DeepCopy(out Animation copy);

    public abstract void ShallowCopy(out Animation copy);
}

public sealed class DelegateAnimation : Animation
{
    public sealed class DelegateFrame : Frame
    {
        public delegate void AnimationEvent(params Object[] objects);

        public AnimationEvent method = null;
        public bool performEachFrame = false;
    }

    public DelegateAnimation(params DelegateFrame[] frames) => this.frames.Add(frames);

    private bool hasDoneOperation = false;//rename?
    private int prevFrameIndex = 0;

    public readonly ICollection<DelegateFrame> frames = new Array<DelegateFrame>(0x10);

    public override int FrameCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ((Array<DelegateFrame>)frames).length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private protected override void GetFrame(out Frame frame, out int frameCount)
    {
        Array<DelegateFrame> frames = (Array<DelegateFrame>)this.frames;
        frame = frames.elements[frameIndex];
        frameCount = frames.length;
    }

    public void Run(params Object[] objects)
    {
        DelegateFrame frame = ((Array<DelegateFrame>)frames).elements[frameIndex];
        if (frame.performEachFrame || !hasDoneOperation)
        {
            frame.method?.Invoke(objects);
            hasDoneOperation = true;
        }
        _animate();
        if (prevFrameIndex != frameIndex) hasDoneOperation = false;//what if frame index is changed manually?
        //would moving it to start fix that?
    }

    public override void DeepCopy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public override void ShallowCopy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }
}