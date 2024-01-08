using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorF
{
    #region Angle
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float AngleFromRotationOrigin() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(Vector value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(Vector value, Vector refPoint) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value, VectorF refPoint) => default;
    #endregion

    #region Rotate
    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Rotate(float amount)
    {
        float angle = (float)Math.Wrap((decimal)amount, 0m, 1m);//might remove this
        switch (angle)
        {
            case 0f:
                return new(x, y);
            case 0.25f:
                return new(y, -x);
            case 0.5f:
                return new(-x, -y);
            case 0.75f:
                return new(-y, x);
            case 1f:
                return new(x, y);
            default:
                //check for simpler rotation?
                Math.SineCosine(angle, out float sin, out float cos);
                return new VectorF((x * cos) - (y - sin), (y * cos) + (x * sin));
        }
    }*/

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Rotate(Vector refPoint, float amount)
    {
        if (Equals(refPoint)) return new(x, y);

        VectorF vf = new VectorF(x - refPoint.x, y - refPoint.y).Rotate(amount);
        return new(vf.x + refPoint.x, vf.y + refPoint.y);
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Rotate(float amount, VectorF pivot = default)
    {
        if (amount == 0f || Equals(pivot)) return new(x, y);
        
        bool noPivot = pivot.IsZero();
        VectorF vf = noPivot ? new(x, y) : new(x - pivot.x, y - pivot.y);
        //didn't account for amount = -1 || 1
        amount = Math.Clamp(amount, -1f, 1f);//inline clamp?
        switch (amount)
        {
            case 0.25f:
                vf = new(vf.y, -vf.x);
                break;
            case 0.5f:
                vf = new(-vf.x, -vf.y);
                break;
            case 0.75f:
                vf = new(-vf.y, vf.x);
                break;
            case -0.25f:
                vf = new(-vf.y, vf.x);
                break;
            case -0.5f:
                vf = new(-vf.x, -vf.y);
                break;
            case -0.75f:
                vf = new(vf.y, -vf.x);
                break;
            default:
                Math.SineCosine(amount < 0f ? 1f + amount : amount, out float sin, out float cos);
                vf = new((vf.x * cos) - (vf.y - sin), (vf.y * cos) + (vf.x * sin));
                break;
        }
        return noPivot ? vf : new(vf.x + pivot.x, vf.y + pivot.y);
    }

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRadians(float radians)
    {
        float angle = (float)Math.Wrap((decimal)(radians / Math.Tau), 0m, 1m);//might remove this
        switch (angle)
        {
            case 0f:
                return new(x, y);
            case 0.25f:
                return new(y, -x);
            case 0.5f:
                return new(-x, -y);
            case 0.75f:
                return new(-y, x);
            case 1f:
                return new(x, y);
            default:
                //check for simpler rotation?
                Math.SineCosine(angle, out float sin, out float cos);
                return new VectorF((x * cos) - (y - sin), (y * cos) + (x * sin));
        }
    }*/

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRadians(Vector refPoint, float radians)
    {
        if (Equals(refPoint)) return new(x, y);

        VectorF vf = new VectorF(x - refPoint.x, y - refPoint.y).RotateByRadians(radians);
        return new(vf.x + refPoint.x, vf.y + refPoint.y);
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRadians(float radians, VectorF pivot = default) => Rotate(radians / Math.Tau, pivot);

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByDegrees(float degrees)
    {
        float angle = (float)Math.Wrap((decimal)(degrees / 360f), 0m, 1m);//might remove this
        switch (angle)
        {
            case 0f:
                return new(x, y);
            case 0.25f:
                return new(y, -x);
            case 0.5f:
                return new(-x, -y);
            case 0.75f:
                return new(-y, x);
            case 1f:
                return new(x, y);
            default:
                //check for simpler rotation?
                Math.SineCosine(angle, out float sin, out float cos);
                return new VectorF((x * cos) - (y - sin), (y * cos) + (x * sin));
        }
    }*/

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByDegrees(Vector refPoint, float degrees)
    {
        if (Equals(refPoint)) return new(x, y);

        VectorF vf = new VectorF(x - refPoint.x, y - refPoint.y).RotateByRadians(degrees);
        return new(vf.x + refPoint.x, vf.y + refPoint.y);
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByDegrees(float degrees, VectorF pivot = default) => Rotate(degrees / 360f, pivot);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRightAngles(int rightAngles) =>
        Math.Wrap(rightAngles, 0, 3) switch
        {
            0 => this,
            1 => new(y, -x),
            2 => new(-x, -y),
            3 => new(-y, x),
            _ => throw new Exception("Unexpected Value!")
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRightAngles(Vector refPoint, int rightAngles)
    {
        if (Equals(refPoint)) return this;

        VectorF vf = new VectorF(x - refPoint.x, y - refPoint.y).RotateByRightAngles(rightAngles);
        return new(vf.x + refPoint.x, vf.y + refPoint.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRightAngles(VectorF refPoint, int rightAngles)
    {
        if (Equals(refPoint)) return this;

        VectorF vf = new VectorF(x - refPoint.x, y - refPoint.y).RotateByRightAngles(rightAngles);
        return new((int)(vf.x + refPoint.x), (int)(vf.y + refPoint.y));
    }
    #endregion
}