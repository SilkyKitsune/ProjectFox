using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Collections.Strings;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public static void SampleTest()
    {
        Sample blank = new Sample(), mono = new Sample(10), stereo = new Sample(10, -12), hex = (Sample)0x1234_5678u;

        SamplePrint(blank);
        SamplePrint(mono);
        SamplePrint(stereo);
        SamplePrint(hex);

        C.WriteLine(Sample.ConcatHex(false, false, blank, mono, stereo, hex));
        C.WriteLine(Sample.ConcatBin(false, false, '|', '_', blank, mono, stereo, hex));
        C.WriteLine(Sample.JoinHex(false, false, ", ", blank, mono, stereo, hex));
        C.WriteLine(Sample.JoinBin(false, false, '|', '_', ", ", blank, mono, stereo, hex));

        byte[] bytes = new byte[9] { 0x12, 0x34, 0x56, 0x78, 0x9A, 0xBC, 0xDE, 0xF0, 0x11 };
        C.WriteLine(Sample.FromBytes(bytes, false).ToHexString());
        C.WriteLine(Sample.FromBytes(bytes, true).ToHexString());
        C.WriteLine(Sample.FromBytesMono(bytes, false).ToHexString());
        C.WriteLine(Sample.FromBytesMono(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", hex.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", hex.GetBytes(true)));
        //getbytesmono

        Sample[] samples = Sample.FromBytesMultiple(bytes, false);
        C.WriteLine(Sample.JoinHex(false, false, ", ", samples));
        C.WriteLine(Sample.JoinHex(false, false, ", ", Sample.FromBytesMultiple(bytes, true)));
        C.WriteLine(Sample.JoinHex(false, false, ", ", Sample.FromBytesMultipleMono(bytes, false)));
        C.WriteLine(Sample.JoinHex(false, false, ", ", Sample.FromBytesMultipleMono(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", Sample.GetBytes(samples, false)));
        C.WriteLine(JoinHex(false, false, ", ", Sample.GetBytes(samples, true)));
        C.WriteLine(JoinHex(false, false, ", ", Sample.GetBytesMono(samples, false, false)));
        C.WriteLine(JoinHex(false, false, ", ", Sample.GetBytesMono(samples, false, true)));
        C.WriteLine(JoinHex(false, false, ", ", Sample.GetBytesMono(samples, true, false)));
        C.WriteLine(JoinHex(false, false, ", ", Sample.GetBytesMono(samples, true, true)));

        IDataTest(stereo);
    }

    private static void SamplePrint(Sample sample)
    {
        C.WriteLine(sample);
        C.WriteLine(sample.ToHexString());
        C.WriteLine(sample.ToBinString());
        C.WriteLine(sample.sample);
        C.WriteLine(ToHexString((int)sample.sample));
        C.WriteLine(ToBinString((int)sample.sample));
    }
}
