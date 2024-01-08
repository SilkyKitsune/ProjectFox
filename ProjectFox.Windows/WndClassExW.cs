using System;
using System.Runtime.InteropServices;

namespace ProjectFox.Windows;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto)]
internal struct WndClassExW
{
    internal uint cbSize, style;
    internal WndProc lpfnWndProc;
    internal int cbClsExtra, cbWndExtra;
    internal IntPtr hInstance, hIcon, hCursor, hbrBackground;
    internal string lpszMenuName, lpszClassName;
    internal IntPtr hIconSm;

#if DEBUG
    public override string ToString() =>//remove?
        $" cbSize: {cbSize}\n" +
        $" style: {style}\n" +
        $" lpfnWndProc: {lpfnWndProc}\n" +
        $" cbClsExtra: {cbClsExtra}\n" +
        $" cbWndExtra: {cbWndExtra}\n" +
        $" hInstance: {hInstance}\n" +
        $" hIcon: {hIcon}\n" +
        $" hCursor: {hCursor}\n" +
        $" hbrBackground: {hbrBackground}\n" +
        $" lpszMenuName: {lpszMenuName}\n" +
        $" lpszClassName: {lpszClassName}\n" +
        $" hIconSm: {hIconSm}";
#endif
}