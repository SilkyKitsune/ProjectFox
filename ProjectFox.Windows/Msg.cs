using System;
using System.Runtime.InteropServices;

namespace ProjectFox.Windows;

[StructLayout(LayoutKind.Sequential)]
internal struct Msg
{
    [StructLayout(LayoutKind.Sequential)]
    private struct Point { private int x, y; }

    internal IntPtr hWnd;
    internal UIntPtr message, wParam;
    internal IntPtr lParam;
    internal uint time;
    private Point pt;
}