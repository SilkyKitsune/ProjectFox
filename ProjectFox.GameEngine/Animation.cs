﻿using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine;

public abstract class Animation//,ICopy?
{
    public enum PlaybackMode//move to own file?
    {
        Normal,
        Reverse,
        PingPong
    }

    public abstract class Frame
    {
        public int delay = 0;
    }

    private static readonly NameID name = new("Anmtion", 0);

    private bool back = false;//rename?

    private int time = 0;
    
    internal int frameIndex = 0;

    public bool play = false, loop = false;

    public PlaybackMode playbackMode = PlaybackMode.Normal;

    public abstract int FrameCount { get; }

    public int FrameIndex
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => frameIndex;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => frameIndex = Math.Wrap(value, 0, FrameCount - 1);
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
                        if (++frameIndex >= length) frameIndex = loop ? 0 : length - 1;
                        back = false;
                        break;
                    case PlaybackMode.Reverse:
                        if (--frameIndex < 0) frameIndex = loop ? length - 1 : 0;
                        back = true;
                        break;
                    case PlaybackMode.PingPong:
                        frameIndex += back ? -1 : 1;
                        if (frameIndex >= length)
                        {
                            frameIndex = length - 2;
                            back = true;
                        }
                        else if (frameIndex < 0)
                        {
                            frameIndex = loop ? 1 : 0;
                            back = !loop;
                        }
                        break;
                    default:
                        Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(playbackMode));
                        playbackMode = PlaybackMode.Normal;
                        break;
                }
            }
        }
    }
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
}