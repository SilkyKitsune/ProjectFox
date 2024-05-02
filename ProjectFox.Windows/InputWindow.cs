using System;
using System.Runtime.CompilerServices;
using static ProjectFox.Windows.WM;

namespace ProjectFox.Windows;

public abstract class InputWindow : Window
{
    public InputWindow(string windowName, CS classStyle, WindowColors backgroundColor, WS windowStyle, int x, int y, int width, int height)
        : base(windowName, classStyle, backgroundColor, windowStyle, x, y, width, height) { }

    private KeyboardMouseState keyboardMouseState;

    public KeyboardMouseState KeyboardMouseState
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            KeyboardMouseState state = keyboardMouseState;
            keyboardMouseState.mouseWheel = 0;
            return state;
        }
    }

    protected override IntPtr WindowProc(IntPtr windowHandle, WM message, UIntPtr wideParam, IntPtr longParam)
    {
        switch (message)
        {
            #region Keyboard
            case KeyDown:
                //use longparam here?
                keyboardMouseState.UpdateKey((VK)wideParam.ToUInt32(), true);
                break;//will these throw exceptions?
            case KeyUp:
                keyboardMouseState.UpdateKey((VK)wideParam.ToUInt32(), false);
                break;

            //syskey?
            #endregion

            #region Mouse
            case MouseMove:
                SeparateParam(longParam,
                    out keyboardMouseState.mousePosition.y,
                    out keyboardMouseState.mousePosition.x);
                break;

            case MouseWheel:
                SeparateParam(wideParam, out int high, out _);
                keyboardMouseState.mouseWheel += (short)high / 120;
                break;

            case LButtonDown:
                keyboardMouseState.LButton = true;
                break;
            case LButtonUp:
                keyboardMouseState.LButton = false;
                break;

            case RButtonDown:
                keyboardMouseState.RButton = true;
                break;
            case RButtonUp:
                keyboardMouseState.RButton = false;
                break;

            case MButtonDown:
                keyboardMouseState.MButton = true;
                break;
            case MButtonUp:
                keyboardMouseState.MButton = false;
                break;
            #endregion

            default:
                return base.WindowProc(windowHandle, message, wideParam, longParam);
        }
        return IntPtr.Zero;
    }
}