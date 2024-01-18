using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
    /// <returns> new vector of ( | x | , | y | )  </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Abs() => new(Math.Abs(x), Math.Abs(y));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(int min, int max) =>
        Math.Between(x, min, max) &&
        Math.Between(y, min, max);

    /// <returns> true if (min.x &lt; this.x &lt; max.x) and (min.y &lt; this.y &lt; max.y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(Vector min, Vector max) =>
        Math.Between(x, min.x, max.x) &&
        Math.Between(y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(int min, int max) =>
        Math.BetweenAgainstBounds(x, min, max) &&
        Math.BetweenAgainstBounds(y, min, max);

    /// <returns> true if (min.x ≤ this.x ≤ max.x) and (min.y ≤ this.y ≤ max.y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(Vector min, Vector max) =>
        Math.BetweenAgainstBounds(x, min.x, max.x) &&
        Math.BetweenAgainstBounds(y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(int min, int max) =>
        Math.BetweenAgainstMax(x, min, max) &&
        Math.BetweenAgainstMax(y, min, max);

    /// <returns> true if (min.x &lt; x ≤ max.x) and (min.y &lt; y ≤ max.y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(Vector min, Vector max) =>
        Math.BetweenAgainstMax(x, min.x, max.x) &&
        Math.BetweenAgainstMax(y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(int min, int max) =>
        Math.BetweenAgainstMin(x, min, max) &&
        Math.BetweenAgainstMin(y, min, max);

    /// <returns> true if (min.x ≤ x &lt; max.x) and (min.y ≤ y &lt; max.y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(Vector min, Vector max) =>
        Math.BetweenAgainstMin(x, min.x, max.x) &&
        Math.BetweenAgainstMin(y, min.y, max.y);
    #endregion

    /// <returns> new vector of (min ≤ x ≤ max, min ≤ y ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Clamp(int min, int max) =>
        new(Math.Clamp(x, min, max), Math.Clamp(y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Clamp(Vector min, Vector max) =>
        new(Math.Clamp(x, min.x, max.x), Math.Clamp(y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Closest(Vector a, Vector b) => DistanceSquared(a) < DistanceSquared(b) ? a : b;

    /// <returns> vector with the smallest distance to 'this' </returns>
    /// <exception cref="ArgumentException"/>
    public Vector Closest(params Vector[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = float.MaxValue, newDelta;

        Vector closest = values[0];
        foreach (Vector v in values)
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

    public int ClosestIndex(Vector[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            Vector current = values[i];
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

    /// <returns> new vector of (x³, y³) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Cube() => new(x * x * x, y * y * y);

    #region Equals
    // <returns> true if the object is a vector and 'x' and 'y' of each vector are equal </returns>
    /*public override bool Equals(object obj)
    {
        if (obj is Vector v) return Equals(v);
        if (obj is VectorF vf) return Equals(vf);
        return false;
    }*/

    /// <returns> true if 'x' and 'y' of each vector are equal </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector v) => x == v.x && y == v.y;

    /// <returns> true if 'x' and 'y' of each vector are equal </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorF vf) => x == vf.x && y == vf.y;

    /// <returns> true if 'x' and 'y' equal 'i' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(int i) => x == i && y == i;
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Farthest(Vector a, Vector b) => DistanceSquared(a) > DistanceSquared(b) ? a : b;

    public Vector Farthest(params Vector[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float delta = 0f, newDelta;

        Vector farthest = values[0];
        foreach (Vector v in values)
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

    public int FarthestIndex(Vector[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            Vector current = values[i];
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
    public Math.Sign FindSign(Vector reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(Vector reference) => default;

    /// <returns> true if (this = (0, 0)) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => x == 0 && y == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Max(Vector value) => DistanceFromZeroSquared() > value.DistanceFromZeroSquared() ? this : value;

    public Vector Max(params Vector[] values)
    {
        if (values.Length == 0) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        Vector max = this;
        foreach (Vector v in values)
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

    public int MaxIndex(Vector[] values)
    {
        if (values.Length == 0) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int max = -1;//this will throw exception
        for (int i = 0; i < values.Length; i++)
        {
            Vector current = values[i];
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
    public Vector Min(Vector value) => DistanceFromZeroSquared() < value.DistanceFromZeroSquared() ? this : value;

    public Vector Min(params Vector[] values)
    {
        if (values.Length == 0 || IsZero()) return this;

        float delta = DistanceFromZeroSquared(), newDelta;

        Vector min = this;
        foreach (Vector v in values)
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

    public int MinIndex(Vector[] values)
    {
        if (values.Length == 0 || IsZero()) return -1;//is this okay?

        float delta = DistanceFromZeroSquared(), newDelta;

        int min = 1;//this will throw exception
        for (int i = 0; i < values.Length; i++)
        {
            Vector current = values[i];
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

    /// <param name="amount"> the absolute distance to move 'this' to (0, 0) </param>
    /// <remarks> has no effect if (amount ≤ 0) </remarks>
    public void MoveToZero(int amount)
    {
        if (amount <= 0 || IsZero()) return;

        if (x < 0) x += Math.Clamp(amount, 1, -x);
        else if (x > 0) x -= Math.Clamp(amount, 1, x);

        if (y < 0) y += Math.Clamp(amount, 1, -y);
        else if (y > 0) y -= Math.Clamp(amount, 1, y);
    }

    public void MoveToZero(Vector amount)
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
    }

    #region pow/root
    /// <returns> new vector of (xᵉˣᵖ, yᵉˣᵖ) </returns>
    /// <remarks> negative exponents will always return zero, check overloads for additional operations </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Pow(int exp) =>
        new(Math.Pow(x, exp), Math.Pow(y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Pow(Vector exp) =>
        new(Math.Pow(x, exp.x), Math.Pow(y, exp.y));

    // <returns> new vector of (xᵉˣᵖ, yᵉˣᵖ) </returns>
    // <remarks> check overloads for additional operations </remarks>
    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Pow(float exp) =>
        new(Math.Pow(x, exp), Math.Pow(y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Pow(VectorF exp) =>
        new(Math.Pow(x, exp.x), Math.Pow(y, exp.y));*/

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Root(int root) =>
        new(Math.Root(x, root), Math.Root(y, root));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Root(Vector root) =>
        new(Math.Root(x, root.x), Math.Root(y, root.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Root(float root) =>
        new(Math.Root(x, root), Math.Root(y, root));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Root(VectorF root) =>
        new(Math.Root(x, root.x), Math.Root(y, root.y));*/
    #endregion

    /// <returns> new vector of (x², y²) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Sqr() => new(x * x, y * y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF SqrRoot() => new(Math.SqrRoot(x), Math.SqrRoot(y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Wrap(Vector min, Vector max) =>
        new(Math.Wrap(x, min.x, max.x), Math.Wrap(y, min.y, max.y));

    /// <returns> new vector with 'this.x' and 'this.y' wrapped over the bounds of 'min' and 'max' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector Wrap(int min, int max) =>
        new(Math.Wrap(x, min, max), Math.Wrap(y, min, max));
}
