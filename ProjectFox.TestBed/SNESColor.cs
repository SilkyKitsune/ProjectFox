using System;
using C = System.Console;
using B = System.BitConverter;
using F = System.IO.File;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;
using ProjectFox.CoreEngine.Collections;
using S = ProjectFox.CoreEngine.Collections.Strings;

using ProjectFox.GameEngine;
using CG = ProjectFox.GameEngine.Debug.Console;
using ProjectFox.GameEngine.Visuals;

using ProjectFox.Windows;

using IPSLib;

using static ProjectFox.TestBed.DebugNames;
using static ProjectFox.TestBed.SNESColorConverter;

namespace ProjectFox.TestBed;

public static class SNESColorConverter
{
    private const int FiveBitMax = 0x1F;
    private const float FiveBitMaxF = FiveBitMax;

    private static Color FromSNESHex(short color) => new((byte)(color & FiveBitMax), (byte)(color >> 5 & FiveBitMax), (byte)(color >> 10 & FiveBitMax));

    private static short GetSNESHex(Color color) => (short)((color.b << 10) | (color.g << 5) | (color.r));

    public static AutoSizedArray<short> ToColor15(params Color[] colors)
    {
        AutoSizedArray<short> snesColors = new();
        foreach (Color c in colors)
        {
            if (c.a == byte.MaxValue)// < 7FFF?
            {
                Color snes = new((byte)(c.R * FiveBitMax), (byte)(c.G * FiveBitMax), (byte)(c.B * FiveBitMax)),
                    back = new((byte)((snes.r / FiveBitMaxF) * byte.MaxValue), (byte)((snes.g / FiveBitMaxF) * byte.MaxValue), (byte)((snes.b / FiveBitMaxF) * byte.MaxValue));
                short hex = GetSNESHex(snes);
                snesColors.Add(hex);
                C.WriteLine(
                    $"24: {c.ToHexString()}\n" +
                    $"RE: {back.ToHexString()} | {(snes - back).ToHexString()}\n" +
                    $"15: {snes.ToHexString()}\n" +
                    $"15 Hex: {S.ToHexString(hex)}\n" +
                    $"---");
            }
            else
            {
                short hex = (short)c.hex;
                snesColors.Add(hex);
                C.WriteLine(
                    $"15: {c.ToHexString()}\n" +
                    $"15 Hex: {S.ToHexString(hex)}\n" +
                    $"---");
            }
        }
        return snesColors;
    }

    public static AutoSizedArray<Color> ToColor24(params short[] colors)
    {
        AutoSizedArray<Color> newColors = new();
        foreach (short s in colors)
        {
            Color c = FromSNESHex(s);
            newColors.Add(new Color((byte)((c.r / FiveBitMaxF) * byte.MaxValue), (byte)((c.g / FiveBitMaxF) * byte.MaxValue), (byte)((c.b / FiveBitMaxF) * byte.MaxValue)));
        }
        return newColors;
    }
}

public static class SNESPalettePatchMaker
{
    private enum PaletteCode : uint
    {
        DarkBrown = 0x1C0202FF,
        DarkRedViolet = 0x310216FF,
        Rouge = 0xBD1425FF,
        Pink = 0xF84267FF,
        Coral = 0xFF7A7AFF,
        SkinColor = 0xFCBEACFF,//rename
        Cream = 0xF0DBD1FF,

        DarkGreen = 0x1A3C36FF,
        Green = 0x12823DFF,
        LightGreen = 0x19C86BFF,

        OffBlack = 0x170D15FF,
        LightOffBlack = 0x24182AFF,
        DarkGrey = 0x423D52FF,
        Grey = 0x6B6B85FF,

        DarkRed = 0x5C0017FF,
        Orange = 0x972420FF,
        CreamOrange = 0xEB7047FF,
        Yellow = 0xEB9742FF,

        Black = 0x05030CFF,
        White = 0xFDF7F8FF,

        DarkViolet = 0x28063DFF,
        Indigo = 0x352367FF,
        NeonPurple = 0x8C128CFF,
        Lavender = 0x7F36FCFF,

        Blue = 0x1E45B8FF,
        LightBlue = 0x2EB6FAFF,
        SkyBlue = 0x9CE9F7FF,
        //greenish blue?

        LightBlueOld = 0x54B1F8FF,
        SkyBlueOld = 0xB8D8FFFF,

        DarkCum = 0xF9EFD8FF,
        LightCum = 0xFBF4E5FF,
    }

    private const int Address = 0x5BD308;

    private const string OutPath = "SMZ3SilkyPalettes";

    private static readonly Color[] colors =
    {
        ///Green
        (uint)PaletteCode.White,
        (uint)PaletteCode.SkyBlue,
        (uint)PaletteCode.Coral,
        (uint)PaletteCode.Cream,
        (uint)PaletteCode.Black,
        (uint)PaletteCode.DarkRed,
        (uint)PaletteCode.DarkRedViolet,
        (uint)PaletteCode.LightBlue,
        (uint)PaletteCode.DarkGreen,
        (uint)PaletteCode.Green,
        (uint)PaletteCode.LightGreen,
        0,
        0,
        0,
        0,

        ///Blue
        (uint)PaletteCode.White,
        (uint)PaletteCode.SkyBlue,
        (uint)PaletteCode.Coral,
        (uint)PaletteCode.Cream,
        (uint)PaletteCode.Black,
        (uint)PaletteCode.DarkRed,
        (uint)PaletteCode.DarkRedViolet,
        (uint)PaletteCode.LightBlue,
        (uint)PaletteCode.Blue,
        0x0DDDC1FF,//greenish blue
        (uint)PaletteCode.SkyBlue,
        0,
        0,
        0,
        0,

        ///Red
        (uint)PaletteCode.White,
        (uint)PaletteCode.SkyBlue,
        (uint)PaletteCode.Coral,
        (uint)PaletteCode.Cream,
        (uint)PaletteCode.Black,
        (uint)PaletteCode.DarkRed,
        (uint)PaletteCode.DarkRedViolet,
        (uint)PaletteCode.LightBlue,
        (uint)PaletteCode.Rouge,
        (uint)PaletteCode.Pink,
        (uint)PaletteCode.Coral,
        0,
        0,
        0,
        0,

        ///Bunny
        (uint)PaletteCode.White,
        (uint)PaletteCode.SkyBlue,//?
        (uint)PaletteCode.Coral,
        (uint)PaletteCode.Cream,
        (uint)PaletteCode.Black,
        (uint)PaletteCode.Coral,
        (uint)PaletteCode.Pink,
        (uint)PaletteCode.LightBlue,//?
        (uint)PaletteCode.Rouge,//?
        (uint)PaletteCode.Pink,//?
        (uint)PaletteCode.Coral,//?
        0,
        0,
        0,
        0,
    };

