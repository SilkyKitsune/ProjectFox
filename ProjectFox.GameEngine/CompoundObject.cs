using System.Runtime.CompilerServices;
using ProjectFox.CoreEngine.Math;
using ProjectFox.GameEngine.Physics;
using ProjectFox.GameEngine.Visuals;

namespace ProjectFox.GameEngine;

public abstract class CompoundObject : Object
{
    /// <param name="name"> the object's ID </param>
    /// <param name="petCount"> number of Objects owned by this CompoundObject </param>
    protected CompoundObject(NameID name, int petCount) : base(name)
    {
        if (petCount < 0)
        {
            Engine.SendError(ErrorCodes.BadArgument, name, nameof(petCount), "Pet Count cannot be negative!");
            petCount = 0;
        }

        objects = new Object[petCount];
        vs = new byte[petCount];
        offsets = new VectorZ[petCount];
    }

    private readonly Object[] objects;//pets?
    private readonly byte[] vs;
    internal readonly VectorZ[] offsets;

    public int followIndex = -1;//leaderIndex, leadingIndex

    internal VectorZ position = new(0, 0, 0);

#if DEBUG
    /// <summary> Not Yet Implemented </summary>
    public bool drawPosition = false, intersectingLines = false;

    /// <summary> Not Yet Implemented </summary>
    public Color positionColor = new(byte.MaxValue, 0, 0);
#endif

    /// <summary> number of Objects owned by this CompoundObject </summary>
    public int PetCount
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => objects.Length;
    }

    /// <summary> the object's position in world space </summary>
    public VectorZ Position
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        get => position;
        set
        {
            position = value;

            foreach (Object obj in objects)
            {
                if (obj == null) Engine.SendError(ErrorCodes.NullPet, name);
                else
                {
                    int i = vs[obj.petIndex];
                    if (i == 1)
                    {
                        VectorZ offset = offsets[obj.petIndex];
                        ((Object2D)obj).position = new(position.x + offset.x, position.y + offset.y);
                    }
                    else if (i == 2)
                    {
                        VectorZ offset = offsets[obj.petIndex];
                        ((Object3D)obj).position = new(position.x + offset.x, position.y + offset.y, position.z + offset.z);
                    }
                }
            }
        }
    }

    internal override void _frame()
    {
        if (!paused || pauseWalks)
        {
            PrePhysics();

            foreach (Object obj in objects)
                if (obj == null) Engine.SendError(ErrorCodes.NullPet, name);
                else if (obj.enabled) obj._frame();

            if (followIndex > -1 && followIndex < objects.Length)
            {
                Object obj = objects[followIndex];
                if (obj != null)
                {
                    int i = vs[obj.petIndex];
                    if (i == 1)
                    {
                        Vector pos = ((Object2D)obj).position;
                        VectorZ offset = offsets[followIndex];
                        Position = new(pos.x - offset.x, pos.y - offset.y, position.z);
                    }
                    else if (i == 2)
                    {
                        VectorZ pos = ((Object3D)obj).position, offset = offsets[followIndex];
                        Position = new(pos.x - offset.x, pos.y - offset.y, pos.z - offset.z);
                    }
                }
            }

            PreDraw();
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal override void Draw(PortableScreen screen = null)
    {
        foreach (Object obj in objects)
            if (obj == null) Engine.SendError(ErrorCodes.NullPet, name);
            else if (obj.enabled) obj.Draw(screen);
#if DEBUG
        //draw pos
#endif
    }

    protected internal override void PostDraw()
    {
        foreach (Object obj in objects)
            if (obj == null) Engine.SendError(ErrorCodes.NullPet, name);
            else if (obj.enabled) obj?.PostDraw();
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected Object GetObject(int index) => index < 0 || index >= objects.Length
        ? Engine.SendError<Object>(ErrorCodes.BadArgument, name, nameof(index)) : objects[index];

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    protected VectorZ GetOffset(int index) => index < 0 || index >= offsets.Length
        ? Engine.SendError<VectorZ>(ErrorCodes.BadArgument, name, nameof(index)) : offsets[index];

    protected void SetObject(int index, Object obj, VectorZ offset = default)
    {
        if (index < 0 || index >= objects.Length)
            Engine.SendError<Object>(ErrorCodes.BadArgument, name, nameof(index));
        else if (obj == null)
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(obj));
        else if (obj.scene != null || obj.owner != null)
            Engine.SendError(ErrorCodes.AlreadyOwnedOrInScene, name, nameof(obj),
                $"{obj.name} scene={scene?.name} owner={owner?.name}");
        else
        {
            Object _obj = objects[index];
            if (_obj != null)
            {
                _obj.owner = null;
                _obj.petIndex = -1;
            }
            
            objects[index] = obj;
            obj.owner = this;
            obj.petIndex = index;

            offsets[index] = offset;

            if (obj is Object2D obj2D)
            {
                vs[index] = 1;
                obj2D.position = new(position.x + offset.x, position.y + offset.y);
            }
            else if (obj is Object3D obj3D)
            {
                vs[index] = 2;
                obj3D.position = new(position.x + offset.x, position.y + offset.y, position.z + offset.z);
            }
        }
    }

    protected void SetOffset(int index, VectorZ offset)
    {
        if (index < 0 || index >= objects.Length)
        {
            Engine.SendError<Object>(ErrorCodes.BadArgument, name, nameof(index));
            return;
        }

        offsets[index] = offset;

        Object obj = objects[index];
        if (obj != null)
        {
            int i = vs[index];
            if (i == 1) ((Object2D)obj).position = new(position.x + offset.x, position.y + offset.y);
            else if (i == 2) ((Object3D)obj).position = new(position.x + offset.x, position.y + offset.y, position.z + offset.z);
        }
    }

    /*
protected? virtual? object[] compoundobject.releasepets()
  object[] = objects
  foreach (object) object.owner = null
  objects = null
  offsets = null
  scene.removeobject(name)
  return object[]
    */

#if DEBUG
    public void DrawPetPositions(bool value)
    {
        foreach (Object obj in objects)
            if (obj is Object2D obj2D)
                obj2D.drawPosition = value;
            //3d
    }

    public void PetPositionsIntersectingLines(bool value)
    {
        foreach (Object2D obj in objects)
            if (obj is Object2D obj2D)
                obj2D.intersectingLines = value;
    }

    public void PetPositionsColor(Color color)
    {
        foreach (Object obj in objects)
            if (obj is Object2D obj2D)
                obj2D.positionColor = color;
            //3d
    }

    public void DrawPhysicsObjects(bool value)
    {
        foreach (Object obj in objects)
            if (obj is PhysicsShape shape)
                shape.drawShape = value;
            //PhysicForm
    }

    public void PhysicsObjectsColor(Color color)
    {
        foreach (Object obj in objects)
            if (obj is PhysicsShape shape)
                shape.shapeColor = color;
            //PhysicForm
    }

    public void DrawTextureBounds(bool value)
    {
        foreach (Object obj in objects)
            if (obj is RasterObject raster)
                raster.drawTextureBounds = value;
    }

    public void TextureBoundsColor(Color color)
    {
        foreach (Object obj in objects)
            if (obj is RasterObject raster)
                raster.boundsColor = color;
    }
#endif
}