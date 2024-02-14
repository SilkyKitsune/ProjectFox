using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;

using static ProjectFox.CoreEngine.Collections.Strings;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public static void VectorTests()
    {
        DirectionTest();
        VectorTest();
        VectorFTest();
        VectorZTest();
        VectorZFTest();
    }

    private static void DirectionTest()
    {
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

        C.WriteLine("-----\n");
    }

    private static void VectorTest()
    {
        C.WriteLine("---Vector---");

        Vector v = new(2, 4);
        Vector vBigger = new(10, 10);
        Vector vSmaller = new(1, -5);
        int vBiggert = 5;
        int vSmallert = -10;
        VectorF vBiggerF = new(5.5f, 5.5f);
        VectorF vSmallerF = new(-1.25f, -10.99f);

        C.WriteLine(Vector.ConcatHex(false, false, v, vBigger, vSmaller));
        C.WriteLine(Vector.ConcatBin(false, false, '|', '_', v, vBigger, vSmaller));
        C.WriteLine(Vector.JoinHex(false, false, ", ", v, vBigger, vSmaller));
        C.WriteLine(Vector.JoinBin(false, false, '|', '_', ", ", v, vBigger, vSmaller));

        byte[] bytes = new byte[17] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
            0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1F };
        C.WriteLine(Vector.FromBytes(bytes, false).ToHexString());
        C.WriteLine(Vector.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", v.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", v.GetBytes(true)));

        Vector[] vectors = Vector.FromBytesMultiple(bytes, false);
        C.WriteLine(Vector.JoinHex(false, false, ", ", vectors));
        C.WriteLine(Vector.JoinHex(false, false, ", ", Vector.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", Vector.GetBytes(vectors, false)));
        C.WriteLine(JoinHex(false, false, ", ", Vector.GetBytes(vectors, true)));

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
    }

    private static void VectorFTest()
    {
        C.WriteLine("---VectorF---");

        VectorF vf = new(2f, 4f);
        VectorF vfBigger = new(10f, 10f);
        VectorF vfSmaller = new(1f, -5f);
        float vfBiggert = 5f;
        float vfSmallert = -10f;
        VectorF vfBiggerF = new(5.5f, 5.5f);
        VectorF vfSmallerF = new(-1.25f, -10.99f);
        Vector v = new(2, 4);

        C.WriteLine(VectorF.ConcatHex(false, false, vf, vfBigger, vfSmaller));
        C.WriteLine(VectorF.ConcatBin(false, false, '|', '_', vf, vfBigger, vfSmaller));
        C.WriteLine(VectorF.JoinHex(false, false, ", ", vf, vfBigger, vfSmaller));
        C.WriteLine(VectorF.JoinBin(false, false, '|', '_', ", ", vf, vfBigger, vfSmaller));

        byte[] bytes = new byte[17] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88,
            0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1F };
        C.WriteLine(VectorF.FromBytes(bytes, false).ToHexString());
        C.WriteLine(VectorF.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", vf.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", vf.GetBytes(true)));

        VectorF[] vectors = VectorF.FromBytesMultiple(bytes, false);
        C.WriteLine(VectorF.JoinHex(false, false, ", ", vectors));
        C.WriteLine(VectorF.JoinHex(false, false, ", ", VectorF.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", VectorF.GetBytes(vectors, false)));
        C.WriteLine(JoinHex(false, false, ", ", VectorF.GetBytes(vectors, true)));

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
    }

    private static void VectorZTest()
    {
        C.WriteLine("---VectorZ---");

        VectorZ vz = new(2, 4, 5);
        VectorZ vzBigger = new(10, 10, 10);
        VectorZ vzSmaller = new(1, -5, -1);
        int vzBiggert = 5;
        int vzSmallert = -10;
        VectorZF vzBiggerF = new(5.5f, 5.5f, 5.5f);
        VectorZF vzSmallerF = new(-1.25f, -10.99f, -0.00001f);

        C.WriteLine(VectorZ.ConcatHex(false, false, vz, vzBigger, vzSmaller));
        C.WriteLine(VectorZ.ConcatBin(false, false, '|', '_', vz, vzBigger, vzSmaller));
        C.WriteLine(VectorZ.JoinHex(false, false, ", ", vz, vzBigger, vzSmaller));
        C.WriteLine(VectorZ.JoinBin(false, false, '|', '_', ", ", vz, vzBigger, vzSmaller));

        byte[] bytes = new byte[25] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC,
            0xDD, 0xEE, 0xFF, 0x00, 0x1F, 0x2F, 0x3F, 0x4F, 0x5F, 0x6F, 0x7F, 0x8F, 0x0F };
        C.WriteLine(VectorZ.FromBytes(bytes, false).ToHexString());
        C.WriteLine(VectorZ.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", vz.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", vz.GetBytes(true)));

        VectorZ[] vectors = VectorZ.FromBytesMultiple(bytes, false);
        C.WriteLine(VectorZ.JoinHex(false, false, ", ", vectors));
        C.WriteLine(VectorZ.JoinHex(false, false, ", ", VectorZ.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", VectorZ.GetBytes(vectors, false)));
        C.WriteLine(JoinHex(false, false, ", ", VectorZ.GetBytes(vectors, true)));

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
    }

    private static void VectorZFTest()
    {
        C.WriteLine("---VectorZF---");

        VectorZF vzf = new(2f, 4f, 5f);
        VectorZF vzfBigger = new(10f, 10f, 10f);
        VectorZF vzfSmaller = new(1f, -5f, -1f);
        float vzfBiggert = 5f;
        float vzfSmallert = -10f;
        VectorZF vzfBiggerF = new(5.5f, 5.5f, 5.5f);
        VectorZF vzfSmallerF = new(-1.25f, -10.99f, -0.00001f);
        VectorZ vz = new(2, 4, 5);

        C.WriteLine(VectorZF.ConcatHex(false, false, vzf, vzfBigger, vzfSmaller));
        C.WriteLine(VectorZF.ConcatBin(false, false, '|', '_', vzf, vzfBigger, vzfSmaller));
        C.WriteLine(VectorZF.JoinHex(false, false, ", ", vzf, vzfBigger, vzfSmaller));
        C.WriteLine(VectorZF.JoinBin(false, false, '|', '_', ", ", vzf, vzfBigger, vzfSmaller));

        byte[] bytes = new byte[25] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC,
            0xDD, 0xEE, 0xFF, 0x00, 0x1F, 0x2F, 0x3F, 0x4F, 0x5F, 0x6F, 0x7F, 0x8F, 0x0F };
        C.WriteLine(VectorZF.FromBytes(bytes, false).ToHexString());
        C.WriteLine(VectorZF.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", vzf.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", vzf.GetBytes(true)));

        VectorZF[] vectors = VectorZF.FromBytesMultiple(bytes, false);
        C.WriteLine(VectorZF.JoinHex(false, false, ", ", vectors));
        C.WriteLine(VectorZF.JoinHex(false, false, ", ", VectorZF.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", VectorZF.GetBytes(vectors, false)));
        C.WriteLine(JoinHex(false, false, ", ", VectorZF.GetBytes(vectors, true)));

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
    }
}
