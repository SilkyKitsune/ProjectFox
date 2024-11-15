using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZF
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params VectorZF[] values)
    {
        string str = "";
        foreach (VectorZF value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params VectorZF[] values)
    {
        string str = "";
        foreach (VectorZF value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static VectorZF FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(VectorZF)) throw new ArgumentNullException();

        float x = 0f, y = 0f, z = 0f;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                x = ptr_[0];
                y = ptr_[1];
                z = ptr_[2];
            }
        else
        {
            int x_ = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3],
            y_ = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7],
            z_ = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7];
            x = *(float*)&x_;
            y = *(float*)&y_;
            z = *(float*)&z_;
        }
        return new(x, y, z);
    }

    public unsafe static VectorZF[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(VectorZF);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        VectorZF[] values = new VectorZF[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++], ptr_[j++]);
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                VectorZ v = new((
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
                values[i] = *(VectorZF*)&v;
            }
        return values;
    }

    public unsafe static byte[] GetBytes(VectorZF[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(VectorZF)];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                {
                    ptr_[j++] = values[i].x;
                    ptr_[j++] = values[i].y;
                    ptr_[j++] = values[i].z;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                VectorZF value_ = values[i];
                VectorZ value = *(VectorZ*)&value_;
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

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static byte[][] GetBytesSeparate(VectorZF[] values, bool littleEndian) => default;

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params VectorZF[] values)
    {
        string str = "";
        foreach (VectorZF value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params VectorZF[] values)
    {
        string str = "";
        foreach (VectorZF value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParse(string str, out VectorZF value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseAny(string str, bool littleEndian, out VectorZF value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseBin(string str, bool littleEndian, out VectorZF value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseHex(string str, bool littleEndian, out VectorZF value)
    {
        value = default;
        return default;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(VectorZF)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                ptr_[0] = x;
                ptr_[1] = y;
                ptr_[2] = z;
            }
        else
        {
            float x_ = this.x, y_ = this.y, z_ = this.z;
            int x = *(int*)&x_, y = *(int*)&y_, z = *(int*)&z_;
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