using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct Sample
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static Sample FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(Sample)) throw new ArgumentNullException();

        short l = 0, r = 0;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                l = ptr_[0];
                r = ptr_[1];
            }
        else
        {
            l = (short)((
                bytes[0] << 0x08) |
                bytes[1]);
            r = (short)((
                bytes[2] << 0x08) |
                bytes[3]);
        }
        return new(l, r);
    }

    public unsafe static Sample FromBytesMono(byte[] bytes, bool littleEndian)
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
        return new(value);
    }

    public unsafe static Sample[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(Sample);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        Sample[] values = new Sample[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++]);
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
                values[i] = new(
                    (short)((
                    bytes[j++] << 0x08) |
                    bytes[j++]),
                    (short)((
                    bytes[j++] << 0x08) |
                    bytes[j++]));
        return values;
    }

    public unsafe static Sample[] FromBytesMultipleMono(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(short);

        if (bytes == null || bytes.Length < size) throw new ArgumentNullException();

        Sample[] values = new Sample[bytes.Length / size];
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                for (int i = 0; i < values.Length; i++)
                    values[i] = new(ptr_[i]);
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
                values[i] = new((short)((
                    bytes[j++] << 0x08) |
                    bytes[j++]));
        return values;
    }

    public unsafe static byte[] GetBytes(Sample[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(Sample)];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                {
                    ptr_[j++] = values[i].left;
                    ptr_[j++] = values[i].right;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                Sample value = values[i];
                bytes[j++] = (byte)(value.left >> 0x08);
                bytes[j++] = (byte)value.left;
                bytes[j++] = (byte)(value.right >> 0x08);
                bytes[j++] = (byte)value.right;
            }
        return bytes;
    }

    public unsafe static byte[] GetBytesMono(Sample[] values, bool littleEndian, bool right)
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
                    ptr_[i] = right ? values[i].right : values[i].left;
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                int value = right ? values[i].right : values[i].left;
                bytes[j++] = (byte)(value >> 0x08);
                bytes[j++] = (byte)value;
            }
        return bytes;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static byte[][] GetBytesSeparate(Sample[] values, bool littleEndian) => default;

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params Sample[] values)
    {
        string str = "";
        foreach (Sample value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParse(string str, out Sample value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseAny(string str, bool littleEndian, out Sample value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseBin(string str, bool littleEndian, out Sample value)
    {
        value = default;
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static bool TryParseHex(string str, bool littleEndian, out Sample value)
    {
        value = default;
        return default;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(Sample)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                short* ptr_ = (short*)ptr;
                ptr_[0] = left;
                ptr_[1] = right;
            }
        else
        {
            bytes[0] = (byte)(left >> 0x08);
            bytes[1] = (byte)left;
            bytes[2] = (byte)(right >> 0x08);
            bytes[3] = (byte)right;
        }
        return bytes;
    }
}