using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 3D int vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct VectorZ : IVector<VectorZ, int, VectorZF>
{
    ///
    public int x, y, z;

    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    /// <param name="z"> z value of the vector </param>
    public VectorZ(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    /// <returns> (x XOR y XOR z) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => x ^ y ^ z;

    /// <returns> "(X: {x}, Y: {y}, Z: {z})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(X: {x}, Y: {y}, Z: {z})";

    /// <returns> "(X: {(0x)x}, Y: {(0x)y}, Z: {(0x)z})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(X: {Strings.ToHexString(x, littleEndian, leadingText)}, Y: {Strings.ToHexString(y, littleEndian, leadingText)}, Z: {Strings.ToHexString(z, littleEndian, leadingText)})";

    /// <returns> "(X: {(0b)x}, Y: {(0b)y}, Z: {(0b)z})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, char byteSeparator = '|', char nibbleSeparator = '_', bool leadingText = false) =>
        $"(X: {Strings.ToBinString(x, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Y: {Strings.ToBinString(y, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Z: {Strings.ToBinString(z, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Vector Methods
    public float Distance(VectorZ value)
    {
        if (Equals(value)) return 0f;

        int xDelta = x - value.x, yDelta = y - value.y, zDelta = z - value.z;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;
        if (zDelta < 0) zDelta = -zDelta;

        return Math.SqrRoot((xDelta * xDelta) + (yDelta * yDelta) + (zDelta * zDelta));
    }

    public float DistanceFromZero()
    {
        if (IsZero()) return 0f;

        int x = this.x < 0 ? -this.x : this.x;
        int y = this.y < 0 ? -this.y : this.y;
        int z = this.z < 0 ? -this.z : this.z;

        return Math.SqrRoot((x * x) + (y * y) + (z * z));
    }

    public float DistanceFromZeroSquared()
    {
        if (IsZero()) return 0f;

        int x = this.x < 0 ? -this.x : this.x;
        int y = this.y < 0 ? -this.y : this.y;
        int z = this.z < 0 ? -this.z : this.z;

        return (x * x) + (y * y) + (z * z);
    }

    public float DistanceSquared(VectorZ value)
    {
        if (Equals(value)) return 0f;

        int xDelta = x - value.x, yDelta = y - value.y, zDelta = z - value.z;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;
        if (zDelta < 0) zDelta = -zDelta;

        return (xDelta * xDelta) + (yDelta * yDelta) + (zDelta * zDelta);
    }
    #endregion

    #region Operators
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
    #endregion
}
