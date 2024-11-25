namespace ProjectFox.CoreEngine.Math;

public interface IShape_ { }

public interface IShape<S, Sf, V, Va, P, Pa, B, Bf, t> : IVector<S, V, Sf>, IShape_
{
    public abstract B Bounds { get; }

    /// <summary> equivalent to the Math.Between method </summary>
    public abstract bool Between(t min, t max);

    /// <summary> equivalent to the Math.BetweenAgainstBounds method </summary>
    public abstract bool BetweenAgainstBounds(t min, t max);

    /// <summary> equivalent to the Math.BetweenAgainstMax method </summary>
    public abstract bool BetweenAgainstMax(t min, t max);

    /// <summary> equivalent to the Math.BetweenAgainstMin method </summary>
    public abstract bool BetweenAgainstMin(t min, t max);

    /// <summary> equivalent to the Math.Clamp method </summary>
    public abstract S Clamp(t min, t max);

    /// <summary> equivalent to the Math.Closest method </summary>
    public abstract V Closest(V a, V b);

    /// <summary> equivalent to the Math.Closest method </summary>
    public abstract V Closest(params V[] values);

    /// <summary> equivalent to the Math.ClosestIndex method </summary>
    public abstract int ClosestIndex(V[] values);

    public abstract float Distance(V value);

    public abstract float DistanceSquared(V value);

    public abstract bool Equals(t value);

    public abstract bool Enveloping(V value);

    public abstract bool Enveloping(Va value);

    public abstract bool Enveloping(S shape);

    public abstract bool Enveloping(Sf shape);

    public abstract bool Enveloping(P shape);

    public abstract bool Enveloping(Pa shape);

    public abstract bool Enveloping(IPolytope<V, P> shape);

    public abstract bool Enveloping(IPolytope<Va, Pa> shape);

    /// <summary> equivalent to the Math.Farthest method </summary>
    public abstract V Farthest(V a, V b);

    /// <summary> equivalent to the Math.Farthest method </summary>
    public abstract V Farthest(params V[] values);

    /// <summary> equivalent to the Math.FarthestIndex method </summary>
    public abstract int FarthestIndex(V[] values);

    public abstract B IntersectionBounds(S shape);

    public abstract Bf IntersectionBounds(Sf shape);

    public abstract B IntersectionBounds(P shape);

    public abstract Bf IntersectionBounds(Pa shape);

    public abstract B IntersectionBounds(IPolytope<V, P> shape);

    public abstract Bf IntersectionBounds(IPolytope<Va, Pa> shape);

    public abstract /*I*/IShape_ IntersectionShape(S shape);//this won't work, why not?
    
    public abstract /*If*/IShape_ IntersectionShape(Sf shape);

    //public abstract /*?*/IShape_ IntersectionShape(IShape_ shape); ?

    public abstract bool Intersecting(S shape);

    public abstract bool Intersecting(Sf shape);

    public abstract bool Intersecting(P shape);

    public abstract bool Intersecting(Pa shape);

    public abstract bool Intersecting(IPolytope<V, P> shape);

    public abstract bool Intersecting(IPolytope<Va, Pa> shape);

    /// <summary> equivalent to the Math.MoveToZero method </summary>
    public abstract void MoveToZero(t amount);

    public abstract bool Overlapping(V vector);

    public abstract bool Overlapping(Va vector);

    public abstract bool Overlapping(S shape);
    
    public abstract bool Overlapping(Sf shape);

    public abstract bool Overlapping(P shape);

    public abstract bool Overlapping(Pa shape);

    public abstract bool Overlapping(IPolytope<V, P> shape);

    public abstract bool Overlapping(IPolytope<Va, Pa> shape);

    public abstract bool Touching(S shape);

    public abstract bool Touching(Sf shape);

    public abstract bool Touching(P shape);

    public abstract bool Touching(Pa shape);

    public abstract bool Touching(IPolytope<V, P> shape);

    public abstract bool Touching(IPolytope<Va, Pa> shape);

    //public abstract bool Within(S shape);//redundant?

    /// <summary> equivalent to the Math.Wrap method </summary>
    public abstract S Wrap(t min, t max);
}