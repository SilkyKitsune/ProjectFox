namespace ProjectFox.CoreEngine.Collections;

/// <summary></summary>
/// <typeparam name="T"> Inheriting type </typeparam>
public interface ICopy<T>
{
    public abstract void DeepCopy(out T copy);

    public abstract void ShallowCopy(out T copy);
}
