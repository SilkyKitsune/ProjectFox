using System;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using static ProjectFox.CoreEngine.Math.Math;
#if DEBUG
using ProjectFox.GameEngine.Visuals;
#endif

namespace ProjectFox.GameEngine.Physics;

/// <summary> Basic rectangle object that can scan other PhysicsShapes for different kinds of interactions </summary>
public class PhysicsRectangle : PhysicsShape
{
    /// <param name="name"> the object's ID </param>
    /// <param name="detectedEvents"> delegates called when a shaped is detected </param>
    public PhysicsRectangle(NameID name, params PhysicsEvent[] detectedEvents) : base(name, detectedEvents) { }//color = new(128, 0, 255);
    //implement these
    private bool touchingTop = false, touchingBottom = false, touchingLeft = false, touchingRight = false;

    /// <summary> Size of the rectangle </summary>
    public Vector size = new(1, 1);

    public bool soft = false, collideWithTop = false, collideWithBottom = false, collideWithLeft = false, collideWithRight = false;

    public sealed override PhysicsSpace Space
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => space;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value == null) space?.RemoveRectangle(name);
            else value.AddRectangle(this);
        }
    }

    public Rectangle Rectangle
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(
            horizontalFlipOffset ? position.x - size.x - shapeOffset.x + (flipOffsetOnPixel ? 1 : 0) : position.x + shapeOffset.x,
            verticalFlipOffset ? position.y - size.y - shapeOffset.y + (flipOffsetOnPixel ? 1 : 0) : position.y + shapeOffset.y,
            size);
    }

    public bool TouchingTop
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && touchingTop;
    }

    public bool TouchingBottom
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && touchingBottom;
    }

    public bool TouchingLeft
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && touchingLeft;
    }

    public bool TouchingRight
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => shapeEnabled && touchingRight;
    }

    private protected sealed override void _scan()
    {
        if (size.x <= 0 || size.y <= 0) return;//size error?

        Rectangle rect = Rectangle;//inline

        Scene scene = owner == null ? this.scene : (owner.owner == null ? owner.scene : owner.Scene);

        for (int i = scanOwnSpace ? -1 : 0; i < space.scanSpaces.codes.length; i++)
        {
            PhysicsSpace scanSpace = i < 0 ? space : space.scanSpaces.values.elements[i];
            
            for (int j = 0, l = Max(
                scanRectangles ? scanSpace.rectangles.codes.length : 0,
                0,//scanRightTriangles ? scanSpace.rightTriangles.codes.length : 0,
                0,//scanPolygons ? scanSpace.polygons.codes.length : 0,
                0,//scanCircles ? scanSpace.circles.codes.length : 0,
                0//scanRays ? scanSpace.rays.codes.length : 0,
                );
                j < l; j++)
            {
                if (scanRectangles && j < scanSpace.rectangles.codes.length)
                {
                    PhysicsRectangle rectangle = scanSpace.rectangles.values.elements[j];

                    Scene shapeScene = rectangle.owner == null ? rectangle.scene : (rectangle.owner.owner == null ? rectangle.owner.scene : rectangle.owner.Scene);
                    
                    if (scene != shapeScene)//what if rectangle is a pet?
                        Engine.SendError(ErrorCodes.PhysicsShapeNotInScene, name, rectangle.name.ToString(),
                            $"Rectangle '{name}' read a PhysicsShape from a null/different scene");

                    if (this != rectangle && rectangle.enabled && rectangle.shapeEnabled && rectangle.size.x > 0 && rectangle.size.y > 0)
                    {
                        Rectangle rect2 = rectangle.Rectangle;//inline

                        switch (scanMode)//test all cases
                        {
                            #region Basic Cases
                            case ScanModes.Equal:
                                {
                                    if (rect.Equals(rect2))
                                    {
                                        equal = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.Intersecting:
                                {
                                    if (rect.Intersecting(rect2))
                                    {
                                        intersecting = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.Enveloping:
                                {
                                    if (rect.Enveloping(rect2))
                                    {
                                        enveloping = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.Within:
                                {
                                    if (rect2.Enveloping(rect))
                                    {
                                        within = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.Touching:
                                {
                                    if (rect.Touching(rect2))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            #endregion

                            #region 2 Cases
                            case ScanModes.A2:
                                {
                                    if (rect.Intersecting(rect2))
                                    {
                                        equal = equal || rect.Equals(rect2);
                                        intersecting = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.B2:
                                {
                                    bool a = rect.Equals(rect2), b = rect.Enveloping(rect2);
                                    equal = equal || a;
                                    enveloping = enveloping || b;
                                    if (a || b)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.C2:
                                {
                                    bool a = rect.Equals(rect2), b = rect2.Enveloping(rect);
                                    equal = equal || a;
                                    within = within || b;
                                    if (a || b)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.D2:
                                {
                                    if (rect.Equals(rect2))
                                    {
                                        equal = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if (rect.Touching(rect2))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.E2:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);

                                    if (r.size.x > 0 && r.size.y > 0)
                                    {
                                        intersecting = true;
                                        enveloping = enveloping || (!r.size.Equals(rect.size) && r.size.Equals(rect2.size));

                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.F2:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);

                                    if (r.size.x > 0 && r.size.y > 0)
                                    {
                                        intersecting = true;
                                        within = within || (r.size.Equals(rect.size) && !r.size.Equals(rect2.size));

                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.G2:
                                {
                                    /*if (rect.Intersecting(rect2))
                                    {
                                        intersecting = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if (rect.Touching(rect2))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }*/

                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x && y)
                                    {
                                        intersecting = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((x && r.size.y == 0) || (y && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.H2:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size), env = !a && b, w = a && !b;

                                    enveloping = enveloping || env;
                                    within = within || w;

                                    if (env || w)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.I2:
                                {
                                    if (rect.Enveloping(rect2))
                                    {
                                        enveloping = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if (rect.Touching(rect2))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.J2:
                                {
                                    if (rect2.Enveloping(rect))
                                    {
                                        within = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if (rect.Touching(rect2))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            #endregion

                            #region 3 Cases
                            case ScanModes.A3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    if (r.size.x > 0 && r.size.y > 0)
                                    {
                                        bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                        equal = equal || (a && b);
                                        intersecting = true;
                                        enveloping = enveloping || (!a && b);

                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.B3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    if (r.size.x > 0 && r.size.y > 0)
                                    {
                                        bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                        equal = equal || (a && b);
                                        intersecting = true;
                                        within = within || (a && !b);

                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.C3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x && y)
                                    {
                                        equal = equal || rect.Equals(rect2);
                                        intersecting = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((x && r.size.y == 0) || (y && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.D3_Overlapping:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size), eq = a && b, env = !a && b, w = a && !b;

                                    equal = equal || eq;
                                    enveloping = enveloping || env;
                                    within = within || w;

                                    if (eq || env || w)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.E3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size), eq = a && b, env = !a && b;

                                    equal = equal || eq;
                                    enveloping = enveloping || env;

                                    if (eq || env)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((r.size.x > 0 && r.size.y == 0) || (r.size.y > 0 && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.F3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size), eq = a && b, w = a && !b;

                                    equal = equal || eq;
                                    within = within || w;

                                    if (eq || w)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((r.size.x > 0 && r.size.y == 0) || (r.size.y > 0 && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.G3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    if (r.size.x > 0 && r.size.y > 0)
                                    {
                                        bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                        intersecting = true;
                                        enveloping = enveloping || (!a && b);
                                        within = within || (a && !b);

                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.H3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x && y)
                                    {
                                        intersecting = true;
                                        enveloping = enveloping || (!r.size.Equals(rect.size) && r.size.Equals(rect2.size));
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((x && r.size.y == 0) || (y && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.I3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x && y)
                                    {
                                        intersecting = true;
                                        within = within || (r.size.Equals(rect.size) && !r.size.Equals(rect2.size));
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((x && r.size.y == 0) || (y && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.J3:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size), env = !a && b, w = a && !b;

                                    enveloping = enveloping || env;
                                    within = within || w;

                                    if (env || w)
                                    {
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    else if ((r.size.x > 0 && r.size.y == 0) || (r.size.y > 0 && r.size.x == 0))
                                    {
                                        touching = true;
                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            #endregion

                            #region 4 Cases
                            case ScanModes.A4:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);

                                    if (r.size.x > 0 && r.size.y > 0)
                                    {
                                        bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                        equal = equal || (a && b);
                                        intersecting = true;
                                        enveloping = enveloping || (!a && b);
                                        within = within || (a && !b);

                                        detected?.Invoke(this, rectangle);
                                        if (!scanThoroughly) return;
                                    }
                                    break;
                                }
                            case ScanModes.B4:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x || y)
                                    {
                                        bool eq = false, intr = false, env = false, t = false;

                                        if (x && y)
                                        {
                                            bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                            eq = a && b;
                                            intr = true;
                                            env = !a && b;
                                        }
                                        else t = (x && r.size.y == 0) || (y && r.size.x == 0);

                                        equal = equal || eq;
                                        intersecting = intersecting || intr;
                                        enveloping = enveloping || env;
                                        touching = touching || t;

                                        if (eq || intr || env || t)
                                        {
                                            detected?.Invoke(this, rectangle);
                                            if (!scanThoroughly) return;
                                        }
                                    }
                                    break;
                                }
                            case ScanModes.C4:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x || y)
                                    {
                                        bool eq = false, intr = false, w = false, t = false;

                                        if (x && y)
                                        {
                                            bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                            eq = a && b;
                                            intr = true;
                                            w = a && !b;
                                        }
                                        else t = (x && r.size.y == 0) || (y && r.size.x == 0);

                                        equal = equal || eq;
                                        intersecting = intersecting || intr;
                                        within = within || w;
                                        touching = touching || t;

                                        if (eq || intr || w || t)
                                        {
                                            detected?.Invoke(this, rectangle);
                                            if (!scanThoroughly) return;
                                        }
                                    }
                                    break;
                                }
                            case ScanModes.D4:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x || y)
                                    {
                                        bool eq = false, env = false, w = false, t = false;

                                        if (x && y)
                                        {
                                            bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2);

                                            eq = a && b;
                                            env = !a && b;
                                            w = a && !b;
                                        }
                                        else t = (x && r.size.y == 0) || (y && r.size.x == 0);

                                        equal = equal || eq;
                                        enveloping = enveloping || env;
                                        within = within || w;
                                        touching = touching || t;

                                        if (eq || env || w || t)
                                        {
                                            detected?.Invoke(this, rectangle);
                                            if (!scanThoroughly) return;
                                        }
                                    }
                                    break;
                                }
                            case ScanModes.E4:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x || y)
                                    {
                                        bool intr = false, env = false, w = false, t = false;

                                        if (x && y)
                                        {
                                            bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                            intr = true;
                                            env = !a && b;
                                            w = a && !b;
                                        }
                                        else t = (x && r.size.y == 0) || (y && r.size.x == 0);

                                        intersecting = intersecting || intr;
                                        enveloping = enveloping || env;
                                        within = within || w;
                                        touching = touching || t;

                                        if (intr || env || w || t)
                                        {
                                            detected?.Invoke(this, rectangle);
                                            if (!scanThoroughly) return;
                                        }
                                    }
                                    break;
                                }
                            #endregion

                            case ScanModes.All:
                                {
                                    Rectangle r = rect.IntersectionBounds(rect2);
                                    bool x = r.size.x > 0, y = r.size.y > 0;

                                    if (x || y)//is this necessary?
                                    {
                                        bool eq = false, intr = false, env = false, w = false, t = false;

                                        if (x && y)
                                        {
                                            bool a = r.size.Equals(rect.size), b = r.size.Equals(rect2.size);

                                            eq = a && b;
                                            intr = true;
                                            env = !a && b;
                                            w = a && !b;
                                        }
                                        else t = (x && r.size.y == 0) || (y && r.size.x == 0);

                                        equal = equal || eq;
                                        intersecting = intersecting || intr;
                                        enveloping = enveloping || env;
                                        within = within || w;
                                        touching = touching || t;

                                        if (eq || intr || env || w || t)
                                        {
                                            detected?.Invoke(this, rectangle);
                                            if (!scanThoroughly) return;
                                        }
                                    }
                                    break;
                                }

                            default:
                                throw new Exception($"ScanMode error in {typeof(PhysicsRectangle)} {nameof(scanMode)}=[{scanMode} : {(int)scanMode}");
                        }
                    }
                }

                //if (scanRightTriangles && j < scanSpace.rightTriangles.codes.length)

                //if (scanPolygons && j < scanSpace.polygons.codes.length)

                //if (scanCircles && j < scanSpace.circles.codes.length)

                //if (scanRays && j < scanSpace.rays.codes.length)
            }
        }
    }

    private protected sealed override void _move()
    {
        if (size.x <= 0 || size.y <= 0) return;//size error?
        
        Vector[] steps = Vector.StepInterpolate(velocity);

        bool xVelZero = velocity.x == 0, xVelNeg = velocity.x < 0, xVelPos = velocity.x > 0,//can these be combined at all?
             yVelZero = velocity.y == 0, yVelNeg = velocity.y < 0, yVelPos = velocity.y > 0;

        Vector corrected = new(0, 0), step = corrected,
            absVel = new(xVelNeg ? -velocity.x : velocity.x, yVelNeg ? -velocity.y : velocity.y);
        Rectangle rect = Rectangle;//inline

        Scene scene = owner == null ? this.scene : (owner.owner == null ? owner.scene : owner.Scene);

        bool xBlocked = false, yBlocked = false, corner = false, yGreater = absVel.y > absVel.x,
            preferY = (absVel.x == absVel.y && this.preferY) || yGreater;

        for (int stepIndex = 0, checkIndex = 0, length = yGreater ? absVel.y : absVel.x;
            checkIndex < length;
            step = steps[stepIndex],
            corrected = new(
                xBlocked ? corrected.x : corrected.x + step.x,
                yBlocked ? corrected.y : corrected.y + step.y),
            rect.position = new(
                position.x + shapeOffset.x + corrected.x,
                position.y + shapeOffset.y + corrected.y),
            stepIndex++, checkIndex++)
        {
            if (stepIndex >= steps.Length) stepIndex = 0;

            xBlocked = xVelZero;
            yBlocked = yVelZero;
            corner = false;

            for (int i = scanOwnSpace ? -1 : 0; i < space.scanSpaces.codes.length; i++)
            {
                PhysicsSpace scanSpace = i < 0 ? space : space.scanSpaces.values.elements[i];
                
                for (int j = 0, l = Max(
                #region
                    collideWithRectangles ? scanSpace.rectangles.codes.length : 0,
                    0,//collideWithRightTriangles ? scanSpace.rightTriangles.codes.length : 0,
                    0,//collideWithPolygons ? scanSpace.polygons.codes.length : 0,
                    0,//collideWithCircles ? scanSpace.circles.codes.length : 0,
                    0//collideWithRays ? scanSpace.rays.codes.length : 0
                    );
                #endregion
                    j < l; j++)
                {
                    if (collideWithRectangles && j < scanSpace.rectangles.codes.length)
                    {
                        PhysicsRectangle rectangle = scanSpace.rectangles.values.elements[j];

                        Scene shapeScene = rectangle.owner == null ? rectangle.scene : (rectangle.owner.owner == null ? rectangle.owner.scene : rectangle.owner.Scene);

                        if (scene != shapeScene)
                            Engine.SendError(ErrorCodes.PhysicsShapeNotInScene, name, rectangle.name.ToString(),
                                $"Rectangle '{name}' read a PhysicsShape from a null/different scene");

                        if (this != rectangle && rectangle.enabled && rectangle.shapeEnabled && rectangle.size.x > 0 && rectangle.size.y > 0)
                        {
                            Rectangle rect2 = rectangle.Rectangle/*inline*/, area = rect.IntersectionBounds(rect2);

                            //if (rectangle.soft)//? //use lines for soft?

                            /*
                            if (shape.soft)
                              if (shape.top && velocity.y > 0) ?
                              if (shape.bottom && velocity.y < 0) ?
                              if (shape.left && velocity.x > 0) ?
                              if (shape.right && velocity.x < 0) ?
                            */

                            bool xPos = area.size.x > 0, yPos = area.size.y > 0, xZero = area.size.x == 0, yZero = area.size.y == 0;

                            if (xPos && yPos) return; //don't apply velocity if intersecting

                            corner = corner || (xZero && yZero);

                            yBlocked = yBlocked || (xPos && yZero &&
                                ((yVelNeg && area.position.y == rect.position.y) || //top
                                 (yVelPos && area.position.y == rect2.position.y))); //bottom

                            xBlocked = xBlocked || (yPos && xZero &&
                                ((xVelNeg && area.position.x == rect.position.x) || //left
                                 (xVelPos && area.position.x == rect2.position.x))); //right

                            if ((!keepMoving && (xBlocked || yBlocked)) || (xBlocked && yBlocked)) goto End;//is this too early if corners are applied later?
                        }
                    }
                    #region
                    //if (collideWithRightTriangles && j < scanSpace.rightTriangles.codes.length)
                    //if (collideWithPolygons && j < scanSpace.polygons.codes.length)
                    //if (collideWithCircles && j < scanSpace.circles.codes.length)
                    //if (collideWithRays && j < scanSpace.rays.codes.length)
                    #endregion
                }
            }
            
            if (corner)
            {
                if (preferY && !yBlocked) xBlocked = true;
                else if (!xBlocked) yBlocked = true;
                //C.QueueMessage("corner");
            }
            //catches nonblocking corners slightly
            //are there any other issues with this?
        }
        End:
        //if (!velocity.Equals(corrected)) C.QueueMessage($"{velocity} != {corrected}\n   {string.Join(',', steps)}");
        position = new(position.x + corrected.x, position.y + corrected.y);

        /*
        this is fixed I believe
        kinematic pushing down-right into two rectangles stacked on top of each other, kinematic.preferY = false and it touches the corner of the lower rectangle
        */
    }

#if DEBUG
    internal override void _draw(PortableScreen screen = null)
    {
        base._draw(screen);

        bool usePortableScreen = screen != null;

        if (!Screen.visible || !Debug.debugLayer.visible || !drawShape || !shapeEnabled || size.x <= 0 || size.y <= 0 || shapeColor.a == 0 ||
            (usePortableScreen && !screen.drawDebug)) return;

        Rectangle rect = Rectangle,//inline?
            screenArea = usePortableScreen ? screen.viewArea : new(Screen.position, Screen.size),
            area = screenArea.IntersectionBounds(rect);
        Color[] layerPixels = usePortableScreen ? Debug.debugLayer.portablePixels : Debug.debugLayer.pixels;

        if (area.size.x <= 0 || area.size.y <= 0) return;

        area.position = new(
            area.position.x - screenArea.position.x,
            area.position.y - screenArea.position.y);

        bool useAlpha = shapeColor.a < byte.MaxValue;
        int x = 0, s = 0, l = area.size.x * area.size.y,
            d = area.position.y * screenArea.size.x + area.position.x,
            step = screenArea.size.x - area.size.x;
        while (s++ < l && d < layerPixels.Length)
        {
            layerPixels[d] = useAlpha ? layerPixels[d].Blend(shapeColor) : shapeColor;
            d++;

            if (++x == area.size.x)
            {
                x = 0;
                d += step;
            }
        }
    }
#endif
}