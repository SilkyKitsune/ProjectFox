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

    #region Angle
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

    public float Angle(Vector value, Vector pivot = default)
    {
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
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Angle(VectorF value, VectorF pivot = default) => default;
    #endregion

    #region Rotate
    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Rotate(float amount)
    {
        if (IsZero()) return this;

        //change to clamp
        //float angle = (float)Math.Wrap((decimal)amount, 0m, 1m);//might remove this
        amount = Math.Clamp(amount, -1f, 1f);//inline clamp?
        switch (amount)
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
                Math.SineCosine(amount < 0f ? 1f + amount : amount, out float sin, out float cos);
                return new VectorF((x * cos) - (y - sin), (y * cos) + (x * sin));
        }
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF Rotate(float amount, VectorF pivot = default)
    {        
        if (amount == 0f || Equals(pivot)) return new(x, y);

        bool noPivot = pivot.IsZero();//vectorf.rotate needs same fixes
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
    public VectorF Rotate(VectorF pivot, float amount)
    {
        if (Equals(pivot)) return new(x, y);

        VectorF vf = new VectorF(x - pivot.x, y - pivot.y).Rotate(amount);
        return new(vf.x + pivot.x, vf.y + pivot.y);
    }*/

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRadians(float radians)
    {
        if (IsZero()) return this;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRadians(float radians, VectorF pivot = default) => Rotate(radians / Math.Tau, pivot);

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByRadians(VectorF pivot, float radians)
    {
        if (Equals(pivot)) return new(x, y);

        VectorF vf = new VectorF(x - pivot.x, y - pivot.y).RotateByRadians(radians);
        return new(vf.x + pivot.x, vf.y + pivot.y);
    }*/

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByDegrees(float degrees)
    {
        if (IsZero()) return this;

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

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByDegrees(float degrees, VectorF pivot = default) => Rotate(degrees / 360f, pivot);

    /*[MethodImpl(MethodImplOptions.AggressiveInlining)]
    public VectorF RotateByDegrees(VectorF pivot, float degrees)
    {
        if (Equals(pivot)) return new(x, y);

        VectorF vf = new VectorF(x - pivot.x, y - pivot.y).RotateByDegrees(degrees);
        return new(vf.x + pivot.x, vf.y + pivot.y);
    }*/

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector RotateByRightAngles(int rightAngles) =>
        Math.Wrap(rightAngles, 0, 3) switch
        {
            0 => this,
            1 => new(y, -x),
            2 => new(-x, -y),
            3 => new(-y, x),
            _ => throw new Exception("Unexpected Value!")
        };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector RotateByRightAngles(Vector pivot, int rightAngles)
    {
        if (Equals(pivot)) return this;

        Vector v = new Vector(x - pivot.x, y - pivot.y).RotateByRightAngles(rightAngles);
        return new(v.x + pivot.x, v.y + pivot.y);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector RotateByRightAngles(VectorF pivot, int rightAngles)
    {
        if (Equals(pivot)) return this;

        VectorF vf = new VectorF(x - pivot.x, y - pivot.y).RotateByRightAngles(rightAngles);
        return new((int)(vf.x + pivot.x), (int)(vf.y + pivot.y));
    }
    #endregion
}