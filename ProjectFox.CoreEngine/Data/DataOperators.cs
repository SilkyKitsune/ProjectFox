using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Data;

public unsafe static partial class Data
{
    /// <returns> f1 AND f2 </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ANDFloat32(float f1, float f2)
    {
        int i1 = *(int*)&f1, i2 = *(int*)&f2, result = i1 & i2;
        return *(float*)&result;
    }

    /// <returns> d1 AND d2 </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ANDFloat64(double d1, double d2)
    {
        long i1 = *(long*)&d1, i2 = *(long*)&d2, result = i1 & i2;
        return *(double*)&result;
    }

    /// <returns> NOT(f) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NOTFloat32(float f)
    {
        int i = *(int*)&f, result = ~i;
        return *(float*)&result;
    }

    /// <returns> NOT(d) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NOTFloat64(double d)
    {
        long i = *(long*)&d, result = ~i;
        return *(double*)&result;
    }

    /// <returns> f1 OR f2 </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float ORFloat32(float f1, float f2)
    {
        int i1 = *(int*)&f1, i2 = *(int*)&f2, result = i1 | i2;
        return *(float*)&result;
    }

    /// <returns> d1 OR d2 </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double ORFloat64(double d1, double d2)
    {
        long i1 = *(long*)&d1, i2 = *(long*)&d2, result = i1 | i2;
        return *(double*)&result;
    }

    /// <returns> f1 XOR f2 </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float XORFloat32(float f1, float f2)
    {
        int i1 = *(int*)&f1, i2 = *(int*)&f2, result = i1 ^ i2;
        return *(float*)&result;
    }

    /// <returns> d1 XOR d2 </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double XORFloat64(double d1, double d2)
    {
        long i1 = *(long*)&d1, i2 = *(long*)&d2, result = i1 ^ i2;
        return *(double*)&result;
    }
}