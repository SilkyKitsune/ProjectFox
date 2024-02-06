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

    public float volume = 1f, leftVolume = 1f, rightVolume = 1f;//, panning -1 = left, 1 = right, 0 = center
    
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
    public void Clear() => samples.Clear();

    protected internal virtual void Blend()
    {
        Sample[] samples = blendAll ? this.samples.ToArray() : this.samples.elements[..Speakers.SamplesPerFrame];//inline?

        int addedLength = samples.Length - Speakers.speakersChannel.samples.length;
        if (addedLength > 0) Speakers.speakersChannel.samples.AddLength(addedLength);

        float l = volume * leftVolume, r = volume * rightVolume;
        
        //incorporate pan
        /*
        if (pan < 0)
          left * 1 + (right * -pan)
          right * (1 - -pan)
        else if (pan > 0)
          left * (1 - pan)
          right * 1 + (left * pan)
        */

        for (int i = 0; i < samples.Length; i++)
        {
            Sample channelSample = samples[i], speakerSample = Speakers.speakersChannel.samples.elements[i];
            Speakers.speakersChannel.samples.elements[i] = new(//could the double clamp be abbreviated?
                (short)(Math.Clamp(speakerSample.left + (channelSample.left * l), short.MinValue, short.MaxValue)),
                (short)(Math.Clamp(speakerSample.right + (channelSample.right * r), short.MinValue, short.MaxValue)));
        }
    }
}