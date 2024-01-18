using C = System.Console;

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
}
