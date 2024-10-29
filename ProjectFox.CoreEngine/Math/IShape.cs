using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public interface IShape_ { }

public interface IShape<S, V, t, F/*, I, If*/, B> : IVector<S, V, F>, IShape_
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

    public abstract bool Enveloping(S shape);

    public abstract bool Enveloping(F shape);

    /// <summary> equivalent to the Math.Farthest method </summary>
    public abstract V Farthest(V a, V b);

    /// <summary> equivalent to the Math.Farthest method </summary>
    public abstract V Farthest(params V[] values);

    /// <summary> equivalent to the Math.FarthestIndex method </summary>
    public abstract int FarthestIndex(V[] values);

    public abstract /*I*/IShape_/*S*/ IntersectionShape(S shape);//this won't work
    //should intersection type be a generic?
    public abstract /*If*/IShape_/*F*/ IntersectionShape(F shape);//ishape_ overload?

    public abstract bool Intersecting(S shape);

    public abstract bool Intersecting(F shape);

    /// <summary> equivalent to the Math.MoveToZero method </summary>
    public abstract void MoveToZero(t amount);

    public abstract bool Overlapping(V vector);

    public abstract bool Overlapping(S shape);
    
    public abstract bool Overlapping(F shape);

    public abstract bool Touching(S shape);

    public abstract bool Touching(F shape);

    //public abstract bool Within(S shape);//redundant?

    /// <summary> equivalent to the Math.Wrap method </summary>
    public abstract S Wrap(t min, t max);
}
