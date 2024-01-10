using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Sequential)]
public partial struct TriangleF : IShape2D<TriangleF, VectorF, float, TriangleF>, IPolytope<VectorF, TriangleF>
{
    #region Constructors
    public TriangleF(VectorF a, VectorF b, VectorF c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
    public TriangleF(float aX, float aY, VectorF b, VectorF c)
    {
        a = new(aX, aY);
        this.b = b;
        this.c = c;
    }
    public TriangleF(VectorF a, float bX, float bY, VectorF c)
    {
        this.a = a;
        b = new(bX, bY);
        this.c = c;
    }
    public TriangleF(VectorF a, VectorF b, float cX, float cY)
    {
        this.a = a;
        this.b = b;
        c = new(cX, cY);
    }
    public TriangleF(float aX, float aY, float bX, float bY, VectorF c)
    {
        a = new(aX, aY);
        b = new(bX, bY);
        this.c = c;
    }
    public TriangleF(VectorF a, float bX, float bY, float cX, float cY)
    {
        this.a = a;
        b = new(bX, bY);
        c = new(cX, cY);
    }
    public TriangleF(float aX, float aY, VectorF b, float cX, float cY)
    {
        a = new(aX, aY);
        this.b = b;
        c = new(cX, cY);
    }
    public TriangleF(float aX, float aY, float bX, float bY, float cX, float cY)
    {
        a = new(aX, aY);
        b = new(bX, bY);
        c = new(cX, cY);
    }
    #endregion

    ///
    public VectorF a, b, c;

    public VectorF CenterPoint
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => throw new NotImplementedException();
    }

    public VectorF[] Points
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new VectorF[3] { a, b, c };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => a.GetHashCode() ^ b.GetHashCode() ^ c.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF[] GetPrimitives() => new TriangleF[1] { this };

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(A = {a}, B = {b}, C = {c})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(A = {a.ToHexString(littleEndian, leadingText)}, B = {b.ToHexString(littleEndian, leadingText)}, C = {c.ToHexString(littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(A = {a.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)}, B = {b.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)}, C = {c.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)})";

#if DEBUG
    public Triangle.TriangleLayout Getlayout()
    {
        float A = a.Distance(b), B = b.Distance(c), C = c.Distance(a);

        int i = A == B ? 0b01 : 0b00;
        i |= B == C ? 0b10 : 0b00;

        return i switch
        {
            0b00 => A == C ? Triangle.TriangleLayout.Isoceles : Triangle.TriangleLayout.Scalene,
            0b01 => Triangle.TriangleLayout.Isoceles,
            0b10 => Triangle.TriangleLayout.Isoceles,
            0b11 => Triangle.TriangleLayout.Equilateral,
            _ => throw new Exception($"Layout error {i}")
        };
    }
#endif

    //isright
    //axisaligned

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Triangle(TriangleF triangle) =>
        new((int)triangle.a.x, (int)triangle.a.y, (int)triangle.b.x, (int)triangle.b.y, (int)triangle.c.x, (int)triangle.c.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(TriangleF triangle1, TriangleF triangle2) => triangle1.Equals(triangle2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(TriangleF triangle1, TriangleF triangle2) => !triangle1.Equals(triangle2);
}
