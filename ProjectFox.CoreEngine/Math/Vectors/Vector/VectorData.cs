using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params Vector[] values)
    {
        string str = "";
        foreach (Vector value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params Vector[] values)
    {
        string str = "";
        foreach (Vector value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static Vector FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(Vector)) throw new ArgumentNullException();

        int x = 0, y = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                x = ptr_[0];
                y = ptr_[1];
            }
        else
        {
            x = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3];
            y = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7];
        }
        return new(x, y);
    }

    public unsafe static Vector[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(Vector);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        Vector[] values = new Vector[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++]);
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
                values[i] = new((
                    bytes[j++] << 0x18) | (
                    bytes[j++] << 0x10) | (
                    bytes[j++] << 0x08) |
                    bytes[j++], (
                    bytes[j++] << 0x18) | (
                    bytes[j++] << 0x10) | (
                    bytes[j++] << 0x08) |
                    bytes[j++]);
        return values;
    }

    public unsafe static byte[] GetBytes(Vector[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(Vector)];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                {
                    ptr_[j++] = values[i].x;
                    ptr_[j++] = values[i].y;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                Vector value = values[i];
                bytes[j++] = (byte)(value.x >> 0x18);
                bytes[j++] = (byte)(value.x >> 0x10);
                bytes[j++] = (byte)(value.x >> 0x08);
                bytes[j++] = (byte)value.x;
                bytes[j++] = (byte)(value.y >> 0x18);
                bytes[j++] = (byte)(value.y >> 0x10);
                bytes[j++] = (byte)(value.y >> 0x08);
                bytes[j++] = (byte)value.y;
            }
        return bytes;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static byte[][] GetBytesSeparate(Vector[] values, bool littleEndian) => default;

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params Vector[] values)
    {
        string str = "";
        foreach (Vector value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params Vector[] values)
    {
        string str = "";
        foreach (Vector value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(Vector)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                ptr_[0] = x;
                ptr_[1] = y;
            }
        else
        {
            bytes[0] = (byte)(x >> 0x18);
            bytes[1] = (byte)(x >> 0x10);
            bytes[2] = (byte)(x >> 0x08);
            bytes[3] = (byte)x;
            bytes[4] = (byte)(y >> 0x18);
            bytes[5] = (byte)(y >> 0x10);
            bytes[6] = (byte)(y >> 0x08);
            bytes[7] = (byte)y;
        }
        return bytes;
    }
}
