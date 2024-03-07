using System.Runtime.CompilerServices;
using System.Threading;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Utility;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

/// <summary> A basic delegate type for engine related events </summary>
public delegate void EngineEvent();

/// <summary> represents the engine's timing values and entry point </summary>
public static class Engine
{
    private static readonly NameID Name = new("Engine", 0);

    private static uint frameCount = 0;
    private static int freq = 1;
    private static ulong ticksPerFrame = Math.ticksPerMillisecond * 1000uL, timeOfLastFrameInTicks = 0u;//shorten name?
    private static bool running = false;//topmost

    private static readonly Array<ErrorMessage> storedErrors = new Array<ErrorMessage>(0x20);

    private static Thread engineThread = null;

    /// <summary> invoked each time Frequency is changed </summary>
    public static event EngineEvent FreqChanged;

    public static event EngineEvent FrameBegin;//rename?

    /// <summary> invoked at the end of each frame </summary>
    public static event EngineEvent FrameComplete;

#if DEBUG
    public static bool uncapped = false;
#endif

    /// <summary> the number of times Engine.Frame() has been invoked since program start </summary>
    public static uint FrameCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => frameCount;
    }

    /// <summary> the number of times per second Engine.Frame() should be called </summary>
    public static int Frequency
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => freq;
        set
        {
            if (value <= 0)
            {
                SendError(ErrorCodes.BadArgument, Name, null, "frequency cannot be less than 1");
                return;
            }
            if (value != freq)
            {
                freq = Math.Clamp(value, 1, 300);
                ticksPerFrame = Math.ticksPerMillisecond * 1000uL / (ulong)freq;
                FreqChanged?.Invoke();
            }
        }
    }

    /// <summary> the number of milliseconds between each frame at the given frequency </summary>
    public static float MillisecondsPerFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (float)ticksPerFrame / Math.ticksPerMillisecond;
    }
    //msoflastframe?
    /// <summary> percent of MillisecondsPerFrame the previous frame took to complete </summary>
    public static float TimeOfLastFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (float)timeOfLastFrameInTicks / ticksPerFrame;
    }

    /// <summary> procresses a single frame callstack, the engine's main entry point </summary>
    /// <param name="millisecondsOfLastFrame"> the number of milliseconds the previous frame took to complete </param>
    public static void Frame(float millisecondsOfLastFrame = -1f)
    {
        FrameBegin?.Invoke();

#if DEBUG
        Debug.debugLayer.Clear();//should these go in scene?
#endif
        //where should ports.process go?
        timeOfLastFrameInTicks = millisecondsOfLastFrame == -1f ? ticksPerFrame//should this go above frame begin?
            : (ulong)(Math.Clamp(millisecondsOfLastFrame, 0f, float.MaxValue) * Math.ticksPerMillisecond);
        
        SceneList.activeScene?._frame();

#if DEBUG
        if (Debug.debugLayer.visible)
            Debug.debugLayer.Blend(Debug.debugLayer.pixels, Screen.screenLayer.pixels);
#endif

        frameCount++;
        FrameComplete?.Invoke();
    }

    public static void Start()
    {
        if (!running)
        {
            running = true;
            engineThread = new(() =>
            {
                System.Diagnostics.Stopwatch stopwatch = new();
                stopwatch.Start();

                while (running)
                {
                    float ticks = stopwatch.ElapsedTicks;//should this be a float?
#if DEBUG
                    if (uncapped || ticks >= ticksPerFrame)//could uncapped run over 300 fps?
#else
                    if (ticks >= ticksPerFrame)
#endif
                    {
                        //stopwatch.Reset();
                        stopwatch.Restart();
                        Engine.Frame(ticks / (float)Math.ticksPerMillisecond);
                    }
                }
                stopwatch.Stop();
            });
            engineThread.Start();
        }
    }

    public static void Stop()
    {
        if (running)
        {
            running = false;
            engineThread.Join();
            engineThread = null;
        }
    }

    //Exit

    /// <returns> available ErrorMessages stored internally (clears afterward) </returns>
    public static ErrorMessage[] GetAvailableErrors()
    {
        ErrorMessage[] errors;
        lock (storedErrors)
        {
            errors = storedErrors.ToArray();
            storedErrors.Clear();
        }
        return errors;
    }

    internal static void SendError(ErrorCodes error, NameID source, string parameterName = null, string additionalMessage = null)
    {
        ErrorMessage message = new ErrorMessage(error, frameCount,
            $"  Location: {source}" +
            (string.IsNullOrEmpty(parameterName) ? string.Empty : $"\n  Paramerter: {parameterName}") +
            (string.IsNullOrEmpty(additionalMessage) ? string.Empty : $"\n  {additionalMessage}"));
        storedErrors.AddDirect(message);
#if DEBUG
        Debug.Console.QueueMessage(message);
#endif
    }

    internal static T SendError<T>(ErrorCodes error, NameID source, string parameterName = null, string additionalMessage = null)
    {
        SendError(error, source, parameterName, additionalMessage);
        return default;
    }

    //sendusererror()

    /// <summary> represents the engine's internal collection of scene </summary>
    public static class SceneList
    {
        private static readonly NameID Name = new NameID("ScnList", 0);
        private static readonly NameID NoScene = new("NoScene", 0);

        internal static readonly HashArray<Scene> scenes = new HashArray<Scene>(0x8);
        internal static Scene activeScene;

        /// <summary> the number of scenes that have been added </summary>
        public static int TotalScenes
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => scenes.codes.length;
        }

        /// <summary> the name of the currently active scene </summary>
        /// <remarks> Returns: "NoScene_0" if no scene is active </remarks>
        public static NameID ActiveScene
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => activeScene == null ? NoScene : activeScene.name;
            set
            {
                int index = scenes.codes.IndexOf(value);
                if (index < 0)
                {
                    SendError(ErrorCodes.BadArgument, Name, nameof(value),
                        $"'{value}' could not be found in {nameof(SceneList)}");
                    return;
                }
                activeScene = scenes.values.elements[index];
            }
        }

        /// <summary> adds a scene </summary>
        /// <param name="scene"> the scene to be added </param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add(Scene scene)
        {
            if (scene == null)
            {
                SendError(ErrorCodes.NullArgument, Name, nameof(scene));
                return;
            }
            scenes.Add(scene.name, scene);
        }

        /// <summary> removes all scenes including the active scene </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Clear()
        {
            scenes.Clear();
            activeScene = null;
        }

        /// <param name="name"> the scene's name </param>
        /// <returns> true if a scene with the specified ame is found </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Contains(NameID name) => scenes.codes.Contains(name);

        /// <summary> removes a scene </summary>
        /// <remarks> if the scene is active it will be disabled </remarks>
        /// <param name="name"> name of the scene to remove </param>
        public static void Remove(NameID name)
        {
            if (name.Equals(activeScene?.name))
            {
                activeScene = null;
                scenes.Remove(name);
                return;
            }

            if (!scenes.Remove(name))
                SendError(ErrorCodes.BadArgument, Name, nameof(name),
                    $"'{name}' could not be found in {nameof(SceneList)}");
        }
    }
}
