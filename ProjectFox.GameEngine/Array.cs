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
                $"Invalid index in ICollection<{typeof(T)}>") :
            elements[index];
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
        //need to better integrate this if possible
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
            T[] array = new T[(newLength / chunkSize + 1) * chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
        for (int i = 0; i < values.Length; i++) elements[length++] = values[i];
        length = newLength;
    }

    [Obsolete] internal void AddLength(int addedLength)
    {
        int newLength = length + addedLength;
        if (newLength >= elements.Length)
        {
            T[] array = new T[(newLength / chunkSize + 1) * chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
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
        for (int i = 0; i < length; i++) s += elements[i].ToString();
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
            $"Invalid index in ICollection<{typeof(T)}>") : elements[length - index];

    public T[] GetMultiple(params int[] indices)
    {
        if (indices.Length == 0) return null;

        T[] array = new T[indices.Length];
        for (int i = 0; i < indices.Length; i++)
            if (indices[i] >= 0 && indices[i] < length) array[i] = elements[indices[i]];
        return array;
    }

    public T[] GetRange(int index, int length)
    {
        if (index < 0)
            return Engine.SendError<T[]>(ErrorCodes.BadArgument, ArrayName, nameof(index),
                $"Invalid index in ICollection<{typeof(T)}>");
        if (length < 0)
            return Engine.SendError<T[]>(ErrorCodes.BadArgument, ArrayName, nameof(length),
                $"Invalid length in ICollection<{typeof(T)}>");

        if (index >= this.length || length == 0) return null;

        int lastIndex = index + length;
        if (lastIndex >= this.length) lastIndex = this.length;

        return elements[index..lastIndex];
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

    public void Insert(int index, params T[] values)
    {
        if (index >= length)
        {
            Add(values);
            return;
        }

        #region temp
        //need to better integrate this if possible
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
            T[] array = new T[(newLength / chunkSize + 1) * chunkSize];

            for (int i = 0; i < index; i++) array[i] = elements[i];
            for (int i = newLength - 1, j = length - 1, k = index - 1; j > k;) array[i--] = elements[j--];

            elements = array;
        }
        else for (int i = newLength - 1, j = length - 1, k = index - 1; j > k;) elements[i--] = elements[j--];

        for (int i = 0; i < values.Length; i++) elements[index++] = values[i];
        length = newLength;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsEmpty() => length == 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Join(char separator)
    {
        if (length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < length; i++) s += elements[i].ToString() + separator;
        return s;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string Join(string separator)
    {
        if (length == 0) return string.Empty;

        string s = "";
        for (int i = 0; i < length; i++) s += elements[i].ToString() + separator;
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

    public int RemoveRange(int index, int length)
    {
        if (index < 0)
            return Engine.SendError<int>(ErrorCodes.BadArgument, ArrayName, nameof(index),
                $"Invalid index in ICollection<{typeof(T)}>");
        if (length < 0)
            return Engine.SendError<int>(ErrorCodes.BadArgument, ArrayName, nameof(length),
                $"Invalid length in ICollection<{typeof(T)}>");

        if (index >= this.length || length == 0) return 0;

        if (index == 0 && length == this.length)
        {
            Clear();
            return length;
        }

        int toEnd = this.length - index;
        if (toEnd < length) length = toEnd;

        int newLength = this.length - length, lastIndex = index + length;
        T[] array = new T[(newLength / chunkSize + 1) * chunkSize];

        for (int i = 0; i < index; i++) array[i] = elements[i];
        while (lastIndex < this.length) array[index++] = elements[lastIndex++];

        elements = array;
        this.length = newLength;

        return length;
    }

    public int ReplaceRange(int index, int length, params T[] values)
    {
        if (index < 0)
            return Engine.SendError<int>(ErrorCodes.BadArgument, ArrayName, nameof(index),
                $"Invalid index in ICollection<{typeof(T)}>");
        if (length < 0)
            return Engine.SendError<int>(ErrorCodes.BadArgument, ArrayName, nameof(length),
                $"Invalid length in ICollection<{typeof(T)}>");

        #region temp
        //need to better integrate this if possible
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

        if (values.Length == 0) return 0;

        if (length == 0) length = values.Length;

        if (index == 0 && length == this.length)
        {
            Clear();
            Add(values);
            return length;
        }

        if (index >= this.length)
        {
            Add(values);
            return 0;
        }

        int toEnd = this.length - index;
        if (toEnd < length) length = toEnd;

        if (length != values.Length)
        {
            int newLength = (this.length - length) + values.Length;
            T[] array = new T[(newLength / chunkSize + 1) * chunkSize];

            for (int i = 0; i < index; i++) array[i] = elements[i];
            for (int i = index + length, j = index + values.Length; i < this.length;)
                array[j++] = elements[i++];

            elements = array;
            this.length = newLength;
        }

        for (int i = 0; i < values.Length; i++) elements[index++] = values[i];

        return length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ToArray() => length == 0 ? new T[0] : elements[..length];
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
    public NameID GetCode(T value)
    {
        int index = values.IndexOf(value);
        if (index < 0) return Engine.SendError<NameID>(
            ErrorCodes.BadArgument, HashArrayName, nameof(value),
            $"'{value}' could not be found in IHashTable<{typeof(T)}>");
        return codes.elements[index];
    }

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
    public int IndexOfCode(NameID code) => codes.IndexOf(code);

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool RemoveValue(T value)
    {
        int index = values.IndexOf(value);
        if (index < 0) return false;
        codes.RemoveAt(index);
        values.RemoveAt(index);
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGet(NameID code, out T value)
    {
        value = default;

        int index = codes.IndexOf(code);
        if (index < 0) return false;

        value = values.elements[index];
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool TryGetCode(T value, out NameID code)
    {
        code = default;

        int index = values.IndexOf(value);
        if (index < 0) return false;

        code = codes.elements[index];
        return true;
    }
}