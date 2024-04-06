using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Audio;
using NAudio.Wave;

namespace ProjectFox.NAudio;

public sealed class GameAudioOutput//GameAudioPlayback? GameAudioPlayer?
{
    private sealed class GameWaveProvider : IWaveProvider
    {
        private readonly WaveFormat format = new(Speakers.SampleRate, 16, 2);

        internal readonly AutoSizedArray<Sample> frames = new(Speakers.SampleRate);//rename?
        
        public WaveFormat WaveFormat => format;

        public unsafe int Read(byte[] buffer, int offset, int count)
        {
            if (frames.Length == 0) return 0;

            int sampleCount = count / sizeof(Sample);

            byte[] data = Sample.GetBytes(frames.GetRange(0, sampleCount), true);
            frames.RemoveRange(0, sampleCount);

            if (count > data.Length) count = data.Length;

            buffer ??= new byte[count];//should this be earlier?

            for (int i = 0, l = offset + count; i < count && offset < l; i++, offset++)
                buffer[offset] = data[i];
            return count;
        }
    }

    public GameAudioOutput()
    {
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