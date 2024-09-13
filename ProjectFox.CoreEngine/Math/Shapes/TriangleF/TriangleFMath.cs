using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct TriangleF
{
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static TriangleF Max(TriangleF a, TriangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static TriangleF Max(params TriangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static int MaxIndex(TriangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static TriangleF Min(TriangleF a, TriangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static TriangleF Min(params TriangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static int MinIndex(TriangleF[] values) => default;


    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Abs() =>
        new(Math.Abs(a.x), Math.Abs(a.y),
            Math.Abs(b.x), Math.Abs(b.y),
            Math.Abs(c.x), Math.Abs(c.y));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(float min, float max) =>
        Math.Between(a.x, min, max) && Math.Between(a.y, min, max) &&
        Math.Between(b.x, min, max) && Math.Between(b.y, min, max) &&
        Math.Between(c.x, min, max) && Math.Between(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(VectorF min, VectorF max) =>
        Math.Between(a.x, min.x, max.x) && Math.Between(a.y, min.y, max.y) &&
        Math.Between(b.x, min.x, max.x) && Math.Between(b.y, min.y, max.y) &&
        Math.Between(c.x, min.x, max.x) && Math.Between(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(TriangleF min, TriangleF max) =>
        Math.Between(a.x, min.a.x, max.a.x) && Math.Between(a.y, min.a.y, max.a.y) &&
        Math.Between(b.x, min.b.x, max.b.x) && Math.Between(b.y, min.b.y, max.b.y) &&
        Math.Between(c.x, min.c.x, max.c.x) && Math.Between(c.y, min.c.y, max.c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(float min, float max) =>
        Math.BetweenAgainstBounds(a.x, min, max) && Math.BetweenAgainstBounds(a.y, min, max) &&
        Math.BetweenAgainstBounds(b.x, min, max) && Math.BetweenAgainstBounds(b.y, min, max) &&
        Math.BetweenAgainstBounds(c.x, min, max) && Math.BetweenAgainstBounds(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(VectorF min, VectorF max) =>
        Math.BetweenAgainstBounds(a.x, min.x, max.x) && Math.BetweenAgainstBounds(a.y, min.y, max.y) &&
        Math.BetweenAgainstBounds(b.x, min.x, max.x) && Math.BetweenAgainstBounds(b.y, min.y, max.y) &&
        Math.BetweenAgainstBounds(c.x, min.x, max.x) && Math.BetweenAgainstBounds(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(TriangleF min, TriangleF max) =>
        Math.BetweenAgainstBounds(a.x, min.a.x, max.a.x) && Math.BetweenAgainstBounds(a.y, min.a.y, max.a.y) &&
        Math.BetweenAgainstBounds(b.x, min.b.x, max.b.x) && Math.BetweenAgainstBounds(b.y, min.b.y, max.b.y) &&
        Math.BetweenAgainstBounds(c.x, min.c.x, max.c.x) && Math.BetweenAgainstBounds(c.y, min.c.y, max.c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(float min, float max) =>
        Math.BetweenAgainstMax(a.x, min, max) && Math.BetweenAgainstMax(a.y, min, max) &&
        Math.BetweenAgainstMax(b.x, min, max) && Math.BetweenAgainstMax(b.y, min, max) &&
        Math.BetweenAgainstMax(c.x, min, max) && Math.BetweenAgainstMax(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(VectorF min, VectorF max) =>
        Math.BetweenAgainstMax(a.x, min.x, max.x) && Math.BetweenAgainstMax(a.y, min.y, max.y) &&
        Math.BetweenAgainstMax(b.x, min.x, max.x) && Math.BetweenAgainstMax(b.y, min.y, max.y) &&
        Math.BetweenAgainstMax(c.x, min.x, max.x) && Math.BetweenAgainstMax(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(TriangleF min, TriangleF max) =>
        Math.BetweenAgainstMax(a.x, min.a.x, max.a.x) && Math.BetweenAgainstMax(a.y, min.a.y, max.a.y) &&
        Math.BetweenAgainstMax(b.x, min.b.x, max.b.x) && Math.BetweenAgainstMax(b.y, min.b.y, max.b.y) &&
        Math.BetweenAgainstMax(c.x, min.c.x, max.c.x) && Math.BetweenAgainstMax(c.y, min.c.y, max.c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(float min, float max) =>
        Math.BetweenAgainstMin(a.x, min, max) && Math.BetweenAgainstMin(a.y, min, max) &&
        Math.BetweenAgainstMin(b.x, min, max) && Math.BetweenAgainstMin(b.y, min, max) &&
        Math.BetweenAgainstMin(c.x, min, max) && Math.BetweenAgainstMin(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(VectorF min, VectorF max) =>
        Math.BetweenAgainstMin(a.x, min.x, max.x) && Math.BetweenAgainstMin(a.y, min.y, max.y) &&
        Math.BetweenAgainstMin(b.x, min.x, max.x) && Math.BetweenAgainstMin(b.y, min.y, max.y) &&
        Math.BetweenAgainstMin(c.x, min.x, max.x) && Math.BetweenAgainstMin(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(TriangleF min, TriangleF max) =>
        Math.BetweenAgainstMin(a.x, min.a.x, max.a.x) && Math.BetweenAgainstMin(a.y, min.a.y, max.a.y) &&
        Math.BetweenAgainstMin(b.x, min.b.x, max.b.x) && Math.BetweenAgainstMin(b.y, min.b.y, max.b.y) &&
        Math.BetweenAgainstMin(c.x, min.c.x, max.c.x) && Math.BetweenAgainstMin(c.y, min.c.y, max.c.y);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Clamp(float min, float max) =>
        new(Math.Clamp(a.x, min, max), Math.Clamp(a.y, min, max),
            Math.Clamp(b.x, min, max), Math.Clamp(b.y, min, max),
            Math.Clamp(c.x, min, max), Math.Clamp(c.y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Clamp(VectorF min, VectorF max) =>
        new(Math.Clamp(a.x, min.x, max.x), Math.Clamp(a.y, min.y, max.y),
            Math.Clamp(b.x, min.x, max.x), Math.Clamp(b.y, min.y, max.y),
            Math.Clamp(c.x, min.x, max.x), Math.Clamp(c.y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Clamp(TriangleF min, TriangleF max) =>
        new(Math.Clamp(a.x, min.a.x, max.a.x), Math.Clamp(a.y, min.a.y, max.a.y),
            Math.Clamp(b.x, min.b.x, max.b.x), Math.Clamp(b.y, min.b.y, max.b.y),
            Math.Clamp(c.x, min.c.x, max.c.x), Math.Clamp(c.y, min.c.y, max.c.y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Closest(VectorF a, VectorF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Closest(params VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public TriangleF Closest(TriangleF a, TriangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public TriangleF Closest(params TriangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(params TriangleF[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Cube() =>
        new(a.x * a.x * a.x, a.y * a.y * a.y,
            b.x * b.x * b.x, b.y * b.y * b.y,
            c.x * c.x * c.x, c.y * c.y * c.y);

    #region Equals
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(float value) =>
        a.x == value && a.y == value &&
        b.x == value && b.y == value &&
        c.x == value && c.y == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorF value) =>
        a.x == value.x && a.y == value.y &&
        b.x == value.x && b.y == value.y &&
        c.x == value.x && c.y == value.y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(TriangleF value) =>
        a.x == value.a.x && a.y == value.a.y &&
        b.x == value.b.x && b.y == value.b.y &&
        c.x == value.c.x && c.y == value.c.y;
    #endregion

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Farthest(VectorF a, VectorF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Farthest(params VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public TriangleF Farthest(TriangleF a, TriangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public TriangleF Farthest(params TriangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(TriangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(TriangleF reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(TriangleF reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() =>
        a.x == 0 && a.y == 0 &&
        b.x == 0 && b.y == 0 &&
        c.x == 0 && c.y == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(float amount)
    {
        a.MoveToZero(amount);
        b.MoveToZero(amount);
        c.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(VectorF amount)
    {
        a.MoveToZero(amount);
        b.MoveToZero(amount);
        c.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(TriangleF amount)
    {
        a.MoveToZero(amount.a);
        b.MoveToZero(amount.b);
        c.MoveToZero(amount.c);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Pow(int exp) =>
        new(Math.Pow(a.x, exp), Math.Pow(a.y, exp),
            Math.Pow(b.x, exp), Math.Pow(b.y, exp),
            Math.Pow(c.x, exp), Math.Pow(c.y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Pow(TriangleF exp) =>
        new(Math.Pow(a.x, (int)exp.a.x), Math.Pow(a.y, (int)exp.a.y),
            Math.Pow(b.x, (int)exp.b.x), Math.Pow(b.y, (int)exp.b.y),
            Math.Pow(c.x, (int)exp.c.x), Math.Pow(c.y, (int)exp.c.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Sqr() =>
        new(a.x * a.x, a.y * a.y,
            b.x * b.x, b.y * b.y,
            c.x * c.x, c.y * c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF SqrRoot() =>
        new(Math.SqrRoot(a.x), Math.SqrRoot(a.y),
            Math.SqrRoot(b.x), Math.SqrRoot(b.y),
            Math.SqrRoot(c.x), Math.SqrRoot(c.y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Wrap(float min, float max) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Wrap(VectorF min, VectorF max) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF Wrap(TriangleF min, TriangleF max) => default;
}