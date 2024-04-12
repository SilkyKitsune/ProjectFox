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
        
        if (!audible) return;

        if (channel == null)
        {
            Engine.SendError(ErrorCodes.NullAudioChannel, name);
            return;
        }

        if (!channel.audible) return;

        if (scene != channel.scene || (owner != null && owner.scene != channel.scene))
            Engine.SendError(ErrorCodes.AudioChannelNotInScene, name, channel.name.ToString(),
                $"AudioSource '{name}' drew to channel from a null/different scene");

        Sample[] waveShape = GetDrawInfo();//if waveShape is changed between frames waveShapeIndex could be invalid, for loop condition covers that but will skip playback on first frame

        if (waveShape == null)
        {
            Engine.SendError(ErrorCodes.NullWaveShape, name);
            return;
        }

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

public class SampleSource : AudioSource//move to own file?
{
    public SampleSource(NameID name) : base(name) { }

    public Sample[] waveShape = null;

    protected override Sample[] GetDrawInfo() => waveShape;
}

#if DEBUG
internal class OSCSource : AudioSource
{
    public enum InterpolationMode
    {
        Up,
        Down,
        Both
    }

    public enum Note
    {
        CNatural = 0,
        CSharp = 1,
        DNatural = 2,
        DSharp = 3,
        ENatural = 4,
        FNatural = 5,
        FSharp = 6,
        GNatural = 7,
        GSharp = 8,
        ANatural = 9,
        ASharp = 10,
        BNatural = 11,

        DFlat = CSharp,
        EFlat = DSharp,
        GFlat = FSharp,
        AFlat = GSharp,
        BFlat = ASharp,
    }

    private static readonly float[] freqs =
    {
        16.35160f, //C
        17.32391f, //C#
        18.35405f, //D
        19.44544f, //D#
        20.60172f, //E
        21.82676f, //F
        23.12465f, //F#
        24.49971f, //G
        25.95654f, //G#
        27.50000f, //A
        29.13524f, //A#
        30.86771f, //B
    };

    //public OSCSource(NameID name) default to sine?

    public OSCSource(NameID name, Sample[] waveShape/*, int octave?*/) : base(name) { }//input an A4 note 440hz?

    //Sample[][] single cycle waveforms, each Sample[] is an A note, octaves 0-9
    private readonly Sample[][] waveShapes = new Sample[10][];

    public InterpolationMode mode = InterpolationMode.Up;

    public bool repeat = true;//rename? oneShot?

    public Note note = Note.ANatural;//should note/oct be properties to reset wave offset?

    public int octave = 4;

    public float pitchOffest = 0f;//, phase = 0f?

    //samplerate?

    //polyphony?

    protected override Sample[] GetDrawInfo()
    {
        bool down = note < Note.ANatural, up = note > Note.ANatural;

        int oct = 0;//temp
        
        //how to tell if note is above or below A?
        Sample[] baseNote = waveShapes[oct], output = new Sample[channel.samples.Length];
        //float freq = freqs[(int)note] * Math.Pow(2, octave);
        if (note != Note.ANatural)//down || up
        {
            Sample[] newNote = new Sample[0];//temp length

            switch (mode)
            {
                case InterpolationMode.Up:
                    for (int i = 0; i < newNote.Length; i++)
                    {
                        float f = (float)i / newNote.Length * baseNote.Length;
                        //newNote[i] = (baseNote[((int)f) + 1] - baseNote[(int)f]) * Math.fraction(f) + baseNote[(int)f];
                        newNote[i] = baseNote[(int)f];//temp
                    }
                    break;
                case InterpolationMode.Down:
                    break;
                case InterpolationMode.Both:
                    break;
            }

            baseNote = newNote;
        }

        int offset = 0;//make member
        for (int i = 0; i < output.Length; i++, offset++)//copy until waveShape ends, leave empty zeros after, this is for oneShot waveShapes
            output[i] = baseNote[Math.Wrap(offset, 0, baseNote.Length)];

        return Engine.SendError<Sample[]>(ErrorCodes.NotImplemented, name);
    }

    //public void SetNote(int octave, Note note, float pitchOffest = 0f) { }//?

    //public void SetNote(float freq) { }//?
}
#endif