using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;

using ProjectFox.CoreEngine.Data;

using static ProjectFox.CoreEngine.Collections.Strings;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public static void MathTest()
    {
        #region Abs
        C.WriteLine("---Abs---");
        C.WriteLine(Abs(0));
        C.WriteLine(Abs(-10));
        C.WriteLine(Abs(10));
        C.WriteLine(Abs(int.MaxValue));
        C.WriteLine(Abs(int.MinValue));

        C.WriteLine(Abs(0L));
        C.WriteLine(Abs(-10L));
        C.WriteLine(Abs(10L));
        C.WriteLine(Abs(long.MaxValue));
        C.WriteLine(Abs(long.MinValue));

        C.WriteLine(Abs(0f));
        C.WriteLine(Abs(-10f));
        C.WriteLine(Abs(10f));
        C.WriteLine(Abs(float.MaxValue));
        C.WriteLine(Abs(float.MinValue));
        C.WriteLine(Abs(float.Epsilon));
        C.WriteLine(Abs(float.NegativeInfinity));
        C.WriteLine(Abs(float.PositiveInfinity));
        C.WriteLine(Abs(float.NaN));

        C.WriteLine(Abs(0d));
        C.WriteLine(Abs(-10d));
        C.WriteLine(Abs(10d));
        C.WriteLine(Abs(double.MaxValue));
        C.WriteLine(Abs(double.MinValue));
        C.WriteLine(Abs(double.Epsilon));
        C.WriteLine(Abs(double.NegativeInfinity));
        C.WriteLine(Abs(double.PositiveInfinity));
        C.WriteLine(Abs(double.NaN));

        C.WriteLine("-----\n");
        #endregion

        #region Between
        C.WriteLine("---Between---");

        ///int
        C.WriteLine(Between(0, -10, 10));
        C.WriteLine(Between(20, -10, 10));
        C.WriteLine(Between(-20, -10, 10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between(0, 10, -10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///uint
        C.WriteLine(Between(0u, 0u, 10u));
        C.WriteLine(Between(20u, 0u, 10u));
        C.WriteLine(Between(0u, 5u, 10u));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between(0, 20, 10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///long
        C.WriteLine(Between(0L, -10L, 10L));
        C.WriteLine(Between(20L, -10L, 10L));
        C.WriteLine(Between(-20L, -10L, 10L));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between(0L, 10L, -10L));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ulong
        C.WriteLine(Between(0uL, 0uL, 10uL));
        C.WriteLine(Between(20uL, 0uL, 10uL));
        C.WriteLine(Between(0uL, 5uL, 10uL));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between(0uL, 20uL, 10uL));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///short
        C.WriteLine(Between((short)0, (short)-10, (short)10));
        C.WriteLine(Between((short)20, (short)-10, (short)10));
        C.WriteLine(Between((short)-20, (short)-10, (short)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between((short)0, (short)10, (short)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ushort
        C.WriteLine(Between((ushort)0, (ushort)0, (ushort)10));
        C.WriteLine(Between((ushort)20, (ushort)0, (ushort)10));
        C.WriteLine(Between((ushort)0, (ushort)5, (ushort)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between((ushort)0, (ushort)20, (ushort)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///sbyte
        C.WriteLine(Between((sbyte)0, (sbyte)-10, (sbyte)10));
        C.WriteLine(Between((sbyte)20, (sbyte)-10, (sbyte)10));
        C.WriteLine(Between((sbyte)-20, (sbyte)-10, (sbyte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between((sbyte)0, (sbyte)10, (sbyte)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///byte
        C.WriteLine(Between((byte)0, (byte)0, (byte)10));
        C.WriteLine(Between((byte)20, (byte)0, (byte)10));
        C.WriteLine(Between((byte)0, (byte)5, (byte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between((byte)0, (byte)20, (byte)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///float
        C.WriteLine(Between(0f, -10f, 10f));
        C.WriteLine(Between(20f, -10f, 10f));
        C.WriteLine(Between(-20f, -10f, 10f));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between(0f, 10f, -10f));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(Between(0d, -10d, 10d));
        C.WriteLine(Between(20d, -10d, 10d));
        C.WriteLine(Between(-20d, -10d, 10d));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Between(0d, 10d, -10d));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region BetweenAgainstBounds
        C.WriteLine("---BetweenAgainstBounds---");

        ///int
        C.WriteLine(BetweenAgainstBounds(0, -10, 10));
        C.WriteLine(BetweenAgainstBounds(20, -10, 10));
        C.WriteLine(BetweenAgainstBounds(-20, -10, 10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds(0, 10, -10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///uint
        C.WriteLine(BetweenAgainstBounds(0u, 0u, 10u));
        C.WriteLine(BetweenAgainstBounds(20u, 0u, 10u));
        C.WriteLine(BetweenAgainstBounds(0u, 5u, 10u));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds(0, 20, 10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///long
        C.WriteLine(BetweenAgainstBounds(0L, -10L, 10L));
        C.WriteLine(BetweenAgainstBounds(20L, -10L, 10L));
        C.WriteLine(BetweenAgainstBounds(-20L, -10L, 10L));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds(0L, 10L, -10L));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ulong
        C.WriteLine(BetweenAgainstBounds(0uL, 0uL, 10uL));
        C.WriteLine(BetweenAgainstBounds(20uL, 0uL, 10uL));
        C.WriteLine(BetweenAgainstBounds(0uL, 5uL, 10uL));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds(0uL, 20uL, 10uL));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///short
        C.WriteLine(BetweenAgainstBounds((short)0, (short)-10, (short)10));
        C.WriteLine(BetweenAgainstBounds((short)20, (short)-10, (short)10));
        C.WriteLine(BetweenAgainstBounds((short)-20, (short)-10, (short)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds((short)0, (short)10, (short)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ushort
        C.WriteLine(BetweenAgainstBounds((ushort)0, (ushort)0, (ushort)10));
        C.WriteLine(BetweenAgainstBounds((ushort)20, (ushort)0, (ushort)10));
        C.WriteLine(BetweenAgainstBounds((ushort)0, (ushort)5, (ushort)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds((ushort)0, (ushort)20, (ushort)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///sbyte
        C.WriteLine(BetweenAgainstBounds((sbyte)0, (sbyte)-10, (sbyte)10));
        C.WriteLine(BetweenAgainstBounds((sbyte)20, (sbyte)-10, (sbyte)10));
        C.WriteLine(BetweenAgainstBounds((sbyte)-20, (sbyte)-10, (sbyte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds((sbyte)0, (sbyte)10, (sbyte)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///byte
        C.WriteLine(BetweenAgainstBounds((byte)0, (byte)0, (byte)10));
        C.WriteLine(BetweenAgainstBounds((byte)20, (byte)0, (byte)10));
        C.WriteLine(BetweenAgainstBounds((byte)0, (byte)5, (byte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds((byte)0, (byte)20, (byte)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///float
        C.WriteLine(BetweenAgainstBounds(0f, -10f, 10f));
        C.WriteLine(BetweenAgainstBounds(20f, -10f, 10f));
        C.WriteLine(BetweenAgainstBounds(-20f, -10f, 10f));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds(0f, 10f, -10f));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(BetweenAgainstBounds(0d, -10d, 10d));
        C.WriteLine(BetweenAgainstBounds(20d, -10d, 10d));
        C.WriteLine(BetweenAgainstBounds(-20d, -10d, 10d));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstBounds(0d, 10d, -10d));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region BetweenAgainstMax
        C.WriteLine("---BetweenAgainstMax---");

        ///int
        C.WriteLine(BetweenAgainstMax(0, -10, 10));
        C.WriteLine(BetweenAgainstMax(20, -10, 10));
        C.WriteLine(BetweenAgainstMax(-20, -10, 10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax(0, 10, -10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///uint
        C.WriteLine(BetweenAgainstMax(0u, 0u, 10u));
        C.WriteLine(BetweenAgainstMax(20u, 0u, 10u));
        C.WriteLine(BetweenAgainstMax(0u, 5u, 10u));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax(0, 20, 10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///long
        C.WriteLine(BetweenAgainstMax(0L, -10L, 10L));
        C.WriteLine(BetweenAgainstMax(20L, -10L, 10L));
        C.WriteLine(BetweenAgainstMax(-20L, -10L, 10L));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax(0L, 10L, -10L));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ulong
        C.WriteLine(BetweenAgainstMax(0uL, 0uL, 10uL));
        C.WriteLine(BetweenAgainstMax(20uL, 0uL, 10uL));
        C.WriteLine(BetweenAgainstMax(0uL, 5uL, 10uL));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax(0uL, 20uL, 10uL));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///short
        C.WriteLine(BetweenAgainstMax((short)0, (short)-10, (short)10));
        C.WriteLine(BetweenAgainstMax((short)20, (short)-10, (short)10));
        C.WriteLine(BetweenAgainstMax((short)-20, (short)-10, (short)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax((short)0, (short)10, (short)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ushort
        C.WriteLine(BetweenAgainstMax((ushort)0, (ushort)0, (ushort)10));
        C.WriteLine(BetweenAgainstMax((ushort)20, (ushort)0, (ushort)10));
        C.WriteLine(BetweenAgainstMax((ushort)0, (ushort)5, (ushort)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax((ushort)0, (ushort)20, (ushort)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///sbyte
        C.WriteLine(BetweenAgainstMax((sbyte)0, (sbyte)-10, (sbyte)10));
        C.WriteLine(BetweenAgainstMax((sbyte)20, (sbyte)-10, (sbyte)10));
        C.WriteLine(BetweenAgainstMax((sbyte)-20, (sbyte)-10, (sbyte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax((sbyte)0, (sbyte)10, (sbyte)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///byte
        C.WriteLine(BetweenAgainstMax((byte)0, (byte)0, (byte)10));
        C.WriteLine(BetweenAgainstMax((byte)20, (byte)0, (byte)10));
        C.WriteLine(BetweenAgainstMax((byte)0, (byte)5, (byte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax((byte)0, (byte)20, (byte)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///float
        C.WriteLine(BetweenAgainstMax(0f, -10f, 10f));
        C.WriteLine(BetweenAgainstMax(20f, -10f, 10f));
        C.WriteLine(BetweenAgainstMax(-20f, -10f, 10f));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax(0f, 10f, -10f));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(BetweenAgainstMax(0d, -10d, 10d));
        C.WriteLine(BetweenAgainstMax(20d, -10d, 10d));
        C.WriteLine(BetweenAgainstMax(-20d, -10d, 10d));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMax(0d, 10d, -10d));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region BetweenAgainstMin
        C.WriteLine("---BetweenAgainstMin---");

        ///int
        C.WriteLine(BetweenAgainstMin(0, -10, 10));
        C.WriteLine(BetweenAgainstMin(20, -10, 10));
        C.WriteLine(BetweenAgainstMin(-20, -10, 10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin(0, 10, -10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///uint
        C.WriteLine(BetweenAgainstMin(0u, 0u, 10u));
        C.WriteLine(BetweenAgainstMin(20u, 0u, 10u));
        C.WriteLine(BetweenAgainstMin(0u, 5u, 10u));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin(0, 20, 10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///long
        C.WriteLine(BetweenAgainstMin(0L, -10L, 10L));
        C.WriteLine(BetweenAgainstMin(20L, -10L, 10L));
        C.WriteLine(BetweenAgainstMin(-20L, -10L, 10L));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin(0L, 10L, -10L));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ulong
        C.WriteLine(BetweenAgainstMin(0uL, 0uL, 10uL));
        C.WriteLine(BetweenAgainstMin(20uL, 0uL, 10uL));
        C.WriteLine(BetweenAgainstMin(0uL, 5uL, 10uL));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin(0uL, 20uL, 10uL));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///short
        C.WriteLine(BetweenAgainstMin((short)0, (short)-10, (short)10));
        C.WriteLine(BetweenAgainstMin((short)20, (short)-10, (short)10));
        C.WriteLine(BetweenAgainstMin((short)-20, (short)-10, (short)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin((short)0, (short)10, (short)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ushort
        C.WriteLine(BetweenAgainstMin((ushort)0, (ushort)0, (ushort)10));
        C.WriteLine(BetweenAgainstMin((ushort)20, (ushort)0, (ushort)10));
        C.WriteLine(BetweenAgainstMin((ushort)0, (ushort)5, (ushort)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin((ushort)0, (ushort)20, (ushort)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///sbyte
        C.WriteLine(BetweenAgainstMin((sbyte)0, (sbyte)-10, (sbyte)10));
        C.WriteLine(BetweenAgainstMin((sbyte)20, (sbyte)-10, (sbyte)10));
        C.WriteLine(BetweenAgainstMin((sbyte)-20, (sbyte)-10, (sbyte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin((sbyte)0, (sbyte)10, (sbyte)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///byte
        C.WriteLine(BetweenAgainstMin((byte)0, (byte)0, (byte)10));
        C.WriteLine(BetweenAgainstMin((byte)20, (byte)0, (byte)10));
        C.WriteLine(BetweenAgainstMin((byte)0, (byte)5, (byte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin((byte)0, (byte)20, (byte)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///float
        C.WriteLine(BetweenAgainstMin(0f, -10f, 10f));
        C.WriteLine(BetweenAgainstMin(20f, -10f, 10f));
        C.WriteLine(BetweenAgainstMin(-20f, -10f, 10f));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin(0f, 10f, -10f));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(BetweenAgainstMin(0d, -10d, 10d));
        C.WriteLine(BetweenAgainstMin(20d, -10d, 10d));
        C.WriteLine(BetweenAgainstMin(-20d, -10d, 10d));
        try
        {
            ///MinMaxException Test
            C.WriteLine(BetweenAgainstMin(0d, 10d, -10d));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region Clamp
        C.WriteLine("---Clamp---");

        ///int
        C.WriteLine(Clamp(0, -10, 10));
        C.WriteLine(Clamp(20, -10, 10));
        C.WriteLine(Clamp(-20, -10, 10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp(0, 10, -10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///uint
        C.WriteLine(Clamp(0u, 0u, 10u));
        C.WriteLine(Clamp(20u, 0u, 10u));
        C.WriteLine(Clamp(0u, 5u, 10u));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp(0, 20, 10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///long
        C.WriteLine(Clamp(0L, -10L, 10L));
        C.WriteLine(Clamp(20L, -10L, 10L));
        C.WriteLine(Clamp(-20L, -10L, 10L));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp(0L, 10L, -10L));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ulong
        C.WriteLine(Clamp(0uL, 0uL, 10uL));
        C.WriteLine(Clamp(20uL, 0uL, 10uL));
        C.WriteLine(Clamp(0uL, 5uL, 10uL));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp(0uL, 20uL, 10uL));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///short
        C.WriteLine(Clamp((short)0, (short)-10, (short)10));
        C.WriteLine(Clamp((short)20, (short)-10, (short)10));
        C.WriteLine(Clamp((short)-20, (short)-10, (short)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp((short)0, (short)10, (short)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ushort
        C.WriteLine(Clamp((ushort)0, (ushort)0, (ushort)10));
        C.WriteLine(Clamp((ushort)20, (ushort)0, (ushort)10));
        C.WriteLine(Clamp((ushort)0, (ushort)5, (ushort)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp((ushort)0, (ushort)20, (ushort)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///sbyte
        C.WriteLine(Clamp((sbyte)0, (sbyte)-10, (sbyte)10));
        C.WriteLine(Clamp((sbyte)20, (sbyte)-10, (sbyte)10));
        C.WriteLine(Clamp((sbyte)-20, (sbyte)-10, (sbyte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp((sbyte)0, (sbyte)10, (sbyte)-10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///byte
        C.WriteLine(Clamp((byte)0, (byte)0, (byte)10));
        C.WriteLine(Clamp((byte)20, (byte)0, (byte)10));
        C.WriteLine(Clamp((byte)0, (byte)5, (byte)10));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp((byte)0, (byte)20, (byte)10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///float
        C.WriteLine(Clamp(0f, -10f, 10f));
        C.WriteLine(Clamp(20f, -10f, 10f));
        C.WriteLine(Clamp(-20f, -10f, 10f));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp(0f, 10f, -10f));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(Clamp(0d, -10d, 10d));
        C.WriteLine(Clamp(20d, -10d, 10d));
        C.WriteLine(Clamp(-20d, -10d, 10d));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Clamp(0d, 10d, -10d));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region Closest
        C.WriteLine("---Closest---");

        C.WriteLine(Closest(6, -1, 5));
        C.WriteLine(Closest(6, -1, 5, 10, 2, 65, 100));

        C.WriteLine(Closest(6u, 0u, 5u));
        C.WriteLine(Closest(6u, 0u, 5u, 10u, 2u, 65u, 100u));

        C.WriteLine(Closest(6f, -1f, 5f));
        C.WriteLine(Closest(6f, -1f, 5f, 10f, 2f, 65f, 100f));

        C.WriteLine(Closest(6d, -1d, 5d));

        C.WriteLine("-----\n");
        #endregion

        #region ClosestIndex
        C.WriteLine("---ClosestIndex---");

        C.WriteLine(ClosestIndex(6, new int[] { -1, 5, 10, 2, 65, 100 }));
        C.WriteLine(Closest(6u, new uint[] { 0u, 5u, 10u, 2u, 65u, 100u }));
        C.WriteLine(Closest(6f, new float[] { -1f, 5f, 10f, 2f, 65f, 100f }));

        C.WriteLine("-----\n");
        #endregion

        #region Cube
        C.WriteLine("---Cube---");

        C.WriteLine(Cube(0));
        C.WriteLine(Cube(1));
        C.WriteLine(Cube(2));
        C.WriteLine(Cube(5));
        C.WriteLine(Cube(10));
        C.WriteLine(Cube(-1));
        C.WriteLine(Cube(-2));
        C.WriteLine(Cube(-5));
        C.WriteLine(Cube(-10));

        C.WriteLine(Cube(0u));
        C.WriteLine(Cube(1u));
        C.WriteLine(Cube(2u));
        C.WriteLine(Cube(5u));
        C.WriteLine(Cube(10u));

        C.WriteLine(Cube(0L));
        C.WriteLine(Cube(1L));
        C.WriteLine(Cube(2L));
        C.WriteLine(Cube(5L));
        C.WriteLine(Cube(10L));
        C.WriteLine(Cube(-1L));
        C.WriteLine(Cube(-2L));
        C.WriteLine(Cube(-5L));
        C.WriteLine(Cube(-10L));

        C.WriteLine(Cube(0uL));
        C.WriteLine(Cube(1uL));
        C.WriteLine(Cube(2uL));
        C.WriteLine(Cube(5uL));
        C.WriteLine(Cube(10uL));

        C.WriteLine(Cube(0f));
        C.WriteLine(Cube(1f));
        C.WriteLine(Cube(2f));
        C.WriteLine(Cube(1.5f));
        C.WriteLine(Cube(2.5f));
        C.WriteLine(Cube(-1f));
        C.WriteLine(Cube(-2f));
        C.WriteLine(Cube(-1.5f));
        C.WriteLine(Cube(-2.5f));

        C.WriteLine(Cube(0d));
        C.WriteLine(Cube(1d));
        C.WriteLine(Cube(2d));
        C.WriteLine(Cube(1.5d));
        C.WriteLine(Cube(2.5d));
        C.WriteLine(Cube(-1d));
        C.WriteLine(Cube(-2d));
        C.WriteLine(Cube(-1.5d));
        C.WriteLine(Cube(-2.5d));

        C.WriteLine("-----\n");
        #endregion

        #region Farthest
        C.WriteLine("---Farthest---");

        C.WriteLine(Farthest(6, -1, 5));
        C.WriteLine(Farthest(6, -1, 5, 10, 2, 65, 100));

        C.WriteLine(Farthest(6u, 0u, 5u));
        C.WriteLine(Farthest(6u, 0u, 5u, 10u, 2u, 65u, 100u));

        C.WriteLine(Farthest(6f, -1f, 5f));
        C.WriteLine(Farthest(6f, -1f, 5f, 10f, 2f, 65f, 100f));

        C.WriteLine(Farthest(6d, -1d, 5d));

        C.WriteLine("-----\n");
        #endregion

        #region FarthestIndex
        C.WriteLine("---FarthestIndex---");

        C.WriteLine(FarthestIndex(6, new int[] { -1, 5, 10, 2, 65, 100 }));
        C.WriteLine(FarthestIndex(6u, new uint[] { 0u, 5u, 10u, 2u, 65u, 100u }));
        C.WriteLine(FarthestIndex(6f, new float[] { -1f, 5f, 10f, 2f, 65f, 100f }));

        C.WriteLine("-----\n");
        #endregion

        #region FixFraction
        C.WriteLine("---FixFraction---");

        C.WriteLine(FixFraction(0f));
        C.WriteLine(FixFraction(1f));
        C.WriteLine(FixFraction(0.5f));
        C.WriteLine(FixFraction(1.25f));
        C.WriteLine(FixFraction(0.0001f));
        C.WriteLine(FixFraction(0.001f));
        C.WriteLine(FixFraction(0.01f));
        C.WriteLine(FixFraction(0.9999f));
        C.WriteLine(FixFraction(0.999f));
        C.WriteLine(FixFraction(0.99f));
        C.WriteLine(FixFraction(5.0001f));
        C.WriteLine(FixFraction(4.9999f));

        C.WriteLine("-----\n");
        #endregion

        #region HasFraction
        C.WriteLine("---HasFraction---");

        C.WriteLine(HasFraction(0f));
        C.WriteLine(HasFraction(0.5f));
        C.WriteLine(HasFraction(0.99f));
        C.WriteLine(HasFraction(1f));
        C.WriteLine(HasFraction(HalfPi));
        C.WriteLine(HasFraction(Pi));
        C.WriteLine(HasFraction(ThreeQuartersTau));
        C.WriteLine(HasFraction(Tau));

        C.WriteLine(HasFraction(0d));
        C.WriteLine(HasFraction(0.5d));
        C.WriteLine(HasFraction(0.99d));
        C.WriteLine(HasFraction(1d));
        C.WriteLine(HasFraction((double)HalfPi));
        C.WriteLine(HasFraction((double)Pi));
        C.WriteLine(HasFraction((double)ThreeQuartersTau));
        C.WriteLine(HasFraction((double)Tau));

        C.WriteLine("-----\n");
        #endregion

        #region IsOdd
        C.WriteLine("---IsOdd---");

        for (int i = -10; i < 11; i++)
            C.WriteLine($"{i} => {IsOdd(i)}");

        C.WriteLine("-----\n");
        #endregion

        #region Max
        C.WriteLine("---Max---");

        C.WriteLine(Max(6, -1));
        C.WriteLine(Max(6, -1, 5, 10, 2, 65, 100));

        C.WriteLine(Max(6u, 0u));
        C.WriteLine(Max(6u, 0u, 5u, 10u, 2u, 65u, 100u));

        C.WriteLine(Max(6L, -1L));
        C.WriteLine(Max(6L, -1L, 5L, 10L, 2L, 65L, 100L));

        C.WriteLine(Max(6uL, 0uL));
        C.WriteLine(Max(6uL, 0uL, 5uL, 10uL, 2uL, 65uL, 100uL));

        C.WriteLine(Max((short)6, (short)-1));
        C.WriteLine(Max((short)6, (short)-1, (short)5, (short)10, (short)2, (short)65, (short)100));

        C.WriteLine(Max((ushort)6, (ushort)0));
        C.WriteLine(Max((ushort)6, (ushort)0, (ushort)5, (ushort)10, (ushort)2, (ushort)65, (ushort)100));

        C.WriteLine(Max((sbyte)6, (sbyte)-1));
        C.WriteLine(Max((sbyte)6, (sbyte)-1, (sbyte)5, (sbyte)10, (sbyte)2, (sbyte)65, (sbyte)100));

        C.WriteLine(Max((byte)6, (byte)0));
        C.WriteLine(Max((byte)6, (byte)0, (byte)5, (byte)10, (byte)2, (byte)65, (byte)100));

        C.WriteLine(Max(6f, -1f));
        C.WriteLine(Max(6f, -1f, 5f, 10f, 2f, 65f, 100f));

        C.WriteLine(Max(6d, -1d));
        C.WriteLine(Max(6d, -1d, 5d, 10d, 2d, 65d, 100d));

        C.WriteLine("-----\n");
        #endregion

        #region MaxIndex
        C.WriteLine("---MaxIndex---");

        C.WriteLine(MaxIndex(new int[] { 6, -1, 5, 10, 2, 65, 100 }));

        C.WriteLine(MaxIndex(new uint[] { 6u, 0u, 5u, 10u, 2u, 65u, 100u }));

        C.WriteLine(MaxIndex(new long[] { 6L, -1L, 5L, 10L, 2L, 65L, 100L }));

        C.WriteLine(MaxIndex(new ulong[] { 6uL, 0uL, 5uL, 10uL, 2uL, 65uL, 100uL }));

        C.WriteLine(MaxIndex(new short[] { (short)6, (short)-1, (short)5, (short)10, (short)2, (short)65, (short)100 }));

        C.WriteLine(MaxIndex(new ushort[] { (ushort)6, (ushort)0, (ushort)5, (ushort)10, (ushort)2, (ushort)65, (ushort)100 }));

        C.WriteLine(MaxIndex(new sbyte[] { (sbyte)6, (sbyte)-1, (sbyte)5, (sbyte)10, (sbyte)2, (sbyte)65, (sbyte)100 }));

        C.WriteLine(MaxIndex(new byte[] { (byte)6, (byte)0, (byte)5, (byte)10, (byte)2, (byte)65, (byte)100 }));

        C.WriteLine(MaxIndex(new float[] { 6f, -1f, 5f, 10f, 2f, 65f, 100f }));

        C.WriteLine(MaxIndex(new double[] { 6d, -1d, 5d, 10d, 2d, 65d, 100d }));

        C.WriteLine("-----\n");
        #endregion

        #region Min
        C.WriteLine("---Min---");

        C.WriteLine(Min(6, -1));
        C.WriteLine(Min(6, -1, 5, 10, 2, 65, 100));

        C.WriteLine(Min(6u, 0u));
        C.WriteLine(Min(6u, 0u, 5u, 10u, 2u, 65u, 100u));

        C.WriteLine(Min(6L, -1L));
        C.WriteLine(Min(6L, -1L, 5L, 10L, 2L, 65L, 100L));

        C.WriteLine(Min(6uL, 0uL));
        C.WriteLine(Min(6uL, 0uL, 5uL, 10uL, 2uL, 65uL, 100uL));

        C.WriteLine(Min((short)6, (short)-1));
        C.WriteLine(Min((short)6, (short)-1, (short)5, (short)10, (short)2, (short)65, (short)100));

        C.WriteLine(Min((ushort)6, (ushort)0));
        C.WriteLine(Min((ushort)6, (ushort)0, (ushort)5, (ushort)10, (ushort)2, (ushort)65, (ushort)100));

        C.WriteLine(Min((sbyte)6, (sbyte)-1));
        C.WriteLine(Min((sbyte)6, (sbyte)-1, (sbyte)5, (sbyte)10, (sbyte)2, (sbyte)65, (sbyte)100));

        C.WriteLine(Min((byte)6, (byte)0));
        C.WriteLine(Min((byte)6, (byte)0, (byte)5, (byte)10, (byte)2, (byte)65, (byte)100));

        C.WriteLine(Min(6f, -1f));
        C.WriteLine(Min(6f, -1f, 5f, 10f, 2f, 65f, 100f));

        C.WriteLine(Min(6d, -1d));
        C.WriteLine(Min(6d, -1d, 5d, 10d, 2d, 65d, 100d));

        C.WriteLine("-----\n");
        #endregion

        #region MinIndex
        C.WriteLine("---MinIndex---");

        C.WriteLine(MinIndex(new int[] { 6, -1, 5, 10, 2, 65, 100 }));

        C.WriteLine(MinIndex(new uint[] { 6u, 0u, 5u, 10u, 2u, 65u, 100u }));

        C.WriteLine(MinIndex(new long[] { 6L, -1L, 5L, 10L, 2L, 65L, 100L }));

        C.WriteLine(MinIndex(new ulong[] { 6uL, 0uL, 5uL, 10uL, 2uL, 65uL, 100uL }));

        C.WriteLine(MinIndex(new short[] { (short)6, (short)-1, (short)5, (short)10, (short)2, (short)65, (short)100 }));

        C.WriteLine(MinIndex(new ushort[] { (ushort)6, (ushort)0, (ushort)5, (ushort)10, (ushort)2, (ushort)65, (ushort)100 }));

        C.WriteLine(MinIndex(new sbyte[] { (sbyte)6, (sbyte)-1, (sbyte)5, (sbyte)10, (sbyte)2, (sbyte)65, (sbyte)100 }));

        C.WriteLine(MinIndex(new byte[] { (byte)6, (byte)0, (byte)5, (byte)10, (byte)2, (byte)65, (byte)100 }));

        C.WriteLine(MinIndex(new float[] { 6f, -1f, 5f, 10f, 2f, 65f, 100f }));

        C.WriteLine(MinIndex(new double[] { 6d, -1d, 5d, 10d, 2d, 65d, 100d }));

        C.WriteLine("-----\n");
        #endregion

        #region MoveToZero
        C.WriteLine("---MoveToZero---");

        C.WriteLine(MoveToZero(10, 1));
        C.WriteLine(MoveToZero(-10, 2));
        C.WriteLine(MoveToZero(10, 5));
        C.WriteLine(MoveToZero(10, 20));

        C.WriteLine(MoveToZero(10L, 1L));
        C.WriteLine(MoveToZero(-10L, 2L));
        C.WriteLine(MoveToZero(10L, 5L));
        C.WriteLine(MoveToZero(10L, 20L));

        C.WriteLine(MoveToZero(10f, 1f));
        C.WriteLine(MoveToZero(-10f, 2f));
        C.WriteLine(MoveToZero(10f, 5f));
        C.WriteLine(MoveToZero(10f, 20f));

        C.WriteLine(MoveToZero(10d, 1d));
        C.WriteLine(MoveToZero(-10d, 2d));
        C.WriteLine(MoveToZero(10d, 5d));
        C.WriteLine(MoveToZero(10d, 20d));

        C.WriteLine("-----\n");
        #endregion

        #region Pow
        C.WriteLine("---Pow---");

        C.WriteLine(Pow(0, 0));
        C.WriteLine(Pow(0, 1));
        C.WriteLine(Pow(1, 1));
        C.WriteLine(Pow(1, -1));
        C.WriteLine(Pow(81, 1));
        C.WriteLine(Pow(2, 2));
        C.WriteLine(Pow(4, 2));
        C.WriteLine(Pow(4, 3));
        C.WriteLine(Pow(5, 10));

        C.WriteLine(Pow(0f, 0));
        C.WriteLine(Pow(0f, 1));
        C.WriteLine(Pow(1f, 1));
        C.WriteLine(Pow(1f, -1));
        C.WriteLine(Pow(81f, 1));
        C.WriteLine(Pow(2f, 2));
        C.WriteLine(Pow(4f, 2));
        C.WriteLine(Pow(4f, 3));
        C.WriteLine(Pow(5f, 10));
        C.WriteLine(Pow(2.5f, 2));
        C.WriteLine(Pow(4.25f, 2));
        C.WriteLine(Pow(4.99f, 3));
        C.WriteLine(Pow(5.0001f, 10));

        C.WriteLine("-----\n");
        #endregion

        #region Round
        C.WriteLine("---Round---");

        C.WriteLine(Round(0f));
        /////////////////////////

        C.WriteLine("-----\n");
        #endregion

        #region Sign
        C.WriteLine("---Sign---");

        C.WriteLine($"{FindSign(0)} : {FindSignInt(0)}");
        C.WriteLine($"{FindSign(1)} : {FindSignInt(1)}");
        C.WriteLine($"{FindSign(2)} : {FindSignInt(2)}");
        C.WriteLine($"{FindSign(10)} : {FindSignInt(10)}");
        C.WriteLine($"{FindSign(-1)} : {FindSignInt(-1)}");
        C.WriteLine($"{FindSign(-2)} : {FindSignInt(-2)}");
        C.WriteLine($"{FindSign(-10)} : {FindSignInt(-10)}");

        C.WriteLine("-----\n");
        #endregion

        #region Sqr
        C.WriteLine("---Sqr---");

        C.WriteLine(Sqr(0));
        C.WriteLine(Sqr(1));
        C.WriteLine(Sqr(2));
        C.WriteLine(Sqr(5));
        C.WriteLine(Sqr(10));
        C.WriteLine(Sqr(-1));
        C.WriteLine(Sqr(-2));
        C.WriteLine(Sqr(-5));
        C.WriteLine(Sqr(-10));

        C.WriteLine(Sqr(0u));
        C.WriteLine(Sqr(1u));
        C.WriteLine(Sqr(2u));
        C.WriteLine(Sqr(5u));
        C.WriteLine(Sqr(10u));

        C.WriteLine(Sqr(0L));
        C.WriteLine(Sqr(1L));
        C.WriteLine(Sqr(2L));
        C.WriteLine(Sqr(5L));
        C.WriteLine(Sqr(10L));
        C.WriteLine(Sqr(-1L));
        C.WriteLine(Sqr(-2L));
        C.WriteLine(Sqr(-5L));
        C.WriteLine(Sqr(-10L));

        C.WriteLine(Sqr(0uL));
        C.WriteLine(Sqr(1uL));
        C.WriteLine(Sqr(2uL));
        C.WriteLine(Sqr(5uL));
        C.WriteLine(Sqr(10uL));

        C.WriteLine(Sqr(0f));
        C.WriteLine(Sqr(1f));
        C.WriteLine(Sqr(2f));
        C.WriteLine(Sqr(1.5f));
        C.WriteLine(Sqr(2.5f));
        C.WriteLine(Sqr(-1f));
        C.WriteLine(Sqr(-2f));
        C.WriteLine(Sqr(-1.5f));
        C.WriteLine(Sqr(-2.5f));

        C.WriteLine(Sqr(0d));
        C.WriteLine(Sqr(1d));
        C.WriteLine(Sqr(2d));
        C.WriteLine(Sqr(1.5d));
        C.WriteLine(Sqr(2.5d));
        C.WriteLine(Sqr(-1d));
        C.WriteLine(Sqr(-2d));
        C.WriteLine(Sqr(-1.5d));
        C.WriteLine(Sqr(-2.5d));

        C.WriteLine("-----\n");
        #endregion

        #region SqrRoot
        C.WriteLine("---SqrRoot---");

        C.WriteLine(SqrRoot(0));
        C.WriteLine(SqrRoot(1));
        C.WriteLine(SqrRoot(2));
        C.WriteLine(SqrRoot(3));
        C.WriteLine(SqrRoot(4));
        C.WriteLine(SqrRoot(10));
        C.WriteLine(SqrRoot(15));
        C.WriteLine(SqrRoot(20));
        C.WriteLine(SqrRoot(50));
        C.WriteLine(SqrRoot(100));

        C.WriteLine(SqrRoot(0f));
        C.WriteLine(SqrRoot(1f));
        C.WriteLine(SqrRoot(2f));
        C.WriteLine(SqrRoot(3f));
        C.WriteLine(SqrRoot(4f));
        C.WriteLine(SqrRoot(10f));
        C.WriteLine(SqrRoot(15f));
        C.WriteLine(SqrRoot(20f));
        C.WriteLine(SqrRoot(50f));
        C.WriteLine(SqrRoot(100f));

        C.WriteLine(SqrRoot(0.5f));
        C.WriteLine(SqrRoot(1.5f));
        C.WriteLine(SqrRoot(2.5f));
        C.WriteLine(SqrRoot(3.5f));
        C.WriteLine(SqrRoot(4.5f));
        C.WriteLine(SqrRoot(10.5f));
        C.WriteLine(SqrRoot(15.5f));
        C.WriteLine(SqrRoot(20.5f));
        C.WriteLine(SqrRoot(50.5f));
        C.WriteLine(SqrRoot(100.5f));

        C.WriteLine("-----\n");
        #endregion

        #region StepInterpolate
        C.WriteLine("---StepInterpolate---");

        C.WriteLine(string.Join(',', StepInterpolate(0f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.5f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.25f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.75f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.0001f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.9999f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.2f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.3f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.4f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.7f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.9f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.35f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.65f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.625f)));
        C.WriteLine(string.Join(',', StepInterpolate(0.325f)));

        //C.WriteLine(string.Join(',', StepInterpolate(0, 0)));

        C.WriteLine(string.Join(',', StepInterpolate(-0.75f)));

        C.WriteLine("-----\n");
        #endregion

        #region Wrap
        C.WriteLine("---Wrap---");

        ///int
        C.WriteLine(Wrap(0, -10, 10));
        C.WriteLine(Wrap(20, -10, 10));
        C.WriteLine(Wrap(-20, -10, 10));
        C.WriteLine(Wrap(20, 0, 0));
        C.WriteLine(Wrap(200, 0, 100));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0, 10, -10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///uint
        C.WriteLine(Wrap(0u, 0u, 10u));
        C.WriteLine(Wrap(20u, 0u, 10u));
        C.WriteLine(Wrap(0u, 5u, 10u));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0, 20, 10));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///long
        C.WriteLine(Wrap(0L, -10L, 10L));
        C.WriteLine(Wrap(20L, -10L, 10L));
        C.WriteLine(Wrap(-20L, -10L, 10L));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0L, 10L, -10L));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///ulong
        C.WriteLine(Wrap(0uL, 0uL, 10uL));
        C.WriteLine(Wrap(20uL, 0uL, 10uL));
        C.WriteLine(Wrap(0uL, 5uL, 10uL));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0uL, 20uL, 10uL));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///float
        C.WriteLine(Wrap(0f, -10f, 10f));
        C.WriteLine(Wrap(20f, -10f, 10f));
        C.WriteLine(Wrap(-20f, -10f, 10f));
        C.WriteLine(Wrap(-20f, -10.5f, 10.5f));
        C.WriteLine(Wrap(-20.9999f, -10.01f, 10.002f));
        C.WriteLine(Wrap(2f, 0f, 1f));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0f, 10f, -10f));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(Wrap(0d, -10d, 10d));
        C.WriteLine(Wrap(20d, -10d, 10d));
        C.WriteLine(Wrap(-20d, -10d, 10d));
        C.WriteLine(Wrap(-20d, -10.5d, 10.5d));
        C.WriteLine(Wrap(-20.9999d, -10.01d, 10.002d));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0d, 10d, -10d));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        ///double
        C.WriteLine(Wrap(0m, -10m, 10m));
        C.WriteLine(Wrap(20m, -10m, 10m));
        C.WriteLine(Wrap(-20m, -10m, 10m));
        C.WriteLine(Wrap(-20m, -10.5m, 10.5m));
        C.WriteLine(Wrap(-20.9999m, -10.01m, 10.002m));
        try
        {
            ///MinMaxException Test
            C.WriteLine(Wrap(0m, 10m, -10m));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion
    }

    public static void MathSineTest()
    {
        #region SineFirstQuarter
        C.WriteLine("---SineFirstQuarter---");

        for (float f = 0f; f <= 1f; f += 0.01f) C.WriteLine($"{f} => {SineFirstQuarter(f)}");
        C.WriteLine(SineFirstQuarter(1f));

        C.WriteLine("-----\n");
        #endregion

        #region SineCosine
        C.WriteLine("---SineCosine---");

        SinePrint(0f);
        SinePrint(0.5f);
        SinePrint(1f);
        SinePrint(0.25f);
        SinePrint(0.75f);
        SinePrint(0.1f);
        SinePrint(2f);
        SinePrint(1.00001f);
        SinePrint(1.56464658f);
        SinePrint(-1f);
        SinePrint(-0.1f);

        C.WriteLine("-----\n");
        #endregion
    }

    private static void SinePrint(float amount)
    {
        SineCosine(amount, out float sine, out float cosine);
        C.WriteLine($"Sine = {sine}, Cosine = {cosine}");
    }

    public static void FloatStepInterpolateTest()
    {

    }

    public static void VectorStepInterpolateTest()
    {
        string s = C.ReadLine();
        int comma = s.IndexOf(',');
        if (comma > -1)
        {
            if (int.TryParse(s.Substring(0, comma), out int x) && int.TryParse(s.Substring(Clamp(comma + 1, 0, s.Length)), out int y))
            {
                Vector v = new(x, y);
                Vector[] steps = Vector.StepInterpolate(v);

                if (steps == null) C.WriteLine("null");
                else if (steps.Length == 0) C.WriteLine("empty");
                else
                {
                    string outPut = "";
                    Vector dist = default;

                    foreach (Vector step in steps)
                    {
                        dist += step;
                        outPut += $"{step},";
                    }
                    C.WriteLine($"{v} => {dist} => {outPut}");
                }
            }
        }
        VectorStepInterpolateTest();
    }
}
