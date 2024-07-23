namespace ProjectFox.CoreEngine.Collections;

/// <summary></summary>
/// <typeparam name="T"> Inheriting type </typeparam>
public interface ICopy<T>
{
    /// <summary></summary>
    /// <param name="copy"></param>
    public abstract void Copy(out T copy);
}
