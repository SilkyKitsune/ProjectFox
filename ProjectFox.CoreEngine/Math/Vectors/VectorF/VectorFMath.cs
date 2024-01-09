using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorF
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Abs() => new(Math.Abs(x), Math.Abs(y));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(float min, float max) =>
        Math.Between(x, min, max) &&
        Math.Between(y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(VectorF min, VectorF max) =>
        Math.Between(x, min.x, max.x) &&
        Math.Between(y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(float min, float max) =>
        Math.BetweenAgainstBounds(x, min, max) &&
        Math.BetweenAgainstBounds(y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(VectorF min, VectorF max) =>
        Math.BetweenAgainstBounds(x, min.x, max.x) &&
        Math.BetweenAgainstBounds(y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(float min, float max) =>
        Math.BetweenAgainstMax(x, min, max) &&
        Math.BetweenAgainstMax(y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(VectorF min, VectorF max) =>
        Math.BetweenAgainstMax(x, min.x, max.x) &&
        Math.BetweenAgainstMax(y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(float min, float max) =>
        Math.BetweenAgainstMin(x, min, max) &&
        Math.BetweenAgainstMin(y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(VectorF min, VectorF max) =>
        Math.BetweenAgainstMin(x, min.x, max.x) &&
        Math.BetweenAgainstMin(y, min.y, max.y);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Clamp(float min, float max) =>
        new(Math.Clamp(x, min, max), Math.Clamp(y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Clamp(VectorF min, VectorF max) =>
        new(Math.Clamp(x, min.x, max.x), Math.Clamp(y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Closest(VectorF a, VectorF b) => DistanceSquared(a) < DistanceSquared(b) ? a : b;

    public VectorF Closest(params VectorF[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        VectorF closest = values[0];
        foreach (VectorF v in values)
        {
            if (Equals(v)) return v;
            if (!closest.Equals(v))
            {
                newDelta = DistanceSquared(v);
                if (newDelta < delta)
                {
                    closest = v;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    public int ClosestIndex(VectorF[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            VectorF current = values[i];
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
    public VectorF Cube() => new(x * x * x, y * y * y);

    #region Equals
    /*public override bool Equals(object obj)
    {
        if (obj is VectorF vf) return Equals(vf);
        if (obj is Vector v) return Equals(v);
        return false;
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorF vf) => x == vf.x && y == vf.y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector v) => x == v.x && y == v.y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(float f) => x == f && y == f;
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Farthest(VectorF a, VectorF b) => DistanceSquared(a) > DistanceSquared(b) ? a : b;

    public VectorF Farthest(params VectorF[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = 0f, newDelta;

        VectorF farthest = values[0];
        foreach (VectorF v in values)
            if (!farthest.Equals(v))
            {
                newDelta = DistanceSquared(v);
                if (newDelta > delta)
                {
                    farthest = v;
                    delta = newDelta;
                }
            }
        return farthest;
    }

    public int FarthestIndex(VectorF[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            VectorF current = values[i];
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

    public Math.Sign FindSign()
    {
        if (x < 0 && y < 0) return Math.Sign.Neg;
        if (x > 0 && y > 0) return Math.Sign.Pos;
        return Math.Sign.Zero;
    }

    public int FindSignInt()
    {
        if (x < 0 && y < 0) return -1;
        if (x > 0 && y > 0) return 1;
        return 0;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(VectorF reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(VectorF reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => x == 0f && y == 0f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Max(VectorF value) => DistanceFromZeroSquared() > value.DistanceFromZeroSquared() ? this : value;

    public VectorF Max(params VectorF[] values)
    {
        if (values.Length == 0) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        VectorF max = this;
        foreach (VectorF v in values)
            if (!max.Equals(v))
            {
                newDelta = v.DistanceFromZeroSquared();
                if (newDelta > delta)
                {
                    max = v;
                    delta = newDelta;
                }
            }
        return max;
    }

    public int MaxIndex(VectorF[] values)
    {
        if (values.Length == 0) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int max = -1;
        for (int i = 0; i < values.Length; i++)
        {
            VectorF current = values[i];
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
    public VectorF Min(VectorF value) => DistanceFromZeroSquared() < value.DistanceFromZeroSquared() ? this : value;

    public VectorF Min(params VectorF[] values)
    {
        if (values.Length == 0 || IsZero()) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        VectorF min = this;
        foreach (VectorF v in values)
        {
            if (v.IsZero()) return v;
            if (!min.Equals(v))
            {
                newDelta = v.DistanceFromZeroSquared();
                if (newDelta < delta)
                {
                    min = v;
                    delta = newDelta;
                }
            }
        }
        return min;
    }

    public int MinIndex(VectorF[] values)
    {
        if (values.Length == 0 || IsZero()) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int min = 1;
        for (int i = 0; i < values.Length; i++)
        {
            VectorF current = values[i];
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

    public void MoveToZero(float amount)
    {
        if (amount <= 0f || IsZero()) return;

        if (x < 0f) x += Math.Clamp(amount, 1f, -x);
        else if (x > 0f) x -= Math.Clamp(amount, 1f, x);

        if (y < 0f) y += Math.Clamp(amount, 1f, -y);
        else if (y > 0f) y -= Math.Clamp(amount, 1f, y);
    }

    public void MoveToZero(VectorF amount)
    {
        if (IsZero()) return;

        if (amount.x > 0f)
        {
            if (x < 0f) x += Math.Clamp(amount.x, 1f, -x);
            else if (x > 0f) x -= Math.Clamp(amount.x, 1f, x);
        }

        if (amount.y > 0f)
        {
            if (y < 0f) y += Math.Clamp(amount.y, 1f, -y);
            else if (y > 0f) y -= Math.Clamp(amount.y, 1f, y);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Pow(int exp) =>
        new(Math.Pow(x, exp), Math.Pow(y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Pow(VectorF exp) =>
        new(Math.Pow(x, (int)exp.x), Math.Pow(y, (int)exp.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Sqr() => new(x * x, y * y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF SqrRoot() => new(Math.SqrRoot(x), Math.SqrRoot(y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Wrap(float min, float max) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Wrap(VectorF min, VectorF max) => default;
}
