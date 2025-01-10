using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;

using ProjectFox.GameEngine;
using C = ProjectFox.GameEngine.Debug.Console;
using ProjectFox.GameEngine.Audio;
using ProjectFox.GameEngine.Input;
using ProjectFox.GameEngine.Visuals;

using ProjectFox.Windows;

using static ProjectFox.TestBed.DebugColors;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    private const bool Cos = false;

    private const int Radius = 50, Steps = 400, Amplitude = 400, Half = Amplitude / 2;

    private const float Step = 0.0001f;

    private static readonly Vector Origin = new(0, 0), RotationOrigin = new(0, -Radius);
    
    public static void MathDrawTest()
    {
        Engine.Frequency = 10;
        Engine.uncapped = false;
        Screen.position = new(0, 0);
        Screen.Size = new(100, 100);
        Screen.Scale = 8f;
        Screen.OneToOne = true;
        Screen.FullScreen = false;
        Debug.DrawDebug = true;
        Debug.DebugAlpha = 128;

        window = new("Test Window", 100, 100, true);

        Scene scene = RotationTest();//BarycentricCoordinator.BarycentricCoordinates();
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        audioOutput = new();
        window.Start();
        audioOutput.Shutdown();
    }

    private static Scene RotationTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128),
        };

        new DebugController(window.kbdMouse)
        {
            Scene = scene,
            //printFrameInfo = true
        };

        new Sprite(new("Origin_", 0))
        {
            Scene = scene,
            Position = Origin,
            visible = false,
            drawPosition = true,
            positionColor = Red
        };

        new Sprite(new("RotOrgn", 0))
        {
            Scene = scene,
            Position = RotationOrigin,
            visible = false,
            drawPosition = true,
            positionColor = Magenta
        };

        new Sprite(new("RghtAng", 1))
        {
            Scene = scene,
            Position = RotationOrigin.RotateByRightAngles(1),
            visible = false,
            drawPosition = true,
            positionColor = Yellow
        };

        new Sprite(new("RghtAng", 2))
        {
            Scene = scene,
            Position = RotationOrigin.RotateByRightAngles(2),
            visible = false,
            drawPosition = true,
            positionColor = Yellow
        };

        new Sprite(new("RghtAng", 3))
        {
            Scene = scene,
            Position = RotationOrigin.RotateByRightAngles(3),
            visible = false,
            drawPosition = true,
            positionColor = Yellow
        };

        new Rotater(new("Rotater", 0), window,
            new Sprite(new("Pos2D", 0))
            {
                Scene = scene,
                Position = RotationOrigin,
                drawPosition = true,
                positionColor = Blue,
                visible = false
            },
            new Sprite(new("Obj2D", 0))
            {
                Scene = scene,
                Position = (Vector)(Vector.PointFromRotationOrigin(0.125f) * Radius),
                drawPosition = true,
                positionColor = Blue,
                visible = false
            },
            new DebugObject3D(new("Pos3D", 0), null)
            {
                Scene = scene,
                paused = true,
                Position = RotationOrigin,
                drawPosition = true,
                positionColor = Blue,
            },
            new DebugObject3D(new("Obj3D", 0), null)
            {
                Scene = scene,
                paused = true,
                Position = (VectorZ)(VectorZ.PointFromRotationOrigin(new(0f, 0f)) * Radius),
                drawPosition = true,
                positionColor = Blue,
            })
        {
            Scene = scene
        };

        return scene;
    }

    private static Scene SineDrawTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128),
        };

        VisualLayer layer = new(new("TstLayr", 0)) { Scene = scene };

        new DebugController(window.kbdMouse)
        {
            Scene = scene,
            //printFrameInfo = true
        };

        new SetPiece(new("SinDraw", 0))
        {
            Scene = scene,
            layer = layer,
            texture = SineTexture(),
            drawTextureBounds = true,
            boundsColor = Cyan
        };

        return scene;
    }

    private static Scene VectorInterpolateDrawTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128),
        };

        VisualLayer layer = new(new("TstLayr", 0)) { Scene = scene };

        new DebugController(window.kbdMouse)
        {
            Scene = scene,
            //printFrameInfo = true
        };

        new LineDrawer(new("LineDrw", 0), window)
        {
            Scene = scene,
            layer = layer,
            texture = new ColorTexture(0, 0),
            drawPosition = true,
            intersectingLines = true,
            positionColor = Blue
        };

        return scene;
    }

    private static Scene AudioChannelDraw()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = new(128, 128, 128),
        };

        AudioLayer layer = new(new("AudLayr", 0))
        {
            Scene = scene,
            audioChannel = new(new("AudChnl", 0)) { Scene = scene }
        };

        new DebugController(window.kbdMouse)
        {
            Scene = scene,
            printFrameInfo = true
        };

        new SampleSourceTest()
        {
            Scene = scene,
            channel = layer.audioChannel,
            audible = false,
            //loop = true,
            //mono = true,
            //swapStereo = true,
            //volume = 10f,
            //leftVolume = -1f,
            //rightVolume = 10f,
            //panning = 2f,
            waveShape = TestShape3,
        };

        return scene;
    }

    private static Scene Projection1DTest()
    {
        Scene scene = new(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Fill,
            bgColor = Grey,
        };

        VisualLayer layer = new(new("TstLayr", 0)) { Scene = scene };

        new DebugController(window.kbdMouse)
        {
            Scene = scene,
            //printFrameInfo = true
        };

        new OrthographicProjector1D(new("Proj1D", 0), window.kbdMouse)
        {
            Scene = scene,
            enabled = false
        };

        new PerspectiveProjector1D(new("Proj1D", 1), window.kbdMouse)
        {
            Scene = scene,
            //enabled = false
        };

        return scene;
    }

    private static ColorTexture SineTexture()
    {
        Color[] pixels = new Color[Steps * Amplitude];
        for (float f = 0; f <= 1f; f += Step)
        {
            SineCosine(f, out float sin, out float cos);

            int x = (int)(f * Steps), y = (int)(-(Cos ? cos : sin) * Half + Half);

            pixels[y * Steps + x] = Blue;
        }
        return new(Steps, Amplitude, pixels);
    }

    private sealed class Rotater : Object
    {
        public Rotater(NameID name, GameWindow window, Object2D pos2D, Object2D obj2D, Object3D pos3D, Object3D obj3D) : base(name)
        {
            this.window = window;
            this.pos2D = pos2D;
            this.obj2D = obj2D;
            this.pos3D = pos3D;
            this.obj3D = obj3D;
        }

        private readonly GameWindow window;

        private readonly Object2D pos2D, obj2D;

        private readonly Object3D pos3D, obj3D;

        private VectorF rotation = new(0f, 0f);

        protected override void PrePhysics()
        {
            if (window.kbdMouse.BackSlash.ChangedTrue)
            {
                rotation = new(0f, 0f);
                pos3D.Position = pos2D.Position = RotationOrigin;
            }

            if (window.kbdMouse.Space)
            {
                rotation.x += window.kbdMouse.Shift ? -0.01f : 0.01f;
                if (rotation.x >= 1f) rotation.x = 0f;

                if (window.kbdMouse.Shift) rotation.x = 0.375f;

                //Vector pos = (Vector)RotationOrigin.Rotate(rotation, /*window.kbdMouse.LeftMouse ? window.kbdMouse.mouse.Position :*/ default);

                VectorF posf = Vector.PointFromRotationOrigin(rotation.x) * Radius;
                Vector pos = (Vector)posf, obj2DPos = obj2D.Position;

                float angle = Vector.Angle(pos, obj2DPos), anglef = VectorF.Angle(posf, obj2DPos);

                Debug.Console.QueueMessage($"{rotation.x} => {pos.AngleFromRotationOrigin()} | {posf.AngleFromRotationOrigin()} => {angle} | {anglef} : {1f - angle} | {1f - anglef}");


                VectorZF poszf = VectorZ.PointFromRotationOrigin(rotation) * Radius;
                VectorZ posz = (VectorZ)poszf, obj3DPos = obj3D.Position;

                VectorF anglePair = VectorZ.Angle(posz, obj3DPos), anglefPair = VectorZF.Angle(poszf, obj3DPos);
                
                Debug.Console.QueueMessage($"{rotation} => {posz.AngleFromRotationOrigin()} | {poszf.AngleFromRotationOrigin()} => {anglePair} | {anglefPair}");


                bool leftMouse = window.kbdMouse.LeftMouse;
                Vector mousePos = window.kbdMouse.mouse;
                pos2D.Position = leftMouse ? pos + mousePos + Screen.position : pos;
                pos3D.Position = leftMouse ? posz + mousePos + Screen.position : posz;
            }

            if (window.kbdMouse.Z)
            {
                rotation.y += window.kbdMouse.Shift ? -0.01f : 0.01f;
                if (rotation.y >= 1f) rotation.y = 0f;

                VectorZF vzf = VectorZ.PointFromRotationOrigin(rotation), poszf = vzf * Radius;
                VectorZ posz = (VectorZ)poszf, obj3DPos = obj3D.Position;

                Debug.Console.QueueMessage($"{rotation} => {posz.z} => {posz.AngleFromRotationOrigin()} | {poszf.AngleFromRotationOrigin()}");

                pos3D.Position = window.kbdMouse.LeftMouse ? posz + window.kbdMouse.mouse.Position + Screen.position : posz;

                if (vzf.z >= 0f)
                {
                    pos3D.positionColor.R = vzf.z;
                    pos3D.positionColor.g = 0;
                }
                else
                {
                    pos3D.positionColor.r = 0;
                    pos3D.positionColor.G = -vzf.z;
                }
            }
        }
    }

    private sealed class LineDrawer : Sprite
    {
        public LineDrawer(NameID name, GameWindow window) : base(name) => this.window = window;

        private readonly GameWindow window;

        private readonly ColorPalette red = new(0, Black, Purple, Red), yellow = new(0, Black, Purple, Yellow), green = new(0, Black, Purple, Green);

        private Vector cursor = default;

        protected override void PrePhysics()
        {
            static string Concat<T>(int elementsPerLine, params T[] values)
            {
                string s = "";
                int i = 0;
                foreach (T value in values)
                {
                    s += value;
                    if (++i == elementsPerLine)
                    {
                        s += '\n';
                        i = 0;
                    }
                }
                return s;
            }

            if (window.kbdMouse.Enter.ChangedTrue)
            {
                bool xBig = cursor.x > cursor.y;
                int big = xBig ? cursor.x : cursor.y;
                decimal d = xBig ? cursor.y / (decimal)cursor.x : cursor.x / (decimal)cursor.y;

                Vector dist = default;
                Vector[] steps = Vector.StepInterpolate(cursor);
                Vector.SeparateAxes(out int[] x, out int[] y, steps);

                foreach (Vector step in steps) dist += step;

                VectorF f = dist / (float)steps.Length;
                int[] stepsF = Math.StepInterpolate(f.x == 1f ? f.y : f.x);

                C.QueueMessage($"{cursor} => {(big)} => {d} => {steps.Length} => {dist} => {f}\n{string.Join(',', xBig ? y : x)}\n{string.Join(',', stepsF)}\n{Concat(9, steps)}\n-----");
            }
            
            if (window.kbdMouse.LeftMouse && window.kbdMouse.mouse.PositionChanged)
            {
                cursor = window.kbdMouse.mouse + Screen.position;

                Vector
                    size = Screen.Size,
                    start = Origin, step = start, end = cursor, endAbs = end.Abs(), dist = end - start, distAbs = dist.Abs();

                horizontalFlipTexture = horizontalFlipOffset = dist.x < 0;
                verticalFlipTexture = verticalFlipOffset = dist.y < 0;
                
                bool yGreater = distAbs.y > distAbs.x;
                int min = yGreater ? distAbs.x : distAbs.y, max = yGreater ? distAbs.y : distAbs.x;
                float maxF = max, f1 = min / maxF, f2 = maxF / min;

                palette = green;

                byte[] pixels = new byte[size.x * size.y];

                for (int i = 0; i < max; i++)
                {
                    pixels[step.y * size.x + step.x] = 1;

                    if (yGreater) step.x = (int)(++step.y * f1);
                    else step.y = (int)(++step.x * f1);
                }

                Vector[] steps = Vector.StepInterpolate(endAbs, start);
                for (int i = 0, lastIndex = steps.Length - 1;
                    i < max; start += steps[Wrap(i++, 0, lastIndex)])
                {
                    int a = start.y * size.x + start.x;//rename
                    
                    if (a >= pixels.Length) palette = red;//this probably won't be true?

                    pixels[a] = (byte)(pixels[a] == 1 ? 2 : 3);
                }

                if (palette != red && start != endAbs) palette = yellow;

                texture = new PalettizedTexture(size, pixels);
            }
        }
    }

    private sealed class AudioLayer : VisualLayer
    {
        private const float MaxF = ushort.MaxValue;
        
        public AudioLayer(NameID name) : base(name) { }

        public AudioChannel audioChannel = null;

        protected override void Blend(Color[] pixels, Color[] reserved)
        {
            if (audioChannel != null)
            {
                Vector size = Screen.Size;
                Sample[] samples = audioChannel.GetSamples();
                for (int i = 0; i < samples.Length && i < size.x; i++)
                {
                    Sample sample = samples[i];
                    int l = (int)((sample.left + short.MaxValue) / MaxF * size.y) * size.x + i,//this can still result in -1
                        r = (int)((sample.right + short.MaxValue) / MaxF * size.y) * size.x + i;

                    if (l == r) pixels[l] = Magenta;
                    else
                    {
                        pixels[l] = Blue;
                        pixels[r] = Red;
                    }
                }
            }
            base.Blend(pixels, reserved);
        }
    }

    private abstract class Projector1D : CompoundObject
    {
        protected Projector1D(NameID name, KeyboardMouseDevice kbd, int objectCount) : base(name, 6 + objectCount)
        {
            this.kbd = kbd;

            SetObject(0, point2D = new SetPiece(new(0))
            {
                visible = false,
                drawPosition = true,
                positionColor = Red
            });
            SetObject(1, point1D = new SetPiece(new(1))
            {
                visible = false,
                drawPosition = true,
                positionColor = Magenta
            });
            SetObject(2, projector2D = new SetPiece(new(2))
            {
                visible = false,
                drawPosition = true,
                positionColor = Blue
            });
            SetObject(3, projEnd2D = new SetPiece(new(3))
            {
                visible = false,
                drawPosition = true,
                positionColor = Blue
            });
            SetObject(4, projector1D = new SetPiece(new(4))
            {
                visible = false,
                drawPosition = true,
                positionColor = Purple
            });
            SetObject(5, projEnd1D = new SetPiece(new(5))
            {
                visible = false,
                drawPosition = true,
                positionColor = Purple
            });
        }

        protected readonly KeyboardMouseDevice kbd;

        protected readonly Object2D point2D, point1D, projector2D, projEnd2D, projector1D, projEnd1D;

        protected float rotation;
    }

    private sealed class OrthographicProjector1D : Projector1D
    {
        public OrthographicProjector1D(NameID name, KeyboardMouseDevice kbd) : base(name, kbd, 0) => projector1D.drawPosition = false;

        private int size = 1;

        protected override void PrePhysics()
        {
            Vector point = point2D.Position, prevPoint = point,
                projPos = projector2D.Position, prevProjPos = projPos,
                projEnd = projEnd2D.Position, prevProjEnd = projEnd;

            if (kbd.LeftMouse) point = Screen.position + kbd.mouse;

            if (kbd.W) projPos.y--;
            else if (kbd.S) projPos.y++;
            if (kbd.A) projPos.x--;
            else if (kbd.D) projPos.x++;

            if (kbd.Up) size++;
            else if (kbd.Down) size--;

            if (kbd.Left) rotation -= 0.01f;
            else if (kbd.Right) rotation += 0.01f;

            rotation = FixFraction(rotation);
            projEnd = (Vector)(Vector.PointFromRotationOrigin(rotation) * size) + projPos;
            
            if (point != prevPoint || projPos != prevProjPos || projEnd != prevProjEnd)
            {
                float _1D = point.OrthographicProjection(projPos, size, rotation);

                //C.QueueMessage($"{point} : {projPos}, {size}, {rotation} => {_1D}");

                point2D.Position = point;
                projector2D.Position = projPos;
                projEnd2D.Position = projEnd;

                point1D.Position = new(projPos.x, (int)(_1D * size + projPos.y));
                projEnd1D.Position = new(projPos.x, projPos.y - size);
            }
        }
    }

    private sealed class PerspectiveProjector1D : Projector1D
    {
        private const float scale = 20f;

        public PerspectiveProjector1D(NameID name, KeyboardMouseDevice kbd) : base(name, kbd, 1)
        {
            SetObject(6, projAngle2D = new SetPiece(new(6))
            {
                visible = false,
                drawPosition = true,
                positionColor = Green
            });
        }

        private readonly Object2D projAngle2D;

        private float angle = 0f;

        protected override void PrePhysics()
        {
            Vector point = point2D.Position, prevPoint = point,
                projPos = projector2D.Position, prevProjPos = projPos;

            float a = angle, r = rotation;

            if (kbd.LeftMouse) point = Screen.position + kbd.mouse;

            if (kbd.W) projPos.y--;
            else if (kbd.S) projPos.y++;
            if (kbd.A) projPos.x--;
            else if (kbd.D) projPos.x++;

            if (kbd.Up) a += 0.01f;
            else if (kbd.Down) a -= 0.01f;
            
            if (kbd.Left) r -= 0.01f;
            else if (kbd.Right) r += 0.01f;
            
            if (point != prevPoint || projPos != prevProjPos || a != angle || r != rotation)
            {
                angle = a;
                rotation = r;

                float _1D = point.PerspectiveProjection((VectorF)projPos, angle, rotation);

                C.QueueMessage($"{point} : {projPos}, {angle}, {rotation} => {_1D}");

                point2D.Position = point;
                projector2D.Position = projPos;
                projEnd2D.Position = (Vector)(Vector.PointFromRotationOrigin(rotation) * scale + projPos);
                projAngle2D.Position = (Vector)(Vector.PointFromRotationOrigin(rotation + angle) * scale + projPos);

                float size = angle * scale * 10f;
                projector1D.Position = projPos = new(projPos.x - 10, projPos.y + 40);
                projEnd1D.Position = new((int)(projPos.x + size), projPos.y);
                point1D.Position = new(projPos.x + (int)(_1D * size), projPos.y);
            }
        }
    }

    private sealed class BarycentricCoordinator : CompoundObject
    {
        public static Scene BarycentricCoordinates()
        {
            Scene scene = new(new("TestScn", 0))
            {
                ClearMode = Screen.ClearModes.Fill,
                bgColor = Grey
            };

            VisualLayer layer = new(new("Layer", 0)) { Scene = scene };

            new DebugController(window.kbdMouse)
            {
                Scene = scene,
                //printFrameInfo = true
            };

            new MouseDrawer(window) { Scene = scene };

            new BarycentricCoordinator(window.kbdMouse, layer) { Scene = scene };

            return scene;
        }

        public BarycentricCoordinator(KeyboardMouseDevice kbm, VisualLayer layer) : base(new("BaryCnt", 0), 4)
        {
            this.kbm = kbm;
            SetObject(0, point1 = new Sprite(new("Point__", 0))
            {
                drawPosition = true,
                visible = false,
            });
            SetObject(1, point2 = new Sprite(new("Point__", 1))
            {
                drawPosition = true,
                visible = false,
            });
            SetObject(2, triangle1 = new(new("Triangl", 0))
            {
                layer = layer,
                palette = GraphicsPalette(),
                rasterizationMode = GraphicsObject.RasterizationMode.Wireframe,
                Triangle = new(0, 10, 10, 0, 20, 10)
            });
            SetObject(3, triangle2 = new(new("Triangl", 1))
            {
                layer = layer,
                palette = triangle1.palette,
                rasterizationMode = GraphicsObject.RasterizationMode.Wireframe,
                Triangle = new(25, 10, 40, 0, 50, 20)
            });
        }

        private readonly KeyboardMouseDevice kbm;

        private readonly Object2D point1, point2;

        private readonly GraphicsTriangle triangle1, triangle2;

        protected override void PrePhysics()
        {
            if (kbm.LeftMouse && kbm.mouse.PositionChanged)
            {
                Vector p1 = kbm.mouse + Screen.position;

                point1.Position = p1;

                Triangle t1 = triangle1.Triangle, t2 = triangle2.Triangle;

                int distBCy = t1.b.y - t1.c.y,
                    distCBx = t1.c.x - t1.b.x,
                    distACx = t1.a.x - t1.c.x,
                    distACy = t1.a.y - t1.c.y,
                    distCAy = t1.c.y - t1.a.y,
                    distPCx =   p1.x - t1.c.x,
                    distPCy =   p1.y - t1.c.y;

                float divisor = (distBCy * distACx) + (distCBx * distACy);

                VectorZF b = new(
                    ((distBCy * distPCx) + (distCBx * distPCy)) / divisor,
                    ((distCAy * distPCx) + (distACx * distPCy)) / divisor, 0);
                b.z = 1f - b.x - b.y;
                Debug.Console.QueueMessage(b);

                VectorF p2 = new(
                    (b.x * t2.a.x) + (b.y * t2.b.x) + (b.z * t2.c.x),
                    (b.x * t2.a.y) + (b.y * t2.b.y) + (b.z * t2.c.y));

                point2.Position = (Vector)p2;
            }
        }
    }
}