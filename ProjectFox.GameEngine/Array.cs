using System;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine;

internal sealed class Array<T> : ICollection<T>
{
    private static readonly NameID ArrayName = new("Collctn", 0);

    internal Array(int chunkSize)
    {
        if (chunkSize <= 0)
            throw new ArgumentException($"{nameof(chunkSize)}={chunkSize} must be greater than zero!");

        this.chunkSize = chunkSize;
        length = 0;
        elements = new T[chunkSize];
    }

    private readonly int chunkSize;
    internal int length;
    internal T[] elements;

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => length;
    }

    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (index >= length || index < 0) ?
            Engine.SendError<T>(ErrorCodes.BadArgument, ArrayName, nameof(index),
                $"Invalid index in ICollection<{typeof(T)}>")
            : elements[index];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value == null)
            {
                Engine.SendError(ErrorCodes.NullArgument, ArrayName, nameof(value),
                    "This type does not allow null elements");
                return;
            }
            if (index >= length || index < 0)
            {
                Engine.SendError(ErrorCodes.BadArgument, ArrayName, nameof(index),
                    $"Invalid index in ICollection<{typeof(T)}>");
                return;
            }
            elements[index] = value;
        }
    }

    public T[] this[int startIndex, int endIndex]
    {
        get => Engine.SendError<T[]>(ErrorCodes.NotImplemented, ArrayName, null,
            "ICollection.this[,] is not implemented for this type");
        set => Engine.SendError(ErrorCodes.NotImplemented, ArrayName, null,
            "ICollection.this[,] is not implemented for this type");
    }

    public void Add(T value)
    {
        if (value == null)
        {
            Engine.SendError(ErrorCodes.NullArgument, ArrayName, nameof(value),
                $"This type does not allow null elements, ICollection<{typeof(T)}>");
            return;
        }

        if (length == elements.Length)
        {
            T[] array = new T[length + chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
        elements[length++] = value;
    }

    internal void AddDirect(T value)
    {
        if (length == elements.Length)
        {
            T[] array = new T[length + chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
        elements[length++] = value;
    }

    /*internal ErrorCodes AddInternal(T value)
    {
        if (value == null) return ErrorCodes.NullArgument;

        if (length == elements.Length)
        {
            T[] array = new T[length + chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
        elements[length++] = value;

        return ErrorCodes.NotImplemented;//no error
    }*/
    
    public void Add(params T[] values)
    {
        if (values.Length == 0) return;

        #region temp
        Array<T> newValues = new(values.Length);
        foreach (T value in values)
        {
            if (value == null)
                Engine.SendError(ErrorCodes.NullArgument, ArrayName, nameof(value),
                    $"This type does not allow null elements, ICollection<{typeof(T)}>");
            else newValues.AddDirect(value);
        }
        values = newValues.ToArray();
        #endregion

        int newLength = length + values.Length;
        if (newLength >= elements.Length)
        {
            T[] array = new T[newLength / chunkSize + 1 * chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
        for (int i = 0; i < values.Length; i++) elements[length++] = values[i];
        length = newLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        length = 0;
        elements = new T[chunkSize];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Concat()
    {
        if (length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < length; i++)
            s += elements[i].ToString();
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T value)
    {
        for (int i = 0; i < length; i++)
            if (elements[i].Equals(value)) return true;
        return false;
    }

    public void CopyTo(ICollection<T> collection)
    {
        if (collection == null)
        {
            Engine.SendError(ErrorCodes.NullArgument, ArrayName, nameof(collection),
                $"{ErrorCodes.NullArgument} in ICollection<{typeof(T)}>");
            return;
        }
        if (length == 0) return;

        collection.Clear();
        collection.Add(elements[0..length]);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T FromLastIndex(int index) => index > length || index <= 0 ?
        Engine.SendError<T>(ErrorCodes.BadArgument, ArrayName, nameof(index),
            $"Invalid index in ICollection<{typeof(T)}>")
        : elements[length - index];

    public T[] GetMultiple(params int[] indices)
    {
        if (indices.Length == 0) return null;

        T[] array = new T[indices.Length];
        for (int i = 0; i < indices.Length; i++)
            if (indices[i] >= 0 && indices[i] < length)
                array[i] = elements[indices[i]];
        return array;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int IndexOf(T value)
    {
        for (int i = 0; i < length; i++)
            if (elements[i].Equals(value)) return i;
        return -1;
    }

    public void Insert(int index, T value)
    {
        if (value == null)
        {
            Engine.SendError(ErrorCodes.NullArgument, ArrayName, nameof(value),
                $"This type does not allow null elements, ICollection<{typeof(T)}>");
            return;
        }

        if (index < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, ArrayName, nameof(index),
                $"Invalid index in ICollection<{typeof(T)}>");
            return;
        }

        if (index >= length)
        {
            Add(value);
            return;
        }

        if (length == elements.Length)
        {
            T[] array = new T[length + chunkSize];
            for (int i = 0; i < index; i++) array[i] = elements[i];
            for (int i = length; i > index;) array[i] = elements[--i];
            elements = array;
        }
        else for (int i = length; i > index;) elements[i] = elements[--i];

        elements[index] = value;
        length++;
    }

    public void Insert(int index, params T[] values) =>
        Engine.SendError(ErrorCodes.NotImplemented, ArrayName, null,
            "ICollection.Insert() is not implemented for this type");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty() => length == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Join(char separator)
    {
        if (length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < length; i++)
            s += elements[i].ToString() + separator;
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Join(string separator)
    {
        if (length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < length; i++)
            s += elements[i].ToString() + separator;
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public int LastIndexOf(T value)
    {
        for (int i = length - 1; i > -1; i--)
            if (elements[i].Equals(value)) return i;
        return -1;
    }

    public bool Move(int prevIndex, int newIndex)
    {
        if (prevIndex >= length || prevIndex < 0 || newIndex < 0) return false;

        if (prevIndex == newIndex) return true;

        T value = elements[prevIndex];
        RemoveAt(prevIndex);
        Insert(newIndex, value);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(T value) => RemoveAt(IndexOf(value));

    public bool RemoveAt(int index)
    {
        if (index >= length || index < 0) return false;

        int lastIndex = length - 1;
        while (index < lastIndex) elements[index] = elements[++index];
        length--;

        if (elements.Length - length >= chunkSize * 2)
        {
            T[] array = new T[length / chunkSize + 1 * chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }

        return true;
    }

    public bool RemoveRange(int startIndex, int endIndex) =>
        Engine.SendError<bool>(ErrorCodes.NotImplemented, ArrayName, null,
            "ICollection.RemoveRange() is not implemented for this type");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ToArray() => length == 0 ? new T[0] : elements[0..length];
}

internal sealed class HashArray<T> : IHashTable<NameID, T>
{
    private static readonly NameID HashArrayName = new("HshTble", 0);

    internal HashArray(int chunkSize)
    {
        codes = new(chunkSize);
        values = new(chunkSize);
    }

    internal readonly Array<NameID> codes;
    internal readonly Array<T> values;

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => codes.length;
    }

    public T this[NameID code]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            int index = codes.IndexOf(code);
            if (index < 0) return Engine.SendError<T>(
                ErrorCodes.BadArgument, HashArrayName, nameof(code),
                $"'{code}' could not be found in IHashTable<{typeof(T)}>");
            return values.elements[index];
        }
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value == null)
            {
                Engine.SendError(ErrorCodes.NullArgument, HashArrayName, nameof(value),
                    $"This type does not allow null elements, IHashTable<{typeof(NameID)}, {typeof(T)}>");
                return;
            }
            int index = codes.IndexOf(code);
            if (index < 0)
            {
                Engine.SendError(ErrorCodes.BadArgument, HashArrayName, nameof(code),
                    $"'{code}' could not be found in IHashTable<{typeof(T)}>");
                return;
            }
            values.elements[index] = value;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Add(NameID code, T value)
    {
        if (value == null)
        {
            Engine.SendError(ErrorCodes.NullArgument, HashArrayName, nameof(value),
                $"This type does not allow null elements, IHashTable<{typeof(NameID)}, {typeof(T)}>");
            return;
        }

        if (codes.length > 0 && codes.Contains(code))
        {
            Engine.SendError(ErrorCodes.BadArgument, HashArrayName, nameof(code),
                $"IHashTable<{typeof(T)}> already contains code '{code}'");
            return;
        }

        codes.Add(code);
        values.Add(value);
    }

    internal void AddDirect(NameID code, T value)
    {
        codes.Add(code);
        values.Add(value);
    }

    /*internal ErrorCodes AddInternal(NameID code, T value)
    {
        if (value == null) return ErrorCodes.NullArgument;
        if (codes.Contains(code)) return ErrorCodes.BadArgument;
        codes.AddInternal(code);
        values.AddInternal(value);
        return ErrorCodes.NotImplemented;//no error
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear()
    {
        codes.Clear();
        values.Clear();
    }

    public string Concat()
    {
        if (codes.length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < codes.length; i++)
            s += $"[{codes.elements[i]}:{values.elements[i]}]";
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ConcatCodes() => codes.Concat();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ConcatValues() => values.Concat();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Contains(T value) => values.Contains(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool ContainsCode(NameID code) => codes.Contains(code);

    public void CopyTo(IHashTable<NameID, T> table) =>
        Engine.SendError(ErrorCodes.NotImplemented, HashArrayName, null,
            "IHashTable.CopyTo() is not implemented for this type");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public NameID[] GetCodes() => codes.ToArray();

    public T[] GetMultiple(params NameID[] codes)
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
    public int IndexOf(NameID code) => codes.IndexOf(code);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty() => codes.length == 0;

    public string Join(char separator)
    {
        if (codes.length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < codes.length; i++)
            s += $"[{codes.elements[i]}:{values.elements[i]}]" + separator;
        return s;
    }

    public string Join(string separator)
    {
        if (codes.length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < codes.length; i++)
            s += $"[{codes.elements[i]}:{values.elements[i]}]" + separator;
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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(T value)
    {
        int index = values.IndexOf(value);
        if (index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool Remove(NameID code)
    {
        int index = codes.IndexOf(code);
        if (index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveAt(int index)
    {
        if (index >= codes.length || index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }
}
