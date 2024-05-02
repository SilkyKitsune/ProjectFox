namespace ProjectFox.CoreEngine.Collections;

public interface IColorGroup
{
    public abstract bool Grayscale();
    
    public abstract void ModifyHSV(float hueModifier, float saturationModifier, float velocityModifier);
    //ignored colors?
    public abstract void HueShift(float modifier);

    public abstract void SaturationMultiply(float modifier);

    public abstract void VelocityMultiply(float modifier);

    //lightness
    //saturation hsl

    //gamma?

    public abstract bool UniformAlpha();
}
