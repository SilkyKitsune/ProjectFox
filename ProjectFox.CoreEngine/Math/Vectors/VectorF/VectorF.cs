using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using D = ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 2D float vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct VectorF : IVector<VectorF, float, VectorF>, IDirection<VectorF>, IRotate2D<VectorF, VectorF, VectorF, VectorF>
{
    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    public VectorF(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    ///
    public float x, y;

    /// <returns> (x XOR y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();//could this work?

    /// <returns> "(X: {x}, Y: {y})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(X: {x}, Y: {y})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(X: {D.ToHexString(x, littleEndian, leadingText)}, Y: {D.ToHexString(y, littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(X: {D.ToBinString(x, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Y: {D.ToBinString(y, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Vector Methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionFromZero() => Vector.FindDirection(
        x < 0 ? Math.Sign.Neg : (x > 0 ? Math.Sign.Pos : Math.Sign.Zero),
        y < 0 ? Math.Sign.Neg : (y > 0 ? Math.Sign.Pos : Math.Sign.Zero));

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionToPoint(VectorF value) => Vector.FindDirection(
        x < value.x ? Math.Sign.Neg : (x > value.x ? Math.Sign.Pos : Math.Sign.Zero),
        y < value.y ? Math.Sign.Neg : (y > value.y ? Math.Sign.Pos : Math.Sign.Zero));

    public float Distance(VectorF value)
    {
        if (Equals(value)) return 0f;

        float xDelta = x - value.x, yDelta = y - value.y;

        if (xDelta < 0f) xDelta = -xDelta;
        if (yDelta < 0f) yDelta = -yDelta;

        return Math.SqrRoot((xDelta * xDelta) + (yDelta * yDelta));
    }

    public float DistanceFromZero()
    {
        if (IsZero()) return 0f;

        float x = this.x < 0f ? -this.x : this.x;
        float y = this.y < 0f ? -this.y : this.y;

        return Math.SqrRoot((x * x) + (y * y));
    }

    public float DistanceFromZeroSquared()
    {
        if (IsZero()) return 0f;

        float x = this.x < 0f ? -this.x : this.x;
        float y = this.y < 0f ? -this.y : this.y;

        return (x * x) + (y * y);
    }

    public float DistanceSquared(VectorF value)
    {
        if (Equals(value)) return 0f;

        float xDelta = x - value.x, yDelta = y - value.y;

        if (xDelta < 0f) xDelta = -xDelta;
        if (yDelta < 0f) yDelta = -yDelta;

        return (xDelta * xDelta) + (yDelta * yDelta);
    }
    #endregion
}
