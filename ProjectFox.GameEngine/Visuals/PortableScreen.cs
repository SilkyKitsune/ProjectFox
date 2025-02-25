using ProjectFox.CoreEngine.Math;
using static ProjectFox.GameEngine.Visuals.Screen;
using static ProjectFox.GameEngine.Visuals.Screen.ClearModes;

namespace ProjectFox.GameEngine.Visuals;

public class PortableScreen : RasterObject//rename?
{
    public PortableScreen(NameID name) : base(name) { }

    public Rectangle viewArea = new(0, 0, 0, 0);//rename?

    //public ClearModes clearMode = Clear;
    //public bool useSceneMode = false;

#if DEBUG
    public bool drawViewArea = false, drawDebug = true;
    public Color viewAreaColor = new(byte.MaxValue, 0, 0);

    internal override void _draw(PortableScreen screen = null)
    {
        base._draw(screen);

        bool usePortableScreen = screen != null;

        if (!Screen.visible || !Debug.debugLayer.visible || !drawViewArea || viewAreaColor.a == 0 || (usePortableScreen && !screen.drawDebug)) return;

        Rectangle screenArea = usePortableScreen ? screen.viewArea : new(Screen.position, Screen.size),
            area = screenArea.IntersectionBounds(viewArea);
        Color[] layerPixels = usePortableScreen ? Debug.debugLayer.portablePixels : Debug.debugLayer.pixels;

        if (area.size.x <= 0 || area.size.y <= 0) return;

        area.position = new(
            area.position.x - screenArea.position.x,
            area.position.y - screenArea.position.y);

        bool useAlpha = viewAreaColor.a < byte.MaxValue;
        int x = 0, s = 0, l = area.size.x * area.size.y,
            d = area.position.y * screenArea.size.x + area.position.x,
            step = screenArea.size.x - area.size.x;
        while (s++ < l && d < layerPixels.Length)
        {
            layerPixels[d] = useAlpha ? layerPixels[d].Blend(viewAreaColor) : viewAreaColor;
            d++;

            if (++x == area.size.x)
            {
                x = 0;
                d += step;
            }
        }
    }
#endif

    protected override void GetDrawInfo(out Texture texture, out bool verticalFlipTexture, out bool horizontalFlipTexture, out Vector drawOffset, out bool verticalFlipOffset, out bool horizontalFlipOffset, out bool flipOffsetOnPixel, out IPalette palette, out int paletteOffset)
    {
        texture = default;
        verticalFlipTexture = default;
        horizontalFlipTexture = default;
        drawOffset = default;
        verticalFlipOffset = default;
        horizontalFlipOffset = default;
        flipOffsetOnPixel = default;
        palette = default;
        paletteOffset = default;

        if (viewArea.size.x <= 0 || viewArea.size.y <= 0) return;//size error?

        int length = viewArea.size.x * viewArea.size.y;
        Color[] pixels = new Color[length];

#if DEBUG
        Debug.debugLayer.portablePixels = new Color[length];//a second screen would clear layers before this one had finished, only if one screen was showing the other?
#endif

        for (int i = 0; i < scene.visualLayers.codes.length; i++)
            scene.visualLayers.values.elements[i].portablePixels = new Color[length];

        for (int i = 0; i < scene.objects.codes.length; i++)
        {
            Object obj = scene.objects.values.elements[i];
            if (obj.enabled && !(obj is PortableScreen)) obj._draw(this);//portable screens can't show up in each other
        }

        for (int i = 0; i < scene.visualLayers.codes.length; i++)
        {
            VisualLayer layer = scene.visualLayers.values.elements[i];
            if (layer.visible && layer.alpha != 0)
            {
                layer.usePortablePixels = true;
                layer.Blend(layer.portablePixels, pixels);
                layer.usePortablePixels = false;
            }
        }

#if DEBUG
        if (drawDebug && Debug.debugLayer.visible && Debug.debugLayer.alpha != 0)
            Debug.debugLayer.Blend(Debug.debugLayer.portablePixels, pixels);
#endif

        texture = new(viewArea.size, pixels);
    }
}