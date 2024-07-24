using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public class SetPiece : RasterObject
{
    public SetPiece(NameID name) : base(name) { }

    public Texture texture = null;

    public Vector drawOffset = new(0, 0);

    public bool verticalFlipTexture = false, horizontalFlipTexture = false, verticalFlipOffset = false, horizontalFlipOffset = false, flipOffsetOnPixel = false;

    public IPalette palette = null;

    public int paletteOffset = 0;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected override void GetDrawInfo(
        out Texture texture, out bool verticalFlipTexture, out bool horizontalFlipTexture,
        out Vector drawOffset, out bool verticalFlipOffset, out bool horizontalFlipOffset, out bool flipOffsetOnPixel,
        out IPalette palette, out int paletteOffset)
    {
        texture = this.texture;
        verticalFlipTexture = this.verticalFlipTexture;
        horizontalFlipTexture = this.horizontalFlipTexture;

        drawOffset = this.drawOffset;
        verticalFlipOffset = this.verticalFlipOffset;
        horizontalFlipOffset = this.horizontalFlipOffset;
        flipOffsetOnPixel = this.flipOffsetOnPixel;

        palette = this.palette;
        paletteOffset = this.paletteOffset;
    }
}