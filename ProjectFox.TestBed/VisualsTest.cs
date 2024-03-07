using System;

using ProjectFox.CoreEngine.Math;
using M = ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Visuals;

using ProjectFox.Windows;

using static ProjectFox.TestBed.DebugColors;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    public static void VisualsTest()
    {
        Engine.Frequency = 15;
        Engine.uncapped = false;
        Screen.position = new(0, 0);
        Screen.Size = new(50, 50);
        Screen.Scale = 8f;
        Screen.OneToOne = true;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;
        //Debug.DebugAlpha = 128;

        window = new("Test Window", 100, 100, true);

        Scene scene = RasterObjectTest();
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        window.Start();
    }

    private static Scene RasterObjectTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128),
            BGTexture = BigGradient(),
            BGPalette = FourColors(),
            BGOffset = new(5, 2),
            BGVerticalFlip = true,
            BGHorizontalFlip = true,
        };

        VisualLayer mask = new(new("TstMask", 0))
        {
            Scene = scene,//disabling this causes a weird blending bug
            //alpha = 128,
        };
        VisualLayer layer = new(new("TstLayr", 0))
        {
            Scene = scene,
            //alpha = 128,
            layerMask = mask,
        };

        new DebugController(window)
        {
            Scene = scene,
            //printFrameInfo = true
        };

        new DebugSetPiece(new("SetPiec", 0))
        {
            Scene = scene,
            drawPosition = true,
            positionColor = Magenta,
            layer = mask,
            //visible = false,
            texture = AlphaGradient(),
            //flipOnPixel = true,
            drawTextureBounds = true,
            boundsColor = Cyan,
        };
        new DebugSetPiece(new("SetPiec", 1))
        {
            Scene = scene,
            paused = true,
            Position = new(10, 10),
            //visible = false,
            layer = layer,
            texture = FourByFour(),
            palette = FourColors(),
        };
        new DebugSetPiece(new("SetPiec", 2))
        {
            Scene = scene,
            //paused = true,
            Position = new(20, 10),
            //visible = false,
            layer = layer,
            texture = new PalettizedTexture(1, 1),
            palette = PalAnim(),
        };
        Sprite sprite = new Sprite(new("Sprite_", 0))
        {
            Scene = scene,
            Position = new(25, 25),
            //visible = false,
            layer = layer,
            //palette = FourColors(),
        };
        NameID animName = new("animate", 0);
        NameID animName1 = new("animate", 1);
        sprite.animations.Add(animName, TexAnim());
        sprite.animations.Add(animName1, TexAnim2());
        sprite.Animation = animName;

        return scene;
    }

    private static ColorTexture Gradient()
    {
        Vector size = new(14, 14);
        Color[] pixels = new Color[size.x * size.y];

        for (byte b = 0; b < pixels.Length && b < byte.MaxValue; b++)
            pixels[b] = new(0, 0, b);

        return new(size, pixels);
    }

    private static ColorTexture Corners()
    {
        Vector size = new(10, 10);
        Color[] pixels = new Color[size.x * size.y];

        for (int i = 0, 
            tl = 0 * size.x + 0,
            tr = 0 * size.x + 9,
            bl = 9 * size.x + 0,
            br = 9 * size.x + 9;
            i < pixels.Length; i++)
        {
            if (i == tl) pixels[i] = Red;
            else if (i == tr) pixels[i] = Green;
            else if (i == bl) pixels[i] = Blue;
            else if (i == br) pixels[i] = Yellow;
            else pixels[i] = Black;
        }

        return new(size, pixels);
    }

    private static ColorTexture AlphaGradient()
    {
        Vector size = new(15, 15);
        Color[] pixels = new Color[size.x * size.y];

        for (byte b = 0; b < pixels.Length && b < byte.MaxValue; b++)
            pixels[b] = new(byte.MaxValue, byte.MaxValue, byte.MaxValue, b);

        return new(size, pixels);
    }

    private static PalettizedTexture FourByFour()
    {
        Vector size = new(4, 4);
        byte[] pixels = new byte[]
        {
            0, 1, 2, 3,
            1, 2, 3, 0,
            2, 3, 0, 1,
            3, 0, 1, 2
        };
        return new PalettizedTexture(size, pixels);
    }

    private static ColorTexture BigGradient()
    {
        Vector size = new(50, 50);
        Color[] pixels = new Color[size.x * size.y];

        for (uint i = 0; i < pixels.Length; i++)
        {
            pixels[i] = (i << 8);
            pixels[i].a = byte.MaxValue;
        }

        return new(size, pixels);
    }

    private static TextureAnimation TexAnim()
    {
        TextureAnimation anim = new()
        {
            play = true,
            //loop = true,
            playbackMode = Animation.PlaybackMode.PingPong,
            //palette = FourColors(),
        };
        anim.frames.Add(
            new TextureAnimation.TextureFrame()
            {
                delay = 10,//5,
                texture = Gradient(),
                offset = new(0, 0),
            },
            new TextureAnimation.TextureFrame()
            {
                delay = 10,//5,
                texture = Corners(),
                offset = new(5, 0),
            },
            new TextureAnimation.TextureFrame()
            {
                delay = 10,//5,
                texture = AlphaGradient(),
                offset = new(0, 5),
            },
            new TextureAnimation.TextureFrame()
            {
                delay = 10,//5,
                texture = FourByFour(),
                palette = FourColors(),
                offset = new(-5, 0),
            });
        return anim;
    }

    private static TextureAnimation TexAnim2()
    {
        TextureAnimation anim = new()
        {
            play = true,
            loop = true
        };
        anim.frames.Add(
            new TextureAnimation.TextureFrame()
            {
                delay = 5,
                texture = Gradient()
            },
            new TextureAnimation.TextureFrame()
            {
                delay = 5,
                texture = AlphaGradient()
            });
        return anim;
    }

    private static ColorPalette FourColors() => new(Red, Green, Blue, Yellow);

    private static PaletteAnimation PalAnim()
    {
        PaletteAnimation anim = new()
        {
            play = true,
            loop = true,
            //reverse = true,
            playbackMode = Animation.PlaybackMode.Reverse,
        };
        anim.frames.Add(
            new PaletteAnimation.PaletteFrame()
            {
                delay = 5,
                palette = new ColorPalette(Red)
            },
            new PaletteAnimation.PaletteFrame()
            {
                delay = 5,
                palette = new ColorPalette(Green)
            },
            new PaletteAnimation.PaletteFrame()
            {
                delay = 5,
                palette = new ColorPalette(Blue)
            },
            new PaletteAnimation.PaletteFrame()
            {
                delay = 5,
                palette = new ColorPalette(Yellow)
            });
        return anim;
    }

    private sealed class DebugSetPiece : SetPiece
    {
        internal DebugSetPiece(NameID name) : base(name) { }

        protected override void PreFrame()
        {
            KeyboardMouseState kbm = window.KeyboardMouseState;

            Vector pos = Position;
            switch (M.FindSign(kbm.A, kbm.D))
            {
                case M.Sign.Neg:
                    pos.x -= 1;
                    break;
                case M.Sign.Pos:
                    pos.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.W, kbm.S))
            {
                case M.Sign.Neg:
                    pos.y -= 1;
                    break;
                case M.Sign.Pos:
                    pos.y += 1;
                    break;
            }
            if (kbm.NumpadZero) pos = default;
            Position = pos;

            switch (M.FindSign(kbm.J, kbm.L))
            {
                case M.Sign.Neg:
                    horizontalFlipTexture = false;
                    break;
                case M.Sign.Pos:
                    horizontalFlipTexture = true;
                    break;
            }
            switch (M.FindSign(kbm.I, kbm.K))
            {
                case M.Sign.Neg:
                    verticalFlipTexture = false;
                    break;
                case M.Sign.Pos:
                    verticalFlipTexture = true;
                    break;
            }

            switch (M.FindSign(kbm.F, kbm.H))
            {
                case M.Sign.Neg:
                    horizontalFlipOffset = false;
                    break;
                case M.Sign.Pos:
                    horizontalFlipOffset = true;
                    break;
            }
            switch (M.FindSign(kbm.T, kbm.G))
            {
                case M.Sign.Neg:
                    verticalFlipOffset = false;
                    break;
                case M.Sign.Pos:
                    verticalFlipOffset = true;
                    break;
            }

            switch (M.FindSign(kbm.Left, kbm.Right))
            {
                case M.Sign.Neg:
                    offset.x -= 1;
                    break;
                case M.Sign.Pos:
                    offset.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.Up, kbm.Down))
            {
                case M.Sign.Neg:
                    offset.y -= 1;
                    break;
                case M.Sign.Pos:
                    offset.y += 1;
                    break;
            }
        }
    }
}