namespace ProjectFox.CoreEngine.Collections;

public interface IHashTable<H, T>
{
    public abstract T this[H code] { get; set; }

    public abstract int Length { get; }

    public abstract void Add(H code, T value);
    public abstract void Clear();
    public abstract string Concat();
    public abstract string ConcatCodes();
    public abstract string ConcatValues();
    public abstract bool Contains(T value);
    public abstract bool ContainsCode(H code);
    public abstract void CopyTo(IHashTable<H, T> table);
    public abstract H GetCode(T value);
    public abstract H[] GetCodes();
    public abstract T[] GetMultiple(params H[] codes);
    public abstract T[] GetValues();
    public abstract int IndexOf(T value);
    public abstract int IndexOfCode(H code);
    public abstract bool IsEmpty();
    public abstract string Join(char separator);
    public abstract string Join(string separator);
    public abstract string JoinCodes(char separator);
    public abstract string JoinCodes(string separator);
    public abstract string JoinValues(char separator);
    public abstract string JoinValues(string separator);
    public abstract int LastIndexOf(T value);
    public abstract bool Remove(H code);
    public abstract bool RemoveAt(int index);
    public abstract bool RemoveValue(T value);
    public abstract bool TryGet(H code, out T value);
    public abstract bool TryGetCode(T value, out H code);
}
