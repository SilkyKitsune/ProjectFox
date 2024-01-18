using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Color
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params Color[] values)
    {
        string str = "";
        foreach (Color value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params Color[] values)
    {
        string str = "";
        foreach (Color value in values) str += value.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color FromBytes(byte[] bytes, bool littleEndian) =>
        (bytes == null || bytes.Length < 4) ? throw new ArgumentException() : new(bytes[0], bytes[1], bytes[2], bytes[3]);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color FromBytes24(byte[] bytes) =>
        (bytes == null || bytes.Length < 3) ? throw new ArgumentException() : new(bytes[0], bytes[1], bytes[2]);

    public static Color[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < 4) throw new ArgumentException();

        Color[] values = new Color[bytes.Length / 4];
        for (int i = 0, j = 0; i < values.Length; i++)
            values[i] = new(bytes[j++], bytes[j++], bytes[j++], bytes[j++]);
        return values;
    }

    public static Color[] FromBytesMultiple24(byte[] bytes)
    {
        if (bytes == null || bytes.Length < 3) throw new ArgumentException();

        Color[] values = new Color[bytes.Length / 3];
        for (int i = 0, j = 0; i < values.Length; i++)
            values[i] = new(bytes[j++], bytes[j++], bytes[j++]);
        return values;
    }

    public static byte[] GetBytes(Color[] values, bool littleEndian)//bgr overload?
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * 4];
        for (int i = 0, j = 0; i < values.Length; i++)
        {
            Color c = values[i];
            bytes[j++] = c.r;
            bytes[j++] = c.g;
            bytes[j++] = c.b;
            bytes[j++] = c.a;
        }
        return bytes;
    }

    public static byte[] GetBytes24(Color[] values)//bgr overload?
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * 3];
        for (int i = 0, j = 0; i < values.Length; i++)
        {
            Color c = values[i];
            bytes[j++] = c.r;
            bytes[j++] = c.g;
            bytes[j++] = c.b;
        }
        return bytes;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params Color[] values)
    {
        string str = "";
        foreach (Color value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params Color[] values)
    {
        string str = "";
        foreach (Color value in values) str += value.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] GetBytes(bool littleEndian) => new byte[4] { r, g, b, a };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public byte[] GetBytes24() => new byte[3] { r, g, b };
}
