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

        window = new("Debug Window", 50, 50, true);

        Engine.SceneList.Add(this);
        Engine.SceneList.ActiveScene = Name;
    }

    private readonly GameWindow window;
}

public sealed class DebugController : Object
{
    private static readonly NameID ID = new("DbgCtrl", 0);

    public DebugController(GameWindow window) : base(ID)
    {
        pauseWalks = true;
        kbm = window.kbdMouse;
    }

    private readonly KeyboardMouseDevice kbm;

    public bool printFrameInfo = false;

    private void SetFPS(int value)
    {
        Engine.Frequency = value;
        QueueMessage($"FPS={Engine.Frequency}");
    }

    protected override void PreFrame()
    {
        if (printFrameInfo)
        {
            float time = Engine.TimeOfLastFrame;
            QueueMessage($"{Engine.FrameCount}: {1000 / (time * Engine.MillisecondsPerFrame)} {time}");
        }

        if (kbm.Insert.ChangedTrue)
        {
            bool value = !Debug.DrawDebug;
            DrawDebug = value;
            QueueMessage($"DrawDebug={value}");
        }

        if (kbm.NumpadOne.ChangedTrue) SetFPS(1);
        else if (kbm.NumpadTwo.ChangedTrue) SetFPS(5);
        else if (kbm.NumpadThree.ChangedTrue) SetFPS(15);
        else if (kbm.NumpadFour.ChangedTrue) SetFPS(30);
        else if (kbm.NumpadFive.ChangedTrue) SetFPS(60);
        else if (kbm.NumpadSix.ChangedTrue) SetFPS(120);

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