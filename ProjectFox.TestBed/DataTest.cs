using System;
using C = System.Console;

using static ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    private static void PackUnpackTest(int packetSize, params byte[] values)
    {
        values = PackBits(packetSize, values);
        C.WriteLine("Pack   " + JoinBin(false, false, '_', " ", values));
        C.WriteLine("Unpack " + JoinBin(false, false, '_', " ", UnpackBits(packetSize, values)));
    }
    
    public unsafe static void DataTest()
    {
        int i1 = 0x11223344, i2 = 0x55667788;
        float f1 = *(float*)&i1, f2 = *(float*)&i2;

        long l1 = 0x1122334455667788L, l2 = unchecked((long)0x99AABBCCEEDDFF00L);
        double d1 = *(double*)&l1, d2 = *(double*)&l2;

        #region Get
        C.WriteLine("---Get---");

        C.WriteLine(JoinHex(false, false, ", ", GetBytes((short)0x1122, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes((short)0x1122, true)));

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(0x11223344, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(0x11223344, true)));

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(0x1122334455667788L, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(0x1122334455667788L, true)));

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(f1, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(f1, true)));

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(d1, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(d1, true)));

        C.WriteLine("-----\n");
        #endregion

        #region GetMultiple
        C.WriteLine("---GetMultiple---");

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new short[] { 0x1122, 0x3344, 0x5566, 0x7788 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new short[] { 0x1122, 0x3344, 0x5566, 0x7788 }, true)));
        try
        {
            short[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new int[] { 0x11223344, 0x55667788 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new int[] { 0x11223344, 0x55667788 }, true)));
        try
        {
            int[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new long[] { 0x1122334455667788, unchecked((long)0x99AABBCCEEDDFF00L) }, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new long[] { 0x1122334455667788, unchecked((long)0x99AABBCCEEDDFF00L) }, true)));
        try
        {
            long[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new float[] { f1, f2 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new float[] { f1, f2 }, true)));
        try
        {
            float[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new double[] { d1, d2 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", GetBytes(new double[] { d1, d2 }, true)));
        try
        {
            double[] n = null;
            C.WriteLine(JoinHex(false, false, ", ", GetBytes(n, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region To
        C.WriteLine("---To---");

        C.WriteLine(ToHexString(ToInt16(new byte[] { 0x11, 0x22 }, false)));
        C.WriteLine(ToHexString(ToInt16(new byte[] { 0x11, 0x22 }, true)));
        try
        {
            C.WriteLine(ToHexString(ToInt16(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(ToInt16(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(ToInt32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, false)));
        C.WriteLine(ToHexString(ToInt32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, true)));
        try
        {
            C.WriteLine(ToHexString(ToInt32(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(ToInt32(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(ToInt64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));//does not work
        C.WriteLine(ToHexString(ToInt64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        try
        {
            C.WriteLine(ToHexString(ToInt64(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(ToInt64(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(ToFloat32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, false)));
        C.WriteLine(ToHexString(ToFloat32(new byte[] { 0x11, 0x22, 0x33, 0x44 }, true)));
        try
        {
            C.WriteLine(ToHexString(ToFloat32(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(ToFloat32(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(ToHexString(ToFloat64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));//does not work
        C.WriteLine(ToHexString(ToFloat64(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        try
        {
            C.WriteLine(ToHexString(ToFloat64(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(ToHexString(ToFloat64(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region ToMultiple
        C.WriteLine("---ToMultiple---");

        C.WriteLine(JoinHex(false, false, ", ", ToInt16Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", ToInt16Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", ToInt16Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 }, false)));
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToInt16Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToInt16Multiple(new byte[] { 0x11 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 }, false)));
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToInt32Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToInt32Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, false)));//does not work
        C.WriteLine(JoinHex(false, false, ", ", ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1E }, false)));//does not work
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToInt64Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToInt64Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, false)));
        C.WriteLine(JoinHex(false, false, ", ", ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99 }, false)));
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToFloat32Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToFloat32Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine(JoinHex(false, false, ", ", ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, false)));//does not work
        C.WriteLine(JoinHex(false, false, ", ", ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00 }, true)));
        C.WriteLine(JoinHex(false, false, ", ", ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1E }, false)));//does not work
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToFloat64Multiple(null, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            C.WriteLine(JoinHex(false, false, ", ", ToFloat64Multiple(new byte[] { 0x11, 0x22, 0x33 }, false)));
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region PackBits
        C.WriteLine("---PackBits---");

        PackUnpackTest(1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1);
        PackUnpackTest(1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1);
        PackUnpackTest(1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1, 0b1);

        PackUnpackTest(2, 0b10, 0b01, 0b10);
        PackUnpackTest(2, 0b10, 0b01, 0b10, 0b01);
        PackUnpackTest(2, 0b10, 0b01, 0b10, 0b01, 0b01);

        PackUnpackTest(3, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101);
        PackUnpackTest(3, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101);
        PackUnpackTest(3, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101, 0b101);

        PackUnpackTest(4, 0b1010);
        PackUnpackTest(4, 0b1010, 0b0101);
        PackUnpackTest(4, 0b1010, 0b0101, 0b0101);

        PackUnpackTest(5, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101);
        PackUnpackTest(5, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101);
        PackUnpackTest(5, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101, 0b10101);

        PackUnpackTest(6, 0b101010, 0b010101, 0b101010);
        PackUnpackTest(6, 0b101010, 0b010101, 0b101010, 0b010101);
        PackUnpackTest(6, 0b101010, 0b010101, 0b101010, 0b010101, 0b101010);
        PackUnpackTest(6, 0b101010, 0b010101, 0b101010, 0b010101, 0b101010, 0b010101);
        PackUnpackTest(6, 0b101010, 0b010101, 0b101010, 0b010101, 0b101010, 0b010101, 0b101010, 0b010101);

        PackUnpackTest(7, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101);
        PackUnpackTest(7, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101);
        PackUnpackTest(7, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101, 0b1010101);

        PackUnpackTest(8, 0b1010_1010);

        try
        {
            PackBits(1, null);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            PackBits(1, new byte[0]);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            PackBits(-1, new byte[1]);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            PackBits(9, new byte[1]);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        try
        {
            UnpackBits(1, null);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            UnpackBits(1, new byte[0]);
        }
        catch (ArgumentNullException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            UnpackBits(-1, new byte[1]);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }
        try
        {
            UnpackBits(9, new byte[1]);
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");
        #endregion

        #region Parse
        C.WriteLine("---Parse---");

        C.WriteLine($"{TryParseBin("1011", false, out byte b)} {b} {ToBinString(b)}");//bin has endianess backwards
        C.WriteLine($"{TryParseBin("1011", true, out b)} {b} {ToBinString(b)}");
        C.WriteLine($"{TryParseBin("%00101011", false, out b)} {b} {ToBinString(b)}");
        C.WriteLine($"{TryParseBin("0b00101011", false, out b)} {b} {ToBinString(b)}");
        
        C.WriteLine($"{TryParseBin("0100000000000000", false, out short s)} {s} {ToBinString(s)}");
        C.WriteLine($"{TryParseBin("0100000000000000", true, out s)} {s} {ToBinString(s)}");
        C.WriteLine($"{TryParseBin("01000000000000000100000000000000", false, out int i)} {i} {ToBinString(i)}");
        C.WriteLine($"{TryParseBin("01000000000000000100000000000000", true, out i)} {i} {ToBinString(i)}");
        C.WriteLine($"{TryParseBin("0100000000000000010000000000000001000000000000000100000000000000", false, out long l)} {l} {ToBinString(l)}");
        C.WriteLine($"{TryParseBin("0100000000000000010000000000000001000000000000000100000000000000", true, out l)} {l} {ToBinString(l)}");
        C.WriteLine($"{TryParseBin("01000000000000000100000000000000", false, out float f)} {f} {ToBinString(f)}");
        C.WriteLine($"{TryParseBin("01000000000000000100000000000000", true, out f)} {f} {ToBinString(f)}");
        C.WriteLine($"{TryParseBin("0100000000000000010000000000000001000000000000000100000000000000", false, out double d)} {d} {ToBinString(d)}");
        C.WriteLine($"{TryParseBin("0100000000000000010000000000000001000000000000000100000000000000", true, out d)} {d} {ToBinString(d)}");

        C.WriteLine($"{TryParseBin(null, false, out b)} {b} {ToBinString(b)}");
        C.WriteLine($"{TryParseBin("", false, out b)} {b} {ToBinString(b)}");
        C.WriteLine($"{TryParseBin("%", false, out b)} {b} {ToBinString(b)}");
        C.WriteLine($"{TryParseBin("0b", false, out b)} {b} {ToBinString(b)}");
        C.WriteLine($"{TryParseBin("011110000", false, out b)} {b} {ToBinString(b)}");

        C.WriteLine($"{TryParseHex("FA", false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("FA", true, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("18", false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("$18", false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("0x18", false, out b)} {b} {ToHexString(b)}");

        C.WriteLine($"{TryParseHex("18FA", false, out s)} {s} {ToHexString(s)}");
        C.WriteLine($"{TryParseHex("18FA", true, out s)} {s} {ToHexString(s)}");
        C.WriteLine($"{TryParseHex("18FA7F1A", false, out i)} {i} {ToHexString(i)}");
        C.WriteLine($"{TryParseHex("18FA7F1A", true, out i)} {i} {ToHexString(i)}");
        C.WriteLine($"{TryParseHex("18FA7F1A01234567", false, out l)} {l} {ToHexString(l)}");
        C.WriteLine($"{TryParseHex("18FA7F1A01234567", true, out l)} {l} {ToHexString(l)}");
        C.WriteLine($"{TryParseHex("18FA7F1A", false, out f)} {f} {ToHexString(f)}");
        C.WriteLine($"{TryParseHex("18FA7F1A", true, out f)} {f} {ToHexString(f)}");
        C.WriteLine($"{TryParseHex("18FA7F1A01234567", false, out d)} {d} {ToHexString(d)}");
        C.WriteLine($"{TryParseHex("18FA7F1A01234567", true, out d)} {d} {ToHexString(d)}");

        C.WriteLine($"{TryParseHex(null, false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("", false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("$", false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("0x", false, out b)} {b} {ToHexString(b)}");
        C.WriteLine($"{TryParseHex("FFF", false, out b)} {b} {ToHexString(b)}");

        C.WriteLine($"{TryParseAny("10", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");
        C.WriteLine($"{TryParseAny("$10", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");
        C.WriteLine($"{TryParseAny("0x10", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");
        C.WriteLine($"{TryParseAny("%10", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");
        C.WriteLine($"{TryParseAny("0b10", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");
        C.WriteLine($"{TryParseAny("7F", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");

        C.WriteLine($"{TryParseAny(short.MaxValue.ToString(), false, out s)} {s} {ToHexString(s)} {ToBinString(s)}");
        C.WriteLine($"{TryParseAny(int.MaxValue.ToString(), false, out i)} {i} {ToHexString(i)} {ToBinString(i)}");
        C.WriteLine($"{TryParseAny(long.MaxValue.ToString(), false, out l)} {l} {ToHexString(l)} {ToBinString(l)}");
        C.WriteLine($"{TryParseAny(float.MaxValue.ToString(), false, out f)} {f} {ToHexString(f)} {ToBinString(f)}");
        C.WriteLine($"{TryParseAny(double.MaxValue.ToString(), false, out d)} {d} {ToHexString(d)} {ToBinString(d)}");

        C.WriteLine($"{TryParseAny("%FF", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");
        C.WriteLine($"{TryParseAny("0bFF", false, out b)} {b} {ToHexString(b)} {ToBinString(b)}");

        C.WriteLine("-----\n");
        #endregion

        #region String
        #region Hex
        C.WriteLine("---Hex---");
        
        C.WriteLine(ToHexString((byte)0x01, false, false));
        C.WriteLine(ToHexString((byte)0x01, true, false));
        C.WriteLine(ToHexString((byte)0x01, false, true));
        C.WriteLine(ToHexString((byte)0x01, true, true));

        C.WriteLine(ToHexString((short)0x0123, false, false));
        C.WriteLine(ToHexString((short)0x0123, true, false));
        C.WriteLine(ToHexString((short)0x0123, false, true));
        C.WriteLine(ToHexString((short)0x0123, true, true));

        C.WriteLine(ToHexString(0x01234567, false, false));
        C.WriteLine(ToHexString(0x01234567, true, false));
        C.WriteLine(ToHexString(0x01234567, false, true));
        C.WriteLine(ToHexString(0x01234567, true, true));

        C.WriteLine(ToHexString(0x0123456789ABCDEF, false, false));
        C.WriteLine(ToHexString(0x0123456789ABCDEF, true, false));
        C.WriteLine(ToHexString(0x0123456789ABCDEF, false, true));
        C.WriteLine(ToHexString(0x0123456789ABCDEF, true, true));

        unsafe
        {
            int fi = 0x01234567;
            C.WriteLine(ToHexString(*(float*)&fi, false, false));
            C.WriteLine(ToHexString(*(float*)&fi, true, false));
            C.WriteLine(ToHexString(*(float*)&fi, false, true));
            C.WriteLine(ToHexString(*(float*)&fi, true, true));

            long dl = 0x0123456789ABCDEF;
            C.WriteLine(ToHexString(*(double*)&dl, false, false));
            C.WriteLine(ToHexString(*(double*)&dl, true, false));
            C.WriteLine(ToHexString(*(double*)&dl, false, true));
            C.WriteLine(ToHexString(*(double*)&dl, true, true));
        }
        
        C.WriteLine("-----\n");
        #endregion

        #region ConcatHex
        #endregion

        #region JoinHex
        #endregion
        
        #region Bin
        C.WriteLine("---Bin---");

        C.WriteLine(ToBinString((byte)0b0101_0101, false, false));
        C.WriteLine(ToBinString((byte)0b0101_0101, false, false, '@'));
        C.WriteLine(ToBinString((byte)0b0101_0101, true, false));
        C.WriteLine(ToBinString((byte)0b0101_0101, false, true));
        C.WriteLine(ToBinString((byte)0b0101_0101, true, true));

        C.WriteLine(ToBinString((short)0b0101_0101__0101_0101, false, false));
        C.WriteLine(ToBinString((short)0b0101_0101__0101_0101, false, false, '#', '@'));
        C.WriteLine(ToBinString((short)0b0101_0101__0101_0101, true, false));
        C.WriteLine(ToBinString((short)0b0101_0101__0101_0101, false, true));
        C.WriteLine(ToBinString((short)0b0101_0101__0101_0101, true, true));

        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101, false, false));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101, false, false, '#', '@'));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101, true, false));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101, false, true));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101, true, true));

        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101, false, false));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101, false, false, '#', '@'));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101, true, false));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101, false, true));
        C.WriteLine(ToBinString(0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101, true, true));

        unsafe
        {
            int fi = 0b0101_0101__0101_0101____0101_0101__0101_0101;
            C.WriteLine(ToBinString(*(float*)&fi, false, false));
            C.WriteLine(ToBinString(*(float*)&fi, false, false, '#', '@'));
            C.WriteLine(ToBinString(*(float*)&fi, true, false));
            C.WriteLine(ToBinString(*(float*)&fi, false, true));
            C.WriteLine(ToBinString(*(float*)&fi, true, true));

            long dl = 0b0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101____0101_0101__0101_0101;
            C.WriteLine(ToBinString(*(double*)&dl, false, false));
            C.WriteLine(ToBinString(*(double*)&dl, false, false, '#', '@'));
            C.WriteLine(ToBinString(*(double*)&dl, true, false));
            C.WriteLine(ToBinString(*(double*)&dl, false, true));
            C.WriteLine(ToBinString(*(double*)&dl, true, true));
        }

        C.WriteLine("-----\n");
        #endregion

        #region ConcatBin
        #endregion

        #region JoinBin
        #endregion
        #endregion
    }
}