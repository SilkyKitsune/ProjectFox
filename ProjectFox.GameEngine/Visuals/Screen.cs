using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public static class Screen
{
    public enum ClearModes
    {
        /// <summary> Does not clear the screen </summary>
        None,
        /// <summary> Zero-fills the screen </summary>
        Clear,
        /// <summary> Fills the screen with the value of bgColor </summary>
        Fill,
        /// <summary> Fills the screen with BGTexture </summary>
        DrawTexture
    }

    private static float scale = 1f;
    private static bool oneToOne = false, fullScreen = false;

    internal static Vector size = new(100, 100);//this default value prevents layers from initializing right
    internal static VisualLayer screenLayer = new(new("ScrLayr", 0));

    public static bool visible = true;

    public static Vector position = new(0, 0);

    public static event EngineEvent sizeChanged, scaleChanged, oneToOneChanged, fullScreenChanged;

    public static Vector Size
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => size;
        set
        {
            if (value.x > 0 && value.y > 0 && !size.Equals(value))
            {
                size = value;
                screenLayer.Clear();
#if DEBUG
                Debug.debugLayer.Clear();
#endif
                for (int i = 0; i < Engine.SceneList.scenes.codes.length; i++)
                {
                    Scene scene = Engine.SceneList.scenes.values.elements[i];
                    for (int j = 0; j < scene.visualLayers.codes.length; j++)
                    {
                        scene.visualLayers.values.elements[j].Clear();
                    }
                }
                sizeChanged?.Invoke();
            }
        }
    }

    public static float Scale
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scale;
        set
        {
            if (value >= 1f && scale != value)
            {
                scale = value;
                scaleChanged?.Invoke();
            }
        }
    }

    public static bool OneToOne//rename
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => oneToOne;
        set
        {
            if (oneToOne != value)
            {
                oneToOne = value;
                oneToOneChanged?.Invoke();
            }
        }
    }

    public static bool FullScreen
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => fullScreen;
        set
        {
            if (fullScreen != value)
            {
                fullScreen = value;
                fullScreenChanged?.Invoke();
            }
        }
    }

    public static Color[] GetFrame() => screenLayer.pixels;

    public static Vector GetScaledSize() => new(
        oneToOne ? size.x * (int)scale : (int)(size.x * scale),
        oneToOne ? size.y * (int)scale : (int)(size.y * scale));
}