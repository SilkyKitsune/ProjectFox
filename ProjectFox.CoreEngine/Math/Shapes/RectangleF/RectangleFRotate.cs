using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    #region Angle
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float AngleFromRotationOrigin() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value, VectorF refPoint) => default;
    #endregion

    #region Rotate
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Rotate(float amount, VectorF pivot = default) => new(position.Rotate(amount, pivot), size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF RotateByRadians(float radians, VectorF pivot = default) => new(position.RotateByRadians(radians, pivot), size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF RotateByDegrees(float degrees, VectorF pivot = default) => new(position.RotateByDegrees(degrees, pivot), size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF RotateByRightAngles(int rightAngles) => new(position.RotateByRightAngles(rightAngles), size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF RotateByRightAngles(VectorF refPoint, int rightAngles) => new(position.RotateByRightAngles(refPoint, rightAngles), size);
    #endregion
}