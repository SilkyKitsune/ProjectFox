using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Visuals;

public class Sprite : RasterObject
{
    public enum PalettePriority
    {
        Frame,
        Animation,
        Sprite
    }
    
    public Sprite(NameID name) : base(name) { }

    public Texture texture = null;

    public TextureAnimation animation = null;

    public Vector drawOffset = new(0, 0);

    public bool verticalFlipTexture = false, horizontalFlipTexture = false, verticalFlipOffset = false, horizontalFlipOffset = false, flipOffsetOnPixel = false, preferTexture = false;

    public IPalette palette = null;

    public int paletteOffset = 0;

    public PalettePriority palettePriority = PalettePriority.Frame;

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

        if ((preferTexture && texture != null) || animation == null) return;

        Array<TextureAnimation.TextureFrame> frames = (Array<TextureAnimation.TextureFrame>)animation.frames;

        if (frames.length == 0)
        {
            Engine.SendError(ErrorCodes.EmptyAnimation, name);
            return;
        }
        
        TextureAnimation.TextureFrame frame = frames.elements[animation.frameIndex >= frames.length || animation.frameIndex < 0 ? 0 : animation.frameIndex];
        
        texture = frame.texture;

        if (animation.verticalFlipTexture) verticalFlipTexture = !verticalFlipTexture;
        if (frame.verticalFlipTexture) verticalFlipTexture = !verticalFlipTexture;
        
        if (animation.horizontalFlipTexture) horizontalFlipTexture = !horizontalFlipTexture;
        if (frame.horizontalFlipTexture) horizontalFlipTexture = !horizontalFlipTexture;

        drawOffset = new(
            this.drawOffset.x + animation.drawOffset.x + frame.drawOffset.x,
            this.drawOffset.y + animation.drawOffset.y + frame.drawOffset.y);

        if (animation.verticalFlipOffset) verticalFlipOffset = !verticalFlipOffset;
        if (frame.verticalFlipOffset) verticalFlipOffset = !verticalFlipOffset;

        if (animation.horizontalFlipOffset) horizontalFlipOffset = !horizontalFlipOffset;
        if (frame.horizontalFlipOffset) horizontalFlipOffset = !horizontalFlipOffset;

        if (animation.flipOffsetOnPixel) flipOffsetOnPixel = !flipOffsetOnPixel;
        if (frame.flipOffsetOnPixel) flipOffsetOnPixel = !flipOffsetOnPixel;

        paletteOffset += animation.paletteOffset + frame.paletteOffset;

        switch (palettePriority)
        {
            case PalettePriority.Frame:
                palette = frame.palette != null ? frame.palette : (animation.palette != null ? animation.palette : this.palette);
                break;
            case PalettePriority.Animation:
                palette = animation.palette != null ? animation.palette : (frame.palette != null ? frame.palette : this.palette);
                break;
            case PalettePriority.Sprite:
                palette = this.palette != null ? this.palette : (frame.palette != null ? frame.palette : animation.palette);
                break;
            default:
                Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(palettePriority));
                palettePriority = PalettePriority.Frame;
                goto case PalettePriority.Frame;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal override void PostDraw() => animation?._animate();
}