using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

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
    
    public abstract void Copy(out Texture copy);

    //rotated copy? 1 clockwise right angle
}

public sealed class ColorTexture : Texture, IColorGroup
{
    public ColorTexture(Vector dimensions, Color[] pixels = null) : base(dimensions, false)
    {
        if (dimensions.x < 0 || dimensions.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, new("ClrTxtr", 0), nameof(dimensions), "Texture dimensions cannot be negative!");
            dimensions = default;
        }
        
        int length = dimensions.x * dimensions.y;
        if (pixels == null || pixels.Length != length)
            pixels = new Color[length];
        this.pixels = pixels;
    }

    public ColorTexture(int width, int height, Color[] pixels = null) : this(new(width, height), pixels) { }

    internal readonly Color[] pixels;

    public override void Copy(out Texture copy)
    {
        Color[] newPixels = new Color[pixels.Length];
        pixels.CopyTo(newPixels, 0);
        copy = new ColorTexture(dimensions, newPixels);
    }

    //palettized copy?

    public bool Grayscale()
    {
        if (pixels.Length == 0) return false;
        
        foreach (Color pixel in pixels) if (!pixel.IsGrey()) return false;
        return true;
    }

    public void ModifyHSV(float hueModifier, float saturationModifier, float velocityModifier)
    {
        for (int i = 0; i < pixels.Length; i++)
        {
            pixels[i].GetHSV(out float hue, out float sat, out float vel, out float a);
            pixels[i] = Color.FromHSV(hue + hueModifier, sat * saturationModifier, vel * velocityModifier, a);
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
            pixels[i].Velocity *= modifier;//pixels[i].Highest = (byte)(pixels[i].Highest * modifier);
    }

    public bool UniformAlpha()
    {
        if (pixels.Length == 0) return false;
        
        byte a = pixels[0].a;
        foreach (Color pixel in pixels) if (pixel.a != a) return false;
        return true;
    }
}

public sealed class PalettizedTexture : Texture
{
    public PalettizedTexture(Vector dimensions, byte[] pixels = null) : base(dimensions, true)
    {
        if (dimensions.x < 0 || dimensions.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, new("PltTxtr", 0), nameof(dimensions), "Texture dimensions cannot be negative!");
            dimensions = default;
        }

        int length = dimensions.x * dimensions.y;
        if (pixels == null || pixels.Length != length)
            pixels = new byte[length];
        this.pixels = pixels;
    }

    public PalettizedTexture(int width, int height, byte[] pixels = null) : this(new(width, height), pixels) { }

    public PalettizedTexture(Vector dimensions, Color[] pixels, ref ColorPalette palette, bool rampToPalette) : base(dimensions, true)
    {
        if (dimensions.x < 0 || dimensions.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, new("PltTxtr", 0), nameof(dimensions), "Texture dimensions cannot be negative!");
            dimensions = default;
        }

        palette ??= new();
        int length = dimensions.x * dimensions.y;
        this.pixels = new byte[length];
        if (pixels != null && pixels.Length == length) for (int i = 0; i < length; i++)
            {
                Color c = pixels[i];
                Color[] colors = palette.colors.ToArray();
                int index = palette.colors.IndexOf(c);

                if (index > -1) this.pixels[i] = (byte)index;
                else if (rampToPalette) this.pixels[i] = (byte)c.ClosestIndex(colors);
                else
                {
                    this.pixels[i] = (byte)palette.colors.Length;
                    palette.colors.Add(c);
                }
            }
    }

    public PalettizedTexture(int width, int height, Color[] pixels, ref ColorPalette palette, bool rampToPalette) : this(new(width, height), pixels, ref palette, rampToPalette) { }

    public PalettizedTexture(Vector dimensions, Color[] pixels, IndexPalette palette/*, rampToGlobal?*/) : base(dimensions, true)
    {
        if (dimensions.x < 0 || dimensions.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, new("PltTxtr", 0), nameof(dimensions), "Texture dimensions cannot be negative!");
            dimensions = default;
        }

        int length = dimensions.x * dimensions.y;
        this.pixels = new byte[length];
        if (pixels != null && pixels.Length == length && palette != null && palette.indices.Length > 0)
        {
            Color[] colors = palette.GetColors();
            for (int i = 0; i < length; i++) this.pixels[i] = (byte)pixels[i].ClosestIndex(colors);
        }
    }

    public PalettizedTexture(int width, int height, Color[] pixels, IndexPalette palette) : this(new(width, height), pixels, palette) { }

    //public PalettizedTexture(Vector dimensions, Color[] pixels, IPalette palette)?

    internal readonly byte[] pixels;

    public override void Copy(out Texture copy)
    {
        byte[] newPixels = new byte[pixels.Length];
        pixels.CopyTo(newPixels, 0);
        copy = new PalettizedTexture(dimensions, newPixels);
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

        return new(dimensions, colorPixels);
    }
}

public sealed class TextureAnimation : Animation
{
    public sealed class TextureFrame : Frame
    {
        public Texture texture = null;
        public Vector drawOffset = new(0, 0);
        public bool verticalFlipTexture = false, horizontalFlipTexture = false, verticalFlipOffset = false, horizontalFlipOffset = false, flipOffsetOnPixel = false;
        public IPalette palette = null;//does _draw animate this? should animate call this? how would that work? maybe from get frame?
        public int paletteOffset = 0;
    }

    public TextureAnimation(params TextureFrame[] frames) => this.frames.Add(frames);

    public readonly ICollection<TextureFrame> frames = new Array<TextureFrame>(0x20);

    public Vector drawOffset = new(0, 0);

    public bool verticalFlipTexture = false, horizontalFlipTexture = false, verticalFlipOffset = false, horizontalFlipOffset = false, flipOffsetOnPixel = false;

    public IPalette palette = null;//should this also be animated from get frame?

    public int paletteOffset = 0;

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

    public override void Copy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }
}