namespace ProjectFox.GameEngine.Physics;

public sealed class PhysicsSpace : NamedType
{
    public PhysicsSpace(NameID name) : base(name) { }

    internal readonly HashArray<PhysicsSpace> scanSpaces = new HashArray<PhysicsSpace>(0x20);
    //do these need to be hash array?
    internal readonly HashArray<PhysicsRectangle> rectangles = new HashArray<PhysicsRectangle>(0x100);
    //polygons
    //circles
    //rays

    //getshape?

    #region ScanSpaces
    public void AddSpaceToScan(PhysicsSpace space)
    {
        if (space == null)
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(space));
        else if (name.Equals(space.name))
            Engine.SendError(ErrorCodes.SelfRegistration, name, nameof(space),
                $"PhysicsSpace '{name}' attempted to add itself as a scanSpace");
        else if (scanSpaces.ContainsCode(space.name))
            Engine.SendError(ErrorCodes.AlreadyOwnedOrInScene,
                name, nameof(space),
                $"PhysicsSpace '{name}' already contains scanSpace '{space.name}'");
        else scanSpaces.AddDirect(space.name, space);
    }

    public void AddSpacesToScan(params PhysicsSpace[] spaces)
    {
        if (spaces == null || spaces.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(spaces));
            return;
        }

        foreach (PhysicsSpace space in spaces)
            if (space == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(space));
            else if (name.Equals(space.name))
                Engine.SendError(ErrorCodes.SelfRegistration, name, nameof(space),
                    $"PhysicsSpace '{name}' attempted to add itself as a scanSpace");
            else if (scanSpaces.ContainsCode(space.name))
                Engine.SendError(ErrorCodes.AlreadyOwnedOrInScene,
                    name, nameof(space),
                    $"PhysicsSpace '{name}' already contains scanSpace '{space.name}'");
            else scanSpaces.AddDirect(space.name, space);
    }

    public PhysicsSpace GetScanSpace(NameID name)
    {
        int index = scanSpaces.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"'{name}' could not be found in PhysicsSpace '{this.name}'");
            return null;
        }
        return scanSpaces.values.elements[index];
    }

    public PhysicsSpace[] GetScanSpaces() => scanSpaces.GetValues();

    public void RemoveSpaceToScan(NameID name)
    {
        int index = scanSpaces.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"'{name}' could not be found in PhysicsSpace '{this.name}'");
            return;
        }
        scanSpaces.RemoveAt(index);
    }

    public void RemoveSpacesToScan(params NameID[] names)
    {
        if (names == null || names.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(names));
            return;
        }
        
        foreach (NameID name in names)
        {
            int index = scanSpaces.codes.IndexOf(name);
            if (index < 0) Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"'{name}' could not be found in PhysicsSpace '{this.name}'");
            else scanSpaces.RemoveAt(index);
        }
    }
    #endregion

    #region Rectangles
    public void AddRectangle(PhysicsRectangle rectangle)
    {
        if (rectangle == null)
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(rectangle));
        else if (rectangles.ContainsCode(rectangle.name))
            Engine.SendError(
                ErrorCodes.AlreadyOwnedOrInScene,
                name, nameof(rectangle),
                $"PhysicsSpace '{name}' already contains rectangle '{rectangle.name}'");
        else
        {
            rectangle.space?.RemoveRectangle(rectangle.name);
            rectangles.AddDirect(rectangle.name, rectangle);
            rectangle.space = this;
        }
    }

    public void AddRectangles(params PhysicsRectangle[] rectangles)
    {
        if (rectangles == null || rectangles.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(rectangles));
            return;
        }
        
        foreach (PhysicsRectangle rectangle in rectangles)
            if (rectangle == null)
                Engine.SendError(ErrorCodes.NullArgument, name, nameof(rectangle));
            else if (this.rectangles.ContainsCode(rectangle.name))
                Engine.SendError(
                    ErrorCodes.AlreadyOwnedOrInScene,
                    name, nameof(rectangle),
                    $"PhysicsSpace '{name}' already contains rectangle '{rectangle.name}'");
            else
            {
                rectangle.space?.RemoveRectangle(rectangle.name);
                this.rectangles.AddDirect(rectangle.name, rectangle);
                rectangle.space = this;
            }
    }

    public PhysicsRectangle GetRectangle(NameID name)
    {
        int index = rectangles.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"'{name}' could not be found in PhysicsSpace '{this.name}'");
            return null;
        }
        return rectangles.values.elements[index];
    }

    public PhysicsRectangle[] GetRectangles() => rectangles.GetValues();

    public void RemoveRectangle(NameID name)
    {
        int index = rectangles.codes.IndexOf(name);
        if (index < 0)
        {
            Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"'{name}' could not be found in PhysicsSpace '{this.name}'");
            return;
        }
        rectangles.values.elements[index].space = null;
        rectangles.RemoveAt(index);
    }

    public void RemoveRectangles(params NameID[] names)
    {
        if (names == null || names.Length == 0)
        {
            Engine.SendError(ErrorCodes.NullArgument, name, nameof(names));
            return;
        }

        foreach (NameID name in names)
        {
            int index = rectangles.codes.IndexOf(name);
            if (index < 0) Engine.SendError(
                ErrorCodes.BadArgument,
                this.name, nameof(name),
                $"'{name}' could not be found in PhysicsSpace '{this.name}'");
            else
            {
                rectangles.values.elements[index].space = null;
                rectangles.RemoveAt(index);
            }
        }
    }
    #endregion
}