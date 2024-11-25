using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static float Angle(Vector a, Vector b, Vector pivot = default)
    {
#if DEBUG
        if (a.Equals(b)) return 0f;

        if (!pivot.IsZero())
        {
            a.x -= pivot.x;
            a.y -= pivot.y;
            b.x -= pivot.x;
            b.y -= pivot.y;
        }

        //any checks for right angles? or is that already covered by AngleFromRotationOrigin()?

        float a_ = a.AngleFromRotationOrigin(), b_ = b.AngleFromRotationOrigin();

        if (a_ < b_) return b_ - a_;
        if (a_ > b_) return a_ - b_;
        return a_;//this is unnecessary because if angles are equal then the points should be too
#else
        return default;
#endif
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static VectorF PointFromRotationOrigin(float angle)
    {
        Math.SineCosine(angle, out float sin, out float cos);//i think cos is coming out 0 when angle is 0
        return new(sin, -cos);
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float AngleFromRotationOrigin()//this doesn't completely work
    {
#if DEBUG
        bool x = this.x == 0, y = this.y == 0;

        if (x && y) return 0f;

        if (x) return this.y < 0 ? 0f : 0.5f;

        if (y) return this.x < 0 ? 0.75f : 0.25f;

        x = this.x < 0;
        y = this.y < 0;

        float xAbs = x ? -this.x : this.x, yAbs = y ? -this.y : this.y,
            angle = ((x ? 0b00 : 0b10) | (y ? 0b00 : 0b01)) switch
    {
            0b10 => 0f,
            0b11 => 0.25f,
            0b01 => 0.5f,
            0b00 => 0.75f,
            _ => throw new Exception()//message?
        };
        
        if (xAbs == yAbs) return angle + 0.125f;

        float q = xAbs > yAbs ? yAbs / xAbs : xAbs / yAbs;

        //if ()
        //?
        //each second 8th needs 1-q

        if (xAbs > yAbs) return (1 - (yAbs / xAbs)) * 0.125f + 0.125f + angle;

        return (xAbs / yAbs) * 0.125f + angle;
#else
        return default;
#endif

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