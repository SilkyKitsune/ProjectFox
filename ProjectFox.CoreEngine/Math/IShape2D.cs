using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;
//why can't this be a polytope?
public interface IShape2D<S, V, t, F> : IShape<S, V, t, F>, /*IPolytope<V, Triangle>,*/ IDirection<V>, IRotate2D<S, V, F, VectorF>
{
    public abstract Vector.Direction DirectionToShape(S shape);

    //Enveloping(Triangle)?
    public abstract bool Enveloping(IPolytope<Vector, Triangle> shape);
    public abstract bool Enveloping(IPolytope<VectorF, TriangleF> shape);

    public abstract bool Enveloping(Vector value);
    public abstract bool Enveloping(VectorF value);

    public bool Equals(IPolytope<Vector, Triangle> shape)//move to IPolytope?, abstract instead
    {
        /*if (shape is S s) Equals(s);

        V[] points = Points;
        Vector[] shapePoints = shape.Points;

        if (points.Length != shapePoints.Length) return false;

        for (int i = 0; i < points.Length; i++)
            if (!points[i].Equals(shapePoints[i])) return false;*/
        return true;
    }

    public bool Equals(IPolytope<VectorF, Triangle> shape)
    {
        /*if (shape is F f) Equals(f);

        V[] points = Points;
        VectorF[] shapePoints = shape.Points;

        if (points.Length != shapePoints.Length) return false;

        for (int i = 0; i < points.Length; i++)
            if (!points[i].Equals(shapePoints[i])) return false;*/
        return true;
    }

    public abstract IPolytope<Vector, Triangle> IntersectionArea(IPolytope<Vector, Triangle> shape);
    public abstract IPolytope<VectorF, TriangleF> IntersectionArea(IPolytope<VectorF, TriangleF> shape);

    public abstract bool Intersecting(IPolytope<Vector, Triangle> shape);
    public abstract bool Intersecting(IPolytope<VectorF, TriangleF> shape);

    public abstract bool Overlapping(Vector vector);
    public abstract bool Overlapping(VectorF vector);

    public abstract bool Overlapping(IPolytope<Vector, Triangle> shape);
    public abstract bool Overlapping(IPolytope<VectorF, TriangleF> shape);

    public abstract bool Touching(IPolytope<Vector, Triangle> shape);
    public abstract bool Touching(IPolytope<VectorF, TriangleF> shape);

    //public abstract bool Within(IGenericShape<Vector> shape);
    //public abstract bool Within(IGenericShape<VectorF> shape);
}
