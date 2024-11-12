using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZF
{
    #region vectorzf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Vector(VectorZF vzf) => new((int)vzf.x, (int)vzf.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator VectorF(VectorZF vzf) => new(vzf.x, vzf.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator VectorZ(VectorZF vzf) => new((int)vzf.x, (int)vzf.y, (int)vzf.z);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator ++(VectorZF vzf) => new(vzf.x + 1f, vzf.y + 1f, vzf.z + 1f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator --(VectorZF vzf) => new(vzf.x - 1f, vzf.y - 1f, vzf.z - 1f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(VectorZF vzf) => new(-vzf.x, -vzf.y, -vzf.z);
    #endregion

    #region vectorzf_vectorzf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorZF vzf1, VectorZF vzf2) => vzf1.Equals(vzf2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorZF vzf1, VectorZF vzf2) => !vzf1.Equals(vzf2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator +(VectorZF vzf1, VectorZF vzf2) => new(vzf1.x + vzf2.x, vzf1.x + vzf2.x, vzf1.x + vzf2.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(VectorZF vzf1, VectorZF vzf2) => new(vzf1.x - vzf2.x, vzf1.x - vzf2.x, vzf1.x - vzf2.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator *(VectorZF vzf1, VectorZF vzf2) => new(vzf1.x * vzf2.x, vzf1.x * vzf2.x, vzf1.x * vzf2.x);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator /(VectorZF vzf1, VectorZF vzf2)
    {
        if (vzf2.x == 0f || vzf2.y == 0f || vzf2.z == 0f) throw new DivideByZeroException();
        return new(vzf1.x / vzf2.x, vzf1.x / vzf2.x, vzf1.x / vzf2.x);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator %(VectorZF vzf1, VectorZF vzf2)
    {
        if (vzf2.x == 0f || vzf2.y == 0f || vzf2.z == 0f) throw new DivideByZeroException();
        return new(vzf1.x % vzf2.x, vzf1.x % vzf2.x, vzf1.x % vzf2.x);
    }
    #endregion

    #region vectorzf_vectorz
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorZF vzf, VectorZ vz) => vzf.Equals(vz);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorZF vzf, VectorZ vz) => !vzf.Equals(vz);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator +(VectorZF vzf, VectorZ vz) => new(vzf.x + vz.x, vzf.x + vz.x, vzf.x + vz.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(VectorZF vzf, VectorZ vz) => new(vzf.x - vz.x, vzf.x - vz.x, vzf.x - vz.x);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator *(VectorZF vzf, VectorZ vz) => new(vzf.x * vz.x, vzf.x * vz.x, vzf.x * vz.x);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator /(VectorZF vzf, VectorZ vz)
    {
        if (vz.x == 0 || vz.y == 0 || vz.z == 0) throw new DivideByZeroException();
        return new(vzf.x / vz.x, vzf.x / vz.x, vzf.x / vz.x);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator %(VectorZF vzf, VectorZ vz)
    {
        if (vz.x == 0 || vz.y == 0 || vz.z == 0) throw new DivideByZeroException();
        return new(vzf.x % vz.x, vzf.x % vz.x, vzf.x % vz.x);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator +(VectorZ vz, VectorZF vzf) => new(vz.x + vzf.x, vz.y + vzf.y, vz.z + vzf.z);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(VectorZ vz, VectorZF vzf) => new(vz.x - vzf.x, vz.y - vzf.y, vz.z - vzf.z);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator *(VectorZ vz, VectorZF vzf) => new(vz.x * vzf.x, vz.y * vzf.y, vz.z * vzf.z);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator /(VectorZ vz, VectorZF vzf)
    {
        if (vzf.x == 0f || vzf.y == 0f || vzf.z == 0f) throw new DivideByZeroException();
        return new(vz.x / vzf.x, vz.y / vzf.y, vz.z / vzf.z);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator %(VectorZ vz, VectorZF vzf)
    {
        if (vzf.x == 0f || vzf.y == 0f || vzf.z == 0f) throw new DivideByZeroException();
        return new(vz.x % vzf.x, vz.y % vzf.y, vz.z % vzf.z);
    }
    #endregion

    #region vectorzf_float
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(VectorZF vzf, float f) => vzf.Equals(f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(VectorZF vzf, float f) => !vzf.Equals(f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator +(VectorZF vzf, float f) => new(vzf.x + f, vzf.x + f, vzf.x + f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(VectorZF vzf, float f) => new(vzf.x - f, vzf.x - f, vzf.x - f);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator *(VectorZF vzf, float f) => new(vzf.x * f, vzf.x * f, vzf.x * f);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator /(VectorZF vzf, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(vzf.x / f, vzf.x / f, vzf.x / f);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator %(VectorZF vzf, float f)
    {
        if (f == 0f) throw new DivideByZeroException();
        return new(vzf.x % f, vzf.x % f, vzf.x % f);
    }
    #endregion

    #region float_vectorzf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator -(float f, VectorZF vzf) => new(f - vzf.x, f - vzf.y, f - vzf.z);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator /(float f, VectorZF vzf)
    {
        if (vzf.x == 0f || vzf.y == 0f || vzf.z == 0f) throw new DivideByZeroException();
        return new(f / vzf.x, f / vzf.y, f / vzf.z);
}

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF operator %(float f, VectorZF vzf)
    {
        if (vzf.x == 0f || vzf.y == 0f || vzf.z == 0f) throw new DivideByZeroException();
        return new(f % vzf.x, f % vzf.y, f % vzf.z);
    }
    #endregion
}