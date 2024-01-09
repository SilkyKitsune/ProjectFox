#if DEBUG
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public interface IShape3D<S, V, t, F> : IShape<S, V, t, F>//, IPolytope<V>
{
    public abstract bool Enveloping(IPolytope<VectorZ, Tetrahedron> shape);
    public abstract bool Enveloping(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract bool Enveloping(VectorZ value);
    public abstract bool Enveloping(VectorZF value);

    public bool Equals(IPolytope<VectorZ, Tetrahedron> shape)
    {
        /*if (shape is S s) Equals(s);

        V[] points = Points;
        VectorZ[] shapePoints = shape.Points;

        if (points.Length != shapePoints.Length) return false;

        for (int i = 0; i < points.Length; i++)
            if (!points[i].Equals(shapePoints[i])) return false;*/
        return true;
    }

    public bool Equals(IPolytope<VectorZF, TetrahedronF> shape)
    {
        /*if (shape is F f) Equals(f);

        V[] points = Points;
        VectorZF[] shapePoints = shape.Points;

        if (points.Length != shapePoints.Length) return false;

        for (int i = 0; i < points.Length; i++)
            if (!points[i].Equals(shapePoints[i])) return false;*/
        return true;
    }

    public abstract IPolytope<VectorZ, Tetrahedron> IntersectionArea(IPolytope<VectorZ, Tetrahedron> shape);
    public abstract IPolytope<VectorZF, TetrahedronF> IntersectionArea(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract bool Intersecting(IPolytope<VectorZ, Tetrahedron> shape);
    public abstract bool Intersecting(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract bool Overlapping(VectorZ vector);
    public abstract bool Overlapping(VectorZF vector);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]//need to update to abstract
    public virtual bool Overlapping(IPolytope<VectorZ, Tetrahedron> shape) => Equals(shape) || Enveloping(shape);//|| Within(shape);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual bool Overlapping(IPolytope<VectorZF, TetrahedronF> shape) => Equals(shape) || Enveloping(shape);//|| Within(shape);

    public abstract bool Touching(IPolytope<VectorZ, Tetrahedron> shape);
    public abstract bool Touching(IPolytope<VectorZF, TetrahedronF> shape);

    //public abstract bool Within(IGenericShape<VectorZ> shape);//redundant?
    //public abstract bool Within(IGenericShape<VectorZF> shape);
}
#endif