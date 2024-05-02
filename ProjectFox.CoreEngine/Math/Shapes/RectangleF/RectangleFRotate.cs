using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value, VectorF pivot = default) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float AngleFromRotationOrigin() => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF Rotate(float amount, VectorF pivot = default) => new(position.Rotate(amount, pivot), size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF RotateByRightAngles(int rightAngles) => new(position.RotateByRightAngles(rightAngles), size);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public RectangleF RotateByRightAngles(int rightAngles, VectorF pivot = default) => new(position.RotateByRightAngles(rightAngles, pivot), size);
}