using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct Rectangle
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params Rectangle[] values)
    {
        string str = "";
        foreach (Rectangle value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params Rectangle[] values)
    {
        string str = "";
        foreach (Rectangle value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static Rectangle FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(Rectangle)) throw new ArgumentNullException();

        int px = 0, py = 0, sx = 0, sy = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                px = ptr_[0];
                py = ptr_[1];
                sx = ptr_[2];
                sy = ptr_[3];
            }
        else
        {
            px = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3];
            py = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7];
            sx = (
                bytes[8] << 0x18) | (
                bytes[9] << 0x10) | (
                bytes[10] << 0x08) |
                bytes[11];
            sy = (
                bytes[12] << 0x18) | (
                bytes[13] << 0x10) | (
                bytes[14] << 0x08) |
                bytes[15];
        }
        return new(px, py, sx, sy);
    }

    public unsafe static Rectangle[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(Rectangle);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        Rectangle[] values = new Rectangle[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++]);
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
                    bytes[j++]);
        return values;
    }

    public unsafe static byte[] GetBytes(Rectangle[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(Rectangle)];

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
                    ptr_[j++] = values[i].position.x;
                    ptr_[j++] = values[i].position.y;
                    ptr_[j++] = values[i].size.x;
                    ptr_[j++] = values[i].size.y;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                Rectangle value = values[i];
                bytes[j++] = (byte)(value.position.x >> 0x18);
                bytes[j++] = (byte)(value.position.x >> 0x10);
                bytes[j++] = (byte)(value.position.x >> 0x08);
                bytes[j++] = (byte)value.position.x;
                bytes[j++] = (byte)(value.position.y >> 0x18);
                bytes[j++] = (byte)(value.position.y >> 0x10);
                bytes[j++] = (byte)(value.position.y >> 0x08);
                bytes[j++] = (byte)value.position.y;
                bytes[j++] = (byte)(value.size.x >> 0x18);
                bytes[j++] = (byte)(value.size.x >> 0x10);
                bytes[j++] = (byte)(value.size.x >> 0x08);
                bytes[j++] = (byte)value.size.x;
                bytes[j++] = (byte)(value.size.y >> 0x18);
                bytes[j++] = (byte)(value.size.y >> 0x10);
                bytes[j++] = (byte)(value.size.y >> 0x08);
                bytes[j++] = (byte)value.size.y;
            }
        return bytes;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static byte[][] GetBytesSeparate(Rectangle[] values, bool littleEndian) => default;

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params Rectangle[] values)
    {
        string str = "";
        foreach (Rectangle value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params Rectangle[] values)
    {
        string str = "";
        foreach (Rectangle value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParse(string str, out Rectangle value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseAny(string str, bool littleEndian, out Rectangle value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseBin(string str, bool littleEndian, out Rectangle value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseHex(string str, bool littleEndian, out Rectangle value)
    {
        value = default;
        return default;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(Rectangle)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                int* ptr_ = (int*)ptr;
                ptr_[0] = position.x;
                ptr_[1] = position.y;
                ptr_[2] = size.x;
                ptr_[3] = size.y;
            }
        else
        {
            bytes[0] = (byte)(position.x >> 0x18);
            bytes[1] = (byte)(position.x >> 0x10);
            bytes[2] = (byte)(position.x >> 0x08);
            bytes[3] = (byte)position.x;
            bytes[4] = (byte)(position.y >> 0x18);
            bytes[5] = (byte)(position.y >> 0x10);
            bytes[6] = (byte)(position.y >> 0x08);
            bytes[7] = (byte)position.y;
            bytes[8] = (byte)(size.x >> 0x18);
            bytes[9] = (byte)(size.x >> 0x10);
            bytes[10] = (byte)(size.x >> 0x08);
            bytes[11] = (byte)size.x;
            bytes[12] = (byte)(size.y >> 0x18);
            bytes[13] = (byte)(size.y >> 0x10);
            bytes[14] = (byte)(size.y >> 0x08);
            bytes[15] = (byte)size.y;
        }
        return bytes;
    }
}