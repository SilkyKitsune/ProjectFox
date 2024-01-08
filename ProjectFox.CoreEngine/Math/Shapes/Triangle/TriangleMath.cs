using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Triangle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Abs() =>
        new(Math.Abs(a.x), Math.Abs(a.y),
            Math.Abs(b.x), Math.Abs(b.y),
            Math.Abs(c.x), Math.Abs(c.y));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(int min, int max) =>
        Math.Between(a.x, min, max) && Math.Between(a.y, min, max) &&
        Math.Between(b.x, min, max) && Math.Between(b.y, min, max) &&
        Math.Between(c.x, min, max) && Math.Between(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(Vector min, Vector max) =>
        Math.Between(a.x, min.x, max.x) && Math.Between(a.y, min.y, max.y) &&
        Math.Between(b.x, min.x, max.x) && Math.Between(b.y, min.y, max.y) &&
        Math.Between(c.x, min.x, max.x) && Math.Between(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(Triangle min, Triangle max) =>
        Math.Between(a.x, min.a.x, max.a.x) && Math.Between(a.y, min.a.y, max.a.y) &&
        Math.Between(b.x, min.b.x, max.b.x) && Math.Between(b.y, min.b.y, max.b.y) &&
        Math.Between(c.x, min.c.x, max.c.x) && Math.Between(c.y, min.c.y, max.c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(int min, int max) =>
        Math.BetweenAgainstBounds(a.x, min, max) && Math.BetweenAgainstBounds(a.y, min, max) &&
        Math.BetweenAgainstBounds(b.x, min, max) && Math.BetweenAgainstBounds(b.y, min, max) &&
        Math.BetweenAgainstBounds(c.x, min, max) && Math.BetweenAgainstBounds(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(Vector min, Vector max) =>
        Math.BetweenAgainstBounds(a.x, min.x, max.x) && Math.BetweenAgainstBounds(a.y, min.y, max.y) &&
        Math.BetweenAgainstBounds(b.x, min.x, max.x) && Math.BetweenAgainstBounds(b.y, min.y, max.y) &&
        Math.BetweenAgainstBounds(c.x, min.x, max.x) && Math.BetweenAgainstBounds(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(Triangle min, Triangle max) =>
        Math.BetweenAgainstBounds(a.x, min.a.x, max.a.x) && Math.BetweenAgainstBounds(a.y, min.a.y, max.a.y) &&
        Math.BetweenAgainstBounds(b.x, min.b.x, max.b.x) && Math.BetweenAgainstBounds(b.y, min.b.y, max.b.y) &&
        Math.BetweenAgainstBounds(c.x, min.c.x, max.c.x) && Math.BetweenAgainstBounds(c.y, min.c.y, max.c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(int min, int max) =>
        Math.BetweenAgainstMax(a.x, min, max) && Math.BetweenAgainstMax(a.y, min, max) &&
        Math.BetweenAgainstMax(b.x, min, max) && Math.BetweenAgainstMax(b.y, min, max) &&
        Math.BetweenAgainstMax(c.x, min, max) && Math.BetweenAgainstMax(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(Vector min, Vector max) =>
        Math.BetweenAgainstMax(a.x, min.x, max.x) && Math.BetweenAgainstMax(a.y, min.y, max.y) &&
        Math.BetweenAgainstMax(b.x, min.x, max.x) && Math.BetweenAgainstMax(b.y, min.y, max.y) &&
        Math.BetweenAgainstMax(c.x, min.x, max.x) && Math.BetweenAgainstMax(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(Triangle min, Triangle max) =>
        Math.BetweenAgainstMax(a.x, min.a.x, max.a.x) && Math.BetweenAgainstMax(a.y, min.a.y, max.a.y) &&
        Math.BetweenAgainstMax(b.x, min.b.x, max.b.x) && Math.BetweenAgainstMax(b.y, min.b.y, max.b.y) &&
        Math.BetweenAgainstMax(c.x, min.c.x, max.c.x) && Math.BetweenAgainstMax(c.y, min.c.y, max.c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(int min, int max) =>
        Math.BetweenAgainstMin(a.x, min, max) && Math.BetweenAgainstMin(a.y, min, max) &&
        Math.BetweenAgainstMin(b.x, min, max) && Math.BetweenAgainstMin(b.y, min, max) &&
        Math.BetweenAgainstMin(c.x, min, max) && Math.BetweenAgainstMin(c.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(Vector min, Vector max) =>
        Math.BetweenAgainstMin(a.x, min.x, max.x) && Math.BetweenAgainstMin(a.y, min.y, max.y) &&
        Math.BetweenAgainstMin(b.x, min.x, max.x) && Math.BetweenAgainstMin(b.y, min.y, max.y) &&
        Math.BetweenAgainstMin(c.x, min.x, max.x) && Math.BetweenAgainstMin(c.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(Triangle min, Triangle max) =>
        Math.BetweenAgainstMin(a.x, min.a.x, max.a.x) && Math.BetweenAgainstMin(a.y, min.a.y, max.a.y) &&
        Math.BetweenAgainstMin(b.x, min.b.x, max.b.x) && Math.BetweenAgainstMin(b.y, min.b.y, max.b.y) &&
        Math.BetweenAgainstMin(c.x, min.c.x, max.c.x) && Math.BetweenAgainstMin(c.y, min.c.y, max.c.y);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Clamp(int min, int max) =>
        new(Math.Clamp(a.x, min, max), Math.Clamp(a.y, min, max),
            Math.Clamp(b.x, min, max), Math.Clamp(b.y, min, max),
            Math.Clamp(c.x, min, max), Math.Clamp(c.y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Clamp(Vector min, Vector max) =>
        new(Math.Clamp(a.x, min.x, max.x), Math.Clamp(a.y, min.y, max.y),
            Math.Clamp(b.x, min.x, max.x), Math.Clamp(b.y, min.y, max.y),
            Math.Clamp(c.x, min.x, max.x), Math.Clamp(c.y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Clamp(Triangle min, Triangle max) =>
        new(Math.Clamp(a.x, min.a.x, max.a.x), Math.Clamp(a.y, min.a.y, max.a.y),
            Math.Clamp(b.x, min.b.x, max.b.x), Math.Clamp(b.y, min.b.y, max.b.y),
            Math.Clamp(c.x, min.c.x, max.c.x), Math.Clamp(c.y, min.c.y, max.c.y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Closest(Vector a, Vector b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Closest(params Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Closest(Triangle a, Triangle b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Closest(params Triangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(params Triangle[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Cube() =>
        new(a.x * a.x * a.x, a.y * a.y * a.y,
            b.x * b.x * b.x, b.y * b.y * b.y,
            c.x * c.x * c.x, c.y * c.y * c.y);

    #region Equals
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(int value) =>
        a.x == value && a.y == value &&
        b.x == value && b.y == value &&
        c.x == value && c.y == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector value) =>
        a.x == value.x && a.y == value.y &&
        b.x == value.x && b.y == value.y &&
        c.x == value.x && c.y == value.y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Triangle value) =>
        a.x == value.a.x && a.y == value.a.y &&
        b.x == value.b.x && b.y == value.b.y &&
        c.y == value.c.x && c.y == value.c.y;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(TriangleF value) =>
        a.x == value.a.x && a.y == value.a.y &&
        b.x == value.b.x && b.y == value.b.y &&
        c.y == value.c.x && c.y == value.c.y;
    #endregion

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Farthest(Vector a, Vector b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Farthest(params Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Farthest(Triangle a, Triangle b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Farthest(params Triangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(Triangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(Triangle reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(Triangle reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() =>
        a.x == 0 && a.y == 0 &&
        b.x == 0 && b.y == 0 &&
        c.x == 0 && c.y == 0;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Max(Triangle value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Max(params Triangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int MaxIndex(Triangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Min(Triangle value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle Min(params Triangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int MinIndex(Triangle[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(int amount)
    {
        a.MoveToZero(amount);
        b.MoveToZero(amount);
        c.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(Vector amount)
    {
        a.MoveToZero(amount);
        b.MoveToZero(amount);
        c.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(Triangle amount)
    {
        a.MoveToZero(amount.a);
        b.MoveToZero(amount.b);
        c.MoveToZero(amount.c);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Pow(int exp) =>
        new(Math.Pow(a.x, exp), Math.Pow(a.y, exp),
            Math.Pow(b.x, exp), Math.Pow(b.y, exp),
            Math.Pow(c.x, exp), Math.Pow(c.y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Pow(Triangle exp) =>
        new(Math.Pow(a.x, exp.a.x), Math.Pow(a.y, exp.a.y),
            Math.Pow(b.x, exp.b.x), Math.Pow(b.y, exp.b.y),
            Math.Pow(c.x, exp.c.x), Math.Pow(c.y, exp.c.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Sqr() =>
        new(a.x * a.x, a.y * a.y,
            b.x * b.x, b.y * b.y,
            c.x * c.x, c.y * c.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF SqrRoot() =>
        new(Math.SqrRoot(a.x), Math.SqrRoot(a.y),
            Math.SqrRoot(b.x), Math.SqrRoot(b.y),
            Math.SqrRoot(c.x), Math.SqrRoot(c.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Wrap(int min, int max) =>
        new(Math.Wrap(a.x, min, max), Math.Wrap(a.y, min, max),
            Math.Wrap(b.x, min, max), Math.Wrap(b.y, min, max),
            Math.Wrap(c.x, min, max), Math.Wrap(c.y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Wrap(Vector min, Vector max) =>
        new(Math.Wrap(a.x, min.x, max.x), Math.Wrap(a.y, min.y, max.y),
            Math.Wrap(b.x, min.x, max.x), Math.Wrap(b.y, min.y, max.y),
            Math.Wrap(c.x, min.x, max.x), Math.Wrap(c.y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle Wrap(Triangle min, Triangle max) =>
        new(Math.Wrap(a.x, min.a.x, max.a.x), Math.Wrap(a.y, min.a.y, max.a.y),
            Math.Wrap(b.x, min.b.x, max.b.x), Math.Wrap(b.y, min.b.y, max.b.y),
            Math.Wrap(c.x, min.c.x, max.c.x), Math.Wrap(c.y, min.c.y, max.c.y));
}