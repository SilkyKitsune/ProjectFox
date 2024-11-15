namespace ProjectFox.CoreEngine.Collections;

public interface ITable<C, T>
{
    public abstract T this[C code] { get; set; }

    public abstract int Length { get; }

    public abstract void Add(C code, T value);
    public abstract void Clear();
    public abstract string Concat();
    public abstract string ConcatCodes();
    public abstract string ConcatValues();
    public abstract bool Contains(T value);
    public abstract bool ContainsCode(C code);
    public abstract void CopyTo(ITable<C, T> table);
    public abstract C GetCode(T value);
    public abstract C[] GetCodes();
    public abstract T[] GetMultiple(params C[] codes);
    public abstract T[] GetValues();
    public abstract int IndexOf(T value);
    public abstract int IndexOfCode(C code);
    public abstract bool IsEmpty();
    public abstract string Join(char separator);
    public abstract string Join(string separator);
    public abstract string JoinCodes(char separator);
    public abstract string JoinCodes(string separator);
    public abstract string JoinValues(char separator);
    public abstract string JoinValues(string separator);
    public abstract int LastIndexOf(T value);
    public abstract bool Remove(C code);
    public abstract bool RemoveAt(int index);
    public abstract bool RemoveValue(T value);
    public abstract bool TryGet(C code, out T value);
    public abstract bool TryGetCode(T value, out C code);
}