using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace ProjectFox.CoreEngine.Collections;

public static class Strings
{
    private const int FourBits = 0b1111;
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="substring"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static int[] GetAllSubstrings(string value, string substring)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring))
            throw new ArgumentNullException($"{nameof(value)}, {nameof(substring)}");

        if (!value.Contains(substring)) return null;

        int index = 0;
        int[] indices = new int[value.Length];

        for (int i = 0; i < value.Length; i++)
            if (value[i] == substring[0] &&
                GetSubstring(value, i, substring.Length).Equals(substring/*, StringComparison.*/))
                indices[index++] = i;

        return indices[0..(index - 1)];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <param name="length"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string GetSubstring(string value, int index, int length)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
        if (index < 0 || length <= 0)
            throw new ArgumentOutOfRangeException($"{nameof(index)}, {nameof(length)}");

        int farIndex = index + length;
        return farIndex >= value.Length ? value[index..^1] : value[index..farIndex];
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <param name="terminationChar"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetSubstringUntilChar(string value, int index, char terminationChar)
    {
        if (string.IsNullOrEmpty(value)) throw new ArgumentNullException(nameof(value));
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

        string s = "";
        while (index < value.Length && value[index] != terminationChar)
            s += value[index++];
        return s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <param name="terminationChars"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetSubstringUntilChar(string value, int index, string terminationChars)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(terminationChars))
            throw new ArgumentNullException($"{nameof(value)}, {nameof(terminationChars)}");
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

        string s = "";
        while (index < value.Length && !terminationChars.Contains(value[index]))
            s += value[index++];
        return s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <param name="index"></param>
    /// <param name="terminationString"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    public static string GetSubstringUntilString(string value, int index, string terminationString)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(terminationString))
            throw new ArgumentNullException($"{nameof(value)}, {nameof(terminationString)}");
        if (index < 0) throw new ArgumentOutOfRangeException(nameof(index));

        string s = "";
        while (index < value.Length)
        {
            if (value[index] == terminationString[0] &&
                GetSubstring(value, index, terminationString.Length).Equals(terminationString/*, StringComparison.OrdinalIgnoreCase*/))
                return s;
            s += value[index++];
        }
        return s;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <param name="substring"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool TryGetFirstSubstring(out int index, string value, string substring)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring))
            throw new ArgumentNullException($"{nameof(value)}, {nameof(substring)}");

        index = -1;
        if (!value.Contains(substring)) return false;

        for (int i = 0; i < value.Length; i++)
            if (value[i] == substring[0] &&
                GetSubstring(value, i, substring.Length).Equals(substring/*, StringComparison.*/))
            {
                index = i;
                return true;
            }
        return false;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="index"></param>
    /// <param name="value"></param>
    /// <param name="substring"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException"></exception>
    public static bool TryGetLastSubstring(out int index,string value,string substring)
    {
        if (string.IsNullOrEmpty(value) || string.IsNullOrEmpty(substring))
            throw new ArgumentNullException($"{nameof(value)}, {nameof(substring)}");

        index = -1;
        if (!value.Contains(substring)) return false;

        for (int i = value.Length - substring.Length; i > -1; i--)
            if (value[i] == substring[0] &&
                GetSubstring(value, i, substring.Length).Equals(substring/*, StringComparison.*/))
            {
                index = i;
                return true;
            }
        return false;
    }

    #region hex_bin
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
             $"{NibbleHex(s & FourBits)}{NibbleHex(s >>  4 & FourBits)}" +
        $"{NibbleHex(s >> 8 & FourBits)}{NibbleHex(s >> 12 & FourBits)}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex(s >> 12 & FourBits)}{NibbleHex(s >> 8 & FourBits)}" +
        $"{NibbleHex(s >>  4 & FourBits)}{NibbleHex(s & FourBits)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(int i, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
              $"{NibbleHex(i & FourBits)}{NibbleHex(i >>  4 & FourBits)}" +
        $"{NibbleHex(i >>  8 & FourBits)}{NibbleHex(i >> 12 & FourBits)}" +
        $"{NibbleHex(i >> 16 & FourBits)}{NibbleHex(i >> 20 & FourBits)}" +
        $"{NibbleHex(i >> 24 & FourBits)}{NibbleHex(i >> 28 & FourBits)}" :
        (leadingText ? "0x" : "") +
        $"{NibbleHex(i >> 28 & FourBits)}{NibbleHex(i >> 24 & FourBits)}" +
        $"{NibbleHex(i >> 20 & FourBits)}{NibbleHex(i >> 16 & FourBits)}" +
        $"{NibbleHex(i >> 12 & FourBits)}{NibbleHex(i >>  8 & FourBits)}" +
        $"{NibbleHex(i >>  4 & FourBits)}{NibbleHex(i & FourBits)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToHexString(long l, bool littleEndian = false, bool leadingText = false) => littleEndian ?
        (leadingText ? "0x" : "") +
              $"{NibbleHex((int)(l & FourBits))}{NibbleHex((int)(l >>  4 & FourBits))}" +
        $"{NibbleHex((int)(l >>  8 & FourBits))}{NibbleHex((int)(l >> 12 & FourBits))}" +
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
        $"{NibbleHex((int)(l >> 12 & FourBits))}{NibbleHex((int)(l >>  8 & FourBits))}" +
        $"{NibbleHex((int)(l >>  4 & FourBits))}{NibbleHex((int)(l & FourBits))}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToHexString(float f, bool littleEndian = false, bool leadingText = false) => ToHexString(*(int*)&f, littleEndian, leadingText);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToHexString(double d, bool littleEndian = false, bool leadingText = false) => ToHexString(*(long*)&d, littleEndian, leadingText);

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
             $"{NibbleBin(s & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s >>  4 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(s >> 8 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 12 & FourBits, littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin(s >> 12 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s >> 8 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(s >>  4 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(s & FourBits, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(int i, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
              $"{NibbleBin(i & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >>  4 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >>  8 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 12 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 16 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 20 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 24 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 28 & FourBits, littleEndian)}" :
        (leadingText ? "0b" : "") +
        $"{NibbleBin(i >> 28 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 24 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 20 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >> 16 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >> 12 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i >>  8 & FourBits, littleEndian)}{byteSeparator}" +
        $"{NibbleBin(i >>  4 & FourBits, littleEndian)}{nibbleSeparator}{NibbleBin(i & FourBits, littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static string ToBinString(long l, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') => littleEndian ?
        (leadingText ? "0b" : "") +
              $"{NibbleBin((int)(l & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >>  4 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >>  8 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >> 12 & FourBits), littleEndian)}{byteSeparator}" +
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
        $"{NibbleBin((int)(l >> 12 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l >>  8 & FourBits), littleEndian)}{byteSeparator}" +
        $"{NibbleBin((int)(l >>  4 & FourBits), littleEndian)}{nibbleSeparator}{NibbleBin((int)(l & FourBits), littleEndian)}";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToBinString(float f, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        ToBinString(*(int*)&f, littleEndian, leadingText, byteSeparator, nibbleSeparator);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public unsafe static string ToBinString(double d, bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        ToBinString(*(long*)&d, littleEndian, leadingText, byteSeparator, nibbleSeparator);

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
