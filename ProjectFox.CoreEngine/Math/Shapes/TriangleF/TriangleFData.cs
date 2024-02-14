using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct TriangleF
{
    public static string ConcatHex(bool littleEndian, bool leadingText, params TriangleF[] values)
    {
        string str = "";
        foreach (TriangleF value in values) str += value.ToHexString(littleEndian, leadingText);
        return str;
    }

    public static string ConcatBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, params TriangleF[] values)
    {
        string str = "";
        foreach (TriangleF value in values) str += value.ToBinString(littleEndian, leadingText);
        return str;
    }

    public unsafe static TriangleF FromBytes(byte[] bytes, bool littleEndian)
    {
        if (bytes == null || bytes.Length < sizeof(TriangleF)) throw new ArgumentNullException();

        float ax = 0f, ay = 0f, bx = 0f, by = 0f, cx = 0f, cy = 0f;
#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                ax = ptr_[0];
                ay = ptr_[1];
                bx = ptr_[2];
                by = ptr_[3];
                cx = ptr_[4];
                cy = ptr_[5];
            }
        else
        {
            int ax_ = (
                bytes[0] << 0x18) | (
                bytes[1] << 0x10) | (
                bytes[2] << 0x08) |
                bytes[3],
            ay_ = (
                bytes[4] << 0x18) | (
                bytes[5] << 0x10) | (
                bytes[6] << 0x08) |
                bytes[7],
            bx_ = (
                bytes[8] << 0x18) | (
                bytes[9] << 0x10) | (
                bytes[10] << 0x08) |
                bytes[11],
            by_ = (
                bytes[12] << 0x18) | (
                bytes[13] << 0x10) | (
                bytes[14] << 0x08) |
                bytes[15],
            cx_ = (
                bytes[16] << 0x18) | (
                bytes[17] << 0x10) | (
                bytes[18] << 0x08) |
                bytes[19],
            cy_ = (
                bytes[20] << 0x18) | (
                bytes[21] << 0x10) | (
                bytes[22] << 0x08) |
                bytes[23];
            ax = *(float*)&ax_;
            ay = *(float*)&ay_;
            bx = *(float*)&bx_;
            by = *(float*)&by_;
            cx = *(float*)&cx_;
            cy = *(float*)&cy_;
        }
        return new(ax, ay, bx, by, cx, cy);
    }

    public unsafe static TriangleF[] FromBytesMultiple(byte[] bytes, bool littleEndian)
    {
        int size = sizeof(TriangleF);

        if (bytes == null || bytes.Length < size) throw new ArgumentException();

        TriangleF[] values = new TriangleF[bytes.Length / size];

#if BIGENDIAN
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                for (int i = 0, j = 0; i < values.Length; i++)
                    values[i] = new(ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++], ptr_[j++]);
            }
        else for (int i = 0, j = 0; i < values.Length; i++)
            {
                Triangle v = new((
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
                values[i] = *(TriangleF*)&v;
            }
        return values;
    }

    public unsafe static byte[] GetBytes(TriangleF[] values, bool littleEndian)
    {
        if (values == null || values.Length == 0) throw new ArgumentException();

        byte[] bytes = new byte[values.Length * sizeof(TriangleF)];

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
                TriangleF value_ = values[i];
                Triangle value = *(Triangle*)&value_;
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

    public static string JoinHex(bool littleEndian, bool leadingText, string separator, params TriangleF[] values)
    {
        string str = "";
        foreach (TriangleF value in values) str += value.ToHexString(littleEndian, leadingText) + separator;
        return str;
    }

    public static string JoinBin(bool littleEndian, bool leadingText, char byteSeparator, char nibbleSeparator, string elementSeparator, params TriangleF[] values)
    {
        string str = "";
        foreach (TriangleF value in values) str += value.ToBinString(littleEndian, leadingText) + elementSeparator;
        return str;
    }

    public unsafe byte[] GetBytes(bool littleEndian)
    {
        byte[] bytes = new byte[sizeof(TriangleF)];
#if BIGENDIAN 
        if (!littleEndian)
#else
        if (littleEndian)
#endif
            fixed (byte* ptr = bytes)
            {
                float* ptr_ = (float*)ptr;
                ptr_[0] = a.x;
                ptr_[1] = a.y;
                ptr_[2] = b.x;
                ptr_[3] = b.y;
                ptr_[4] = c.x;
                ptr_[5] = c.y;
            }
        else
        {
            float ax_ = this.a.x, ay_ = this.a.y, bx_ = this.b.x, by_ = this.b.y, cx_ = this.c.x, cy_ = this.c.y;
            int ax = *(int*)&ax_, ay = *(int*)&ay_, bx = *(int*)&bx_, by = *(int*)&by_, cx = *(int*)&cx_, cy = *(int*)&cy_;
            bytes[0] = (byte)(ax >> 0x18);
            bytes[1] = (byte)(ax >> 0x10);
            bytes[2] = (byte)(ax >> 0x08);
            bytes[3] = (byte)ax;
            bytes[4] = (byte)(ay >> 0x18);
            bytes[5] = (byte)(ay >> 0x10);
            bytes[6] = (byte)(ay >> 0x08);
            bytes[7] = (byte)ay;
            bytes[8] = (byte)(bx >> 0x18);
            bytes[9] = (byte)(bx >> 0x10);
            bytes[10] = (byte)(bx >> 0x08);
            bytes[11] = (byte)bx;
            bytes[12] = (byte)(by >> 0x18);
            bytes[13] = (byte)(by >> 0x10);
            bytes[14] = (byte)(by >> 0x08);
            bytes[15] = (byte)by;
            bytes[16] = (byte)(cx >> 0x18);
            bytes[17] = (byte)(cx >> 0x10);
            bytes[18] = (byte)(cx >> 0x08);
            bytes[19] = (byte)cx;
            bytes[20] = (byte)(cy >> 0x18);
            bytes[21] = (byte)(cy >> 0x10);
            bytes[22] = (byte)(cy >> 0x08);
            bytes[23] = (byte)cy;
        }
        return bytes;
    }
}
