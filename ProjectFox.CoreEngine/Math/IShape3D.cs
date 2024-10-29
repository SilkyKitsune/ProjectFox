#if DEBUG
namespace ProjectFox.CoreEngine.Math;

public interface IShape3D<S, V, t, F, B> : IShape<S, V, t, F, B>
{//add triangle overloads
    public abstract bool Enveloping(VectorZ value);

    public abstract bool Enveloping(VectorZF value);

    public abstract bool Enveloping(IPolytope<VectorZ, Tetrahedron> shape);

    public abstract bool Enveloping(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract Rectangle IntersectionVolume(S shape);//should this always be float?

    public abstract RectangleF IntersectionVolume(F shape);//3d rectangles

    public abstract Rectangle/*IPolytope<VectorZ, Tetrahedron>*/ IntersectionVolume(IPolytope<VectorZ, Tetrahedron> shape);

    public abstract RectangleF/*IPolytope<VectorZF, TetrahedronF>*/ IntersectionVolume(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract bool Intersecting(IPolytope<VectorZ, Tetrahedron> shape);

    public abstract bool Intersecting(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract bool Overlapping(VectorZ vector);

    public abstract bool Overlapping(VectorZF vector);

    public abstract bool Overlapping(IPolytope<VectorZ, Tetrahedron> shape);

    public abstract bool Overlapping(IPolytope<VectorZF, TetrahedronF> shape);

    public abstract bool Touching(IPolytope<VectorZ, Tetrahedron> shape);

    public abstract bool Touching(IPolytope<VectorZF, TetrahedronF> shape);
}
#endif