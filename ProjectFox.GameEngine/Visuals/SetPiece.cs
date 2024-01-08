using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public class SetPiece : RasterObject
{
    public SetPiece(NameID name) : base(name) { }

    public Texture texture = null;

    public IPalette palette = null;

    public Vector offset = new(0, 0);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void GetDrawInfo(out Texture texture, out IPalette palette, out Vector offset)
    {
        texture = this.texture;
        palette = this.palette;
        offset = this.offset;
    }
}