using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Collections;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public interface IPalette : ICopy<IPalette>
{
    public abstract Color this[int index] { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual void _animate() { }

    public abstract Color[] GetColors();
}

public class ColorPalette : IPalette, IColorGroup
{
    private static readonly NameID Name = new("ClrPltt", 0);

    public ColorPalette(params Color[] colors)
    {
        this.colors_.Add(colors);
        this.colors = colors_;
    }

    private readonly Array<Color> colors_ = new(0x10);

    public readonly ICollection<Color> colors;

    public Color this[int index]
    {
        get => index >= colors_.length ?
            Engine.SendError<Color>(ErrorCodes.BadArgument, Name, nameof(index), "Invalid index in ColorPalette")
            : colors_.elements[index];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void DeepCopy(out IPalette copy) => copy = new ColorPalette(colors_.ToArray());

    public bool Grayscale()
    {
        if (colors_.length == 0) return false;
        for (int i = 0; i < colors_.length; i++) if (!colors_.elements[i].IsGrey()) return false;
        return true;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color[] GetColors() => colors_.ToArray();

    public void HueShift(float modifier)
    {
        for (int i = 0; i < colors_.length; i++) colors_.elements[i].Hue += modifier;
    }

    public void ModifyHSV(float hueModifier, float saturationModifier, float velocityModifier)
    {
        for (int i = 0; i < colors_.length; i++)
        {
            colors_.elements[i].GetHSV(out float hue, out float sat, out float vel, out float a);
            colors_.elements[i] = Color.FromHSV(
                hue + hueModifier, sat * saturationModifier, vel * velocityModifier, a);
        }
    }

    public void SaturationMultiply(float modifier)
    {
        for (int i = 0; i < colors_.length; i++) colors_.elements[i].Saturation *= modifier;
    }

    public void ShallowCopy(out IPalette copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public bool UniformAlpha()
    {
        if (colors_.length == 0) return false;

        byte a = colors_.elements[0].a;
        for (int i = 1; i < colors_.length; i++) if (colors_.elements[i].a != a) return false;
        return true;
    }

    public void VelocityMultiply(float modifier)
    {
        for (int i = 0; i < colors_.length; i++) colors_.elements[i].Velocity *= modifier;//colors.elements[i].Highest = (byte)(colors.elements[i].Highest * modifier);
    }
}

public abstract class IndexPalette : IPalette
{
    private static readonly NameID Name = new("IndxPlt", 0);

    public IndexPalette(params byte[] indices)
    {
        this.indices_.Add(indices);
        this.indices = indices_;
    }

    private readonly Array<byte> indices_ = new(0x10);
    
    public readonly ICollection<byte> indices;
    
    public Color this[int index]
    {
        get => index >= indices_.length ?
                Engine.SendError<Color>(ErrorCodes.BadArgument, Name, nameof(index), "Invalid index in IndexPalette") :
            GetColor(indices_.elements[index]);
        }

    public byte this[int index]
    {
        get => index >= indices_.length ?
                Engine.SendError<byte>(ErrorCodes.BadArgument, Name, nameof(index), "Invalid index in IndexPalette") :
            indices_.elements[index];
        }

    protected abstract Color GetColor(byte index);

    public Color[] GetColors()
    {
        Color[] colors = new Color[indices_.length];
        for (int i = 0; i < indices_.length; i++) colors[i] = GetColor(indices_.elements[i]);
        return colors;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ColorPalette ColorCopy() => new(GetColors());

    public abstract void DeepCopy(out IPalette copy);

    public abstract void ShallowCopy(out IPalette copy);
}

public sealed class PaletteAnimation : Animation, IPalette
{
    public sealed class PaletteFrame : Frame
    {
        public IPalette palette = null;
        public int paletteOffset = 0;//how to implement this?
    }

    public PaletteAnimation(params PaletteFrame[] frames) => this.frames.Add(frames);

    public readonly ICollection<PaletteFrame> frames = new Array<PaletteFrame>(0x10);

    public Color this[int index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ((Array<PaletteFrame>)frames).elements[frameIndex].palette[index];
    }

    public override int FrameCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ((Array<PaletteFrame>)frames).length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private protected override void GetFrame(out Frame frame, out int frameCount)
    {
        Array<PaletteFrame> frames = (Array<PaletteFrame>)this.frames;
        frameCount = frames.length;
        frame = frames.elements[frameIndex >= frameCount || frameIndex < 0 ? 0 : frameIndex];
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color[] GetColors()
    {
        Array<PaletteFrame> frames = (Array<PaletteFrame>)this.frames;
        return frames.length <= 0 ? null : frames.elements[frameIndex].palette.GetColors();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IPalette._animate() => _animate();//is this okay?

    public override void DeepCopy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public void DeepCopy(out IPalette copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public override void ShallowCopy(out Animation copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public void ShallowCopy(out IPalette copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }
}