using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZF
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Abs() => new(Math.Abs(x), Math.Abs(y), Math.Abs(z));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(float min, float max) =>
        Math.Between(x, min, max) &&
        Math.Between(y, min, max) &&
        Math.Between(z, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(VectorZF min, VectorZF max) =>
        Math.Between(x, min.x, max.x) &&
        Math.Between(y, min.y, max.y) &&
        Math.Between(z, min.z, max.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(float min, float max) =>
        Math.BetweenAgainstBounds(x, min, max) &&
        Math.BetweenAgainstBounds(y, min, max) &&
        Math.BetweenAgainstBounds(z, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(VectorZF min, VectorZF max) =>
        Math.BetweenAgainstBounds(x, min.x, max.x) &&
        Math.BetweenAgainstBounds(y, min.y, max.y) &&
        Math.BetweenAgainstBounds(z, min.z, max.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(float min, float max) =>
        Math.BetweenAgainstMax(x, min, max) &&
        Math.BetweenAgainstMax(y, min, max) &&
        Math.BetweenAgainstMax(z, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(VectorZF min, VectorZF max) =>
        Math.BetweenAgainstMax(x, min.x, max.x) &&
        Math.BetweenAgainstMax(y, min.y, max.y) &&
        Math.BetweenAgainstMax(z, min.z, max.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(float min, float max) =>
        Math.BetweenAgainstMin(x, min, max) &&
        Math.BetweenAgainstMin(y, min, max) &&
        Math.BetweenAgainstMin(z, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(VectorZF min, VectorZF max) =>
        Math.BetweenAgainstMin(x, min.x, max.x) &&
        Math.BetweenAgainstMin(y, min.y, max.y) &&
        Math.BetweenAgainstMin(z, min.z, max.z);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Clamp(float min, float max) =>
        new(Math.Clamp(x, min, max), Math.Clamp(y, min, max), Math.Clamp(z, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Clamp(VectorZF min, VectorZF max) =>
        new(Math.Clamp(x, min.x, max.x), Math.Clamp(y, min.y, max.y), Math.Clamp(z, min.z, max.z));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Closest(VectorZF a, VectorZF b) => DistanceSquared(a) < DistanceSquared(b) ? a : b;

    public VectorZF Closest(params VectorZF[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        VectorZF closest = values[0];
        foreach (VectorZF v in values)
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

    public int ClosestIndex(VectorZF[] values)
    {
        if(values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            VectorZF current = values[i];
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
    public VectorZF Cube() => new(x * x * x, y * y * y, z * z * z);

    #region Equals
    /*public override bool Equals(object obj)
    {
        if (obj is VectorZF vzf) return Equals(vzf);
        if (obj is VectorZ vz) return Equals(vz);
        return false;
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorZF vzf) => x == vzf.x && y == vzf.y && z == vzf.z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorZ vz) => x == vz.x && y == vz.y && z == vz.z;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(float f) => x == f && y == f && z == f;
    #endregion

    public VectorZF Farthest(VectorZF a, VectorZF b) => DistanceSquared(a) > DistanceSquared(b) ? a : b;

    public VectorZF Farthest(params VectorZF[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = 0f, newDelta;

        VectorZF farthest = values[0];
        foreach (VectorZF v in values)
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

    public int FarthestIndex(VectorZF[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            VectorZF current = values[i];
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
        if (x < 0 && y < 0 && z < 0) return Math.Sign.Neg;
        if (x > 0 && y > 0 && z > 0) return Math.Sign.Pos;
        return Math.Sign.Zero;
    }

    public int FindSignInt()
    {
        if (x < 0 && y < 0 && z < 0) return -1;
        if (x > 0 && y > 0 && z > 0) return 1;
        return 0;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(VectorZF reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(VectorZF reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => x == 0f && y == 0f && z == 0f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Max(VectorZF value) => DistanceFromZeroSquared() > value.DistanceFromZeroSquared() ? this : value;

    public VectorZF Max(params VectorZF[] values)
    {
        if (values.Length == 0) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        VectorZF max = this;
        foreach (VectorZF v in values)
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

    public int MaxIndex(VectorZF[] values)
    {
        if (values.Length == 0) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int max = -1;//this will throw exception
        for (int i = 0; i < values.Length; i++)
        {
            VectorZF current = values[i];
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
    public VectorZF Min(VectorZF value) => DistanceFromZeroSquared() > value.DistanceFromZeroSquared() ? this : value;

    public VectorZF Min(params VectorZF[] values)
    {
        if (values.Length == 0 || IsZero()) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        VectorZF min = this;
        foreach (VectorZF v in values)
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

    public int MinIndex(VectorZF[] values)
    {
        if (values.Length == 0 || IsZero()) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int min = 1;
        for (int i = 0; i < values.Length; i++)
        {
            VectorZF current = values[i];
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

        if (x < 0f) x += Math.Clamp(x, 1f, -x);
        else if (x > 0f) x -= Math.Clamp(x, 1f, x);

        if (y < 0f) y += Math.Clamp(y, 1f, -y);
        else if (y > 0f) y -= Math.Clamp(y, 1f, y);

        if (z < 0f) z += Math.Clamp(z, 1f, -z);
        else if (z > 0f) z -= Math.Clamp(z, 1f, z);
    }

    public void MoveToZero(VectorZF amount)
    {
        if (IsZero()) return;

        if (amount.x > 0)
        {
            if (x < 0f) x += Math.Clamp(amount.x, 1f, -x);
            else if (x > 0f) x -= Math.Clamp(amount.x, 1f, x);
        }

        if (amount.y > 0)
        {
            if (y < 0f) y += Math.Clamp(amount.y, 1f, -y);
            else if (y > 0f) y -= Math.Clamp(amount.y, 1f, y);
        }

        if (amount.z > 0)
        {
            if (z < 0f) z += Math.Clamp(amount.z, 1f, -z);
            else if (z > 0f) z -= Math.Clamp(amount.z, 1f, z);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Pow(int exp) =>
        new(Math.Pow(x, exp), Math.Pow(y, exp), Math.Pow(z, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Pow(VectorZF exp) =>
        new(Math.Pow(x, (int)exp.x), Math.Pow(y, (int)exp.y), Math.Pow(z, (int)exp.z));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Sqr() => new(x * x, y * y, z * z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF SqrRoot() => new(Math.SqrRoot(x), Math.SqrRoot(y), Math.SqrRoot(z));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Wrap(float min, float max) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF Wrap(VectorZF min, VectorZF max) => default;
}
