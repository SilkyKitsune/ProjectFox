namespace ProjectFox.CoreEngine.Collections;

/// <summary></summary>
/// <typeparam name="T"> Inheriting type </typeparam>
public interface ICopy<T>
{
    /// <summary></summary>
    /// <returns></returns>
    public abstract T Copy();
}
