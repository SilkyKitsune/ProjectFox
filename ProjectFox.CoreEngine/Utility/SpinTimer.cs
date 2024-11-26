using System;
using System.Threading;
using SW = System.Diagnostics.Stopwatch;

namespace ProjectFox.CoreEngine.Utility;

/// <summary> A wait spin timer that can optionally sleep for a specified fraction of each interval </summary>
/// <remarks> Longer sleep periods reduces processor usage but typically reduces accuracy as well </remarks>
public sealed class SpinTimer
{
    public delegate void SpinTimerElapsed(float millisecondsPerInterval, float millisecondsOfLastInterval);//rename

    public static readonly float ticksPerMillisecond = SW.Frequency / 1000f;

    public SpinTimer(int/*float?*/ intervalsPerSecond, float sleepPeriod, bool startTimer, params SpinTimerElapsed[] events)
    {
        if (intervalsPerSecond < 0f) throw new ArgumentException($"{nameof(intervalsPerSecond)} cannot be negative!");

        if (sleepPeriod < 0f) throw new ArgumentException($"{nameof(sleepPeriod)} cannot be negative!");

        this.intervalsPerSecond = intervalsPerSecond;
        this.sleepPeriod = sleepPeriod;

        foreach (SpinTimerElapsed event_ in events) elapsed += event_;

        if (startTimer) Start();
    }

    ~SpinTimer() => Stop();

    private readonly int intervalsPerSecond;

    private readonly float sleepPeriod;

    private readonly SpinTimerElapsed elapsed;//rename?

    private Thread thread = null;

    private bool running = false;

    public int IntervalsPerSecond => intervalsPerSecond;

    public float SleepPeriod => sleepPeriod;

    public bool Running => running;

    public void Start()
    {
        if (thread != null) return;

        thread = new(() =>
        {
            bool sleepingEnabled = sleepPeriod > 0f;

            long ticksPerInterval = SW.Frequency / intervalsPerSecond, ticksPerSleep = (long)(ticksPerInterval * sleepPeriod);

            float millisecondsPerInterval = ticksPerInterval / ticksPerMillisecond;

            int millisecondsPerSleep = (int)(ticksPerSleep / ticksPerMillisecond);

            long prevTimestamp = SW.GetTimestamp();

            while (running)
            {
                long currentTimestamp = SW.GetTimestamp(), elapsedTicks = currentTimestamp - prevTimestamp;

                if (sleepingEnabled && elapsedTicks < ticksPerSleep) Thread.Sleep(millisecondsPerSleep);

                if (elapsedTicks >= ticksPerInterval)
                {
                    prevTimestamp = currentTimestamp;
                    elapsed.Invoke(millisecondsPerInterval, elapsedTicks / ticksPerMillisecond);//is this actually the ms of last interval?
                }
            }
        });
        running = true;
        thread.Start();
    }

    public void Stop()
    {
        if (thread != null)
        {
            running = false;
            thread.Join();
            thread = null;
        }
    }
}