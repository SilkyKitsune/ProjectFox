using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorZ
{
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static VectorF Angle(VectorZ a, VectorZ b, VectorZ pivot = default) => default;

    //public static float Angle(VectorZ a, VectorZ b, VectorZ pivot = default) => default;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorZF PointFromRotationOrigin(VectorF angle)
    {
        Math.SineCosine(angle.x, out float sinA, out float cosA);
        Math.SineCosine(angle.y, out float sinB, out float cosB);
        return new(sinA * cosB, -cosA * cosB, sinB);
    }

    //public static VectorZF PointFromRotationOriginTowardPoint(float angle, VectorZF destination) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorF AngleFromRotationOrigin()
    {
#if DEBUG
        bool xZero = x == 0, yZero = y == 0, zZero = z == 0;

        if (xZero && yZero && zZero) return new(0f, 0f);

        if (xZero)
        {
            if (yZero) return new(0f, z < 0 ? 0.25f : 0.75f);

            //if (zZero) return new();

            //return new(0f, angle around x axis)
        }

        if (yZero)
        {
            if (zZero) return new(x < 0 ? 0.25f : 0.75f, 0f);

            //angle around y

            Vector v = new(z, x < 0 ? x : -x);
            return new(0.25f, v.AngleFromRotationOrigin());
        }

        if (zZero)
        {
            //angle around z, ie normal 2d angle

            Vector v = new(x, y);
            return new(v.AngleFromRotationOrigin(), 0f);//?
        }

        //?

        //somewhere in one of eight possible quadrants

        return new();
#else
        return default;
#endif
    }

    //public float AngleFromRotationOrigin() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorZF Rotate(VectorF amount, VectorZF pivot = default) => default;

    //public VectorZF RotateAroundX(float amount, VectorZF pivot = default) => default;
    //public VectorZF RotateAroundY(float amount, VectorZF pivot = default) => default;
    //public VectorZF RotateAroundZ(float amount, VectorZF pivot = default) => default;

    //public VectorZF RotateTowardPoint(VectorF amount, VectorZF destination, VectorZF pivot = default) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorZ RotateByRightAngles(Vector rightAngles) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public VectorZF RotateByRightAngles(Vector rightAngles, VectorZF pivot = default) => default;
}