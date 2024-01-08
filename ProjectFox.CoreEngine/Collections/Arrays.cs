#if DEBUG
using System;

namespace ProjectFox.CoreEngine.Collections;

[Obsolete]
internal static class Arrays
{
    public static void Copy<T>(T[] source, T[] destination, int length)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        if (length > source.Length || length > destination.Length)
            throw new ArgumentException($"{nameof(length)}={length} was too long!");

        for (int i = 0; i < length; i++) destination[i] = source[i];
    }

    public static void Copy<T>(T[] source, int sourceIndex, T[] destination, int destinationIndex, int length)
    {
        if (source == null) throw new ArgumentNullException(nameof(source));
        if (destination == null) throw new ArgumentNullException(nameof(destination));

        if (sourceIndex >= source.Length || sourceIndex < 0)
            throw new IndexOutOfRangeException(nameof(sourceIndex));

        if (destinationIndex >= destination.Length || destinationIndex < 0)
            throw new IndexOutOfRangeException(nameof(destinationIndex));

        if (sourceIndex + length > source.Length || destinationIndex + length > destination.Length)
            throw new ArgumentException($"{nameof(length)}={length} was too long!");

        for (int destLength = destinationIndex + length;
            destinationIndex < destLength; sourceIndex++, destinationIndex++)
            destination[destinationIndex] = source[sourceIndex];
    }

    public static void CopyRange<T>(T[] source, int sourceIndex, int sourceIndexFar, T[] destination, int destinationIndex)
    {

    }
}
#endif