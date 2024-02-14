using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params RectangleF[] values)
    {
        string str = "";
        foreach (RectangleF value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params RectangleF[] values)
    {
        string str = "";
        foreach (RectangleF value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static RectangleF FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(RectangleF)) throw new ArgumentNullException();

        float px = 0f, py = 0f, sx = 0f, sy = 0f;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                px = ptr_[0];
                py = ptr_[1];
                sx = ptr_[2];
                sy = ptr_[3];
            }
        else
        {
            int px_ = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3],
            py_ = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7],
            sx_ = (
                bytes[8] << 0x18) | (
                bytes[9] << 0x10) | (
                bytes[10] << 0x08) |
                bytes[11],
            sy_ = (
                bytes[12] << 0x18) | (
                bytes[13] << 0x10) | (
                bytes[14] << 0x08) |
                bytes[15];
            px = *(float*)&px_;
            py = *(float*)&py_;
            sx = *(float*)&sx_;
            sy = *(float*)&sy_;
        }
        return new(px, py, sx, sy);
    }

    public unsafe static RectangleF[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(RectangleF);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        RectangleF[] values = new RectangleF[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++]);
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                Rectangle v = new((
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
                values[i] = *(RectangleF*)&v;
            }
        return values;
    }

    public unsafe static byte[] GetBytes(RectangleF[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(RectangleF)];

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
                    ptr_[j++] = values[i].position.x;
                    ptr_[j++] = values[i].position.y;
                    ptr_[j++] = values[i].size.x;
                    ptr_[j++] = values[i].size.y;
                }
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                RectangleF value_ = values[i];
                Rectangle value = *(Rectangle*)&value_;
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

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params RectangleF[] values)
    {
        string str = "";
        foreach (RectangleF value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params RectangleF[] values)
    {
        string str = "";
        foreach (RectangleF value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(RectangleF)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                ptr_[0] = position.x;
                ptr_[1] = position.y;
                ptr_[2] = size.x;
                ptr_[3] = size.y;
            }
        else
        {
            float px_ = this.position.x, py_ = this.position.y, sx_ = this.size.x, sy_ = this.size.y;
            int px = *(int*)&px_, py = *(int*)&py_, sx = *(int*)&sx_, sy = *(int*)&sy_;
            bytes[0] = (byte)(px >> 0x18);
            bytes[1] = (byte)(px >> 0x10);
            bytes[2] = (byte)(px >> 0x08);
            bytes[3] = (byte)px;
            bytes[4] = (byte)(py >> 0x18);
            bytes[5] = (byte)(py >> 0x10);
            bytes[6] = (byte)(py >> 0x08);
            bytes[7] = (byte)py;
            bytes[8] = (byte)(sx >> 0x18);
            bytes[9] = (byte)(sx >> 0x10);
            bytes[10] = (byte)(sx >> 0x08);
            bytes[11] = (byte)sx;
            bytes[12] = (byte)(sy >> 0x18);
            bytes[13] = (byte)(sy >> 0x10);
            bytes[14] = (byte)(sy >> 0x08);
            bytes[15] = (byte)sy;
        }
        return bytes;
    }
}
