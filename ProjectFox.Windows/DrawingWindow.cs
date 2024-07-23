using System;
using System.Runtime.InteropServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.Windows;

public abstract class DrawingWindow : InputWindow
{
    public DrawingWindow(string windowName, CS classStyle, WindowColors backgroundColor, WS windowStyle, int x, int y, int width, int height)
        : base(windowName, classStyle, backgroundColor, windowStyle, x, y, width, height) { }

    private int stretchMode = -1;

    private Color[] buffer = null;
    private Vector bufferDimensions = default;

    protected void SendRedrawMessage(Color[] pixels, Vector dimensions)//should this have a control bool for minimized?
    {
        if (Minimized || pixels == null || pixels.Length == 0 || dimensions.x <= 0 || dimensions.y <= 0) return;

        buffer = pixels;
        bufferDimensions = dimensions;
        WinUser.RedrawWindow(windowHandle, IntPtr.Zero, IntPtr.Zero, 0x0001u);
    }

    protected override IntPtr WindowProc(IntPtr windowHandle, WM message, UIntPtr wideParam, IntPtr longParam)
    {
        switch (message)
        {
            case WM.Paint:
                {
                    if (buffer != null)
                    {
                        Color[] pixels;
                        Vector dimensions = bufferDimensions;

                        lock (buffer)
                        {
                            pixels = buffer;
                            buffer = null;
                        }

                        if (pixels.Length > 0 && dimensions.x > 0 && dimensions.y > 0)
                        {
                            PaintStruct ps = default;
                            IntPtr hdc = WinUser.BeginPaint(windowHandle, ref ps);

                            if (stretchMode == -1)
                            {
                                stretchMode = WinGDI.SetStretchBltMode(hdc, 3);

                                if (stretchMode == 0 || stretchMode == 87)
                                    throw new Exception($"StretchMode could not be set, fatal error! {stretchMode}");
                            }
                            
                            BitmapInfo bmi = new()
                            {
                                bmiHeader = new()
                                {
                                    biSize = (uint)Marshal.SizeOf(typeof(BitmapInfo.BitmapInfoHeader)),//change
                                    biWidth = dimensions.x,
                                    biHeight = dimensions.y,
                                    biPlanes = 1,
                                    biBitCount = 32
                                }
                            };

                            uint[] finalBuffer = new uint[pixels.Length];
                            for (int i = 0; i < pixels.Length; i++)
                                finalBuffer[i] = pixels[i].hex >> 8;
                            
                            Vector size = Size;
                            WinGDI.StretchDIBits(hdc,
                                //0, 0, ps.rcPaint.right - ps.rcPaint.left, ps.rcPaint.bottom - ps.rcPaint.top,//this squishes based on window visibility
                                //0, 0, size.x, size.y,//this is upside down
                                0, size.y, size.x, -size.y,//this vflips
                                //0, size.y / 2, size.x / 2, -size.y / 2,

                                0, 0, dimensions.x, dimensions.y,
                                finalBuffer, bmi, 0, 0x00CC0020);

                            WinUser.EndPaint(windowHandle, ps);
                        }
                    }
                    break;
                }
            default:
                return base.WindowProc(windowHandle, message, wideParam, longParam);
        }
        return IntPtr.Zero;
    }
}