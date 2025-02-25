using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public abstract class RasterObject : Object2D
{
    public RasterObject(NameID name) : base(name) { }

    public VisualLayer layer = null;

    public bool visible = true, clampToPalette = false, colorFallback = false;

    public VectorF parallaxFactor = new(1f, 1f);
    
#if DEBUG
    public bool drawTextureBounds = false;

    public Color boundsColor = new(byte.MaxValue, 0, 0);
#endif

    //bool wrapPartial?
    //bool repeatTexture?

    protected abstract void GetDrawInfo(
        out Texture texture, out bool verticalFlipTexture, out bool horizontalFlipTexture,
        out Vector drawOffset, out bool verticalFlipOffset, out bool horizontalFlipOffset, out bool flipOffsetOnPixel,
        out IPalette palette, out int paletteOffset);

    internal override void _draw(PortableScreen screen = null)
    {
#if DEBUG
        base._draw(screen);
#endif

        if (!Screen.visible || !visible) return;

        if (layer == null)
        {
            Engine.SendError(ErrorCodes.NullVisualLayer, name);
            return;
        }

        if (!layer.visible || layer.alpha == 0) return;

        Scene scene = owner == null ? this.scene : (owner.owner == null ? owner.scene : owner.Scene);
        
        if (scene != layer.scene)
            Engine.SendError(ErrorCodes.VisualLayerNotInScene, name, layer.name.ToString(),
                $"RasterObject '{name}' drew to a layer from a null/different scene");

        GetDrawInfo(
            out Texture texture, out bool verticalFlipTexture, out bool horizontalFlipTexture,
            out Vector drawOffset, out bool verticalFlipOffset, out bool horizontalFlipOffset, out bool flipOffsetOnPixel,
            out IPalette palette, out int paletteOffset);

        //add null scene check? how would _draw run if scene/owner was null?
        if ((!scene.paused && !paused) || pauseWalks) palette?._animate();//this can probably throw exceptions with compound objects as pets

        if (texture == null)
        {
            Engine.SendError(ErrorCodes.NullTexture, name);
            return;
        }

        if (texture.size.x <= 0 || texture.size.y <= 0) return;//size error?

        bool usePortableScreen = screen != null;

        Rectangle textureArea = new(
            horizontalFlipOffset ? position.x - texture.size.x - drawOffset.x + (flipOffsetOnPixel ? 1 : 0) : position.x + drawOffset.x,
            verticalFlipOffset ? position.y - texture.size.y - drawOffset.y + (flipOffsetOnPixel ? 1 : 0) : position.y + drawOffset.y,
            texture.size),
            screenArea = usePortableScreen ? new(
                parallaxFactor.x == 1f ? screen.viewArea.position.x : (int)(screen.viewArea.position.x * parallaxFactor.x),
                parallaxFactor.y == 1f ? screen.viewArea.position.y : (int)(screen.viewArea.position.y * parallaxFactor.y),
                screen.viewArea.size) :
                new(
                parallaxFactor.x == 1f ? Screen.position.x : (int)(Screen.position.x * parallaxFactor.x),
                parallaxFactor.y == 1f ? Screen.position.y : (int)(Screen.position.y * parallaxFactor.y),
                Screen.size),
            drawArea = screenArea.IntersectionBounds(textureArea);

        if (drawArea.size.x <= 0 || drawArea.size.y <= 0) return;

        int sourceStep;
        Vector topLeft = new(
            drawArea.position.x - textureArea.position.x,
            drawArea.position.y - textureArea.position.y);

        if (verticalFlipTexture)
        {
            topLeft.y = textureArea.size.y - 1 - topLeft.y;
            sourceStep = -textureArea.size.x;
        }
        else sourceStep = textureArea.size.x;

        if (horizontalFlipTexture)
        {
            topLeft.x = textureArea.size.x - 1 - topLeft.x;
            sourceStep += drawArea.size.x;
        }
        else sourceStep -= drawArea.size.x;

        drawArea.position = new(
            drawArea.position.x - screenArea.position.x,
            drawArea.position.y - screenArea.position.y);

#if DEBUG
        if (Debug.debugLayer.visible && drawTextureBounds && boundsColor.a > 0 && (!usePortableScreen || screen.drawDebug))
        {
            Rectangle boundArea = new(
                drawArea.position.x - 1, drawArea.position.y - 1,
                drawArea.size.x + 1, drawArea.size.y + 1);
            Vector boundEnd = new(
                boundArea.position.x + boundArea.size.x,
                boundArea.position.y + boundArea.size.y);
            int boundX, boundD, boundHeight = boundArea.position.y < 0 ? boundArea.size.y : boundArea.size.y + 1;
            bool useAlpha = positionColor.a < byte.MaxValue;

            Color[] debugLayerPixels = usePortableScreen ? Debug.debugLayer.portablePixels : Debug.debugLayer.pixels;
            //are these conditions necessary?
            //top loop
            if (boundArea.position.y >= 0)//can these be combined to two loops?
            {
                boundX = 0;
                boundD = boundArea.position.y * screenArea.size.x + (drawArea.position.x < 0 ? 0 : drawArea.position.x);

                while (boundX++ < drawArea.size.x && boundD < debugLayerPixels.Length)
                {
                    debugLayerPixels[boundD] = useAlpha ? debugLayerPixels[boundD].Blend(boundsColor) : boundsColor;
                    boundD++;
            }
            }
            //bottom loop
            if (boundEnd.y < screenArea.size.y)
            {
                boundX = 0;
                boundD = boundEnd.y * screenArea.size.x + (drawArea.position.x < 0 ? 0 : drawArea.position.x);

                while (boundX++ < drawArea.size.x && boundD < debugLayerPixels.Length)
                {
                    debugLayerPixels[boundD] = useAlpha ? debugLayerPixels[boundD].Blend(boundsColor) : boundsColor;
                    boundD++;
            }
            }
            //left loop
            if (boundArea.position.x >= 0)
            {
                boundX = 0;
                boundD = (boundArea.position.y < 0 ? 0 : boundArea.position.y) * screenArea.size.x + boundArea.position.x;

                while (boundX++ < boundHeight && boundD < debugLayerPixels.Length)
                {
                    debugLayerPixels[boundD] = useAlpha ? debugLayerPixels[boundD].Blend(boundsColor) : boundsColor;
                    boundD += screenArea.size.x;
                }
            }
            //right loop
            if (boundEnd.x < screenArea.size.x)
            {
                boundX = 0;
                boundD = (boundArea.position.y < 0 ? 0 : boundArea.position.y) * screenArea.size.x + boundEnd.x;

                while (boundX++ < boundHeight && boundD < debugLayerPixels.Length)
                {
                    debugLayerPixels[boundD] = useAlpha ? debugLayerPixels[boundD].Blend(boundsColor) : boundsColor;
                    boundD += screenArea.size.x;
                }
            }
        }
#endif

        Color[] layerPixels = usePortableScreen ? layer.portablePixels : layer.pixels;

        int x = 0, s = topLeft.y * textureArea.size.x + topLeft.x,
            d = drawArea.position.y * screenArea.size.x + drawArea.position.x,
            destStep = screenArea.size.x - drawArea.size.x;

        bool palettized = texture.palettized;

        Color[] colors = null;
        int lastColorIndex = 0;
        if (palettized)//anyway to shorten this?
        {
            if (palette == null)
            {
                if (colorFallback) palettized = false;
                else
                {
                Engine.SendError(ErrorCodes.NullPalette, name);
                return;
            }
            }
            else
            {
                colors = palette.GetColors();
            if (colors == null || colors.Length == 0)
            {
                    if (colorFallback) palettized = false;
                    else
                    {
                Engine.SendError(ErrorCodes.EmptyPalette, name);
                return;
            }
                }
                lastColorIndex = colors.Length - 1;
            }
        }

            bool usePaletteOffset = paletteOffset != 0;
        while (s > -1 && s < texture.pixels.Length && d < layerPixels.Length)
            {
            Color pixel = texture.pixels[horizontalFlipTexture ? s-- : s++];

            if (palettized)//anyway to shorten all this?
            {
                int index = (int)pixel.hex;

                if (index == int.MinValue || (!clampToPalette && (index < 0 || index >= colors.Length))) pixel = new(0, 0, 0, 0);
                else
                {
                    if (clampToPalette)
                {
                        if (index >= colors.Length) index = lastColorIndex;
                        else if (index < 0) index = 0;
                }
                    //will it work like this?
                    pixel = colors[usePaletteOffset ? Math.Wrap(index + paletteOffset, 0, lastColorIndex) : index];//inline wrap?
            }
        }

            if (pixel.a == byte.MaxValue) layerPixels[d] = pixel;
            else if (pixel.a > byte.MinValue) layerPixels[d] = layerPixels[d].Blend(pixel);
            d++;

            if (++x == drawArea.size.x)
            {
                x = 0;
                s += sourceStep;
                d += destStep;
            }
        }
    }
}