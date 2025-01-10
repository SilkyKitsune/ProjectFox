using System;

using ProjectFox.CoreEngine.Math;
using M = ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using static ProjectFox.GameEngine.Debug.Console;
using ProjectFox.GameEngine.Physics;
using ProjectFox.GameEngine.Visuals;
using ProjectFox.GameEngine.Input;

using ProjectFox.Windows;

using static ProjectFox.TestBed.DebugNames;
using static ProjectFox.TestBed.DebugColors;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    public static PhysicsSpace space = new(new("TestSpc", 0));

    public static void PhysicsTest()
    {
        Engine.Frequency = 15;
        Engine.uncapped = false;
        Screen.position = new(0, 0);
        Screen.Size = new(100, 100);
        Screen.Scale = 8f;
        Screen.OneToOne = true;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;
        //Debug.DebugAlpha = 128;

        window = new(WindowName, 50, 50, true);

        Scene scene = ScannerTest();
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        window.Start();
    }

    private static Scene IntersectionTest()
    {
        Scene scene = new(TestScene)
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = Grey
        };

        new IntersectionsController(new("IntrCtl", 0), scene) { Scene = scene };

        return scene;
    }

    private static Scene ScannerTest()
    {
        Scene scene = new(TestScene)
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = Grey
        };

        new DebugScannerRectangle(new(TestRect, 1))
        {
            Scene = scene,
            Space = space,
            Position = new(50, 10)
        };
        new DebugScannerRectangle(new(TestRect, 2))
        {
            Scene = scene,
            Space = space,
            Position = new(50, 50)
        };
        new DebugScannerRectangle(new(TestRect, 0))
        {
            Scene = scene,
            Space = space,

            paused = false,
            drawPosition = true,
            size = new(10, 10),

            scan = true,
            scanThoroughly = true,
            scanOwnSpace = true,
            scanRectangles = true,

            //test compound cases
            //ScanForEqual = true,
            //ScanForIntersecting = true,
            ScanForEnveloping = true,//this one doesn't work
            //ScanForWithin = true,//doesn't work either
            //ScanForTouching = true,
        };
        new DebugScannerRectangle(new(TestRect, 3))
        {
            Scene = scene,
            Space = space,
            Position = new(20, 60),
            shapeColor = new(0, 255, 0, 128)
        };

        return scene;
    }

    private static Scene KinematicTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = Grey
        };

        new DebugController(window.kbdMouse)
        {
            Scene = scene,
            //printFrameInfo = true,
        };

        new DebugScannerRectangle(new(TestRect, 0))
        {
            Scene = scene,
            Space = space,
            Position = new(30, 20),
            size = new(30, 30),
            //soft = true,
            collideWithTop = true,
            collideWithBottom = true,
            collideWithLeft = true,
            collideWithRight = true,
            drawShape = true,
        };
        new DebugScannerRectangle(new(TestRect, 1))
        {
            Scene = scene,
            Space = space,
            Position = new(50, 20),
            //shapeEnabled = false,
            size = new(20, 20),
            //soft = true,
            collideWithTop = true,
            collideWithBottom = true,
            collideWithLeft = true,
            collideWithRight = true,
            drawShape = true,
            shapeColor = Cyan
        };

        new DebugKinematicRectangle(new("TestKnm", 0))
        {
            Scene = scene,
            Space = space,
            size = new(5, 5),
            //shapeOffset = new(10, 10),
            //applyVelocity = true,
            velocity = new(3, 2),
            collideWithRectangles = true,
            keepMoving = true,
            preferY = true,
            scanOwnSpace = true,
            drawShape = true,
            shapeColor = Magenta,
        };

        return scene;
    }

    private sealed class IntersectionsController : GameEngine.Object
    {
        public IntersectionsController(NameID name, Scene scene) : base(name) => scene.AddObjects(red, blue, green);

        private readonly PhysicsRectangle
            red = new(new(TestRect, 0))
            {
                paused = true,
                drawShape = true,
                shapeColor = Red
            },
            blue = new(new(TestRect, 1))
            {
                paused = true,
                drawShape = true,
                shapeColor = Blue
            },
            green = new(new(TestRect, 2))
            {
                paused = true,
                drawShape = true,
                shapeColor = Green
            };

        protected override void PrePhysics()
        {
            KeyboardMouseState kbm = window.KeyboardMouseState;

            Vector pos = red.Position;
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
            red.Position = pos;

            switch (M.FindSign(kbm.F, kbm.H))
            {
                case M.Sign.Neg:
                    red.size.x -= 1;
                    break;
                case M.Sign.Pos:
                    red.size.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.T, kbm.G))
            {
                case M.Sign.Neg:
                    red.size.y -= 1;
                    break;
                case M.Sign.Pos:
                    red.size.y += 1;
                    break;
            }

            pos = blue.Position;
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
            blue.Position = pos;

            switch (M.FindSign(kbm.Left, kbm.Right))
            {
                case M.Sign.Neg:
                    blue.size.x -= 1;
                    break;
                case M.Sign.Pos:
                    blue.size.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.Up, kbm.Down))
            {
                case M.Sign.Neg:
                    blue.size.y -= 1;
                    break;
                case M.Sign.Pos:
                    blue.size.y += 1;
                    break;
            }
        }

        protected override void PreDraw()
        {
            Rectangle r = new(red.Position, red.size), b = new(blue.Position, blue.size), g = r.IntersectionBounds(b);

            green.shapeColor = Green;

            if (g.size.x < 0)
            {
                g.position.x += g.size.x;
                g.size.x = -g.size.x;
                green.shapeColor = Yellow;
            }
            if (g.size.y < 0)
            {
                g.position.y += g.size.y;
                g.size.y = -g.size.y;
                green.shapeColor = Yellow;
            }

            green.Position = g.position;
            green.size = g.size;
        }
    }

    private sealed class DebugScannerRectangle : PhysicsRectangle
    {
        internal DebugScannerRectangle(NameID name) : base(name, 
            (scanner, shape) =>
            {
                //QueueMessage(shape.Name);
            })
        {
            paused = true;
            drawShape = true;
            size = new(20, 20);
        }
        
        protected override void PrePhysics()
        {
            KeyboardMouseState kbm = window.KeyboardMouseState;
            KeyboardMouseDevice kbm_ = window.kbdMouse;

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

            switch (M.FindSign(kbm.J, kbm.L))
            {
                case M.Sign.Neg:
                    size.x -= 1;
                    break;
                case M.Sign.Pos:
                    size.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.I, kbm.K))
            {
                case M.Sign.Neg:
                    size.y -= 1;
                    break;
                case M.Sign.Pos:
                    size.y += 1;
                    break;
            }

            switch (M.FindSign(kbm.Left, kbm.Right))
            {
                case M.Sign.Neg:
                    shapeOffset.x -= 1;
                    break;
                case M.Sign.Pos:
                    shapeOffset.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.Up, kbm.Down))
            {
                case M.Sign.Neg:
                    shapeOffset.y -= 1;
                    break;
                case M.Sign.Pos:
                    shapeOffset.y += 1;
                    break;
            }

            if (kbm_.F.ChangedTrue) flipOffsetOnPixel = !flipOffsetOnPixel;
            if (kbm_.H.ChangedTrue) horizontalFlipOffset = !horizontalFlipOffset;
            if (kbm_.V.ChangedTrue) verticalFlipOffset = !verticalFlipOffset;
        }

        protected override void PreDraw()
        {
            bool eql = Equal, intr = Intersecting, env = Enveloping, wthn = Within, tch = Touching;

            shapeColor = Black;

            if (tch) shapeColor = Cyan;
            if (intr) shapeColor = Red;
            if (env) shapeColor = Yellow;
            if (wthn) shapeColor = Magenta;
            if (eql) shapeColor = Green;

            if (eql && intr && env && wthn && tch) shapeColor = White;
        }
    }

    private sealed class DebugKinematicRectangle : PhysicsRectangle
    {
        public DebugKinematicRectangle(NameID name) : base(name) { }

        protected override void PrePhysics()
        {
            KeyboardMouseState kbm = window.KeyboardMouseState;

            switch (M.FindSign(kbm.A, kbm.D))
            {
                case M.Sign.Neg:
                    velocity.x -= 1;
                    break;
                case M.Sign.Pos:
                    velocity.x += 1;
                    break;
            }
            switch (M.FindSign(kbm.W, kbm.S))
            {
                case M.Sign.Neg:
                    velocity.y -= 1;
                    break;
                case M.Sign.Pos:
                    velocity.y += 1;
                    break;
            }

            if (kbm.Zero) velocity = default;

            if (window.kbdMouse.LeftMouse) Position = window.kbdMouse.mouse;
            
            applyVelocity = kbm.Space;

            QueueMessage($"{Position}, {velocity}");
        }
    }
}