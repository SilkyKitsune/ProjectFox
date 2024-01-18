using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Color
{
    public static Color FromHSV(float hue, float saturation, float veloctiy, float alpha = 1f)
    {
        byte a = (byte)(Math.Clamp(alpha, 0f, 1f) * MaxByteF);
        if (veloctiy == 0f) return new(0, 0, 0, a);

        byte high = (byte)(Math.Clamp(veloctiy, 0f, 1f) * MaxByteF);
        if (saturation == 0f) return new(high, high, high, a);

        float delta = Math.Clamp(saturation, 0f, 1f) * high;
        byte low = (byte)(high - delta);

        hue = Math.Clamp((float)Math.Wrap((decimal)hue, 0m, 359m), 0f, 359f) / 60f;
        int order = (int)hue % 6;
        hue -= order;

        return order switch
        {
            0 => new(high, (byte)(hue * delta + low), low, a), //rgb
            1 => new((byte)(delta - (hue * delta) + low), high, low, a), //grb
            2 => new(low, high, (byte)(hue * delta + low), a), //gbr
            3 => new(low, (byte)(delta - (hue * delta) + low), high, a), //bgr
            4 => new((byte)(hue * delta + low), low, high, a), //brg
            5 => new(high, low, (byte)(delta - (hue * delta) + low), a), //rbg
            _ => throw new Exception($"Hue error: {nameof(hue)}={hue} {nameof(order)}={order}")
        };
    }

    public byte Highest
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Math.Max(r, g > b ? g : b);
        set
        {
            byte highest = Math.Max(r, g > b ? g : b);
            if (value == 0 || highest == 0)
            {
                r = value;
                g = value;
                b = value;
                return;
            }
            r = (byte)(r / highest * value);
            g = (byte)(g / highest * value);
            b = (byte)(b / highest * value);
        }
    }

    public byte Middle
    {
        get
        {
            int i = r >= g ? 0b01 : 0b00;
            i |= g >= b ? 0b10 : 0b00;

            return i switch
            {
                0b00 => g, //bgr
                0b01 => r > b ? b : r, //rbg : brg
                0b10 => r > b ? r : b, //grb : gbr
                0b11 => IsGrey() ? r : g, //rgb
                _ => throw new Exception($"Hue error: Order={i}"),
            };
        }
    }

    public byte Lowest
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Math.Min(r, g < b ? g : b);
    }

    public byte Delta
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => (byte)(Highest - Lowest);
    }

    public int HueOrder
    {
        get
        {
            int i = r >= g ? 0b01 : 0b00;
            i |= g >= b ? 0b10 : 0b00;

            return i switch
            {
                0b00 => 4, //bgr
                0b01 => r > b ? 6 : 5, //rbg : brg
                0b10 => r > b ? 2 : 3, //grb : gbr
                0b11 => IsGrey() ? 0 : 1, //rgb
                _ => throw new Exception($"Hue error: Order={i}"),
            };
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string MathString() => $"Order: {HueOrder}, Highest: {Highest}, Lowest: {Lowest}, Delta: {Delta}";

    public float Velocity
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => Math.Max(r, g > b ? g : b) / MaxByteF;
        set
        {
            float newV = Math.Clamp(value, 0f, 1f);
            if (newV == 0f)
            {
                r = 0;
                g = 0;
                b = 0;
                return;
            }
            float oldV = Math.Max(r, g > b ? g : b) / MaxByteF;
            if (oldV == 0f)
            {
                r = (byte)(newV * MaxByteF);
                g = (byte)(newV * MaxByteF);
                b = (byte)(newV * MaxByteF);
                return;
            }
            r = (byte)(r / oldV * newV);
            g = (byte)(g / oldV * newV);
            b = (byte)(b / oldV * newV);
        }
    }

    public float Saturation
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            float highest = Math.Max(r, g > b ? g : b);
            return highest - Math.Min(r, g < b ? g : b) / highest;
        }
        set
        {
            float sat = Math.Clamp(value, 0f, 1f);
            if (sat == 0f)
            {
                byte highest = Math.Max(r, g > b ? g : b);
                r = highest;
                g = highest;
                b = highest;
                return;
            }

            int i = r >= g ? 0b01 : 0b00;
            i |= g >= b ? 0b10 : 0b00;

            switch (i)
            {
                case 0b00: //bgr
                    {
                        float m = g - r / (float)(b - r);
                        r = (byte)(b - (int)(sat * b));
                        g = (byte)((b - r) * m + r);
                        break;
                    }
                case 0b01: //rbg : brg
                    {
                        if (r > b) //rbg
                        {
                            float m = b - g / (float)(r - g);
                            g = (byte)(r - (int)(sat * r));
                            b = (byte)((r - g) * m + g);
                        }
                        else //brg
                        {
                            float m = r - g / (float)(b - g);
                            g = (byte)(b - (int)(sat * b));
                            r = (byte)((b - g) * m + g);
                        }
                        break;
                    }
                case 0b10: //grb : gbr
                    {
                        if (r > b) //grb
                        {
                            float m = r - b / (float)(g - b);
                            b = (byte)(g - (int)(sat * g));
                            r = (byte)((g - b) * m + b);
                        }
                        else //gbr
                        {
                            float m = b - r / (float)(g - r);
                            r = (byte)(g - (int)(sat * g));
                            b = (byte)((g - r) * m + r);
                        }
                        break;
                    }
                case 0b11: //rgb
                    {
                        float m = g - b / (float)(r - b);
                        b = (byte)(r - (int)(sat * r));
                        g = (byte)((r - b) * m + b);
                        break;
                    }
                default:
                    throw new Exception($"Hue error: Order={i}");
            };
        }
    }

    public float Hue
    {
        get
        {
            int i = r >= g ? 0b01 : 0b00;
            i |= g >= b ? 0b10 : 0b00;

            return i switch
            {
                0b00 => g - r / (float)Delta + 3f * 60f, //bgr
                0b01 => r > b ?
                    b - g / (float)Delta + 5f * 60f ://rbg
                    r - g / (float)Delta + 4f * 60f, //brg
                0b10 => r > b ?
                    r - b / (float)Delta + 1f * 60f ://grb
                    b - r / (float)Delta + 2f * 60f, //gbr
                0b11 => IsGrey() ? 0f :
                    g - b / (float)Delta * 60f, //rgb
                _ => throw new Exception($"Hue error: Order={i}"),
            };
            /**return HueOrder switch
            {
                0 => 0f,
                1 => g - b / (float)Delta * 60f,
                2 => r - b / (float)Delta + 1f * 60f,
                3 => b - r / (float)Delta + 2f * 60f,
                4 => g - r / (float)Delta + 3f * 60f,
                5 => r - g / (float)Delta + 4f * 60f,
                6 => b - g / (float)Delta + 5f * 60f
            };*/
        }
        set
        {
            if (IsGrey()) return;

            float hue = Math.Clamp((float)Math.Wrap((decimal)value, 0m, 359m), 0f, 359f) / 60f;
            int order = (int)hue % 6;
            hue -= order;

            byte high = Math.Max(r, g > b ? g : b);
            byte low = Math.Min(r, g < b ? g : b);
            byte mid = (byte)((high - low) * hue + low);

            switch (order)
            {
                case 0: //rgb
                    r = high;
                    g = mid;
                    b = low;
                    break;
                case 1: //grb
                    g = high;
                    r = mid;
                    b = low;
                    break;
                case 2: //gbr
                    g = high;
                    b = mid;
                    r = low;
                    break;
                case 3: //bgr
                    b = high;
                    g = mid;
                    r = low;
                    break;
                case 4: //brg
                    b = high;
                    r = mid;
                    g = low;
                    break;
                case 5: //rbg
                    r = high;
                    b = mid;
                    g = low;
                    break;
                default:
                    throw new Exception($"Hue error: {nameof(hue)}={hue} {nameof(order)}={order}");
            }
        }
    }

    public void GetHSV(out float hue, out float saturation, out float velocity, out float alpha)
    {
        alpha = a / MaxByteF;

        if (IsGrey())
        {
            velocity = r / MaxByteF;
            saturation = 0f;
            hue = 0f;
            return;
        }

        byte highest = Math.Max(r, g > b ? g : b);
        byte lowest = Math.Min(r, g < b ? g : b);
        byte delta = (byte)(highest - lowest);

        velocity = highest / MaxByteF;
        saturation = delta / (float)highest;

        if (highest == r)
        {
            int h = g - b;
            hue = (h < 0 ? h + delta : h) / (float)delta + (g == lowest ? 5f : 0f) * 60f;
        }
        else if (highest == g)
        {
            int h = b - r;
            hue = (h < 0 ? h + delta : h) / (float)delta + (r == lowest ? 2f : 1f) * 60f;
        }
        else
        {
            int h = r - g;
            hue = (h < 0 ? h + delta : h) / (float)delta + (g == lowest ? 4f : 3f) * 60f;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string HSVString()
    {
        GetHSV(out float hue, out float saturation, out float velocity, out float alpha);
        return $"(Hue: {hue}, Saturation: {saturation}, Velocity: {velocity}, A: {alpha})";
    }
}