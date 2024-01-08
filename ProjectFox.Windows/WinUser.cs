using System;
using System.Runtime.InteropServices;

namespace ProjectFox.Windows;

[UnmanagedFunctionPointer(CallingConvention.Winapi)]
internal delegate IntPtr WndProc(IntPtr hWnd, uint message, UIntPtr wParam, IntPtr lParam);

internal static class WinUser
{
    private const string user32 = "user32.dll";

    [DllImport("kernel32.dll")] internal static extern uint GetLastError();

    [DllImport(user32)] internal static extern ushort RegisterClassEx(ref WndClassExW lpWndClass);

    [DllImport(user32)] internal static extern bool AdjustWindowRect(ref PaintStruct.Rect lpRect, uint dwStyle, bool bMenu);

    [DllImport(user32)] internal static extern IntPtr CreateWindowEx(
        uint dwExStyle, ushort lpClassName, string lpWindowName, uint dwStyle,
        int x, int y, int nWidth, int nHeight,
        IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

    [DllImport(user32)] internal static extern int ShowWindow(IntPtr hWnd, int nCmdShow);

    [DllImport(user32)] internal static extern int UpdateWindow(IntPtr hWnd);

    [DllImport(user32)] internal static extern int RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);

    [DllImport(user32)] internal static extern int DestroyWindow(IntPtr hWnd);

    [DllImport(user32)] internal static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

    [DllImport(user32)] internal static extern UIntPtr SetWindowLongPtr(IntPtr hWnd, int nIndex, UIntPtr dwNewLong);

    [DllImport(user32)] internal static extern IntPtr DefWindowProc(IntPtr hWnd, uint message, UIntPtr wParam, IntPtr lParam);

    [DllImport(user32)] internal static extern sbyte GetMessage(out Msg lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);

    [DllImport(user32)] internal static extern bool TranslateMessage(ref Msg lpMsg);

    [DllImport(user32)] internal static extern IntPtr DispatchMessageW(ref Msg lpMsg);//this is W explicitly

    [DllImport(user32)] internal static extern bool PostMessage(IntPtr hWnd, uint Msg, UIntPtr wParam, IntPtr lParam);

    [DllImport(user32)] internal static extern void PostQuitMessage(int nExitCode);

    [DllImport(user32)] internal static extern IntPtr BeginPaint(IntPtr hWnd, ref PaintStruct ps);

    [DllImport(user32)] internal static extern bool EndPaint(IntPtr hWnd, PaintStruct ps);
}