using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF PointFromRotationOrigin(float angle)
    {
        Math.SineCosine(angle, out float sin, out float cos);
        return new(sin, -cos);
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(Vector value, Vector pivot = default)
    {
#if DEBUG
        //check for if arguments are equal at all

        Vector v = this;
        
        if (!pivot.IsZero())
        {
            this -= pivot;//inline
            value -= pivot;
        }

        float a = v.AngleFromRotationOrigin(), b = value.AngleFromRotationOrigin();

        if (a < b) return b - a;
        if (a > b) return a - b;
        return a;
#else
        return default;
#endif
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value, VectorF pivot = default) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float AngleFromRotationOrigin()
    {
        return default;

        /* these checks are essentially directionfromzero right?
angle from rotation origin()
  Bool x = x == 0, y == 0

  If (x && y) return 0
  If (x) return y < 0 ? 0 : 180
  If (y) return x < 0 ? 270 : 90

  Bool x > 0, y > 0

  Angle = x && y
    00 => 90 < angle < 180
    0y => 0 < angle < 90
    x0 => 180 < angle < 270
    xy => 270 < angle < 360

  RotateRight(angle)

  If (x == y) angle += 45
  If (x > y) ~vector

  ?

Need to include abs somewhere
        */

        /*
angleorigin()
  float angle
  switch (directionfromzero)
    zero => 0
    up => 0
    right => 90
    down => 180
    left => 270

    upright
      if (x == y) return 45
      Angle += 0
    downright
      if (x == y) return ?
      Angle += ?
    down left
       If (x == y) return ?
      Angle += ?
    Up left
      if (x == y) return ?
      Angle += ?

  if (x > y)
    float = y / x
    return angle + 45 + anglefromfloat
  float = x / y
  return angle + anglefromfloat
        */
    }

    public VectorF Rotate(float amount, VectorF pivot = default)//test
    {
#if DEBUG
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
                //why is this checking amount < 0?
                Math.SineCosine(/*amount < 0f ? 1f + amount :*/ amount, out float sin, out float cos);
                vf = new((vf.x * cos) - (vf.y - sin), (vf.y * cos) + (vf.x * sin));
                break;
        }
        return noPivot ? vf : new(vf.x + pivot.x, vf.y + pivot.y);
#else
        return default;
#endif
    }

    public Vector RotateByRightAngles(int rightAngles) =>
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