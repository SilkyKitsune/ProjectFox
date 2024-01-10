using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZ
{
    #region vectorz
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Vector(VectorZ vz) => new(vz.x, vz.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator VectorF(VectorZ vz) => new(vz.x, vz.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator VectorZF(VectorZ vz) => new(vz.x, vz.y, vz.z);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator ++(VectorZ vz) => new(vz.x + 1, vz.y + 1, vz.z + 1);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator --(VectorZ vz) => new(vz.x - 1, vz.y - 1, vz.z - 1);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator -(VectorZ vz) => new(-vz.x, -vz.y, -vz.z);
    #endregion

    #region vectorz_vectorz
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorZ vz1, VectorZ vz2) => vz1.Equals(vz2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorZ vz1, VectorZ vz2) => !vz1.Equals(vz2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator +(VectorZ vz1, VectorZ vz2) => new(vz1.x + vz2.x, vz1.y + vz2.x, vz1.z + vz2.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator -(VectorZ vz1, VectorZ vz2) => new(vz1.x - vz2.x, vz1.y - vz2.x, vz1.z - vz2.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator *(VectorZ vz1, VectorZ vz2) => new(vz1.x * vz2.x, vz1.y * vz2.x, vz1.z * vz2.x);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator /(VectorZ vz1, VectorZ vz2)
    {
        if (vz2.x == 0 || vz2.y == 0 || vz2.z == 0) throw new DivideByZeroException();
        return new(vz1.x / vz2.x, vz1.y / vz2.x, vz1.z / vz2.x);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator %(VectorZ vz1, VectorZ vz2)
    {
        if (vz2.x == 0 || vz2.y == 0 || vz2.z == 0) throw new DivideByZeroException();
        return new(vz1.x % vz2.x, vz1.y % vz2.x, vz1.z % vz2.x);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator &(VectorZ vz1, VectorZ vz2) => new(vz1.x & vz2.x, vz1.y & vz2.x, vz1.z & vz2.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator |(VectorZ vz1, VectorZ vz2) => new(vz1.x | vz2.x, vz1.y | vz2.x, vz1.z | vz2.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator ^(VectorZ vz1, VectorZ vz2) => new(vz1.x ^ vz2.x, vz1.y ^ vz2.x, vz1.z ^ vz2.x);
    #endregion

    #region vectorz_vectorzf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorZ vz, VectorZF vzf) => vz.Equals(vzf);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorZ vz, VectorZF vzf) => !vz.Equals(vzf);
    #endregion

    #region vectorz_int
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorZ vz, int i) => vz.Equals(i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorZ vz, int i) => !vz.Equals(i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator +(VectorZ vz, int i) => new(vz.x + i, vz.y + i, vz.z + i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator -(VectorZ vz, int i) => new(vz.x - i, vz.y - i, vz.z - i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator *(VectorZ vz, int i) => new(vz.x * i, vz.y * i, vz.z * i);

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator /(VectorZ vz, int i)
    {
        if (i == 0) throw new DivideByZeroException();
        return new(vz.x / i, vz.y / i, vz.z / i);
    }

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator %(VectorZ vz, int i)
    {
        if (i == 0) throw new DivideByZeroException();
        return new(vz.x % i, vz.y % i, vz.z % i);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator &(VectorZ vz, int i) => new(vz.x & i, vz.y & i, vz.z & i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator |(VectorZ vz, int i) => new(vz.x | i, vz.y | i, vz.z | i);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZ operator ^(VectorZ vz, int i) => new(vz.x ^ i, vz.y ^ i, vz.z ^ i);
    #endregion

    #region vectorz_float
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator +(VectorZ vz, float f) => new(vz.x + f, vz.y + f, vz.z + f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(VectorZ vz, float f) => new(vz.x - f, vz.y - f, vz.z - f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator *(VectorZ vz, float f) => new(vz.x * f, vz.y * f, vz.z * f);

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator /(VectorZ vz, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(vz.x / f, vz.y / f, vz.z / f);
    }

    /// <exception cref="DivideByZeroException"/>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator %(VectorZ vz, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(vz.x % f, vz.y % f, vz.z % f);
    }
    #endregion
}
