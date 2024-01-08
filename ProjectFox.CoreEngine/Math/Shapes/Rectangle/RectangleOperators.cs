using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Rectangle
{
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static implicit operator RectangleF(Rectangle rectangle) => new(rectangle.position.x, rectangle.position.y, rectangle.size.x, rectangle.size.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(Rectangle rect1, Rectangle rect2) => rect1.Equals(rect2);
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(Rectangle rect1, Rectangle rect2) => !rect1.Equals(rect2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator +(Rectangle rect1, Rectangle rect2) =>
        new(rect1.position, rect1.size.x + rect2.size.x, rect1.size.y + rect2.size.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator -(Rectangle rect1, Rectangle rect2) =>
        new(rect1.position, rect1.size.x - rect2.size.x, rect1.size.y - rect2.size.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator *(Rectangle rect1, Rectangle rect2) =>
        new(rect1.position, rect1.size.x * rect2.size.x, rect1.size.y * rect2.size.y);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator /(Rectangle rect1, Rectangle rect2)
    {
        if (rect2.size.x == 0 || rect2.size.y == 0) throw new DivideByZeroException();
        return new(rect1.position, rect1.size.x / rect2.size.x, rect1.size.y / rect2.size.y);
    }

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator &(Rectangle rect1, Rectangle rect2) =>
        new(rect1.position.x + rect2.position.x, rect1.position.y + rect2.position.y, rect1.size);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator |(Rectangle rect1, Rectangle rect2) =>
        new(rect1.position.x - rect2.position.x, rect1.position.y - rect2.position.y, rect1.size);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator ^(Rectangle rect1, Rectangle rect2) =>
        new(rect1.position.x * rect2.position.x, rect1.position.y * rect2.position.y, rect1.size);

    /// <exception cref="DivideByZeroException"></exception>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static Rectangle operator %(Rectangle rect1, Rectangle rect2)
    {
        if (rect2.position.x == 0 || rect2.position.y == 0) throw new DivideByZeroException();
        return new(rect1.position.x / rect2.position.x, rect1.position.y / rect2.position.y, rect1.size);
    }
}