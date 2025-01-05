using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Physics;
using ProjectFox.GameEngine.Audio;
using ProjectFox.GameEngine.Visuals;
using static ProjectFox.GameEngine.Visuals.Screen;
using static ProjectFox.GameEngine.Visuals.Screen.ClearModes;

namespace ProjectFox.GameEngine;

/// <summary> a single threaded scene </summary>
public class Scene : NamedType
{
    /// <param name="name"> the scene's ID </param>
    public Scene(NameID name) : base(name) => bg.scene = this;//temp

    private readonly Table<Object> objects = new(0x100);
    internal readonly Table<VisualLayer> visualLayers = new(0x40);
    private readonly Table<AudioChannel> audioChannels = new(0x40);

    private ClearModes clearMode = Clear;
    private readonly SetPiece bg = new(new("SceneBG", 0)) { parallaxFactor = new(0f, 0f) };

    public bool paused = false;

    /// <summary> the color used when ClearMode == Fill (black by default) </summary>
    public Color bgColor = new(255, 255, 255);
    
    public ClearModes ClearMode
    {
        get => clearMode;
        set
        {
            switch (value)
            {
                case None:
                    clearMode = value;
                    break;
                case Clear:
                    goto case None;
                case Fill:
                    goto case None;
                case DrawTexture:
                    goto case None;
                default:
                    Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(clearMode));
                    break;
            }
        }
    }
    
    public Texture BGTexture { get => bg.texture; set => bg.texture = value; }

    public IPalette BGPalette { get => bg.palette; set => bg.palette = value; }

    /// <summary> the offset of the background when ClearMode == DrawTexture </summary>
    public Vector BGOffset { get => bg.drawOffset; set => bg.drawOffset = value; }

    public bool BGVerticalFlip { get => bg.verticalFlipTexture; set => bg.verticalFlipTexture = value; }

    public bool BGHorizontalFlip { get => bg.horizontalFlipTexture; set => bg.horizontalFlipTexture = value; }

    internal void _frame()
    {
        switch (clearMode)
        {
            case None:
                break;
            case Clear:
                screenLayer.Clear();
                break;
            case Fill:
                screenLayer.Fill(bgColor);
                break;
            case DrawTexture:
                screenLayer.scene = this;//temp
                bg._draw(screenLayer);
                screenLayer.scene = null;//temp
                break;
            default:
                Engine.SendError(ErrorCodes.BadEnumValue, name, nameof(clearMode));
                clearMode = None;
                break;
        }

        Speakers.speakersChannel.Clear();

        for (int i = 0; i < visualLayers.codes.length; i++)
            visualLayers.values.elements[i].Clear();

        for (int i = 0; i < audioChannels.codes.length; i++)
            audioChannels.values.elements[i].Clear();

        bool paused = this.paused;

        for (int i = 0; i < objects.codes.length; i++)
        {
            Object obj = objects.values.elements[i];
            if (obj.enabled)
            {
                bool p = !paused || obj.pauseWalks;
                if (p) obj._frame();
                obj._draw();
                if (p) obj.PostDraw();
            }
        }

        if (Screen.visible) for (int i = 0; i < visualLayers.codes.length; i++)
            {
                VisualLayer layer = visualLayers.values.elements[i];
                if (layer.visible && layer.alpha != 0) layer.Blend(layer.pixels, screenLayer.pixels);
            }

        if (Speakers.audible) for (int i = 0; i < audioChannels.codes.length; i++)
            {
                AudioChannel channel = audioChannels.values.elements[i];
                if (channel.audible && channel.volume != 0f && (channel.leftVolume != 0f || channel.rightVolume != 0f))
                    channel.Blend();
            }
    }

    #region Objects
    public void AddObject(Object obj)
    {
        if (obj == null)
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
        else if (obj.owner != null)
            Engine.SendError(ErrorCodes.AlreadyOwnedOrInScene, name, nameof(obj));
        else if (objects.codes.Contains(obj.name))
            Engine.SendError(
                ErrorCodes.AlreadyOwnedOrInScene,
                name, nameof(obj),
                $"Scene '{name}' already contains object '{obj.name}'");
        else
        {
            obj.scene?.RemoveObject(obj.name);
            objects.AddDirect(obj.name, obj);
            obj.scene = this;
        }
    }

    /// <summary></summary>
    /// <param name="objects"></param>
    /// <remarks> Possible Errors: "NullArgument", "AlreadyOwnedOrInScene" </remarks>
    public void AddObjects(params Object[] objects)
    {
        if (objects == null || objects.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(objects));
            return;
        }

        foreach (Object obj in objects)
            if (obj == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
            else if (obj.owner != null)
                Engine.SendError(ErrorCodes.AlreadyOwnedOrInScene, name, nameof(obj));
            else if (this.objects.codes.Contains(obj.name))
                Engine.SendError(
                    ErrorCodes.AlreadyOwnedOrInScene,
                    name, nameof(obj),
                    $"Scene '{name}' already contains object '{obj.name}'");
            else
            {
                obj.scene?.RemoveObject(obj.name);
                this.objects.AddDirect(obj.name, obj);
                obj.scene = this;
            }
    }

    public void RemoveObject(NameID name)
    {
        int index = objects.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"Object '{name}' could not be found in scene '{this.name}'");
            return;
        }
        objects.values.elements[index].scene = null;
        objects.RemoveAt(index);
    }

    public void RemoveObjects(params NameID[] names)
    {
        if (names == null || names.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(names));
            return;
        }

        foreach (NameID name in names)
        {
            int index = objects.codes.IndexOf(name);
            if (index < 0) Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"Object '{name}' could not be found in scene '{this.name}'");
            else
            {
                objects.values.elements[index].scene = null;
                objects.RemoveAt(index);
            }
        }
    }
    #endregion

    #region VisualLayers
    public void AddVisualLayer(VisualLayer layer)
    {
        if (layer == null)
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(layer));
        else if (visualLayers.codes.Contains(layer.name))
            Engine.SendError(
                ErrorCodes.AlreadyOwnedOrInScene,
                name, nameof(layer),
                $"Scene '{name}' already contains layer '{layer.name}'");
        else
        {
            layer.scene?.RemoveVisualLayer(layer.name);
            visualLayers.AddDirect(layer.name, layer);
            layer.scene = this;
            layer.Clear();
        }
    }

    public void AddVisualLayers(params VisualLayer[] layers)
    {
        if (layers == null || layers.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(layers));
            return;
        }

        foreach (VisualLayer layer in layers)
            if (layer == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(layer));
            else if (visualLayers.codes.Contains(layer.name))
                Engine.SendError(
                    ErrorCodes.AlreadyOwnedOrInScene,
                    name, nameof(layer),
                    $"Scene '{name}' already contains layer '{layer.name}'");
            else
            {
                layer.scene?.RemoveVisualLayer(layer.name);
                visualLayers.AddDirect(layer.name, layer);
                layer.scene = this;
                layer.Clear();
            }
    }

    public void RemoveVisualLayer(NameID name)
    {
        int index = visualLayers.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"Layer '{name}' could not be found in scene '{this.name}'");
            return;
        }
        visualLayers.values.elements[index].scene = null;
        visualLayers.RemoveAt(index);
    }

    public void RemoveVisualLayers(params NameID[] names)
    {
        if (names == null || names.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(names));
            return;
        }

        foreach (NameID name in names)
        {
            int index = visualLayers.codes.IndexOf(name);
            if (index < 0) Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"Layer '{name}' could not be found in scene '{this.name}'");
            else
            {
                visualLayers.values.elements[index].scene = null;
                visualLayers.RemoveAt(index);
            }
        }
    }
    #endregion

    #region AudioChannels
    public void AddAudioChannel(AudioChannel channel)
    {
        if (channel == null)
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(channel));
        else if (audioChannels.codes.Contains(channel.name))
            Engine.SendError(
                ErrorCodes.AlreadyOwnedOrInScene,
                name, nameof(channel),
                $"Scene '{name}' already contains channel '{channel.name}");
        else
        {
            channel.scene?.RemoveAudioChannel(channel.name);
            audioChannels.AddDirect(channel.name, channel);
            channel.scene = this;
            channel.Clear();
        }
    }

    public void AddAudioChannels(params AudioChannel[] channels)
    {
        if (channels == null || channels.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(channels));
            return;
        }

        foreach (AudioChannel channel in channels)
            if (channel == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(channel));
            else if (audioChannels.codes.Contains(channel.name))
                Engine.SendError(
                    ErrorCodes.AlreadyOwnedOrInScene,
                    name, nameof(channel),
                    $"Scene '{name}' already contains channel '{channel.name}'");
            else
            {
                channel.scene?.RemoveAudioChannel(channel.name);
                audioChannels.AddDirect(channel.name, channel);
                channel.scene = this;
                channel.Clear();
            }
    }

    public void RemoveAudioChannel(NameID name)
    {
        int index = audioChannels.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"Channel '{name}' could not be found in scene '{this.name}'");
            return;
        }
        audioChannels.values.elements[index].scene = null;
        audioChannels.RemoveAt(index);
    }

    public void RemoveAudioChannels(params NameID[] names)
    {
        if (names == null || names.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(names));
            return;
        }

        foreach (NameID name in names)
        {
            int index = audioChannels.codes.IndexOf(name);
            if (index < 0) Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"Channel '{name} could not be found in scene '{this.name}'");
            else
            {
                audioChannels.values.elements[index].scene = null;
                audioChannels.RemoveAt(index);
            }
        }
    }
    #endregion
}