using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 3D float vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct VectorZF : IVector<VectorZF, float, VectorZF>
{
    ///
    public float x, y, z;

    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    /// <param name="z"> z value of the vector </param>
    public VectorZF(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <returns> ((x XOR y) XOR z) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();

    /// <returns> "(X: {x}, Y: {y}, Z: {z})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(X: {x}, Y: {y}, Z: {z})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(X: {Strings.ToHexString(x, littleEndian, leadingText)}, Y: {Strings.ToHexString(y, littleEndian, leadingText)}, Z: {Strings.ToHexString(z, littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, char byteSeparator = '|', char nibbleSeparator = '_', bool leadingText = false) =>
        $"(X: {Strings.ToBinString(x, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Y: {Strings.ToBinString(y, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Z: {Strings.ToBinString(z, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Vector Methods
    public float Distance(VectorZF value)
    {
        if (Equals(value)) return 0f;
        
        float xDelta = x - value.x, yDelta = y - value.y, zDelta = z - value.z;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;
        if (zDelta < 0) zDelta = -zDelta;

        return Math.SqrRoot((xDelta * xDelta) + (yDelta * yDelta) + (zDelta * zDelta));
    }

    public float DistanceFromZero()
    {
        if (IsZero()) return 0f;

        float x = this.x < 0 ? -this.x : this.x;
        float y = this.y < 0 ? -this.y : this.y;
        float z = this.z < 0 ? -this.z : this.z;

        return Math.SqrRoot((x * x) + (y * y) + (z * z));
    }

    public float DistanceFromZeroSquared()
    {
        if (IsZero()) return 0f;

        float x = this.x < 0 ? -this.x : this.x;
        float y = this.y < 0 ? -this.y : this.y;
        float z = this.z < 0 ? -this.z : this.z;

        return (x * x) + (y * y) + (z * z);
    }

    public float DistanceSquared(VectorZF value)
    {
        if (Equals(value)) return 0f;

        float xDelta = x - value.x, yDelta = y - value.y, zDelta = z - value.z;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;
        if (zDelta < 0) zDelta = -zDelta;

        return (xDelta * xDelta) + (yDelta * yDelta) + (zDelta * zDelta);
    }
    #endregion

    #region Operators
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
    #endregion
}
