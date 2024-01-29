using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;

using ProjectFox.CoreEngine.Data;

using static ProjectFox.CoreEngine.Collections.Strings;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    private static void IDataTest<T>(IData<T> iData)
    {
        C.WriteLine("-IData-");

        C.WriteLine(iData.GetType());
        C.WriteLine(typeof(IData<T>));
        C.WriteLine(typeof(T));

        //static methods?

        //C.WriteLine(ConcatHex(false, false, iData.GetBytes()));

        C.WriteLine(iData.ToHexString());
        C.WriteLine(iData.ToHexString(false, true));
        C.WriteLine(iData.ToHexString(true, false));
        C.WriteLine(iData.ToHexString(true, true));

        C.WriteLine(iData.ToBinString());
        C.WriteLine(iData.ToBinString(false, true, '^', '&'));
        C.WriteLine(iData.ToBinString(true, false, '#', '%'));
        C.WriteLine(iData.ToBinString(true, true, '<', '>'));
    }

    private static void IMathTest<T, F>(IMath<T, F> iMath, T bigger, T smaller, F biggerF, F smallerF)
    {
        IDataTest(iMath);

        C.WriteLine("-IMath-");

        C.WriteLine(iMath.GetType());
        C.WriteLine(typeof(IMath<T, F>));
        C.WriteLine(typeof(T));
        C.WriteLine(typeof(F));

        C.WriteLine(iMath.ToString());

        C.WriteLine(iMath.GetHashCode());

        C.WriteLine($"{iMath} == {bigger} => {iMath.Equals(bigger)}");
        C.WriteLine($"{iMath} == {smaller} => {iMath.Equals(smaller)}");
        C.WriteLine($"{iMath} == {biggerF} => {iMath.Equals(biggerF)}");
        C.WriteLine($"{iMath} == {smallerF} => {iMath.Equals(smallerF)}");
        C.WriteLine($"{iMath} == {true} => {iMath.Equals(true)}");

        C.WriteLine(iMath.Abs());

        C.WriteLine(iMath.Between(smaller, bigger));
        C.WriteLine(iMath.BetweenAgainstBounds(smaller, bigger));
        C.WriteLine(iMath.BetweenAgainstMax(smaller, bigger));
        C.WriteLine(iMath.BetweenAgainstMin(smaller, bigger));

        C.WriteLine(iMath.Clamp(smaller, bigger));

        C.WriteLine(iMath.Closest(bigger, smaller));
        C.WriteLine(iMath.Closest(bigger, smaller, bigger));
        C.WriteLine(iMath.ClosestIndex(new T[] { bigger, smaller }));
        C.WriteLine(iMath.ClosestIndex(new T[] { bigger, smaller, bigger }));

        C.WriteLine(iMath.Cube());

        C.WriteLine(iMath.Farthest(bigger, smaller));
        C.WriteLine(iMath.Farthest(bigger, smaller, bigger));
        C.WriteLine(iMath.FarthestIndex(new T[] { bigger, smaller }));
        C.WriteLine(iMath.FarthestIndex(new T[] { bigger, smaller, bigger }));

        C.WriteLine(iMath.IsZero());

        C.WriteLine(iMath.Max(bigger));
        C.WriteLine(iMath.Max(bigger, smaller, bigger));
        //C.WriteLine(iMath.MaxIndex(new T[] { bigger }));
        //C.WriteLine(iMath.MaxIndex(new T[] { bigger, smaller, bigger }));

        C.WriteLine(iMath.Min(bigger));
        C.WriteLine(iMath.Min(bigger, smaller, bigger));
        //C.WriteLine(iMath.MinIndex(new T[] { bigger }));
        //C.WriteLine(iMath.MinIndex(new T[] { bigger, smaller, bigger }));

        C.WriteLine(iMath.Pow(2));
        C.WriteLine(iMath.Pow(3));
        C.WriteLine(iMath.Pow(10));

        C.WriteLine($"{iMath.FindSign()} : {iMath.FindSignInt()}");

        C.WriteLine($"{iMath.FindSign(bigger)} : {iMath.FindSignInt(bigger)}");
        C.WriteLine($"{iMath.FindSign(smaller)} : {iMath.FindSignInt(smaller)}");

        C.WriteLine(iMath.Sqr());
        C.WriteLine(iMath.SqrRoot());

        C.WriteLine(iMath.Wrap(smaller, bigger));

        iMath.MoveToZero(bigger);
        C.WriteLine(iMath);
    }

    private static void IVectorTest<V, t, F>(IVector<V, t, F> iVector, V bigger, V smaller, t biggert, t smallert, F biggerF, F smallerF)
    {
        IMathTest(iVector, bigger, smaller, biggerF, smallerF);

        C.WriteLine("-IVector-");

        C.WriteLine(iVector.GetType());
        C.WriteLine(typeof(IVector<V, t, F>));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));

        C.WriteLine(iVector.Equals(biggert));
        C.WriteLine(iVector.Equals(smallert));

        C.WriteLine(iVector.Between(smallert, biggert));
        C.WriteLine(iVector.BetweenAgainstBounds(smallert, biggert));
        C.WriteLine(iVector.BetweenAgainstMax(smallert, biggert));
        C.WriteLine(iVector.BetweenAgainstMin(smallert, biggert));

        C.WriteLine(iVector.Clamp(smallert, biggert));

        C.WriteLine($"{iVector.Distance(bigger)}^2 = {iVector.DistanceSquared(bigger)}");
        C.WriteLine($"{iVector.Distance(smaller)}^2 = {iVector.DistanceSquared(smaller)}");
        C.WriteLine($"{iVector.DistanceFromZero()}^2 = {iVector.DistanceFromZeroSquared()}");

        C.WriteLine(iVector.Pow(bigger));
        C.WriteLine(iVector.Pow(smaller));

        C.WriteLine(iVector.Wrap(smallert, biggert));

        iVector.MoveToZero(biggert);
        C.WriteLine(iVector);
    }

    private static void IDirectionTest<T>(IDirection<T> iDirection, T bigger, T smaller)
    {
        C.WriteLine("-IDirection-");

        C.WriteLine(iDirection.GetType());
        C.WriteLine(typeof(IDirection<T>));
        C.WriteLine(typeof(T));

        C.WriteLine(iDirection.DirectionFromZero());
        C.WriteLine(iDirection.DirectionToPoint(bigger));
        C.WriteLine(iDirection.DirectionToPoint(smaller));
    }

    private static void IRotate2DTest<S, V, F, Vf>(IRotate2D<S, V, F, Vf> iRotate2D, V bigger, V smaller, Vf biggerF, Vf smallerF, float rotation1, float rotation2)
    {
        C.WriteLine("-IRotate2D-");

        C.WriteLine(iRotate2D.GetType());
        C.WriteLine(typeof(IRotate2D<S, V, F, Vf>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(F));
        C.WriteLine(typeof(Vf));

        C.WriteLine(iRotate2D.AngleFromRotationOrigin());

        C.WriteLine(iRotate2D.Angle(bigger));
        C.WriteLine(iRotate2D.Angle(smaller));

        C.WriteLine(iRotate2D.Angle(biggerF));
        C.WriteLine(iRotate2D.Angle(smallerF));

        C.WriteLine(iRotate2D.Angle(bigger, smaller));
        C.WriteLine(iRotate2D.Angle(smaller, bigger));

        C.WriteLine(iRotate2D.Angle(biggerF, smallerF));
        C.WriteLine(iRotate2D.Angle(smallerF, biggerF));

        C.WriteLine(iRotate2D.Rotate(rotation1));
        C.WriteLine(iRotate2D.Rotate(rotation1, biggerF));

        C.WriteLine(iRotate2D.RotateByRadians(rotation1));
        C.WriteLine(iRotate2D.RotateByRadians(rotation1, biggerF));

        C.WriteLine(iRotate2D.RotateByDegrees(rotation1));
        C.WriteLine(iRotate2D.RotateByDegrees(rotation1, biggerF));

        C.WriteLine(iRotate2D.RotateByRightAngles(0));
        C.WriteLine(iRotate2D.RotateByRightAngles(1));
        C.WriteLine(iRotate2D.RotateByRightAngles(2));
        C.WriteLine(iRotate2D.RotateByRightAngles(3));
        C.WriteLine(iRotate2D.RotateByRightAngles(4));

        C.WriteLine(iRotate2D.RotateByRightAngles(bigger, 1));
        C.WriteLine(iRotate2D.RotateByRightAngles(biggerF, 2));
    }

    private static void IPolytopeTest<V, P>(IPolytope<V, P> iPolytope)
    {
        C.WriteLine("-IPolytope-");

        C.WriteLine(iPolytope.GetType());
        C.WriteLine(typeof(IPolytope<V, P>));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(P));

        C.WriteLine(iPolytope.CenterPoint);
        C.WriteLine(string.Join(',', iPolytope.Points));
        C.WriteLine(string.Join(',', iPolytope.GetPrimitives()));
    }

    private static void IShapeTest<S, V, t, F>(IShape<S, V, t, F> iShape, S bigger, S smaller, V biggerV, V smallerV, t biggert, t smallert, F biggerF, F smallerF)
    {
        IVectorTest(iShape, bigger, smaller, biggerV, smallerV, biggerF, smallerF);

        C.WriteLine("-IShape-");

        C.WriteLine(iShape.GetType());
        C.WriteLine(typeof(IShape<S, V, t, F>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));

        C.WriteLine(iShape.Between(smallert, biggert));
        C.WriteLine(iShape.BetweenAgainstBounds(smallert, biggert));
        C.WriteLine(iShape.BetweenAgainstMax(smallert, biggert));
        C.WriteLine(iShape.BetweenAgainstMin(smallert, biggert));

        C.WriteLine(iShape.Clamp(smallert, biggert));

        C.WriteLine(iShape.Closest(biggerV, smallerV));
        C.WriteLine(iShape.Closest(biggerV, smallerV, biggerV));
        C.WriteLine(iShape.ClosestIndex(new V[] { biggerV, smallerV, biggerV }));

        C.WriteLine($"{iShape.Distance(biggerV)} = {iShape.DistanceSquared(biggerV)}");
        C.WriteLine($"{iShape.Distance(smallerV)} = {iShape.DistanceSquared(smallerV)}");

        C.WriteLine(iShape.Enveloping(biggerV));
        C.WriteLine(iShape.Enveloping(smallerV));
        C.WriteLine(iShape.Enveloping(bigger));
        C.WriteLine(iShape.Enveloping(smaller));

        C.WriteLine(iShape.Farthest(biggerV, smallerV));
        C.WriteLine(iShape.Farthest(biggerV, smallerV, biggerV));
        C.WriteLine(iShape.FarthestIndex(new V[] { biggerV, smallerV, biggerV }));

        C.WriteLine(iShape.IntersectionArea(bigger));
        C.WriteLine(iShape.IntersectionArea(smaller));
        C.WriteLine(iShape.IntersectionArea(biggerF));
        C.WriteLine(iShape.IntersectionArea(smallerF));

        C.WriteLine(iShape.Intersecting(bigger));
        C.WriteLine(iShape.Intersecting(smaller));
        C.WriteLine(iShape.Intersecting(biggerF));
        C.WriteLine(iShape.Intersecting(smallerF));

        C.WriteLine(iShape.Overlapping(biggerV));
        C.WriteLine(iShape.Overlapping(smallerV));
        C.WriteLine(iShape.Overlapping(bigger));
        C.WriteLine(iShape.Overlapping(smaller));
        C.WriteLine(iShape.Overlapping(biggerF));
        C.WriteLine(iShape.Overlapping(smallerF));

        C.WriteLine(iShape.Touching(bigger));
        C.WriteLine(iShape.Touching(smaller));
        C.WriteLine(iShape.Touching(biggerF));
        C.WriteLine(iShape.Touching(smallerF));

        C.WriteLine(iShape.Wrap(smallert, biggert));

        iShape.MoveToZero(biggert);
        C.WriteLine(iShape);
    }

    private static void IShape2DTest<S, V, t, F>(IShape2D<S, V, t, F> iShape2D, S bigger, S smaller, V biggerV, V smallerV, t biggert, t smallert, F biggerF, F smallerF, IPolytope<Vector, Triangle> polytope, IPolytope<VectorF, TriangleF> polytopeF)
    {
        IShapeTest(iShape2D, bigger, smaller, biggerV, smallerV, biggert, smallert, biggerF, smallerF);
        IDirectionTest(iShape2D, biggerV, smallerV);

        C.WriteLine("-IShape2D-");

        C.WriteLine(iShape2D.GetType());
        C.WriteLine(typeof(IShape2D<S, V, t, F>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));
        C.WriteLine(typeof(IPolytope<Vector, Triangle>));
        C.WriteLine(typeof(IPolytope<VectorF, TriangleF>));

        C.WriteLine(iShape2D.DirectionToShape(bigger));
        C.WriteLine(iShape2D.DirectionToShape(smaller));

        C.WriteLine(iShape2D.Enveloping(polytope));
        C.WriteLine(iShape2D.Enveloping(polytopeF));

        C.WriteLine(iShape2D.Enveloping(biggerV));
        C.WriteLine(iShape2D.Enveloping(smallerV));
        C.WriteLine(iShape2D.Enveloping(biggerF));
        C.WriteLine(iShape2D.Enveloping(smallerF));

        C.WriteLine(iShape2D.Equals(polytope));
        C.WriteLine(iShape2D.Equals(polytopeF));

        C.WriteLine(iShape2D.IntersectionArea(polytope));
        C.WriteLine(iShape2D.IntersectionArea(polytopeF));

        C.WriteLine(iShape2D.Intersecting(polytope));
        C.WriteLine(iShape2D.Intersecting(polytopeF));

        C.WriteLine(iShape2D.Overlapping(biggerV));
        C.WriteLine(iShape2D.Overlapping(smallerV));
        C.WriteLine(iShape2D.Overlapping(biggerF));
        C.WriteLine(iShape2D.Overlapping(smallerF));

        C.WriteLine(iShape2D.Overlapping(polytope));
        C.WriteLine(iShape2D.Overlapping(polytopeF));

        C.WriteLine(iShape2D.Touching(polytope));
        C.WriteLine(iShape2D.Touching(polytopeF));
    }

    private static void IShape3DTest<S, V, t, F>(IShape3D<S, V, t, F> iShape3D, S bigger, S smaller, V biggerV, V smallerV, t biggert, t smallert, F biggerF, F smallerF, IPolytope<VectorZ, Tetrahedron> polytope, IPolytope<VectorZF, TetrahedronF> polytopeF)
    {
        IShapeTest(iShape3D, bigger, smaller, biggerV, smallerV, biggert, smallert, biggerF, smallerF);

        C.WriteLine("-IShape3D-");

        C.WriteLine(iShape3D.GetType());
        C.WriteLine(typeof(IShape2D<S, V, t, F>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));
        C.WriteLine(typeof(IPolytope<Vector, Triangle>));
        C.WriteLine(typeof(IPolytope<VectorF, TriangleF>));

        C.WriteLine(iShape3D.Enveloping(biggerV));
        C.WriteLine(iShape3D.Enveloping(smallerV));
        C.WriteLine(iShape3D.Enveloping(biggerF));
        C.WriteLine(iShape3D.Enveloping(smallerF));

        C.WriteLine(iShape3D.Enveloping(polytope));
        C.WriteLine(iShape3D.Enveloping(polytopeF));

        C.WriteLine(iShape3D.Equals(polytope));
        C.WriteLine(iShape3D.Equals(polytopeF));

        C.WriteLine(iShape3D.IntersectionArea(polytope));
        C.WriteLine(iShape3D.IntersectionArea(polytopeF));

        C.WriteLine(iShape3D.Intersecting(polytope));
        C.WriteLine(iShape3D.Intersecting(polytopeF));

        C.WriteLine(iShape3D.Overlapping(biggerV));
        C.WriteLine(iShape3D.Overlapping(smallerV));
        C.WriteLine(iShape3D.Overlapping(biggerF));
        C.WriteLine(iShape3D.Overlapping(smallerF));

        C.WriteLine(iShape3D.Overlapping(polytope));
        C.WriteLine(iShape3D.Overlapping(polytopeF));

        C.WriteLine(iShape3D.Touching(polytope));
        C.WriteLine(iShape3D.Touching(polytopeF));
    }
}
