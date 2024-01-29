using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Data;
using D = ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public struct Sample : IData<Sample>//other interfaces?
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public static Sample FromBytes(byte[] bytes, bool littleEndian) => new(
            D.ToInt16(new byte[2] { bytes[0], bytes[1] }, littleEndian),
            D.ToInt16(new byte[2] { bytes[2], bytes[3] }, littleEndian));

    //FromBytesMono?

    public static Sample[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < 4) throw new ArgumentException();

        Sample[] values = new Sample[bytes.Length / 4];
        for (int i = 0, j = 0; i < values.Length; i++)
            values[i] = new(
                D.ToInt16(new byte[2] { bytes[j++], bytes[j++] }, littleEndian),
                D.ToInt16(new byte[2] { bytes[j++], bytes[j++] }, littleEndian));
        return values;
    }

    //FromBytesMultipleMono?

    public static byte[] GetBytes(Sample[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * 4];
        for (int i = 0, j = 0; i < values.Length; i++)
        {
            Sample s = values[i];
            byte[] left = D.GetBytes(s.left, littleEndian), right = D.GetBytes(s.right, littleEndian);
            bytes[j++] = left[0];
            bytes[j++] = left[1];
            bytes[j++] = right[0];
            bytes[j++] = right[1];
        }
        return bytes;
    }

    //GetBytesMono?

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    //these aren't giving errors for not fully assigning
    public Sample(short sample)
    {
        //this.sample = 0u;
        left = sample;
        right = sample;
    }
    public Sample(short left, short right)
    {
        //this.sample = 0u;
        this.left = left;
        this.right = right;
    }
    private Sample(uint sample)
    {
        //left = 0;
        //right = 0;
        this.sample = sample;
    }

    [FieldOffset(2)] public short left;
    [FieldOffset(0)] public short right;
    [FieldOffset(0)] public uint sample;

    public override string ToString() => $"(L: {left}, R: {right})";

    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(L: {Strings.ToHexString(left, littleEndian, leadingText)}, R: {Strings.ToHexString(right, littleEndian, leadingText)})";

    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(L: {Strings.ToBinString(left, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, R: {Strings.ToBinString(right, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    public byte[] GetBytes(bool littleEndian)
    {
        byte[] left = D.GetBytes(this.left, littleEndian),
            right = D.GetBytes(this.right, littleEndian);
        return new byte[4] { left[0], left[1], right[0], right[1] };
    }

    //GetBytesMono?

    public static implicit operator Sample(uint i) => new(i);

    public static implicit operator Sample(int i) => new((uint)i);
}
