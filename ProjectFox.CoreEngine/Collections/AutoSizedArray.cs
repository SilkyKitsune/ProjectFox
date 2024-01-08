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
        if (chunkSize <= 0)
            throw new ArgumentException($"{nameof(chunkSize)}={chunkSize} must be greater than zero!");

        this.chunkSize = chunkSize;
        length = array.Length;
        elements = new T[length / chunkSize + 1 * chunkSize];
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
    private int length;//internal?
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

    /// <summary></summary>
    /// <param name="startIndex"></param>
    /// <param name="endIndex"></param>
    /// <returns></returns>
    /// <remarks> set is DEBUG only </remarks>
    /// <exception cref="ArgumentException"></exception>
    /// <exception cref="IndexOutOfRangeException"></exception>
    /// <exception cref="ArgumentNullException"></exception>
    public T[] this[int startIndex, int endIndex]//might change to length
    {
        get
        {
            if (startIndex > endIndex)
                throw new ArgumentException($"MinMaxException ({nameof(startIndex)}={startIndex} > {nameof(endIndex)}={endIndex})");

            if (startIndex >= length || startIndex < 0) throw new IndexOutOfRangeException();

            if (endIndex >= length) return elements[startIndex..length];
            return elements[startIndex..endIndex];
        }
        set//this doesn't seem to work right
        {//endIndex is redundant here because value.Length is functionally a lenght argument
#if DEBUG
            if (value == null) throw new ArgumentNullException(nameof(value));

            if (startIndex > endIndex)
                throw new ArgumentException($"MinMaxException ({nameof(startIndex)}={startIndex} > {nameof(endIndex)}={endIndex})");

            if (startIndex >= length || startIndex < 0) throw new IndexOutOfRangeException();

            int lengthToWrite = endIndex - startIndex;
            if (lengthToWrite > value.Length)
                endIndex -= lengthToWrite - value.Length;

            if (endIndex >= length)
            {
                T[] array = new T[endIndex + 1 / chunkSize + 1 * chunkSize];
                for (int i = 0; i < length; i++) array[i] = elements[i];
                elements = array;
            }
            for (int i = 0; i < value.Length; i++) elements[startIndex++] = value[i];
            length = endIndex + 1;
#else
            throw new NotImplementedException();
#endif
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
    public AutoSizedArray<T> Copy() => new(this);

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

    /// <remarks> DEBUG only </remarks>
    public void Insert(int index, params T[] values)//don't think this works right
    {
#if DEBUG
        if (index >= length)
        {
            Add(values);
            return;
        }

        int newLength = length + values.Length;
        if (newLength >= elements.Length)
        {
            T[] array = new T[(int)(newLength / (float)chunkSize) + 1 * chunkSize];
            //copying the elements up to just before index
            for (int i = 0; i < index; i++) array[i] = elements[i];
            //copying the elements after index + values.Length backwards
            for (int i = newLength - 1, j = length - 1; j > index;)
                array[i--] = elements[j--];
            elements = array;
        }//  copying the elements after index + values.Length backwards
        else for (int i = newLength - 1, j = length - 1; j > index;)
                elements[i--] = elements[j--];

        for (int i = 0; i < values.Length; i++) elements[index++] = values[i];
        length = newLength;
#else
        throw new NotImplementedException();
#endif
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

    public bool RemoveRange(int startIndex, int endIndex)//change to length
    {
        if (startIndex > endIndex)
            throw new ArgumentException($"MinMaxException ({nameof(startIndex)}={startIndex} > {nameof(endIndex)}={endIndex})");

        if (startIndex >= length || startIndex < 0) return false;

        //if endIndex is >= the final index
        if (endIndex >= length - 1) length = startIndex;
        else
        {
            //storing the new length before startIndex and endIndex are modified
            int newLength = length - (endIndex - startIndex);
            //moving the values from one ahead of endIndex to startIndex
            while (endIndex < length) elements[startIndex++] = elements[++endIndex];
            length = newLength;
        }

        //if the number of empty elements is >= 2 chunks
        if (elements.Length - length >= chunkSize * 2)
        {
            T[] array = new T[length / chunkSize + 1 * chunkSize];
            for (int i = 0; i < length; i++) array[i] = elements[i];
            elements = array;
        }

        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public T[] ToArray() => length == 0 ? new T[0] : elements[0..length];
}
