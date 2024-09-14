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

public class SampleSource : AudioSource//move to own file?
{
    public SampleSource(NameID name) : base(name) { }

    public Sample[] waveShape = null;

    protected override Sample[] GetDrawInfo() => waveShape;
}

#if DEBUG
public class OscillatorSource : AudioSource
{
    public enum InterpolationMode
    {
        Up,
        Down,
        Both
    }

    public enum Note : int
    {
        ///
        CNatural = 0,
        ///
        CSharp = 1,
        ///
        DNatural = 2,
        ///
        DSharp = 3,
        ///
        ENatural = 4,
        ///
        FNatural = 5,
        ///
        FSharp = 6,
        ///
        GNatural = 7,
        ///
        GSharp = 8,
        ///
        ANatural = 9,
        ///
        ASharp = 10,
        ///
        BNatural = 11,

        ///
        DFlat = CSharp,
        ///
        EFlat = DSharp,
        ///
        GFlat = FSharp,
        ///
        AFlat = GSharp,
        ///
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

    private OscillatorSource(NameID name, Sample[][][] waveShapes/*, bool interpolateEmptyNotes*/) : base(name)
    {

    }

    public OscillatorSource(NameID name, Sample[] waveShape, int octave, Note note/*, float pitchOffset = 0f, float freqOffset = 0f*/) : base(name)
    {
        if (octave < 0 || octave >= waveShapes.Length)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(octave), "Octave must be witin 0-9!");
            octave = 4;
        }

        if (note < Note.CNatural || note > Note.BNatural)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(note), "Note must be a valid constant!");
            note = Note.ANatural;
        }

        float baseFreq = freqs[(int)note];
        for (int k = 0; k < octave; k++) baseFreq *= 2;

        for (int i = 0; i < waveShapes.Length; i++)
        {
            Sample[][] oct = waveShapes[i] = new Sample[12][];
            for (int j = 0; j < oct.Length; j++)
            {
                float f = freqs[j];
                for (int k = 0; k < i; k++) f *= 2;
                oct[j] = Math.Scale(waveShape, baseFreq / f);
            }
        }
    }

    private readonly Sample[][][] waveShapes = new Sample[10][][];//[Octaves][Notes][WaveShapes]

    private int waveShapeIndex = 0, octave = 4;

    private Note note = Note.ANatural;

    public bool repeatWaveShape = true, resetPhase = true;//rename resetPhase?
    
    public float pitchOffset = 0f, freqOffset = 0f;//, phase = 0f?

    //samplerate?, polyphony?

    public int Octave
    {
        get => octave;
        set
        {
            if (value != octave)
            {
                octave = Math.Clamp(value, 0, waveShapes.Length);
                if (resetPhase) waveShapeIndex = 0;//phase
            }
        }
    }

    public Note Note_//temp name
    {
        get => note;
        set
        {
            if (value != note)
            {
                note = (Note)Math.Clamp((int)value, (int)Note.CNatural, (int)Note.BNatural);//is this okay?
                if (resetPhase) waveShapeIndex = 0;//phase
            }
        }
    }
        
    protected override Sample[] GetDrawInfo()
        {
        int octave = this.octave, note = (int)this.note;
        float pitchOffset = this.pitchOffset;

        //could this be simplified?
        if (pitchOffset >= 1f)
            {
            int p = (int)pitchOffset, max = (int)Note.BNatural;
            pitchOffset -= p;
            note += p;
            while (note > max)
                    {
                note = note - max;
                octave++;
            }
            if (octave >= waveShapes.Length) octave = waveShapes.Length - 1;
        }
        else if (pitchOffset <= -1f)
        {
            int p = (int)pitchOffset, max = (int)Note.BNatural, min = (int)Note.CNatural;
            pitchOffset -= p;
            note += p;
            while (note < min)
            {
                note = max + note;
                octave--;
                    }
            if (octave < 0) octave = 0;
            }
        //print vars for debug
        
        Sample[] baseNote = waveShapes[octave][note], output = new Sample[channel.samples.Length];

        if (pitchOffset != 0f || freqOffset != 0f)
        {
            float baseFreq = freqs[(int)note];
            for (int k = 0; k < octave; k++) baseFreq *= 2;

            //what to do with pitchOffset?

            //outFreq += freqOffset

            //baseNote
        }

        waveShapeIndex = Math.Wrap(waveShapeIndex, 0, baseNote.Length - 1);//inline later
        //will this condition work?
        for (int i = 0; i < output.Length && (repeatWaveShape || waveShapeIndex < baseNote.Length); i++, waveShapeIndex++)//copy until waveShape ends, leave empty zeros after, this is for oneShot waveShapes
            output[i] = baseNote[Math.Wrap(waveShapeIndex, 0, baseNote.Length - 1)];//inline later

        return output;
    }

    public void SetNote(int octave, Note note)
    {
        if (octave != this.octave || note != this.note)
        {
            octave = Math.Clamp(octave, 0, waveShapes.Length);
            note = (Note)Math.Clamp((int)note, (int)Note.CNatural, (int)Note.BNatural);//is this okay?
            if (resetPhase) waveShapeIndex = 0;//phase
        }
    }

    //public void SetNote(float freq) { }//?
}
#endif