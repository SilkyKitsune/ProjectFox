using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
#if DEBUG
using ProjectFox.GameEngine.Visuals;
#endif

namespace ProjectFox.GameEngine;

/// <summary> the base of all objects with a 2D Position </summary>
public abstract class Object2D : Object
{
    /// <param name="name"> the object's ID </param>
    public Object2D(NameID name) : base(name) { }

    internal Vector position = new(0, 0);

#if DEBUG
    public bool drawPosition = false, intersectingLines = false;//rename?
    public Color positionColor = new(byte.MaxValue, 0, 0);
#endif

    /// <summary> the object's position in world space, or offset relative to the owner if it's a pet </summary>
    public Vector Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => owner == null ? position : (Vector)owner.offsets[petIndex];//manual inline?

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (owner != null)
            {
                owner.offsets[petIndex] = value;
                position = new(owner.position.x + value.x, owner.position.y + value.y);
            }
            else position = value;
        }
    }

#if DEBUG
    internal override void _draw(VisualLayer layer = null)
    {
        if (!Debug.debugLayer.visible || !drawPosition) return;

        Rectangle screen = new(Screen.position, Screen.size);
        if (screen.Overlapping(position))
        {
            Vector pos = new(
                position.x - screen.position.x,
                position.y - screen.position.y);
            if (intersectingLines)
            {
                int x = 0, d = pos.y * screen.size.x;
                while (x++ < screen.size.x && d < Debug.debugLayer.pixels.Length)
                    Debug.debugLayer.pixels[d++] = positionColor;//blend?
                d = pos.x;
                while (d < Debug.debugLayer.pixels.Length)
                {
                    Debug.debugLayer.pixels[d] = positionColor;//blend?
                    d += screen.size.x;
                }
            }
            else Debug.debugLayer.pixels[pos.y * screen.size.x + pos.x] = positionColor;//blend?
        }
    }
#endif

    public Object2D Closest(params Object2D[] objects)
    {
        if (objects.Length == 0)
            return Engine.SendError<Object2D>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return objects[0];

        float delta = float.MaxValue, newDelta;

        Object2D closest = objects[0];
        foreach (Object2D obj in objects)
        {
            if (obj == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
            //scene check? don't forget 3d if you add this
            else
            {
                if (position.Equals(obj.position)) return obj;
                if (closest != obj)
                {
                    newDelta = position.DistanceSquared(obj.position);
                    if (newDelta < delta)
                    {
                        closest = obj;
                        delta = newDelta;
                    }
                }
            }
        }
        return closest;
    }

    public int ClosestIndex(Object2D[] objects)
    {
        if (objects == null || objects.Length == 0)
            return Engine.SendError<int>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            Object2D obj = objects[i];
            if (obj == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
            //scene check? don't forget 3d if you add this
            else
            {
                if (position.Equals(obj.position)) return i;
                if (objects[closest] != obj)
                {
                    newDelta = position.DistanceSquared(obj.position);
                    if (newDelta < delta)
                    {
                        closest = i;
                        delta = newDelta;
                    }
                }
            }
        }
        return closest;
    }

    public Object2D Farthest(params Object2D[] objects)
    {
        if (objects.Length == 0)
            return Engine.SendError<Object2D>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return objects[0];

        float delta = 0f, newDelta;

        Object2D farthest = objects[0];
        foreach (Object2D obj in objects)
        {
            if (obj == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
            //scene check? don't forget 3d if you add this
            else if (farthest != obj)
            {
                newDelta = position.DistanceSquared(obj.position);
                if (newDelta > delta)
                {
                    farthest = obj;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }

    public int FarthestIndex(Object2D[] objects)
    {
        if (objects == null || objects.Length == 0)
            return Engine.SendError<int>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            Object2D obj = objects[i];
            if (obj == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
            //scene check? don't forget 3d if you add this
            else if (objects[farthest] != obj)
            {
                newDelta = position.DistanceSquared(obj.position);
                if (newDelta > delta)
                {
                    farthest = i;
                    delta = newDelta;
                }
            }
        }
        return farthest;
    }
}
