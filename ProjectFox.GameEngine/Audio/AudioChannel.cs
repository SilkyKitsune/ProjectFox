using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Audio;

public class AudioChannel : SceneType
{
    public AudioChannel(NameID name) : base(name) { }

    internal readonly Array<Sample> samples = new(Speakers.SampleRate);

    //polyphony? property? -1 = unlimited?
    public bool audible = true, monophonic = false, blendAll = true;//rename playperframe

    public float volume = 1f, leftVolume = 1f, rightVolume = 1f, panning = 0f;//-1 = left, 1 = right, 0 = center
    
    public sealed override Scene Scene
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scene;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value == null) scene?.RemoveAudioChannel(name);
            else value.AddAudioChannel(this);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => samples.Clear();//play per frame doesn't work because channels are cleared each frame

    protected internal virtual void Blend()//this needs an argument to pass to base
    {
        Sample[] outSamples = this.samples.elements[..(blendAll ? this.samples.length : Speakers.SamplesPerFrame)];//inline samples per frame?

        int addedLength = outSamples.Length - Speakers.speakersChannel.samples.length;
        if (addedLength > 0) Speakers.speakersChannel.samples.AddLength(addedLength);

        bool leftPan = panning < 0, rightPan = panning > 0;
        float l = volume * leftVolume, r = volume * rightVolume, pan = leftPan ? -panning : panning, reversePan = 1 - pan;

        for (int i = 0; i < outSamples.Length; i++)
        {
            Sample channelSample = outSamples[i], speakerSample = Speakers.speakersChannel.samples.elements[i];
            float left = speakerSample.left + (channelSample.left * l), right = speakerSample.right + (channelSample.right * r);//speaker sample should be after pan

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

            Speakers.speakersChannel.samples.elements[i] = new(//could the double clamp be abbreviated?
                (short)(Math.Clamp(left, short.MinValue, short.MaxValue)),
                (short)(Math.Clamp(right, short.MinValue, short.MaxValue)));
        }
    }
}