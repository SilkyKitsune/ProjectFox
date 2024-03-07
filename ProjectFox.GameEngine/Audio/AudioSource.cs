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
        
        if (!audible || volume <= 0f || (leftVolume <= 0f && rightVolume <= 0)) return;

        if (channel == null)
        {
            Engine.SendError(ErrorCodes.NullAudioChannel, name);
            return;
        }

        if (!channel.audible || channel.volume <= 0 || (channel.leftVolume <= 0f && rightVolume <= 0f)) return;

        if (scene != channel.scene || (owner != null && owner.scene != channel.scene))
            Engine.SendError(ErrorCodes.AudioChannelNotInScene, name, channel.name.ToString(),
                $"AudioSource '{name}' drew to channel from a null/different scene");

        Sample[] waveShape = GetDrawInfo();

        if (waveShape == null)
        {
            Engine.SendError(ErrorCodes.NullWaveShape, name);
            return;
        }

        float v = volume;

        Array<Object2D> listeners = (Array<Object2D>)this.listeners;
        if (listeners.length > 0)
        {
            //clamp distances?

            //overload for awway<>?
            float dist = Closest(listeners.ToArray()).position.Distance(position),
                f = 1 - ((dist - maxVolumeDistance) / (minVolumeDistance - maxVolumeDistance));
            v *= f;
            //clean up later
            //Debug.Console.QueueMessage($"{dist} : {f}");//remove
        }

        if (v <= 0f) v = 0f;
        else if (!exceedMaxVolume && v > 1f) v = 1f;//should l/r vol be clamped?

        bool leftPan = panning < 0, rightPan = panning > 0;//clamp pan?
        float l = v * leftVolume, r = v * rightVolume, pan = leftPan ? -panning : panning, reversePan = 1 - pan;
        //how to skip this when v = 0, but still advance playback?
        for (int i = 0; i < channel.samples.Length && waveShapeIndex < waveShape.Length; i++, waveShapeIndex++)
        {
            Sample sample = waveShape[waveShapeIndex];
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
            
            channel.samples[i] = new(//could the double clamp be abbreviated?
                (short)(Math.Clamp(left, short.MinValue, short.MaxValue)),
                (short)(Math.Clamp(right, short.MinValue, short.MaxValue)));
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
public class OSCSource : AudioSource
{
    public enum InterpolationMode
    {
        Up,
        Down,
        Both
    }

    public enum Note
    {
        CNatural,
        CSharp,
        DNatural,
        DSharp,
        ENatural,
        FNatural,
        FSharp,
        GNatural,
        GSharp,
        ANatural,
        ASharp,
        BNatural,

        DFlat = CSharp,
        EFlat = DSharp,
        GFlat = FSharp,
        AFlat = GSharp,
        BFlat = ASharp,
    }

    public OSCSource(NameID name, Sample[] waveShape/*, int octave?*/) : base(name) { }//input an A4 note 440hz?

    //Sample[][] single cycle waveforms, each Sample[] is an A note, octaves 0-9
    private readonly Sample[][] waveShapes = new Sample[10][];

    public InterpolationMode mode = InterpolationMode.Up;

    public bool repeat = true;//?

    public Note note = Note.ANatural;

    public int octave = 4;

    public float pitchOffest = 0f;

    //samplerate?

    //polyphony?

    protected override Sample[] GetDrawInfo() => Engine.SendError<Sample[]>(ErrorCodes.NotImplemented, name);

    //public void SetNote(int octave, Note note, float pitchOffest = 0f) { }//?

    //public void SetNote(float freq) { }//?
}
#endif