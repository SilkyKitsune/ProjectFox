using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;
using static ProjectFox.GameEngine.Visuals.PaletteAnimation;

namespace ProjectFox.GameEngine.Visuals;

public abstract class Texture : ICopy<Texture>
{
    internal Texture(Vector dimensions, bool indexed)
    {
        this.dimensions = dimensions;
        this.indexed = indexed;
    }

    internal readonly bool indexed;
    internal readonly Vector dimensions;//size?

    public Vector Dimensions => dimensions;
    
    public abstract Texture Copy();

    //rotated copy? 1 clockwise right angle
}

public sealed class ColorTexture : Texture, IColorGroup
{
    public ColorTexture(Vector dimensions, Color[] pixels = null) : base(dimensions, false)
    {
        int length = dimensions.x * dimensions.y;
        if (pixels == null || pixels.Length != length)
            pixels = new Color[length];
        this.pixels = pixels;
    }

    public ColorTexture(int width, int height, Color[] pixels = null) : this(new(width, height), pixels) { }

    internal readonly Color[] pixels;

    public override Texture Copy()
    {
        Color[] newPixels = new Color[pixels.Length];
        pixels.CopyTo(newPixels, 0);
        return new ColorTexture(dimensions, newPixels);
    }

    //palettized copy?

    public void ModifyHSV(float hueModifier, float saturationModifier, float velocityModifier)
    {
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].GetHSV(out float hue, out float sat, out float vel, out float a);
            pixels[i] = Color.FromHSV(
                hue + hueModifier, sat * saturationModifier, vel * velocityModifier, a);
        }
    }

    public void HueShift(float modifier)
    {
        for (int i = 0; i < pixels.Length; i++)
            pixels[i].Hue += modifier;
    }

    public void SaturationMultiply(float modifier)
    {
        for (int i = 0; i < pixels.Length; i++)
            pixels[i].Saturation *= modifier;
    }

    public void VelocityMultiply(float modifier)
    {
        for (int i = 0; i < pixels.Length; i++)
            pixels[i].Highest = (byte)(pixels[i].Highest * modifier);
    }
}

public sealed class PalettizedTexture : Texture
{
    public PalettizedTexture(Vector dimensions, byte[] pixels = null) : base(dimensions, true)
    {
        int length = dimensions.x * dimensions.y;
        if (pixels == null || pixels.Length != length)
            pixels = new byte[length];
        this.pixels = pixels;
    }

    public PalettizedTexture(int width, int height, byte[] pixels = null) : this(new(width, height), pixels) { }

#if DEBUG
    public PalettizedTexture(Vector dimensions, Color[] pixels, /*ref?*/ ColorPalette palette) : base(dimensions, true)
    {
        int length = dimensions.x * dimensions.y;
        if (pixels == null || pixels.Length != length)
        {
            Engine.SendError(ErrorCodes.BadArgument, new("PltTxtr", 0), nameof(pixels));
            this.pixels = new byte[length];
        }
        else
        {

        }
    }

    public PalettizedTexture(int width, int height, Color[] pixels, ColorPalette palette) : this(new(width, height), pixels, palette) { }
#endif

    internal readonly byte[] pixels;

    public override Texture Copy()
    {
        byte[] newPixels = new byte[pixels.Length];
        pixels.CopyTo(newPixels, 0);
        return new PalettizedTexture(dimensions, newPixels);
    }

    public ColorTexture ColorCopy(IPalette palette)
    {
        if (palette == null)
            return Engine.SendError<ColorTexture>(
                ErrorCodes.NullArgument, new("PltTxtr", 0), nameof(palette));

        Color[] colors = palette.GetColors(),
            colorPixels = new Color[pixels.Length];

        for (int i = 0; i < pixels.Length; i++)
            colorPixels[i] = colors[Math.Clamp(pixels[i], 0, colors.Length)];

        return new ColorTexture(dimensions, colorPixels);
    }
}

public sealed class TextureAnimation : Animation//named type?
{
    public sealed class TextureFrame : Frame
    {
        public Texture texture = null;
        public IPalette palette = null;//does _draw animate this?
        public Vector offset = new(0, 0);
        //FlipTexture?
        //flip offset?
    }

    public TextureAnimation(params TextureFrame[] frames) => this.frames.Add(frames);

    public readonly ICollection<TextureFrame> frames = new Array<TextureFrame>(0x20);

    public IPalette palette = null;

    public Vector offset = new(0, 0);

    public override int FrameCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ((Array<TextureFrame>)frames).length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private protected override void GetFrame(out Frame frame, out int frameCount)
    {
        Array<TextureFrame> frames = (Array<TextureFrame>)this.frames;
        frame = frames.elements[frameIndex];
        frameCount = frames.length;
    }
}