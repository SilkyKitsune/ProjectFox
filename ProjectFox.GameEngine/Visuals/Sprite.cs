using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Visuals;

public class Sprite : RasterObject
{
    public Sprite(NameID name) : base(name) { }

    private int animIndex = 0;
    private NameID animation = new(0);

    public readonly IHashTable<NameID, TextureAnimation> animations = new HashArray<TextureAnimation>(0x20);

    public IPalette palette = null;

    public Vector offset = new(0, 0);

    public NameID Animation
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => animation;
        set
        {
            if (animation.Equals(value)) return;

            HashArray<TextureAnimation> anims = (HashArray<TextureAnimation>)animations;
            int index = anims.codes.IndexOf(value);

            if (index < 0)
            {
                Engine.SendError(ErrorCodes.BadArgument, name, nameof(value),
                    $"'{value}' could not be found in {nameof(animations)}");
                return;
            }
            animation = value;
            animIndex = index;
            anims.values.elements[animIndex].frameIndex = 0;
        }
    }

    protected override void GetDrawInfo(out Texture texture, out IPalette palette, out Vector offset)
    {
        texture = null;
        palette = null;
        offset = new(0, 0);

        HashArray<TextureAnimation> anims = (HashArray<TextureAnimation>)animations;
        NameID anim = anims.codes.elements[animIndex];

        if (animIndex >= anims.codes.length || !animation.Equals(anim))
        {
            Engine.SendError(ErrorCodes.MissingAnimation, name, animation.ToString());
            return;
        }

        TextureAnimation texAnim = anims.values.elements[animIndex];
        Array<TextureAnimation.TextureFrame> frames = (Array<TextureAnimation.TextureFrame>)texAnim.frames;

        if (frames.length == 0)
        {
            Engine.SendError(ErrorCodes.EmptyAnimation, name, anim.ToString());
            return;
        }

        TextureAnimation.TextureFrame frame = frames.elements[texAnim.frameIndex];
        
        texture = frame.texture;
        palette = frame.palette == null ? (texAnim.palette == null ? this.palette : texAnim.palette) : frame.palette;
        offset = new(
            this.offset.x + texAnim.offset.x + frame.offset.x,
            this.offset.y + texAnim.offset.y + frame.offset.y);
        //textureframe/anim flip should invert rasterobject flip
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override void _frame()
    {
        if (!paused || pauseWalks)
        {
            PreFrame();
            ((HashArray<TextureAnimation>)animations).values.elements[animIndex]?._animate();
            PostFrame();
        }
    }
}