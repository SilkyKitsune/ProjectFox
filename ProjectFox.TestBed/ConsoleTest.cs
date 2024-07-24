using System.Threading;

using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Collections.Strings;

using ProjectFox.GameEngine;
using static ProjectFox.GameEngine.Engine;
using static ProjectFox.GameEngine.Debug;
using static ProjectFox.GameEngine.Debug.Console;

using ProjectFox.GameEngine.Physics;
//using M = ProjectFox.GameEngine.Physics.PhysicsShape.ScanModes;

using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.TestBed;

public static partial class GameEngineTest
{
    #region Main
    public static void ConsoleMainTest()
    {
        #region console
        QueueMessage("test");

        ReadMode = true;
        System.Diagnostics.Debug.WriteLine($"readmode {ReadMode}");
        Thread.Sleep(5000);
        ReadMode = false;
        System.Diagnostics.Debug.WriteLine($"readmode {ReadMode}");

        GetInputs(out string[] messages, out int length);
        QueueMessage(length);
        QueueMessage(messages.Length);
        QueueMessage(string.Join(',', messages));

        GetInputs(out messages, out length);
        QueueMessage(length);
        QueueMessage(messages.Length);
        QueueMessage(string.Join(',', messages));
        #endregion

        #region name
        QueueMessage($"---{nameof(NameID)}---");

        NameID name = new("TstName", 0);
        QueueMessage(name);
        QueueMessage(name.GetHashCode());
        QueueMessage(name.Equals(new("TstName", 0)));
        QueueMessage(name.Equals(false));

        name = new(0uL);
        QueueMessage(name);
        QueueMessage(name.GetHashCode());
        QueueMessage(name.Equals(new(0uL)));
        QueueMessage(name.Equals(false));
        #endregion

        #region engine
        QueueMessage($"---{nameof(Engine)}---");

        FreqChanged += () => { QueueMessage("Frequency was changed"); };

        QueueMessage(Frequency);
        QueueMessage(MillisecondsPerFrame);
        
        Frequency = 60;
        QueueMessage(Frequency);
        QueueMessage(MillisecondsPerFrame);

        Frequency = 500;
        QueueMessage(Frequency);
        QueueMessage(MillisecondsPerFrame);

        Frequency = 0;
        QueueMessage(Frequency);
        QueueMessage(MillisecondsPerFrame);

        Frequency = 50;
        QueueMessage(Frequency);
        QueueMessage(MillisecondsPerFrame);

        QueueMessage(FrameCount);
        QueueMessage(TimeOfLastFrame);

        Frame();
        QueueMessage(FrameCount);
        QueueMessage(TimeOfLastFrame);

        Frame(40);
        QueueMessage(FrameCount);
        QueueMessage(TimeOfLastFrame);

        Frame(10);
        QueueMessage(FrameCount);
        QueueMessage(TimeOfLastFrame);

        TestArray.Insert(0, 1, 0);
        _ = TestArray.RemoveRange(0, 1);
        _ = TestTable[name];

        string s = null;
        TestArrayRef.Add(s);
        TestArrayRef.Add(null, null);
        TestArrayRef.Insert(10, s);
        TestTableRef.Add(name, null);
        TestTableRef.Add(name, string.Empty);
        TestTableRef[name] = null;
        #endregion

        #region scene
        QueueMessage($"---{nameof(Scene)}---");

        Scene scene = new Scene(new("TestScn", 0))
        {
            ClearMode = Screen.ClearModes.Clear,
            bgColor = 0x808080FF
        };
        QueueMessage(scene.Name);
        QueueMessage(scene.ClearMode);
        QueueMessage(scene.bgColor);
        QueueMessage(scene.BGOffset);
        scene.ClearMode = (Screen.ClearModes)(-1);
        #endregion

        #region scenelist
        QueueMessage($"---{nameof(SceneList)}---");

        QueueMessage(SceneList.ActiveScene);
        QueueMessage(SceneList.Contains(scene.Name));
        QueueMessage(SceneList.TotalScenes);

        SceneList.ActiveScene = scene.Name;
        SceneList.Remove(scene.Name);
        SceneList.Add(null);

        SceneList.Add(scene);
        QueueMessage(SceneList.ActiveScene);
        QueueMessage(SceneList.Contains(scene.Name));
        QueueMessage(SceneList.TotalScenes);

        SceneList.ActiveScene = scene.Name;
        QueueMessage(SceneList.ActiveScene);
        QueueMessage(SceneList.Contains(scene.Name));
        QueueMessage(SceneList.TotalScenes);

        SceneList.Remove(scene.Name);
        QueueMessage(SceneList.ActiveScene);
        QueueMessage(SceneList.Contains(scene.Name));
        QueueMessage(SceneList.TotalScenes);

        SceneList.Add(scene);
        QueueMessage(SceneList.ActiveScene);
        QueueMessage(SceneList.Contains(scene.Name));
        QueueMessage(SceneList.TotalScenes);

        SceneList.Clear();
        QueueMessage(SceneList.ActiveScene);
        QueueMessage(SceneList.Contains(scene.Name));
        QueueMessage(SceneList.TotalScenes);

        SceneList.Add(scene);
        SceneList.ActiveScene = scene.Name;
        Frame();
        QueueMessage(FrameCount);
        #endregion

        #region object
        QueueMessage($"---{nameof(Object)}---");

        TestObject obj = new TestObject(new("TestObj", 0));
        QueueMessage(obj.Scene);
        QueueMessage(obj.Owner);
        scene.AddObject(null);
        scene.AddObject(obj);
        scene.AddObject(obj);
        Frame();
        QueueMessage(FrameCount);

        TestObject2D obj2D = new TestObject2D(new("Test2D", 0));
        TestObject3D obj3D = new TestObject3D(new("Test3D", 0));
        scene.AddObjects(null);
        scene.AddObjects();
        scene.AddObjects(obj2D, obj3D, null);
        scene.AddObjects(obj2D);
        Frame();
        QueueMessage(FrameCount);

        TestCompoundObject compobj = new TestCompoundObject(new("TestCom", 0));
        scene.AddObject(compobj.pet);
        scene.AddObjects(compobj.pet);
        scene.AddObject(compobj);
        Frame();
        QueueMessage(FrameCount);

        scene.RemoveObject(new(0uL));
        scene.RemoveObject(compobj.Name);
        Frame();
        QueueMessage(FrameCount);

        scene.RemoveObjects(null);
        scene.RemoveObjects();
        scene.RemoveObjects(obj2D.Name, obj3D.Name, new(0uL));
        Frame();
        QueueMessage(FrameCount);

        compobj.Position = new(1, 2, 3);
        QueueMessage(compobj.Position);
        QueueMessage(compobj.Offset(0));

        compobj.pet.Position = new(1, 2);
        QueueMessage(compobj.pet.Position);
        QueueMessage(compobj.Offset(0));

        //followindex
        #endregion

        #region statemachine
        QueueMessage($"---{nameof(StateMachine)}---");

        TestStateMachine machine = new();

        QueueMessage(machine.State);
        machine.CurrentStatePreFrame();
        machine.CurrentStatePostFrame();

        machine.State = TestStateMachine.States.TestStateTwo;

        QueueMessage(machine.State);
        machine.CurrentStatePreFrame();
        machine.CurrentStatePostFrame();
        #endregion

        Thread.Sleep(5000);
        Shutdown();
    }

