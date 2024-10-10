using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Audio;
using NAudio.Wave;

namespace ProjectFox.NAudio;

public sealed class GameAudioOutput
{
    private sealed class GameWaveProvider : IWaveProvider
    {
        private readonly WaveFormat format = new(Speakers.SampleRate, 16, 2);

        internal readonly AutoSizedArray<Sample> samples = new(Speakers.SampleRate);
        
        public WaveFormat WaveFormat => format;

        public unsafe int Read(byte[] buffer, int offset, int count)
        {
            if (samples.Length == 0) return 0;

            int sampleCount = count / sizeof(Sample);

            byte[] data = Sample.GetBytes(samples.GetRange(0, sampleCount), true);
            samples.RemoveRange(0, sampleCount);

            if (count > data.Length) count = data.Length;

            buffer ??= new byte[count];

            for (int i = 0, l = offset + count; i < count && offset < l; i++, offset++)
                buffer[offset] = data[i];
            return count;
        }
    }

    public GameAudioOutput(int latency = 60)
    {
        waveOut.DesiredLatency = latency;
        waveOut.Init(provider);
        Engine.FrameComplete += FrameComplete;
    }

    private readonly GameWaveProvider provider = new();
    private readonly WaveOutEvent waveOut = new();

    private void FrameComplete()//will this playback even during silent frames? is that a concern?
    {
        provider.frames.Add(Speakers.GetFrame());
        waveOut.Play();
    }
    
    public void Shutdown() => waveOut.Dispose();//rename?
}