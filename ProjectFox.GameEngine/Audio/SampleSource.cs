using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Audio;

public class SampleSource : AudioSource
{
    public SampleSource(NameID name) : base(name) { }

    public Sample[] waveShape = null;

    public bool loop = false;

    public int waveShapeIndex = 0;

    protected override Sample[] GetDrawInfo()
    {
        if (waveShape == null || waveShape.Length == 0) return waveShape;

        if (waveShapeIndex < 0)
        {
            Engine.SendError(ErrorCodes.BadIndex, name, nameof(waveShapeIndex));//neg index message?
            waveShapeIndex = 0;
        }

        if (loop)
        {
            Sample[] output = new Sample[channel.samples.Length];
            
            waveShapeIndex = Math.Wrap(waveShapeIndex, 0, waveShape.Length - 1);//inline later
            
            for (int i = 0; i < output.Length; i++, waveShapeIndex++)
                output[i] = waveShape[Math.Wrap(waveShapeIndex, 0, waveShape.Length - 1)];//inline later

            return output;
        }

        if (waveShapeIndex >= waveShape.Length) return new Sample[0];

        int index = waveShapeIndex, lastIndex = waveShapeIndex + channel.samples.Length;

        waveShapeIndex = lastIndex;

        return lastIndex >= waveShape.Length ? waveShape[index..] : waveShape[index..lastIndex];
    }
}