//using D = System.Diagnostics.Debug;

namespace ProjectFox.CoreEngine.Math;

public partial struct Vector
{
    [System.Obsolete] public static Vector[] StepInterpolateOld(Vector endPoint, Vector startPoint = default)
    {
        Vector distance = startPoint.IsZero() ? endPoint : new(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
        
        Vector sign = new(distance.x < 0 ? -1 : 1, distance.y < 0 ? -1 : 1), v = distance.Abs();
        
        if (v.x == v.y) return v.x == 0 ? null : new Vector[1] { sign };

        //if (v.x == 0 || v.y == 0) return new Vector[1] { distance };//does this make sense?
        if (v.x == 0) return new Vector[1] { new(0, sign.y) };
        if (v.y == 0) return new Vector[1] { new(sign.x, 0) };

        while (!Math.IsOdd(v.x) && !Math.IsOdd(v.y)) v = new(v.x / 2, v.y / 2);
        
        bool yGreater = v.y > v.x;
        int low = yGreater ? v.x / v.y : v.y / v.x, high = low + 1;//this always results in 0 & 1 I think?
        int smallDoubled = 2 * (yGreater ? v.x : v.y), big = yGreater ? v.y : v.x;
        
        if (smallDoubled == big)
            return yGreater ?
                new Vector[2] { new(sign.x * high, sign.y * 1), new(sign.x * low, sign.y * 1) } :
                new Vector[2] { new(sign.x * 1, sign.y * high), new(sign.x * 1, sign.y * low) };//why do these multiply by one?

        bool upper = smallDoubled > big;//upper fraction may be unhelpful here
        
        Vector[] array = new Vector[yGreater ? v.y : v.x];
        for (int i = 0, count = 0, nextIndex = 0, secondarySteps = yGreater ? v.x : v.y,
            primary = upper ? high : low, secondary = upper ? low : high,
            interval = Math.Round(array.Length / (float)secondarySteps);
            i < array.Length; i++)
        {
            if (count < secondarySteps && i == nextIndex)
            {
                array[i] = yGreater ? new(sign.x * secondary, sign.y * 1) : new(sign.x * 1, sign.y * secondary);//why do these multiply by one?
                nextIndex += interval;
                count++;
            }
            else array[i] = yGreater ? new(sign.x * primary, sign.y * 1) : new(sign.x * 1, sign.y * primary);//why do these multiply by one?
        }
        return array;
    }

    public static Vector[] StepInterpolate(Vector endPoint, Vector startPoint = default)//there are some values that give bad steps
    {
        Vector distance = startPoint.IsZero() ? endPoint : new(endPoint.x - startPoint.x, endPoint.y - startPoint.y);
        
        Vector sign = new(distance.x < 0 ? -1 : 1, distance.y < 0 ? -1 : 1), signX = new(sign.x, 0), signY = new(0, sign.y), v = distance.Abs();
        
        if (v.x == v.y) return v.x == 0 ? null : new Vector[1] { sign };

        if (v.x == 0) return new Vector[1] { signY };
        if (v.y == 0) return new Vector[1] { signX };

        while (v.x % 10 == 0 && v.y % 10 == 0) v = new(v.x / 10, v.y / 10);

        while (!Math.IsOdd(v.x) && !Math.IsOdd(v.y)) v = new(v.x / 2, v.y / 2);//inline?
        
        int small, big;
        Vector secondary;
        if (v.y > v.x)
        {
            small = v.x;
            big = v.y;
            secondary = signY;
        }
        else
        {
            small = v.y;
            big = v.x;
            secondary = signX;
        }
        
        if (small * 2 == big) return new Vector[2] { sign, secondary };

        Vector[] array = new Vector[big];
        for (int i = 0, count = 0, nextIndex = 0, interval = Math.Round(array.Length / (float)small);
            i < array.Length; i++)
        {
            if (count < small && i == nextIndex)
            {
                array[i] = sign;
                nextIndex += interval;
                count++;
            }
            else array[i] = secondary;
        }
        return array;
    }
}