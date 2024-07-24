using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Physics;

/// <summary> Base class for all 2D physics object types </summary>
public abstract class PhysicsShape : Object2D
{
    private protected enum ScanModes : byte
    {
        None = 0b00000,
        Equal = 0b00001,
        Intersecting = 0b00010,
        Enveloping = 0b00100,
        Within = 0b01000,
        Touching = 0b10000,

        A2 = Equal | Intersecting,
        B2 = Equal | Enveloping,
        C2 = Equal | Within,
        D2 = Equal | Touching,
        E2 = Intersecting | Enveloping,
        F2 = Intersecting | Within,
        G2 = Intersecting | Touching,
        H2 = Enveloping | Within,
        I2 = Enveloping | Touching,
        J2 = Within | Touching,

        A3 = Equal | Intersecting | Enveloping,
        B3 = Equal | Intersecting | Within,
        C3 = Equal | Intersecting | Touching,
        D3_Overlapping = Equal | Enveloping | Within,
        E3 = Equal | Enveloping | Touching,
        F3 = Equal | Within | Touching,
        G3 = Intersecting | Enveloping | Within,
        H3 = Intersecting | Enveloping | Touching,
        I3 = Intersecting | Within | Touching,
        J3 = Enveloping | Within | Touching,

        A4 = Equal | Intersecting | Enveloping | Within,
        B4 = Equal | Intersecting | Enveloping | Touching,
        C4 = Equal | Intersecting | Within | Touching,
        D4 = Equal | Enveloping | Within | Touching,
        E4 = Intersecting | Enveloping | Within | Touching,

        All = Equal | Intersecting | Enveloping | Within | Touching,
        //0b0_0010_0000 ttop?
        //0b0_0100_0000 tbottom?
        //0b0_1000_0000 tleft?
        //0b1_0000_0000 tright?
    }

    /// <summary> Used by the engine for when Scanners detect other shapes </summary>
    /// <param name="scanner"> Scanner that detected the shape </param>
    /// <param name="shape"> Shape detected by the scanner</param>
    public delegate void PhysicsEvent(PhysicsShape scanner, PhysicsShape shape/*, bools?*/);

    internal PhysicsShape(NameID name, params PhysicsEvent[] detectedEvents) : base(name)
    {
#if DEBUG
        shapeColor = DefaultShapeColor;
#endif
        if (detectedEvents != null && detectedEvents.Length > 0)
            foreach (PhysicsEvent detectedEvent in detectedEvents)
                detected += detectedEvent;
    }

    private protected ScanModes scanMode = ScanModes.None;

    private protected bool equal = false, intersecting = false, enveloping = false, within = false, touching = false;

    private protected readonly PhysicsEvent detected;

    internal PhysicsSpace space = null;

    /// <summary> controls whether the object should process physics behavior or be scanned by other objects </summary>
    public bool shapeEnabled = true;//physicsEnabled?

    /// <summary> Position of the shape relative to the object </summary>
    public Vector shapeOffset = new(0, 0);
    //test these
    public bool verticalFlipShape = false, horizontalFlipShape = false, verticalFlipOffset = false, horizontalFlipOffset = false, flipOffsetOnPixel = false;
    
    /// <summary> Controls whether the shape should scan other shapes </summary>
    public bool scan = false;

    /// <summary> Controls whether scanning should stop at the first interaction detected </summary>
    public bool scanThoroughly = false;

    /// <summary> Controls whether the object should scan shapes in its own PhysicsSpace </summary>
    public bool scanOwnSpace = false;

    /// <summary> Controls whether the shape scans rectangle objects </summary>
    public bool scanRectangles = false;
    //scanRightTriangles
    //scanPolygons
    //scanCircles
    //scanRays

    /// <summary> Controls whether the shape transforms with respect to other shapes based on it's velocity </summary>
    public bool applyVelocity = false;//rename move?

    public bool keepMoving = true; //rename //remove?
    public bool preferY = false; //rename?
    //push on diagonals

