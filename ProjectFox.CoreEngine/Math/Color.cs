using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public partial struct Color : IVector<Color, byte, Color>
{
    private const float MaxByteF = byte.MaxValue;

    /// <summary> The silliest limegirl </summary>
    public static Color Lime => 0x80FF40FF;

    public Color(byte r, byte g, byte b)
    {
        hex = 0;
        this.r = r;
        this.g = g;
        this.b = b;
        a = byte.MaxValue;
    }
    public Color(byte r, byte g, byte b, byte a)
    {
        hex = 0;
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }
    private Color(uint hex)
    {
        r = 0;
        g = 0;
        b = 0;
        a = 0;
        this.hex = hex;
    }

    /// <summary> Red channel </summary>
    [FieldOffset(3)] public byte r;
    /// <summary> Green channel </summary>
    [FieldOffset(2)] public byte g;
    /// <summary> Blue channel </summary>
    [FieldOffset(1)] public byte b;
    /// <summary> Alpha channel </summary>
    [FieldOffset(0)] public byte a;
    /// <summary> Integer value of entire struct </summary>
    [FieldOffset(0)] public uint hex;//reword

    #region Float
    public float R
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => r / MaxByteF;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => r = (byte)(value * MaxByteF);
    }

    public float G
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => g / MaxByteF;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => g = (byte)(value * MaxByteF);
    }

    public float B
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => b / MaxByteF;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => b = (byte)(value * MaxByteF);
    }

    public float A
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => a / MaxByteF;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set => a = (byte)(value * MaxByteF);
    }

    public string FloatString() => $"(R: {R}, G: {G}, B: {B}, A: {A})";
    #endregion

    #region HSL
    //FromHSL()

    //Lightness
    //SaturationHSL
    //GetHSL()
    //HSLString()
    #endregion

    //lch

    //yuv

    //yiq

    #region YPbPr
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => (int)hex;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(R: {r}, G: {g}, B: {b}, A: {a})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(R: {Strings.ToHexString(r, littleEndian, leadingText)}, G: {Strings.ToHexString(g, littleEndian, leadingText)}, " +
        $"B: {Strings.ToHexString(b, littleEndian, leadingText)}, A: {Strings.ToHexString(a, littleEndian, leadingText)}, " +
        $"Hex: {Strings.ToHexString((int)hex, littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, char byteSeparator = '|', char nibbleSeparator = '_', bool leadingText = false) =>
        $"(R: {Strings.ToBinString(r, littleEndian, leadingText, nibbleSeparator)}, G: {Strings.ToBinString(g, littleEndian, leadingText, nibbleSeparator)}, " +
        $"B: {Strings.ToBinString(b, littleEndian, leadingText, nibbleSeparator)}, A: {Strings.ToBinString(a, littleEndian, leadingText, nibbleSeparator)}, " +
        $"Hex: {Strings.ToBinString((int)hex, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Vector Methods
    public float Distance(Color value)
    {
        if (EqualsColor(value)) return 0f;

        int rDelta = r - value.r, gDelta = g - value.g, bDelta = b - value.b;

        if (rDelta < 0) rDelta = -rDelta;
        if (gDelta < 0) gDelta = -gDelta;
        if (bDelta < 0) bDelta = -bDelta;

        return Math.SqrRoot((rDelta * rDelta) + (gDelta * gDelta) + (bDelta * bDelta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceFromZero() => IsBlack() ? 0f : Math.SqrRoot((r * r) + (g * g) + (b * b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceFromZeroSquared() => IsBlack() ? 0f : (r * r) + (g * g) + (b * b);

    public float DistanceSquared(Color value)
    {
        if (EqualsColor(value)) return 0f;

        int rDelta = r - value.r, gDelta = g - value.g, bDelta = b - value.b;

        if (rDelta < 0) rDelta = -rDelta;
        if (gDelta < 0) gDelta = -gDelta;
        if (bDelta < 0) bDelta = -bDelta;

        return (rDelta * rDelta) + (gDelta * gDelta) + (bDelta * bDelta);
    }
    #endregion

    #region Color Methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EqualsColor(Color c) => r == c.r && g == c.g && b == c.b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EqualsColor(byte value) => r == value && g == value && b == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsBlack() => r == 0 && g == 0 && b == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsGrey() => r == g && r == b;

    public void MoveToBlack(byte amount)
    {
        if (amount <= 0 || IsZero()) return;

        byte one = 1;
        if (r > 0) r -= Math.Clamp(amount, one, r);
        if (g > 0) g -= Math.Clamp(amount, one, g);
        if (b > 0) b -= Math.Clamp(amount, one, b);
    }

    public void MoveToBlack(Color amount)
    {
        if (amount.IsZero() || IsZero()) return;

        byte one = 1;
        if (r > 0) r -= Math.Clamp(amount.r, one, r);
        if (g > 0) g -= Math.Clamp(amount.g, one, g);
        if (b > 0) b -= Math.Clamp(amount.b, one, b);
    }
    #endregion

    #region Blending
    /// <summary></summary>
    /// <param name="c"></param>
    /// <param name="topColor"> fill this out </param>
    /// <returns></returns>
    public Color Blend(Color c, bool topColor = false)
    {
        Color top = topColor ? this : c, bottom = topColor ? c : this;

        if (top.a == byte.MaxValue || bottom.a == byte.MinValue) return top;

        if (top.a == byte.MinValue) return bottom;
        //this doesn't work right if both colors have a < max
        float a = top.A;
        return new(
            (byte)((top.r - bottom.r) * a + bottom.r),
            (byte)((top.g - bottom.g) * a + bottom.g),
            (byte)((top.b - bottom.b) * a + bottom.b),
            bottom.a == byte.MaxValue ? bottom.a :
            (byte)((int)((1f - a) * top.a) + bottom.a));
    }

    //blendto?
    //blendover?
    #endregion//move this up?

    #region Operators
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(uint i) => new(i);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Color(int i) => new((uint)i);

    #region Color
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Color operator ++(Color c) => new((byte)(c.r + 1), (byte)(c.g + 1), (byte)(c.b + 1), c.a);

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
