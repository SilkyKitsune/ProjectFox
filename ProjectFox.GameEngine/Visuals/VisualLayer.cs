using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public class VisualLayer : SceneType//could render layers have a render frequency? maybe raster objects instead? this might make more sense for rasterization
{
    public VisualLayer(NameID name) : base(name) => Clear();

    internal Color[] pixels = null, portablePixels = new Color[0];//temp?

    internal bool usePortablePixels = false;//temp

    public bool visible = true;
    public byte alpha = byte.MaxValue;

    public VisualLayer layerMask = null;

    public sealed override Scene Scene
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scene;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value == null) scene?.RemoveVisualLayer(name);
            else value.AddVisualLayer(this);
        }
    }

    public int Order
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => scene == null ? -1 : scene.visualLayers.codes.IndexOf(name);
        set
        {
            if (scene != null)
            {
                int index = scene.visualLayers.codes.IndexOf(name);
                if (value != index)
                {
                    scene.visualLayers.codes.Move(index, value);
                    scene.visualLayers.values.Move(index, value);
                }
            }
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Clear() => pixels = new Color[Screen.size.x * Screen.size.y];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Fill(Color color)
    {
        for (int i = 0; i < pixels.Length; i++)
            pixels[i] = new(color.r, color.g, color.b);
    }

    protected internal virtual void Blend(Color[] pixels, Color[] reserved)
    {
        bool useAlpha = alpha < byte.MaxValue, useMask = layerMask != null && layerMask.visible;

        if (useMask)
        {
            if (this == layerMask)
            {
                useMask = false;
                Engine.SendError(ErrorCodes.SelfRegistration, name, nameof(layerMask),
                    $"Layer '{name}' attempted to use itself as a mask");
                //return?
            }
            else if (scene != layerMask?.scene)
                Engine.SendError(ErrorCodes.VisualLayerNotInScene, name, layerMask.name.ToString(),
                    $"Layer '{name}' used a mask from a null/different scene");

            Color[] maskPixels = usePortablePixels ? layerMask.portablePixels : layerMask.pixels;

            if (useAlpha)
            {
                float a = alpha / (float)byte.MaxValue;
                for (int i = 0; i < pixels.Length; i++)
                {
                    //reserved[i].a = byte.MaxValue;

                    if (maskPixels[i].a == byte.MinValue)
                    {
                        Color pixel = pixels[i];
                        pixel.a = pixel.a < byte.MaxValue ? pixel.a = (byte)(pixel.a * a) : alpha;
                        reserved[i] = reserved[i].Blend(pixel);

                        //reserved[i].a = byte.MaxValue;//this should be moved if there's going to be an else
                    }
                    //else if (maskPixels[i].a < max)?
                }
                return;
            }

            for (int i = 0; i < pixels.Length; i++)
            {
                //reserved[i].a = byte.MaxValue;

                Color maskPixel = maskPixels[i];
                if (maskPixel.a == byte.MinValue)
                {
                    Color pixel = pixels[i];

                    if (pixel.a == byte.MaxValue) reserved[i] = pixel;
                    else if (pixel.a > byte.MinValue)
                        reserved[i] = reserved[i].Blend(pixel);
                }
                else if (maskPixel.a < byte.MaxValue)
                {
                    Color pixel = pixels[i];

                    if (pixel.a == byte.MaxValue) reserved[i] = pixel.Blend(maskPixel);
                    //else if (pixel.a > byte.MinValue)
                }

                //reserved[i].a = byte.MaxValue;//this should be moved if there's going to be an else
            }
            return;
        }

        if (useAlpha)
        {
            float a = alpha / (float)byte.MaxValue;
            for (int i = 0; i < pixels.Length; i++)
            {
                //reserved[i].a = byte.MaxValue;

                Color pixel = pixels[i];
                pixel.a = pixel.a < byte.MaxValue ? (byte)(pixel.a * a) : alpha;
                reserved[i] = reserved[i].Blend(pixel);

                //reserved[i].a = byte.MaxValue;
            }
            return;
        }
        
        for (int i = 0; i < pixels.Length; i++)
        {
            //reserved[i].a = byte.MaxValue;

            Color pixel = pixels[i];

            if (pixel.a == byte.MaxValue) reserved[i] = pixel;
            else if (pixel.a > byte.MinValue)
                reserved[i] = reserved[i].Blend(pixel);

            //reserved[i].a = byte.MaxValue;
    }
    }//overwriting reserved.a opaque-ifies a portable screens bg
}