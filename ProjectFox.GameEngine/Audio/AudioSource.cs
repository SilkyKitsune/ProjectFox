using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine.Audio;

public abstract class AudioSource : Object2D
{
    public AudioSource(NameID name) : base(name) { }

    private int waveShapeIndex = 0;

    public AudioChannel channel = null;

    public readonly ICollection<Object2D> listeners = new Array<Object2D>(0x4);

    public bool audible = true, loop = false, exceedMaxVolume = false, mono = false, swapStereo = false;//rename mono? mergestereo?

    public float volume = 1f, leftVolume = 1f, rightVolume = 1f, panning = 0f, maxVolumeDistance = 1f, minVolumeDistance = 10f;//Separate pan into leftMergeToRight/rightMergeToLeft?

    protected abstract Sample[] GetDrawInfo();

    internal sealed override void _draw(VisualLayer layer = null)
    {
#if DEBUG
        base._draw();
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

        Sample[] waveShape = GetDrawInfo();//if waveShape is changed between frames waveShapeIndex could be invalid, for loop condition covers that but will skip playback on first frame

        if (waveShape == null)
        {
            Engine.SendError(ErrorCodes.NullWaveShape, name);
            return;
        }

        if (waveShape.Length == 0) return;//length error?

        bool noVol = volume == 0f || (leftVolume == 0f && rightVolume == 0) ||
            channel.volume == 0 || (channel.leftVolume == 0f && rightVolume == 0f);

        float v = volume;

        Array<Object2D> listeners = (Array<Object2D>)this.listeners;
        if (!noVol && listeners.length > 0)
        {
            if (minVolumeDistance < maxVolumeDistance)
                Engine.SendError(ErrorCodes.MinGreaterThanMax, name, nameof(minVolumeDistance));
            else
            {
                float distance = Closest(listeners.ToArray()).position.Distance(position);//overload for awway<>?
                if (distance >= minVolumeDistance) v = 0f;
                else if (exceedMaxVolume || distance > maxVolumeDistance)
                    v *= 1 - ((distance - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
            }
        }

        if (noVol || v == 0f) waveShapeIndex += channel.samples.Length;
        else
        {
            bool leftPan = panning < 0, rightPan = panning > 0;//clamp pan? what happens if it's not?
            float l = v * leftVolume, r = v * rightVolume, pan = leftPan ? -panning : panning, reversePan = 1 - pan;

            for (int i = 0; i < channel.samples.Length && waveShapeIndex < waveShape.Length; i++, waveShapeIndex++)
            {
                Sample sample = waveShape[waveShapeIndex];//move waveShapeIndex < waveShape.Length here? or maybe wrap it? this might be where the click on loop comes from
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
                    Sample channelSample = channel.samples[i];
                    left += channelSample.left;
                    right += channelSample.right;
                }

                channel.samples[i] = new(
                    (short)(Math.Clamp(left, short.MinValue, short.MaxValue)),
                    (short)(Math.Clamp(right, short.MinValue, short.MaxValue)));
            }
        }

        if (waveShapeIndex >= waveShape.Length)
        {
            waveShapeIndex = 0;
            if (!loop) audible = false;
        }
    }
}