    /// <summary> Controls whether the shape respects rectangles when transforming based on it's velocity </summary>
    public bool collideWithRectangles = false;
    //collideWithRightTriangles
    //collideWithPolygons
    //collideWithCircles
    //collideWithRays

    /// <summary> Distance the shape will transform each frame if shape is enabled and applyVelocity is true </summary>
    public Vector velocity = new(0, 0);

#if DEBUG
    /// <summary> Whether the shape should be drawn for debugging </summary>
    public bool drawShape = false;

    /// <summary> Color used for debug drawing </summary>
    public Color shapeColor;

    /// <summary> Value initially assigned to shapeColor, can be overriden, blue by default </summary>
    protected virtual Color DefaultShapeColor => new(0, 0, byte.MaxValue);
#endif

    /// <summary> PhysicsSpace the shape is placed on </summary>
    public abstract PhysicsSpace Space { get; set; }

    /// <summary> Scan for if this object's shape is identical to others' </summary>
    public bool ScanForEqual
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (scanMode & ScanModes.Equal) == ScanModes.Equal;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value) scanMode |= ScanModes.Equal;
            else scanMode &= ~ScanModes.Equal;
        }
    }

    /// <summary> Scan for if this object's shape is intersecting others' </summary>
    public bool ScanForIntersecting
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (scanMode & ScanModes.Intersecting) == ScanModes.Intersecting;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value) scanMode |= ScanModes.Intersecting;
            else scanMode &= ~ScanModes.Intersecting;
        }
    }

    /// <summary> Scan for if this object's shape is enveloping others' </summary>
    public bool ScanForEnveloping
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (scanMode & ScanModes.Enveloping) == ScanModes.Enveloping;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value) scanMode |= ScanModes.Enveloping;
            else scanMode &= ~ScanModes.Enveloping;
        }
    }

    /// <summary> Scan for if this object's shape is within others' </summary>
    public bool ScanForWithin
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (scanMode & ScanModes.Within) == ScanModes.Within;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value) scanMode |= ScanModes.Within;
            else scanMode &= ~ScanModes.Within;
        }
    }

    /// <summary> Scan for if this object's shape is touching others' </summary>
    public bool ScanForTouching
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (scanMode & ScanModes.Touching) == ScanModes.Touching;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value) scanMode |= ScanModes.Touching;
            else scanMode &= ~ScanModes.Touching;
        }
    }

    /// <summary> If this object's shape is interacting with another's </summary>
    public bool AnyInteraction
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && (intersecting || enveloping || within || touching);
    }

    /// <summary> If this object's shape is equal to another's </summary>
    public bool Equal
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && equal;
    }

    /// <summary> If this object's shape is intersecting another's </summary>
    public bool Intersecting
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && intersecting;
    }

    /// <summary> If this object's shape is enveloping another's </summary>
    public bool Enveloping
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && enveloping;
    }

    /// <summary> If this object's shape is within another's </summary>
    public bool Within
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && within;
    }

    /// <summary> If this object's shape is touching another's </summary>
    public bool Touching
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && touching;
    }

    private protected abstract void _scan();
    
    private protected abstract void _move();//_translate()?

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override void _frame()
    {
        if (!paused || pauseWalks)
        {
            PreFrame();

            equal = false;
            intersecting = false;
            enveloping = false;
            within = false;
            touching = false;

            if (shapeEnabled)
            {
                if (space == null) Engine.SendError(ErrorCodes.NullPhysicsSpace, name);
                else
                {
                    if (applyVelocity && (velocity.x != 0 || velocity.y != 0))
                    {
                        if (collideWithRectangles/* || collideWithRightTriangles || collideWithPolygons || collideWithCircles || collideWithRays*/) _move();
                        else position = new(position.x + velocity.x, position.y + velocity.y);
                    }
                    if (scan && scanMode != ScanModes.None) _scan();
                }
            }
            PostFrame();
        }
    }
}