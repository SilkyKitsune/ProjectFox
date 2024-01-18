using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Collections;

public static partial class Strings
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static string NibbleBin(int i, bool littleEndian) => i switch
    {
        0b0000 => littleEndian ? "0000" : "0000",
        0b0001 => littleEndian ? "1000" : "0001",
        0b0010 => littleEndian ? "0100" : "0010",
        0b0011 => littleEndian ? "1100" : "0011",
        0b0100 => littleEndian ? "0010" : "0100",
        0b0101 => littleEndian ? "1010" : "0101",
        0b0110 => littleEndian ? "0110" : "0110",
        0b0111 => littleEndian ? "1110" : "0111",
        0b1000 => littleEndian ? "0001" : "1000",
        0b1001 => littleEndian ? "1001" : "1001",
        0b1010 => littleEndian ? "0101" : "1010",
        0b1011 => littleEndian ? "1101" : "1011",
        0b1100 => littleEndian ? "0011" : "1100",
        0b1101 => littleEndian ? "1011" : "1101",
        0b1110 => littleEndian ? "0111" : "1110",
        0b1111 => littleEndian ? "1111" : "1111",
        _ => throw new Exception($"Unexpected nibble value {i}")
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(byte b, bool littleEndian = false, bool leadingText = false, char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") + $"{NibbleBin(b & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(b >> 4 & FourBits, littleEndian)}" :
        (leadingText ? "0b" : "") + $"{NibbleBin(b >> 4 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(b & FourBits, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(short s, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
             $"{NibbleBin(s & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 4 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(s >> 8 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 12 & FourBits, littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin(s >> 12 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 8 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(s >> 4 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s & FourBits, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(int i, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
              $"{NibbleBin(i & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 4 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 8 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 12 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 16 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 20 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 24 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 28 & FourBits, littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin(i >> 28 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 24 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 20 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 16 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 12 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 8 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 4 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i & FourBits, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(long l, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
              $"{NibbleBin((int)(l & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 4 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 8 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 12 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 16 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 20 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 24 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 28 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 32 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 36 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 40 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 44 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 48 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 52 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 56 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 60 & FourBits), littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin((int)(l >> 60 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 56 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 52 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 48 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 44 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 40 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 36 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 32 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 28 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 24 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 20 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 16 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 12 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 8 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 4 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l & FourBits), littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToBinString(float f, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        ToBinString(*(int*)&f, littleEndian, leadingText, byteSeparator, nibbleSeparator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToBinString(double d, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        ToBinString(*(long*)&d, littleEndian, leadingText, byteSeparator, nibbleSeparator);

    #region Concat
    public static string ConcatBin(bool littleEndian, bool leadingText, char nibbleSeparator, params byte[] values)
    {
        string str = "";
        foreach (byte value in values) str += ToBinString(value, littleEndian, leadingText, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char nibbleSeparator, ReadOnlySpan<byte> values)
    {
        string str = "";
        foreach (byte value in values) str += ToBinString(value, littleEndian, leadingText, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params short[] values)
    {
        string str = "";
        foreach (short value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, ReadOnlySpan<short> values)
    {
        string str = "";
        foreach (short value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params int[] values)
    {
        string str = "";
        foreach (int value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, ReadOnlySpan<int> values)
    {
        string str = "";
        foreach (int value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params long[] values)
    {
        string str = "";
        foreach (long value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, ReadOnlySpan<long> values)
    {
        string str = "";
        foreach (long value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params float[] values)
    {
        string str = "";
        foreach (float value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, ReadOnlySpan<float> values)
    {
        string str = "";
        foreach (float value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params double[] values)
    {
        string str = "";
        foreach (double value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, ReadOnlySpan<double> values)
    {
        string str = "";
        foreach (double value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator);
        return str;
    }
    #endregion

    #region Join
    public static string JoinBin(bool littleEndian, bool leadingText, char nibbleSeparator, string elementSeparator, params byte[] values)
    {
        string str = "";
        foreach (byte value in values) str += ToBinString(value, littleEndian, leadingText, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char nibbleSeparator, string elementSeparator, ReadOnlySpan<byte> values)
    {
        string str = "";
        foreach (byte value in values) str += ToBinString(value, littleEndian, leadingText, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params short[] values)
    {
        string str = "";
        foreach (short value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, ReadOnlySpan<short> values)
    {
        string str = "";
        foreach (short value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params int[] values)
    {
        string str = "";
        foreach (int value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, ReadOnlySpan<int> values)
    {
        string str = "";
        foreach (int value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params long[] values)
    {
        string str = "";
        foreach (long value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, ReadOnlySpan<long> values)
    {
        string str = "";
        foreach (long value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params float[] values)
    {
        string str = "";
        foreach (float value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, ReadOnlySpan<float> values)
    {
        string str = "";
        foreach (float value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params double[] values)
    {
        string str = "";
        foreach (double value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, ReadOnlySpan<double> values)
    {
        string str = "";
        foreach (double value in values) str += ToBinString(value, littleEndian, leadingText, byteSeparator, nibbleSeparator) + elementSeparator;
        return str;
    }
    #endregion
}
