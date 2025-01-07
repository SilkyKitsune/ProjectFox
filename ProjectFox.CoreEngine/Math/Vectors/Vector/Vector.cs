using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using D = ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 2D int vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct Vector : IVector<Vector, int, VectorF>, IDirection<Vector>, IRotate<Vector, VectorF, float, int>, IProjection<Vector, float, Vector, VectorF, int, float, float>
{
    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    public Vector(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    ///
    public int x, y;

    /// <returns> (x XOR y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => x ^ y;

    /// <returns> "(X: {x}, Y: {y})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(X: {x}, Y: {y})";

    /// <returns> "(X: {(0x)x}, Y: {(0x)y})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(X: {D.ToHexString(x, littleEndian, leadingText)}, Y: {D.ToHexString(y, littleEndian, leadingText)})";

    /// <returns> "(X: {(0b)x}, Y: {(0b)y})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(X: {D.ToBinString(x, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Y: {D.ToBinString(y, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Vector Methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Direction DirectionFromZero() => FindDirection(
            x < 0 ? Math.Sign.Neg : (x > 0 ? Math.Sign.Pos : Math.Sign.Zero),
            y < 0 ? Math.Sign.Neg : (y > 0 ? Math.Sign.Pos : Math.Sign.Zero));

    /// <returns> the genereal direction 'value' is, in reference to 'this' </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Direction DirectionToPoint(Vector value) => FindDirection(
        value.x < x ? Math.Sign.Neg : (value.x > x ? Math.Sign.Pos : Math.Sign.Zero),
        value.y < y ? Math.Sign.Neg : (value.y > y ? Math.Sign.Pos : Math.Sign.Zero));

    /// <returns> the distance between 'this' and 'value' </returns>
    public float Distance(Vector value)
    {
        if (Equals(value)) return 0f;

        int xDelta = x - value.x, yDelta = y - value.y;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;

        return Math.SqrRoot((xDelta * xDelta) + (yDelta * yDelta));
    }

    public float DistanceFromZero()
    {
        if (IsZero()) return 0f;

        int x = this.x < 0 ? -this.x : this.x;
        int y = this.y < 0 ? -this.y : this.y;
        return Math.SqrRoot((x * x) + (y * y));
    }

    public float DistanceFromZeroSquared()
    {
        if (IsZero()) return 0f;

        int x = this.x < 0 ? -this.x : this.x;
        int y = this.y < 0 ? -this.y : this.y;
        return (x * x) + (y * y);
    }

    public float DistanceSquared(Vector value)
    {
        if (Equals(value)) return 0f;

        int xDelta = x - value.x, yDelta = y - value.y;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;

        return (xDelta * xDelta) + (yDelta * yDelta);
    }
    #endregion
}