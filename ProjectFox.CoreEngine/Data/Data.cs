using System;
using System.Runtime.CompilerServices;
using M = ProjectFox.CoreEngine.Math.Math;

namespace ProjectFox.CoreEngine.Data;

public unsafe static partial class Data
{
    //bit count consts?
    //private const int i = sizeof(byte);
    //private static readonly int i = sizeof(ProjectFox.CoreEngine.Math.Vector);

    //does the preprocessor actually work?

    #region GetBytes
    public static byte[] GetBytes(short value, bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(short)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) *(short*)ptr = value;
        else
        {
            bytes[0] = (byte)(value >> 0x08);
            bytes[1] = (byte)value;
        }
        return bytes;
    }

    public static byte[] GetBytes(short[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        byte[] bytes = new byte[values.Length * sizeof(short)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                for (int i = 0; i < values.Length; i++)
                    ptr_[i] = values[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                int value = values[i];
                bytes[j++] = (byte)(value >> 0x08);
                bytes[j++] = (byte)value;
            }
        return bytes;
    }

    public static byte[] GetBytes(int value, bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(int)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) *(int*)ptr = value;
        else
        {
            bytes[0] = (byte)(value >> 0x18);
            bytes[1] = (byte)(value >> 0x10);
            bytes[2] = (byte)(value >> 0x08);
            bytes[3] = (byte)value;
        }
        return bytes;
    }

    public static byte[] GetBytes(int[] values, bool littleEndian)//params?
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        byte[] bytes = new byte[values.Length * sizeof(int)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0; i < values.Length; i++)
                    ptr_[i] = values[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                int value = values[i];
                bytes[j++] = (byte)(value >> 0x18);
                bytes[j++] = (byte)(value >> 0x10);
                bytes[j++] = (byte)(value >> 0x08);
                bytes[j++] = (byte)value;
            }
        return bytes;
    }

    public static byte[] GetBytes(long value, bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(long)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) *(long*)ptr = value;
        else
        {
            bytes[0] = (byte)(value >> 0x38);
            bytes[1] = (byte)(value >> 0x30);
            bytes[2] = (byte)(value >> 0x28);
            bytes[3] = (byte)(value >> 0x20);
            bytes[4] = (byte)(value >> 0x18);
            bytes[5] = (byte)(value >> 0x10);
            bytes[6] = (byte)(value >> 0x08);
            bytes[7] = (byte)value;
        }
        return bytes;
    }

    public static byte[] GetBytes(long[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        byte[] bytes = new byte[values.Length * sizeof(long)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                long* ptr_ = (long*)ptr;
                for (int i = 0; i < values.Length; i++)
                    ptr_[i] = values[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                long value = values[i];
                bytes[j++] = (byte)(value >> 0x38);
                bytes[j++] = (byte)(value >> 0x30);
                bytes[j++] = (byte)(value >> 0x28);
                bytes[j++] = (byte)(value >> 0x20);
                bytes[j++] = (byte)(value >> 0x18);
                bytes[j++] = (byte)(value >> 0x10);
                bytes[j++] = (byte)(value >> 0x08);
                bytes[j++] = (byte)value;
            }
        return bytes;
    }

    public static byte[] GetBytes(float value, bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(float)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) *(float*)ptr = value;
        else
        {
            int _ = *(int*)&value;
            bytes[0] = (byte)(_ >> 0x18);
            bytes[1] = (byte)(_ >> 0x10);
            bytes[2] = (byte)(_ >> 0x08);
            bytes[3] = (byte)_;
        }
        return bytes;
    }

    public static byte[] GetBytes(float[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        byte[] bytes = new byte[values.Length * sizeof(float)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                for (int i = 0; i < values.Length; i++)
                    ptr_[i] = values[i];
            }
        else fixed (float* ptr = values)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                {
                    int value = ptr_[i];
                    bytes[j++] = (byte)(value >> 0x18);
                    bytes[j++] = (byte)(value >> 0x10);
                    bytes[j++] = (byte)(value >> 0x08);
                    bytes[j++] = (byte)value;
                }
            }
        return bytes;
    }

    public static byte[] GetBytes(double value, bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(double)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) *(double*)ptr = value;
        else
        {
            int _ = *(int*)&value;
            bytes[0] = (byte)(_ >> 0x38);
            bytes[1] = (byte)(_ >> 0x30);
            bytes[2] = (byte)(_ >> 0x28);
            bytes[3] = (byte)(_ >> 0x20);
            bytes[4] = (byte)(_ >> 0x18);
            bytes[5] = (byte)(_ >> 0x10);
            bytes[6] = (byte)(_ >> 0x08);
            bytes[7] = (byte)_;
        }
        return bytes;
    }

    public static byte[] GetBytes(double[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException();

        byte[] bytes = new byte[values.Length * sizeof(double)];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                double* ptr_ = (double*)ptr;
                for (int i = 0; i < values.Length; i++)
                    ptr_[i] = values[i];
            }
        else fixed (double* ptr = values)
            {
                long* ptr_ = (long*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                {
                    long value = ptr_[i];
                    bytes[j++] = (byte)(value >> 0x38);
                    bytes[j++] = (byte)(value >> 0x30);
                    bytes[j++] = (byte)(value >> 0x28);
                    bytes[j++] = (byte)(value >> 0x20);
                    bytes[j++] = (byte)(value >> 0x18);
                    bytes[j++] = (byte)(value >> 0x10);
                    bytes[j++] = (byte)(value >> 0x08);
                    bytes[j++] = (byte)value;
                }
            }
        return bytes;
    }
    #endregion

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

    #region To
    public static float ToFloat32(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(float)) throw new ArgumentNullException();

        float value = 0f;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) value = *(float*)ptr;
        else
        {
            int _ = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3];
            value = *(float*)&_;
        }
        return value;
    }

    public static double ToFloat64(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(double)) throw new ArgumentNullException();

        double value = 0d;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) value = *(double*)ptr;
        else
        {
            long _ = (
                (long)bytes[0] << 0x38) | (
                (long)bytes[1] << 0x30) | (
                (long)bytes[2] << 0x28) | (
                (long)bytes[3] << 0x20) | (
                (long)bytes[4] << 0x18) | (
                (long)bytes[5] << 0x10) | (
                (long)bytes[6] << 0x08) |
                (long)bytes[7];
            value = *(double*)&_;
        }
        return value;
    }

    public static short ToInt16(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(short)) throw new ArgumentNullException();

        short value = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) value = *(short*)ptr;
        else value = (short)((
                bytes[0] << 0x08) |
                bytes[1]);
        return value;
    }

    public static int ToInt32(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(int)) throw new ArgumentNullException();

        int value = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) value = *(int*)ptr;
        else value = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3];
        return value;
    }

    public static long ToInt64(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(long)) throw new ArgumentNullException();

        long value = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes) value = *(long*)ptr;
        else value = (
                (long)bytes[0] << 0x38) | (
                (long)bytes[1] << 0x30) | (
                (long)bytes[2] << 0x28) | (
                (long)bytes[3] << 0x20) | (
                (long)bytes[4] << 0x18) | (
                (long)bytes[5] << 0x10) | (
                (long)bytes[6] << 0x08) |
                (long)bytes[7];
        return value;
    }

    //rename these?
    public static float[] ToFloat32Multiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(float);

        if (bytes == null || bytes.Length < size) throw new ArgumentNullException();

        float[] values = new float[bytes.Length / size];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                for (int i = 0; i < values.Length; i++)
                    values[i] = ptr_[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                int _ = (
                    bytes[j++] << 0x18) | (
                    bytes[j++] << 0x10) | (
                    bytes[j++] << 0x08) |
                    bytes[j++];
                values[i] = *(float*)&_;
            }
        return values;
    }

    public static double[] ToFloat64Multiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(double);

        if (bytes == null || bytes.Length < size) throw new ArgumentNullException();

        double[] values = new double[bytes.Length / size];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                double* ptr_ = (double*)ptr;
                for (int i = 0; i < values.Length; i++)
                    values[i] = ptr_[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                long _ = (
                    (long)bytes[j++] << 0x38) | (
                    (long)bytes[j++] << 0x30) | (
                    (long)bytes[j++] << 0x28) | (
                    (long)bytes[j++] << 0x20) | (
                    (long)bytes[j++] << 0x18) | (
                    (long)bytes[j++] << 0x10) | (
                    (long)bytes[j++] << 0x08) |
                    (long)bytes[j++];
                values[i] = *(double*)&_;
            }
        return values;
    }

    public static short[] ToInt16Multiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(short);

        if (bytes == null || bytes.Length < size) throw new ArgumentNullException();

        short[] values = new short[bytes.Length / size];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                for (int i = 0; i < values.Length; i++)
                    values[i] = ptr_[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
                values[i] = (short)((
                    bytes[j++] << 0x08) |
                    bytes[j++]);
        return values;
    }

    public static int[] ToInt32Multiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(int);

        if (bytes == null || bytes.Length < size) throw new ArgumentNullException();

        int[] values = new int[bytes.Length / size];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0; i < values.Length; i++)
                    values[i] = ptr_[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
                values[i] = (
                    bytes[j++] << 0x18) | (
                    bytes[j++] << 0x10) | (
                    bytes[j++] << 0x08) |
                    bytes[j++];
        return values;
    }

    public static long[] ToInt64Multiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(long);

        if (bytes == null || bytes.Length < size) throw new ArgumentNullException();

        long[] values = new long[bytes.Length / size];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                long* ptr_ = (long*)ptr;
                for (int i = 0; i < values.Length; i++)
                    values[i] = ptr_[i];
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
                values[i] = (
                    (long)bytes[j++] << 0x38) | (
                    (long)bytes[j++] << 0x30) | (
                    (long)bytes[j++] << 0x28) | (
                    (long)bytes[j++] << 0x20) | (
                    (long)bytes[j++] << 0x18) | (
                    (long)bytes[j++] << 0x10) | (
                    (long)bytes[j++] << 0x08) |
                    (long)bytes[j++];
        return values;
    }
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