#if DEBUG
using System.Runtime.CompilerServices;
using System.Threading;

namespace ProjectFox.CoreEngine.Utility;

/// <summary> A sort of accurate-ish Stopwatch class (experimental, not meant for deployment) </summary>
/// <remarks> DEBUG Only </remarks>
public sealed class Stopwatch
{
    internal const ulong ticksPerMillisecond = 10000;

    private bool running = false;
    private long count = 0u;
    
    private Thread thread;

    public long ElapsedTicks
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => count / 21L;
    }

    public float ElapsedMilliseconds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => count / 21L / (float)ticksPerMillisecond;
    }

    public override string ToString()//remove
    {
        if (thread != null) thread.Name = "Stopwatch-Thread";
        return $"running={running}, count={count}, ticks={ElapsedTicks}, thread={thread != null} & {thread?.IsAlive}";
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Reset() => count = 0u;//test

    public void Restart()
    {
        if (running)
        {
            running = false;
            thread.Join();
            thread = null;
        }
        
        count = 0u;
        running = true;
        thread = new(() => { while (running) count++; });
        thread.Start();
    }

    public void Start()
    {
        if (!running)
        {
            running = true;
            thread = new(() => { while (running) count++; });
            thread.Start();
        }
    }

    public void Stop()
    {
        if (running)
        {
            running = false;
            thread.Join();
            thread = null;
        }
    }
}
#endif