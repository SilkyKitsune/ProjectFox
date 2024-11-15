using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Collections;

public sealed class LookupTable<C, T> : ITable<C, T>, ICopy<LookupTable<C, T>>
{
    public LookupTable(int chunkSize = 0x10)
    {
        codes = new(chunkSize);
        values = new(chunkSize);
    }

    private LookupTable(AutoSizedArray<C> codes, AutoSizedArray<T> values)
    {
        this.codes = codes;
        this.values = values;
    }

    private readonly AutoSizedArray<C> codes;
    private readonly AutoSizedArray<T> values;

    public int ChunkSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => codes.ChunkSize;
    }

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => codes.Length;
    }

    public T this[C code]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            int index = codes.IndexOf(code);
            if (index < 0) throw new ArgumentException($"Code not found '{code}'");
            return values[index];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            int index = codes.IndexOf(code);
            if (index < 0) throw new ArgumentException($"Code not found '{code}'");
            values[index] = value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(C code, T value)
    {
        if (codes.Length > 0 && codes.Contains(code))
            throw new Exception($"Already contains code '{code}'");

        codes.Add(code);
        values.Add(value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        codes.Clear();
        values.Clear();
    }

    public string Concat()
    {
        int length = this.codes.Length;
        if (length == 0) return string.Empty;

        C[] codes = this.codes.ToArray();
        T[] values = this.values.ToArray();

        string s = "";
        for (int i = 0; i < length; i++)
            s += $"[{codes[i]}:{values[i]}]";
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ConcatCodes() => codes.Concat();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ConcatValues() => values.Concat();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T value) => values.Contains(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsCode(C code) => codes.Contains(code);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Copy(out LookupTable<C, T> copy)
    {
        this.codes.Copy(out AutoSizedArray<C> codes);
        this.values.Copy(out AutoSizedArray<T> values);
        copy = new(codes, values);
    }

    public void CopyTo(ITable<C, T> table)
    {
        if (table == null) throw new ArgumentNullException(nameof(table));
        if (this.codes.Length == 0) return;

        if (table is LookupTable<C, T> hashTable)
        {
            CopyTo(hashTable);
            return;
        }

        C[] codes = this.codes.ToArray();
        T[] values = this.values.ToArray();

        table.Clear();
        for (int i = 0; i < codes.Length; i++)
            table.Add(codes[i], values[i]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CopyTo(LookupTable<C, T> table)
    {
        if (table == null) throw new ArgumentNullException(nameof(table));
        if (codes.Length == 0) return;

        codes.CopyTo(table.codes);
        values.CopyTo(table.values);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public C GetCode(T value)
    {
        int index = values.IndexOf(value);
        if (index < 0) throw new ArgumentException($"Value not found '{value}'");
        return codes[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public C[] GetCodes() => codes.ToArray();

    public T[] GetMultiple(params C[] codes)
    {
        if (codes.Length == 0) return null;

        T[] array = new T[codes.Length];
        for (int i = 0, j; i < codes.Length; i++)
        {
            j = this.codes.IndexOf(codes[i]);
            if (j > -1) array[i] = values[j];
        }
        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] GetValues() => values.ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T value) => values.IndexOf(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOfCode(C code) => codes.IndexOf(code);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty() => codes.IsEmpty();

    public string Join(char separator)
    {
        int length = this.codes.Length;
        if (length == 0) return string.Empty;

        C[] codes = this.codes.ToArray();
        T[] values = this.values.ToArray();

        string s = "";
        for (int i = 0; i < length; i++)
            s += $"[{codes[i]}:{values[i]}]" + separator;
        return s;
    }

    public string Join(string separator)
    {
        int length = this.codes.Length;
        if (length == 0) return string.Empty;

        C[] codes = this.codes.ToArray();
        T[] values = this.values.ToArray();

        string s = "";
        for (int i = 0; i < length; i++)
            s += $"[{codes[i]}:{values[i]}]" + separator;
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string JoinCodes(char separator) => codes.Join(separator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string JoinCodes(string separator) => codes.Join(separator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string JoinValues(char separator) => values.Join(separator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string JoinValues(string separator) => values.Join(separator);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int LastIndexOf(T value) => values.LastIndexOf(value);

    public bool Remove(C code)
    {
        int index = codes.IndexOf(code);
        if (index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }

    public bool RemoveAt(int index)
    {
        if (index >= codes.Length || index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }

    public bool RemoveValue(T value)
    {
        int index = values.IndexOf(value);
        if (index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }

    public bool TryGet(C code, out T value)
    {
        value = default;

        int index = codes.IndexOf(code);
        if (index < 0) return false;

        value = values[index];
        return true;
    }

    public bool TryGetCode(T value, out C code)
    {
        code = default;

        int index = values.IndexOf(value);
        if (index < 0) return false;

        code = codes[index];
        return true;
    }
}