    public static void Run()
    {
        IPS patch = new();

        short[] snesColors = ToColor15(colors).ToArray();
        AutoSizedArray<byte> data = new(0x40);

        string str = "", separator = ", ";
        foreach (short s in snesColors)
        {
            data.Add(B.GetBytes(s));
            str += S.ToHexString(s) + separator;
        }
        C.WriteLine(str);
        patch.Add(false, Address, data.ToArray());
        
        C.WriteLine(patch);
        patch.WritePatch(OutPath);
    }

    private static ColorPalette RandomColors(int length)//move to colorpalette?
    {
        if (length <= 0) throw new Exception();

        Random r = new();//seed

        Color[] colors = new Color[length];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = r.Next(0xFFFFFF) << 8 & 0xFF;

        return new(colors);
    }
}

public static class SNESPaletteViewer
{
    private const int Address = 0x0DD308, Length = 0x700, Line = 15;

    private const string Path = "F:\\Emulators\\Super NES\\ROMs\\Zelda no Densetsu - Kamigami no Triforce (Japan).sfc";

    private static GameWindow window;

    private static readonly short[]
        Overworld00 = { 0x14A5, 0x14E9, 0x152C, 0x1D8F, 0x19C6, 0x2669, 0x25F1 },
        Overworld01 = { 0x14A5, 0x14E9, 0x152C, 0x1D8F, 0x158D, 0x2211, 0x25F1 },
        Overworld02 = { 0x14A5, 0x14E9, 0x152C, 0x1D8F, 0x152C, 0x25F1, 0x25F1 },
        Overworld03 = { 0x14A5, 0x3A0A, 0x4AEF, 0x5FB6, 0x1565, 0x2669, 0x1B46 },
        Overworld04 = { 0x14A5, 0x194C, 0x25D1, 0x3234, 0x1DE5, 0x2669, 0x2628 },
        Overworld05 = { 0x14A5, 0x3D26, 0x5587, 0x7ACE, 0x14E9, 0x620B, 0x1D8F },
        Overworld06 = { 0x14C5, 0x216E, 0x29F3, 0x3677, 0x1CED, 0x2952, 0x46D7 },
        Overworld07 = { 0x14C5, 0x3A0A, 0x4AEF, 0x5FB6, 0x6BDA, 0x19C6, 0x2669 },
        Overworld08 = { 0x14A5, 0x194C, 0x21D1, 0x3255, 0x21A5, 0x21E8, 0x1965 },
        Overworld09 = { 0x18C6, 0x1D8F, 0x620B, 0x6B98, 0x1DE7, 0x2669, 0x25F1 },
        Overworld10 = { 0x18C6, 0x5187, 0x1E36, 0x3F9E, 0x1DE7, 0x2669, 0x620B },
        Overworld11 = { 0x14A5, 0x14E9, 0x152C, 0x1D8F, 0x19C6, 0x2669, 0x25F1 };

    private static string JoinHex(Color[] colors)//move to core engine
    {
        string s = "";
        foreach (Color c in colors) s += c.ToHexString() + '\n';
        return s;
    }

    public static void Run()
    {
        byte[] data = F.ReadAllBytes(Path);
        data = data[Address..(Address + Length)];
        CG.QueueMessage(S.ToHexString(Address + Length, false, true));

        AutoSizedArray<short> snesColors = new();
        for (int i = 0; i < data.Length; i++)
            snesColors.Add(B.ToInt16(new byte[] { data[i++], data[i] }));

        //CG.QueueMessage(JoinHex(snesColors.ToArray()));

        AutoSizedArray<Color> colors = SNESColorConverter.ToColor24(snesColors.ToArray());
        for (int l = colors.Length, i = Abs(l - ((l / Line + 1) * Line)); i > 0; i--) colors.Add(new Color());

        //CG.QueueMessage(JoinHex(colors.ToArray()));

        ColorTexture texture = new(Line, colors.Length / Line, colors.ToArray());

        Engine.Frequency = 5;
        Engine.uncapped = false;
        Screen.position = new(0, 0);
        Screen.Size = (texture.Dimensions + 5).Clamp(1, 50);
        Screen.Scale = 16f;
        Screen.OneToOne = true;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;

        window = new(WindowName, 100, 100, true);

        Scene scene = new(TestScene) { ClearMode = Screen.ClearModes.Clear };
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        new DebugController(window) { Scene = scene };

        new SetPiece(new("Display", 0))
        {
            Scene = scene,
            layer = new(new("TstLayr", 0)) { Scene = scene },
            Position = new(1, 1),
            texture = texture,
            drawTextureBounds = true,
        };

        window.Start();
    }
}