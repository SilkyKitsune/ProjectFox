using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Rectangle
{
    #region Enveloping
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(Vector value) => (value.x > position.x && value.x < position.x + size.x - 1) && (value.y > position.y && value.y < position.y + size.y - 1);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(VectorF value) => (value.x > position.x && value.x < position.x + size.x) && (value.y > position.y && value.y < position.y + size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(Rectangle shape)//I dont think this will work
    {
        return (shape.size.x > size.x || shape.size.y > size.y) ? false : Enveloping(shape.position);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Enveloping(RectangleF shape)
    {
        return (shape.size.x > size.x || shape.size.y > size.y) ? false : Enveloping(shape.position);
    }

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool Enveloping(Triangle shape) => Enveloping(shape.a) && Enveloping(shape.b) && Enveloping(shape.c);

    //[MethodImpl(MethodImplOptions.AggressiveInlining)]
    //public bool Enveloping(TriangleF shape) => Enveloping(shape.a) && Enveloping(shape.b) && Enveloping(shape.c);

    public bool Enveloping(IPolytope<Vector, Triangle> shape)
    {
        Vector[] points = shape?.Points;

        if (points == null || points.Length == 0) return false;

        foreach (Vector v in points)
            if (!Enveloping(v)) return false;
        return true;
    }

    public bool Enveloping(IPolytope<VectorF, TriangleF> shape)
    {
        VectorF[] points = shape?.Points;

        if (points == null || points.Length == 0) return false;

        foreach (VectorF v in points)
            if (!Enveloping(v)) return false;
        return true;
    }
    #endregion

    #region IntersectionArea
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Rectangle IntersectionArea(Rectangle shape)
    {
        if (Equals(shape)) return shape;

        int x = shape.position.x - position.x, y = shape.position.y - position.y;
        bool xNeg = x < 0, yNeg = y < 0,
            exNeg = shape.position.x + shape.size.x < position.x + size.x,//rename?
            eyNeg = shape.position.y + shape.size.y < position.y + size.y;

        return new(
            xNeg ? shape.position.x - x : position.x + x,
            yNeg ? shape.position.y - y : position.y + y,
            xNeg == exNeg ? (xNeg ? shape.size.x + x : size.x - x) : (exNeg ? shape.size.x : size.x),
            yNeg == eyNeg ? (yNeg ? shape.size.y + y : size.y - y) : (eyNeg ? shape.size.y : size.y));
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF IntersectionArea(RectangleF shape) => default;

    //public bool GetIntersectionArea(Triangle shape)?

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
    public bool Intersecting(Rectangle shape)
    {
        if (Equals(shape)) return true;

        int x = shape.position.x - position.x, y = shape.position.y - position.y;
        return (x < 0 ? -x < shape.size.x : x < size.x) && (y < 0 ? -y < shape.size.y : y < size.y);
    }

    public bool Intersecting(RectangleF shape)
    {
        if (Equals(shape)) return true;

        float x = shape.position.x - position.x, y = shape.position.y - position.y;
        return (x < 0f ? -x < shape.size.x : x < size.x) && (y < 0f ? -y < shape.size.y : y < size.y);
    }

    /*public bool Intersecting(Triangle shape) => default;
    public bool Intersecting(TriangleF shape) => default;*/

    public bool Intersecting(IPolytope<Vector, Triangle> shape)
    {
        if (shape == null) throw new ArgumentNullException();

        if (shape is Rectangle rectangle) return Intersecting(rectangle);

        //foreach (Triangle triangle in shape.GetPrimitives())
            //if (Intersecting(triangle)) return true;//this needs the triangle overload
        return false;
    }

    public bool Intersecting(IPolytope<VectorF, TriangleF> shape)
    {
        if (shape == null) throw new ArgumentNullException();

        if (shape is RectangleF rectangle) return Intersecting(rectangle);

        //foreach (TriangleF triangle in shape.GetPrimitives())
            //if (Intersecting(triangle)) return true;
        return false;
    }
    #endregion

    #region Overlapping
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(Vector vector) => (vector.x >= position.x && vector.x < position.x + size.x) && (vector.y >= position.y && vector.y < position.y + size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(VectorF vector) => (vector.x >= position.x && vector.x < position.x + size.x) && (vector.y >= position.y && vector.x < position.y + size.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(Rectangle shape) => Equals(shape) || Enveloping(shape) || shape.Enveloping(this);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(RectangleF shape) => Equals(shape) || Enveloping(shape) || shape.Enveloping((RectangleF)this);//is this okay?

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(IPolytope<Vector, Triangle> shape) => Equals(shape) || Enveloping(shape);// || shape.Enveloping(this);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Overlapping(IPolytope<VectorF, TriangleF> shape) => Equals(shape) || Enveloping(shape);// || shape.Enveloping(this);
    #endregion

    #region Touching
    public bool Touching(Rectangle shape)
    {
        Vector end = EndPoint, shapeEnd = shape.EndPoint;

        if (shapeEnd.y == position.y || shape.position.y == end.y) //top/bottom
            return (shape.position.x >= position.x && shape.position.x < end.x) || (shapeEnd.x > position.x && shapeEnd.x <= end.x) ||
                    (position.x >= shape.position.x && position.x < shapeEnd.x) || (end.x > shape.position.x && end.x <= shapeEnd.x);
        if (shapeEnd.x == position.x || shape.position.x == end.x) //left/right
            return (shape.position.y >= position.y && shape.position.y < end.y) || (shapeEnd.y > position.y && shapeEnd.y <= end.y) ||
                    (position.y >= shape.position.y && position.y < shapeEnd.y) || (end.y > shape.position.y && end.y <= shapeEnd.y);
        return false;
    }

    public bool Touching(RectangleF shape)
    {
        Vector end = EndPoint;
        VectorF shapeEnd = shape.EndPoint;

        if (shapeEnd.y == position.y || shape.position.y == end.y) //top/bottom
            return (shape.position.x >= position.x && shape.position.x < end.x) || (shapeEnd.x > position.x && shapeEnd.x <= end.x) ||
                    (position.x >= shape.position.x && position.x < shapeEnd.x) || (end.x > shape.position.x && end.x <= shapeEnd.x);
        if (shapeEnd.x == position.x || shape.position.x == end.x) //left/right
            return (shape.position.y >= position.y && shape.position.y < end.y) || (shapeEnd.y > position.y && shapeEnd.y <= end.y) ||
                    (position.y >= shape.position.y && position.y < shapeEnd.y) || (end.y > shape.position.y && end.y <= shapeEnd.y);
        return false;
    }

    /*public bool Touching(Triangle shape) => default;
    public bool Touching(TriangleF shape) => default;*/

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(IPolytope<VectorF, TriangleF> shape) => default;

    #region Touching Sides
    /// <returns> true if shape is touching the top </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingTop(Rectangle shape)
    {
        Vector end = EndPoint, shapeEnd = shape.EndPoint;

        return shapeEnd.y == position.y &&
            ((shape.position.x >= position.x && shape.position.x < end.x) || (shapeEnd.x > position.x && shapeEnd.x <= end.x) ||
              (position.x >= shape.position.x && position.x < shapeEnd.x) || (end.x > shape.position.x && end.x <= shapeEnd.x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingTop(RectangleF shape)
    {
        Vector end = EndPoint;
        VectorF shapeEnd = shape.EndPoint;

        return shapeEnd.y == position.y &&
            ((shape.position.x >= position.x && shape.position.x < end.x) || (shapeEnd.x > position.x && shapeEnd.x <= end.x) ||
              (position.x >= shape.position.x && position.x < shapeEnd.x) || (end.x > shape.position.x && end.x <= shapeEnd.x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingBottom(Rectangle shape)
    {
        Vector end = EndPoint, shapeEnd = shape.EndPoint;

        return shape.position.y == end.y &&
            ((shape.position.x >= position.x && shape.position.x < end.x) || (shapeEnd.x > position.x && shapeEnd.x <= end.x) ||
              (position.x >= shape.position.x && position.x < shapeEnd.x) || (end.x > shape.position.x && end.x <= shapeEnd.x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingBottom(RectangleF shape)
    {
        Vector end = EndPoint;
        VectorF shapeEnd = shape.EndPoint;

        return shape.position.y == end.y &&
            ((shape.position.x >= position.x && shape.position.x < end.x) || (shapeEnd.x > position.x && shapeEnd.x <= end.x) ||
              (position.x >= shape.position.x && position.x < shapeEnd.x) || (end.x > shape.position.x && end.x <= shapeEnd.x));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingLeft(Rectangle shape)
    {
        Vector end = EndPoint, shapeEnd = shape.EndPoint;

        return shapeEnd.x == position.x &&
            ((shape.position.y >= position.y && shape.position.y < end.y) || (shapeEnd.y > position.y && shapeEnd.y <= end.y) ||
              (position.y >= shape.position.y && position.y < shapeEnd.y) || (end.y > shape.position.y && end.y <= shapeEnd.y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingLeft(RectangleF shape)
    {
        Vector end = EndPoint;
        VectorF shapeEnd = shape.EndPoint;

        return shapeEnd.x == position.x &&
            ((shape.position.y >= position.y && shape.position.y < end.y) || (shapeEnd.y > position.y && shapeEnd.y <= end.y) ||
              (position.y >= shape.position.y && position.y < shapeEnd.y) || (end.y > shape.position.y && end.y <= shapeEnd.y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingRight(Rectangle shape)
    {
        Vector end = EndPoint, shapeEnd = shape.EndPoint;

        return shape.position.x == end.x &&
            ((shape.position.y >= position.y && shape.position.y < end.y) || (shapeEnd.y > position.y && shapeEnd.y <= end.y) ||
              (position.y >= shape.position.y && position.y < shapeEnd.y) || (end.y > shape.position.y && end.y <= shapeEnd.y));
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TouchingRight(RectangleF shape)
    {
        Vector end = EndPoint;
        VectorF shapeEnd = shape.EndPoint;

        return shape.position.x == end.x &&
            ((shape.position.y >= position.y && shape.position.y < end.y) || (shapeEnd.y > position.y && shapeEnd.y <= end.y) ||
              (position.y >= shape.position.y && position.y < shapeEnd.y) || (end.y > shape.position.y && end.y <= shapeEnd.y));
    }
    #endregion
    #endregion
}
