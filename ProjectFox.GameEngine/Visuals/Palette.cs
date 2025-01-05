using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.CoreEngine.Collections;

namespace ProjectFox.GameEngine.Visuals;

public interface IPalette : ICopy<IPalette>
{
    public abstract Color this[byte index] { get; }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual void _animate() { }

    public abstract Color[] GetColors();
}

public class ColorPalette : IPalette, IColorGroup
{
    private static readonly NameID Name = new("ClrPltt", 0);

    public ColorPalette(params Color[] colors) => this.colors.Add(colors);

    public readonly ICollection<Color> colors = new Array<Color>(0x10);
    
    public Color this[byte index]
    {
        get
        {
            Array<Color> colors = (Array<Color>)this.colors;
            return index >= colors.length ?
                Engine.SendError<Color>(ErrorCodes.BadArgument, Name, nameof(index), "Invalid index in ColorPalette") :
                colors.elements[index];
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Color[] GetColors() => colors.ToArray();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Copy(out IPalette copy) => copy = new ColorPalette(colors.ToArray());

    public bool Grayscale()
    {
        Array<Color> colors = (Array<Color>)this.colors;

        if (colors.length == 0) return false;

        for (int i = 0; i < colors.length; i++) if (!colors.elements[i].IsGrey()) return false;
        return true;
    }

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

    public void ShallowCopy(out IPalette copy)
    {
        copy = null;
        Engine.SendError(ErrorCodes.NotImplemented, default);
    }

    public void VelocityMultiply(float modifier)
    {
        Array<Color> colors = (Array<Color>)this.colors;
        for (int i = 0; i < colors.length; i++)
            colors.elements[i].Velocity *= modifier;//colors.elements[i].Highest = (byte)(colors.elements[i].Highest * modifier);
    }

    public bool UniformAlpha()
    {
        Array<Color> colors = (Array<Color>)this.colors;

        if (colors.length == 0) return false;

        byte a = colors.elements[0].a;
        for (int i = 1; i < colors.length; i++) if (colors.elements[i].a != a) return false;
        return true;
    }
}

public abstract class IndexPalette : IPalette
{
    private static readonly NameID Name = new("IndxPlt", 0);

    public IndexPalette(params byte[] indices) => this.indices.Add(indices);

    public readonly ICollection<byte> indices = new Array<byte>(0x10);
    
    public Color this[byte index]
    {
        get
        {
            Array<byte> indices = (Array<byte>)this.indices;
            return index >= indices.length ?
                Engine.SendError<Color>(ErrorCodes.BadArgument, Name, nameof(index), "Invalid index in IndexPalette") :
                GetColor(indices.elements[index]);
        }
    }

    public byte this[int index]
    {
        get
        {
            Array<byte> indices = (Array<byte>)this.indices;
            return index >= indices.length ?
                Engine.SendError<byte>(ErrorCodes.BadArgument, Name, nameof(index), "Invalid index in IndexPalette") :
                indices.elements[index];
        }
    }

    protected abstract Color GetColor(byte index);

    public Color[] GetColors()
    {
        Array<byte> indices = (Array<byte>)this.indices;
        Color[] colors = new Color[indices.length];
        
        for (int i = 0; i < indices.length; i++) colors[i] = GetColor(indices.elements[i]);

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