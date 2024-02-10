using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Visuals;

public interface IPalette : ICopy<IPalette>
{
    public abstract Color this[byte index] { get; }

    public abstract Color[] GetColors();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual void _animate() { }
}

public class ColorPalette : IPalette, IColorGroup
{
    public ColorPalette(params Color[] colors) => this.colors.Add(colors);

    public readonly ICollection<Color> colors = new Array<Color>(0x10);
    
    public Color this[byte index]
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => ((Array<Color>)colors).elements[index];//index error?
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color[] GetColors() => colors.ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public IPalette Copy() => new ColorPalette(colors.ToArray());

    public void ModifyHSV(float hueModifier, float saturationModifier, float velocityModifier)
    {
        Array<Color> colors = (Array<Color>)this.colors;
        for (int i = 0; i < colors.length; i++)
        {
            colors.elements[i].GetHSV(out float hue, out float sat, out float vel, out float a);
            colors.elements[i] = Color.FromHSV(
                hue + hueModifier, sat * saturationModifier, vel * velocityModifier, a);
        }
    }

    public void HueShift(float modifier)
    {
        Array<Color> colors = (Array<Color>)this.colors;
        for (int i = 0; i < colors.length; i++)
            colors.elements[i].Hue += modifier;
    }

    public void SaturationMultiply(float modifier)
    {
        Array<Color> colors = (Array<Color>)this.colors;
        for (int i = 0; i < colors.length; i++)
            colors.elements[i].Saturation *= modifier;
    }

    public void VelocityMultiply(float modifier)
    {
        Array<Color> colors = (Array<Color>)this.colors;
        for (int i = 0; i < colors.length; i++)
            colors.elements[i].Highest = (byte)(colors.elements[i].Highest * modifier);
    }
}

public abstract class IndexPalette : IPalette
{
    public IndexPalette(params byte[] indices) => this.indices.Add(indices);

    public readonly ICollection<byte> indices = new Array<byte>(0x10);
    
    public abstract Color this[byte index] { get; }

    public Color[] GetColors()
    {
        Array<byte> indices = (Array<byte>)this.indices;
        Color[] colors = new Color[indices.length];

        for (int i = 0; i < indices.length; i++)//can this throw exceptions?
            colors[i] = this[indices.elements[i]];//could this be inlined somehow?

        return colors;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public ColorPalette ColorCopy() => new(GetColors());

    public abstract IPalette Copy();
}

public sealed class PaletteAnimation : Animation, IPalette
{
    public sealed class PaletteFrame : Frame
    {
        public IPalette palette;
    }

    public PaletteAnimation(params PaletteFrame[] frames) => this.frames.Add(frames);

    public readonly ICollection<PaletteFrame> frames = new Array<PaletteFrame>(0x10);

    public Color this[byte index]
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
        frame = frames.elements[frameIndex];
        frameCount = frames.length;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color[] GetColors() => FrameCount <= 0 ? null ://is this okay?
        ((Array<PaletteFrame>)frames).elements[frameIndex].palette.GetColors();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    void IPalette._animate() => _animate();//is this okay?

    public IPalette Copy() => Engine.SendError<IPalette>(ErrorCodes.NotImplemented, new("PalAnim", 0));
}