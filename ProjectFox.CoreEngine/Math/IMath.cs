using ProjectFox.CoreEngine.Data;

namespace ProjectFox.CoreEngine.Math;

/// <summary> An interface for implementing basic math types </summary>
/// <typeparam name="T"> inheriting type </typeparam>
/// <typeparam name="F"> equivalent floating point type, use inheriting type for floating point types </typeparam>
public interface IMath<T, F> : IData<T>
{
    /// <summary> equivalent to the Math.Max method </summary>
    public static abstract T Max(T a, T b);

    /// <summary> equivalent to the Math.Max method </summary>
    public static abstract T Max(params T[] values);

    /// <summary> equivalent to the Math.MaxIndex method </summary>
    public static abstract int MaxIndex(T[] values);

    /// <summary> equivalent to the Math.Min method </summary>
    public static abstract T Min(T a, T b);

    /// <summary> equivalent to the Math.Min method </summary>
    public static abstract T Min(params T[] values);

    /// <summary> equivalent to the Math.MinIndex method </summary>
    public static abstract int MinIndex(T[] values);

    //operators

    /// <summary> equivalent to the Math.Abs method </summary>
    public abstract T Abs();

    /// <summary> equivalent to the Math.Between method </summary>
    public abstract bool Between(T min, T max);

    /// <summary> equivalent to the Math.BetweenAgainstBounds method </summary>
    public abstract bool BetweenAgainstBounds(T min, T max);

    /// <summary> equivalent to the Math.BetweenAgainstMax method </summary>
    public abstract bool BetweenAgainstMax(T min, T max);

    /// <summary> equivalent to the Math.BetweenAgainstMin method </summary>
    public abstract bool BetweenAgainstMin(T min, T max);

    /// <summary> equivalent to the Math.Clamp method </summary>
    public abstract T Clamp(T min, T max);

    /// <summary> equivalent to the Math.Closest method </summary>
    public abstract T Closest(T a, T b);

    /// <summary> equivalent to the Math.Closest method </summary>
    public abstract T Closest(params T[] values);

    /// <summary> equivalent to the Math.ClosestIndex method </summary>
    public abstract int ClosestIndex(T[] values);

    /// <summary> equivalent to the Math.Cube method </summary>
    public abstract T Cube();

    public bool Equals(object obj)//will this work?, test
    {
        if (obj is T t) return Equals(t);
        if (obj is F f) return Equals(f);
        return false;
    }

    public abstract bool Equals(T value);

    public abstract bool Equals(F value);

    /// <summary> equivalent to the Math.Farthest method </summary>
    public abstract T Farthest(T a, T b);

    /// <summary> equivalent to the Math.Farthest method </summary>
    public abstract T Farthest(params T[] values);

    /// <summary> equivalent to the Math.FarthestIndex method </summary>
    public abstract int FarthestIndex(T[] values);

    /// <summary> if the value is equal to zero </summary>
    public abstract bool IsZero();

    public abstract void MoveToZero(T amount);

    /// <summary> equivalent to the Math.Pow method </summary>
    public abstract T Pow(int exp);
    //public abstract F Pow(float exp);
    //public abstract F Root(int root);
    //public abstract F Root(float root);

    public abstract Math.Sign FindSign();
    public abstract int FindSignInt();

    public abstract Math.Sign FindSign(T reference);
    public abstract int FindSignInt(T reference);

    /// <summary> equivalent to the Math.Sqr method </summary>
    public abstract T Sqr();

    /// <summary> equivalent to the Math.SqrRoot method </summary>
    public abstract F SqrRoot();

    /// <summary> equivalent to the Math.Wrap method </summary>
    public abstract T Wrap(T min, T max);
}