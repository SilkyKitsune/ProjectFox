using System;
using D = System.Diagnostics.Debug;

using ProjectFox.CoreEngine.Math;
using M = ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Visuals;
using ProjectFox.GameEngine.Physics;

using ProjectFox.Windows;
using static ProjectFox.Windows.WM;

namespace ProjectFox.TestBed;

public sealed class TestWindow : DrawingWindow
{
    public static void GameWindowTest()
    {
        Engine.Frequency = 5;
        Screen.position = new(0, 0);
        Screen.Size = new(100, 100);
        Screen.Scale = 2.5f;
        Screen.OneToOne = false;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;

        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128)
        };

        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        GameWindow window = new("Test Window", 50, 50, true);

        scene.AddObject(new WindowTestObject(new("TestObj", 0), window)
        {
            size = new(25, 25),
            drawShape = true
        });

        window.Start();
    }

    private class WindowTestObject : KinematicScannerRectangle
    {
        internal WindowTestObject(NameID name, GameWindow window) : base(name) => this.window = window;

        private readonly GameWindow window; 

        protected override void PreFrame()
        {
            KeyboardMouseState kbm = window.KeyboardMouseState;

            Vector pos = Position;
            switch (M.FindSign(kbm.Left, kbm.Right))
            {
                case M.Sign.Neg:
                    pos.x -= 1;
                    break;
                case M.Sign.Pos:
                    pos.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.Up, kbm.Down))
            {
                case M.Sign.Neg:
                    pos.y -= 1;
                    break;
                case M.Sign.Pos:
                    pos.y += 1;
                    break;
            }
            Position = pos;

            switch (M.FindSign(kbm.J, kbm.L))
            {
                case M.Sign.Neg:
                    size.x -= 1;
                    break;
                case M.Sign.Pos:
                    size.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.I, kbm.K))
            {
                case M.Sign.Neg:
                    size.y -= 1;
                    break;
                case M.Sign.Pos:
                    size.y += 1;
                    break;
            }

            if (kbm.Return)
                Screen.FullScreen = true;
            else if (kbm.Back)
                Screen.FullScreen = false;
            else if (kbm.NumpadZero)
                Screen.OneToOne = true;
            else if (kbm.NumpadOne)
                Screen.OneToOne = false;
            else if (kbm.Space)
                Screen.Size = new(150, 150);

            /*Scene scene = Scene;
            if (scene != null)
                scene.bgColor = new(scene.bgColor.r, scene.bgColor.g,
                    scene.bgColor.b == byte.MaxValue ? byte.MinValue :
                    (byte)M.Clamp(scene.bgColor.b + 1, byte.MinValue, byte.MaxValue));*/
        }
    }

    public TestWindow() : base("Test Window",
        CS.VRedraw | CS.HRedraw | CS.DblClks, WindowColors.ScrollBar,
        WS.OverlappedWindow, 50, 100, 500, 300) { }

    protected override void OnStart() => D.WriteLine("Started");

    protected override void OnClose() => D.WriteLine("Cleaning Up");

    protected override IntPtr WindowProc(IntPtr windowHandle, WM message, UIntPtr wideParam, IntPtr longParam)
    {
        int high, low;
        switch (message)
        {
            case Quit:
                D.WriteLine(message);
                break;
            case Close:
                D.WriteLine(message);
                break;
            case Destroy:
                D.WriteLine(message);
                break;

            case Move:
                SeparateParam(longParam, out high, out low);
                D.WriteLine($"{message} ({(short)low}, {(short)high}) {Region}");
                break;
            case WM.Size:
                SeparateParam(longParam, out high, out low);
                D.WriteLine($"{message} ({low}, {high}) {Region}");
                break;

            case EraseBkgnd:
                //D.WriteLine(message);
                break;

            #region KeyboardMouse
            case KeyDown:
                {
                    VK key = (VK)wideParam;
                    switch (key)
                    {
                        case VK.Escape:
                            SendCloseMessage();
                            break;
                        case VK.Space:
                            SendRedrawMessage(new Color[4] { 0xFF0000FF, 0x00FF00FF, 0x0000FFFF, 0xFF00FFFF }, new(2, 2));
                            break;
                        case VK.NumpadZero:
                            Region = new(500, 500, 300, 500);
                            break;
                        case VK.NumpadOne:
                            Position = new(500, 500);
                            break;
                        case VK.NumpadTwo:
                            Size = new(300, 500);
                            break;
                        case VK.Return:
                            SetWindowStyle(WS.PopUp);
                            break;
                        case VK.A:
                            D.WriteLine(Region);
                            break;
                    }
                    D.WriteLine($"{message} wide={key}, long={longParam}");
                    break;
                }
            case KeyUp:
                D.WriteLine($"{message} wide={(VK)wideParam}, long={longParam}");
                break;

            case SysKeyDown:
                D.WriteLine($"{message} wide={(VK)wideParam}, long={longParam}");
                break;
            case SysKeyUp:
                D.WriteLine($"{message} wide={(VK)wideParam}, long={longParam}");
                break;

            case MouseMove:
                //IntToShorts(longParam, out int y, out int x);
                //D.WriteLine($"{message} wide={wideParam}, long=({x}, {y})");
                break;
            case MouseWheel:
                SeparateParam(/*(int)*/wideParam/*.ToUInt32()*/, out high, out low);
                D.WriteLine($"{message} wide=({(short)high}, {low}), long={longParam}");
                break;

            case LButtonDown:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case LButtonUp:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case LButtonDblClk:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;

            case NCLButtonDown:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case NCLButtonUp:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case NCLButtonDblClk:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;

            case RButtonDown:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case RButtonUp:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case RButtonDblClk:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;

            case MButtonDown:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case MButtonUp:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            case MButtonDblClk:
                D.WriteLine($"{message} wide={wideParam}, long={longParam}");
                break;
            #endregion

            /*default:
                D.WriteLine(message);
                break;*/
        }
        return base.WindowProc(windowHandle, message, wideParam, longParam);
    }
}