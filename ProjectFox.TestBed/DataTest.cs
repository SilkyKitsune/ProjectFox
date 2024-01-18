using System;
using C = System.Console;

using ProjectFox.CoreEngine.Data;

using static ProjectFox.CoreEngine.Collections.Strings;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public unsafe static void DataTest()
    {
        int i1 = 0x11223344, i2 = 0x55667788;
        float f1 = *(float*)&i1, f2 = *(float*)&i2;

        long l1 = 0x1122334455667788L, l2 = unchecked((long)0x99AABBCCEEDDFF00L);
        double d1 = *(double*)&l1, d2 = *(double*)&l2;

        #region Get
        C.WriteLine("---Get---");

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes((short)0x1122, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes((short)0x1122, true)));

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(0x11223344, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(0x11223344, true)));

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(0x1122334455667788L, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(0x1122334455667788L, true)));

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(f1, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(f1, true)));

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(d1, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(d1, true)));

        C.WriteLine("-----\n");
        #endregion

        #region GetMultiple
        C.WriteLine("---GetMultiple---");

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new short[] { 0x1122, 0x3344, 0x5566, 0x7788 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new short[] { 0x1122, 0x3344, 0x5566, 0x7788 }, true)));
        try
        {
            short[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new int[] { 0x11223344, 0x55667788 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new int[] { 0x11223344, 0x55667788 }, true)));
        try
        {
            int[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new long[] { 0x1122334455667788, unchecked((long)0x99AABBCCEEDDFF00L) }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new long[] { 0x1122334455667788, unchecked((long)0x99AABBCCEEDDFF00L) }, true)));
        try
        {
            long[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new float[] { f1, f2 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new float[] { f1, f2 }, true)));
        try
        {
            float[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new double[] { d1, d2 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(new double[] { d1, d2 }, true)));
        try
        {
            double[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", Data.GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region To
        C.WriteLine("---To---");

        C.WriteLine(ToHexString(Data.ToInt16(new byte[] { 0x11, 0x22 }, false)));
        C.WriteLine(ToHexString(Data.ToInt16(new byte[] { 0x11, 0x22 }, true)));
        try
        {
            C.WriteLine(ToHexString(Data.ToInt16(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(Data.ToInt16(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(Data.ToInt32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, false)));
        C.WriteLine(ToHexString(Data.ToInt32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, true)));
        try
        {
            C.WriteLine(ToHexString(Data.ToInt32(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(Data.ToInt32(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(Data.ToInt64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));//does not work
        C.WriteLine(ToHexString(Data.ToInt64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        try
        {
            C.WriteLine(ToHexString(Data.ToInt64(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(Data.ToInt64(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(Data.ToFloat32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, false)));
        C.WriteLine(ToHexString(Data.ToFloat32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, true)));
        try
        {
            C.WriteLine(ToHexString(Data.ToFloat32(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(Data.ToFloat32(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(Data.ToFloat64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));//does not work
        C.WriteLine(ToHexString(Data.ToFloat64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        try
        {
            C.WriteLine(ToHexString(Data.ToFloat64(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(Data.ToFloat64(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region ToMultiple
        C.WriteLine("---ToMultiple---");

        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt16Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt16Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt16Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 }, false)));
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToInt16Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToInt16Multiple(new byte[] { 0x11 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 }, false)));
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToInt32Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, false)));//does not work
        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1E }, false)));//does not work
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToInt64Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 }, false)));
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat32Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, false)));//does not work
        C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1E }, false)));//does not work
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat64Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", Data.ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion
    }
}
