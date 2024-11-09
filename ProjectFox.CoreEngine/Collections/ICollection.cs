namespace ProjectFox.CoreEngine.Collections;

public interface ICollection<T>
{
    public abstract T this[int index] { get; set; }

    public abstract int Length { get; }

    public abstract void Add(T value);
    public abstract void Add(params T[] values);
    public abstract void AddConcat(T[][] arrays);
    public abstract void Clear();
    public abstract string Concat();
    public abstract bool Contains(T value);
    public abstract void CopyTo(ICollection<T> collection);
    public abstract T FromLastIndex(int index);
    public abstract T[] GetMultiple(params int[] indices);//shorten name?
    public abstract T[] GetRange(int index, int length);//shorten name?
    public abstract int IndexOf(T value);
    public abstract void Insert(int index, T value);
    public abstract void Insert(int index, params T[] values);
    public abstract bool IsEmpty();
    public abstract string Join(char separator);
    public abstract string Join(string separator);
    public abstract int LastIndexOf(T value);
    public abstract bool Move(int prevIndex, int newIndex);
    public abstract bool Remove(T value);
    public abstract bool RemoveAt(int index);
    public abstract int RemoveRange(int index, int length);//shorten name?
    public abstract int ReplaceRange(int index, int length, params T[] values);//shorten name?
    public abstract T[] ToArray();
}