using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZ
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params VectorZ[] values)
    {
        string str = "";
        foreach (VectorZ value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params VectorZ[] values)
    {
        string str = "";
        foreach (VectorZ value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static VectorZ FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(VectorZ)) throw new ArgumentNullException();

        int x = 0, y = 0, z = 0;
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
                z = ptr_[2];
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
            z = (
                bytes[8] << 0x18) | (
                bytes[9] << 0x10) | (
                bytes[10] << 0x08) |
                bytes[11];
        }
        return new(x, y, z);
    }

    public unsafe static VectorZ[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(VectorZ);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        VectorZ[] values = new VectorZ[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++], ptr_[j++]);
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
                    bytes[j++], (
                    bytes[j++] << 0x18) | (
                    bytes[j++] << 0x10) | (
                    bytes[j++] << 0x08) |
                    bytes[j++]);
        return values;
    }

    public unsafe static byte[] GetBytes(VectorZ[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(VectorZ)];

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
                    ptr_[j++] = values[i].z;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                VectorZ value = values[i];
                bytes[j++] = (byte)(value.x >> 0x18);
                bytes[j++] = (byte)(value.x >> 0x10);
                bytes[j++] = (byte)(value.x >> 0x08);
                bytes[j++] = (byte)value.x;
                bytes[j++] = (byte)(value.y >> 0x18);
                bytes[j++] = (byte)(value.y >> 0x10);
                bytes[j++] = (byte)(value.y >> 0x08);
                bytes[j++] = (byte)value.y;
                bytes[j++] = (byte)(value.z >> 0x18);
                bytes[j++] = (byte)(value.z >> 0x10);
                bytes[j++] = (byte)(value.z >> 0x08);
                bytes[j++] = (byte)value.z;
            }
        return bytes;
    }

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params VectorZ[] values)
    {
        string str = "";
        foreach (VectorZ value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params VectorZ[] values)
    {
        string str = "";
        foreach (VectorZ value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(VectorZ)];
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
                ptr_[2] = z;
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
            bytes[8] = (byte)(z >> 0x18);
            bytes[9] = (byte)(z >> 0x10);
            bytes[10] = (byte)(z >> 0x08);
            bytes[11] = (byte)z;
        }
        return bytes;
    }
}
