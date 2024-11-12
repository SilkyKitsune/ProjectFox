using System;
using System.Runtime.CompilerServices;
using D = ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorF
{
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

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator &(VectorF vf1, VectorF vf2) => new(D.ANDFloat32(vf1.x, vf2.x), D.ANDFloat32(vf1.y, vf2.y));

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator |(VectorF vf1, VectorF vf2) => new(D.ORFloat32(vf1.x, vf2.x), D.ORFloat32(vf1.y, vf2.y));

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator ^(VectorF vf1, VectorF vf2) => new(D.XORFloat32(vf1.x, vf2.x), D.XORFloat32(vf1.y, vf2.y));
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

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator &(VectorF vf, float f) => new(D.ANDFloat32(vf.x, f), D.ANDFloat32(vf.y, f));

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator |(VectorF vf, float f) => new(D.ORFloat32(vf.x, f), D.ORFloat32(vf.y, f));

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator ^(VectorF vf, float f) => new(D.XORFloat32(vf.x, f), D.XORFloat32(vf.y, f));
    #endregion

    #region float_vectorf
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator -(float f, VectorF vf) => new(f - vf.x, f - vf.y);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator /(float f, VectorF vf)
    {
        if (vf.x == 0f || vf.y == 0f) throw new DivideByZeroException();
        return new(f / vf.x, f / vf.y);
    }

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF operator %(float f, VectorF vf)
    {
        if (vf.x == 0f || vf.y == 0f) throw new DivideByZeroException();
        return new(f % vf.x, f % vf.y);
    }
    #endregion
}