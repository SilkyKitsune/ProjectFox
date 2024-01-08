namespace ProjectFox.CoreEngine.Math;

/// <summary> Interface for implementing types consisting of real or imaginary vectors </summary>
/// <typeparam name="V"> Vector type </typeparam>
/// <typeparam name="P"> Primitive type </typeparam>
public interface IPolytope<V, P>
{
    /// <summary> the vector at the center of the shape </summary>
    public abstract V CenterPoint { get; }

    /// <summary> all of the vectors that make up the shape </summary>
    public abstract V[] Points { get; }

    public abstract P[] GetPrimitives();//virtual? can't be virtual because p is undefined
    /*
    GetTriangles() abstract or virtual?
    Triangle count = points.length - 2

    Points[0].closest(points[1..^1])
    Arrange points by distance from points[0]
    Triangle (p[0], p1, p2)
    Triangle (p1, p2, p3)
    Triangle (p2, p3, p4)
    Etc...
     */
}
