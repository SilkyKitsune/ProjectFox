using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine.Visuals;

public abstract class RasterObject : Object2D
{
    public RasterObject(NameID name) : base(name) { }

    public VisualLayer layer = null;

    public bool visible = true, verticalFlipTexture = false, horizontalFlipTexture = false, verticalFlipOffset = false, horizontalFlipOffset = false, flipOffsetOnPixel = false;

    public VectorF parallaxFactor = new(1f, 1f);

#if DEBUG
    public bool drawTextureBounds = false;

    public Color boundsColor = new(byte.MaxValue, 0, 0);
#endif

    protected abstract void GetDrawInfo(out Texture texture, out IPalette palette, out Vector offset/*, vflip, hflip*/);

    internal override void _draw(VisualLayer layer = null)
    {
#if DEBUG
        base._draw();
#endif

        if (layer == null) layer = this.layer;

        if (!Screen.visible || !visible) return;

        if (layer == null)
        {
            Engine.SendError(ErrorCodes.NullVisualLayer, name);
            return;
        }

        if (!layer.visible) return;

        bool isPet = owner != null;
        if ((!isPet && scene != layer.scene) || (isPet && owner.scene != layer.scene))
            Engine.SendError(ErrorCodes.VisualLayerNotInScene, name, layer.name.ToString(),
                $"RasterObject '{name}' drew to a layer from a null/different scene");

        GetDrawInfo(out Texture texture, out IPalette palette, out Vector offset);

        if (!paused || pauseWalks) palette?._animate();

        if (texture == null)
        {
            Engine.SendError(ErrorCodes.NullTexture, name);
            return;
        }

        Rectangle textureArea = new(
            horizontalFlipOffset ? position.x - texture.dimensions.x - offset.x + (flipOffsetOnPixel ? 1 : 0) : position.x + offset.x,
            verticalFlipOffset ? position.y - texture.dimensions.y - offset.y + (flipOffsetOnPixel ? 1 : 0) : position.y + offset.y,
            texture.dimensions),
            screen = new(
                parallaxFactor.x == 1f ? Screen.position.x : (int)(Screen.position.x * parallaxFactor.x),
                parallaxFactor.y == 1f ? Screen.position.y : (int)(Screen.position.y * parallaxFactor.y),
                Screen.size),
            drawArea = screen.IntersectionArea(textureArea);

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
            drawArea.position.x - screen.position.x,
            drawArea.position.y - screen.position.y);

#if DEBUG
        if (drawTextureBounds && Debug.debugLayer.visible)
        {
            Rectangle boundArea = new(
                drawArea.position.x - 1, drawArea.position.y - 1,
                drawArea.size.x + 1, drawArea.size.y + 1);
            Vector boundEnd = new(
                boundArea.position.x + boundArea.size.x,
                boundArea.position.y + boundArea.size.y);
            int boundX, boundD, boundHeight = boundArea.position.y < 0 ? boundArea.size.y : boundArea.size.y + 1;

            //top loop
            if (boundArea.position.y >= 0)
            {
                boundX = 0;
                boundD = boundArea.position.y * screen.size.x + (drawArea.position.x < 0 ? 0 : drawArea.position.x);

                while (boundX++ < drawArea.size.x && boundD < Debug.debugLayer.pixels.Length)
                    Debug.debugLayer.pixels[boundD++] = boundsColor;
            }
            //bottom loop
            if (boundEnd.y < screen.size.y)
            {
                boundX = 0;
                boundD = boundEnd.y * screen.size.x + (drawArea.position.x < 0 ? 0 : drawArea.position.x);

                while (boundX++ < drawArea.size.x && boundD < Debug.debugLayer.pixels.Length)
                    Debug.debugLayer.pixels[boundD++] = boundsColor;
            }
            //left loop
            if (boundArea.position.x >= 0)
            {
                boundX = 0;
                boundD = (boundArea.position.y < 0 ? 0 : boundArea.position.y) * screen.size.x + boundArea.position.x;

                while (boundX++ < boundHeight && boundD < Debug.debugLayer.pixels.Length)
                {
                    Debug.debugLayer.pixels[boundD] = boundsColor;
                    boundD += screen.size.x;
                }
            }
            //right loop
            if (boundEnd.x < screen.size.x)
            {
                boundX = 0;
                boundD = (boundArea.position.y < 0 ? 0 : boundArea.position.y) * screen.size.x + boundEnd.x;

                while (boundX++ < boundHeight && boundD < Debug.debugLayer.pixels.Length)
                {
                    Debug.debugLayer.pixels[boundD] = boundsColor;
                    boundD += screen.size.x;
                }
            }
        }
#endif

        int x = 0, s = topLeft.y * textureArea.size.x + topLeft.x,
            d = drawArea.position.y * screen.size.x + drawArea.position.x,
            destStep = screen.size.x - drawArea.size.x;

        if (texture.indexed)
        {
            if (palette == null)
            {
                Engine.SendError(ErrorCodes.NullPalette, name);
                return;
            }

            Color[] colors = palette.GetColors();
            if (colors == null || colors.Length == 0)
            {
                Engine.SendError(ErrorCodes.EmptyPalette, name);
                return;
            }

            PalettizedTexture palettizedTexture = (PalettizedTexture)texture;
            while (s > -1 && s < palettizedTexture.pixels.Length && d < layer.pixels.Length)
            {
                byte index = palettizedTexture.pixels[horizontalFlipTexture ? s-- : s++];
                Color pixel = index < colors.Length ? colors[index] : new(0, 0, 0, 0);
                //send out of range error?

                if (pixel.a == byte.MaxValue) layer.pixels[d] = pixel;
                else if (pixel.a > byte.MinValue) layer.pixels[d] = layer.pixels[d].Blend(pixel);
                d++;

                if (++x == drawArea.size.x)
                {
                    x = 0;
                    s += sourceStep;
                    d += destStep;
                }
            }
            return;
        }

        ColorTexture colorTexture = (ColorTexture)texture;
        while (s > -1 && s < colorTexture.pixels.Length && d < layer.pixels.Length)
        {
            Color pixel = colorTexture.pixels[horizontalFlipTexture ? s-- : s++];
            if (pixel.a == byte.MaxValue) layer.pixels[d] = pixel;
            else if (pixel.a > byte.MinValue) layer.pixels[d] = layer.pixels[d].Blend(pixel);
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