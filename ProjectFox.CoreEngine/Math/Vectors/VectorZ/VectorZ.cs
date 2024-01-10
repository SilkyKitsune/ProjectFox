using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 3D int vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct VectorZ : IVector<VectorZ, int, VectorZF>
{
    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    /// <param name="z"> z value of the vector </param>
    public VectorZ(int x, int y, int z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    ///
    public int x, y, z;

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
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
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
}
