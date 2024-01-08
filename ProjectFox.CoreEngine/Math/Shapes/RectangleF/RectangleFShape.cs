using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    #region Enveloping
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(Vector value) => (value.x > position.x && value.x < position.x + size.x) && (value.y > position.y && value.y < position.y + size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(VectorF value) => (value.x > position.x && value.x < position.x + size.x) && (value.y > position.y && value.y < position.y + size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(RectangleF shape) => (shape.size.x > size.x || shape.size.y > size.y) ? false : Enveloping(shape.position);

    public bool Enveloping(IPolytope<Vector, Triangle> shape)
    {
        Vector[] points = shape.Points;

        if (points == null || points.Length == 0) return false;

        foreach (Vector v in points)
            if (!Enveloping(v)) return false;
        return true;
    }

    public bool Enveloping(IPolytope<VectorF, TriangleF> shape)
    {
        VectorF[] points = shape.Points;

        if (points == null || points.Length == 0) return false;

        foreach (VectorF v in points)
            if (!Enveloping(v)) return false;
        return true;
    }
    #endregion

    #region IntersectionArea
    public RectangleF IntersectionArea(RectangleF shape)
    {
        if (Equals(shape)) return shape;

        float x = shape.position.x - position.x, y = shape.position.y - position.y;
        bool xb = x < 0f, yb = y < 0f;

        return new(
            xb ? shape.position.x - x : position.x + x,
            yb ? shape.position.y - y : position.y + y,
            xb ? shape.size.x - x : size.x + x,
            yb ? shape.size.y - y : size.y + y);
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle IntersectionArea(Rectangle shape) => default;

    public IPolytope<Vector, Triangle> IntersectionArea(IPolytope<Vector, Triangle> shape)
    {
        if (shape == null) throw new ArgumentNullException();

        if (shape is Rectangle rectangle) return IntersectionArea(rectangle);

        return null;
    }

    public IPolytope<VectorF, TriangleF> IntersectionArea(IPolytope<VectorF, TriangleF> shape)
    {
        if (shape == null) throw new ArgumentNullException();

        if (shape is RectangleF rectangle) return IntersectionArea(rectangle);

        return null;
    }
    #endregion

    #region Intersecting
    public bool Intersecting(RectangleF shape)
    {
        if (Equals(shape)) return true;

        float x = shape.position.x - position.x, y = shape.position.y - position.x;
        return (x < 0f ? -x < shape.size.x : x < size.x) && (y < 0f ? -y < shape.size.y : y < size.y);
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Intersecting(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Intersecting(IPolytope<VectorF, TriangleF> shape) => default;
    #endregion

    #region Overlapping
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(Vector vector) => (vector.x >= position.x && vector.x <= position.x + size.x) && (vector.y >= position.y && vector.y <= position.y + size.y);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(VectorF vector) => (vector.x >= position.x && vector.x <= position.x + size.x) && (vector.y >= position.y && vector.y <= position.y + size.y);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(RectangleF shape) => Equals(shape) || Enveloping(shape) || shape.Enveloping(this);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(IPolytope<Vector, Triangle> shape) => Equals(shape) || Enveloping(shape);// || shape.Enveloping(this);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(IPolytope<VectorF, TriangleF> shape) => Equals(shape) || Enveloping(shape);// || shape.Enveloping(this);
    #endregion

    #region Touching
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(RectangleF shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(IPolytope<VectorF, TriangleF> shape) => default;
    #endregion
}