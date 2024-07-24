using System.Runtime.CompilerServices;

namespace ProjectFox.GameEngine;

/// <summary> a lightweight base class for implementing state behaviors </summary>
public abstract class State
{
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void Enter() { }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void PreFrame() { }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void PostFrame() { }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void PostDraw() { }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public virtual void Exit() { }
}

/// <summary> a simple base class for implementing state machines </summary>
public abstract class StateMachine
{
    ///
    public StateMachine() => states = new State[StateCount];
    
    /// <summary> array for all attached states </summary>
    protected readonly State[] states;

    /// <summary> numeric value assigned to the currently active state </summary>
    protected int currentState = 0;

    /// <summary> readonly number of states </summary>
    protected abstract int StateCount { get; }

    /// <summary> calls Enter() on the current state </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CurrentStateEnter() => states[currentState].Enter();

    /// <summary> calls PreFrame() on the current state </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CurrentStatePreFrame() => states[currentState].PreFrame();

    /// <summary> calls PostFrame() on the current state </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CurrentStatePostFrame() => states[currentState].PostFrame();

    /// <summary> calls PostDraw() on the current state </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CurrentStatePostDraw() => states[currentState].PostDraw();

    /// <summary> calls Exit() on the current state </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void CurrentStateExit() => states[currentState].Exit();
}
