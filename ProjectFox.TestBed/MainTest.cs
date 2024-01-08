using System;

using ProjectFox.CoreEngine.Math;
using M = ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Physics;
using ProjectFox.GameEngine.Visuals;

using ProjectFox.Windows;

using static ProjectFox.TestBed.DebugColors;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    private static GameWindow window;

    public static void MainTest()
    {
        Engine.Frequency = 15;
        Screen.position = new(0, 0);
        Screen.Size = new(100, 100);
        Screen.Scale = 8f;
        Screen.OneToOne = true;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;

        Scene scene = PositionDrawTest();
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        window = new("Test Window", 100, 100, true);
        window.Start();
    }

    private static Scene PositionDrawTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128)
        };

        new DebugObject2D(new("TestObj", 0),
            new KinematicScannerRectangle(new("ScnRect", 0))
            {
                Scene = scene,
                size = new(10, 10),
                Position = new(20, 20),
                drawShape = true
            })
        {
            enabled = false,
            Scene = scene,
            drawPosition = true,
            intersectingLines = true,
        };

        new DebugCompoundObject(new("TestCmp", 0),
            new(new("VslLayr", 0)) { Scene = scene })
        { Scene = scene };

        return scene;
    }

    private sealed class DebugObject2D : Object2D
    {
        public DebugObject2D(NameID name, KinematicScannerRectangle rectangle) : base(name) => this.rectangle = rectangle;

        private readonly KinematicScannerRectangle rectangle;

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
            Position = pos;
        }

        protected override void PostFrame()
        {
            Rectangle r = new(rectangle.Position + rectangle.shapeOffset, rectangle.size);
            rectangle.shapeColor = r.Overlapping(Position) ? Green : Blue;
        }
    }

    private sealed class DebugObject3D : Object3D
    {
        public DebugObject3D(NameID name) : base(name) { }
    }

    private sealed class DebugCompoundObject : CompoundObject
    {
        public DebugCompoundObject(NameID name, VisualLayer layer) : base(name)
        {
            SetObject(0, new DebugObject2D(new("TestPet", 0), null)
            {
                paused = true,
                drawPosition = true,
            }, new(0, 1, 0));

            SetObject(1, new DebugObject2D(new("TestPet", 1), null)
            {
                paused = true,
                drawPosition = true,
                positionColor = Magenta,
                intersectingLines = true
            }, new(10, 0, 0));

            SetObject(2, new DebugObject3D(new("TestPet", 2)), new(0, 0, 0));

            SetObject(3, new KinematicScannerRectangle(new("TestPet", 3))
            {
                drawShape = true,
                size = new(10, 10),
            }, new(20, 10, 0));

            SetObject(4, new SetPiece(new("TestPet", 4))
            {
                layer = layer,
                //visible = false,
                texture = new PalettizedTexture(3, 3,
                new byte[]
                {
                    0, 0, 0,
                    1, 1, 1,
                    2, 2, 2,
                }),
                palette = new PaletteAnimation(
                    new PaletteAnimation.PaletteFrame()
                    {
                        delay = 6,
                        palette = new ColorPalette(Red, Green, Blue)
                    },
                    new PaletteAnimation.PaletteFrame()
                    {
                        delay = 6,
                        palette = new ColorPalette(Cyan, Magenta, Yellow)
                    })
                {
                    play = true,
                    loop = true,
                }
            }, new(12, 20, 0));

            Sprite sprite = new(new("TestPet", 5))
            {
                layer = layer,
                //visible = false,
                drawTextureBounds = true
            };
            NameID anim = new("TestAnm", 0);
            sprite.animations.Add(anim, new(
                new TextureAnimation.TextureFrame()
                {
                    delay = 5,
                    texture = AlphaGradient()
                },
                new TextureAnimation.TextureFrame()
                {
                    delay = 5,
                    texture = Gradient()
                })
            {
                play = true,
                loop = true,
            });
            sprite.Animation = anim;
            SetObject(5, sprite, new(20, 20, 0));
        }
 
        protected override int ObjectCount => 6;

        protected override void PreFrame()
        {
            KeyboardMouseState kbm = window.KeyboardMouseState;

            VectorZ pos = Position;
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
            Position = pos;

            //pos = GetOffset(0);
            Object2D obj = (Object2D)GetObject(0);
            pos = obj.Position;
            switch (M.FindSign(kbm.Left, kbm.Right))
            {
                case M.Sign.Neg:
                    pos.x -= 1;
                    break;
                case M.Sign.Pos:
                    pos.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.Up, kbm.Down))
            {
                case M.Sign.Neg:
                    pos.y -= 1;
                    break;
                case M.Sign.Pos:
                    pos.y += 1;
                    break;
            }
            //SetOffset(0, pos);
            obj.Position = (Vector)pos;
        }
    }
}