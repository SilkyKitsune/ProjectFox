using System;
using System.Runtime.InteropServices;
using ProjectFox.CoreEngine.Math;
using static ProjectFox.Windows.WinUser;

namespace ProjectFox.Windows;

/// <summary> Base class for building a window using WinUser/WinGDI </summary>
public abstract class Window
{
    private const string ClassName = "ProjectFoxWindow";

    protected static void SeparateParam(IntPtr param, out int high, out int low)
    {
        long l = param.ToInt64();
        high = (int)((l & 0xFFFF0000) >> 0x10);
        low = (int)(l & 0x0000FFFF);
    }//anyway to combine these?
    protected static void SeparateParam(UIntPtr param, out int high, out int low)
    {
        ulong l = param.ToUInt64();
        high = (int)((l & 0xFFFF0000) >> 0x10);
        low = (int)(l & 0x0000FFFF);
    }

    /// <param name="windowName"> Name displayed on the window </param>
    /// <param name="classStyle"> Class style used by the window </param>
    /// <param name="backgroundColor"> Color used by the window (if equal to WindowColors.ScrollBar default behavior for the EraseBkgnd message will be disabled) </param>
    /// <param name="windowStyle"> Window style used by the window </param>
    /// <param name="x"> X position of the window </param>
    /// <param name="y"> Y position of the window </param>
    /// <param name="width"> Width of the window </param>
    /// <param name="height"> Height of the window </param>
    /// <remarks> Invalid width or height values throw an exception </remarks>
    /// <exception cref="Exception"></exception>
    public Window(string windowName, CS classStyle, WindowColors backgroundColor, WS windowStyle, int x, int y, int width, int height)
    {
        if (width <= 0 || height <= 0)
            throw new Exception($"Invalid window size ({width}, {height})");

        windowStyle &= ~WS.Visible;
        
        IntPtr hInstance = Marshal.GetHINSTANCE(typeof(Window).Module);
        
        WndClassExW wcexw = new()
        {
            cbSize = (uint)Marshal.SizeOf(typeof(WndClassExW)),
            style = (uint)classStyle,
            hbrBackground = new((int)backgroundColor),
            cbClsExtra = 0,
            cbWndExtra = 0,
            hInstance = hInstance,
            hIcon = IntPtr.Zero,
            hCursor = IntPtr.Zero,
            lpszMenuName = null,
            lpszClassName = ClassName,
            lpfnWndProc = (hWnd, message, wParam, lParam) => WindowProc(hWnd, (WM)message, wParam, lParam),
            hIconSm = IntPtr.Zero
        };
        
        ushort atom = RegisterClassEx(ref wcexw);

        if (atom == 0)
            throw new Exception($"Window could not be registered, fatal error! error={GetLastError()}");

        PaintStruct.Rect rect = new()
        {
            left = x,
            top = y,
            right = x + width,
            bottom = y + height
        };
        AdjustWindowRect(ref rect, (uint)windowStyle, false);

        IntPtr hWnd = CreateWindowEx(0u, atom,
            windowName, (uint)windowStyle,
            x, y, rect.right - rect.left, rect.bottom - rect.top,
            IntPtr.Zero, IntPtr.Zero, wcexw.hInstance, IntPtr.Zero);
        
        if (hWnd.Equals(IntPtr.Zero))
            throw new Exception($"Handle could not be generated, fatal error! error={GetLastError()}");

        windowClass = wcexw;
        windowHandle = hWnd;
        this.windowStyle = windowStyle;
        region = new(x, y, width, height);
    }

    private readonly WndClassExW windowClass;
    private protected readonly IntPtr windowHandle;
    private WS windowStyle;
    private Rectangle region;
    private bool started = false, minimized = false;//can it be minimized at start?

    //bool topmost

    public bool Minimized => minimized;

