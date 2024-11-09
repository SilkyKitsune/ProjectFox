using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

/// <summary> A collection of basic math methods for built in types </summary>
public static partial class Math
{
    /// <summary> an enum for representing sign or 1D direction </summary>
    public enum Sign : byte
    {
        /// <summary> a direction that is negative </summary>
        Neg = 0b10,
        /// <summary> no direction </summary>
        Zero = 0b00,
        /// <summary> a direction that is positive </summary>
        Pos = 0b01
    }

    /// <summary> the total number of ticks in one millisecond </summary>
    public const ulong ticksPerMillisecond = 10000;//move?

    #region Abs
    /// <returns> absolute value of 'value' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Abs(int value) =>
        value == int.MinValue ? int.MaxValue : (value < 0 ? -value : value);

    /// <returns> absolute value of 'value' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Abs(long value) =>
        value == long.MinValue ? long.MaxValue : (value < 0 ? -value : value);

    /// <returns> absolute value of 'value' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Abs(float value) => value < 0 ? -value : value;

    /// <returns> absolute value of 'value' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Abs(double value) => value < 0 ? -value : value;
    #endregion
    
    #region Between
    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(int value, int min, int max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(uint value, uint min, uint max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(long value, long min, long max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(ulong value, ulong min, ulong max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(short value, short min, short max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(ushort value, ushort min, ushort max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(sbyte value, sbyte min, sbyte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(byte value, byte min, byte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(float value, float min, float max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);

    /// <returns> true if (min &lt; value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool Between(double value, double min, double max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value < max);
    #endregion

    #region BetweenAgainstBounds
    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(int value, int min, int max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(uint value, uint min, uint max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(long value, long min, long max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(ulong value, ulong min, ulong max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(short value, short min, short max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(ushort value, ushort min, ushort max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(sbyte value, sbyte min, sbyte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(byte value, byte min, byte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(float value, float min, float max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);

    /// <returns> true if (min ≤ value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstBounds(double value, double min, double max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value <= max);
    #endregion

    #region BetweenAgainstMax
    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(int value, int min, int max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(uint value, uint min, uint max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(long value, long min, long max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(ulong value, ulong min, ulong max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(short value, short min, short max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(ushort value, ushort min, ushort max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(sbyte value, sbyte min, sbyte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(byte value, byte min, byte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(float value, float min, float max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);

    /// <returns> true if (min &lt; value ≤ max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMax(double value, double min, double max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value > min && value <= max);
    #endregion

    #region BetweenAgainstMin
    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(int value, int min, int max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(uint value, uint min, uint max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(long value, long min, long max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(ulong value, ulong min, ulong max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(short value, short min, short max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(ushort value, ushort min, ushort max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(sbyte value, sbyte min, sbyte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(byte value, byte min, byte max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(float value, float min, float max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);

    /// <returns> true if (min ≤ value &lt; max) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool BetweenAgainstMin(double value, double min, double max) =>
        min > max ? throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})") :
        (value >= min && value < max);
    #endregion

    #region Clamp
    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Clamp(int value, int min, int max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Clamp(uint value, uint min, uint max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Clamp(long value, long min, long max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Clamp(ulong value, ulong min, ulong max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Clamp(short value, short min, short max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Clamp(ushort value, ushort min, ushort max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Clamp(sbyte value, sbyte min, sbyte max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Clamp(byte value, byte min, byte max) => min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Clamp(float value, float min, float max) => float.IsNaN(value) ? min : (min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value)));

    /// <returns> min ≤ value ≤ max, unless min &gt; max then the return value will be outside the range </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Clamp(double value, double min, double max) => double.IsNaN(value) ? min : (min > max ?
        (value < min && value > max ? max : value) :
        (value < min ? min : (value > max ? max : value)));
    #endregion

    #region ClampImplicit
    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int ClampImplicit(int value, int a, int b)
    {
        int max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint ClampImplicit(uint value, uint a, uint b)
    {
        uint max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long ClampImplicit(long value, long a, long b)
    {
        long max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong ClampImplicit(ulong value, ulong a, ulong b)
    {
        ulong max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short ClampImplicit(short value, short a, short b)
    {
        short max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort ClampImplicit(ushort value, ushort a, ushort b)
    {
        ushort max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte ClampImplicit(sbyte value, sbyte a, sbyte b)
    {
        sbyte max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte ClampImplicit(byte value, byte a, byte b)
    {
        byte max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ClampImplicit(float value, float a, float b)
    {
        float max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }

    /// <summary> determines the min and max implicitly from a and b </summary>
    /// <returns> min ≤ value ≤ max </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ClampImplicit(double value, double a, double b)
    {
        double max, min;
        if (a > b)
        {
            max = a;
            min = b;
        }
        else
        {
            max = b;
            min = a;
        }
        return value < min ? min : (value > max ? max : value);
    }
    #endregion

    #region Closest
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Closest(int reference, int a, int b) => Abs(reference - a) < Abs(reference - b) ? a : b;
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Closest(uint reference, uint a, uint b) => Abs((long)reference - a) < Abs((long)reference - b) ? a : b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Closest(float reference, float a, float b) => Abs(reference - a) < Abs(reference - b) ? a : b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Closest(double reference, double a, double b) => Abs(reference - a) < Abs(reference - b) ? a : b;
    
    /// <summary> finds the first element in 'values' with the smallest distance to 'reference' </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static int Closest(int reference, params int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        //long longRef = reference, delta = Max(Abs(longRef - int.MaxValue), Abs(longRef - int.MinValue)), newDelta;
        long longRef = reference, delta = uint.MaxValue, newDelta;

        int closest = values[0];
        foreach (int i in values)
        {
            if (reference == i) return i;
            if (closest != i)
            {
                newDelta = Abs(longRef - i);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    /// <summary> finds the first element in 'values' with the smallest distance to 'value' </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static uint Closest(uint reference, params uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        long longRef = reference, delta = uint.MaxValue, newDelta;

        uint closest = values[0];
        foreach (uint i in values)
        {
            if (reference == i) return i;
            if (closest != i)
            {
                newDelta = Abs(longRef - i);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
        /*int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            if (reference == values[i]) return values[i];
            if (values[closest] != values[i])
            {
                newDelta = Abs(longRef - values[i]);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return values[closest];*/
    }

    /// <summary> finds the first element in 'values' with the smallest distance to 'value' </summary>
    /// <exception cref="ArgumentNullException"></exception>
    public static float Closest(float reference, params float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        //double doubleRef = reference, delta = Max(Abs(doubleRef - float.MaxValue), Abs(doubleRef - float.MinValue)), newDelta;
        double doubleRef = reference, delta = double.MaxValue, newDelta;

        float closest = values[0];
        foreach (float f in values)
        {
            if (reference == f) return closest;
            if (closest != f)
            {
                newDelta = Abs(doubleRef - f);
                if (newDelta < delta)
                {
                    closest = f;
                    delta = newDelta;
                }
            }
        }
        return closest;
        /*int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            if (reference == values[i]) return values[i];

            if (values[closest] != values[i])
            {
                newDelta = Abs(reference - values[i]);

                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return values[closest];*/
    }
    #endregion

    #region ClosestIndex
    public static int ClosestIndex(int reference, int[] values)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        long longRef = reference, delta = uint.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            int current = values[i];
            if (reference == current) return i;
            if (values[closest] != current)
            {
                newDelta = Abs(longRef - current);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    public static int ClosestIndex(uint reference, uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        long longRef = reference, delta = uint.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            uint current = values[i];
            if (reference == current) return i;
            if (values[closest] != current)
            {
                newDelta = Abs(longRef - current);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }

    public static int ClosestIndex(float reference, float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        double doubleRef = reference, delta = double.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            float current = values[i];
            if (reference == current) return closest;
            if (values[closest] != current)
            {
                newDelta = Abs(doubleRef - current);
                if (newDelta < delta)
                {
                    closest = i;
                    delta = newDelta;
                }
            }
        }
        return closest;
    }
    #endregion

    #region Cube
    /// <returns> value³ </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Cube(int value) => value * value * value;

    /// <returns> value³ </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Cube(uint value) => value * value * value;

    /// <returns> value³ </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Cube(long value) => value * value * value;

    /// <returns> value³ </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Cube(ulong value) => value * value * value;

    /// <returns> value³ </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Cube(float value) => value * value * value;

    /// <returns> value³ </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Cube(double value) => value * value * value;
    #endregion
    
    #region Farthest
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Farthest(int reference, int a, int b) => Abs(reference - a) > Abs(reference - b) ? a : b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Farthest(uint reference, uint a, uint b) => Abs(reference - a) > Abs(reference - b) ? a : b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Farthest(float reference, float a, float b) => Abs(reference - a) > Abs(reference - b) ? a : b;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Farthest(double reference, double a, double b) => Abs(reference - a) > Abs(reference - b) ? a : b;

    public static int Farthest(int reference, params int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        long longRef = reference, delta = 0, newDelta, maxDelta = Max(Abs(longRef - int.MaxValue), Abs(longRef - int.MinValue));

        int farthest = 0;
        foreach (int i in values)
            if (farthest != i)
            {
                newDelta = Abs(longRef - i);
                if (newDelta == maxDelta) return i;
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        return farthest;
        /*int farthest = 0;
        for (int i = 0; i < values.Length; i++)
            if (values[farthest] != values[i])
            {
                newDelta = Abs(longRef - values[i]);
                if (newDelta == maxDelta) return values[i];
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        return values[farthest];*/
    }
    
    public static uint Farthest(uint reference, params uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        long longRef = reference, delta = 0, newDelta, maxDelta = Max(Abs(longRef - uint.MaxValue), longRef);

        uint farthest = 0;
        foreach (uint i in values)
            if (farthest != i)
            {
                newDelta = Abs(longRef - i);
                if (newDelta == maxDelta) return i;
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        return farthest;
    }

    public static float Farthest(float reference, params float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        double doubleRef = reference, delta = 0f, newDelta, maxDelta = Max(Abs(doubleRef - float.MaxValue), Abs(doubleRef - float.MinValue));

        float farthest = 0f;
        foreach (float f in values)
            if (farthest != f)
            {
                newDelta = Abs(doubleRef - f);
                if (newDelta == maxDelta) return f;
                if (newDelta > delta)
                {
                    farthest = f;
                    delta = newDelta;
                }
            }
        return farthest;
    }
    #endregion

    #region FarthestIndex
    public static int FarthestIndex(int reference, int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        long longRef = reference, delta = 0, newDelta, maxDelta = Max(Abs(longRef - int.MaxValue), Abs(longRef - int.MinValue));

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            int current = values[i];
            if (farthest != current)
            {
                newDelta = Abs(longRef - current);
                if (newDelta == maxDelta) return i;
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }

    public static int FarthestIndex(uint reference, uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        long longRef = reference, delta = 0, newDelta, maxDelta = Max(Abs(longRef - uint.MaxValue), longRef);

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            uint current = values[i];
            if (farthest != current)
            {
                newDelta = Abs(longRef - current);
                if (newDelta == maxDelta) return i;
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }

    public static int FarthestIndex(float reference, float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        double doubleRef = reference, delta = 0f, newDelta, maxDelta = Max(Abs(doubleRef - float.MaxValue), Abs(doubleRef - float.MinValue));

        int farthest = 0;
        for (int i = 0; i < values.Length; i++)
        {
            float current = values[i];
            if (farthest != current)
            {
                newDelta = Abs(doubleRef - current);
                if (newDelta == maxDelta) return i;
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }
    #endregion

    public static float FixFraction(float value)
    {
        bool neg = value < 0f;
        value = neg ? -value : value;
        
        int i = (int)value;
        float fraction = value - i;

        if (fraction < 0.01f) return neg ? -i : i;
        if (fraction > 0.99f) return neg ? -(i + 1) : (i + 1);
        return neg ? -value : value;
    }

    #region HasFraction
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFraction(float value) => value - (int)value != 0f;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool HasFraction(double value) => value - (long)value != 0d;
    #endregion

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool IsOdd(int value) => (value & 1) == 1;

    #region Max
    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Max(int a, int b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Max(uint a, uint b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Max(long a, long b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Max(ulong a, ulong b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Max(short a, short b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Max(ushort a, ushort b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Max(sbyte a, sbyte b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Max(byte a, byte b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Max(float a, float b) => a > b ? a : b;

    /// <returns> the higher value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Max(double a, double b) => a > b ? a : b;
    
    public static int Max(params int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];
        
        int max = int.MinValue;
        foreach (int i in values) if (i > max) max = i;
        return max;
    }
    
    public static uint Max(params uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        uint max = uint.MinValue;
        foreach (uint i in values) if (i > max) max = i;
        return max;
    }

    public static long Max(params long[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        long max = long.MinValue;
        foreach (long l in values) if (l > max) max = l;
        return max;
    }

    public static ulong Max(params ulong[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        ulong max = ulong.MinValue;
        foreach (ulong l in values) if (l > max) max = l;
        return max;
    }

    public static short Max(params short[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        short max = short.MinValue;
        foreach (short s in values) if (s > max) max = s;
        return max;
    }

    public static ushort Max(params ushort[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        ushort max = ushort.MinValue;
        foreach (ushort s in values) if (s > max) max = s;
        return max;
    }

    public static sbyte Max(params sbyte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        sbyte max = sbyte.MinValue;
        foreach (sbyte b in values) if (b > max) max = b;
        return max;
    }

    public static byte Max(params byte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        byte max = byte.MinValue;
        foreach (byte b in values) if (b > max) max = b;
        return max;
    }

    public static float Max(params float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float max = float.MinValue;
        foreach (float f in values) if (f > max) max = f;
        return max;
    }

    public static double Max(params double[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        double max = double.MinValue;
        foreach (double d in values) if (d > max) max = d;
        return max;
    }
    #endregion
    
    #region MaxIndex
    public static int MaxIndex(int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(long[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(ulong[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(short[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(ushort[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(sbyte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(byte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }

    public static int MaxIndex(double[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int max = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] > values[max]) max = i;
        return max;
    }
    #endregion

    #region Min
    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Min(int a, int b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Min(uint a, uint b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Min(long a, long b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Min(ulong a, ulong b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static short Min(short a, short b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ushort Min(ushort a, ushort b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static sbyte Min(sbyte a, sbyte b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static byte Min(byte a, byte b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Min(float a, float b) => a < b ? a : b;

    /// <returns> the lower value of 'a' and 'b' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Min(double a, double b) => a < b ? a : b;
    
    public static int Min(params int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        int min = int.MaxValue;
        foreach (int i in values) if (i < min) min = i;
        return min;
    }

    public static uint Min(params uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        uint min = uint.MaxValue;
        foreach (uint i in values) if (i < min) min = i;
        return min;
    }

    public static long Min(params long[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        long min = long.MaxValue;
        foreach (long l in values) if (l < min) min = l;
        return min;
    }

    public static ulong Min(params ulong[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        ulong min = ulong.MaxValue;
        foreach (ulong l in values) if (l < min) min = l;
        return min;
    }

    public static short Min(params short[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        short min = short.MaxValue;
        foreach (short s in values) if (s < min) min = s;
        return min;
    }

    public static ushort Min(params ushort[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        ushort min = ushort.MaxValue;
        foreach (ushort s in values) if (s < min) min = s;
        return min;
    }

    public static sbyte Min(params sbyte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        sbyte min = sbyte.MaxValue;
        foreach (sbyte b in values) if (b < min) min = b;
        return min;
    }

    public static byte Min(params byte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        byte min = byte.MaxValue;
        foreach (byte b in values) if (b < min) min = b;
        return min;
    }

    public static float Min(params float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        float min = float.MaxValue;
        foreach (float f in values) if (f < min) min = f;
        return min;
    }

    public static double Min(params double[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        double min = double.MaxValue;
        foreach (double d in values) if (d < min) min = d;
        return min;
    }
    #endregion

    #region MinIndex
    public static int MinIndex(int[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(uint[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(long[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(ulong[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(short[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return values[0];

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(ushort[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(sbyte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(byte[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(float[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }

    public static int MinIndex(double[] values)
    {
        if (values.Length == 0) throw new ArgumentNullException(nameof(values));
        if (values.Length == 1) return 0;

        int min = 0;
        for (int i = 0; i < values.Length; i++) if (values[i] < values[min]) min = i;
        return min;
    }
    #endregion

    #region MoveToZero
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int MoveToZero(int value, int amount)
    {
        if (value == 0 || amount <= 0) return value;
        return value < 0 ? value + Clamp(amount, 1, -value) : value - Clamp(amount, 1, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long MoveToZero(long value, long amount)
    {
        if (value == 0 || amount <= 0) return value;
        return value < 0 ? value + Clamp(amount, 1L, -value) : value - Clamp(amount, 1L, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float MoveToZero(float value, float amount)
    {
        if (value == 0 || amount <= 0) return value;
        return value < 0 ? value + Clamp(amount, 1f, -value) : value - Clamp(amount, 1f, value);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double MoveToZero(double value, double amount)
    {
        if (value == 0 || amount <= 0) return value;
        return value < 0 ? value + Clamp(amount, 1d, -value) : value - Clamp(amount, 1d, value);
    }
    #endregion

    #region Pow
    /// <returns> valueᵉˣᵖ </returns>
    /// <remarks> negative exponents will always return zero, check overloads for additional operations </remarks>
    public static int Pow(int value, int exp)
    {
        if (value == 1 || exp == 0) return 1;
        if (value == 0 || exp < 0) return 0;
        if (exp == 1) return value;

        int output = value;
        for (int i = 1; i < exp; i++) output *= value;
        return output;
    }

    /// <returns> valueᵉˣᵖ </returns>
    /// <remarks> check overloads for additional operations </remarks>
    public static float Pow(float value, int exp)
    {
        if (value == 1f || exp == 0) return 1f;
        if (value == 0f) return 0f;
        if (exp == 1) return value;

        float output = value;
        for (int i = 1, abs = Abs(exp); i < abs; i++) output *= value;
        if (exp < 0) output = 1f / output;
        return output;
    }

    //public static float Pow(float value, float exp)

    //public static float Root(int value, int root)
    //public static float Root(float value, int root)
    //public static float Root(float value, float root)
    #endregion

    public static int Round(float value)
    {
        bool neg = value < 0f;
        value = neg ? -value : value;

        int i = (int)value;
        float fraction = value - i;

        if (fraction >= 0.5f) return neg ? -(i + 1) : (i + 1);
        return neg ? -i : i;
    }

    #region Scale
    public static T[] Scale<T>(T[] array, int newLength)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));

        if (newLength < 0) throw new ArgumentException($"{nameof(newLength)} cannot be negative");

        if (array.Length == 0 || newLength == 0) return new T[0];

        if (array.Length == newLength) return array;//copy?

        T[] newArray = new T[newLength];
        float l = array.Length, nl = newArray.Length;

        for (int i = 0, lastIndex = array.Length - 1; i < newArray.Length; i++)
        {
            int i_ = (int)(i / nl * l);
            newArray[i] = array[i_ >= array.Length ? lastIndex : i_];
        }
        return newArray;
    }
    
    public static T[] Scale<T>(T[] array, float lengthModifier)
    {
        if (array == null) throw new ArgumentNullException(nameof(array));

        if (lengthModifier < 0f) throw new ArgumentException($"{nameof(lengthModifier)} cannot be negative");

        if (array.Length == 0 || lengthModifier == 0f) return new T[0];

        if (lengthModifier == 1f) return array;//copy?

        float l = array.Length, nl = array.Length * lengthModifier;
        T[] newArray = new T[(int)nl];

        for (int i = 0, lastIndex = array.Length - 1; i < newArray.Length; i++)
        {
            int i_ = (int)(i / nl * l);
            newArray[i] = array[i_ >= array.Length ? lastIndex : i_];
        }
        return newArray;
    }
    #endregion

    #region Sign
    [MethodImpl(MethodImplOptions.AggressiveInlining)]//rename?
    public static Sign FindSign(int value) => value < 0 ? Sign.Neg : (value > 0 ? Sign.Pos : Sign.Zero);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FindSignInt(int value) => value < 0 ? -1 : (value > 0 ? 1 : 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Sign FindSign(int reference, int value) => value < reference ? Sign.Neg : (value > reference ? Sign.Pos : Sign.Zero);
    
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FindSignInt(int reference, int value) => value < reference ? -1 : (value > reference ? 1 : 0);

    /// <summary> compares the arguments </summary>
    /// <remarks> prefers negative </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Sign FindSign(bool neg, bool pos) => neg ? Sign.Neg : pos ? Sign.Pos : Sign.Zero;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int FindSignInt(bool neg, bool pos) => neg ? -1 : pos ? 1 : 0;
    #endregion
    
    #region Sqr
    /// <returns> value² </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int Sqr(int value) => value * value;

    /// <returns> value² </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint Sqr(uint value) => value * value;

    /// <returns> value² </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long Sqr(long value) => value * value;

    /// <returns> value² </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong Sqr(ulong value) => value * value;

    /// <returns> value² </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float Sqr(float value) => value * value;

    /// <returns> value² </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double Sqr(double value) => value * value;
    #endregion

    #region SqrRoot
    /// <returns> Approximate square root of 'value' </returns>
    /// <remarks> Results are less accurate the closer to zero they are </remarks>
    public static float SqrRoot(uint value)
    {
        if (value == 0u || value == 1u) return value;
        
        for (uint lowRoot = 1, lowPower = 1, highPower = 4, incrementor = 3; value > lowPower;
            lowRoot++, lowPower += incrementor, incrementor += 2u, highPower += incrementor)
        {
            if (value == highPower) return lowRoot + 1u;
            else if (value < highPower) return lowRoot + (((float)value - lowPower) / (highPower - lowPower));
        }
        throw new Exception("Operation Error Root was not found!");
    }

    /// <returns> Approximate square root of 'value' </returns>
    /// <remarks> Results are less accurate the closer to zero they are </remarks>
    public static float SqrRoot(float value)
    {
        if (value == 0f) return value;

        for (float lowRoot = 0f, lowPower = 0f, highPower = 1f, incrementor = 1f; value > lowPower;
            lowRoot++, lowPower += incrementor, incrementor += 2f, highPower += incrementor)
        {
            if (value == highPower) return lowRoot + 1f;
            else if (value < highPower) return lowRoot + ((value - lowPower) / (highPower - lowPower));
        }
        throw new Exception("Operation Error Root was not found!");
    }
    #endregion

    public static int[] StepInterpolate(float value)
    {
        bool neg = value < 0f;
        if (neg) value = -value;

        int number = (int)value;
        float fraction = value - number;

        if (fraction < 0.01f) fraction = 0f;
        else if (fraction > 0.99f)
        {
            number++;
            fraction = 0f;
        }

        if (fraction == 0f) return new int[1] { number };
        if (fraction == 0.5f) return new int[2] { number + 1, number };

        float highSteps = fraction;
        int steps = 1;
        while (HasFraction(highSteps)) highSteps = FixFraction(fraction * ++steps);
        
        bool upper = fraction >= 0.5f;

        int[] array = new int[steps];
        for (int i = 0, count = 0, nextIndex = 0,
            secondarySteps = upper ? array.Length - (int)highSteps : (int)highSteps,
            primary = upper ? number + 1 : number, secondary = upper ? number : number + 1,
            interval = Round(array.Length / (float)secondarySteps);
            i < array.Length; i++)
        {
            if (count < secondarySteps && i == nextIndex)
            {
                array[i] = neg ? -secondary : secondary;
                nextIndex += interval;
                count++;
            }
            else array[i] = neg ? -primary : primary;
        }
        return array;
    }

    #region Wrap
    //how to write comment better?
    /// <returns> 'value' wrapped over the bounds of 'min' and 'max' | => min ≤ value ≤ max => | </returns>
    /// <exception cref="ArgumentException"></exception>
    public static int Wrap(int value, int min, int max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");
        
        if (min == max) return max;
        if (value > max) return min + ((value - max - 1) % (max - min + 1));
        if (value < min) return max - ((min - value - 1) % (max - min + 1));
        return value;
    }

    /// <returns> 'value' wrapped over the bounds of 'min' and 'max' </returns>
    /// <exception cref="ArgumentException"></exception>
    public static uint Wrap(uint value, uint min, uint max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");

        if (min == max) return max;
        if (value > max) return min + ((value - max - 1u) % (max - min + 1u));
        if (value < min) return max - ((min - value - 1u) % (max - min + 1u));
        return value;
    }

    /// <returns> 'value' wrapped over the bounds of 'min' and 'max' </returns>
    /// <exception cref="ArgumentException"></exception>
    public static long Wrap(long value, long min, long max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");

        if (min == max) return max;
        if (value > max) return min + ((value - max - 1L) % (max - min + 1L));
        if (value < min) return max - ((min - value - 1L) % (max - min + 1L));
        return value;
    }

    /// <returns> 'value' wrapped over the bounds of 'min' and 'max' </returns>
    /// <exception cref="ArgumentException"></exception>
    public static ulong Wrap(ulong value, ulong min, ulong max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");

        if (min == max) return max;
        if (value > max) return min + ((value - max - 1uL) % (max - min + 1uL));
        if (value < min) return max - ((min - value - 1uL) % (max - min + 1uL));
        return value;
    }

#if DEBUG
    [Obsolete] public static float Wrap(float value, float min, float max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");

        if (min == max) return max;
        if (value > max) return min + ((value - max - 1f) % (max - min + 1f));
        if (value < min) return max - ((min - value - 1f) % (max - min + 1f));
        return value;
    }

    [Obsolete] public static double Wrap(double value, double min, double max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");

        if (min == max) return max;
        if (value > max) return min + ((value - max - 1d) % (max - min + 1d));
        if (value < min) return max - ((min - value - 1d) % (max - min + 1d));
        return value;
    }
#endif

    public static decimal Wrap(decimal value, decimal min, decimal max)
    {
        if (min > max) throw new ArgumentException($"MinMaxException ({nameof(min)}={min} > {nameof(max)}={max})");
        //clamp the return?
        if (min == max) return max;
        if (value > max) return min + ((value - max - 1m) % (max - min + 1m));
        if (value < min) return max - ((min - value - 1m) % (max - min + 1m));
        return value;
    }
    #endregion
}