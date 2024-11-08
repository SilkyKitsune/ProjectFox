using System;
using M = ProjectFox.CoreEngine.Math.Math;

namespace ProjectFox.CoreEngine.Data;

public static partial class Data
{
    #region PackBits
    public static byte[] PackBits(int packetSize, /*params?*/ byte[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        if (packetSize < 1) throw new ArgumentException($"Invalid {nameof(packetSize)}! '{packetSize}'");

        int size = sizeof(byte) * 8;

        if (packetSize > size) throw new ArgumentException($"{nameof(packetSize)} cannot be larger than element size!");
        else if (packetSize == size)
        {
            byte[] copy = new byte[values.Length];
            values.CopyTo(copy, 0);
            return copy;
        }

        int bitMask = 0, i = 0;
        while (i++ < packetSize) bitMask = bitMask << 1 | 1;

        float packedValueCount = (values.Length * packetSize) / (float)size;
        byte[] packedValues = new byte[(int)packedValueCount + (M.HasFraction(packedValueCount) ? 1 : 0)];//is there a better way to do this?

        for (int s = 0, d = 0; s < values.Length; d++)
            for (int bitsToFill = size, bitsToPlace = packetSize; s < values.Length && bitsToFill != 0;)
            {
                int shiftAmount = bitsToFill - bitsToPlace;
                if (shiftAmount < 0)
                {
                    bitsToPlace = -shiftAmount;
                    int bitsToAdd = (values[s] & bitMask) >> bitsToPlace;
                    packedValues[d++] |= (byte)bitsToAdd;
                    bitsToFill = size;
                }
                else
                {
                    bitsToFill = shiftAmount;
                    int bitsToAdd = (values[s++] & bitMask) << bitsToFill;
                    packedValues[d] |= (byte)bitsToAdd;
                    bitsToPlace = packetSize;
                }
            }
        return packedValues;
    }

    //overloads
    //public static ushort[] PackBits(int packetSize, ushort[] values)
    #endregion

    #region UnpackBits
    public static byte[] UnpackBits(int packetSize, byte[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        if (packetSize < 1) throw new ArgumentException($"Invalid {nameof(packetSize)}! '{packetSize}'");

        int size = sizeof(byte) * 8;

        if (packetSize > size) throw new ArgumentException($"{nameof(packetSize)} cannot be larger than element size!");
        else if (packetSize == size)
        {
            byte[] copy = new byte[values.Length];
            values.CopyTo(copy, 0);
            return copy;
        }

        int bitMask = 0, i = 0;
        while (i++ < packetSize) bitMask = bitMask << 1 | 1;

        byte[] unpackedValues = new byte[values.Length * 8 / packetSize];

        for (int s = 0, d = 0; s < values.Length && d < unpackedValues.Length; d++)
            for (int bitsToFill = packetSize, bitsToPlace = size; s < values.Length && d < unpackedValues.Length && bitsToPlace != 0;)
            {
                int shiftAmount = bitsToFill - bitsToPlace;
                if (shiftAmount < 0)
                {
                    bitsToPlace = -shiftAmount;
                    int bitsToAdd = (values[s] >> bitsToPlace) & bitMask;
                    unpackedValues[d++] |= (byte)bitsToAdd;
                    bitsToFill = packetSize;
                }
                else
                {
                    bitsToFill = shiftAmount;
                    int bitsToAdd = (values[s++] << bitsToFill) & bitMask;
                    unpackedValues[d] |= (byte)bitsToAdd;
                    bitsToPlace = size;
                }
            }
        return unpackedValues;
    }

    //overloads
    #endregion
}