using System;
using System.Runtime.InteropServices;

namespace ProjectFox.Windows;

[StructLayout(LayoutKind.Sequential, Size = 64)]
internal struct PaintStruct
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Rect { internal int left, top, right, bottom; }

    internal IntPtr hdc;
    internal int fErase;
    internal Rect rcPaint;
    internal int fRestore, fIncUpdate;
    internal IntPtr rgbReserved;
}