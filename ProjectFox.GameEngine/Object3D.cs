using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;

namespace ProjectFox.GameEngine;

/// <summary> the base of all objects with a 3D Position </summary>
public abstract class Object3D : Object
{
    /// <param name="name"> the object's ID </param>
    public Object3D(NameID name) : base(name) { }

    internal VectorZ position = new(0, 0, 0);

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
