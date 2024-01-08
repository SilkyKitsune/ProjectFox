using System;
using System.Runtime.InteropServices;

namespace ProjectFox.Windows;

[StructLayout(LayoutKind.Sequential)]
internal struct BitmapInfo
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct BitmapInfoHeader
    {
        internal uint biSize;
        internal int biWidth, biHeight;
        internal ushort biPlanes, biBitCount;
        internal int biCompression, biSizeImage;
        internal int biXPelsPerMeter, biYPelPerMeter;
        internal uint biClrUsed, biClrImportant;
    }

    [StructLayout(LayoutKind.Sequential)]
    internal struct RGBQuad { internal byte rgbBlue, rgbGreen, rgbRed, rgbReserved; }

    internal BitmapInfoHeader bmiHeader;
    internal RGBQuad bmiColors;
}