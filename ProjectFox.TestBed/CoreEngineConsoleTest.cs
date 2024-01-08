using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;
using static ProjectFox.CoreEngine.Math.Vector;
using ProjectFox.CoreEngine.Collections;
using static ProjectFox.CoreEngine.Collections.Strings;
using ProjectFox.CoreEngine.Utility;

using ProjectFox.GameEngine;
using System.Net;

namespace ProjectFox.TestBed;

internal static class CoreEngineConsoleTest//split this into partial classes
{
    #region Math Test
    internal static void MathTest()
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
        C.WriteLine(ClosestIndex(6, new int[] { -1, 5, 10, 2, 65, 100 }));

        C.WriteLine(Closest(6u, 0u, 5u));
        C.WriteLine(Closest(6u, 0u, 5u, 10u, 2u, 65u, 100u));

        C.WriteLine(Closest(6f, -1f, 5f));
        C.WriteLine(Closest(6f, -1f, 5f, 10f, 2f, 65f, 100f));

        C.WriteLine(Closest(6d, -1d, 5d));

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
        
        #region Sine
        C.WriteLine("---Sine---");

        //C.WriteLine(Sine(0f));
        //C.WriteLine(SineRadians(0f));
        //C.WriteLine(SineDegrees(0f));

        //C.WriteLine(Sine(0.5f));
        //C.WriteLine(SineRadians(Pi));
        //C.WriteLine(SineDegrees(180f));

        //C.WriteLine(Sine(1f));
        //C.WriteLine(SineRadians(Tau));
        //C.WriteLine(SineDegrees(360f));

        //C.WriteLine(Sine(0.25f));
        //C.WriteLine(SineRadians(HalfPi));
        //C.WriteLine(SineDegrees(90f));

        //C.WriteLine(Sine(0.75f));
        //C.WriteLine(SineRadians(ThreeQuartersTau));
        //C.WriteLine(SineDegrees(270f));

        //C.WriteLine(Sine(0.1f));
        //C.WriteLine(Sine(2f));
        //C.WriteLine(Sine(1.00001f));//wrapped arguments arent' always right
        //C.WriteLine(Sine(1.56464658f));
        //C.WriteLine(SineRadians(3f));
        //C.WriteLine(SineDegrees(25f));

        C.WriteLine("-----\n");
        #endregion

        #region Cosine
        C.WriteLine("---Cosine---");

        /*C.WriteLine(Cosine(0f));
        C.WriteLine(CosineRadians(0f));
        C.WriteLine(CosineDegrees(0f));

        C.WriteLine(Cosine(0.5f));
        C.WriteLine(CosineRadians(Pi));
        C.WriteLine(CosineDegrees(180f));

        C.WriteLine(Cosine(1f));
        C.WriteLine(CosineRadians(Tau));
        C.WriteLine(CosineDegrees(360f));

        C.WriteLine(Cosine(0.25f));
        C.WriteLine(CosineRadians(HalfPi));
        C.WriteLine(CosineDegrees(90f));

        C.WriteLine(Cosine(0.75f));
        C.WriteLine(CosineRadians(ThreeQuartersTau));
        C.WriteLine(CosineDegrees(270f));

        C.WriteLine(Cosine(0.1f));
        C.WriteLine(CosineRadians(3f));
        C.WriteLine(CosineDegrees(25f));*/

        C.WriteLine("-----\n");
        #endregion

        #region SineCosine
        C.WriteLine("---SineCosine---");

        /*SineCosine(0f, out float sine, out float cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineRadians(0f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineDegrees(0f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");

        SineCosine(0.5f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineRadians(Pi, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineDegrees(180f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");

        SineCosine(1f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineRadians(Tau, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineDegrees(360f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");

        SineCosine(0.25f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineRadians(HalfPi, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineDegrees(90f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");

        SineCosine(0.75f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineRadians(ThreeQuartersTau, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineDegrees(270f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");

        SineCosine(0.1f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineRadians(3f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");
        SineCosineDegrees(25f, out sine, out cosine);
        C.WriteLine($"{sine}, {cosine}");*/

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

        #region Structs
        #region Vector
        C.WriteLine("---Vector Static---");

        C.WriteLine(ToBinString((byte)Vector.Direction.Zero));
        C.WriteLine(ToBinString((byte)Vector.Direction.YNeg));
        C.WriteLine(ToBinString((byte)Vector.Direction.PosNegQuad));
        C.WriteLine(ToBinString((byte)Vector.Direction.XPos));
        C.WriteLine(ToBinString((byte)Vector.Direction.PosQuad));
        C.WriteLine(ToBinString((byte)Vector.Direction.YPos));
        C.WriteLine(ToBinString((byte)Vector.Direction.NegPosQuad));
        C.WriteLine(ToBinString((byte)Vector.Direction.XNeg));
        C.WriteLine(ToBinString((byte)Vector.Direction.NegQuad));

        C.WriteLine(ToBinString((byte)Vector.Direction.Center));
        C.WriteLine(ToBinString((byte)Vector.Direction.Up));
        C.WriteLine(ToBinString((byte)Vector.Direction.UpRight));
        C.WriteLine(ToBinString((byte)Vector.Direction.Right));
        C.WriteLine(ToBinString((byte)Vector.Direction.DownRight));
        C.WriteLine(ToBinString((byte)Vector.Direction.Down));
        C.WriteLine(ToBinString((byte)Vector.Direction.DownLeft));
        C.WriteLine(ToBinString((byte)Vector.Direction.Left));
        C.WriteLine(ToBinString((byte)Vector.Direction.Up));

        C.WriteLine(Vector.FindDirection(Sign.Zero, Sign.Zero));
        C.WriteLine(Vector.FindDirection(Sign.Neg, Sign.Neg));
        C.WriteLine(Vector.FindDirection(Sign.Pos, Sign.Pos));
        C.WriteLine(Vector.FindDirection(Sign.Pos, Sign.Neg));
        C.WriteLine(Vector.FindDirection(Sign.Zero, Sign.Neg));

        C.WriteLine(Vector.FindDirection(false, false, false, false));
        C.WriteLine(Vector.FindDirection(true, false, false, false));
        C.WriteLine(Vector.FindDirection(false, true, false, true));
        C.WriteLine(Vector.FindDirection(false, true, true, false));

