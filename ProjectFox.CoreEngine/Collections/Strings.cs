using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Collections;

public static partial class Strings
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
}
