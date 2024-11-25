using System.Runtime.InteropServices;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

[StructLayout(LayoutKind.Sequential)]
public partial struct RectangleF : IShape<RectangleF, RectangleF, VectorF, Vector, TriangleF, Triangle, RectangleF, RectangleF, float>, IPolytope<VectorF, TriangleF>, IDirection<VectorF>
{
    #region Constructors
    public RectangleF(VectorF position, VectorF size)
    {
        this.position = position;
        this.size = size;
    }
    public RectangleF(VectorF position, float width, float height)
    {
        this.position = position;
        size = new(width, height);
    }
    public RectangleF(float x, float y, VectorF size)
    {
        position = new(x, y);
        this.size = size;
    }
    public RectangleF(float x, float y, float width, float height)
    {
        position = new(x, y);
        size = new(width, height);
    }
    #endregion

    ///
    public VectorF position, size;
    
    /// <summary> position + size </summary>
    public VectorF EndPoint
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(position.x + size.x, position.y + size.y);
    }

    public VectorF CenterPoint
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => new(position.x + (size.x / 2), position.y + (size.y / 2));
    }
    
    /// <summary> four corners of the rect in order: top-left, top-right, bottom-right, bottom-left </summary>
    public VectorF[] Points
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get
        {
            VectorF endPoint = EndPoint;
            return new VectorF[4] { position, new(endPoint.x, position.y), endPoint, new(position.x, endPoint.y) };
        }
    }

    public RectangleF Bounds
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => this;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override int GetHashCode() => position.GetHashCode() ^ size.GetHashCode();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public TriangleF[] GetPrimitives()
    {
        VectorF[] points = Points;
        return new TriangleF[2]
        {
            new(points[0], points[1], points[2]),
            new(points[2], points[3], points[0])
        };
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public override string ToString() => $"(Pos = {position}, Size = {size})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToHexString(bool littleEndian = false, bool leadingText = false) =>
        $"(Pos = {position.ToHexString(littleEndian, leadingText)}, Size = {size.ToHexString(littleEndian, leadingText)})";

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public string ToBinString(bool littleEndian = false, bool leadingText = false, char byteSeparator = '|', char nibbleSeparator = '_') =>
        $"(Pos = {position.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)}, Size = {size.ToBinString(littleEndian, leadingText, byteSeparator, nibbleSeparator)})";
}