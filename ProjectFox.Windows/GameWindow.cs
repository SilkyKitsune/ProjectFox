using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Visuals;
using ProjectFox.GameEngine.Input;

namespace ProjectFox.Windows;

public sealed class GameWindow : DrawingWindow
{
    private const WS FixedWindow = WS.Caption | WS.SysMenu | WS.MinimizeBox, BorderlessWindow = WS.PopUp | WS.MinimizeBox;

    public GameWindow(string windowName, int x, int y, bool windowStartsEngineThread = false)
        : base(windowName,
            CS.VRedraw | CS.HRedraw,
            WindowColors.ScrollBar,
            Screen.FullScreen ? BorderlessWindow : FixedWindow,
            Screen.FullScreen ? 0 : x,
            Screen.FullScreen ? 0 : y,
            Screen.GetScaledSize().x,
            Screen.GetScaledSize().y)
    {
        this.windowStartsEngineThread = windowStartsEngineThread;
        Engine.FrameBegin += FrameBegin;
        Engine.FrameComplete += FrameComplete;
        Screen.sizeChanged += SizeChanged;
        Screen.scaleChanged += SizeChanged;
        Screen.oneToOneChanged += SizeChanged;
        Screen.fullScreenChanged += FullScreenChanged;
    }

    private readonly bool windowStartsEngineThread;//rename?

    public readonly KeyboardMouseDevice kbdMouse = new();//rename?

    //bool matchScreenScale, updateScreenVisibility;
    //vector screenOffset, could this go in screen?

    private void FrameBegin()//update screen.visible with minimized?
    {
        KeyboardMouseState kbm = KeyboardMouseState;
        kbdMouse.UpdateValues(new bool[]
            {
                kbm.LButton, kbm.RButton, kbm.MButton, kbm.XButton1, kbm.XButton2,

                kbm.Escape,

                kbm.Return, kbm.Space, kbm.Tab, kbm.Insert, kbm.Back, kbm.Delete,

                kbm.Shift, kbm.LShift, kbm.RShift,

                kbm.Control, kbm.LControl, kbm.RControl,
                kbm.Menu, kbm.LMenu, kbm.RMenu,
                kbm.LWin, kbm.RWin,
                kbm.Apps,
                kbm.SnapShot,

                kbm.Capital, kbm.Scroll,

                kbm.Home, kbm.End, kbm.Prior, kbm.Next,
                kbm.Left, kbm.Right, kbm.Up, kbm.Down,

                kbm.F1, kbm.F2, kbm.F3, kbm.F4, kbm.F5, kbm.F6, kbm.F7, kbm.F8, kbm.F9, kbm.F10, kbm.F11, kbm.F12,

                kbm.Zero, kbm.One, kbm.Two, kbm.Three, kbm.Four, kbm.Five, kbm.Six, kbm.Seven, kbm.Eight, kbm.Nine,

                kbm.A, kbm.B, kbm.C, kbm.D, kbm.E, kbm.F, kbm.G, kbm.H, kbm.I, kbm.J, kbm.K, kbm.L, kbm.M, kbm.N, kbm.O, kbm.P, kbm.Q, kbm.R, kbm.S, kbm.T, kbm.U, kbm.V, kbm.W, kbm.X, kbm.Y, kbm.Z,

                kbm.OEMPlus, kbm.OEMMinus,
                kbm.OEM4, kbm.OEM6,
                kbm.OEM1, kbm.OEM7,
                kbm.OEMComma, kbm.OEMPeriod,
                kbm.OEM2, kbm.OEM5,
                kbm.OEM3,

                kbm.NumLock,
                kbm.NumpadZero, kbm.NumpadOne, kbm.NumpadTwo, kbm.NumpadThree, kbm.NumpadFour,
                kbm.NumpadFive, kbm.NumpadSix, kbm.NumpadSeven, kbm.NumpadEight, kbm.NumpadNine,
                kbm.Add, kbm.Subtract, kbm.Multiply, kbm.Divide,
                kbm.Decimal, kbm.Separator
            }, null, null, new Vector[] { new(0, kbm.mouseWheel) }, new Vector[] { kbm.mousePosition });

        //if (updateScreenVisibility) Screen.visible = !Minimized;
    }

    private void FrameComplete()
    {
        if (Screen.visible) SendRedrawMessage(Screen.GetFrame(), Screen.Size);
    }

    private void SizeChanged() => Size = Screen.GetScaledSize();

    private void FullScreenChanged() => SetWindowStyle(Screen.FullScreen ? BorderlessWindow : FixedWindow);

    protected sealed override void OnStart()
    {
        if (windowStartsEngineThread) Engine.Start();
    }

    protected sealed override void OnClose()
    {
        Engine.Stop();
#if DEBUG
        Debug.Console.Shutdown();
#endif
    }
}