#if DEBUG
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Audio;

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