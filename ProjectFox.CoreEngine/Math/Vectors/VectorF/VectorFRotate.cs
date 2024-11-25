using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorF
{
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static float Angle(VectorF a, VectorF b, VectorF pivot = default) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float AngleFromRotationOrigin() => default;

    public VectorF Rotate(float amount, VectorF pivot = default)
    {
        amount -= (int)amount;

        if (amount == 0f || Equals(pivot)) return this;

        bool noPivot = pivot.IsZero();
        VectorF vf = noPivot ? new(x, y) : new(x - pivot.x, y - pivot.y);

        switch (amount)
        {
            case 0.25f:
            case -0.75f:
                vf = new(vf.y, -vf.x);
                break;
            case 0.5f:
            case -0.5f:
                vf = new(-vf.x, -vf.y);
                break;
            case 0.75f:
            case -0.25f:
                vf = new(-vf.y, vf.x);
                break;
            default:
                Math.SineCosine(amount, out float sin, out float cos);
                vf = new((vf.x * cos) - (vf.y * sin), (vf.y * cos) + (vf.x * sin));
                break;
        }
        return noPivot ? vf : new(vf.x + pivot.x, vf.y + pivot.y);
    }

    public VectorF RotateByRightAngles(int rightAngles) =>
        Math.Wrap(rightAngles, 0, 3) switch
        {
            0 => this,
            1 => new(y, -x),
            2 => new(-x, -y),
            3 => new(-y, x),
            _ => throw new Exception("Unexpected Value!")
        };

    public VectorF RotateByRightAngles(int rightAngles, VectorF pivot = default)
    {
        rightAngles = Math.Wrap(rightAngles, 0, 3);

        if (rightAngles == 0 || Equals(pivot)) return this;

        bool noPivot = pivot.IsZero();
        VectorF vf = noPivot ? new(x, y) : new(x - pivot.x, y - pivot.y);

        switch (rightAngles)
        {
            case 1:
                vf = new(vf.y, -vf.x);
                break;
            case 2:
                vf = new(-vf.x, -vf.y);
                break;
            case 3:
                vf = new(-vf.y, vf.x);
                break;
        };
        return noPivot ? vf : new(vf.x + pivot.x, vf.y + pivot.y);
    }
}