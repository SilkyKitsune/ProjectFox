using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.CoreEngine.Math;

/// <summary> 3D float vector </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct VectorZF : IVector<VectorZF, float, VectorZF>
{
    /// <param name="x"> x value of the vector </param>
    /// <param name="y"> y value of the vector </param>
    /// <param name="z"> z value of the vector </param>
    public VectorZF(float x, float y, float z)
    {
        this.x = x;
        this.y = y;
        this.z = z;
    }

    ///
    public float x, y, z;

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
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
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
}
