using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine.Audio;

public abstract class AudioSource : Object2D
{
    public AudioSource(NameID name) : base(name) { }

    public AudioChannel channel = null;

    public readonly ICollection<Object2D> listeners = new Array<Object2D>(0x4);

    public bool audible = true, exceedMaxVolume = false, mono = false, swapStereo = false;

    public float volume = 1f, leftVolume = 1f, rightVolume = 1f, panning = 0f, maxVolumeDistance = 1f, minVolumeDistance = 10f;//Separate pan into leftMergeToRight/rightMergeToLeft?

    protected abstract Sample[] GetDrawInfo();

    internal sealed override void Draw(PortableScreen screen = null)
    {
#if DEBUG
        base.Draw(screen);
#endif
        
        if (!Speakers.audible || !audible) return;

        if (channel == null)
        {
            Engine.SendError(ErrorCodes.NullAudioChannel, name);
            return;
        }

        if (!channel.audible) return;

        Scene scene = owner == null ? this.scene : (owner.owner == null ? owner.scene : owner.Scene);

        if (scene != channel.scene)
            Engine.SendError(ErrorCodes.AudioChannelNotInScene, name, channel.name.ToString(),
                $"AudioSource '{name}' drew to channel from a null/different scene");

        Sample[] waveShape = GetDrawInfo();

        if (waveShape == null)
        {
            Engine.SendError(ErrorCodes.NullWaveShape, name);
            return;
        }

        if (waveShape.Length == 0) return;//length error?

        if (volume == 0f || (leftVolume == 0f && rightVolume == 0) ||//should this be before GetDrawInfo()?
            channel.volume == 0 || (channel.leftVolume == 0f && rightVolume == 0f)) return;

        float v = volume;

        Array<Object2D> listeners = (Array<Object2D>)this.listeners;
        if (listeners.length > 0)
        {
            if (minVolumeDistance < maxVolumeDistance)
                Engine.SendError(ErrorCodes.MinGreaterThanMax, name, nameof(minVolumeDistance));
            else
            {
                float distance = Closest(listeners.ToArray()).position.DistanceSquared(position);

                if (distance >= minVolumeDistance * minVolumeDistance) v = 0f;
                else if (exceedMaxVolume || distance > maxVolumeDistance * maxVolumeDistance)
                    v *= 1 - ((Math.SqrRoot(distance) - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
            }
        }

        if (v == 0f) return;

            bool leftPan = panning < 0, rightPan = panning > 0;//clamp pan? what happens if it's not?
            float l = v * leftVolume, r = v * rightVolume, pan = leftPan ? -panning : panning, reversePan = 1 - pan;

        for (int i = 0, j = 0; i < waveShape.Length && j < channel.samples.Length; i++, j++)
            {
            Sample sample = waveShape[i];
                float left = sample.left * l, right = sample.right * r;

                if (mono)
                {
                    left += right;
                    right = left;
                }
                else//could this be simplified?
                {
                    if (leftPan)
                    {
                        left += right * pan;
                        right *= reversePan;
                    }
                    else if (rightPan)
                    {
                        right += left * pan;
                        left *= reversePan;
                    }

                    if (swapStereo)
                    {
                        float f = left;
                        left = right;
                        right = f;
                    }
                }

                if (!channel.monophonic)
                {
                Sample channelSample = channel.samples[j];
                    left += channelSample.left;
                    right += channelSample.right;
                }

                channel.samples[i] = new(
                    (short)(Math.Clamp(left, short.MinValue, short.MaxValue)),
                    (short)(Math.Clamp(right, short.MinValue, short.MaxValue)));
            }
        }
}