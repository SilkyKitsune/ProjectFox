using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

/// <summary> the base of all objects with a 3D Position </summary>
public abstract class Object3D : Object
{
    /// <param name="name"> the object's ID </param>
    public Object3D(NameID name) : base(name) { }

    internal VectorZ position = new(0, 0, 0);

#if DEBUG
    public bool drawPosition = false, intersectingLines = false, depthColor = false;
    public Color positionColor = new(byte.MaxValue, 0, 0);
#endif

    /// <summary> the object's position in world space, or offset relative to the owner if it's a pet </summary>
    public VectorZ Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => owner == null ? position : owner.offsets[petIndex];

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        set
        {
            if (owner != null)
            {
                owner.offsets[petIndex] = value;
                position = new(owner.position.x + value.x, owner.position.y + value.y, owner.position.z + value.z);
            }
            else position = value;
        }
    }

#if DEBUG
    internal override void Draw(PortableScreen screen = null)
    {
        bool usePortableScreen = screen != null, zNeg = position.z < 0;

        if (!Screen.visible || !Debug.debugLayer.visible || !drawPosition || (usePortableScreen && !screen.drawDebug)) return;

        Rectangle screenArea = usePortableScreen ? screen.viewArea : new(Screen.position, Screen.size);
        Color[] layerPixels = usePortableScreen ? Debug.debugLayer.portablePixels : Debug.debugLayer.pixels;
        Color c = depthColor ? new(//behavior to color based on depth
            (byte)(zNeg ? 0 : position.z), byte.MaxValue,//temp
            (byte)(zNeg ? -position.z : 0)) ://clamp z
            positionColor;

        if (c.a == 0) return;

        if (screenArea.Overlapping((Vector)position))//inline cast
        {
            bool useAlpha = positionColor.a < byte.MaxValue;
            Vector pos = new(
                position.x - screenArea.position.x,
                position.y - screenArea.position.y);
            if (intersectingLines)
            {
                int x = 0, d = pos.y * screenArea.size.x;
                while (x++ < screenArea.size.x && d < layerPixels.Length)
                {
                    layerPixels[d] = useAlpha ? layerPixels[d].Blend(c) : c;
                    d++;
                }
                d = pos.x;
                while (d < layerPixels.Length)
                {
                    layerPixels[d] = useAlpha ? layerPixels[d].Blend(c) : c;
                    d += screenArea.size.x;
                }
            }
            else
            {
                int i = pos.y * screenArea.size.x + pos.x;
                layerPixels[i] = useAlpha ? layerPixels[i].Blend(c) : c;
            }
        }

        //behavior for when viewed by camera
    }
#endif

    public Object3D Closest(params Object3D[] objects)
    {
        if (objects.Length == 0)
            return Engine.SendError<Object3D>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return objects[0];

        float delta = float.MaxValue, newDelta;

        Object3D closest = objects[0];
        foreach (Object3D obj in objects)
        {
            if (obj == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
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

    public int ClosestIndex(Object3D[] objects)
    {
        if (objects == null || objects.Length == 0)
            return Engine.SendError<int>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return 0;

        float delta = float.MaxValue, newDelta;

        int closest = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            Object3D obj = objects[i];
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

    public float Distance(Object3D object3D)
    {
        if (object3D == null)
            return Engine.SendError<int>(ErrorCodes.NullArgument, name);//message?

        VectorZ pos0 = owner == null ? position : owner.offsets[petIndex],
            pos1 = object3D.owner == null ? object3D.position : object3D.owner.offsets[object3D.petIndex];

        if (pos0.Equals(pos1)) return 0f;

        int xDelta = pos0.x - pos1.x, yDelta = pos0.y - pos1.y, zDelta = pos0.z - pos1.z;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;
        if (zDelta < 0) zDelta = -zDelta;

        return Math.SqrRoot((xDelta * xDelta) + (yDelta * yDelta) + (zDelta * zDelta));
    }

    public int DistanceSquared(Object3D object3D)
    {
        if (object3D == null)
            return Engine.SendError<int>(ErrorCodes.NullArgument, name);//message?

        VectorZ pos0 = owner == null ? position : owner.offsets[petIndex],
            pos1 = object3D.owner == null ? object3D.position : object3D.owner.offsets[object3D.petIndex];

        if (pos0.Equals(pos1)) return 0;

        int xDelta = pos0.x - pos1.x, yDelta = pos0.y - pos1.y, zDelta = pos0.z - pos1.z;

        if (xDelta < 0) xDelta = -xDelta;
        if (yDelta < 0) yDelta = -yDelta;
        if (zDelta < 0) zDelta = -zDelta;

        return (xDelta * xDelta) + (yDelta * yDelta) + (zDelta * zDelta);
    }

    public Object3D Farthest(params Object3D[] objects)
    {
        if (objects.Length == 0)
            return Engine.SendError<Object3D>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return objects[0];

        float delta = 0f, newDelta;

        Object3D farthest = objects[0];
        foreach (Object3D obj in objects)
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

    public int FarthestIndex(Object3D[] objects)
    {
        if (objects == null || objects.Length == 0)
            return Engine.SendError<int>(ErrorCodes.NullArgument, name, nameof(objects));

        if (objects.Length == 1) return 0;

        float delta = 0f, newDelta;

        int farthest = 0;
        for (int i = 0; i < objects.Length; i++)
        {
            Object3D obj = objects[i];
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
