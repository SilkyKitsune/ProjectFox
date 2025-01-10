using ProjectFox.CoreEngine.Math;
using M = ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using static ProjectFox.GameEngine.Debug;
using static ProjectFox.GameEngine.Debug.Console;
using ProjectFox.GameEngine.Visuals;
using ProjectFox.GameEngine.Input;

using ProjectFox.Windows;

namespace ProjectFox.TestBed;

public static class DebugNames
{
    public static readonly NameID TestScene = new("TestScn", 0);

    public static readonly string WindowName = "Test Window", TestRect = "TstRect";
}

public static class DebugColors
{
    public static readonly Color Black = new(0, 0, 0);
    public static readonly Color Grey = new(128, 128, 128);
    public static readonly Color White = new(255, 255, 255);

    public static readonly Color Red = new(255, 0, 0);
    public static readonly Color Green = new(0, 255, 0);
    public static readonly Color Blue = new(0, 0, 255);

    public static readonly Color Yellow = new(255, 255, 0);
    public static readonly Color Magenta = new(255, 0, 255);
    public static readonly Color Cyan = new(0, 255, 255);

    public static readonly Color Orange = new(255, 128, 0);
    public static readonly Color Purple = new(128, 0, 255);
}

public sealed class DebugScene : Scene
{
    private static byte b = 0;

    public DebugScene(int freq, bool uncapped, Vector size, float scale, bool oneToOne, bool fullScreen) : base(new("DbugScn", b++))
    {
        Engine.Frequency = freq;
        Engine.uncapped = uncapped;
        Screen.Size = size;
        Screen.Scale = scale;
        Screen.OneToOne = oneToOne;
        Screen.FullScreen = fullScreen;
        Debug.DrawDebug = true;

        //window = new("Debug Window", 50, 50, true);

        Engine.SceneList.Add(this);
        Engine.SceneList.ActiveScene = Name;
    }

    //private readonly GameWindow window;
}

public sealed class DebugController : Object
{
    private static readonly NameID ID = new("DbgCtrl", 0);

    public DebugController(KeyboardMouseDevice kbm) : base(ID)
    {
        pauseWalks = true;
        this.kbm = kbm;
    }

    private readonly KeyboardMouseDevice kbm;

    public bool printFrameInfo = false;

    private void SetFPS(int value)
    {
        Engine.Frequency = value;
        QueueMessage($"FPS={Engine.Frequency}");
    }

    private void SetSleepPeriod(float value)
    {
        Engine.SleepPeriod = value;
        QueueMessage($"RestPeriod={Engine.SleepPeriod}");
    }

    protected override void PrePhysics()
    {
        if (printFrameInfo)
        {
            float msPer = Engine.MillisecondsPerFrame, msPrev = Engine.MillisecondsOfLastFrame;
            QueueMessage($"{Engine.FrameCount}: {1000f / msPrev} {msPrev / msPer}");
        }

        if (kbm.Insert.ChangedTrue)
        {
            if (kbm.Ctrl)
            {
                byte value = (byte)(DebugAlpha == byte.MaxValue ? 128 : byte.MaxValue);
                DebugAlpha = value;
                QueueMessage($"DebugAlpha={value}");
            }
            else
            {
                bool value = !DrawDebug;
                DrawDebug = value;
                QueueMessage($"DrawDebug={value}");
            }
        }

        if (kbm.NumpadOne.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(0f);
            else SetFPS(1);
        else if (kbm.NumpadTwo.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(0.1f);
            else SetFPS(5);
        else if (kbm.NumpadThree.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(0.25f);
            else SetFPS(15);
        else if (kbm.NumpadFour.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(0.5f);
            else SetFPS(30);
        else if (kbm.NumpadFive.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(0.75f);
            else SetFPS(60);
        else if (kbm.NumpadSix.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(0.9f);
            else SetFPS(120);
        else if (kbm.NumpadSubtract.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(Engine.SleepPeriod - 0.01f);
            else SetFPS(Engine.Frequency - 1);
        else if (kbm.NumpadAdd.ChangedTrue)
            if (kbm.Ctrl) SetSleepPeriod(Engine.SleepPeriod + 0.01f);
            else SetFPS(Engine.Frequency + 1);
        
        int moveSpeed = kbm.Shift ? 4 : 1;

        if (kbm.Delete) Screen.position.x -= moveSpeed;
        else if (kbm.PageDown) Screen.position.x += moveSpeed;

        if (kbm.Home) Screen.position.y -= moveSpeed;
        else if (kbm.End) Screen.position.y += moveSpeed;

        if (kbm.NumpadZero.ChangedTrue)
        {
            Screen.position = default;
            QueueMessage("Screen pos reset");
        }

        if (kbm.BackSlash.ChangedTrue) Screen.visible = !Screen.visible;

        //if (kbm.RightBracket.ChangedTrue) Speakers.audible = !Speakers.audible;
    }
}

public sealed class MouseDrawer : Object2D
{
    private static readonly NameID ID = new("MousDrw", 0);

    public MouseDrawer(GameWindow window) : base(ID)
    {
        pauseWalks = true;
        drawPosition = true;
        positionColor = DebugColors.Magenta;
        kbm = window.kbdMouse;
    }

    private readonly KeyboardMouseDevice kbm;

    protected override void PrePhysics() => Position = Screen.position + kbm.mouse;
}