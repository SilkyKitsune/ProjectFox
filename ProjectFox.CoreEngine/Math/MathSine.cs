using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public static partial class Math
{
    private const float
        Sine_0_03 =   0.187381314586f,//might veryify the precision of these consts later
        Sine_0_0575 = 0.353474843779f,
        Sine_0_08 =   0.481753674102f,
        Sine_0_1 =    0.587785252292f,
        Sine_0_115 =  0.661311865324f,
        Sine_0_155 =  0.827080574275f,
        Sine_0_19 =   0.929776485888f;

    /// <summary> the closest approximation to Sine(0.125) = 0.707106781187... </summary>
    public const float HalfSine = 0.707106769084930419921875f;//rename

    /// <summary> the closest approximation to π (3.14159265359...) </summary>
    public const float Pi = 3.1415927410125732421875f;

    /// <summary> the closest approximation to τ (6.28318530718...) </summary>
    public const float Tau = 6.283185482025146484375f;

    /// <summary> the closest approximation to 2τ (12.5663706144...) </summary>
    public const float DoubleTau = 12.56637096405029296875f;//rename?

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public static float ArcCosine(float cosine) => default;

    public static float ArcSine(float sine)
    {
        switch (sine)
        {
            case 0f:
                return 0f;
            case HalfSine:
                return 0.125f;
            case 1f:
                return 0.25f;
            case -HalfSine:
                return 0.625f;
            case -1f:
                return 0.75f;
        }

        if (sine > 1f || sine < -1f || float.IsNaN(sine)) throw new ArgumentException($"Invalid Sine! '{sine}'");

        float f = 0f;
        
        if (sine < 0f)
        {
            f = 0.5f;
            sine = -sine;
        }
        //add these consts to switch? if so, then quarter sine should probably have a switch for the opposite
        if (sine < Sine_0_03)   return sine / 6.24f + f;
        
        if (sine < Sine_0_0575) return (sine - Sine_0_03) / 6.04f + 0.03f + f;

        if (sine < Sine_0_08)   return (sine - Sine_0_0575) / 5.7f +  0.0575f + f;

        if (sine < Sine_0_1)    return (sine - Sine_0_08) / 5.3f +  0.08f + f;

        if (sine < Sine_0_115)  return (sine - Sine_0_1) / 4.9f +  0.1f + f;

        if (sine < HalfSine)    return (sine - Sine_0_115) / 4.57f + 0.115f + f;
        
        if (sine < Sine_0_155)  return 0.25f - SqrRoot((0.992f - sine) / 18.25f) + f;

        if (sine < Sine_0_19)   return 0.25f - SqrRoot((0.9975f - sine) / 18.85f) + f;

        return 0.25f - SqrRoot((1f - sine) / 19.5f) + f;
    }

    //ArcSineCosine?

    public static float QuarterSine(float amount)//temp public //anyway to simplify these equations?
    {
        if (amount < 0f || amount > 0.25f) throw new ArgumentException($"Unexpected quarter sine! '{amount}'");
        
        if (amount < 0.03f)   return amount * 6.24f;

        if (amount < 0.0575f) return (amount - 0.03f) * 6.04f + Sine_0_03;

        if (amount < 0.08f)   return (amount - 0.0575f) * 5.7f + Sine_0_0575;

        if (amount < 0.1f)    return (amount - 0.08f) * 5.3f + Sine_0_08;

        if (amount < 0.115f)  return (amount - 0.1f) * 4.9f + Sine_0_1;

        if (amount < 0.125f)  return (amount - 0.115f) * 4.57f + Sine_0_115;

        float f = amount - 0.25f;

        if (amount < 0.155f) return 0.992f - ((f * f) * 18.25f);

        if (amount < 0.19f)  return 0.9975f - ((f * f) * 18.85f);

        return 1f - ((f * f) * 19.5f);
    }

    public static void SineCosine(float amount, out float sine, out float cosine)
    {
        bool neg = amount < 0f;
        if (neg) amount = -amount;

        amount -= (int)amount;

        switch (amount)
        {
            case 0f:
                sine = 0f;
                cosine = 1f;
                return;
            case 0.25f:
                sine = 1f;
                cosine = 0f;
                return;
            case 0.5f:
                sine = 0f;
                cosine = -1f;
                return;
            case 0.75f:
                sine = -1f;
                cosine = 0f;
                return;

            case 0.125f:
                sine = cosine = HalfSine;
                return;
            case 0.375f:
                sine = HalfSine;
                cosine = -HalfSine;
                return;
            case 0.625f:
                sine = cosine = -HalfSine;
                return;
            case 0.875f:
                sine = -HalfSine;
                cosine = HalfSine;
                return;
        }

        int quarter = (int)(amount * 4f);
        amount -= quarter * 0.25f;
        float normal = QuarterSine(amount), reverse = QuarterSine(0.25f - amount);

        switch (quarter)
        {
            default:
                sine = normal;
                cosine = reverse;
                break;
            case 1:
                sine = reverse;
                cosine = -normal;
                break;
            case 2:
                sine = -normal;
                cosine = -reverse;
                break;
            case 3:
                sine = -reverse;
                cosine = normal;
                break;
        }

        if (neg) sine = -sine;
    }

    //private static float[] SingleCycleSine(/*int length?*/ /*float phase?*/)

    /*private static float[] Interpolate(float[] array, int length)//move to another file
    {
        int extra = length / array.Length;//this isn't completely right
        //number of additional elements per interval
    }*/
}