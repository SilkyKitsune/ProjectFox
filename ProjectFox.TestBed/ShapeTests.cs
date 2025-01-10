using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public static void ShapeTests()
    {
        RectangleTest();
        RectangleFTest();
        TriangleTest();
        TriangleFTest();
    }

    private static void RectangleTest()
    {
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

        C.WriteLine(Rectangle.ConcatHex(false, false, r, rBigger, rSmaller));
        C.WriteLine(Rectangle.ConcatBin(false, false, '|', '_', r, rBigger, rSmaller));
        C.WriteLine(Rectangle.JoinHex(false, false, ", ", r, rBigger, rSmaller));
        C.WriteLine(Rectangle.JoinBin(false, false, '|', '_', ", ", r, rBigger, rSmaller));

        byte[] bytes = new byte[33] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00,
            0x1F, 0x2F, 0x3F, 0x4F, 0x5F, 0x6F, 0x7F, 0x8F, 0x9F, 0xAF, 0xBF, 0xCF, 0xDF, 0xEF, 0xFF, 0x0F, 0xF0 };
        C.WriteLine(Rectangle.FromBytes(bytes, false).ToHexString());
        C.WriteLine(Rectangle.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", r.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", r.GetBytes(true)));

        Rectangle[] rectangles = Rectangle.FromBytesMultiple(bytes, false);
        C.WriteLine(Rectangle.JoinHex(false, false, ", ", rectangles));
        C.WriteLine(Rectangle.JoinHex(false, false, ", ", Rectangle.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", Rectangle.GetBytes(rectangles, false)));
        C.WriteLine(JoinHex(false, false, ", ", Rectangle.GetBytes(rectangles, true)));

        IShapeTest(r, rBigger, rSmaller, rBiggerV, rSmallerV, rBiggert, rSmallert, rBiggerF, rSmallerF, rBigger, rBiggerF);
        IDirectionTest(r, rBiggerV, rSmallerV);

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
    }

    private static void RectangleFTest()
    {
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
        Rectangle rBigger = new(10, 10, 10, 10);

        C.WriteLine(RectangleF.ConcatHex(false, false, rf, rfBigger, rfSmaller));
        C.WriteLine(RectangleF.ConcatBin(false, false, '|', '_', rf, rfBigger, rfSmaller));
        C.WriteLine(RectangleF.JoinHex(false, false, ", ", rf, rfBigger, rfSmaller));
        C.WriteLine(RectangleF.JoinBin(false, false, '|', '_', ", ", rf, rfBigger, rfSmaller));

        byte[] bytes = new byte[33] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00,
            0x1F, 0x2F, 0x3F, 0x4F, 0x5F, 0x6F, 0x7F, 0x8F, 0x9F, 0xAF, 0xBF, 0xCF, 0xDF, 0xEF, 0xFF, 0x0F, 0xF0 };
        C.WriteLine(RectangleF.FromBytes(bytes, false).ToHexString());
        C.WriteLine(RectangleF.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", rf.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", rf.GetBytes(true)));

        RectangleF[] rectangles = RectangleF.FromBytesMultiple(bytes, false);
        C.WriteLine(RectangleF.JoinHex(false, false, ", ", rectangles));
        C.WriteLine(RectangleF.JoinHex(false, false, ", ", RectangleF.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", RectangleF.GetBytes(rectangles, false)));
        C.WriteLine(JoinHex(false, false, ", ", RectangleF.GetBytes(rectangles, true)));

        IShapeTest(rf, rfBigger, rfSmaller, rfBiggerV, rfSmallerV, rfBiggert, rfSmallert, rfBiggerF, rfSmallerF, rfBiggerF, rBigger);
        IDirectionTest(rf, rfBiggerV, rfSmallerV);

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
    }

    private static void TriangleTest()
    {
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

        C.WriteLine(Triangle.ConcatHex(false, false, t, tBigger, tSmaller));
        C.WriteLine(Triangle.ConcatBin(false, false, '|', '_', t, tBigger, tSmaller));
        C.WriteLine(Triangle.JoinHex(false, false, ", ", t, tBigger, tSmaller));
        C.WriteLine(Triangle.JoinBin(false, false, '|', '_', ", ", t, tBigger, tSmaller));

        byte[] bytes = new byte[49] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1F, 0x2F, 0x3F, 0x4F, 0x5F, 0x6F, 0x7F, 0x8F,
            0x9F, 0xAF, 0xBF, 0xCF, 0xDF, 0xEF, 0xFF, 0x0F, 0x1A, 0x2A, 0x3A, 0x4A, 0x5A, 0x6A, 0x7A, 0x8A, 0x9A, 0xAA, 0xBA, 0xCA, 0xDA, 0xEA, 0xFA, 0xAF,
            0xF0 };
        C.WriteLine(Triangle.FromBytes(bytes, false).ToHexString());
        C.WriteLine(Triangle.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", t.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", t.GetBytes(true)));

        Triangle[] triangles = Triangle.FromBytesMultiple(bytes, false);
        C.WriteLine(Triangle.JoinHex(false, false, ", ", triangles));
        C.WriteLine(Triangle.JoinHex(false, false, ", ", Triangle.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", Triangle.GetBytes(triangles, false)));
        C.WriteLine(JoinHex(false, false, ", ", Triangle.GetBytes(triangles, true)));

        IShapeTest(new Triangle(2, 2, 4, 4, 6, 3), tBigger, tSmaller, tBiggerV, tSmallerV, tBiggert, tSmallert, tBiggerF, tSmallerF, tBigger, tBiggerF);
        IDirectionTest(t, tBiggerV, tSmallerV);

        C.WriteLine("-Operators-");

        C.WriteLine((TriangleF)t);

        C.WriteLine(t == tBigger);
        C.WriteLine(t == tSmaller);
        C.WriteLine(t != tBigger);
        C.WriteLine(t != tSmaller);

        C.WriteLine("-----\n");
    }

    private static void TriangleFTest()
    {
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
        Triangle tBigger = new(10, 10, 10, 10, 10, 10);

        C.WriteLine(TriangleF.ConcatHex(false, false, tf, tfBigger, tfSmaller));
        C.WriteLine(TriangleF.ConcatBin(false, false, '|', '_', tf, tfBigger, tfSmaller));
        C.WriteLine(TriangleF.JoinHex(false, false, ", ", tf, tfBigger, tfSmaller));
        C.WriteLine(TriangleF.JoinBin(false, false, '|', '_', ", ", tf, tfBigger, tfSmaller));

        byte[] bytes = new byte[49] {
            0x11, 0x22, 0x33, 0x44, 0x55, 0x66, 0x77, 0x88, 0x99, 0xAA, 0xBB, 0xCC, 0xDD, 0xEE, 0xFF, 0x00, 0x1F, 0x2F, 0x3F, 0x4F, 0x5F, 0x6F, 0x7F, 0x8F,
            0x9F, 0xAF, 0xBF, 0xCF, 0xDF, 0xEF, 0xFF, 0x0F, 0x1A, 0x2A, 0x3A, 0x4A, 0x5A, 0x6A, 0x7A, 0x8A, 0x9A, 0xAA, 0xBA, 0xCA, 0xDA, 0xEA, 0xFA, 0xAF,
            0xF0 };
        C.WriteLine(TriangleF.FromBytes(bytes, false).ToHexString());
        C.WriteLine(TriangleF.FromBytes(bytes, true).ToHexString());

        C.WriteLine(JoinHex(false, false, ", ", tf.GetBytes(false)));
        C.WriteLine(JoinHex(false, false, ", ", tf.GetBytes(true)));

        TriangleF[] triangles = TriangleF.FromBytesMultiple(bytes, false);
        C.WriteLine(TriangleF.JoinHex(false, false, ", ", triangles));
        C.WriteLine(TriangleF.JoinHex(false, false, ", ", TriangleF.FromBytesMultiple(bytes, true)));

        C.WriteLine(JoinHex(false, false, ", ", TriangleF.GetBytes(triangles, false)));
        C.WriteLine(JoinHex(false, false, ", ", TriangleF.GetBytes(triangles, true)));

        IShapeTest(tf, tfBigger, tfSmaller, tfBiggerV, tfSmallerV, tfBiggert, tfSmallert, tfBiggerF, tfSmallerF, tfBiggerF, tBigger);
        IDirectionTest(tf, tfBiggerV, tfSmallerV);

        C.WriteLine("-Operators-");

        C.WriteLine((Triangle)tf);

        C.WriteLine(tf == tfBigger);
        C.WriteLine(tf == tfSmaller);
        C.WriteLine(tf != tfBigger);
        C.WriteLine(tf != tfSmaller);

        C.WriteLine("-----\n");
    }
}