    #region object
    private sealed class TestObject : Object
    {
        internal TestObject(NameID name) : base(name) { }

        protected override void PreFrame()
        {
            QueueMessage("TestObject.PreFrame()");
            QueueMessage(Name);
            QueueMessage(Scene?.Name);
            QueueMessage(Owner?.Name);
            QueueMessage(enabled);
            QueueMessage(paused);
            QueueMessage(pauseWalks);
        }

        protected override void PostFrame() => QueueMessage("TestObject.PostFrame()");
    }

    private sealed class TestObject2D : Object2D
    {
        internal TestObject2D(NameID name) : base(name) { }

        protected override void PreFrame()
        {
            QueueMessage("TestObject2D.PreFrame()");
            QueueMessage(Name);
            QueueMessage(Scene?.Name);
            QueueMessage(Owner?.Name);
            QueueMessage(enabled);
            QueueMessage(paused);
            QueueMessage(pauseWalks);
            QueueMessage(Position);
        }

        protected override void PostFrame() => QueueMessage("TestObject2D.PostFrame()");
    }

    private sealed class TestObject3D : Object3D
    {
        internal TestObject3D(NameID name) : base(name) { }

        protected override void PreFrame()
        {
            QueueMessage("TestObject3D.PreFrame()");
            QueueMessage(Name);
            QueueMessage(Scene?.Name);
            QueueMessage(Owner?.Name);
            QueueMessage(enabled);
            QueueMessage(paused);
            QueueMessage(pauseWalks);
            QueueMessage(Position);
        }

        protected override void PostFrame() => QueueMessage("TestObject3D.PostFrame()");
    }

