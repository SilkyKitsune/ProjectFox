using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Rectangle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Abs() =>
        new(Math.Abs(position.x), Math.Abs(position.y),
            Math.Abs(size.x), Math.Abs(size.y));

    #region Between
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(int min, int max) =>
        Math.Between(position.x, min, max) && Math.Between(position.y, min, max) &&
        Math.Between(size.x, min, max) && Math.Between(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(Vector min, Vector max) =>
        Math.Between(position.x, min.x, max.x) && Math.Between(position.y, min.y, max.y) &&
        Math.Between(size.x, min.x, max.x) && Math.Between(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Between(Rectangle min, Rectangle max) =>
        Math.Between(position.x, min.position.x, max.position.x) && Math.Between(position.y, min.position.y, max.position.y) &&
        Math.Between(size.x, min.size.x, max.size.x) && Math.Between(size.y, min.size.y, max.size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(int min, int max) =>
        Math.BetweenAgainstBounds(position.x, min, max) && Math.BetweenAgainstBounds(position.y, min, max) &&
        Math.BetweenAgainstBounds(size.x, min, max) && Math.BetweenAgainstBounds(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(Vector min, Vector max) =>
        Math.BetweenAgainstBounds(position.x, min.x, max.x) && Math.BetweenAgainstBounds(position.y, min.y, max.y) &&
        Math.BetweenAgainstBounds(size.x, min.x, max.x) && Math.BetweenAgainstBounds(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstBounds(Rectangle min, Rectangle max) =>
        Math.BetweenAgainstBounds(position.x, min.position.x, max.position.x) && Math.BetweenAgainstBounds(position.y, min.position.y, max.position.y) &&
        Math.BetweenAgainstBounds(size.x, min.size.x, max.size.x) && Math.BetweenAgainstBounds(size.y, min.size.y, max.size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(int min, int max) =>
        Math.BetweenAgainstMax(position.x, min, max) && Math.BetweenAgainstMax(position.y, min, max) &&
        Math.BetweenAgainstMax(size.x, min, max) && Math.BetweenAgainstMax(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(Vector min, Vector max) =>
        Math.BetweenAgainstMax(position.x, min.x, max.x) && Math.BetweenAgainstMax(position.y, min.y, max.y) &&
        Math.BetweenAgainstMax(size.x, min.x, max.x) && Math.BetweenAgainstMax(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMax(Rectangle min, Rectangle max) =>
        Math.BetweenAgainstMax(position.x, min.position.x, max.position.x) && Math.BetweenAgainstMax(position.y, min.position.y, max.position.y) &&
        Math.BetweenAgainstMax(size.x, min.size.x, max.size.x) && Math.BetweenAgainstMax(size.y, min.size.y, max.size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(int min, int max) =>
        Math.BetweenAgainstMin(position.x, min, max) && Math.BetweenAgainstMin(position.y, min, max) &&
        Math.BetweenAgainstMin(size.x, min, max) && Math.BetweenAgainstMin(size.y, min, max);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(Vector min, Vector max) =>
        Math.BetweenAgainstMin(position.x, min.x, max.x) && Math.BetweenAgainstMin(position.y, min.y, max.y) &&
        Math.BetweenAgainstMin(size.x, min.x, max.x) && Math.BetweenAgainstMin(size.y, min.y, max.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool BetweenAgainstMin(Rectangle min, Rectangle max) =>
        Math.BetweenAgainstMin(position.x, min.position.x, max.position.x) && Math.BetweenAgainstMin(position.y, min.position.y, max.position.y) &&
        Math.BetweenAgainstMin(size.x, min.size.x, max.size.x) && Math.BetweenAgainstMin(size.y, min.size.y, max.size.y);
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Clamp(int min, int max) =>
        new(Math.Clamp(position.x, min, max), Math.Clamp(position.y, min, max),
            Math.Clamp(size.x, min, max), Math.Clamp(size.y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Clamp(Vector min, Vector max) =>
        new(Math.Clamp(position.x, min.x, max.x), Math.Clamp(position.y, min.y, max.y),
            Math.Clamp(size.x, min.x, max.x), Math.Clamp(size.y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Clamp(Rectangle min, Rectangle max) =>
        new(Math.Clamp(position.x, min.position.x, max.position.x), Math.Clamp(position.y, min.position.y, max.position.y),
            Math.Clamp(size.x, min.size.x, max.size.x), Math.Clamp(size.y, min.size.y, max.size.y));

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Closest(Vector a, Vector b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Closest(params Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Closest(Rectangle a, Rectangle b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Closest(params Rectangle[] values)
    {
        //this might suck
        /*if (values.Length == 0) throw new ArgumentNullException(nameof(values));

        int closest = 0;
        float delta = float.MaxValue, newDelta;
        Vector centerPoint = CenterPoint, closestCenter = new(int.MaxValue, int.MaxValue), currentCenter;
        for (int i = 0; i < values.Length; i++)
        {
            currentCenter = values[i].CenterPoint;
            if (centerPoint.Equals(currentCenter)) return values[i];

            if (!closestCenter.Equals(currentCenter))
            {
                newDelta = Math.Abs(centerPoint.Distance(currentCenter));

                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                    closestCenter = currentCenter;
                }
            }
        }
        return values[closest];*/
        //throw new NotImplementedException();
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int ClosestIndex(Rectangle[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Cube() =>
        new(position.x * position.x * position.x, position.y * position.y * position.y,
            size.x * size.x * size.x, size.y * size.y * size.y);

    #region Equals
    /*public override bool Equals(object obj)
    {
        if (obj is Rectangle rect) return Equals(rect);
        if (obj is RectangleF rectf) return Equals(rectf);
        return false;
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(int value) => position == value && size == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Vector value) => position == value && size == value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(Rectangle value) => position == value.position && size == value.size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Equals(RectangleF value) => position == value.position && size == value.size;
    #endregion

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Farthest(Vector a, Vector b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Vector Farthest(Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Farthest(Rectangle a, Rectangle b) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Farthest(Rectangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(Vector[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FarthestIndex(Rectangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Math.Sign FindSign(Rectangle reference) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int FindSignInt(Rectangle reference) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsZero() => position.x == 0 && position.y == 0 && size.x == 0 && size.y == 0;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Max(Rectangle value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Max(params Rectangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int MaxIndex(Rectangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Min(Rectangle value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle Min(params Rectangle[] values) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public int MinIndex(Rectangle[] values) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(int amount)
    {
        position.MoveToZero(amount);
        size.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(Vector amount)
    {
        position.MoveToZero(amount);
        size.MoveToZero(amount);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void MoveToZero(Rectangle amount)
    {
        position.MoveToZero(amount.position);
        size.MoveToZero(amount.size);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Pow(int exp) =>
        new(Math.Pow(position.x, exp), Math.Pow(position.y, exp),
            Math.Pow(size.x, exp), Math.Pow(size.y, exp));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Pow(Rectangle exp) =>
        new(Math.Pow(position.x, exp.position.x), Math.Pow(position.y, exp.position.y),
            Math.Pow(size.x, exp.size.x), Math.Pow(size.y, exp.size.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Sqr() =>
        new(position.x * position.x, position.y * position.y,
            size.x * size.x, size.y * size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF SqrRoot() =>
        new(Math.SqrRoot(position.x), Math.SqrRoot(position.y),
            Math.SqrRoot(size.x), Math.SqrRoot(size.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Wrap(int min, int max) =>
        new(Math.Wrap(position.x, min, max), Math.Wrap(position.y, min, max),
            Math.Wrap(size.x, min, max), Math.Wrap(size.y, min, max));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Wrap(Vector min, Vector max) =>
        new(Math.Wrap(position.x, min.x, max.x), Math.Wrap(position.y, min.y, max.y),
            Math.Wrap(size.x, min.x, max.x), Math.Wrap(size.y, min.y, max.y));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle Wrap(Rectangle min, Rectangle max) =>
        new(Math.Wrap(position.x, min.position.x, max.position.x), Math.Wrap(position.y, min.position.y, max.position.y),
            Math.Wrap(size.x, min.size.x, max.size.x), Math.Wrap(size.y, min.size.y, max.size.y));
}