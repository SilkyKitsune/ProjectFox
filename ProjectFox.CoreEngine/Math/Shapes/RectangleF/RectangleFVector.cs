using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionFromZero() => CenterPoint.DirectionFromZero();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionToPoint(VectorF value) => CenterPoint.DirectionToPoint(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionToShape(RectangleF shape) => CenterPoint.DirectionToPoint(shape.CenterPoint);

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Distance(VectorF value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Distance(RectangleF value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceFromZero() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceFromZeroSquared() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceSquared(VectorF value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceSquared(RectangleF value) => default;
}