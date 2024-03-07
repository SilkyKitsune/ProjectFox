using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Audio;

public class AudioChannel : SceneType
{
    public AudioChannel(NameID name) : base(name) => Clear();

    internal Sample[] samples = null;

    //polyphony? property? -1 = unlimited?
    public bool audible = true, monophonic = false;//mono?, swapstereo?

    public float volume = 1f, leftVolume = 1f, rightVolume = 1f, panning = 0f;
    
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]//inline properties
    public void Clear() => samples = new Sample[/*matchTime ? (int)(SamplesPerFrame * TimeOfLastFrame) :*/ Speakers.SamplesPerFrame];

    protected internal virtual void Blend()//this needs an argument to pass to base
    {
        bool leftPan = panning < 0, rightPan = panning > 0;//clamp pan?
        float l = volume * leftVolume, r = volume * rightVolume, pan = leftPan ? -panning : panning, reversePan = 1 - pan;

        for (int i = 0; i < samples.Length; i++)
        {
            Sample channelSample = samples[i], speakerSample = Speakers.speakersChannel.samples[i];
            float left = channelSample.left * l, right = channelSample.right * r;

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

            Speakers.speakersChannel.samples[i] = new(//could the double clamp be abbreviated?
                (short)(Math.Clamp(speakerSample.left + left, short.MinValue, short.MaxValue)),
                (short)(Math.Clamp(speakerSample.right + right, short.MinValue, short.MaxValue)));
        }
    }
}