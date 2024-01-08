using System;
using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Sequential)]
public partial struct Triangle : IShape2D<Triangle, Vector, int, TriangleF>, IPolytope<Vector, Triangle>
{
#if DEBUG
    /// <summary>  </summary>
    public enum TriangleLayout
    {
        /// <summary> a triangle with 3 equal length sides </summary>
        Equilateral,
        /// <summary> a triangle with 2 equal length sides </summary>
        Isoceles,
        /// <summary> a triangle with no equal length sides </summary>
        Scalene
    }
#endif

    #region Constructors
    ///
    public Triangle(Vector a, Vector b, Vector c)
    {
        this.a = a;
        this.b = b;
        this.c = c;
    }
    ///
    public Triangle(int aX, int aY, Vector b, Vector c)
    {
        a = new(aX, aY);
        this.b = b;
        this.c = c;
    }
    ///
    public Triangle(Vector a, int bX, int bY, Vector c)
    {
        this.a = a;
        b = new(bX, bY);
        this.c = c;
    }
    ///
    public Triangle(Vector a, Vector b, int cX, int cY)
    {
        this.a = a;
        this.b = b;
        c = new(cX, cY);
    }
    ///
    public Triangle(int aX, int aY, int bX, int bY, Vector c)
    {
        a = new(aX, aY);
        b = new(bX, bY);
        this.c = c;
    }
    ///
    public Triangle(Vector a, int bX, int bY, int cX, int cY)
    {
        this.a = a;
        b = new(bX, bY);
        c = new(cX, cY);
    }
    ///
    public Triangle(int aX, int aY, Vector b, int cX, int cY)
    {
        a = new(aX, aY);
        this.b = b;
        c = new(cX, cY);
    }
    ///
    public Triangle(int aX, int aY, int bX, int bY, int cX, int cY)
    {
        a = new(aX, aY);
        b = new(bX, bY);
        c = new(cX, cY);
    }
    #endregion

    ///
    public Vector a, b, c;
    
    public Vector CenterPoint
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => throw new NotImplementedException();
    }

    public Vector[] Points
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new Vector[3] { a, b, c };
    }

    /// <returns> (a XOR b XOR c) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => a.GetHashCode() ^ b.GetHashCode() ^ c.GetHashCode();

#if DEBUG
    public TriangleLayout GetLayout()
    {
        float A = a.Distance(b), B = b.Distance(c), C = c.Distance(a);

        int i = A == B ? 0b01 : 0b00;
        i |= B == C ? 0b10 : 0b00;

        return i switch
        {
            0b00 => A == C ? TriangleLayout.Isoceles : TriangleLayout.Scalene,
            0b01 => TriangleLayout.Isoceles,
            0b10 => TriangleLayout.Isoceles,
            0b11 => TriangleLayout.Equilateral,
            _ => throw new Exception($"Layout error {i}"),
        };
    }
#endif

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle[] GetPrimitives() => new Triangle[1] { this };

    /// <returns> $"(A = {a}, B = {b}, C = {c})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(A = {a}, B = {b}, C = {c})";

    /// <returns> $"(A = {(0x)a}, B = {(0x)b}, C = {(0x)c})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(A = {a.ToHexString(littleEndian, leadingText)}, B = {b.ToHexString(littleEndian, leadingText)}, C = {c.ToHexString(littleEndian, leadingText)})";

    /// <returns> $"(A = {(0b)a}, B = {(0b)b}, C = {(0b)c})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, char byteSeparator = '|', char nibbleSeparator = '_', bool leadingText = false) =>
        $"(A = {a.ToBinString(littleEndian, byteSeparator, nibbleSeparator, leadingText)}, B = {b.ToBinString(littleEndian, byteSeparator, nibbleSeparator, leadingText)}, C = {c.ToBinString(littleEndian, byteSeparator, nibbleSeparator, leadingText)})";

    /*public bool IsRight()
    {
        //topleft
        //topright
        //bottomright
        //bottomleft

        /*
        IsRight
            a == (bx, cy) || (cx, by)
            b == (ax, cy) || (cx, ay)
            c == (ax, by) || (bx, ay)
        *
    }*/

    //axisaligned?
    /*
Public bool axisaligned(out bool x, out bool y)
  X = ax == bx || bx == cx || cx == ax
  Y = same as x
  Return x || y
    */

    //test diagonal adjustment

    /*
Triangle
  coordinates = (
    A / AB >= CA ? AB : CA,
    B / AB >= BC ? AB : BC,
    C / BC >= CA ? BC : CA)
    */

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator TriangleF(Triangle triangle) =>
        new(triangle.a.x, triangle.a.y, triangle.b.x, triangle.b.y, triangle.c.x, triangle.c.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Triangle triangle1, Triangle triangle2) => triangle1.Equals(triangle2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Triangle triangle1, Triangle triangle2) => !triangle1.Equals(triangle2);
}
