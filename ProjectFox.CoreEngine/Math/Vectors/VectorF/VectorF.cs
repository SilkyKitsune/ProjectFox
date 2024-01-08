using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 2D float vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct VectorF : IVector<VectorF, float, VectorF>, IDirection<VectorF>, IRotate2D<VectorF, VectorF, VectorF, VectorF>
{
    ///
    public float x, y;

    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    public VectorF(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    /// <returns> (x XOR y) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode();//could this work?

    /// <returns> "(X: {x}, Y: {y})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(X: {x}, Y: {y})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(X: {Strings.ToHexString(x, littleEndian, leadingText)}, Y: {Strings.ToHexString(y, littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, char byteSeparator = '|', char nibbleSeparator = '_', bool leadingText = false) =>
        $"(X: {Strings.ToBinString(x, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Y: {Strings.ToBinString(y, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

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

    #region Operators
    #region vectorf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Vector(VectorF vf) => new((int)vf.x, (int)vf.y);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator VectorZ(VectorF vf) => new((int)vf.x, (int)vf.y, 0);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator VectorZF(VectorF vf) => new(vf.x, vf.y, 0f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator ++(VectorF vf) => new(vf.x + 1f, vf.y + 1f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator --(VectorF vf) => new(vf.x - 1f, vf.y - 1f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(VectorF vf) => new(-vf.x, -vf.y);

    /// <summary> swaps the x and y values </summary>
    /// <remarks> Not a bitwise operator! </remarks>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator ~(VectorF vf) => new(vf.y, vf.x);
    #endregion

    #region vectorf_vectorf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorF vf1, VectorF vf2) => vf1.Equals(vf2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorF vf1, VectorF vf2) => !vf1.Equals(vf2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator +(VectorF vf1, VectorF vf2) => new(vf1.x + vf2.x, vf1.y + vf2.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(VectorF vf1, VectorF vf2) => new(vf1.x - vf2.x, vf1.y - vf2.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator *(VectorF vf1, VectorF vf2) => new(vf1.x * vf2.x, vf1.y * vf2.y);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator /(VectorF vf1, VectorF vf2)
    {
        if (vf2.x == 0f || vf2.y == 0f) throw new DivideByZeroException();
        return new(vf1.x / vf2.x, vf1.y / vf2.y);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator %(VectorF vf1, VectorF vf2)
    {
        if (vf2.x == 0f || vf2.y == 0f) throw new DivideByZeroException();
        return new(vf1.x % vf2.x, vf1.y % vf2.y);
    }
    #endregion

    #region vectorf_vector
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorF vf, Vector v) => vf.Equals(v);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorF vf, Vector v) => !vf.Equals(v);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator +(VectorF vf, Vector v) => new(vf.x + v.x, vf.y + v.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(VectorF vf, Vector v) => new(vf.x - v.x, vf.y - v.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator *(VectorF vf, Vector v) => new(vf.x * v.x, vf.y * v.y);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator /(VectorF vf, Vector v)
    {
        if (v.x == 0 || v.y == 0) throw new DivideByZeroException();
        return new(vf.x / v.x, vf.y / v.y);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator %(VectorF vf, Vector v)
    {
        if (v.x == 0 || v.y == 0) throw new DivideByZeroException();
        return new(vf.x % v.x, vf.y % v.y);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator +(Vector v, VectorF vf) => new(v.x + vf.x, v.y + vf.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(Vector v, VectorF vf) => new(v.x - vf.x, v.y - vf.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator *(Vector v, VectorF vf) => new(v.x * vf.x, v.y * vf.y);

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator /(Vector v, VectorF vf)
    {
        if (vf.x == 0f || vf.y == 0f) throw new DivideByZeroException();
        return new(v.x / vf.x, v.y / vf.y);
    }

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator %(Vector v, VectorF vf)
    {
        if (vf.x == 0f || vf.y == 0f) throw new DivideByZeroException();
        return new(v.x % vf.x, v.y % vf.y);
    }
    #endregion

    #region vectorf_float
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorF vf, float f) => vf.Equals(f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorF vf, float f) => !vf.Equals(f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator +(VectorF vf, float f) => new(vf.x + f, vf.y + f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(VectorF vf, float f) => new(vf.x - f, vf.y - f);
    
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator *(VectorF vf, float f) => new(vf.x * f, vf.y * f);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator /(VectorF vf, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(vf.x / f, vf.y / f);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator %(VectorF vf, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(vf.x % f, vf.y % f);
    }
    #endregion
    #endregion
}
