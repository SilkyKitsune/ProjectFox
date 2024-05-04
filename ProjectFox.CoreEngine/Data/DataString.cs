using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Data;

public static partial class Data//datastring? DataParse?
{
    private const int LowNibbleMask = 0b1111;

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

    public static string ConcatHex(bool littleEndian, bool leadingText, params byte[] values)
    {
        string str = "";
        foreach (byte value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, ReadOnlySpan<byte> values)
    {
        string str = "";
        foreach (byte value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, params short[] values)
    {
        string str = "";
        foreach (short value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, ReadOnlySpan<short> values)
    {
        string str = "";
        foreach (short value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, params int[] values)
    {
        string str = "";
        foreach (int value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, ReadOnlySpan<int> values)
    {
        string str = "";
        foreach (int value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, params long[] values)
    {
        string str = "";
        foreach (long value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, ReadOnlySpan<long> values)
    {
        string str = "";
        foreach (long value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, params float[] values)
    {
        string str = "";
        foreach (float value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, ReadOnlySpan<float> values)
    {
        string str = "";
        foreach (float value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, params double[] values)
    {
        string str = "";
        foreach (double value in values) str += ToHexString(value, littleEndian, leadingText);
        return str;
    }

    public static string ConcatHex(bool littleEndian, bool leadingText, ReadOnlySpan<double> values)
    {
        string str = "";
        foreach (double value in values) str += ToHexString(value, littleEndian, leadingText);
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

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, params byte[] values)
    {
        string str = "";
        foreach (byte value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, ReadOnlySpan<byte> values)
    {
        string str = "";
        foreach (byte value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, params short[] values)
    {
        string str = "";
        foreach (short value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, ReadOnlySpan<short> values)
    {
        string str = "";
        foreach (short value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, params int[] values)
    {
        string str = "";
        foreach (int value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, ReadOnlySpan<int> values)
    {
        string str = "";
        foreach (int value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, params long[] values)
    {
        string str = "";
        foreach (long value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, ReadOnlySpan<long> values)
    {
        string str = "";
        foreach (long value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, params float[] values)
    {
        string str = "";
        foreach (float value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, ReadOnlySpan<float> values)
    {
        string str = "";
        foreach (float value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, params double[] values)
    {
        string str = "";
        foreach (double value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string elementSeparator, ReadOnlySpan<double> values)
    {
        string str = "";
        foreach (double value in values) str += ToHexString(value, littleEndian, leadingText) + elementSeparator;
        return str;
    }
    #endregion

    #region ToString
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
        (leadingText ? "0b" : "") + $"{NibbleBin(b & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(b >> 4 & LowNibbleMask, littleEndian)}" :
        (leadingText ? "0b" : "") + $"{NibbleBin(b >> 4 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(b & LowNibbleMask, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(short s, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
             $"{NibbleBin(s & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 4 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(s >> 8 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 12 & LowNibbleMask, littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin(s >> 12 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 8 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(s >> 4 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(s & LowNibbleMask, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(int i, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
              $"{NibbleBin(i & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 4 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 8 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 12 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 16 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 20 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 24 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 28 & LowNibbleMask, littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin(i >> 28 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 24 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 20 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 16 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 12 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 8 & LowNibbleMask, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 4 & LowNibbleMask, littleEndian)}{nibbleSeparator}{NibbleBin(i & LowNibbleMask, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(long l, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
              $"{NibbleBin((int)(l & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 4 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 8 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 12 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 16 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 20 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 24 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 28 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 32 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 36 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 40 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 44 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 48 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 52 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 56 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 60 & LowNibbleMask), littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin((int)(l >> 60 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 56 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 52 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 48 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 44 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 40 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 36 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 32 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 28 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 24 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 20 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 16 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 12 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 8 & LowNibbleMask), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >> 4 & LowNibbleMask), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l & LowNibbleMask), littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToBinString(float f, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        ToBinString(*(int*)&f, littleEndian, leadingText, byteSeparator, nibbleSeparator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToBinString(double d, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        ToBinString(*(long*)&d, littleEndian, leadingText, byteSeparator, nibbleSeparator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private static char NibbleHex(int i) => i switch
    {
        0x0 => '0',
        0x1 => '1',
        0x2 => '2',
        0x3 => '3',
        0x4 => '4',
        0x5 => '5',
        0x6 => '6',
        0x7 => '7',
        0x8 => '8',
        0x9 => '9',
        0xA => 'A',
        0xB => 'B',
        0xC => 'C',
        0xD => 'D',
        0xE => 'E',
        0xF => 'F',
        _ => throw new Exception($"Unexpected nibble value {i}")
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]//should endianess be per nibble or per byte? could it be a bool?
    public static string ToHexString(byte b, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") + $"{NibbleHex(b & LowNibbleMask)}{NibbleHex(b >> 4 & LowNibbleMask)}" :
        (leadingText ? "0x" : "") + $"{NibbleHex(b >> 4 & LowNibbleMask)}{NibbleHex(b & LowNibbleMask)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(short s, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
             $"{NibbleHex(s & LowNibbleMask)}{NibbleHex(s >> 4 & LowNibbleMask)}" +
        $"{NibbleHex(s >> 8 & LowNibbleMask)}{NibbleHex(s >> 12 & LowNibbleMask)}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex(s >> 12 & LowNibbleMask)}{NibbleHex(s >> 8 & LowNibbleMask)}" +
        $"{NibbleHex(s >> 4 & LowNibbleMask)}{NibbleHex(s & LowNibbleMask)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(int i, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
              $"{NibbleHex(i & LowNibbleMask)}{NibbleHex(i >> 4 & LowNibbleMask)}" +
        $"{NibbleHex(i >> 8 & LowNibbleMask)}{NibbleHex(i >> 12 & LowNibbleMask)}" +
        $"{NibbleHex(i >> 16 & LowNibbleMask)}{NibbleHex(i >> 20 & LowNibbleMask)}" +
        $"{NibbleHex(i >> 24 & LowNibbleMask)}{NibbleHex(i >> 28 & LowNibbleMask)}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex(i >> 28 & LowNibbleMask)}{NibbleHex(i >> 24 & LowNibbleMask)}" +
        $"{NibbleHex(i >> 20 & LowNibbleMask)}{NibbleHex(i >> 16 & LowNibbleMask)}" +
        $"{NibbleHex(i >> 12 & LowNibbleMask)}{NibbleHex(i >> 8 & LowNibbleMask)}" +
        $"{NibbleHex(i >> 4 & LowNibbleMask)}{NibbleHex(i & LowNibbleMask)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(long l, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
              $"{NibbleHex((int)(l & LowNibbleMask))}{NibbleHex((int)(l >> 4 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 8 & LowNibbleMask))}{NibbleHex((int)(l >> 12 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 16 & LowNibbleMask))}{NibbleHex((int)(l >> 20 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 24 & LowNibbleMask))}{NibbleHex((int)(l >> 28 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 32 & LowNibbleMask))}{NibbleHex((int)(l >> 36 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 40 & LowNibbleMask))}{NibbleHex((int)(l >> 44 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 48 & LowNibbleMask))}{NibbleHex((int)(l >> 52 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 56 & LowNibbleMask))}{NibbleHex((int)(l >> 60 & LowNibbleMask))}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex((int)(l >> 60 & LowNibbleMask))}{NibbleHex((int)(l >> 56 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 52 & LowNibbleMask))}{NibbleHex((int)(l >> 48 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 44 & LowNibbleMask))}{NibbleHex((int)(l >> 40 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 36 & LowNibbleMask))}{NibbleHex((int)(l >> 32 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 28 & LowNibbleMask))}{NibbleHex((int)(l >> 24 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 20 & LowNibbleMask))}{NibbleHex((int)(l >> 16 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 12 & LowNibbleMask))}{NibbleHex((int)(l >> 8 & LowNibbleMask))}" +
        $"{NibbleHex((int)(l >> 4 & LowNibbleMask))}{NibbleHex((int)(l & LowNibbleMask))}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToHexString(float f, bool littleEndian = false, bool leadingText = false) => ToHexString(*(int*)&f, littleEndian, leadingText);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToHexString(double d, bool littleEndian = false, bool leadingText = false) => ToHexString(*(long*)&d, littleEndian, leadingText);
    #endregion

    #region TryParse
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseAny(string str, bool littleEndian, out byte value) =>
        byte.TryParse(str, out value) || TryParseBin(str, littleEndian, out value) || TryParseHex(str, littleEndian, out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseAny(string str, bool littleEndian, out short value) =>
        short.TryParse(str, out value) || TryParseBin(str, littleEndian, out value) || TryParseHex(str, littleEndian, out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseAny(string str, bool littleEndian, out int value) =>
        int.TryParse(str, out value) || TryParseBin(str, littleEndian, out value) || TryParseHex(str, littleEndian, out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseAny(string str, bool littleEndian, out long value) =>
        long.TryParse(str, out value) || TryParseBin(str, littleEndian, out value) || TryParseHex(str, littleEndian, out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseAny(string str, bool littleEndian, out float value) =>
        float.TryParse(str, out value) || TryParseBin(str, littleEndian, out value) || TryParseHex(str, littleEndian, out value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool TryParseAny(string str, bool littleEndian, out double value) =>
        double.TryParse(str, out value) || TryParseBin(str, littleEndian, out value) || TryParseHex(str, littleEndian, out value);

    public static bool TryParseBin(string str, bool littleEndian, out byte value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('%')) str = str.Substring(1);
        if (str.StartsWith("0B")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(byte) * 8) return false;

        for (int i = 0, j = !littleEndian ? 0 : str.Length - 1, inc = !littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            char c = str[j];
            if (c == '0') value <<= 1;
            else if (c == '1') value = (byte)(value << 1 | 1);
            else return false;
        }
        return true;
    }

    public static bool TryParseBin(string str, bool littleEndian, out short value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('%')) str = str.Substring(1);
        if (str.StartsWith("0B")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(short) * 8) return false;

        for (int i = 0, j = !littleEndian ? 0 : str.Length - 1, inc = !littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            char c = str[j];
            if (c == '0') value <<= 1;
            else if (c == '1') value = (short)(value << 1 | 1);
            else return false;
        }
        return true;
    }

    public static bool TryParseBin(string str, bool littleEndian, out int value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('%')) str = str.Substring(1);
        if (str.StartsWith("0B")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(int) * 8) return false;

        for (int i = 0, j = !littleEndian ? 0 : str.Length - 1, inc = !littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            char c = str[j];
            if (c == '0') value <<= 1;
            else if (c == '1') value = value << 1 | 1;
            else return false;
        }
        return true;
    }

    public static bool TryParseBin(string str, bool littleEndian, out long value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('%')) str = str.Substring(1);
        if (str.StartsWith("0B")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(long) * 8) return false;

        for (int i = 0, j = !littleEndian ? 0 : str.Length - 1, inc = !littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            char c = str[j];
            if (c == '0') value <<= 1;
            else if (c == '1') value = value << 1 | 1;
            else return false;
        }
        return true;
    }

    public unsafe static bool TryParseBin(string str, bool littleEndian, out float value)
    {
        bool success = TryParseBin(str, littleEndian, out int i);
        value = *(float*)&i;
        return success;
    }

    public unsafe static bool TryParseBin(string str, bool littleEndian, out double value)
    {
        bool success = TryParseBin(str, littleEndian, out long l);
        value = *(double*)&l;
        return success;
    }

    public static bool TryParseHex(string str, bool littleEndian, out byte value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('$')) str = str.Substring(1);
        else if (str.StartsWith("0X")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(byte) * 2) return false;

        for (int i = 0, j = littleEndian ? 0 : str.Length - 1, inc = littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            int nibble = str[j] switch
            {
                '0' => 0x0,
                '1' => 0x1,
                '2' => 0x2,
                '3' => 0x3,
                '4' => 0x4,
                '5' => 0x5,
                '6' => 0x6,
                '7' => 0x7,
                '8' => 0x8,
                '9' => 0x9,
                'A' => 0xA,
                'B' => 0xB,
                'C' => 0xC,
                'D' => 0xD,
                'E' => 0xE,
                'F' => 0xF,
                _ => 0x10
            };

            if (nibble == 0x10) return false;

            value |= (byte)(nibble << i * 4);
        }
        return true;
    }

    public static bool TryParseHex(string str, bool littleEndian, out short value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('$')) str = str.Substring(1);
        else if (str.StartsWith("0X")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(short) * 2) return false;

        for (int i = 0, j = littleEndian ? 0 : str.Length - 1, inc = littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            int nibble = str[j] switch
            {
                '0' => 0x0,
                '1' => 0x1,
                '2' => 0x2,
                '3' => 0x3,
                '4' => 0x4,
                '5' => 0x5,
                '6' => 0x6,
                '7' => 0x7,
                '8' => 0x8,
                '9' => 0x9,
                'A' => 0xA,
                'B' => 0xB,
                'C' => 0xC,
                'D' => 0xD,
                'E' => 0xE,
                'F' => 0xF,
                _ => 0x10
            };

            if (nibble == 0x10) return false;

            value |= (short)(nibble << i * 4);
        }
        return true;
    }

    public static bool TryParseHex(string str, bool littleEndian, out int value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('$')) str = str.Substring(1);
        else if (str.StartsWith("0X")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(int) * 2) return false;

        for (int i = 0, j = littleEndian ? 0 : str.Length - 1, inc = littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            int nibble = str[j] switch
            {
                '0' => 0x0,
                '1' => 0x1,
                '2' => 0x2,
                '3' => 0x3,
                '4' => 0x4,
                '5' => 0x5,
                '6' => 0x6,
                '7' => 0x7,
                '8' => 0x8,
                '9' => 0x9,
                'A' => 0xA,
                'B' => 0xB,
                'C' => 0xC,
                'D' => 0xD,
                'E' => 0xE,
                'F' => 0xF,
                _ => 0x10
            };

            if (nibble == 0x10) return false;

            value |= nibble << i * 4;
        }
        return true;
    }

    public static bool TryParseHex(string str, bool littleEndian, out long value)
    {
        value = default;

        if (string.IsNullOrEmpty(str)) return false;

        str = str.ToUpper();

        if (str.StartsWith('$')) str = str.Substring(1);
        else if (str.StartsWith("0X")) str = str.Substring(2);

        if (string.IsNullOrEmpty(str) || str.Length > sizeof(long) * 2) return false;

        for (int i = 0, j = littleEndian ? 0 : str.Length - 1, inc = littleEndian ? 1 : -1; i < str.Length; i++, j += inc)
        {
            long nibble = str[j] switch
            {
                '0' => 0x0,
                '1' => 0x1,
                '2' => 0x2,
                '3' => 0x3,
                '4' => 0x4,
                '5' => 0x5,
                '6' => 0x6,
                '7' => 0x7,
                '8' => 0x8,
                '9' => 0x9,
                'A' => 0xA,
                'B' => 0xB,
                'C' => 0xC,
                'D' => 0xD,
                'E' => 0xE,
                'F' => 0xF,
                _ => 0x10
            };

            if (nibble == 0x10) return false;

            value |= nibble << i * 4;
        }
        return true;
    }

    public unsafe static bool TryParseHex(string str, bool littleEndian, out float value)
    {
        bool success = TryParseHex(str, littleEndian, out int i);
        value = *(float*)&i;
        return success;
    }

    public unsafe static bool TryParseHex(string str, bool littleEndian, out double value)
    {
        bool success = TryParseHex(str, littleEndian, out long l);
        value = *(double*)&l;
        return success;
    }
    #endregion
}