    public Rectangle Region
    {
        get => region;
        protected set
        {
            SWP flags = 0;

            if (value.size.x <= 0 || value.size.y <= 0 || region.size.Equals(value.size))
                flags |= SWP.NoSize;
            else
            {
                region.size = value.size;

                Vector end = region.EndPoint;
                PaintStruct.Rect rect = new()
                {
                    left = region.position.x,
                    top = region.position.y,
                    right = end.x,
                    bottom = end.y
                };
                AdjustWindowRect(ref rect, (uint)windowStyle, false);

                value.size = new(rect.right - rect.left, rect.bottom - rect.top);
            }

            if (region.position.Equals(value.position))
                flags |= SWP.NoMove;
            else region.position = value.position;

            if (flags != (SWP.NoSize | SWP.NoMove))
                SetWindowPos(windowHandle, IntPtr.Zero,
                    value.position.x, value.position.y, value.size.x, value.size.y,
                    (uint)(flags | SWP.NoZOrder));
        }
    }

    public Vector Position
    {
        get => region.position;
        protected set
        {
            if (!region.position.Equals(value))
            {
                region.position = value;
                SetWindowPos(windowHandle, IntPtr.Zero,
                    value.x, value.y, 0, 0,
                    (uint)(SWP.NoSize | SWP.NoZOrder));
            }
        }
    }

    public Vector Size
    {
        get => region.size;
        protected set
        {
            if (value.x > 0 && value.y > 0 && !region.size.Equals(value))
            {
                region.size = value;

                Vector end = region.EndPoint;
                PaintStruct.Rect rect = new()
                {
                    left = region.position.x,
                    top = region.position.y,
                    right = end.x,
                    bottom = end.y
                };
                AdjustWindowRect(ref rect, (uint)windowStyle, false);

                SetWindowPos(windowHandle, IntPtr.Zero,
                    0, 0, rect.right - rect.left, rect.bottom - rect.top,
                    (uint)(SWP.NoMove | SWP.NoZOrder));
            }
        }
    }

    /// <summary> Begins processing the window </summary>
    /// <exception cref="Exception"></exception>
    public void Start()
    {
        if (started)
            throw new Exception("Window is already started!");

        if (ShowWindow(windowHandle, 1) != 0)
            throw new Exception($"Window was already visible, fatal error! error={GetLastError()}");

        if (UpdateWindow(windowHandle) == 0)
            throw new Exception($"UpdateWindow failed, fatal error! error={GetLastError()}");

        started = true;

        OnStart();

        while (GetMessage(out Msg msg, IntPtr.Zero, 0, 0) != 0)
        {
            TranslateMessage(ref msg);
            DispatchMessageW(ref msg);
        }
    }

    /// <summary> Sends a message telling the window to begin closing </summary>
    protected void SendCloseMessage() => PostMessage(windowHandle, (uint)WM.Close, UIntPtr.Zero, IntPtr.Zero);

    protected void SetWindowStyle(WS style/*, bool topMost*/)
    {
        windowStyle = style;
        SetWindowLongPtr(windowHandle, -16, new((uint)(style | WS.Visible)));

        Vector end = region.EndPoint;
        PaintStruct.Rect rect = new()
        {
            left = region.position.x,
            top = region.position.y,
            right = end.x,
            bottom = end.y
        };
        AdjustWindowRect(ref rect, (uint)windowStyle, false);

        SetWindowPos(windowHandle, IntPtr.Zero,
            region.position.x, region.position.y, 
            rect.right - rect.left, rect.bottom - rect.top,
            (uint)(SWP.NoZOrder | SWP.FrameChanged));
    }

    protected virtual IntPtr WindowProc(IntPtr windowHandle, WM message, UIntPtr wideParam, IntPtr longParam)
    {
        switch (message)
        {
            case WM.Move:
                SeparateParam(longParam, out region.position.y, out region.position.x);//does this make sense to do?
                break;
            case WM.Size://I think minimize sets region to zero
                minimized = wideParam.ToUInt64() == 1uL;
                SeparateParam(longParam, out region.size.y, out region.size.x);//does this make sense to do? maybe it shouldn't happen when minimized
                break;
            case WM.Close:
                OnClose();
                DestroyWindow(windowHandle);
                break;
            case WM.Destroy:
                PostQuitMessage(0);
                break;
            default:
                return DefWindowProc(windowHandle, (uint)message, wideParam, longParam);
        }
        return IntPtr.Zero;
    }

    protected virtual void OnStart() { }

    protected virtual void OnClose() { }
}