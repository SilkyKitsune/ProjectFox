using System;

using ProjectFox.CoreEngine.Math;
using M = ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using ProjectFox.GameEngine.Input;
using ProjectFox.GameEngine.Physics;
using ProjectFox.GameEngine.Visuals;

using ProjectFox.Windows;

using ProjectFox.NAudio;

using static ProjectFox.TestBed.DebugColors;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    private static GameWindow window;
    private static GameAudioOutput audioOutput;
    private static DebugController debugController;

    public static void MainTest()
    {
        Engine.Frequency = 15;
        Screen.position = new(0, 0);
        Screen.Size = new(100, 100);
        Screen.Scale = 8f;
        Screen.OneToOne = true;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;

        window = new("Test Window", 100, 100, true);
        
        Scene scene = PositionDrawTest();
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

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
            new PhysicsRectangle(new("ScnRect", 0))
            {
                Scene = scene,
                enabled = false,
                size = new(10, 10),
                Position = new(20, 20),
                drawShape = true
            })
        {
            enabled = false,
            //pauseWalks = true,
            Scene = scene,
            drawPosition = true,
            intersectingLines = true,
        };

        new DebugObject3D(new("Obj3D", 0), window.kbdMouse)
        {
            Scene = scene,
            //enabled = false,
            drawPosition = true,
            intersectingLines = true,
            depthColor = true,
            positionColor = Blue
        };

        new DebugCompoundObject(new("TestCmp", 0),
            new(new("VslLayr", 0)) { Scene = scene })
        {
            Scene = scene,
            enabled = false
        };

        return scene;
    }

    private sealed class DebugObject2D : Object2D
    {
        public DebugObject2D(NameID name, PhysicsRectangle rectangle) : base(name) => this.rectangle = rectangle;

        private readonly PhysicsRectangle rectangle;

        protected override void PrePhysics()
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

        protected override void PreDraw()
        {
            Rectangle r = new(rectangle.Position + rectangle.shapeOffset, rectangle.size);
            rectangle.shapeColor = r.Overlapping(Position) ? Green : Blue;

            if (window.kbdMouse.Enter.ChangedTrue) Scene.paused = !Scene.paused;
        }
    }

    private sealed class DebugObject3D : Object3D
    {
        public DebugObject3D(NameID name, KeyboardMouseDevice kbm) : base(name) => this.kbm = kbm;

        private readonly KeyboardMouseDevice kbm;

        protected override void PrePhysics()
        {
            VectorZ pos = Position;
            switch (M.FindSign(kbm.J, kbm.L))
            {
                case M.Sign.Neg:
                    pos.x -= 1;
                    break;
                case M.Sign.Pos:
                    pos.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.I, kbm.K))
            {
                case M.Sign.Neg:
                    pos.y -= 1;
                    break;
                case M.Sign.Pos:
                    pos.y += 1;
                    break;
            }
            switch (M.FindSign(kbm.Semicolon, kbm.Apostrophe))
            {
                case M.Sign.Neg:
                    pos.z -= 1;
                    break;
                case M.Sign.Pos:
                    pos.z += 1;
                    break;
            }
            Position = pos;
        }
    }

    private sealed class DebugCompoundObject : CompoundObject
    {
        public DebugCompoundObject(NameID name, VisualLayer layer) : base(name, 6)
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

            SetObject(2, new DebugObject3D(new("TestPet", 2), null)
            {
                paused = true,
            }, new(0, 0, 0));

            SetObject(3, new PhysicsRectangle(new("TestPet", 3))
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
                drawTextureBounds = true,
                animation = new(
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
                }
            };
            SetObject(5, sprite, new(20, 20, 0));
        }

        protected override void PrePhysics()
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