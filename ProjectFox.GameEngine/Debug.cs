#if DEBUG
using C = System.Console;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Utility;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

/// <summary> represents all of the engine's DEBUG exclusive features </summary>
public static class Debug
{
    internal static readonly VisualLayer debugLayer = new(new("DbgLayr", 0)) { visible = false };

    public static readonly ICollection<int> TestArray = new Array<int>(0x8);
    public static readonly ITable<NameID, int> TestTable = new Table<int>(0x8);
    public static readonly ICollection<string> TestArrayRef = new Array<string>(0x8);
    public static readonly ITable<NameID, string> TestTableRef = new Table<string>(0x8);

    public static bool DrawDebug { get => debugLayer.visible; set => debugLayer.visible = value; }

    public static byte DebugAlpha { get => debugLayer.alpha; set => debugLayer.alpha = value; }

    /// <summary> a class for queueing console messages on a separate thread </summary>
    public static class Console
    {
        private static bool readMode = false;

        private static readonly Array<object> outputMessageQueue = new Array<object>(0x100);
        private static readonly Array<string> inputMessageQueue = new Array<string>(0x10);

        private static readonly SpinTimer timer = new(10, 0.5f, true, (t, p) =>
        {
            while (readMode) inputMessageQueue.AddDirect(C.ReadLine());
            if (outputMessageQueue.length > 0)
            {
                int length;
                object[] queue;
                lock (outputMessageQueue)
                {
                    length = outputMessageQueue.length;
                    queue = outputMessageQueue.elements;
                    outputMessageQueue.Clear();
                }
                if (queue != null)
                    for (int i = 0; i < length; i++)
                    {
                        if (queue[i] is ErrorMessage error)
                        {
                            switch (error.severity)
                            {
                                case ErrorMessage.ErrorSeverity.None:
                                    C.WriteLine(error.error);
                                    break;
                                case ErrorMessage.ErrorSeverity.Warning:
                                    C.ForegroundColor = System.ConsoleColor.Yellow;
                                    C.WriteLine(error.error);
                                    C.ForegroundColor = System.ConsoleColor.White;
                                    break;
                                case ErrorMessage.ErrorSeverity.Error:
                                    C.ForegroundColor = System.ConsoleColor.Red;
                                    C.WriteLine(error.error);
                                    C.ForegroundColor = System.ConsoleColor.White;
                                    break;
                            }
                            C.WriteLine(error.message);
                        }
                        else C.WriteLine(queue[i]);
                    }
            }
        });//higher sleep?

        /// <summary> console will read messages until set to false </summary>
        public static bool ReadMode
        {
            get => readMode;
            set
            {
                if (value != readMode)
                {
                    readMode = value;
                    if (readMode) C.WriteLine("ReadMode Enabled");
                    else C.WriteLine("ReadMode Disabled, press enter to continue writing");
                }
            }
        }

        /// <summary> queues a message for printing to the console </summary>
        /// <param name="obj"></param>
        public static void QueueMessage(object obj) => outputMessageQueue.AddDirect(obj);

        /// <summary>  </summary>
        /// <param name="inputMessages"> array of all input messages from the console </param>
        /// <param name="length"> number of non-null elements in inputMessages </param>
        /// <returns> true if length > 0 </returns>
        public static bool GetInputs(out string[] inputMessages, out int length)
        {
            inputMessages = inputMessageQueue.elements;
            length = inputMessageQueue.length;
            inputMessageQueue.Clear();
            return length > 0;
        }

        public static void Clear() => C.Clear();

        /// <summary> Stops the console timer, CANNOT BE RESTARTED!!! </summary>
        public static void Shutdown()
        {
            readMode = false;
            timer.Stop();
        }
    }
}
#endif