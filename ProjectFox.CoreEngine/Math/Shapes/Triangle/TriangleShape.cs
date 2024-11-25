namespace ProjectFox.CoreEngine.Math;

public partial struct Triangle
{
    #region Enveloping
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Enveloping(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Enveloping(TriangleF shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Enveloping(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Enveloping(IPolytope<VectorF, TriangleF> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Enveloping(Vector value)
    {
        /*
intersecting
  (A + B / (bc + ca - ab),
  B + C / (ab + ca - bc),
  C + A / (ab + bc - ca)) 

  If (pa + pb > ab && pc > acceptable distance) not intersecting
        */
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Enveloping(VectorF value) => default;
    #endregion

    #region Intersection
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle IntersectionBounds(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF IntersectionBounds(TriangleF shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Rectangle IntersectionBounds(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public RectangleF IntersectionBounds(IPolytope<VectorF, TriangleF> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public IShape_ IntersectionShape(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public IShape_ IntersectionShape(TriangleF shape) => default;
    #endregion

    #region Intersecting
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Intersecting(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Intersecting(TriangleF shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Intersecting(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Intersecting(IPolytope<VectorF, TriangleF> shape) => default;
    #endregion

    #region Overlapping
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Overlapping(Vector vector)
    {
        /*
overlapping(vector)
  Ab = a->b, bc = b->c, CA = c->a

  If (! axis aligned)
    B.Angle(c, a)
    Ab >= CA ? C.rotate(angle origin) : B.Rotate(angle origin)

  If (x aligned)
    B.rotateright(1)
    C.rotateright(1)

  If (right)
    xperc = x - smallx / bigx - smallx
    if (bool = xperc <= 1) return betweenagainst(y, 0, bigy - smally * xperc)
    return false
  Else
    Split
        */
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Overlapping(VectorF vector) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Overlapping(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Overlapping(IPolytope<VectorF, TriangleF> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Overlapping(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Overlapping(TriangleF shape) => default;
    #endregion

    #region Touching
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(TriangleF shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public bool Touching(IPolytope<VectorF, TriangleF> shape) => default;
    #endregion
}