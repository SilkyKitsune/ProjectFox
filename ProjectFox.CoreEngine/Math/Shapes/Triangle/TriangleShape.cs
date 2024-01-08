namespace ProjectFox.CoreEngine.Math;

public partial struct Triangle
{
    /*
//what if the triangle was already right?
Triangle.intersecting(Vector)
|-----
| Pivot point = min side
|    Ab => c
|    Bc => a
|    CA => b
|//these assign the same thing
| if (ab < bc)
|   min side = ab
|   pivot point = c
| else
|   min side = bc
|   pivot point = a
| if (ca < minside) pivot point = b
|-----
Far point = max side from pivot
A => b || c
B => c || a
C => a || b

Value -= pivot
Far point -= pivot
Midpoint -= pivot
Pivot = (0, 0)

Angle = farpoint.angleorigin()

Value.rotate(angle, pivot)//don't need pivot since it's origin
Far point.Rotate (angle, pivot)
Midpoint.rotate(angle, pivot)

Right triangles =
(0, mid.y, mid.x, -mid.y),
(0, mid.y, mid.x, far.y - mid.y)

Return red.intersecting(value) || blue.intersecting(value)
*/

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

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public Triangle IntersectionArea(Triangle shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public TriangleF IntersectionArea(TriangleF shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public IPolytope<Vector, Triangle> IntersectionArea(IPolytope<Vector, Triangle> shape) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public IPolytope<VectorF, TriangleF> IntersectionArea(IPolytope<VectorF, TriangleF> shape) => default;

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
}