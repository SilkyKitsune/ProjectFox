using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Color
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Abs() => this;

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(byte min, byte max) =>
        Math.Between(r, min, max) &&
        Math.Between(g, min, max) &&
        Math.Between(b, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(Color min, Color max) =>
        Math.Between(r, min.r, max.r) &&
        Math.Between(g, min.g, max.g) &&
        Math.Between(b, min.b, max.b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(byte min, byte max) =>
        Math.BetweenAgainstBounds(r, min, max) &&
        Math.BetweenAgainstBounds(g, min, max) &&
        Math.BetweenAgainstBounds(b, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(Color min, Color max) =>
        Math.BetweenAgainstBounds(r, min.r, max.r) &&
        Math.BetweenAgainstBounds(g, min.g, max.g) &&
        Math.BetweenAgainstBounds(b, min.b, max.b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(byte min, byte max) =>
        Math.BetweenAgainstMax(r, min, max) &&
        Math.BetweenAgainstMax(g, min, max) &&
        Math.BetweenAgainstMax(b, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(Color min, Color max) =>
        Math.BetweenAgainstMax(r, min.r, max.r) &&
        Math.BetweenAgainstMax(g, min.g, max.g) &&
        Math.BetweenAgainstMax(b, min.b, max.b);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(byte min, byte max) =>
        Math.BetweenAgainstMin(r, min, max) &&
        Math.BetweenAgainstMin(g, min, max) &&
        Math.BetweenAgainstMin(b, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(Color min, Color max) =>
        Math.BetweenAgainstMin(r, min.r, max.r) &&
        Math.BetweenAgainstMin(g, min.g, max.g) &&
        Math.BetweenAgainstMin(b, min.b, max.b);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Clamp(byte min, byte max) =>
        new(Math.Clamp(r, min, max), Math.Clamp(g, min, max), Math.Clamp(b, min, max), a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Clamp(Color min, Color max) =>
        new(Math.Clamp(r, min.r, max.r), Math.Clamp(g, min.g, max.g), Math.Clamp(b, min.b, max.b), a);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Closest(Color a, Color b) => DistanceSquared(a) < DistanceSquared(b) ? a : b;

    public Color Closest(params Color[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        Color closest = values[0];
        foreach (Color c in values)
        {
            if (Equals(c)) return c;
            if (!closest.Equals(c))
            {
                newDelta = DistanceSquared(c);
                if (newDelta < delta)
                {
                    closest = c;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    public int ClosestIndex(Color[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            Color current = values[i];
            if (Equals(current)) return i;
            if (!values[closest].Equals(current))
            {
                newDelta = DistanceSquared(current);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Cube() => new((byte)(r * r * r), (byte)(g * g * g), (byte)(b * b * b), a);

    #region Equals
    public override bool Equals(object obj)
    {
        if (obj is Color c) return Equals(c);
        if (obj is uint u) return Equals(u);
        if (obj is int i) return Equals(i);
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Color c) => hex == c.hex;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(uint i) => hex == i;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(int i) => hex == i;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(byte value) => r == value && g == value && b == value && a == value;
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Farthest(Color a, Color b) => DistanceSquared(a) > DistanceSquared(b) ? a : b;

    public Color Farthest(params Color[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        Color farthest = values[0];
        foreach (Color c in values)
            if (!farthest.Equals(c))
            {
                newDelta = DistanceSquared(c);
                if (newDelta > delta)
                {
                    farthest = c;
                    delta = newDelta;
                }
            }
        return farthest;
    }

    public int FarthestIndex(Color[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            Color current = values[i];
            if (!values[farthest].Equals(current))
            {
                newDelta = DistanceSquared(current);
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(Color reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(Color reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => hex == 0u;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Max(Color value) => DistanceFromZeroSquared() > value.DistanceFromZeroSquared() ? this : value;

    public Color Max(params Color[] values)
    {
        if (values.Length == 0) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        Color max = this;
        foreach (Color c in values)
            if (!max.EqualsColor(c))
            {
                newDelta = c.DistanceFromZeroSquared();
                if (newDelta > delta)
                {
                    max = c;
                    delta = newDelta;
                }
            }
        return max;
    }

    public int MaxIndex(Color[] values)
    {
        if (values.Length == 0) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int max = -1;
        for (int i = 0; i < values.Length; i++)
        {
            Color current = values[i];
            if (!values[max].Equals(current))
            {
                newDelta = current.DistanceFromZeroSquared();
                if (newDelta > delta)
                {
                    max = i;
                    delta = newDelta;
                }
            }
        }
        return max;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Min(Color value) => DistanceFromZeroSquared() < value.DistanceFromZeroSquared() ? this : value;

    public Color Min(params Color[] values)
    {
        if (values.Length == 0 || IsBlack()) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        Color min = this;
        foreach (Color c in values)
        {
            if (c.IsBlack()) return c;
            if (!min.EqualsColor(c))
            {
                newDelta = c.DistanceFromZeroSquared();
                if (newDelta < delta)
                {
                    min = c;
                    delta = newDelta;
                }
            }
        }
        return min;
    }

    public int MinIndex(Color[] values)
    {
        if (values.Length == 0 || IsZero()) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int min = 1;
        for (int i = 0; i < values.Length; i++)
        {
            Color current = values[i];
            if (current.IsZero()) return i;
            if (!values[min].Equals(current))
            {
                newDelta = current.DistanceFromZeroSquared();
                if (newDelta < delta)
                {
                    min = i;
                    delta = newDelta;
                }
            }
        }
        return min;
    }

    public void MoveToZero(byte amount)
    {
        if (amount <= 0 || IsZero()) return;

        byte one = 1;
        if (r > 0) r -= Math.Clamp(amount, one, r);
        if (g > 0) g -= Math.Clamp(amount, one, g);
        if (b > 0) b -= Math.Clamp(amount, one, b);
        if (a > 0) a -= Math.Clamp(amount, one, a);
    }

    public void MoveToZero(Color amount)
    {
        if (amount.IsZero() || IsZero()) return;

        byte one = 1;
        if (r > 0) r -= Math.Clamp(amount.r, one, r);
        if (g > 0) g -= Math.Clamp(amount.g, one, g);
        if (b > 0) b -= Math.Clamp(amount.b, one, b);
        if (a > 0) a -= Math.Clamp(amount.a, one, a);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Pow(int exp) =>
        new((byte)Math.Pow(r, exp), (byte)Math.Pow(g, exp), (byte)Math.Pow(b, exp), a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Pow(Color exp) =>
        new((byte)Math.Pow(r, exp.r), (byte)Math.Pow(g, exp.g), (byte)Math.Pow(b, exp.b), a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Sqr() => new((byte)(r * r), (byte)(g * g), (byte)(b * b), a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color SqrRoot() => new((byte)Math.SqrRoot(r), (byte)Math.SqrRoot(g), (byte)Math.SqrRoot(b), a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Wrap(byte min, byte max) =>
        new((byte)Math.Wrap(r, min, max), (byte)Math.Wrap(g, min, max), (byte)Math.Wrap(b, min, max), a);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color Wrap(Color min, Color max) =>
        new((byte)Math.Wrap(r, min.r, max.r), (byte)Math.Wrap(g, min.g, max.g), (byte)Math.Wrap(b, min.b, max.b), a);
}