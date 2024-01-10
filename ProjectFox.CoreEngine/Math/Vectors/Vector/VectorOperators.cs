using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
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
}
