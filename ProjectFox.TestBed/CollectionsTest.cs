using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;

using ProjectFox.CoreEngine.Collections;

using ProjectFox.GameEngine;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    internal static void CollectionsTest()
    {
        #region Substrings
        C.WriteLine("---Substrings---");

        C.WriteLine(string.Join(',', Strings.GetAllSubstrings("Test_Test_N/A_Test_Test", "Test")));//incorrect
        C.WriteLine(Strings.GetSubstring("Test_Test_N/A_Test_Test", 5, 10));
        C.WriteLine(Strings.GetSubstringUntilChar("Test_Test_N/A_Test_Test", 0, 'A'));
        C.WriteLine(Strings.GetSubstringUntilChar("Test_Test_N/A_Test_Test", 0, "N/A"));
        C.WriteLine(Strings.GetSubstringUntilString("Test_Test_N/A_Test_Test", 0, @"N\A"));
        C.WriteLine(Strings.TryGetFirstSubstring(out int i, "Test_Test_N/A_Test_Test", "_Test"));
        C.WriteLine(i);
        C.WriteLine(Strings.TryGetLastSubstring(out i, "Test_Test_N/A_Test_Test", "_Test"));//incorrect
        C.WriteLine(i);

        C.WriteLine("-----\n");
        #endregion

        #region AutoSizedArray
        C.WriteLine("---AutoSizedArray---");

        AutoSizedArray<char> array = new AutoSizedArray<char>(new char[]
        {
            'a', 'b', 'c', 'd', 'e'
        });

        ICopyTest(array, out AutoSizedArray<char> arrayCopy);
        ICollectionTest(array, arrayCopy, 'x', 'y', 'z');

        try
        {
            new AutoSizedArray<int>(-1);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            new AutoSizedArray<int>(new int[1] { 0 }, -1);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            C.WriteLine(array[array.Length]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array[-1]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            array[array.Length] = ']';
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[-1] = ']';
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        /*try
        {
            C.WriteLine(array[1, 0]);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array[array.Length, array.Length + 1]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array[-1, 0]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            array[1, 0] = null;
            C.WriteLine(']');
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[1, 0] = new char[0];
            C.WriteLine(']');
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[array.Length, array.Length + 1] = new char[0];
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[-1, 0] = new char[0];
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }*/

        try
        {
            ICollection<char> c = null;
            array.CopyTo(c);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array.CopyTo(null);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            C.WriteLine(array.FromLastIndex(array.Length + 1));
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array.FromLastIndex(-1));
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            C.WriteLine(array.GetRange(-1, 0));
        }
        catch (ArgumentOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array.GetRange(0, -1));
        }
        catch (ArgumentOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            array.RemoveRange(-1, 0);
        }
        catch (ArgumentOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array.RemoveRange(0, -1);
        }
        catch (ArgumentOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        //replace range

        AutoSizedArray<int> array2 = new AutoSizedArray<int>(new int[]
        {
            0, 1, 2, 3, 4
        });

        ICopyTest(array2, out AutoSizedArray<int> array2Copy);
        ICollectionTest(array2, array2Copy, 7, 8, 9);

        C.WriteLine("-----\n");
        #endregion

        #region HashLookupTable
        C.WriteLine("---HashLookupTable---");

        HashLookupTable<char, Vector> hashTable = new HashLookupTable<char, Vector>();
        hashTable.Add('a', new Vector(1, 1));
        hashTable.Add('b', new Vector(2, 2));
        hashTable.Add('c', new Vector(3, 3));
        hashTable.Add('d', new Vector(4, 4));
        hashTable.Add('e', new Vector(5, 5));
        hashTable.Add('&'/*(char)0*/, new Vector(-1, -1));

        ICopyTest(hashTable, out HashLookupTable<char, Vector> hashTableCopy);
        IHashTableTest(hashTable, hashTableCopy, 'a', 'b', 'c', 'x', 'y', 'z', new Vector(-69, 69));

        try
        {
            C.WriteLine(hashTable['1']);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            hashTable['1'] = default;
            C.WriteLine(']');
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            hashTable.Add('a', default);
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            IHashTable<char, Vector> t = null;
            hashTable.CopyTo(t);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            hashTable.CopyTo(null);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion
    }

    #region Interface Types
    private static void ICopyTest<T>(ICopy<T> iCopy, out T copy)
    {
        C.WriteLine("-ICopy-");

        copy = iCopy.Copy();

        C.WriteLine(iCopy.GetType());
        C.WriteLine(typeof(ICopy<T>));
        C.WriteLine(copy.GetType());
        C.WriteLine(typeof(T));

        C.WriteLine(iCopy.ToString());
        C.WriteLine(copy.ToString());
    }

    private static void ICollectionTest<T>(ICollection<T> iCollection, ICollection<T> copy, T value1, T value2, T value3)
    {
        C.WriteLine("-ICollection-");

        C.WriteLine(iCollection.GetType());
        C.WriteLine(typeof(ICollection<T>));
        C.WriteLine(typeof(T));

        C.WriteLine(iCollection.ToString());

        C.WriteLine(iCollection.Length);

        C.WriteLine(iCollection[0]);
        T item = iCollection[0];
        iCollection[0] = value1;
        C.WriteLine(iCollection[0]);
        iCollection[0] = item;
        C.WriteLine(iCollection[0]);

        //C.WriteLine(string.Join(", ", iCollection[0, iCollection.Length]));

        //T[] items = iCollection[0, iCollection.Length];
        //iCollection[0, iCollection.Length] = new T[3] { value1, value2, value3 };//incorrect
        //C.WriteLine(string.Join(", ", iCollection[0, iCollection.Length]));
        //C.WriteLine(iCollection.Length);

        //iCollection[0, iCollection.Length] = items;
        //C.WriteLine(string.Join(", ", iCollection[0, iCollection.Length]));

        //C.WriteLine(string.Join(", ", iCollection[0, 2]));

        C.WriteLine(iCollection.Concat());

        C.WriteLine(iCollection.Contains(value1));

        C.WriteLine(iCollection.FromLastIndex(iCollection.Length));
        C.WriteLine(iCollection.FromLastIndex(1));

        C.WriteLine(string.Join(", ", iCollection.GetMultiple(3, 1, 4)));
        
        C.WriteLine(string.Join(", ", iCollection.GetRange(1, 3)));

        C.WriteLine(iCollection.IndexOf(iCollection[0]));
        C.WriteLine(iCollection.IndexOf(iCollection[1]));
        C.WriteLine(iCollection.IndexOf(value1));

        C.WriteLine(iCollection.Join(','));

        C.WriteLine(iCollection.LastIndexOf(iCollection[0]));
        C.WriteLine(iCollection.LastIndexOf(iCollection[1]));
        C.WriteLine(iCollection.LastIndexOf(value1));

        C.WriteLine(string.Join(", ", iCollection.ToArray()));

        iCollection.Add(value1);
        C.WriteLine(iCollection.Contains(value1));
        C.WriteLine(iCollection.Join(", "));

        iCollection.Remove(value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Add(value1, value2, value3);
        C.WriteLine(iCollection.Join(", "));

        iCollection.RemoveRange(iCollection.Length - 3, 2);
        C.WriteLine(iCollection.Join(", "));

        iCollection.RemoveAt(iCollection.Length - 1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Insert(2, value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Remove(value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Insert(3, value1, value2, value3);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Move(2, 0);
        C.WriteLine(iCollection.Join(", "));

        copy.CopyTo(iCollection);
        C.WriteLine(iCollection.Join(", "));

        T[] values = iCollection.GetRange(1, 2);
        iCollection.ReplaceRange(1, 2, value1, value2, value3);
        C.WriteLine(iCollection.Join(", "));

        iCollection.ReplaceRange(1, 3, values);
        C.WriteLine(iCollection.Join(", "));

        iCollection.ReplaceRange(3, iCollection.Length, value1, value1, value1, value1, value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Clear();
        C.WriteLine(iCollection.Join(", "));

        copy.CopyTo(iCollection);
        C.WriteLine(iCollection.Join(", "));
    }

    private static void IHashTableTest<H, T>(IHashTable<H, T> iHashTable, IHashTable<H, T> copy, H code1, H code2, H code3, H newCode1, H newCode2, H newCode3, T value)
    {
        C.WriteLine("-IHashTable-");

        C.WriteLine(iHashTable.GetType());
        C.WriteLine(typeof(IHashTable<H, T>));
        C.WriteLine(typeof(H));
        C.WriteLine(typeof(T));

        C.WriteLine(iHashTable.ToString());

        C.WriteLine(iHashTable.Length);

        C.WriteLine(iHashTable[code1]);
        T item = iHashTable[code1];
        iHashTable[code1] = value;
        C.WriteLine(iHashTable[code1]);
        iHashTable[code1] = item;
        C.WriteLine(iHashTable[code1]);

        C.WriteLine(iHashTable.Concat());
        C.WriteLine(iHashTable.ConcatCodes());
        C.WriteLine(iHashTable.ConcatValues());

        C.WriteLine(iHashTable.Contains(value));
        C.WriteLine(iHashTable.ContainsCode(code1));

        C.WriteLine(string.Join(", ", iHashTable.GetCodes()));
        C.WriteLine(string.Join(", ", iHashTable.GetValues()));

        C.WriteLine(string.Join(", ", iHashTable.GetMultiple(code1, code2, code3)));

        C.WriteLine(iHashTable.IndexOf(value));
        C.WriteLine(iHashTable.IndexOf(iHashTable[code1]));
        C.WriteLine(iHashTable.IndexOf(iHashTable[code2]));
        C.WriteLine(iHashTable.IndexOf(iHashTable[code3]));

        C.WriteLine(iHashTable.Join(','));
        C.WriteLine(iHashTable.JoinCodes(','));
        C.WriteLine(iHashTable.JoinCodes(", "));
        C.WriteLine(iHashTable.JoinValues(','));
        C.WriteLine(iHashTable.JoinValues(", "));

        C.WriteLine(iHashTable.LastIndexOf(value));
        C.WriteLine(iHashTable.LastIndexOf(iHashTable[code1]));
        C.WriteLine(iHashTable.LastIndexOf(iHashTable[code2]));
        C.WriteLine(iHashTable.LastIndexOf(iHashTable[code3]));

        iHashTable.Add(newCode1, value);
        C.WriteLine(iHashTable.Contains(value));
        C.WriteLine(iHashTable.ContainsCode(newCode1));
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.RemoveValue(value);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Add(newCode1, value);
        iHashTable.Add(newCode2, value);
        iHashTable.Add(newCode3, value);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.RemoveAt(iHashTable.Length - 1);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Remove(newCode1);
        iHashTable.Remove(newCode2);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Clear();
        C.WriteLine(iHashTable.Join(", "));

        copy.CopyTo(iHashTable);
        C.WriteLine(iHashTable.Join(", "));
    }

    private static void IColorGroupTest(IColorGroup iColorGroup)
    {
        C.WriteLine("-IColorGroup-");

        C.WriteLine(iColorGroup.GetType());

        C.WriteLine(iColorGroup.ToString());

        iColorGroup.HueShift(50f);
        iColorGroup.SaturationMultiply(0.5f);
        iColorGroup.VelocityMultiply(0.5f);
    }
    #endregion

    public static void GameEngineCollectionsTest()
    {
        C.WriteLine("---Array---");

        Debug.TestArray.Add(1, 2, 3, 4, 5);
        ICollectionTest(Debug.TestArray, new AutoSizedArray<int>(Debug.TestArray.ToArray()), 69, 420, 324);

        C.WriteLine("-----\n");

        C.WriteLine("---HashArray---");

        Debug.TestTable.Add(new("TestVal", 1), 1);
        Debug.TestTable.Add(new("TestVal", 2), 2);
        Debug.TestTable.Add(new("TestVal", 3), 3);
        Debug.TestTable.Add(new("TestVal", 4), 4);
        Debug.TestTable.Add(new("TestVal", 5), 5);

        HashLookupTable<NameID, int> tableCopy = new HashLookupTable<NameID, int>();
        foreach (int i in Debug.TestTable.GetValues())
            tableCopy.Add(new("TestVal", (byte)i), i);

        IHashTableTest(Debug.TestTable, tableCopy, new("TestVal", 1), new("TestVal", 2), new("TestVal", 3), new("TestVal", 6), new("TestVal", 7), new("TestVal", 8), 324);

        C.WriteLine("-----\n");
    }
}