        Vector[] vs = Vector.StepInterpolate(default);
        C.WriteLine(vs == null /*|| vs.Length == 0*/ ? "null" : string.Join(',', vs));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(1, 1))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(10, 10))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(-8, 8))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(1, 0))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(0, 1))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(15, 0))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(0, 25))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(2, 5))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(4, 10))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(5, 10))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(2, 4))));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(3, 2))));//this one gives (3, 1)

        vs = Vector.StepInterpolate(default, new(1, 1));
        C.WriteLine(vs == null ? "null" : string.Join(',', vs));
        C.WriteLine(string.Join(',', Vector.StepInterpolate(new(2, 5), new(10, 10))));

        C.WriteLine("---Vector---");

        Vector v = new(2, 4);
        Vector vBigger = new(10, 10);
        Vector vSmaller = new(1, -5);
        int vBiggert = 5;
        int vSmallert = -10;
        VectorF vBiggerF = new(5.5f, 5.5f);
        VectorF vSmallerF = new(-1.25f, -10.99f);

        IVectorTest(v, vBigger, vSmaller, vBiggert, vSmallert, vBiggerF, vSmallerF);

        C.WriteLine("-Operators-");

        #region vector
        C.WriteLine((VectorF)v);
        C.WriteLine((VectorZ)v);
        C.WriteLine((VectorZF)v);

        C.WriteLine(v++);
        C.WriteLine(v);
        C.WriteLine(++v);
        C.WriteLine(v--);
        C.WriteLine(v);
        C.WriteLine(--v);
        C.WriteLine(-v);
        C.WriteLine(~v);
        #endregion

        #region vector_vector
        C.WriteLine(v == vBigger);
        C.WriteLine(v == vSmaller);
        C.WriteLine(v != vBigger);
        C.WriteLine(v != vSmaller);
        C.WriteLine(v + vBigger);
        C.WriteLine(v + vSmaller);
        C.WriteLine(v - vBigger);
        C.WriteLine(v - vSmaller);
        C.WriteLine(v * vBigger);
        C.WriteLine(v * vSmaller);
        C.WriteLine(v / vBigger);
        C.WriteLine(v / vSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v / new Vector(0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(v % vBigger);
        C.WriteLine(v % vSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v % new Vector(0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(v & vBigger);
        C.WriteLine(v & vSmaller);
        C.WriteLine(v | vBigger);
        C.WriteLine(v | vSmaller);
        C.WriteLine(v ^ vBigger);
        C.WriteLine(v ^ vSmaller);
        #endregion

        #region vector_vectorf
        C.WriteLine(v == vBiggerF);
        C.WriteLine(v == vSmallerF);
        C.WriteLine(v != vBiggerF);
        C.WriteLine(v != vSmallerF);
        #endregion

        #region vector_int
        C.WriteLine(v == vBiggert);
        C.WriteLine(v == vSmallert);
        C.WriteLine(v == 1);
        C.WriteLine(v != vBiggert);
        C.WriteLine(v != vSmallert);
        C.WriteLine(v != 1);
        C.WriteLine(v + vBiggert);
        C.WriteLine(v + vSmallert);
        C.WriteLine(v + 1);
        C.WriteLine(v - vBiggert);
        C.WriteLine(v - vSmallert);
        C.WriteLine(v - 1);
        C.WriteLine(v * vBiggert);
        C.WriteLine(v * vSmallert);
        C.WriteLine(v * 1);
        C.WriteLine(v / vBiggert);
        C.WriteLine(v / vSmallert);
        C.WriteLine(v / 1);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v / 0);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(v % vBiggert);
        C.WriteLine(v % vSmallert);
        C.WriteLine(v % 1);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v % 0);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(v & vBiggert);
        C.WriteLine(v & vSmallert);
        C.WriteLine(v & 0);
        C.WriteLine(v | vBiggert);
        C.WriteLine(v | vSmallert);
        C.WriteLine(v | 0);
        C.WriteLine(v ^ vBiggert);
        C.WriteLine(v ^ vSmallert);
        C.WriteLine(v ^ 0);
        #endregion

        #region vector_float
        C.WriteLine(v + 1f);
        C.WriteLine(v - 1f);
        C.WriteLine(v * 1f);
        C.WriteLine(v / 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v / 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(v % 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v % 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        C.WriteLine("-----\n");
        #endregion

        #region VectorF
        C.WriteLine("---VectorF---");

        VectorF vf = new(2f, 4f);
        VectorF vfBigger = new(10f, 10f);
        VectorF vfSmaller = new(1f, -5f);
        float vfBiggert = 5f;
        float vfSmallert = -10f;
        VectorF vfBiggerF = new(5.5f, 5.5f);
        VectorF vfSmallerF = new(-1.25f, -10.99f);

        IVectorTest(vf, vfBigger, vfSmaller, vfBiggert, vfSmallert, vfBiggerF, vfSmallerF);

        C.WriteLine("-Operators-");

        #region vectorf
        C.WriteLine((Vector)vf);
        C.WriteLine((VectorZ)vf);
        C.WriteLine((VectorZF)vf);

        C.WriteLine(vf++);
        C.WriteLine(vf);
        C.WriteLine(++vf);
        C.WriteLine(vf--);
        C.WriteLine(vf);
        C.WriteLine(--vf);
        C.WriteLine(-vf);
        C.WriteLine(~vf);
        #endregion

        #region vectorf_vectorf
        C.WriteLine(vf == vfBigger);
        C.WriteLine(vf == vfSmaller);
        C.WriteLine(vf != vfBigger);
        C.WriteLine(vf != vfSmaller);
        C.WriteLine(vf + vfBigger);
        C.WriteLine(vf + vfSmaller);
        C.WriteLine(vf - vfBigger);
        C.WriteLine(vf - vfSmaller);
        C.WriteLine(vf * vfBigger);
        C.WriteLine(vf * vfSmaller);
        C.WriteLine(vf / vfBigger);
        C.WriteLine(vf / vfSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vf / new VectorF(0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vf % vfBigger);
        C.WriteLine(vf % vfSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vf % new VectorF(0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        #region vectorf_vector
        C.WriteLine(vf == v);
        C.WriteLine(vf != v);
        C.WriteLine(vf + v);
        C.WriteLine(vf - v);
        C.WriteLine(vf * v);
        C.WriteLine(vf / v);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vf / new Vector(0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vf % v);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vf % new Vector(0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(v == vf);
        C.WriteLine(v != vf);
        C.WriteLine(v + vf);
        C.WriteLine(v - vf);
        C.WriteLine(v * vf);
        C.WriteLine(v / vf);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v / new VectorF(0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(v % vf);
        try
        {
            ///DivideByZero Test
            C.WriteLine(v % new VectorF(0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        #region vectorf_float
        C.WriteLine(vf == vfBiggert);
        C.WriteLine(vf == vfSmallert);
        C.WriteLine(vf == 1f);
        C.WriteLine(vf != vfBiggert);
        C.WriteLine(vf != vfSmallert);
        C.WriteLine(vf != 1f);
        C.WriteLine(vf + vfBiggert);
        C.WriteLine(vf + vfSmallert);
        C.WriteLine(vf + 1f);
        C.WriteLine(vf - vfBiggert);
        C.WriteLine(vf - vfSmallert);
        C.WriteLine(vf - 1f);
        C.WriteLine(vf * vfBiggert);
        C.WriteLine(vf * vfSmallert);
        C.WriteLine(vf * 1f);
        C.WriteLine(vf / vfBiggert);
        C.WriteLine(vf / vfSmallert);
        C.WriteLine(vf / 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vf / 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vf % vfBiggert);
        C.WriteLine(vf % vfSmallert);
        C.WriteLine(vf % 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vf % 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        C.WriteLine("-----\n");
        #endregion

        #region VectorZ
        C.WriteLine("---VectorZ---");

        VectorZ vz = new(2, 4, 5);
        VectorZ vzBigger = new(10, 10, 10);
        VectorZ vzSmaller = new(1, -5, -1);
        int vzBiggert = 5;
        int vzSmallert = -10;
        VectorZF vzBiggerF = new(5.5f, 5.5f, 5.5f);
        VectorZF vzSmallerF = new(-1.25f, -10.99f, -0.00001f);

        IVectorTest(vz, vzBigger, vzSmaller, vzBiggert, vzSmallert, vzBiggerF, vzSmallerF);

        C.WriteLine("-Operators-");

        #region vectorz
        C.WriteLine((Vector)vz);
        C.WriteLine((VectorF)vz);
        C.WriteLine((VectorZF)vz);

        C.WriteLine(vz++);
        C.WriteLine(vz);
        C.WriteLine(++vz);
        C.WriteLine(vz--);
        C.WriteLine(vz);
        C.WriteLine(--vz);
        C.WriteLine(-vz);
        #endregion

        #region vectorz_vectorz
        C.WriteLine(vz == vzBigger);
        C.WriteLine(vz == vzSmaller);
        C.WriteLine(vz != vzBigger);
        C.WriteLine(vz != vzSmaller);
        C.WriteLine(vz + vzBigger);
        C.WriteLine(vz + vzSmaller);
        C.WriteLine(vz - vzBigger);
        C.WriteLine(vz - vzSmaller);
        C.WriteLine(vz * vzBigger);
        C.WriteLine(vz * vzSmaller);
        C.WriteLine(vz / vzBigger);
        C.WriteLine(vz / vzSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz / new VectorZ(0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vz % vzBigger);
        C.WriteLine(vz % vzSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz % new VectorZ(0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vz & vzBigger);
        C.WriteLine(vz & vzSmaller);
        C.WriteLine(vz | vzBigger);
        C.WriteLine(vz | vzSmaller);
        C.WriteLine(vz ^ vzBigger);
        C.WriteLine(vz ^ vzSmaller);
        #endregion

        #region vectorz_vectorzf
        C.WriteLine(vz == vzBiggerF);
        C.WriteLine(vz == vzSmallerF);
        C.WriteLine(vz != vzBiggerF);
        C.WriteLine(vz != vzSmallerF);
        #endregion

        #region vectorz_int
        C.WriteLine(vz == vzBiggert);
        C.WriteLine(vz == vzSmallert);
        C.WriteLine(vz == 1);
        C.WriteLine(vz != vzBiggert);
        C.WriteLine(vz != vzSmallert);
        C.WriteLine(vz != 1);
        C.WriteLine(vz + vzBiggert);
        C.WriteLine(vz + vzSmallert);
        C.WriteLine(vz + 1);
        C.WriteLine(vz - vzBiggert);
        C.WriteLine(vz - vzSmallert);
        C.WriteLine(vz - 1);
        C.WriteLine(vz * vzBiggert);
        C.WriteLine(vz * vzSmallert);
        C.WriteLine(vz * 1);
        C.WriteLine(vz / vzBiggert);
        C.WriteLine(vz / vzSmallert);
        C.WriteLine(vz / 1);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz / 0);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vz % vzBiggert);
        C.WriteLine(vz % vzSmallert);
        C.WriteLine(vz % 1);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz % 0);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vz & vzBiggert);
        C.WriteLine(vz & vzSmallert);
        C.WriteLine(vz & 0);
        C.WriteLine(vz | vzBiggert);
        C.WriteLine(vz | vzSmallert);
        C.WriteLine(vz | 0);
        C.WriteLine(vz ^ vzBiggert);
        C.WriteLine(vz ^ vzSmallert);
        C.WriteLine(vz ^ 0);
        #endregion

        #region vectorz_float
        C.WriteLine(vz + 1f);
        C.WriteLine(vz - 1f);
        C.WriteLine(vz * 1f);
        C.WriteLine(vz / 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz / 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vz % 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz % 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        C.WriteLine("-----\n");
        #endregion

        #region VectorZF
        C.WriteLine("---VectorZF---");

        VectorZF vzf = new(2f, 4f, 5f);
        VectorZF vzfBigger = new(10f, 10f, 10f);
        VectorZF vzfSmaller = new(1f, -5f, -1f);
        float vzfBiggert = 5f;
        float vzfSmallert = -10f;
        VectorZF vzfBiggerF = new(5.5f, 5.5f, 5.5f);
        VectorZF vzfSmallerF = new(-1.25f, -10.99f, -0.00001f);

        IVectorTest(vzf, vzfBigger, vzfSmaller, vzfBiggert, vzfSmallert, vzfBiggerF, vzfSmallerF);

        C.WriteLine("-Operators-");

        #region vectorzf
        C.WriteLine((Vector)vzf);
        C.WriteLine((VectorF)vzf);
        C.WriteLine((VectorZ)vzf);

        C.WriteLine(vzf++);
        C.WriteLine(vzf);
        C.WriteLine(++vzf);
        C.WriteLine(vzf--);
        C.WriteLine(vzf);
        C.WriteLine(--vzf);
        C.WriteLine(-vzf);
        #endregion

        #region vectorzf_vectorzf
        C.WriteLine(vzf == vzfBigger);
        C.WriteLine(vzf == vzfSmaller);
        C.WriteLine(vzf != vzfBigger);
        C.WriteLine(vzf != vzfSmaller);
        C.WriteLine(vzf + vzfBigger);
        C.WriteLine(vzf + vzfSmaller);
        C.WriteLine(vzf - vzfBigger);
        C.WriteLine(vzf - vzfSmaller);
        C.WriteLine(vzf * vzfBigger);
        C.WriteLine(vzf * vzfSmaller);
        C.WriteLine(vzf / vzfBigger);
        C.WriteLine(vzf / vzfSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vzf / new VectorZF(0f, 0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vzf % vzfBigger);
        C.WriteLine(vzf % vzfSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vzf % new VectorZF(0f, 0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        #region vectorzf_vectorz
        C.WriteLine(vzf == vz);
        C.WriteLine(vzf != vz);
        C.WriteLine(vzf + vz);
        C.WriteLine(vzf - vz);
        C.WriteLine(vzf * vz);
        C.WriteLine(vzf / vz);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vzf / new VectorZ(0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vzf % vz);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vzf % new VectorZ(0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(vz + vzf);
        C.WriteLine(vz - vzf);
        C.WriteLine(vz * vzf);
        C.WriteLine(vz / vzf);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz / new VectorZF(0f, 0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vz % vzf);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vz % new VectorZF(0f, 0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        #region vectorzf_float
        C.WriteLine(vzf == vzfBiggert);
        C.WriteLine(vzf == vzfSmallert);
        C.WriteLine(vzf == 1f);
        C.WriteLine(vzf != vzfBiggert);
        C.WriteLine(vzf != vzfSmallert);
        C.WriteLine(vzf != 1f);
        C.WriteLine(vzf + vzfBiggert);
        C.WriteLine(vzf + vzfSmallert);
        C.WriteLine(vzf + 1f);
        C.WriteLine(vzf - vzfBiggert);
        C.WriteLine(vzf - vzfSmallert);
        C.WriteLine(vzf - 1f);
        C.WriteLine(vzf * vzfBiggert);
        C.WriteLine(vzf * vzfSmallert);
        C.WriteLine(vzf * 1f);
        C.WriteLine(vzf / vzfBiggert);
        C.WriteLine(vzf / vzfSmallert);
        C.WriteLine(vzf / 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vzf / 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(vzf % vzfBiggert);
        C.WriteLine(vzf % vzfSmallert);
        C.WriteLine(vzf % 1f);
        try
        {
            ///DivideByZero Test
            C.WriteLine(vzf % 0f);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        C.WriteLine("-----\n");
        #endregion

        #region Color
        C.WriteLine("---Color---");

        Color c = new(2, 4, 5);
        Color cBigger = new(10, 10, 10);
        Color cSmaller = new(1, 1, 1);
        byte cBiggert = 5;
        byte cSmallert = 1;
        Color cBiggerF = new(6, 6, 6);
        Color cSmallerF = new(1, 1, 1);

        IVectorTest(c, cBigger, cSmaller, cBiggert, cSmallert, cBiggerF, cSmallerF);

        C.WriteLine("-Color-");

        C.WriteLine($"{c} == {cBigger} => {c.EqualsColor(cBigger)}");
        C.WriteLine($"{c} == {cSmaller} => {c.EqualsColor(cSmaller)}");
        C.WriteLine($"{c} == {cBiggert} => {c.EqualsColor(cBiggert)}");
        C.WriteLine($"{c} == {cSmallert} => {c.EqualsColor(cSmallert)}");

        C.WriteLine(c.IsBlack());
        C.WriteLine(c.IsGrey());

        c.MoveToZero(1);
        C.WriteLine(c);
        c.MoveToZero(cBigger);
        C.WriteLine(c);

        C.WriteLine("-Blend-");
        Color top = new(0, 128, 64, 128), btm = new(200, 100, 50, 128);
        C.WriteLine(top.Blend(btm));
        C.WriteLine(top.Blend(btm, true));
        C.WriteLine(btm.Blend(top));
        C.WriteLine(btm.Blend(top, true));

        C.WriteLine("-Operators-");

        #region color
        C.WriteLine(c++);
        C.WriteLine(c);
        C.WriteLine(++c);
        C.WriteLine(c--);
        C.WriteLine(c);
        C.WriteLine(--c);
        C.WriteLine(-c);
        #endregion

        #region color_color
        C.WriteLine(c == cBigger);
        C.WriteLine(c == cSmaller);
        C.WriteLine(c != cBigger);
        C.WriteLine(c != cSmaller);
        C.WriteLine(c + cBigger);
        C.WriteLine(c + cSmaller);
        C.WriteLine(c - cBigger);
        C.WriteLine(c - cSmaller);
        C.WriteLine(c * cBigger);
        C.WriteLine(c * cSmaller);
        C.WriteLine(c / cBigger);
        C.WriteLine(c / cSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(c / new Color(0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(c % cBigger);
        C.WriteLine(c % cSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(c % new Color(0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        #region color_byte
        C.WriteLine(c + cBiggert);
        C.WriteLine(c + cSmallert);
        C.WriteLine(c + 1);
        C.WriteLine(c - cBiggert);
        C.WriteLine(c - cSmallert);
        C.WriteLine(c - 1);
        C.WriteLine(c * cBiggert);
        C.WriteLine(c * cSmallert);
        C.WriteLine(c * 1);
        C.WriteLine(c / cBiggert);
        C.WriteLine(c / cSmallert);
        C.WriteLine(c / 1);
        try
        {
            ///DivideByZero Test
            C.WriteLine(c / 0);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(c % cBiggert);
        C.WriteLine(c % cSmallert);
        C.WriteLine(c % 1);
        try
        {
            ///DivideByZero Test
            C.WriteLine(c % 0);
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        #endregion

        C.WriteLine("-----\n");
        #endregion

        #region Rectangle
        C.WriteLine("---Rectangle---");

        Rectangle r = new(2, 4, 5, 6);
        Rectangle rBigger = new(10, 10, 10, 10);
        Rectangle rSmaller = new(-1, -5, 1, 1);
        Vector rBiggerV = new(10, 10);
        Vector rSmallerV = new(1, -5);
        int rBiggert = 5;
        int rSmallert = -10;
        RectangleF rBiggerF = new(6.5f, 6.5f, 6.5f, 6.5f);
        RectangleF rSmallerF = new(-1.25f, -10.99f, -0.00001f, -0.5f);

        IShape2DTest(r, rBigger, rSmaller, rBiggerV, rSmallerV, rBiggert, rSmallert, rBiggerF, rSmallerF, rBigger, rBiggerF);

        C.WriteLine("-Operators-");

        C.WriteLine((RectangleF)r);

        C.WriteLine(r == rBigger);
        C.WriteLine(r == rSmaller);
        C.WriteLine(r != rBigger);
        C.WriteLine(r != rSmaller);
        C.WriteLine(r + rBigger);
        C.WriteLine(r + rSmaller);
        C.WriteLine(r - rBigger);
        C.WriteLine(r - rSmaller);
        C.WriteLine(r * rBigger);
        C.WriteLine(r * rSmaller);
        C.WriteLine(r / rBigger);
        C.WriteLine(r / rSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(r / new Rectangle(0, 0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(r % rBigger);
        C.WriteLine(r % rSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(r % new Rectangle(0, 0, 0, 0));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(r & rBigger);
        C.WriteLine(r & rSmaller);
        C.WriteLine(r | rBigger);
        C.WriteLine(r | rSmaller);
        C.WriteLine(r ^ rBigger);
        C.WriteLine(r ^ rSmaller);

        C.WriteLine("-----\n");
        #endregion

        #region RectangleF
        C.WriteLine("---RectangleF---");

        RectangleF rf = new(2f, 4f, 5f, 6f);
        RectangleF rfBigger = new(10f, 10f, 10f, 10f);
        RectangleF rfSmaller = new(-1f, -5f, 1f, 1f);
        VectorF rfBiggerV = new(10f, 10f);
        VectorF rfSmallerV = new(1f, -5f);
        float rfBiggert = 5f;
        float rfSmallert = -10f;
        RectangleF rfBiggerF = new(6.5f, 6.5f, 6.5f, 6.5f);
        RectangleF rfSmallerF = new(-1.25f, -10.99f, -0.00001f, -0.5f);

        IShape2DTest(rf, rfBigger, rfSmaller, rfBiggerV, rfSmallerV, rfBiggert, rfSmallert, rfBiggerF, rfSmallerF, rBigger, rfBiggerF);

        C.WriteLine("-Operators-");

        C.WriteLine((Rectangle)rf);

        C.WriteLine(rf == rfBigger);
        C.WriteLine(rf == rfSmaller);
        C.WriteLine(rf != rfBigger);
        C.WriteLine(rf != rfSmaller);
        C.WriteLine(rf + rfBigger);
        C.WriteLine(rf + rfSmaller);
        C.WriteLine(rf - rfBigger);
        C.WriteLine(rf - rfSmaller);
        C.WriteLine(rf * rfBigger);
        C.WriteLine(rf * rfSmaller);
        C.WriteLine(rf / rfBigger);
        C.WriteLine(rf / rfSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(rf / new RectangleF(0f, 0f, 0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(rf % rfBigger);
        C.WriteLine(rf % rfSmaller);
        try
        {
            ///DivideByZero Test
            C.WriteLine(rf % new RectangleF(0f, 0f, 0f, 0f));
        }
        catch (DivideByZeroException e)
        {
            C.WriteLine(e.Message);
        }
        C.WriteLine(rf & rfBigger);
        C.WriteLine(rf & rfSmaller);
        C.WriteLine(rf | rfBigger);
        C.WriteLine(rf | rfSmaller);
        C.WriteLine(rf ^ rfBigger);
        C.WriteLine(rf ^ rfSmaller);

        C.WriteLine("-----\n");
        #endregion

        #region Triangle
        C.WriteLine("---Triangle---");

        Triangle t = new(2, 4, 2, 4, 2, 4);
        Triangle tBigger = new(10, 10, 10, 10, 10, 10);
        Triangle tSmaller = new(1, -5, 1, -5, 1, -5);
        Vector tBiggerV = new(10, 10);
        Vector tSmallerV = new(1, -5);
        int tBiggert = 5;
        int tSmallert = -10;
        TriangleF tBiggerF = new(5.5f, 5.5f, 5.5f, 5.5f, 5.5f, 5.5f);
        TriangleF tSmallerF = new(-1.25f, -10.99f, -1.25f, -10.99f, -1.25f, -10.99f);

        IShape2DTest(t, tBigger, tSmaller, tBiggerV, tSmallerV, tBiggert, tSmallert, tBiggerF, tSmallerF, tBigger, tBiggerF);

        C.WriteLine("-Operators-");

        C.WriteLine((TriangleF)t);

        C.WriteLine(t == tBigger);
        C.WriteLine(t == tSmaller);
        C.WriteLine(t != tBigger);
        C.WriteLine(t != tSmaller);

        C.WriteLine("-----\n");
        #endregion

        #region TriangleF
        C.WriteLine("---TriangleF---");

        TriangleF tf = new(2f, 4f, 2f, 4f, 2f, 4f);
        TriangleF tfBigger = new(10f, 10f, 10f, 10f, 10f, 10f);
        TriangleF tfSmaller = new(1f, -5f, 1f, -5f, 1f, -5f);
        VectorF tfBiggerV = new(10f, 10f);
        VectorF tfSmallerV = new(1f, -5f);
        float tfBiggert = 5f;
        float tfSmallert = -10f;
        TriangleF tfBiggerF = new(5.5f, 5.5f, 5.5f, 5.5f, 5.5f, 5.5f);
        TriangleF tfSmallerF = new(-1.25f, -10.99f, -1.25f, -10.99f, -1.25f, -10.99f);

        IShape2DTest(tf, tfBigger, tfSmaller, tfBiggerV, tfSmallerV, tfBiggert, tfSmallert, tfBiggerF, tfSmallerF, tBigger, tfBiggerF);

        C.WriteLine("-Operators-");

        C.WriteLine((Triangle)tf);

        C.WriteLine(tf == tfBigger);
        C.WriteLine(tf == tfSmaller);
        C.WriteLine(tf != tfBigger);
        C.WriteLine(tf != tfSmaller);

        C.WriteLine("-----\n");
        #endregion
        #endregion
    }

    #region Interface Types
    private static void IMathTest<T, F>(IMath<T, F> iMath, T bigger, T smaller, F biggerF, F smallerF)
    {
        C.WriteLine("-IMath-");

        C.WriteLine(iMath.GetType());
        C.WriteLine(typeof(IMath<T, F>));
        C.WriteLine(typeof(T));
        C.WriteLine(typeof(F));

        C.WriteLine(iMath.ToString());

        C.WriteLine(iMath.ToHexString());
        C.WriteLine(iMath.ToHexString(false, true));
        C.WriteLine(iMath.ToHexString(true, false));
        C.WriteLine(iMath.ToHexString(true, true));

        C.WriteLine(iMath.ToBinString());
        C.WriteLine(iMath.ToBinString(false, '^', '&', true));
        C.WriteLine(iMath.ToBinString(true, '#', '%', false));
        C.WriteLine(iMath.ToBinString(true, '<', '>', true));

        C.WriteLine(iMath.GetHashCode());

        C.WriteLine($"{iMath} == {bigger} => {iMath.Equals(bigger)}");
        C.WriteLine($"{iMath} == {smaller} => {iMath.Equals(smaller)}");
        C.WriteLine($"{iMath} == {biggerF} => {iMath.Equals(biggerF)}");
        C.WriteLine($"{iMath} == {smallerF} => {iMath.Equals(smallerF)}");
        C.WriteLine($"{iMath} == {true} => {iMath.Equals(true)}");

        C.WriteLine(iMath.Abs());

        C.WriteLine(iMath.Between(smaller, bigger));
        C.WriteLine(iMath.BetweenAgainstBounds(smaller, bigger));
        C.WriteLine(iMath.BetweenAgainstMax(smaller, bigger));
        C.WriteLine(iMath.BetweenAgainstMin(smaller, bigger));

        C.WriteLine(iMath.Clamp(smaller, bigger));

        C.WriteLine(iMath.Closest(bigger, smaller));
        C.WriteLine(iMath.Closest(bigger, smaller, bigger));

        C.WriteLine(iMath.Cube());

        C.WriteLine(iMath.Farthest(bigger, smaller));
        C.WriteLine(iMath.Farthest(bigger, smaller, bigger));

        C.WriteLine(iMath.IsZero());

        C.WriteLine(iMath.Max(bigger));
        C.WriteLine(iMath.Max(bigger, smaller, bigger));

        C.WriteLine(iMath.Min(bigger));
        C.WriteLine(iMath.Min(bigger, smaller, bigger));

        C.WriteLine(iMath.Pow(2));
        C.WriteLine(iMath.Pow(3));
        C.WriteLine(iMath.Pow(10));

        C.WriteLine($"{iMath.FindSign()} : {iMath.FindSignInt()}");

        C.WriteLine($"{iMath.FindSign(bigger)} : {iMath.FindSignInt(bigger)}");
        C.WriteLine($"{iMath.FindSign(smaller)} : {iMath.FindSignInt(smaller)}");

        C.WriteLine(iMath.Sqr());

        C.WriteLine(iMath.Wrap(smaller, bigger));

        iMath.MoveToZero(bigger);
        C.WriteLine(iMath);
    }

    private static void IVectorTest<V, t, F>(IVector<V, t, F> iVector, V bigger, V smaller, t biggert, t smallert, F biggerF, F smallerF)
    {
        IMathTest(iVector, bigger, smaller, biggerF, smallerF);

        C.WriteLine("-IVector-");

        C.WriteLine(iVector.GetType());
        C.WriteLine(typeof(IVector<V, t, F>));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));

        C.WriteLine(iVector.Equals(biggert));
        C.WriteLine(iVector.Equals(smallert));

        //Angle

        C.WriteLine(iVector.Between(smallert, biggert));
        C.WriteLine(iVector.BetweenAgainstBounds(smallert, biggert));
        C.WriteLine(iVector.BetweenAgainstMax(smallert, biggert));
        C.WriteLine(iVector.BetweenAgainstMin(smallert, biggert));

        C.WriteLine(iVector.Clamp(smallert, biggert));

        //no float sqrroot yet
        /*try
        {
            C.WriteLine($"{iVector.Distance(bigger)}^2 = {iVector.DistanceSquared(bigger)}");
            C.WriteLine($"{iVector.Distance(smaller)}^2 = {iVector.DistanceSquared(smaller)}");
            C.WriteLine($"{iVector.DistanceFromZero()}^2 = {iVector.DistanceFromZeroSquared()}");
        }
        catch (NotImplementedException e)
        {
            C.WriteLine(e.Message);
            C.WriteLine(iVector.DistanceSquared(bigger));
            C.WriteLine(iVector.DistanceSquared(smaller));
            C.WriteLine(iVector.DistanceFromZeroSquared());
        }*/
        C.WriteLine($"{iVector.Distance(bigger)}^2 = {iVector.DistanceSquared(bigger)}");
        C.WriteLine($"{iVector.Distance(smaller)}^2 = {iVector.DistanceSquared(smaller)}");
        C.WriteLine($"{iVector.DistanceFromZero()}^2 = {iVector.DistanceFromZeroSquared()}");

        C.WriteLine(iVector.Pow(bigger));
        C.WriteLine(iVector.Pow(smaller));

        //Rotate

        C.WriteLine(iVector.Wrap(smallert, biggert));

        iVector.MoveToZero(biggert);
        C.WriteLine(iVector);
    }

    private static void IDirectionTest<T>(IDirection<T> iDirection, T bigger, T smaller)
    {
        C.WriteLine("-IDirection-");

        C.WriteLine(iDirection.GetType());
        C.WriteLine(typeof(IDirection<T>));
        C.WriteLine(typeof(T));

        C.WriteLine(iDirection.DirectionFromZero());
        C.WriteLine(iDirection.DirectionToPoint(bigger));
        C.WriteLine(iDirection.DirectionToPoint(smaller));
    }

    private static void IRotate2DTest<S, V, F, Vf>(IRotate2D<S, V, F, Vf> iRotate2D, V bigger, V smaller, Vf biggerF, Vf smallerF, float rotation1, float rotation2)
    {
        C.WriteLine("-IRotate2D-");

        C.WriteLine(iRotate2D.GetType());
        C.WriteLine(typeof(IRotate2D<S, V, F, Vf>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(F));
        C.WriteLine(typeof(Vf));

        C.WriteLine(iRotate2D.AngleFromRotationOrigin());

        C.WriteLine(iRotate2D.Angle(bigger));
        C.WriteLine(iRotate2D.Angle(smaller));

        C.WriteLine(iRotate2D.Angle(biggerF));
        C.WriteLine(iRotate2D.Angle(smallerF));

        C.WriteLine(iRotate2D.Angle(bigger, smaller));
        C.WriteLine(iRotate2D.Angle(smaller, bigger));

        C.WriteLine(iRotate2D.Angle(biggerF, smallerF));
        C.WriteLine(iRotate2D.Angle(smallerF, biggerF));

        C.WriteLine(iRotate2D.Rotate(rotation1));
        //C.WriteLine(iRotate2D.Rotate(rotation1, bigger));
        C.WriteLine(iRotate2D.Rotate(rotation1, biggerF));

        C.WriteLine(iRotate2D.RotateByRadians(rotation1));
        //C.WriteLine(iRotate2D.RotateByRadians(rotation1, bigger));
        C.WriteLine(iRotate2D.RotateByRadians(rotation1, biggerF));

        C.WriteLine(iRotate2D.RotateByDegrees(rotation1));
        //C.WriteLine(iRotate2D.RotateByDegrees(rotation1, bigger));
        C.WriteLine(iRotate2D.RotateByDegrees(rotation1, biggerF));

        C.WriteLine(iRotate2D.RotateByRightAngles(0));
        C.WriteLine(iRotate2D.RotateByRightAngles(1));
        C.WriteLine(iRotate2D.RotateByRightAngles(2));
        C.WriteLine(iRotate2D.RotateByRightAngles(3));
        C.WriteLine(iRotate2D.RotateByRightAngles(4));

        C.WriteLine(iRotate2D.RotateByRightAngles(bigger, 1));
        C.WriteLine(iRotate2D.RotateByRightAngles(biggerF, 2));
    }

    private static void IPolytopeTest<V, P>(IPolytope<V, P> iPolytope)
    {
        C.WriteLine("-IPolytope-");

        C.WriteLine(iPolytope.GetType());
        C.WriteLine(typeof(IPolytope<V, P>));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(P));

        C.WriteLine(iPolytope.CenterPoint);
        C.WriteLine(string.Join(',', iPolytope.Points));
        C.WriteLine(string.Join(',', iPolytope.GetPrimitives()));
    }

    private static void IShapeTest<S, V, t, F>(IShape<S, V, t, F> iShape, S bigger, S smaller, V biggerV, V smallerV, t biggert, t smallert, F biggerF, F smallerF)
    {
        IVectorTest(iShape, bigger, smaller, biggerV, smallerV, biggerF, smallerF);

        C.WriteLine("-IShape-");

        C.WriteLine(iShape.GetType());
        C.WriteLine(typeof(IShape<S, V, t, F>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));

        C.WriteLine(iShape.Between(smallert, biggert));
        C.WriteLine(iShape.BetweenAgainstBounds(smallert, biggert));
        C.WriteLine(iShape.BetweenAgainstMax(smallert, biggert));
        C.WriteLine(iShape.BetweenAgainstMin(smallert, biggert));

        C.WriteLine(iShape.Clamp(smallert, biggert));

        C.WriteLine(iShape.Closest(biggerV, smallerV));
        C.WriteLine(iShape.Closest(biggerV, smallerV));

        C.WriteLine($"{iShape.Distance(biggerV)} = {iShape.DistanceSquared(biggerV)}");
        C.WriteLine($"{iShape.Distance(smallerV)} = {iShape.DistanceSquared(smallerV)}");

        C.WriteLine(iShape.Enveloping(biggerV));
        C.WriteLine(iShape.Enveloping(smallerV));
        C.WriteLine(iShape.Enveloping(bigger));
        C.WriteLine(iShape.Enveloping(smaller));

        C.WriteLine(iShape.Farthest(biggerV, smallerV));
        C.WriteLine(iShape.Farthest(biggerV, smallerV));

        try
        {
            C.WriteLine(iShape.IntersectionArea(bigger));
            C.WriteLine(iShape.IntersectionArea(smaller));

            C.WriteLine(iShape.Intersecting(bigger));
            C.WriteLine(iShape.Intersecting(smaller));

            C.WriteLine(iShape.Touching(bigger));
            C.WriteLine(iShape.Touching(smaller));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(iShape.Overlapping(biggerV));
        C.WriteLine(iShape.Overlapping(smallerV));
        C.WriteLine(iShape.Overlapping(bigger));
        C.WriteLine(iShape.Overlapping(smaller));

        C.WriteLine(iShape.Wrap(smallert, biggert));

        iShape.MoveToZero(biggert);
        C.WriteLine(iShape);
    }

    private static void IShape2DTest<S, V, t, F>(IShape2D<S, V, t, F> iShape2D, S bigger, S smaller, V biggerV, V smallerV, t biggert, t smallert, F biggerF, F smallerF, IPolytope<Vector, Triangle> polytope, IPolytope<VectorF, TriangleF> polytopeF)
    {
        IShapeTest(iShape2D, bigger, smaller, biggerV, smallerV, biggert, smallert, biggerF, smallerF);
        IDirectionTest(iShape2D, biggerV, smallerV);

        C.WriteLine("-IShape2D-");

        C.WriteLine(iShape2D.GetType());
        C.WriteLine(typeof(IShape2D<S, V, t, F>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));
        C.WriteLine(typeof(IPolytope<Vector, Triangle>));
        C.WriteLine(typeof(IPolytope<VectorF, TriangleF>));

        C.WriteLine(iShape2D.DirectionToShape(bigger));
        C.WriteLine(iShape2D.DirectionToShape(smaller));

        C.WriteLine(iShape2D.Enveloping(polytope));
        C.WriteLine(iShape2D.Enveloping(polytopeF));

        C.WriteLine(iShape2D.Enveloping(biggerV));
        C.WriteLine(iShape2D.Enveloping(smallerV));
        //float

        C.WriteLine(iShape2D.Equals(polytope));
        C.WriteLine(iShape2D.Equals(polytopeF));

        C.WriteLine(iShape2D.IntersectionArea(polytope));
        C.WriteLine(iShape2D.IntersectionArea(polytopeF));

        C.WriteLine(iShape2D.Intersecting(polytope));
        C.WriteLine(iShape2D.Intersecting(polytopeF));

        C.WriteLine(iShape2D.Overlapping(biggerV));
        C.WriteLine(iShape2D.Overlapping(smallerV));
        //float

        C.WriteLine(iShape2D.Overlapping(polytope));
        C.WriteLine(iShape2D.Overlapping(polytopeF));

        C.WriteLine(iShape2D.Touching(polytope));
        C.WriteLine(iShape2D.Touching(polytopeF));
    }

    private static void IShape3DTest<S, V, t, F>(IShape3D<S, V, t, F> iShape3D, S bigger, S smaller, V biggerV, V smallerV, t biggert, t smallert, F biggerF, F smallerF, IPolytope<VectorZ, Tetrahedron> polytope, IPolytope<VectorZF, TetrahedronF> polytopeF)
    {
        IShapeTest(iShape3D, bigger, smaller, biggerV, smallerV, biggert, smallert, biggerF, smallerF);

        C.WriteLine("-IShape3D-");

        C.WriteLine(iShape3D.GetType());
        C.WriteLine(typeof(IShape2D<S, V, t, F>));
        C.WriteLine(typeof(S));
        C.WriteLine(typeof(V));
        C.WriteLine(typeof(t));
        C.WriteLine(typeof(F));
        C.WriteLine(typeof(IPolytope<Vector, Triangle>));
        C.WriteLine(typeof(IPolytope<VectorF, TriangleF>));

        C.WriteLine(iShape3D.Enveloping(polytope));
        C.WriteLine(iShape3D.Enveloping(polytopeF));

        C.WriteLine(iShape3D.Enveloping(biggerV));
        C.WriteLine(iShape3D.Enveloping(smallerV));
        //float

        C.WriteLine(iShape3D.Equals(polytope));
        C.WriteLine(iShape3D.Equals(polytopeF));

        C.WriteLine(iShape3D.IntersectionArea(polytope));
        C.WriteLine(iShape3D.IntersectionArea(polytopeF));

        C.WriteLine(iShape3D.Intersecting(polytope));
        C.WriteLine(iShape3D.Intersecting(polytopeF));

        C.WriteLine(iShape3D.Overlapping(biggerV));
        C.WriteLine(iShape3D.Overlapping(smallerV));
        //float

        C.WriteLine(iShape3D.Overlapping(polytope));
        C.WriteLine(iShape3D.Overlapping(polytopeF));

        C.WriteLine(iShape3D.Touching(polytope));
        C.WriteLine(iShape3D.Touching(polytopeF));
    }
    #endregion

    //FloatStepInterpolateTest()

    internal static void VectorStepInterpolateTest()
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
    #endregion

    #region Collections Test
    internal static void CollectionsTest()
    {
        #region Substrings
        C.WriteLine("---Substrings---");

        C.WriteLine(string.Join(',', Strings.GetAllSubstrings("Test_Test_N/A_Test_Test", "Test")));//incorrect
        C.WriteLine(Strings.GetSubstring("Test_Test_N/A_Test_Test", 5, 10));
        C.WriteLine(Strings.GetSubstringUntilChar("Test_Test_N/A_Test_Test", 0, 'A'));
        C.WriteLine(Strings.GetSubstringUntilChar("Test_Test_N/A_Test_Test", 0, "N/A"));
        C.WriteLine(Strings.GetSubstringUntilString("Test_Test_N/A_Test_Test", 0, @"N\A"));
        C.WriteLine(Strings.TryGetFirstSubstring(out int i, "Test_Test_N/A_Test_Test", "_Test"));
        C.WriteLine(i);
        C.WriteLine(Strings.TryGetLastSubstring(out i, "Test_Test_N/A_Test_Test", "_Test"));//incorrect
        C.WriteLine(i);

        C.WriteLine("-----\n");
        #endregion

        #region Hex
        C.WriteLine("---Hex---");

        C.WriteLine(Strings.ToHexString((byte)0x01, false, false));
        C.WriteLine(Strings.ToHexString((byte)0x01, true, false));
        C.WriteLine(Strings.ToHexString((byte)0x01, false, true));
        C.WriteLine(Strings.ToHexString((byte)0x01, true, true));

        C.WriteLine(Strings.ToHexString((short)0x0123, false, false));
        C.WriteLine(Strings.ToHexString((short)0x0123, true, false));
        C.WriteLine(Strings.ToHexString((short)0x0123, false, true));
        C.WriteLine(Strings.ToHexString((short)0x0123, true, true));

        C.WriteLine(Strings.ToHexString(0x01234567, false, false));
        C.WriteLine(Strings.ToHexString(0x01234567, true, false));
        C.WriteLine(Strings.ToHexString(0x01234567, false, true));
        C.WriteLine(Strings.ToHexString(0x01234567, true, true));

        C.WriteLine(Strings.ToHexString(0x0123456789ABCDEF, false, false));
        C.WriteLine(Strings.ToHexString(0x0123456789ABCDEF, true, false));
        C.WriteLine(Strings.ToHexString(0x0123456789ABCDEF, false, true));
        C.WriteLine(Strings.ToHexString(0x0123456789ABCDEF, true, true));

        unsafe
        {
            int f = 0x01234567;
            C.WriteLine(Strings.ToHexString(*(float*)&f, false, false));
            C.WriteLine(Strings.ToHexString(*(float*)&f, true, false));
            C.WriteLine(Strings.ToHexString(*(float*)&f, false, true));
            C.WriteLine(Strings.ToHexString(*(float*)&f, true, true));

            long d = 0x0123456789ABCDEF;
            C.WriteLine(Strings.ToHexString(*(double*)&d, false, false));
            C.WriteLine(Strings.ToHexString(*(double*)&d, true, false));
            C.WriteLine(Strings.ToHexString(*(double*)&d, false, true));
            C.WriteLine(Strings.ToHexString(*(double*)&d, true, true));
        }

        C.WriteLine("-----\n");
        #endregion

        #region Bin
        C.WriteLine("---Bin---");

        C.WriteLine(Strings.ToBinString((byte)0b0101_0101,
            false, false, '_'));
        C.WriteLine(Strings.ToBinString((byte)0b0101_0101,
            true, false, '_'));
        C.WriteLine(Strings.ToBinString((byte)0b0101_0101,
            false, true, '_'));
        C.WriteLine(Strings.ToBinString((byte)0b0101_0101,
            true, true, '_'));

        C.WriteLine(Strings.ToBinString((short)0b0101_0101__0101_0101,
            false, false, '|', '_'));
        C.WriteLine(Strings.ToBinString((short)0b0101_0101__0101_0101,
            true, false, '|', '_'));
        C.WriteLine(Strings.ToBinString((short)0b0101_0101__0101_0101,
            false, true, '|', '_'));
        C.WriteLine(Strings.ToBinString((short)0b0101_0101__0101_0101,
            true, true, '|', '_'));

        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101,
            false, false, '|', '_'));
        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101,
            true, false, '|', '_'));
        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101,
            false, true, '|', '_'));
        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101,
            true, true, '|', '_'));

        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101,
            false, false, '|', '_'));
        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101,
            true, false, '|', '_'));
        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101,
            false, true, '|', '_'));
        C.WriteLine(Strings.ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101,
            true, true, '|', '_'));

        unsafe
        {
            int f = 0b0101_0101__0101_0101____0101_0101__0101_0101;
            C.WriteLine(Strings.ToBinString(*(float*)&f, false, false, '|', '_'));
            C.WriteLine(Strings.ToBinString(*(float*)&f, true, false, '|', '_'));
            C.WriteLine(Strings.ToBinString(*(float*)&f, false, true, '|', '_'));
            C.WriteLine(Strings.ToBinString(*(float*)&f, true, true, '|', '_'));

            long d = 0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101;
            C.WriteLine(Strings.ToBinString(*(double*)&d, false, false, '|', '_'));
            C.WriteLine(Strings.ToBinString(*(double*)&d, true, false, '|', '_'));
            C.WriteLine(Strings.ToBinString(*(double*)&d, false, true, '|', '_'));
            C.WriteLine(Strings.ToBinString(*(double*)&d, true, true, '|', '_'));
        }

        C.WriteLine("-----\n");
        #endregion

        #region AutoSizedArray
        C.WriteLine("---AutoSizedArray---");

        AutoSizedArray<char> array = new AutoSizedArray<char>(new char[]
        {
                'a', 'b', 'c', 'd', 'e'
        });

        ICopyTest(array, out AutoSizedArray<char> arrayCopy);
        ICollectionTest(array, arrayCopy, 'x', 'y', 'z');

        try
        {
            new AutoSizedArray<int>(-1);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            new AutoSizedArray<int>(new int[1] { 0 }, -1);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            C.WriteLine(array[array.Length]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array[-1]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            array[array.Length] = ']';
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[-1] = ']';
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            C.WriteLine(array[1, 0]);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array[array.Length, array.Length + 1]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array[-1, 0]);
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            array[1, 0] = null;
            C.WriteLine(']');
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[1, 0] = new char[0];
            C.WriteLine(']');
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[array.Length, array.Length + 1] = new char[0];
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array[-1, 0] = new char[0];
            C.WriteLine(']');
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            ICollection<char> c = null;
            array.CopyTo(c);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            array.CopyTo(null);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            C.WriteLine(array.FromLastIndex(array.Length + 1));
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(array.FromLastIndex(-1));
        }
        catch (IndexOutOfRangeException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            array.RemoveRange(1, 0);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        AutoSizedArray<int> array2 = new AutoSizedArray<int>(new int[]
        {
                0, 1, 2, 3, 4
        });

        ICopyTest(array2, out AutoSizedArray<int> array2Copy);
        ICollectionTest(array2, array2Copy, 7, 8, 9);

        C.WriteLine("-----\n");
        #endregion

        #region HashLookupTable
        C.WriteLine("---HashLookupTable---");

        HashLookupTable<char, Vector> hashTable = new HashLookupTable<char, Vector>();
        hashTable.Add('a', new Vector(1, 1));
        hashTable.Add('b', new Vector(2, 2));
        hashTable.Add('c', new Vector(3, 3));
        hashTable.Add('d', new Vector(4, 4));
        hashTable.Add('e', new Vector(5, 5));
        hashTable.Add((char)0, new Vector(-1, -1));

        ICopyTest(hashTable, out HashLookupTable<char, Vector> hashTableCopy);
        IHashTableTest(hashTable, hashTableCopy, 'a', 'b', 'c', 'x', 'y', 'z', new Vector(-69, 69));

        try
        {
            C.WriteLine(hashTable['1']);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            hashTable['1'] = default;
            C.WriteLine(']');
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            hashTable.Add('a', default);
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            IHashTable<char, Vector> t = null;
            hashTable.CopyTo(t);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            hashTable.CopyTo(null);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion
    }

    #region Interface Types
    private static void ICopyTest<T>(ICopy<T> iCopy, out T copy)
    {
        C.WriteLine("-ICopy-");

        copy = iCopy.Copy();

        C.WriteLine(iCopy.GetType());
        C.WriteLine(typeof(ICopy<T>));
        C.WriteLine(copy.GetType());
        C.WriteLine(typeof(T));

        C.WriteLine(iCopy.ToString());
        C.WriteLine(copy.ToString());
    }

    private static void ICollectionTest<T>(ICollection<T> iCollection, ICollection<T> copy, T value1, T value2, T value3)
    {
        C.WriteLine("-ICollection-");

        C.WriteLine(iCollection.GetType());
        C.WriteLine(typeof(ICollection<T>));
        C.WriteLine(typeof(T));

        C.WriteLine(iCollection.ToString());

        C.WriteLine(iCollection.Length);

        C.WriteLine(iCollection[0]);
        T item = iCollection[0];
        iCollection[0] = value1;
        C.WriteLine(iCollection[0]);
        iCollection[0] = item;
        C.WriteLine(iCollection[0]);

        C.WriteLine(string.Join(", ", iCollection[0, iCollection.Length]));

        T[] items = iCollection[0, iCollection.Length];
        iCollection[0, iCollection.Length] = new T[3] { value1, value2, value3 };//incorrect
        C.WriteLine(string.Join(", ", iCollection[0, iCollection.Length]));
        C.WriteLine(iCollection.Length);

        iCollection[0, iCollection.Length] = items;
        C.WriteLine(string.Join(", ", iCollection[0, iCollection.Length]));

        C.WriteLine(string.Join(", ", iCollection[0, 2]));

        C.WriteLine(iCollection.Concat());

        C.WriteLine(iCollection.Contains(value1));

        C.WriteLine(iCollection.FromLastIndex(iCollection.Length));
        C.WriteLine(iCollection.FromLastIndex(1));

        C.WriteLine(string.Join(", ", iCollection.GetMultiple(3, 1, 4)));

        C.WriteLine(iCollection.IndexOf(iCollection[0]));
        C.WriteLine(iCollection.IndexOf(iCollection[1]));
        C.WriteLine(iCollection.IndexOf(value1));

        C.WriteLine(iCollection.Join(','));

        C.WriteLine(iCollection.LastIndexOf(iCollection[0]));
        C.WriteLine(iCollection.LastIndexOf(iCollection[1]));
        C.WriteLine(iCollection.LastIndexOf(value1));

        C.WriteLine(string.Join(", ", iCollection.ToArray()));

        iCollection.Add(value1);
        C.WriteLine(iCollection.Contains(value1));
        C.WriteLine(iCollection.Join(", "));

        iCollection.Remove(value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Add(value1, value2, value3);
        C.WriteLine(iCollection.Join(", "));

        iCollection.RemoveAt(iCollection.Length - 1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.RemoveRange(iCollection.Length - 2, iCollection.Length - 1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Insert(2, value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Remove(value1);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Insert(3, value1, value2, value3);//incorrect
        C.WriteLine(iCollection.Join(", "));

        iCollection.Move(2, 0);
        C.WriteLine(iCollection.Join(", "));

        iCollection.Clear();
        C.WriteLine(iCollection.Join(", "));

        copy.CopyTo(iCollection);
        C.WriteLine(iCollection.Join(", "));
    }

    private static void IHashTableTest<H, T>(IHashTable<H, T> iHashTable, IHashTable<H, T> copy, H code1, H code2, H code3, H newCode1, H newCode2, H newCode3, T value)
    {
        C.WriteLine("-IHashTable-");

        C.WriteLine(iHashTable.GetType());
        C.WriteLine(typeof(IHashTable<H, T>));
        C.WriteLine(typeof(H));
        C.WriteLine(typeof(T));

        C.WriteLine(iHashTable.ToString());

        C.WriteLine(iHashTable.Length);

        C.WriteLine(iHashTable[code1]);
        T item = iHashTable[code1];
        iHashTable[code1] = value;
        C.WriteLine(iHashTable[code1]);
        iHashTable[code1] = item;
        C.WriteLine(iHashTable[code1]);

        C.WriteLine(iHashTable.Concat());
        C.WriteLine(iHashTable.ConcatCodes());
        C.WriteLine(iHashTable.ConcatValues());

        C.WriteLine(iHashTable.Contains(value));
        C.WriteLine(iHashTable.ContainsCode(code1));

        C.WriteLine(string.Join(", ", iHashTable.GetCodes()));
        C.WriteLine(string.Join(", ", iHashTable.GetValues()));

        C.WriteLine(string.Join(", ", iHashTable.GetMultiple(code1, code2, code3)));

        C.WriteLine(iHashTable.IndexOf(value));
        C.WriteLine(iHashTable.IndexOf(iHashTable[code1]));
        C.WriteLine(iHashTable.IndexOf(iHashTable[code2]));
        C.WriteLine(iHashTable.IndexOf(iHashTable[code3]));

        C.WriteLine(iHashTable.Join(','));
        C.WriteLine(iHashTable.JoinCodes(','));
        C.WriteLine(iHashTable.JoinCodes(", "));
        C.WriteLine(iHashTable.JoinValues(','));
        C.WriteLine(iHashTable.JoinValues(", "));

        C.WriteLine(iHashTable.LastIndexOf(value));
        C.WriteLine(iHashTable.LastIndexOf(iHashTable[code1]));
        C.WriteLine(iHashTable.LastIndexOf(iHashTable[code2]));
        C.WriteLine(iHashTable.LastIndexOf(iHashTable[code3]));

        iHashTable.Add(newCode1, value);
        C.WriteLine(iHashTable.Contains(value));
        C.WriteLine(iHashTable.ContainsCode(newCode1));
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Remove(value);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Add(newCode1, value);
        iHashTable.Add(newCode2, value);
        iHashTable.Add(newCode3, value);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.RemoveAt(iHashTable.Length - 1);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Remove(newCode1);
        iHashTable.Remove(newCode2);
        C.WriteLine(iHashTable.Join(", "));

        iHashTable.Clear();
        C.WriteLine(iHashTable.Join(", "));

        copy.CopyTo(iHashTable);
        C.WriteLine(iHashTable.Join(", "));
    }

    private static void IColorGroupTest(IColorGroup iColorGroup)
    {
        C.WriteLine("-IColorGroup-");

        C.WriteLine(iColorGroup.GetType());

        C.WriteLine(iColorGroup.ToString());

        iColorGroup.HueShift(50f);
        iColorGroup.SaturationMultiply(0.5f);
        iColorGroup.VelocityMultiply(0.5f);
    }
    #endregion
    #endregion

    #region Utility Test
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
    #endregion

    #region Game Engine Collections
    public static void GameEngineCollections()
    {
        C.WriteLine("---Array---");

        Debug.TestArray.Add(1, 2, 3, 4, 5);
        ICollectionTest(Debug.TestArray, new AutoSizedArray<int>(Debug.TestArray.ToArray()), 69, 420, 324);

        C.WriteLine("-----\n");

        C.WriteLine("---HashArray---");

        Debug.TestTable.Add(new("TestVal", 1), 1);
        Debug.TestTable.Add(new("TestVal", 2), 2);
        Debug.TestTable.Add(new("TestVal", 3), 3);
        Debug.TestTable.Add(new("TestVal", 4), 4);
        Debug.TestTable.Add(new("TestVal", 5), 5);

        HashLookupTable<NameID, int> tableCopy = new HashLookupTable<NameID, int>();
        foreach (int i in Debug.TestTable.GetValues())
            tableCopy.Add(new("TestVal", (byte)i), i);

        IHashTableTest(Debug.TestTable, tableCopy, new("TestVal", 1), new("TestVal", 2), new("TestVal", 3), new("TestVal", 6), new("TestVal", 7), new("TestVal", 8), 324);

        C.WriteLine("-----\n");
    }
    #endregion
}
