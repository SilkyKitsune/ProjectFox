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

    public TextureAnimation animation = null;

    public IPalette palette = null;

    public Vector offset = new(0, 0);

    public PalettePriority palettePriority = PalettePriority.Frame;

    protected override void GetDrawInfo(out Texture texture, out IPalette palette, out Vector offset)
    {
        texture = null;
        palette = null;
        offset = new(0, 0);

        if (animation == null)
        {
            Engine.SendError(ErrorCodes.NullAnimation, name);
            return;
        }

        Array<TextureAnimation.TextureFrame> frames = (Array<TextureAnimation.TextureFrame>)animation.frames;

        if (frames.length == 0)
        {
            Engine.SendError(ErrorCodes.EmptyAnimation, name);
            return;
        }
        
        TextureAnimation.TextureFrame frame = frames.elements[animation.frameIndex];
        
        texture = frame.texture;
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
        offset = new(
            this.offset.x + animation.offset.x + frame.offset.x,
            this.offset.y + animation.offset.y + frame.offset.y);
        //textureframe/anim flip should invert rasterobject flip
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override void _frame()
    {
        if (!paused || pauseWalks)
        {
            PreFrame();
            animation?._animate();
            PostFrame();
        }
    }
}