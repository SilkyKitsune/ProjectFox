namespace ProjectFox.CoreEngine.Math;

public interface IShape2D<S, V, t, F, B> : IShape<S, V, t, F, B>, IDirection<V>
{
    public abstract Vector.Direction DirectionToShape(S shape);

    public abstract bool Enveloping(Vector value);

    public abstract bool Enveloping(VectorF value);

    //public abstract bool Enveloping(Triangle shape);

    //public abstract bool Enveloping(TriangleF shape);

    public abstract bool Enveloping(IPolytope<Vector, Triangle> shape);

    public abstract bool Enveloping(IPolytope<VectorF, TriangleF> shape);

    public abstract Rectangle IntersectionArea(S shape);//should this always be float?
    //these could be B return and moved to IShape
    public abstract RectangleF IntersectionAreaF(F shape);//temp name

    //public abstract B IntersectionArea(Triangle shape);

    //public abstract B IntersectionArea(TriangleF shape);

    public abstract Rectangle/*IPolytope<Vector, Triangle>*/ IntersectionArea(IPolytope<Vector, Triangle> shape);

    public abstract RectangleF/*IPolytope<VectorF, TriangleF>*/ IntersectionArea(IPolytope<VectorF, TriangleF> shape);

    //public abstract bool Intersecting(Triangle shape);

    //public abstract bool Intersecting(TriangleF shape);

    public abstract bool Intersecting(IPolytope<Vector, Triangle> shape);

    public abstract bool Intersecting(IPolytope<VectorF, TriangleF> shape);

    public abstract bool Overlapping(Vector vector);

    public abstract bool Overlapping(VectorF vector);

    //public abstract bool Overlapping(Triangle shape);

    //public abstract bool Overlapping(TriangleF shape);

    public abstract bool Overlapping(IPolytope<Vector, Triangle> shape);

    public abstract bool Overlapping(IPolytope<VectorF, TriangleF> shape);

    //public abstract bool Touching(Triangle shape);

    //public abstract bool Touching(TriangleF shape);

    public abstract bool Touching(IPolytope<Vector, Triangle> shape);

    public abstract bool Touching(IPolytope<VectorF, TriangleF> shape);
}