using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Collections;

public sealed class AutoSizedArray<T> : ICollection<T>, ICopy<AutoSizedArray<T>>
{
    public AutoSizedArray(int chunkSize = 0x10)
    {
        if (chunkSize <= 0)
            throw new ArgumentException($"{nameof(chunkSize)}={chunkSize} must be greater than zero!");
        
        this.chunkSize = chunkSize;
        length = 0;
        elements = new T[chunkSize];
    }

    public AutoSizedArray(T[] array, int chunkSize = 0x10)
    {
        if (array == null)
            throw new ArgumentNullException(nameof(array));

        if (chunkSize <= 0)
            throw new ArgumentException($"{nameof(chunkSize)}={chunkSize} must be greater than zero!");

        this.chunkSize = chunkSize;
        length = array.Length;
        elements = new T[(length / chunkSize + 1) * chunkSize];
        for (int i = 0; i < length; i++) elements[i] = array[i];
    }

    private AutoSizedArray(AutoSizedArray<T> array)
    {
        chunkSize = array.chunkSize;
        length = array.length;
        elements = new T[array.elements.Length];
        for (int i = 0; i < length; i++) elements[i] = array.elements[i];
    }
    
    private readonly int chunkSize;
    private int length;
    private T[] elements;

    public int ChunkSize
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => chunkSize;
    }

    public int Length
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => length;
    }

    public T this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (index >= length || index < 0) ?
            throw new IndexOutOfRangeException() :
            elements[index];
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (index >= length || index < 0)
                throw new IndexOutOfRangeException();
            elements[index] = value;
        }
    }
    
    public void Add(T value)
    {
        if (length == elements.Length)
        {
            T[] array = new T[length + chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }
        elements[length++] = value;
    }

    public void Add(params T[] values)
    {
        if (values == null) throw new ArgumentNullException(nameof(values));
        if (values.Length == 0) return;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AddConcat(T[][] arrays)//could this be inlined at all? is it worth inlining?
    {
        if (arrays == null) throw new ArgumentNullException(nameof(arrays));
        if (arrays.Length == 0) return;

        foreach (T[] array in arrays) Add(array);//should this handle null elements?
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
            if (/*element != null && */elements[i].Equals(value)) return true;
        return false;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Copy(out AutoSizedArray<T> copy) => copy = new(this);

    public void CopyTo(ICollection<T> collection)
    {
        if (collection == null) throw new ArgumentNullException(nameof(collection));
        if (length == 0) return;

        if (collection is AutoSizedArray<T> array)
        {
            CopyTo(array);
            return;
        }
        
        collection.Clear();
        collection.Add(elements[0..length]);
    }

    public void CopyTo(AutoSizedArray<T> array)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));
        if (length == 0) return;

        if (length > array.chunkSize)
            array.elements = new T[length / array.chunkSize + 1 * array.chunkSize];
        
        for (int i = 0; i < length; i++) array.elements[i] = elements[i];
        array.length = length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T FromLastIndex(int index) => index > length || index <= 0 ?
        throw new IndexOutOfRangeException() : elements[length - index];

    public T[] GetMultiple(params int[] indices)
    {
        if (indices.Length == 0) return null;

        T[] array = new T[indices.Length];
        for (int i = 0; i < indices.Length; i++)
            if (indices[i] >= 0 && indices[i] < length)
                array[i] = elements[indices[i]];
        return array;
    }

    public T[] GetRange(int index, int length)
    {
        if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();

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
        if (index < 0)
            throw new IndexOutOfRangeException();

        if (index >= length)
        {
            Add(value);
            return;
        }

        if (length == elements.Length)
        {
            T[] array = new T[length + chunkSize];
            //copying the elements up to just before index
            for (int i = 0; i < index; i++) array[i] = elements[i];
            //copying the elements after index backwards
            for (int i = length; i > index;) array[i] = elements[--i];
            elements = array;
        }//  copying the elements after index backwards
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

        int newLength = length + values.Length;
        if (newLength >= elements.Length)
        {
            T[] array = new T[(newLength / chunkSize + 1) * chunkSize];
            
            for (int i = 0; i < index; i++) array[i] = elements[i]; //copying the elements up to just before index
            for (int i = newLength - 1, j = length - 1, k = index - 1; j > k;) array[i--] = elements[j--]; //copying the elements after index + values.Length backwards

            elements = array;
        }
        else for (int i = newLength - 1, j = length - 1, k = index - 1; j > k;) elements[i--] = elements[j--]; //copying the elements after index + values.Length backwards

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

        //moving the subsequent values back by one index
        int lastIndex = length - 1;
        while (index < lastIndex) elements[index] = elements[++index];
        length--;

        //if the number of empty elements is >= 2 chunks
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
        if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();

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
        if (index < 0 || length < 0) throw new ArgumentOutOfRangeException();

        if (values.Length == 0) return 0;//removerange instead?

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
    public T[] ToArray() => length == 0 ? new T[0] : elements[0..length];
}
