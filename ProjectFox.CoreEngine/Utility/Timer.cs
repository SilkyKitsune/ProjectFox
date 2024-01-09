#if DEBUG
using System.Threading;
using static ProjectFox.CoreEngine.Math.Math;

namespace ProjectFox.CoreEngine.Utility;

/// <summary> A sort of accurate-ish Timer class (not meant for deployment) </summary>
/// <remarks> DEBUG Only </remarks>
public sealed class Timer
{
    public delegate void TimerDelegate();

    private long interval = 1000L * (long)ticksPerMillisecond * 21L;
    private bool running = false;
    private long count = 0L;
    private Thread thread;

    public TimerDelegate elapsed;

    public bool Running
    {
        get => running;
        set
        {
            if (value != running)
            {
                if (value) Start();
                else Stop();
            }
        }
    }

    public long Interval
    {
        get => interval / 21L / (long)ticksPerMillisecond;
        set => interval = value * (long)ticksPerMillisecond * 21L;
    }

    public void Start()
    {
        if (thread != null)
        {
            running = false;
            thread.Join();
            thread = null;
        }
        
        count = 0L;
        running = true;
        thread = new Thread(() =>
        {
            while (running) if (count++ >= interval)
                {
                    elapsed?.Invoke();
                    count = 0L;//use thread.sleep instead?
                }
        });
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
#endif