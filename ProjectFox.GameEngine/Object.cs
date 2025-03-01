using System.Runtime.CompilerServices;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

/// <summary> the most basic type of object inherited by all other objects in the engine </summary>
public abstract class Object : SceneType
{
    /// <param name="name"> the object's ID </param>
    public Object(NameID name) : base(name) { }

    internal CompoundObject owner = null;
    internal int petIndex = -1;

    /// <summary> controls whether the object should process behavior and visual/audio rendering, if true PrePhysics, PreDraw, and PostDraw will not be called </summary>
    public bool enabled = true;

    /// <summary> controls whether the object should process behavior, if true PrePhysics, PreDraw, and PostDraw will not be called </summary>
    public bool paused = false;

    /// <summary> controls whether paused is ignored </summary>
    public bool pauseWalks = false;

    /// <summary> the object's scene or the owner's scene if owned by a CompoundObject, will be null if the object has no scene or owner </summary>
    public sealed override Scene Scene
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => owner == null ? scene : owner.scene;
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (value == null) scene?.RemoveObject(name);
            else value.AddObjects(this);
        }
    }

    /// <summary> the object's owner if it is a pet </summary>
    public CompoundObject Owner
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => owner;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual void _draw(PortableScreen screen = null) { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal virtual void _frame()
    {
        if (!paused || pauseWalks)
        {
            PrePhysics();
            PreDraw();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void PrePhysics() { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected virtual void PreDraw() { }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected internal virtual void PostDraw() { }
}