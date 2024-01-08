using System;
using System.Runtime.InteropServices;

namespace ProjectFox.Windows;

internal static class WinGDI
{
    private const string gdi32 = "gdi32.dll";

    [DllImport(gdi32)] internal static extern int SetStretchBltMode(IntPtr hdc, int mode);

    [DllImport(gdi32)] internal static extern int StretchDIBits(
        IntPtr hdc,
        int xDest, int yDest, int destWidth, int destHeight,
        int xSrc, int ySrc, int srcWidth, int srcHeight,
        uint[] lpBits, BitmapInfo lpbmi, uint iUsage, uint rop);
}