using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct Triangle
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params Triangle[] values)
    {
        string str = "";
        foreach (Triangle value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params Triangle[] values)
    {
        string str = "";
        foreach (Triangle value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static Triangle FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(Triangle)) throw new ArgumentNullException();

        int ax = 0, ay = 0, bx = 0, by = 0, cx = 0, cy = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                ax = ptr_[0];
                ay = ptr_[1];
                bx = ptr_[2];
                by = ptr_[3];
                cx = ptr_[4];
                cy = ptr_[5];
            }
        else
        {
            ax = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3];
            ay = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7];
            bx = (
                bytes[8] << 0x18) | (
                bytes[9] << 0x10) | (
                bytes[10] << 0x08) |
                bytes[11];
            by = (
                bytes[12] << 0x18) | (
                bytes[13] << 0x10) | (
                bytes[14] << 0x08) |
                bytes[15];
            cx = (
                bytes[16] << 0x18) | (
                bytes[17] << 0x10) | (
                bytes[18] << 0x08) |
                bytes[19];
            cy = (
                bytes[20] << 0x18) | (
                bytes[21] << 0x10) | (
                bytes[22] << 0x08) |
                bytes[23];
        }
        return new(ax, ay, bx, by, cx, cy);
    }

    public unsafe static Triangle[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(Triangle);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        Triangle[] values = new Triangle[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++]);
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
                    bytes[j++], (
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

    public unsafe static byte[] GetBytes(Triangle[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(Triangle)];

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
                    ptr_[j++] = values[i].a.x;
                    ptr_[j++] = values[i].a.y;
                    ptr_[j++] = values[i].b.x;
                    ptr_[j++] = values[i].b.y;
                    ptr_[j++] = values[i].c.x;
                    ptr_[j++] = values[i].c.y;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                Triangle value = values[i];
                bytes[j++] = (byte)(value.a.x >> 0x18);
                bytes[j++] = (byte)(value.a.x >> 0x10);
                bytes[j++] = (byte)(value.a.x >> 0x08);
                bytes[j++] = (byte)value.a.x;
                bytes[j++] = (byte)(value.a.y >> 0x18);
                bytes[j++] = (byte)(value.a.y >> 0x10);
                bytes[j++] = (byte)(value.a.y >> 0x08);
                bytes[j++] = (byte)value.a.y;
                bytes[j++] = (byte)(value.b.x >> 0x18);
                bytes[j++] = (byte)(value.b.x >> 0x10);
                bytes[j++] = (byte)(value.b.x >> 0x08);
                bytes[j++] = (byte)value.b.x;
                bytes[j++] = (byte)(value.b.y >> 0x18);
                bytes[j++] = (byte)(value.b.y >> 0x10);
                bytes[j++] = (byte)(value.b.y >> 0x08);
                bytes[j++] = (byte)value.b.y;
                bytes[j++] = (byte)(value.c.x >> 0x18);
                bytes[j++] = (byte)(value.c.x >> 0x10);
                bytes[j++] = (byte)(value.c.x >> 0x08);
                bytes[j++] = (byte)value.c.x;
                bytes[j++] = (byte)(value.c.y >> 0x18);
                bytes[j++] = (byte)(value.c.y >> 0x10);
                bytes[j++] = (byte)(value.c.y >> 0x08);
                bytes[j++] = (byte)value.c.y;
            }
        return bytes;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static byte[][] GetBytesSeparate(Triangle[] values, bool littleEndian) => default;

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params Triangle[] values)
    {
        string str = "";
        foreach (Triangle value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params Triangle[] values)
    {
        string str = "";
        foreach (Triangle value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParse(string str, out Triangle value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseAny(string str, bool littleEndian, out Triangle value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseBin(string str, bool littleEndian, out Triangle value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseHex(string str, bool littleEndian, out Triangle value)
    {
        value = default;
        return default;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(Triangle)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                ptr_[0] = a.x;
                ptr_[1] = a.y;
                ptr_[2] = b.x;
                ptr_[3] = b.y;
                ptr_[4] = c.x;
                ptr_[5] = c.y;
            }
        else
        {
            bytes[0] = (byte)(a.x >> 0x18);
            bytes[1] = (byte)(a.x >> 0x10);
            bytes[2] = (byte)(a.x >> 0x08);
            bytes[3] = (byte)a.x;
            bytes[4] = (byte)(a.y >> 0x18);
            bytes[5] = (byte)(a.y >> 0x10);
            bytes[6] = (byte)(a.y >> 0x08);
            bytes[7] = (byte)a.y;
            bytes[8] = (byte)(b.x >> 0x18);
            bytes[9] = (byte)(b.x >> 0x10);
            bytes[10] = (byte)(b.x >> 0x08);
            bytes[11] = (byte)b.x;
            bytes[12] = (byte)(b.y >> 0x18);
            bytes[13] = (byte)(b.y >> 0x10);
            bytes[14] = (byte)(b.y >> 0x08);
            bytes[15] = (byte)b.y;
            bytes[16] = (byte)(c.x >> 0x18);
            bytes[17] = (byte)(c.x >> 0x10);
            bytes[18] = (byte)(c.x >> 0x08);
            bytes[19] = (byte)c.x;
            bytes[20] = (byte)(c.y >> 0x18);
            bytes[21] = (byte)(c.y >> 0x10);
            bytes[22] = (byte)(c.y >> 0x08);
            bytes[23] = (byte)c.y;
        }
        return bytes;
    }
}