using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Audio;

public static class Speakers
{
    public const int SampleRate = 48000;

    internal static AudioChannel speakersChannel = new(new("SpkChnl", 0));

    public static int SamplesPerFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => SampleRate / Engine.Frequency;//inline?
    }

    public static Sample[] GetFrame() => speakersChannel.samples;
}
