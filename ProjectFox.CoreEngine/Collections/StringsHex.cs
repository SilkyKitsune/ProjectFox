using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Collections;

public static partial class Strings
{
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
        (leadingText ? "0x" : "") + $"{NibbleHex(b & FourBits)}{NibbleHex(b >> 4 & FourBits)}" :
        (leadingText ? "0x" : "") + $"{NibbleHex(b >> 4 & FourBits)}{NibbleHex(b & FourBits)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(short s, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
             $"{NibbleHex(s & FourBits)}{NibbleHex(s >> 4 & FourBits)}" +
        $"{NibbleHex(s >> 8 & FourBits)}{NibbleHex(s >> 12 & FourBits)}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex(s >> 12 & FourBits)}{NibbleHex(s >> 8 & FourBits)}" +
        $"{NibbleHex(s >> 4 & FourBits)}{NibbleHex(s & FourBits)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(int i, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
              $"{NibbleHex(i & FourBits)}{NibbleHex(i >> 4 & FourBits)}" +
        $"{NibbleHex(i >> 8 & FourBits)}{NibbleHex(i >> 12 & FourBits)}" +
        $"{NibbleHex(i >> 16 & FourBits)}{NibbleHex(i >> 20 & FourBits)}" +
        $"{NibbleHex(i >> 24 & FourBits)}{NibbleHex(i >> 28 & FourBits)}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex(i >> 28 & FourBits)}{NibbleHex(i >> 24 & FourBits)}" +
        $"{NibbleHex(i >> 20 & FourBits)}{NibbleHex(i >> 16 & FourBits)}" +
        $"{NibbleHex(i >> 12 & FourBits)}{NibbleHex(i >> 8 & FourBits)}" +
        $"{NibbleHex(i >> 4 & FourBits)}{NibbleHex(i & FourBits)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(long l, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
              $"{NibbleHex((int)(l & FourBits))}{NibbleHex((int)(l >> 4 & FourBits))}" +
        $"{NibbleHex((int)(l >> 8 & FourBits))}{NibbleHex((int)(l >> 12 & FourBits))}" +
        $"{NibbleHex((int)(l >> 16 & FourBits))}{NibbleHex((int)(l >> 20 & FourBits))}" +
        $"{NibbleHex((int)(l >> 24 & FourBits))}{NibbleHex((int)(l >> 28 & FourBits))}" +
        $"{NibbleHex((int)(l >> 32 & FourBits))}{NibbleHex((int)(l >> 36 & FourBits))}" +
        $"{NibbleHex((int)(l >> 40 & FourBits))}{NibbleHex((int)(l >> 44 & FourBits))}" +
        $"{NibbleHex((int)(l >> 48 & FourBits))}{NibbleHex((int)(l >> 52 & FourBits))}" +
        $"{NibbleHex((int)(l >> 56 & FourBits))}{NibbleHex((int)(l >> 60 & FourBits))}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex((int)(l >> 60 & FourBits))}{NibbleHex((int)(l >> 56 & FourBits))}" +
        $"{NibbleHex((int)(l >> 52 & FourBits))}{NibbleHex((int)(l >> 48 & FourBits))}" +
        $"{NibbleHex((int)(l >> 44 & FourBits))}{NibbleHex((int)(l >> 40 & FourBits))}" +
        $"{NibbleHex((int)(l >> 36 & FourBits))}{NibbleHex((int)(l >> 32 & FourBits))}" +
        $"{NibbleHex((int)(l >> 28 & FourBits))}{NibbleHex((int)(l >> 24 & FourBits))}" +
        $"{NibbleHex((int)(l >> 20 & FourBits))}{NibbleHex((int)(l >> 16 & FourBits))}" +
        $"{NibbleHex((int)(l >> 12 & FourBits))}{NibbleHex((int)(l >> 8 & FourBits))}" +
        $"{NibbleHex((int)(l >> 4 & FourBits))}{NibbleHex((int)(l & FourBits))}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToHexString(float f, bool littleEndian = false, bool leadingText = false) => ToHexString(*(int*)&f, littleEndian, leadingText);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToHexString(double d, bool littleEndian = false, bool leadingText = false) => ToHexString(*(long*)&d, littleEndian, leadingText);

    #region Concat
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
}
