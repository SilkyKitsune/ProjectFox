﻿using System;
using C = System.Console;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.TestBed;

public static partial class CoreEngineTest
{
    public static void ColorTest()
    {
        C.WriteLine("---Color Convert---");

        C.WriteLine(Color.Convert(0b101, 1, false));
        C.WriteLine(Color.Convert(0b010, 1, false));
        C.WriteLine(Color.Convert(0b1110_0100, 2, true));
        C.WriteLine(Color.Convert(0x7FFF, 5, false));
        C.WriteLine(Color.Convert(0x7DE0, 5, false));
        C.WriteLine(Color.Convert(0b11111_11111__00000_11111__00000_00000, 10, false));
        try
        {
            C.WriteLine(Color.Convert(0b11111_11111__00000_11111__00000_00000, 10, true));
        }
        catch (Exception e)
        {
            C.WriteLine(e.Message);
        }

        Color c2 = new Color(255, 128, 64, 0);
        int c3 = c2.Convert(2, true);
        C.WriteLine(ToBinString(new Color(255, 128, 255).Convert(1, false)));
        C.WriteLine(ToBinString(new Color(128, 255, 128).Convert(1, false)));
        C.WriteLine(c2.ToHexString());
        C.WriteLine(c3);
        C.WriteLine(ToBinString(c3));
        C.WriteLine(ToHexString(new Color(255, 255, 255).Convert(5, false)));
        C.WriteLine(ToHexString(new Color(255, 128, 0).Convert(5, false)));
        C.WriteLine(ToBinString(new Color(255, 128, 0).Convert(10, false)));
        try
        {
            C.WriteLine(ToBinString(new Color(255, 128, 0).Convert(10, true)));
        }
        catch (ArgumentException e)
        {
            C.WriteLine(e.Message);
        }

        C.WriteLine("-----\n");

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

        C.WriteLine($"{c.DistanceColor(cBigger)}^2 = {c.DistanceColorSquared(cBigger)}");
        C.WriteLine($"{c.DistanceColor(cSmaller)}^2 = {c.DistanceColorSquared(cSmaller)}");
        C.WriteLine($"{c.DistanceFromBlack()}^2 = {c.DistanceFromBlackSquared()}");

        C.WriteLine(c.ClosestColor(cBigger, cSmaller));
        C.WriteLine(c.ClosestColor(cBigger, cSmaller, cBigger));
        C.WriteLine(c.ClosestColorIndex(new Color[] { cBigger, cSmaller }));
        C.WriteLine(c.ClosestColorIndex(new Color[] { cBigger, cSmaller, cBigger }));

        C.WriteLine(c.FarthestColor(cBigger, cSmaller));
        C.WriteLine(c.FarthestColor(cBigger, cSmaller, cBigger));
        C.WriteLine(c.FarthestColorIndex(new Color[] { cBigger, cSmaller }));//this returns 1?
        C.WriteLine(c.FarthestColorIndex(new Color[] { cBigger, cSmaller, cBigger }));//this returns 2?

        c.MoveToZero(1);
        C.WriteLine(c);
        c.MoveToZero(cBigger);
        C.WriteLine(c);

        //move to black test?

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
    }
}