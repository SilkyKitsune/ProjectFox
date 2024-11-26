using System;
using C = System.Console;
using Thread = System.Threading.Thread;
using SW = System.Diagnostics.Stopwatch;

using ProjectFox.CoreEngine.Utility;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    internal static void UtilityTest()
    {
        #region Stopwatch
        C.WriteLine("---Stopwatch---");

        int length = 100;

        Stopwatch stopwatch = new Stopwatch();

        //C.WriteLine(stopwatch.ElapsedTicks);
        C.WriteLine(stopwatch);

        stopwatch.Start();
        for (int i = 0, j; i < length; i++) j = 0;
        C.WriteLine(stopwatch);
        stopwatch.Stop();
        //C.WriteLine(stopwatch.ElapsedTicks);
        C.WriteLine(stopwatch);

        stopwatch.Start();
        for (int i = 0, j; i < length; i++) j = 0;
        C.WriteLine(stopwatch);
        stopwatch.Stop();
        //C.WriteLine(stopwatch.ElapsedTicks);
        C.WriteLine(stopwatch);

        stopwatch.Reset();
        //C.WriteLine(stopwatch.ElapsedTicks);
        C.WriteLine(stopwatch);

        stopwatch.Start();
        for (int i = 0, j; i < length; i++) j = 0;
        C.WriteLine(stopwatch);
        stopwatch.Stop();
        //C.WriteLine(stopwatch.ElapsedTicks);
        C.WriteLine(stopwatch);

        stopwatch.Restart();
        for (int i = 0, j; i < length; i++) j = 0;
        C.WriteLine(stopwatch);
        stopwatch.Stop();
        //C.WriteLine(stopwatch.ElapsedTicks);
        C.WriteLine(stopwatch);

        C.WriteLine("-----\n");
        #endregion

        #region Timer
        C.WriteLine("---Timer---");

        Timer timer = new Timer()
        {
            Interval = 1000L,
            elapsed = () => { C.WriteLine("timer elapsed"); }
        };
        timer.Start();
        System.Threading.Thread.Sleep(10000);
        timer.Stop();

        C.WriteLine("-----\n");
        #endregion
    }

    public static void TimingTest()
    {
        Start:

        C.Clear();
        C.WriteLine($"{SW.IsHighResolution} {SW.Frequency}");

        RetryTime:

        TimeSpan runtime = TimeSpan.FromSeconds(60d);
        C.WriteLine("Type a run time in minutes...");
        if (double.TryParse(C.ReadLine(), out double d) && d > 0d) runtime = TimeSpan.FromSeconds(60d * d);
        else
        {
            C.WriteLine("Invalid!");
            goto RetryTime;
        }

        RetryFreq:

        long intervalsPerSec = 1L;
        C.WriteLine("Type a frequency...");
        if (long.TryParse(C.ReadLine(), out long l) && l >= 1L) intervalsPerSec = l;
        else
        {
            C.WriteLine("Invalid!");
            goto RetryFreq;
        }

        long ticksPerInterval = SW.Frequency / intervalsPerSec, restTime = ticksPerInterval / 2L, elapsedIntervals = 0L, elapsedTicksPerSecond = 0L;
        double ticksPerMS = SW.Frequency / 1000d, msPerInterval = ticksPerInterval / ticksPerMS;
        int msRestTime = (int)(msPerInterval / 2d);

        C.WriteLine($"Target interval: {msPerInterval}ms ({ticksPerInterval})");

        long prevTimestamp = SW.GetTimestamp();
        DateTime start = DateTime.Now, t = start;

        while (t - start < runtime)
        {
            long currentTimestamp = SW.GetTimestamp(), elapsedTicks = currentTimestamp - prevTimestamp;

            if (elapsedTicks < restTime) Thread.Sleep(msRestTime);

            if (elapsedTicks >= ticksPerInterval)
            {
                elapsedTicksPerSecond += elapsedTicks;
                prevTimestamp = currentTimestamp;

                if (++elapsedIntervals >= intervalsPerSec)
                {
                    double averageMSIntervalPerSecond = elapsedTicksPerSecond / ticksPerMS / intervalsPerSec;

                    t = t.AddSeconds(1d);
                    elapsedTicksPerSecond = 0L;
                    elapsedIntervals = 0L;

                    C.WriteLine($"{t}\n{currentTimestamp}\n{averageMSIntervalPerSecond}\n{averageMSIntervalPerSecond / msPerInterval}\n-");
                }
            }
        }

        DateTime end = DateTime.Now;
        C.WriteLine($"Ran for {start.TimeOfDay} to {end.TimeOfDay}");

        C.WriteLine("Restart? 'y' for yes");
        if (C.ReadLine() == "y") goto Start;
    }

    public static void SpinTimerTest()
    {
        Start:
        C.Clear();

        SetFreq:
        C.WriteLine("Type intervals per second...");
        int freq = 1;
        if (!int.TryParse(C.ReadLine(), out freq) || freq <= 0)
        {
            C.WriteLine("Invalid!");
            goto SetFreq;
        }
        bool b = false;
        C.WriteLine("Allow sleeping? 'y' for yes");
        SpinTimer timer = new(freq, C.ReadLine() == "y" ? 0.5f : 0f, false, (t, p) =>//temp
        {
            C.WriteLine($"{DateTime.Now}\n{t}\n{p}\n-");
            if (!b)
            {
                b = true;
                Thread.Sleep((int)t);
            }
        });
        timer.Start();

        C.ReadLine();
        timer.Stop();

        C.WriteLine("Restart? 'y' for yes");
        if (C.ReadLine() == "y") goto Start;
    }

    public static void LoopTimingTest()
    {
        const int length = byte.MaxValue + 1;
        Restart:

        long prevTimestamp = SW.GetTimestamp();

        for (int i = 0; i < length; i++)
        {
            Empty();
            Empty();
            Empty();
            Empty();
        }

        long currentTimestamp = SW.GetTimestamp(), elapsedTicks = currentTimestamp - prevTimestamp;
        float elapsedMS = elapsedTicks / SpinTimer.ticksPerMillisecond;
        C.WriteLine($"{elapsedTicks} : {elapsedMS}");
        


        prevTimestamp = SW.GetTimestamp();

        for (int i = 0; i < length; i++) Empty();
        for (int i = 0; i < length; i++) Empty();
        for (int i = 0; i < length; i++) Empty();
        for (int i = 0; i < length; i++) Empty();

        currentTimestamp = SW.GetTimestamp();
        long difference = elapsedTicks;
        elapsedTicks = currentTimestamp - prevTimestamp;
        elapsedMS = elapsedTicks / SpinTimer.ticksPerMillisecond;
        C.WriteLine($"{elapsedTicks} : {elapsedMS} : {elapsedTicks - difference}");
        
        if (C.ReadLine() != "e") goto Restart;
    }
    
    private static void Empty() { }
}