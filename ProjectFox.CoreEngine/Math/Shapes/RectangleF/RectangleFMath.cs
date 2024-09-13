using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static RectangleF Max(RectangleF a, RectangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static RectangleF Max(params RectangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static int MaxIndex(RectangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static RectangleF Min(RectangleF a, RectangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static RectangleF Min(params RectangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static int MinIndex(RectangleF[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Abs() =>
        new(Math.Abs(position.x), Math.Abs(position.y),
            Math.Abs(size.x), Math.Abs(size.y));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(float min, float max) =>
        Math.Between(position.x, min, max) && Math.Between(position.y, min, max) &&
        Math.Between(size.x, min, max) && Math.Between(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(VectorF min, VectorF max) =>
        Math.Between(position.x, min.x, max.x) && Math.Between(position.y, min.y, max.y) &&
        Math.Between(size.x, min.x, max.x) && Math.Between(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(RectangleF min, RectangleF max) =>
        Math.Between(position.x, min.position.x, max.position.x) && Math.Between(position.y, min.position.y, max.position.y) &&
        Math.Between(size.x, min.size.x, max.size.x) && Math.Between(size.y, min.size.y, max.size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(float min, float max) =>
        Math.BetweenAgainstBounds(position.x, min, max) && Math.BetweenAgainstBounds(position.y, min, max) &&
        Math.BetweenAgainstBounds(size.x, min, max) && Math.BetweenAgainstBounds(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(VectorF min, VectorF max) =>
        Math.BetweenAgainstBounds(position.x, min.x, max.x) && Math.BetweenAgainstBounds(position.y, min.y, max.y) &&
        Math.BetweenAgainstBounds(size.x, min.x, max.x) && Math.BetweenAgainstBounds(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(RectangleF min, RectangleF max) =>
        Math.BetweenAgainstBounds(position.x, min.position.x, max.position.x) && Math.BetweenAgainstBounds(position.y, min.position.y, max.position.y) &&
        Math.BetweenAgainstBounds(size.x, min.size.x, max.size.x) && Math.BetweenAgainstBounds(size.y, min.size.y, max.size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(float min, float max) =>
        Math.BetweenAgainstMax(position.x, min, max) && Math.BetweenAgainstMax(position.y, min, max) &&
        Math.BetweenAgainstMax(size.x, min, max) && Math.BetweenAgainstMax(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(VectorF min, VectorF max) =>
        Math.BetweenAgainstMax(position.x, min.x, max.x) && Math.BetweenAgainstMax(position.y, min.y, max.y) &&
        Math.BetweenAgainstMax(size.x, min.x, max.x) && Math.BetweenAgainstMax(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(RectangleF min, RectangleF max) =>
        Math.BetweenAgainstMax(position.x, min.position.x, max.position.x) && Math.BetweenAgainstMax(position.y, min.position.y, max.position.y) &&
        Math.BetweenAgainstMax(size.x, min.size.x, max.size.x) && Math.BetweenAgainstMax(size.y, min.size.y, max.size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(float min, float max) =>
        Math.BetweenAgainstMin(position.x, min, max) && Math.BetweenAgainstMin(position.y, min, max) &&
        Math.BetweenAgainstMin(size.x, min, max) && Math.BetweenAgainstMin(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(VectorF min, VectorF max) =>
        Math.BetweenAgainstMin(position.x, min.x, max.x) && Math.BetweenAgainstMin(position.y, min.y, max.y) &&
        Math.BetweenAgainstMin(size.x, min.x, max.x) && Math.BetweenAgainstMin(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(RectangleF min, RectangleF max) =>
        Math.BetweenAgainstMin(position.x, min.position.x, max.position.x) && Math.BetweenAgainstMin(position.y, min.position.y, max.position.y) &&
        Math.BetweenAgainstMin(size.x, min.size.x, max.size.x) && Math.BetweenAgainstMin(size.y, min.size.y, max.size.y);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Clamp(float min, float max) =>
        new(Math.Clamp(position.x, min, max), Math.Clamp(position.y, min, max),
            Math.Clamp(size.x, min, max), Math.Clamp(size.y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Clamp(VectorF min, VectorF max) =>
        new(Math.Clamp(position.x, min.x, max.x), Math.Clamp(position.y, min.y, max.y),
            Math.Clamp(size.x, min.x, max.x), Math.Clamp(size.y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Clamp(RectangleF min, RectangleF max) =>
        new(Math.Clamp(position.x, min.position.x, max.position.x), Math.Clamp(position.y, min.position.y, max.position.y),
            Math.Clamp(size.x, min.size.x, max.size.x), Math.Clamp(size.y, min.size.y, max.size.y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Closest(VectorF a, VectorF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF Closest(RectangleF a, RectangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Closest(params VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF Closest(params RectangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(RectangleF[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Cube() =>
        new(position.x * position.x * position.x, position.y * position.y * position.y,
            size.x * size.x * size.x, size.y * size.y * size.y);

    #region Equals
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(float value) => position == value && size == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(VectorF value) => position == value && size == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(RectangleF value) => position == value.position && size == value.size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Rectangle value) => position == value.position && size == value.size;
    #endregion

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Farthest(VectorF a, VectorF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF Farthest(RectangleF a, RectangleF b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF Farthest(params VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF Farthest(params RectangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(VectorF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(RectangleF[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(RectangleF reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(RectangleF reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => position.x == 0 && position.y == 0 && size.x == 0 && size.y == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(float amount)
    {
        position.MoveToZero(amount);
        size.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(VectorF amount)
    {
        position.MoveToZero(amount);
        size.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(RectangleF amount)
    {
        position.MoveToZero(amount.position);
        size.MoveToZero(amount.size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Pow(int exp) =>
        new(Math.Pow(position.x, exp), Math.Pow(position.y, exp),
            Math.Pow(size.x, exp), Math.Pow(size.y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Pow(RectangleF exp) =>
        new(Math.Pow(position.x, (int)exp.position.x), Math.Pow(position.y, (int)exp.position.y),
            Math.Pow(size.x, (int)exp.size.x), Math.Pow(size.y, (int)exp.size.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Sqr() =>
        new(position.x * position.x, position.y * position.y,
            size.x * size.x, size.y * size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF SqrRoot() =>
        new(Math.SqrRoot(position.x), Math.SqrRoot(position.y),
            Math.SqrRoot(size.x), Math.SqrRoot(size.y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Wrap(float min, float max) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Wrap(VectorF min, VectorF max) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Wrap(RectangleF min, RectangleF max) => default;
}