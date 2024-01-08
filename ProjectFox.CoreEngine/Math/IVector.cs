namespace ProjectFox.CoreEngine.Math;

/// <summary> An interface for implementing vector types </summary>
/// <typeparam name="V"> inheriting type </typeparam>
/// <typeparam name="t"> member type for vectors (i.e. int, float, etc) </typeparam>
/// <typeparam name="F"> equivalent floating point type, use inheriting type for floating point types </typeparam>
public interface IVector<V, t, F> : IMath<V, F>
{
    /// <summary> equivalent to the Math.Between method </summary>
    public abstract bool Between(t min, t max);

    /// <summary> equivalent to the Math.BetweenAgainstBounds method </summary>
    public abstract bool BetweenAgainstBounds(t min, t max);

    /// <summary> equivalent to the Math.BetweenAgainstMax method </summary>
    public abstract bool BetweenAgainstMax(t min, t max);

    /// <summary> equivalent to the Math.BetweenAgainstMin method </summary>
    public abstract bool BetweenAgainstMin(t min, t max);

    /// <summary> equivalent to the Math.Clamp method </summary>
    public abstract V Clamp(t min, t max);

    /// <summary> for finding distance between vectors </summary>
    public abstract float Distance(V value);

    public abstract float DistanceFromZero();

    public abstract float DistanceFromZeroSquared();

    public abstract float DistanceSquared(V value);

    public abstract bool Equals(t value);

    /// <summary> equivalent to the Math.MoveToZero method </summary>
    public abstract void MoveToZero(t amount);

    /// <summary> equivalent to the Math.Pow method </summary>
    public abstract V Pow(V exp);

    /// <summary> equivalent to the Math.Wrap method </summary>
    public abstract V Wrap(t min, t max);
}
