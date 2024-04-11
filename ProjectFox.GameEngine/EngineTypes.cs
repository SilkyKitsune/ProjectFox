﻿namespace ProjectFox.GameEngine;

///
public abstract class NamedType
{
    internal NamedType(NameID name) => this.name = name;

    internal readonly NameID name;

    /// <summary> ID given to this instance </summary>
    public NameID Name => name;
}

///
public abstract class SceneType : NamedType
{
    internal SceneType(NameID name) : base(name) { }

    internal Scene scene = null;

    /// <summary>  </summary>
    public abstract Scene Scene { get; set; }
}