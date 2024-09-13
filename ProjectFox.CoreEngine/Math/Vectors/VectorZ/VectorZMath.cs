using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZ
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ Max(VectorZ a, VectorZ b) => a.DistanceFromZero() > b.DistanceFromZero() ? a : b;

    public static VectorZ Max(params VectorZ[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        VectorZ max = values[0];
        float delta = max.DistanceFromZero(), newDelta;

        foreach (VectorZ v in values)
            if (!max.Equals(v))
            {
                newDelta = v.DistanceFromZero();
                if (newDelta > delta)
                {
                    max = v;
                    delta = newDelta;
                }
            }
        return max;
    }

    public static int MaxIndex(VectorZ[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        float delta = values[max].DistanceFromZeroSquared(), newDelta;

        for (int i = 1; i < values.Length; i++)
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
    public static VectorZ Min(VectorZ a, VectorZ b) => a.DistanceFromZero() < b.DistanceFromZero() ? a : b;

    public static VectorZ Min(params VectorZ[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        VectorZ min = values[0];
        float delta = min.DistanceFromZero(), newDelta;

        foreach (VectorZ v in values)
        {
            if (v.IsZero()) return v;
            if (!min.Equals(v))
            {
                newDelta = v.DistanceFromZero();
                if (newDelta < delta)
                {
                    min = v;
                    delta = newDelta;
                }
            }
        }
        return min;
    }

    public static int MinIndex(VectorZ[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        float delta = values[min].DistanceFromZeroSquared(), newDelta;

        for (int i = 1; i < values.Length; i++)
        {
            VectorZ current = values[i];
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

    /// <returns> new vector of ( | x | , | y | , | z | )  </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Abs() => new(Math.Abs(x), Math.Abs(y), Math.Abs(z));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(int min, int max) =>
        Math.Between(x, min, max) &&
        Math.Between(y, min, max) &&
        Math.Between(z, min, max);

    /// <returns> true if (min.x &lt; x &lt; max.x), (min.y &lt; y &lt; max.y) and (min.z &lt; z &lt; max.z) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(VectorZ min, VectorZ max) =>
        Math.Between(x, min.x, max.x) &&
        Math.Between(y, min.y, max.y) &&
        Math.Between(z, min.z, max.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(int min, int max) =>
        Math.BetweenAgainstBounds(x, min, max) &&
        Math.BetweenAgainstBounds(y, min, max) &&
        Math.BetweenAgainstBounds(z, min, max);

    /// <returns> true if (min.x ≤ x ≤ max.x), (min.y ≤ y ≤ max.y) and (min.z ≤ z ≤ max.z) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(VectorZ min, VectorZ max) =>
        Math.BetweenAgainstBounds(x, min.x, max.x) &&
        Math.BetweenAgainstBounds(y, min.y, max.y) &&
        Math.BetweenAgainstBounds(z, min.z, max.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(int min, int max) =>
        Math.BetweenAgainstMax(x, min, max) &&
        Math.BetweenAgainstMax(y, min, max) &&
        Math.BetweenAgainstMax(z, min, max);

    /// <returns> true if (min.x &lt; x ≤ max.x), (min.y &lt; y ≤ max.y) and (min.z &lt; z ≤ max.z) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(VectorZ min, VectorZ max) =>
        Math.BetweenAgainstMax(x, min.x, max.x) &&
        Math.BetweenAgainstMax(y, min.y, max.y) &&
        Math.BetweenAgainstMax(z, min.z, max.z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(int min, int max) =>
        Math.BetweenAgainstMin(x, min, max) &&
        Math.BetweenAgainstMin(y, min, max) &&
        Math.BetweenAgainstMin(z, min, max);

    /// <returns> true if (min.x ≤ x &lt; max.x), (min.y ≤ y &lt; max.y) and (min.z ≤ z &lt; max.z) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(VectorZ min, VectorZ max) =>
        Math.BetweenAgainstMin(x, min.x, max.x) &&
        Math.BetweenAgainstMin(y, min.y, max.y) &&
        Math.BetweenAgainstMin(z, min.z, max.z);
    #endregion

    /// <returns> new vector of (min ≤ x ≤ max, min ≤ y ≤ max, min ≤ z ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Clamp(int min, int max) =>
        new(Math.Clamp(x, min, max), Math.Clamp(y, min, max), Math.Clamp(z, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Clamp(VectorZ min, VectorZ max) =>
        new(Math.Clamp(x, min.x, max.y), Math.Clamp(y, min.x, max.y), Math.Clamp(z, min.x, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Closest(VectorZ a, VectorZ b) => DistanceSquared(a) < DistanceSquared(b) ? a : b;

    public VectorZ Closest(params VectorZ[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        VectorZ closest = values[0];
        foreach (VectorZ v in values)
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

    public int ClosestIndex(VectorZ[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            VectorZ current = values[i];
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
    public VectorZ Cube() => new(x * x * x, y * y * y, z * z * z);

    #region Equals
    // <returns> true if the object is a vector and 'x', 'y' and 'z' of each vector are equal </returns>
    /*public override bool Equals(object obj)
    {
        if (obj is VectorZ vz) return Equals(vz);
        if (obj is VectorZF vzf) return Equals(vzf);
        return false;
    }*/

    /// <returns> true if 'x', 'y' and 'z' of each vector are equal </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorZ vz) => x == vz.x && y == vz.y && z == vz.z;

    /// <returns> true if 'x', 'y' and 'z' of each vector are equal </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorZF vzf) => x == vzf.x && y == vzf.y && z == vzf.z;

    /// <returns> true if 'x', 'y' and 'z' equal 'i' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(int i) => x == i && y == i && z == i;
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Farthest(VectorZ a, VectorZ b) => DistanceSquared(a) > DistanceSquared(b) ? a : b;

    public VectorZ Farthest(params VectorZ[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = 0f, newDelta;

        VectorZ farthest = values[0];
        foreach (VectorZ v in values)
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

    public int FarthestIndex(VectorZ[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            VectorZ current = values[i];
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
    public Math.Sign FindSign(VectorZ reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(VectorZ reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => x == 0 && y == 0 && z == 0;

    public void MoveToZero(int amount)
    {
        if (amount <= 0 || IsZero()) return;

        if (x < 0) x += Math.Clamp(amount, 1, -x);
        else if (x > 0) x -= Math.Clamp(amount, 1, x);

        if (y < 0) y += Math.Clamp(amount, 1, -y);
        else if (y > 0) y -= Math.Clamp(amount, 1, y);

        if (z < 0) z += Math.Clamp(amount, 1, -z);
        else if (z > 0) z -= Math.Clamp(amount, 1, z);
    }

    public void MoveToZero(VectorZ amount)
    {
        if (IsZero()) return;

        if (amount.x > 0)
        {
            if (x < 0) x += Math.Clamp(amount.x, 1, -x);
            else if (x > 0) x -= Math.Clamp(amount.x, 1, x);
        }

        if (amount.y > 0)
        {
            if (y < 0) y += Math.Clamp(amount.y, 1, -y);
            else if (y > 0) y -= Math.Clamp(amount.y, 1, y);
        }

        if (amount.z > 0)
        {
            if (z < 0) z += Math.Clamp(amount.z, 1, -z);
            else if (z > 0) z -= Math.Clamp(amount.z, 1, z);
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Pow(int exp) =>
        new(Math.Pow(x, exp), Math.Pow(y, exp), Math.Pow(z, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Pow(VectorZ exp) =>
        new(Math.Pow(x, exp.x), Math.Pow(y, exp.y), Math.Pow(z, exp.z));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Sqr() => new(x * x, y * y, z * z);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZF SqrRoot() => new(Math.SqrRoot(x), Math.SqrRoot(y), Math.SqrRoot(z));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Wrap(int min, int max) =>
        new(Math.Wrap(x, min, max), Math.Wrap(y, min, max), Math.Wrap(z, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorZ Wrap(VectorZ min, VectorZ max) =>
        new(Math.Wrap(x, min.x, max.y), Math.Wrap(y, min.x, max.y), Math.Wrap(z, min.x, max.y));
}
