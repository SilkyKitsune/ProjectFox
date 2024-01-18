using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public static void ShapeTests()
    {
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
    }
}
