using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using D = ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Explicit, Size = 4)]
public partial struct Color : IVector<Color, byte, Color>
{
    private const float MaxByteF = byte.MaxValue;

    /// <summary> The silliest limegirl </summary>
    public static Color Lime => 0x80FF40FF;

    public static Color Convert(int value, int channelDepth, bool alpha)
    {
        if (channelDepth < 1) throw new ArgumentException($"Invalid channelDepth! '{channelDepth}'");

        if (channelDepth == 8) return alpha ? new((uint)value) : new((uint)value << 8 | byte.MaxValue);

        int channelCount = alpha ? 4 : 3, depth = sizeof(int) * 8;
        if (depth / channelDepth < channelCount) throw new ArgumentException($"Value is too small! {depth}bits < {channelDepth * channelCount}bits");

        int max = 0;
        for (int i = 0; i < channelDepth; i++) max = max << 1 | 1;
        float maxF = max;

        return alpha ?
          new(
            (byte)((value >> (channelDepth * 3) & max) / maxF * MaxByteF),
            (byte)((value >> (channelDepth * 2) & max) / maxF * MaxByteF),
            (byte)((value >> channelDepth & max) / maxF * MaxByteF),
            (byte)((value & max) / maxF * MaxByteF)) :
          new(
            (byte)((value >> (channelDepth * 2) & max) / maxF * MaxByteF),
            (byte)((value >> channelDepth & max) / maxF * MaxByteF),
            (byte)((value & max) / maxF * MaxByteF));
    }

    public static Color[] Convert(int[] values, int channelDepth, bool alpha)
    {
        Color[] colors = new Color[values.Length];
        for (int i = 0; i < values.Length; i++)
            colors[i] = Color.Convert(values[i], channelDepth, alpha);//inline?
        return colors;
    }

    public static int[] Convert(Color[] values, int channelDepth, bool alpha)
    {
        int[] colors = new int[values.Length];
        for (int i = 0; i < values.Length; i++)
            colors[i] = values[i].Convert(channelDepth, alpha);//inline?
        return colors;
    }

    public Color(byte r, byte g, byte b, byte a = byte.MaxValue)
    {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
    }

    private Color(uint hex) => this.hex = hex;

