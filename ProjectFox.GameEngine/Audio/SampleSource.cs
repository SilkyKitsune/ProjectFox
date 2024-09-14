using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Audio;

public class SampleSource : AudioSource
{
    public SampleSource(NameID name) : base(name) { }

    public Sample[] waveShape = null;

    protected override Sample[] GetDrawInfo() => waveShape;
}