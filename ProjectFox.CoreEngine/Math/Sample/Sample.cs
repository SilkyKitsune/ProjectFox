using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Data;
using D = ProjectFox.CoreEngine.Data.Data;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Explicit, Size = 4)]//change to sequential?
public partial struct Sample : IData<Sample>//other interfaces?
{
    public Sample(short sample)
    {
        left = sample;
        right = sample;
    }

    public Sample(short left, short right)
    {
        this.left = left;
        this.right = right;
    }

    [FieldOffset(2)] public short left;
    [FieldOffset(0)] public short right;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(L: {left}, R: {right})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(L: {D.ToHexString(left, littleEndian, leadingText)}, R: {D.ToHexString(right, littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(L: {D.ToBinString(left, littleEndian, leadingText, byteSeparator, nibbleSeparator)}, R: {D.ToBinString(right, littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

    #region Sample Methods
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public bool IsMono() => left == right;
    #endregion

    //operators?
}