    private sealed class TestCompoundObject : CompoundObject
    {
        internal TestCompoundObject(NameID name) : base(name) => SetObject(0, pet);

        internal TestObject2D pet = new TestObject2D(new("TestPet", 0));

        protected override int ObjectCount => 1;

        protected override void PreFrame()
        {
            QueueMessage("TestCompoundObject.PreFrame()");
            QueueMessage(Name);
            QueueMessage(Scene?.Name);
            QueueMessage(Owner?.Name);
            QueueMessage(enabled);
            QueueMessage(paused);
            QueueMessage(pauseWalks);
            QueueMessage(Position);
        }

        protected override void PostFrame() => QueueMessage("TestCompoundObject.PostFrame()");

        public VectorZ Offset(int index) => GetOffset(index);
    }
    #endregion

    private sealed class TestStateMachine : StateMachine
    {
        internal enum States
        {
            TestStateOne,
            TestStateTwo
        }

        internal TestStateMachine()
        {
            states[(int)States.TestStateOne] = new TestStateOne();
            states[(int)States.TestStateTwo] = new TestStateTwo();
            currentState = (int)States.TestStateOne;
            CurrentStateEnter();
        }

        protected override int StateCount => 2;

        internal States State
        {
            get => (States)currentState;
            set
            {
                CurrentStateExit();
                currentState = (int)value;
                CurrentStateEnter();
            }
        }

        private sealed class TestStateOne : State
        {
            public override void Enter() => QueueMessage("TestStateOne.Enter()");
            public void PreFrame() => QueueMessage("TestStateOne.PreFrame()");
            public void PostFrame() => QueueMessage("TestStateOne.PostFrame()");
            public void Exit() => QueueMessage("TestStateOne.Exit()");
        }

        private sealed class TestStateTwo : State
        {
            public void Enter() => QueueMessage("TestStateTwo.Enter()");
            public void PreFrame() => QueueMessage("TestStateTwo.PreFrame()");
            public void PostFrame() => QueueMessage("TestStateTwo.PostFrame()");
            public void Exit() => QueueMessage("TestStateTwo.Exit()");
        }
    }
    #endregion

