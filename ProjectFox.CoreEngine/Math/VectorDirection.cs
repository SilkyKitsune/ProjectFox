using System.Runtime.CompilerServices;
using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
    /// <summary> an enum for representing 2D direction </summary>
    public enum Direction : byte
    {
        /// <summary> no x or y direction 0b00_00 </summary>
        Zero = 0b00_00,
        /// <summary> no x direction and negative y direction 0b00_10 </summary>
        YNeg = 0b00_10,
        /// <summary> positive x direction and negative y direction 0b01_10 </summary>
        PosNegQuad = XPos | YNeg,//0b01_10,
        /// <summary> positive x direction and no y direction 0b01_00 </summary>
        XPos = 0b01_00,
        /// <summary> positive x and y direction 0b01_01 </summary>
        PosQuad = XPos | YPos,//0b01_01,
        /// <summary> no x direction and positive y direction 0b00_01 </summary>
        YPos = 0b00_01,
        /// <summary> negative x direction and positive y direction 0b10_01 </summary>
        NegPosQuad = XNeg | YPos,//0b10_01
        /// <summary> negative x direction and no y direction 0b10_00 </summary>
        XNeg = 0b10_00,
        /// <summary> negative x and y direction 0b10_10 </summary>
        NegQuad = XNeg | YNeg,//0b10_10,

        //what to fill these with?
        /// <summary>  </summary>
        Center = Zero,
        /// <summary>  </summary>
        Up = YNeg,
        /// <summary>  </summary>
        UpRight = PosNegQuad,
        /// <summary>  </summary>
        Right = XPos,
        /// <summary>  </summary>
        DownRight = PosQuad,
        /// <summary>  </summary>
        Down = YPos,
        /// <summary>  </summary>
        DownLeft = NegPosQuad,
        /// <summary>  </summary>
        Left = XNeg,
        /// <summary>  </summary>
        UpLeft = NegQuad
    }

    /// <summary></summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    /// <remarks> Equal/Center will throw ArgumentException </remarks>
    /// <exception cref="ArgumentException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float AngleFromDirection(Direction direction) => direction switch
    {
        Direction.YNeg => 0f,
        Direction.PosNegQuad => 0.125f,
        Direction.XPos => 0.25f,
        Direction.PosQuad => 0.375f,
        Direction.YPos => 0.5f,
        Direction.NegPosQuad => 0.625f,
        Direction.XNeg => 0.75f,
        Direction.NegQuad => 0.875f,
        _ => throw new ArgumentException()//should zero return NaN instead?
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction DirectionFromAngle(float angle)
    {
        angle = Math.Clamp(angle, -1f, 1f);
        if (angle < 0) angle += 1f;//same as 1f+angle ?

        if (angle >= 0.0625f && angle < 0.1875f) return Direction.PosNegQuad;
        if (angle >= 0.1875f && angle < 0.3125f) return Direction.XPos;
        if (angle >= 0.3125f && angle < 0.4375f) return Direction.PosQuad;
        if (angle >= 0.4375f && angle < 0.5625f) return Direction.YPos;
        if (angle >= 0.5625f && angle < 0.6875f) return Direction.NegPosQuad;
        if (angle >= 0.6875f && angle < 0.8125f) return Direction.XNeg;
        if (angle >= 0.8125f && angle < 0.9375f) return Direction.NegQuad;
        if ((angle >= 0f && angle < 0.0625f) || (angle >= 0.9375f && angle <= 1f)) return Direction.YNeg;

        throw new Exception($"Angle evaluation error angle={angle}");
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]//rename?
    public static VectorF DirectionToRotatedVector(Direction direction) => direction switch
    {
        Direction.Zero => new(0f, 0f),
        Direction.YNeg => new(0f, -1f),
        Direction.PosNegQuad => new(0.707106781187f, -0.707106781187f),//simplify value?
        Direction.XPos => new(1f, 0f),
        Direction.PosQuad => new(0.707106781187f, 0.707106781187f),
        Direction.YPos => new(0f, 1f),
        Direction.NegPosQuad => new(-0.707106781187f, 0.707106781187f),
        Direction.XNeg => new(-1f, 0f),
        Direction.NegQuad => new(-0.707106781187f, -0.707106781187f),
        _ => throw new ArgumentException()
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector DirectionToVector(Direction direction) => direction switch
    {
        Direction.Zero => new(0, 0),
        Direction.YNeg => new(0, -1),
        Direction.PosNegQuad => new(1, -1),
        Direction.XPos => new(1, 0),
        Direction.PosQuad => new(1, 1),
        Direction.YPos => new(0, 1),
        Direction.NegPosQuad => new(-1, 1),
        Direction.XNeg => new(-1, 0),
        Direction.NegQuad => new(-1, -1),
        _ => throw new ArgumentException()
    };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction FindDirection(Math.Sign x, Math.Sign y) => (Direction)((int)x << 2 | (int)y);//how to reduce casts?, will (int) work?

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction FindDirection(bool xNeg, bool xPos, bool yNeg, bool yPos) => FindDirection(Math.FindSign(xNeg, xPos), Math.FindSign(yNeg, yPos));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Direction RotateDirection(Direction direction, int steps) => direction switch
    {
        Direction.Zero => Direction.Zero,//this doesn't incorporate steps
        Direction.YNeg => Direction.PosNegQuad,
        Direction.PosNegQuad => Direction.XPos,
        Direction.XPos => Direction.PosQuad,
        Direction.PosQuad => Direction.YPos,
        Direction.YPos => Direction.NegPosQuad,
        Direction.NegPosQuad => Direction.XNeg,
        Direction.XNeg => Direction.NegQuad,
        Direction.NegQuad => Direction.YNeg,
        _ => throw new ArgumentException()
    };

    public static void FindSigns(Direction direction, out Math.Sign x, out Math.Sign y)
    {
        x = (Math.Sign)(((int)direction & 0b11_00) >> 2);
        y = (Math.Sign)((int)direction & 0b00_11);
    }
}