    //are these backwards? do they change with endianess?
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
        $"(R: {D.ToHexString(r, littleEndian, leadingText)}, G: {D.ToHexString(g, littleEndian, leadingText)}, " +
        $"B: {D.ToHexString(b, littleEndian, leadingText)}, A: {D.ToHexString(a, littleEndian, leadingText)}, " +
        $"Hex: {D.ToHexString((int)hex, littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(R: {D.ToBinString(r, littleEndian, leadingText, nibbleSeparator)}, G: {D.ToBinString(g, littleEndian, leadingText, nibbleSeparator)}, " +
        $"B: {D.ToBinString(b, littleEndian, leadingText, nibbleSeparator)}, A: {D.ToBinString(a, littleEndian, leadingText, nibbleSeparator)}, " +
        $"Hex: {D.ToBinString((int)hex, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Vector Methods
    public float Distance(Color value)
    {
        if (Equals(value)) return 0f;

        int rDelta = r - value.r, gDelta = g - value.g, bDelta = b - value.b, aDelta = a - value.a;

        if (rDelta < 0) rDelta = -rDelta;
        if (gDelta < 0) gDelta = -gDelta;
        if (bDelta < 0) bDelta = -bDelta;
        if (aDelta < 0) aDelta = -aDelta;

        return Math.SqrRoot((rDelta * rDelta) + (gDelta * gDelta) + (bDelta * bDelta) + (aDelta * aDelta));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceFromZero() => IsZero() ? 0f : Math.SqrRoot((r * r) + (g * g) + (b * b) + (a * a));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceFromZeroSquared() => IsZero() ? 0f : (r * r) + (g * g) + (b * b) + (a * a);

    public float DistanceSquared(Color value)
    {
        if (Equals(value)) return 0f;

        int rDelta = r - value.r, gDelta = g - value.g, bDelta = b - value.b, aDelta = a - value.a;

        if (rDelta < 0) rDelta = -rDelta;
        if (gDelta < 0) gDelta = -gDelta;
        if (bDelta < 0) bDelta = -bDelta;
        if (aDelta < 0) aDelta = -aDelta;

        return (rDelta * rDelta) + (gDelta * gDelta) + (bDelta * bDelta) + (aDelta * aDelta);
    }
    #endregion

    #region Color Methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color ClosestColor(Color a, Color b) => DistanceColorSquared(a) < DistanceColorSquared(b) ? a : b;
    
    public Color ClosestColor(params Color[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        Color closest = values[0];
        foreach (Color c in values)
        {
            if (EqualsColor(c)) return c;
            if (!closest.EqualsColor(c))
            {
                newDelta = DistanceColorSquared(c);
                if (newDelta < delta)
                {
                    closest = c;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    public int ClosestColorIndex(Color[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            Color current = values[i];
            if (EqualsColor(current)) return i;
            if (!values[closest].EqualsColor(current))
            {
                newDelta = DistanceColorSquared(current);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    public int Convert(int channelDepth, bool alpha)
    {
        if (channelDepth < 1) throw new ArgumentException($"Invalid channelDepth! '{channelDepth}'");

        if (channelDepth == 8) return alpha ? (int)hex : (int)hex >> 8;

        int channelCount = alpha ? 4 : 3, depth = sizeof(int) * 8;
        if (depth / channelDepth < channelCount) throw new ArgumentException($"Value is too small! {depth}bits < {channelDepth * channelCount}bits");

        int max = 0;
        for (int i = 0; i < channelDepth; i++) max = max << 1 | 1;
        float maxF = max;

        return alpha ?
            (int)(r / MaxByteF * maxF) << (channelDepth * 3) |
            (int)(g / MaxByteF * maxF) << (channelDepth * 2) |
            (int)(b / MaxByteF * maxF) << channelDepth |
            (int)(a / MaxByteF * maxF)
            :
            (int)(r / MaxByteF * maxF) << (channelDepth * 2) |
            (int)(g / MaxByteF * maxF) << channelDepth |
            (int)(b / MaxByteF * maxF);
    }

    public float DistanceColor(Color value)
    {
        if (EqualsColor(value)) return 0f;

        int rDelta = r - value.r, gDelta = g - value.g, bDelta = b - value.b;

        if (rDelta < 0) rDelta = -rDelta;
        if (gDelta < 0) gDelta = -gDelta;
        if (bDelta < 0) bDelta = -bDelta;

        return Math.SqrRoot((rDelta * rDelta) + (gDelta * gDelta) + (bDelta * bDelta));
    }

    public float DistanceColorSquared(Color value)
    {
        if (EqualsColor(value)) return 0f;

        int rDelta = r - value.r, gDelta = g - value.g, bDelta = b - value.b;

        if (rDelta < 0) rDelta = -rDelta;
        if (gDelta < 0) gDelta = -gDelta;
        if (bDelta < 0) bDelta = -bDelta;

        return (rDelta * rDelta) + (gDelta * gDelta) + (bDelta * bDelta);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceFromBlack() => IsBlack() ? 0f : Math.SqrRoot((r * r) + (g * g) + (b * b));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float DistanceFromBlackSquared() => IsBlack() ? 0f : (r * r) + (g * g) + (b * b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EqualsColor(Color c) => r == c.r && g == c.g && b == c.b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool EqualsColor(byte value) => r == value && g == value && b == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color FarthestColor(Color a, Color b) => DistanceColorSquared(a) > DistanceColorSquared(b) ? a : b;

    public Color FarthestColor(params Color[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        Color farthest = values[0];
        foreach (Color c in values)
            if (!farthest.EqualsColor(c))
            {
                newDelta = DistanceColorSquared(c);
                if (newDelta > delta)
                {
                    farthest = c;
                    delta = newDelta;
                }
            }
        return farthest;
    }

    public int FarthestColorIndex(Color[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            Color current = values[i];
            if (!values[farthest].EqualsColor(current))
            {
                newDelta = DistanceColorSquared(current);
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }

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
}
