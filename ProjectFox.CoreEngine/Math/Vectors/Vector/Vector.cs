using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 2D int vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct Vector : IVector<Vector, int, VectorF>, IDirection<Vector>, IRotate2D<Vector, Vector, VectorF, VectorF>
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
        $"(X: {Strings.ToHexString(x, littleEndian, leadingText)}, Y: {Strings.ToHexString(y, littleEndian, leadingText)})";

    /// <returns> "(X: {(0b)x}, Y: {(0b)y})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, char byteSeparator = '|', char nibbleSeparator = '_', bool leadingText = false) =>
        $"(X: {Strings.ToBinString(x, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Y: {Strings.ToBinString(y, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

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

    #region Operators
    #region vector
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator VectorF(Vector v) => new(v.x, v.y);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator VectorZ(Vector v) => new(v.x, v.y, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator VectorZF(Vector v) => new(v.x, v.y, 0f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator ++(Vector v) => new(v.x + 1, v.y + 1);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator --(Vector v) => new(v.x - 1, v.y - 1);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator -(Vector v) => new(-v.x, -v.y);

    /// <summary> swaps the x and y values </summary>
    /// <remarks> Not a bitwise operator! </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator ~(Vector v) => new(v.y, v.x);
    #endregion

    #region vector_vector
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector v1, Vector v2) => v1.Equals(v2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector v1, Vector v2) => !v1.Equals(v2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator +(Vector v1, Vector v2) => new(v1.x + v2.x, v1.y + v2.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator -(Vector v1, Vector v2) => new(v1.x - v2.x, v1.y - v2.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator *(Vector v1, Vector v2) => new(v1.x * v2.x, v1.y * v2.y);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator /(Vector v1, Vector v2)
    {
        if (v2.x == 0 || v2.y == 0) throw new DivideByZeroException();
        return new(v1.x / v2.x, v1.y / v2.y);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator %(Vector v1, Vector v2)
    {
        if (v2.x == 0 || v2.y == 0) throw new DivideByZeroException();
        return new(v1.x % v2.x, v1.y % v2.y);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator &(Vector v1, Vector v2) => new(v1.x & v2.x, v1.y & v2.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator |(Vector v1, Vector v2) => new(v1.x | v2.x, v1.y | v2.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator ^(Vector v1, Vector v2) => new(v1.x ^ v2.x, v1.y ^ v2.y);
    #endregion

    #region vector_vectorf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector v, VectorF vf) => vf.Equals(v);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector v, VectorF vf) => !vf.Equals(v);
    #endregion

    #region vector_int
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Vector v, int i) => v.Equals(i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Vector v, int i) => !v.Equals(i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator +(Vector v, int i) => new(v.x + i, v.y + i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator -(Vector v, int i) => new(v.x - i, v.y - i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator *(Vector v, int i) => new(v.x * i, v.y * i);

    /// <exception cref="DivideByZeroException"/>
    public static Vector operator /(Vector v, int i)
    {
        if (i == 0) throw new DivideByZeroException();
        return new(v.x / i, v.y / i);
    }

    /// <exception cref="DivideByZeroException"/>
    public static Vector operator %(Vector v, int i)
    {
        if (i == 0) throw new DivideByZeroException();
        return new(v.x % i, v.y % i);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator &(Vector v, int i) => new(v.x & i, v.y & i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator |(Vector v, int i) => new(v.x | i, v.y | i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Vector operator ^(Vector v, int i) => new(v.x ^ i, v.y ^ i);
    #endregion

    #region vector_float
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator +(Vector v, float f) => new(v.x + f, v.y + f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(Vector v, float f) => new(v.x - f, v.y - f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator *(Vector v, float f) => new(v.x * f, v.y * f);

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator /(Vector v, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(v.x / f, v.y / f);
    }

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator %(Vector v, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(v.x % f, v.y % f);
    }
    #endregion
    #endregion
}
