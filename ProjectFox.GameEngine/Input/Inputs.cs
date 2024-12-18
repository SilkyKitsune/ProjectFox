using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine.Input;

public class DigitalButton
{
    internal bool value = false, changed = false;

    public readonly ICollection<DigitalButton> bindings = new Array<DigitalButton>(0x2);
    //analog button bindings
    //dpad bindings?
    //stick bindings?

    public bool Value
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => value;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            changed = value != this.value;
            this.value = value;
        }
    }

    public bool Changed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => changed;
    }

    public bool ChangedTrue
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => value && changed;
    }

    public bool ChangedFalse
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => !value && changed;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator bool(DigitalButton digitalButton) => digitalButton.value;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator byte(DigitalButton digitalButton) => digitalButton.value ? byte.MaxValue : byte.MinValue;
}

public class AnalogButton
{
    public byte value = 0, deadZone = 0;

    public readonly ICollection<AnalogButton> bindings = new Array<AnalogButton>(0x2);
    public readonly ICollection<DigitalButton> digitalBindings = new Array<DigitalButton>(0x2);

    public bool Pressed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => value > deadZone;
    }

    public bool FullyPressed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => value == byte.MaxValue;
    }
}

public class DirectionalPad
{
    public Vector.Direction value = Vector.Direction.Zero;

    public readonly ICollection<DirectionalPad> bindings = new Array<DirectionalPad>(0x2);
    public readonly ICollection<DigitalButton>
        xNegBindings = new Array<DigitalButton>(0x2),
        xPosBindings = new Array<DigitalButton>(0x2),
        yNegBindings = new Array<DigitalButton>(0x2),
        yPosBindings = new Array<DigitalButton>(0x2);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector.Direction(DirectionalPad directionalPad) => directionalPad.value;
}

public class Stick//directional stick?
{
    private const float MaxF = MaxValue;
    
    public const int MinValue = -127, MaxValue = 127;
    
    internal Vector position = new(0, 0), deadZone = new(0, 0), negDeadZone = new(0, 0);//neg deadzone redundant?
    
    internal readonly DigitalButton xMoved = new(), yMoved = new();

    public readonly ICollection<Stick> bindings = new Array<Stick>(0x2);
    //analog button bindings
    //digital button bindings
    //dpad bindings
    //cursor bindings?

    //Analog stick = direction.tovector() * 127

    public Vector Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => position;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            position = value.Clamp(MinValue, MaxValue);
            xMoved.Value = position.x > deadZone.x || position.x < negDeadZone.x;
            yMoved.Value = position.y > deadZone.y || position.y < negDeadZone.y;
        }
    }

    public int X
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => position.x;
    }

    public int Y
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => position.y;
    }

    public VectorF PositionF//ToFloat?
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(position.x / MaxF, position.y / MaxF);
    }

    public float Xf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => position.x / MaxF;
    }

    public float Yf
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => position.y / MaxF;
    }

    public Vector DeadZone//these need to update buttons maybe?
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => deadZone;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            deadZone = value.Clamp(MinValue, MaxValue);
            negDeadZone = new(-deadZone.x, -deadZone.y);
        }
    }

    public int DeadZoneX
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => deadZone.x;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            deadZone.x = Math.Clamp(value, MinValue, MaxValue);
            negDeadZone.x = -deadZone.x;
        }
    }

    public int DeadZoneY
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => deadZone.y;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            deadZone.y = Math.Clamp(value, MinValue, MaxValue);
            negDeadZone.y = -deadZone.y;
        }
    }

    public bool Moved//rename?
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //get => position.x > deadZone.x || position.x < negDeadZone.x || position.y > deadZone.y || position.y < negDeadZone.y;
        get => xMoved.value || yMoved.value;
    }

    public bool XAxisMoved//rename?
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //get => position.x > deadZone.x || position.x < negDeadZone.x;
        get => xMoved.value;
    }

    public bool YAxisMoved//rename?
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        //get => position.y > deadZone.y || position.y < negDeadZone.y;
        get => yMoved.value;
    }

    public bool Changed
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => xMoved.changed || yMoved.changed;
    }

    public bool XAxisChanged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => xMoved.changed;
    }

    public bool YAxisChanged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => yMoved.changed;
    }

    //left right up down?

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector(Stick analogStick) => analogStick.position;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]//shouldn't this be weighted?
    public static implicit operator Vector.Direction(Stick analogStick) => analogStick.position.DirectionFromZero();
}

public class Cursor
{
    internal Vector nativePos = new(0, 0), scaledPos = new(0, 0);
    internal bool posChanged = false;//digital button?

    public Cursor binding = null;

    public Vector Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scaledPos;
        set
        {
            if (nativePos.Equals(value))
            {
                posChanged = false;
                return;
            }

            nativePos = value;

            float scale = Screen.Scale;//is the member private?
            int scaleInt = (int)scale;
            Vector scaled = Screen.OneToOne ?//is the member private?
            new(value.x / scaleInt, value.y / scaleInt) :
            new((int)(value.x / scale), (int)(value.y / scale));

            scaled = scaled.Clamp(default, Screen.size);

            if (scaledPos.Equals(scaled))
            {
                posChanged = false;
                return;
            }

            scaledPos = scaled;
            posChanged = true;
        }
    }

    public int X
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scaledPos.x;
    }

    public int Y
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scaledPos.y;
    }

    public bool PositionChanged
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => posChanged;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator Vector(Cursor cursor) => cursor.scaledPos;
}