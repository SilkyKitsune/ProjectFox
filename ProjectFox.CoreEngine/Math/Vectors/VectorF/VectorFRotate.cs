﻿using System;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorF
{
    public static float Angle(VectorF a, VectorF b, VectorF pivot = default)
    {
        if (a.Equals(b) || a.Equals(pivot) || b.Equals(pivot)) return 0f;

        if (!pivot.IsZero())
        {
            a.x -= pivot.x;
            a.y -= pivot.y;
            b.x -= pivot.x;
            b.y -= pivot.y;
        }

        float a_ = a.AngleFromRotationOrigin(), b_ = b.AngleFromRotationOrigin();
        return a_ < b_ ? b_ - a_ : a_ - b_;
    }

    public float AngleFromRotationOrigin()
    {
        bool xZero = x == 0, yZero = y == 0;

        if (xZero && yZero) return 0f;

        if (xZero) return y < 0 ? 0f : 0.5f;

        if (yZero) return x < 0 ? 0.75f : 0.25f;

        float angle = 0f;
        VectorF v;

        int q = (x < 0 ? 0b00 : 0b10) | (y < 0 ? 0b00 : 0b01);
        switch (q)
        {
            case 0b10:
                v = new(x, -y);
                break;
            case 0b11:
                angle = 0.25f;
                v = new(y, x);
                break;
            case 0b01:
                angle = 0.5f;
                v = new(-x, y);
                break;
            case 0b00:
                angle = 0.75f;
                v = new(-y, -x);
                break;
            default:
                throw new Exception($"Angle error: Quadrant={q}");
        }

        if (v.x == v.y) return angle + 0.125f;

        return (v.x > v.y ?
            0.25f - Math.ArcSine(v.y / DistanceFromZero()) :
            Math.ArcSine(v.x / DistanceFromZero()))
            + angle;
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