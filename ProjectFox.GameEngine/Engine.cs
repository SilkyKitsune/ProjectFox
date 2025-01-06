using System.Runtime.CompilerServices;
using System.Threading;
using Stopwatch = System.Diagnostics.Stopwatch;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Utility;
using ProjectFox.GameEngine.Input;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

/// <summary> A basic delegate type for engine related events </summary>
public delegate void EngineEvent();

/// <summary> represents the engine's timing values and entry point </summary>
public static class Engine
{
    private static readonly NameID Name = new("Engine", 0);
    
    private static uint frameCount = 0;
    private static int freq = 1, msPerSleep = 0;
    private static long ticksPerFrame = Stopwatch.Frequency / freq, ticksPerSleep = 0;
    private static float msPerFrame = ticksPerFrame / SpinTimer.ticksPerMillisecond, msOfLastFrame = msPerFrame, sleepPeriod = 0f;
    private static bool running = false, sleepingEnabled = false;//topmost

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
                ticksPerFrame = Stopwatch.Frequency / freq;
                msPerFrame = ticksPerFrame / SpinTimer.ticksPerMillisecond;
                sleepingEnabled = sleepPeriod > 0f;
                ticksPerSleep = (long)(ticksPerFrame * sleepPeriod);
                msPerSleep = (int)(ticksPerSleep / SpinTimer.ticksPerMillisecond);
                FreqChanged?.Invoke();
            }
        }
    }

    public static float SleepPeriod
    {
        get => sleepPeriod;
        set
        {
            if (value < 0f)
            {
                SendError(ErrorCodes.BadArgument, Name, null, "sleep period cannot be negative");
                return;
            }
            if (value != sleepPeriod)
            {
                sleepPeriod = Math.Clamp(value, 0f, 0.9f);//is max okay as 0.9?
                sleepingEnabled = sleepPeriod > 0f;
                ticksPerSleep = (long)(ticksPerFrame * sleepPeriod);
                msPerSleep = (int)(ticksPerSleep / SpinTimer.ticksPerMillisecond);
            }
        }
    }

    /// <summary> the number of milliseconds between each frame at the given frequency </summary>
    public static float MillisecondsPerFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => msPerFrame;
    }

    public static float MillisecondsOfLastFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => msOfLastFrame;
    }

    /// <summary> percent of MillisecondsPerFrame the previous frame took to complete </summary>
    public static float TimeOfLastFrame
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => msOfLastFrame / msPerFrame;
    }

    /// <summary> procresses a single frame callstack, the engine's main entry point </summary>
    /// <param name="millisecondsOfLastFrame"> the number of milliseconds the previous frame took to complete </param>
    public static void Frame(float millisecondsOfLastFrame = -1f)
    {
        FrameBegin?.Invoke();

        //should this go above frame begin?
        msOfLastFrame = millisecondsOfLastFrame < 0f || float.IsNaN(millisecondsOfLastFrame) ?
            msPerFrame : (millisecondsOfLastFrame > float.MaxValue ? float.MaxValue : millisecondsOfLastFrame);
        
        Ports.ProcessDevices();

#if DEBUG
        Debug.debugLayer.Clear();//should these go in scene?
#endif
        
        SceneList.activeScene?._frame();

#if DEBUG
        if (Debug.debugLayer.visible && Debug.debugLayer.alpha != 0)
            Debug.debugLayer.Blend(Debug.debugLayer.pixels, Screen.screenLayer.pixels);
#endif

        frameCount++;
        FrameComplete?.Invoke();
    }

    public static void Start()
    {
        if (engineThread != null) return;

        engineThread = new(() =>
        {
            long prevTimestamp = Stopwatch.GetTimestamp();
            while (running)
            {
                long currentTimestamp = Stopwatch.GetTimestamp(), elapsedTicks = currentTimestamp - prevTimestamp;
                if (
#if DEBUG
                    !uncapped &&
#endif
                    sleepingEnabled && elapsedTicks < ticksPerSleep) Thread.Sleep(msPerSleep);
                if (
#if DEBUG
                    uncapped ||//could uncapped run over 300 fps?
#endif
                    elapsedTicks >= ticksPerFrame)
                {
                    prevTimestamp = currentTimestamp;
                    Engine.Frame(elapsedTicks/ SpinTimer.ticksPerMillisecond);
                }
            }
        });
        running = true;
        engineThread.Start();
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
            (string.IsNullOrEmpty(parameterName) ? string.Empty : $"\n  Parameter: {parameterName}") +
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

        internal static readonly Table<Scene> scenes = new(0x8);
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