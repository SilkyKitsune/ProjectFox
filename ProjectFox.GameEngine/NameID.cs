using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
#if DEBUG
using ProjectFox.CoreEngine.Data;
#endif

namespace ProjectFox.GameEngine;

/// <summary> a 64-bit string like type used for IDs </summary>
[StructLayout(LayoutKind.Explicit, Size = 8)]
public partial struct NameID
{
    /// <summary> create an ID from an integer value </summary>
    /// <param name="value"> the ID's 64-bit integer value </param>
    public NameID(ulong value) => l = value;

    /// <summary> create an ID from a string and number value </summary>
    /// <param name="chars"> the 7 char length string </param>
    /// <param name="number"> the 8-bit unsigned integer </param>
    public NameID(string chars, byte number)
    {
        if (chars == null) chars = string.Empty;

        c0 = 0;
        c1 = 0;
        c2 = 0;
        c3 = 0;
        c4 = 0;
        c5 = 0;
        c6 = 0;
        num = number;

        switch (chars.Length)
        {
            case 7:
                c6 = (sbyte)(chars[6] & 0x7F);
                goto case 6;
            case 6:
                c5 = (sbyte)(chars[5] & 0x7F);
                goto case 5;
            case 5:
                c4 = (sbyte)(chars[4] & 0x7F);
                goto case 4;
            case 4:
                c3 = (sbyte)(chars[3] & 0x7F);
                goto case 3;
            case 3:
                c2 = (sbyte)(chars[2] & 0x7F);
                goto case 2;
            case 2:
                c1 = (sbyte)(chars[1] & 0x7F);
                goto case 1;
            case 1:
                c0 = (sbyte)(chars[0] & 0x7F);
                break;
        }
    }

    [FieldOffset(0)] private readonly ulong l;
    [FieldOffset(7)] private readonly sbyte c0;
    [FieldOffset(6)] private readonly sbyte c1;
    [FieldOffset(5)] private readonly sbyte c2;
    [FieldOffset(4)] private readonly sbyte c3;
    [FieldOffset(3)] private readonly sbyte c4;
    [FieldOffset(2)] private readonly sbyte c5;
    [FieldOffset(1)] private readonly sbyte c6;
    [FieldOffset(0)] private readonly byte num;

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => l.GetHashCode();

    ///
    public override bool Equals(object obj)
    {
        if (obj is NameID name) return Equals(name);
        return false;
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(NameID name) => l == name.l;

#if DEBUG
    /// <returns> $"{chars}_{number} : {integer value}" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"{(char)c0}{(char)c1}{(char)c2}{(char)c3}{(char)c4}{(char)c5}{(char)c6}_{num} : {Data.ToHexString((long)l)}";
#endif

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator ulong(NameID nameID) => nameID.l;
}