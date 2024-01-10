using System.Runtime.CompilerServices;
using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct Color
{
    #region Operators
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(uint i) => new(i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(int i) => new((uint)i);

    #region Color
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator ++(Color c) => new((byte)(c.r + 1), (byte)(c.g + 1), (byte)(c.b + 1), c.a);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator --(Color c) => new((byte)(c.r - 1), (byte)(c.g - 1), (byte)(c.b - 1), c.a);

    /// <summary> Inverts the color </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator -(Color c) => new((byte)(byte.MaxValue - c.r), (byte)(byte.MaxValue - c.g), (byte)(byte.MaxValue - c.b)/*, c.a?*/);
    #endregion

    #region Color_Color
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Color c1, Color c2) => c1.EqualsColor(c2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Color c1, Color c2) => !c1.EqualsColor(c2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator +(Color c1, Color c2) => new((byte)(c1.r + c2.r), (byte)(c1.g + c2.g), (byte)(c1.b + c2.b));//max(c1.a, c2.a)?

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator -(Color c1, Color c2) => new((byte)(c1.r - c2.r), (byte)(c1.g - c2.g), (byte)(c1.b - c2.b));//min(c1.a, c2.a)?

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator *(Color c1, Color c2) => new((byte)(c1.r * c2.r), (byte)(c1.g * c2.g), (byte)(c1.b * c2.b));

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator /(Color c1, Color c2)
    {
        if (c2.r == 0 || c2.g == 0 || c2.b == 0) throw new DivideByZeroException();
        return new((byte)(c1.r / c2.r), (byte)(c1.g / c2.g), (byte)(c1.b / c2.b));
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator %(Color c1, Color c2)
    {
        if (c2.r == 0 || c2.g == 0 || c2.b == 0) throw new DivideByZeroException();
        return new((byte)(c1.r % c2.r), (byte)(c1.g % c2.g), (byte)(c1.b % c2.b));
    }

    //normal blend?
    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Color operator &(Color c1, Color c2) => new(, , , /*?*/);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Color operator |(Color c1, Color c2) => new(, , , /*?*/);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public static Color operator ^(Color c1, Color c2) => new(, , , /*?*/);
    #endregion
    //are byte operators redundant? int instead?
    #region Color_Byte
    //public static bool operator ==(Color c, byte b) => c.Equals(b);
    //public static bool operator !=(Color c, byte b) => !c.Equals(b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator +(Color c, byte b) => new((byte)(c.r + b), (byte)(c.g + b), (byte)(c.b + b)/*, ?*/);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator -(Color c, byte b) => new((byte)(c.r - b), (byte)(c.g - b), (byte)(c.b - b)/*, ?*/);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator *(Color c, byte b) => new((byte)(c.r * b), (byte)(c.g * b), (byte)(c.b * b)/*, ?*/);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator /(Color c, byte b)
    {
        if (b == 0) throw new DivideByZeroException();
        return new((byte)(c.r / b), (byte)(c.g / b), (byte)(c.b / b)/*, ?*/);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator %(Color c, byte b)
    {
        if (b == 0) throw new DivideByZeroException();
        return new((byte)(c.r % b), (byte)(c.g % b), (byte)(c.b % b)/*, ?*/);
    }
    #endregion
    #endregion
}