    #region Physics
    public static void ConsolePhysicsTest()
    {
        #region ScanModes
        /*QueueMessage($"{M.None} : {(short)M.None} : {ToBinString((short)M.None, false, '|', '_', true)}");
        QueueMessage($"{M.Equal} : {(short)M.Equal} : {ToBinString((short)M.Equal, false, '|', '_', true)}");
        QueueMessage($"{M.Intersecting} : {(short)M.Intersecting} : {ToBinString((short)M.Intersecting, false, '|', '_', true)}");
        QueueMessage($"{M.Enveloping} : {(short)M.Enveloping} : {ToBinString((short)M.Enveloping, false, '|', '_', true)}");
        QueueMessage($"{M.Within} : {(short)M.Within} : {ToBinString((short)M.Within, false, '|', '_', true)}");
        QueueMessage($"{M.Touching} : {(short)M.Touching} : {ToBinString((short)M.Touching, false, '|', '_', true)}");

        QueueMessage($"{M.IntersectingEqual} : {(short)M.IntersectingEqual} : {ToBinString((short)M.IntersectingEqual, false, '|', '_', true)}");
        QueueMessage($"{M.EnvelopingEqual} : {(short)M.EnvelopingEqual} : {ToBinString((short)M.EnvelopingEqual, false, '|', '_', true)}");
        QueueMessage($"{M.WithinEqual} : {(short)M.WithinEqual} : {ToBinString((short)M.WithinEqual, false, '|', '_', true)}");
        QueueMessage($"{M.TouchingEqual} : {(short)M.TouchingEqual} : {ToBinString((short)M.TouchingEqual, false, '|', '_', true)}");

        QueueMessage($"{M.a} : {(short)M.a} : {ToBinString((short)M.a, false, '|', '_', true)}");
        QueueMessage($"{M.b} : {(short)M.b} : {ToBinString((short)M.b, false, '|', '_', true)}");
        QueueMessage($"{M.c} : {(short)M.c} : {ToBinString((short)M.c, false, '|', '_', true)}");

        QueueMessage($"{M.Overlapping} : {(short)M.Overlapping} : {ToBinString((short)M.Overlapping, false, '|', '_', true)}");
        QueueMessage($"{M.d} : {(short)M.d} : {ToBinString((short)M.d, false, '|', '_', true)}");

        QueueMessage($"{M.e} : {(short)M.e} : {ToBinString((short)M.e, false, '|', '_', true)}");

        QueueMessage($"{M.f} : {(short)M.f} : {ToBinString((short)M.f, false, '|', '_', true)}");
        QueueMessage($"{M.g} : {(short)M.g} : {ToBinString((short)M.g, false, '|', '_', true)}");

        QueueMessage($"{M.h} : {(short)M.h} : {ToBinString((short)M.h, false, '|', '_', true)}");

        QueueMessage($"{M.i} : {(short)M.i} : {ToBinString((short)M.i, false, '|', '_', true)}");

        QueueMessage($"{M.j} : {(short)M.j} : {ToBinString((short)M.j, false, '|', '_', true)}");
        QueueMessage($"{M.k} : {(short)M.k} : {ToBinString((short)M.k, false, '|', '_', true)}");
        QueueMessage($"{M.l} : {(short)M.l} : {ToBinString((short)M.l, false, '|', '_', true)}");

        QueueMessage($"{M.m} : {(short)M.m} : {ToBinString((short)M.m, false, '|', '_', true)}");
        QueueMessage($"{M.n} : {(short)M.n} : {ToBinString((short)M.n, false, '|', '_', true)}");

        QueueMessage($"{M.o} : {(short)M.o} : {ToBinString((short)M.o, false, '|', '_', true)}");

        QueueMessage($"{M.p} : {(short)M.p} : {ToBinString((short)M.p, false, '|', '_', true)}");

        QueueMessage($"{M.q} : {(short)M.q} : {ToBinString((short)M.q, false, '|', '_', true)}");
        QueueMessage($"{M.r} : {(short)M.r} : {ToBinString((short)M.r, false, '|', '_', true)}");

        QueueMessage($"{M.s} : {(short)M.s} : {ToBinString((short)M.s, false, '|', '_', true)}");

        QueueMessage($"{M.t} : {(short)M.t} : {ToBinString((short)M.t, false, '|', '_', true)}");

        QueueMessage($"{M.u} : {(short)M.u} : {ToBinString((short)M.u, false, '|', '_', true)}");*/
        #endregion

        Scene scene = new(new("TestScn", 0));
        Engine.SceneList.Add(scene);
        Engine.SceneList.ActiveScene = scene.Name;

        #region PhysicsSpace
        PhysicsSpace mainSpace = new PhysicsSpace(new("TestSpc", 0));
        PhysicsSpace scanSpace = new PhysicsSpace(new("TestSpc", 1));

        mainSpace.AddSpaceToScan(null);
        mainSpace.AddSpaceToScan(mainSpace);
        mainSpace.AddSpaceToScan(scanSpace);
        mainSpace.AddSpaceToScan(scanSpace);

        mainSpace.RemoveSpaceToScan(scanSpace.Name);
        mainSpace.RemoveSpaceToScan(scanSpace.Name);

        mainSpace.AddSpacesToScan(null);
        mainSpace.AddSpacesToScan();
        mainSpace.AddSpacesToScan(null, mainSpace, scanSpace, scanSpace);

        mainSpace.RemoveSpacesToScan(null);
        mainSpace.RemoveSpacesToScan();
        mainSpace.RemoveSpacesToScan(scanSpace.Name, scanSpace.Name);

        PhysicsRectangle scanner = new(new("Scanner", 0));
        QueueMessage(scanner.Space?.Name);

        mainSpace.AddRectangle(null);
        mainSpace.AddRectangle(scanner);
        mainSpace.AddRectangle(scanner);
        QueueMessage(scanner.Space?.Name);

        mainSpace.RemoveRectangle(scanner.Name);
        mainSpace.RemoveRectangle(scanner.Name);
        QueueMessage(scanner.Space?.Name);

        mainSpace.AddRectangles(null);
        mainSpace.AddRectangles();
        mainSpace.AddRectangles(null, scanner, scanner);
        QueueMessage(scanner.Space?.Name);

        mainSpace.RemoveRectangles(null);
        mainSpace.RemoveRectangles();
        mainSpace.RemoveRectangles(scanner.Name, scanner.Name);
        QueueMessage(scanner.Space?.Name);
        #endregion

        #region ScannerRectangle
        mainSpace.AddSpaceToScan(scanSpace);

        scanner = new(new("Scanner", 0), DetectedTest)
        {
            Scene = scene,
            Space = mainSpace,
            scan = true,
            scanRectangles = true,
            size = new(10, 10)
        };
        PhysicsRectangle identical = new(new("Idnticl", 0))
        {
            Scene = scene,
            Space = scanSpace,
            size = new(10, 10)
        };
        PhysicsRectangle smaller = new(new("Smaller", 0))
        {
            Scene = scene,
            Space = scanSpace,
            size = new(5, 5)
        };
        PhysicsRectangle bigger = new(new("Bigger", 0))
        {
            Scene = scene,
            Space = scanSpace,
            size = new(20, 20)
        };
        
        ScannerTest(scanner);

        scanner.scanThoroughly = true;
        ScannerTest(scanner);

        identical.Scene = null;
        ScannerTest(scanner);
        identical.Scene = scene;

        identical.Space = null;
        ScannerTest(scanner);
        identical.Space = scanSpace;

        scanner.scanThoroughly = false;
        identical.Position = new(10, 0);
        bigger.Position = new(0, 10);
        smaller.Position = new(10, 10);
        //ScannerTest(scanner);

        //scanner.Position = identical.Position;
        //ScannerTest(scanner);

        //scanner.Position = smaller.Position;
        //ScannerTest(scanner);

        //scanner.Position--;
        //ScannerTest(scanner);

        //scanner.Position = bigger.Position;
        //ScannerTest(scanner);

        //scanner.Position++;
        //ScannerTest(scanner);

        QueueMessage("---scanOwnSpace---");
        scanner.scanOwnSpace = true;
        scanner.Position = new(0, 0);
        identical.Space = mainSpace;
        identical.Position = new(0, 0);
        ScannerTest(scanner);
        #endregion

        Thread.Sleep(20000);
        Shutdown();
    }

