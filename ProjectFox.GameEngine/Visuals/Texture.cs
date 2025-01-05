using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Visuals;

public abstract class Texture : ICopy<Texture>
{
    internal Texture(Vector size, bool indexed)
    {
        this.size = size;
        this.indexed = indexed;
    }

    internal readonly bool indexed;//rename palettized?
    internal readonly Vector size;

    public Vector Size => size;
    
    public abstract void DeepCopy(out Texture copy);

    public abstract void ShallowCopy(out Texture copy);

    //rotated copy? 1 clockwise right angle
}

public sealed class ColorTexture : Texture, IColorGroup
{
    private static readonly NameID name = new("ClrTxtr", 0);

    public ColorTexture(Vector size, Color[] pixels = null) : base(size, false)
    {
        if (size.x < 0 || size.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(size), "Texture size cannot be negative!");
            size = new(0, 0);
        }
        
        int length = size.x * size.y;
        if (pixels == null || pixels.Length != length)
            pixels = new Color[length];
        this.pixels = pixels;
    }

    public ColorTexture(int width, int height, Color[] pixels = null) : this(new(width, height), pixels) { }

    internal readonly Color[] pixels;

    public override void DeepCopy(out Texture copy)
    {
        Color[] newPixels = new Color[pixels.Length];
        pixels.CopyTo(newPixels, 0);
        copy = new ColorTexture(size, newPixels);
    }

    public override void ShallowCopy(out Texture copy) => copy = new ColorTexture(size, pixels);

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
    private static readonly NameID name = new("PltTxtr", 0);

    public PalettizedTexture(Vector size, byte[] pixels = null) : base(size, true)
    {
        if (size.x < 0 || size.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(size), "Texture size cannot be negative!");
            size = new(0, 0);
        }

        int length = size.x * size.y;
        if (pixels == null || pixels.Length != length)
            pixels = new byte[length];
        this.pixels = pixels;
    }

    public PalettizedTexture(int width, int height, byte[] pixels = null) : this(new(width, height), pixels) { }

    public PalettizedTexture(Vector size, Color[] pixels, ref ColorPalette palette, bool rampToPalette) : base(size, true)
    {
        if (size.x < 0 || size.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(size), "Texture size cannot be negative!");
            size = new(0, 0);
        }

        palette ??= new();
        int length = size.x * size.y;
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

    public PalettizedTexture(Vector size, Color[] pixels, IndexPalette palette/*, rampToGlobal?*/) : base(size, true)
    {
        if (size.x < 0 || size.y < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(size), "Texture size cannot be negative!");
            size = new(0, 0);
        }

        int length = size.x * size.y;
        this.pixels = new byte[length];
        if (pixels != null && pixels.Length == length && palette != null && palette.indices.Length > 0)
        {
            Color[] colors = palette.GetColors();
            for (int i = 0; i < length; i++) this.pixels[i] = (byte)pixels[i].ClosestIndex(colors);
        }
    }

    public PalettizedTexture(int width, int height, Color[] pixels, IndexPalette palette) : this(new(width, height), pixels, palette) { }

    //public PalettizedTexture(Vector size, Color[] pixels, IPalette palette)?

    internal readonly byte[] pixels;

    public ColorTexture ColorCopy(IPalette palette)
    {
        if (palette == null)
            return Engine.SendError<ColorTexture>(
                ErrorCodes.NullArgument, name, nameof(palette));

        Color[] colors = palette.GetColors(),
            colorPixels = new Color[pixels.Length];

        for (int i = 0; i < pixels.Length; i++)
            colorPixels[i] = colors[Math.Clamp(pixels[i], 0, colors.Length)];

        return new(size, colorPixels);
    }

    public override void DeepCopy(out Texture copy)
    {
        byte[] newPixels = new byte[pixels.Length];
        pixels.CopyTo(newPixels, 0);
        copy = new PalettizedTexture(size, newPixels);
}

    public override void ShallowCopy(out Texture copy) => copy = new PalettizedTexture(size, pixels);
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
        frameCount = frames.length;
        frame = frames.elements[frameIndex >= frameCount || frameIndex < 0 ? 0 : frameIndex];//is this okay?
    }

    public override void DeepCopy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public override void ShallowCopy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }
}