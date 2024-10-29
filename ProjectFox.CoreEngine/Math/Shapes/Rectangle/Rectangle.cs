using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

/// <summary> an axis aligned 2D shape comprised of a position and size </summary>
[StructLayout(LayoutKind.Sequential)]
public partial struct Rectangle : IShape2D<Rectangle, Vector, int, RectangleF, Rectangle>, IPolytope<Vector, Triangle>
{
    #region Constructors
    ///
    public Rectangle(Vector position, Vector size)
    {
        this.position = position;
        this.size = size;
    }
    ///
    public Rectangle(int x, int y, Vector size)
    {
        position = new Vector(x, y);
        this.size = size;
    }
    ///
    public Rectangle(Vector position, int width, int height)
    {
        this.position = position;
        size = new Vector(width, height);
    }
    ///
    public Rectangle(int x, int y, int width, int height)
    {
        position = new Vector(x, y);
        size = new Vector(width, height);
    }
    #endregion

    ///
    public Vector position, size;

    /// <summary> position + size </summary>
    public Vector EndPoint
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(position.x + size.x, position.y + size.y);
    }

    public Vector CenterPoint
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(position.x + (size.x / 2), position.y + (size.y / 2));
    }

    /// <summary> four corners of the rect in order: top-left, top-right, bottom-right, bottom-left </summary>
    public Vector[] Points
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            Vector endPoint = EndPoint;
            return new Vector[4] { position, new(endPoint.x, position.y), endPoint, new(position.x, endPoint.y) };
        }
    }

    public Rectangle Bounds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this;
    }

    /// <returns> (position XOR size) </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => position.GetHashCode() ^ size.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Triangle[] GetPrimitives()
    {
        Vector[] points = Points;
        return new Triangle[2]
        {
            new(points[0], points[1], points[2]),
            new(points[2], points[3], points[0])
        };
    }

    /// <returns> $"(Pos = {position}, Size = {size})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(Pos = {position}, Size = {size})";

    /// <returns> $"(Pos = {(0x)position}, Size = {(0x)size})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(Pos = {position.ToHexString(littleEndian, leadingText)}, Size = {size.ToHexString(littleEndian, leadingText)})";

    /// <returns> $"(Pos = {(0b)position}, Size = {(0b)size})" </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(Pos = {position.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Size = {size.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)})";
}