    private static void DetectedTest(PhysicsShape scanner, PhysicsShape shape)
    {
        QueueMessage("-Detected-");
        
        QueueMessage(scanner.Name);
        QueueMessage(scanner.Scene?.Name);
        QueueMessage(scanner.Space?.Name);
        QueueMessage(scanner.Position);
        QueueMessage(scanner.shapeOffset);
        //QueueMessage(scanner.size);
        QueueMessage(scanner.AnyInteraction);
        QueueMessage(scanner.Equal);
        QueueMessage(scanner.Intersecting);
        QueueMessage(scanner.Enveloping);
        QueueMessage(scanner.Within);
        QueueMessage(scanner.Touching);
        
        QueueMessage(shape.GetType());
        QueueMessage(shape is PhysicsRectangle);
        if (shape is PhysicsRectangle rectangle)
        {
            QueueMessage(rectangle.Name);
            QueueMessage(rectangle.Scene?.Name);
            QueueMessage(rectangle.Space?.Name);
            QueueMessage(rectangle.Position);
            QueueMessage(rectangle.shapeOffset);
            QueueMessage(rectangle.size);
        }

        QueueMessage("---");
    }

    private static void PrintScanMode(PhysicsRectangle scanner) { }//=> QueueMessage($"{scanner.scanMode} : {ToBinString((byte)scanner.scanMode)}");

    private static void ScannerTest(PhysicsRectangle scanner)
    {
        QueueMessage("---Scanner Test---");
        
        Engine.Frame();

        scanner.ScanForEqual = true;
        PrintScanMode(scanner);
        Engine.Frame();
        scanner.ScanForEqual = false;
        PrintScanMode(scanner);

        scanner.ScanForIntersecting = true;
        PrintScanMode(scanner);
        Engine.Frame();
        scanner.ScanForIntersecting = false;
        PrintScanMode(scanner);

        scanner.ScanForEnveloping = true;
        PrintScanMode(scanner);
        Engine.Frame();
        scanner.ScanForEnveloping = false;
        PrintScanMode(scanner);

        scanner.ScanForWithin = true;
        PrintScanMode(scanner);
        Engine.Frame();
        scanner.ScanForWithin = false;
        PrintScanMode(scanner);

        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);

        #region Two
        //A2
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();

        //B2
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //C2
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //D2
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //E2
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //F2
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //G2
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //H2
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //I2
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //J2
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        #endregion

        #region Three
        //A3
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();
        
        //B3
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();

        //C3
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //D3
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();

        //E3
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //F3
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //G3
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();

        //H3
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //I3
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //J3
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        #endregion

        #region Four
        //A4
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = false;
        PrintScanMode(scanner);
        Engine.Frame();

        //B4
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = false;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //C4
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = false;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //D4
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = false;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        //E4
        scanner.ScanForEqual = false;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();
        #endregion

        //All
        scanner.ScanForEqual = true;
        scanner.ScanForIntersecting = true;
        scanner.ScanForEnveloping = true;
        scanner.ScanForWithin = true;
        scanner.ScanForTouching = true;
        PrintScanMode(scanner);
        Engine.Frame();

        QueueMessage("-----");
    }
    #endregion
}
