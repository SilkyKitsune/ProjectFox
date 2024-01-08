using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct RectangleF
{
    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static explicit operator Rectangle(RectangleF rectangle) => new((int)rectangle.position.x, (int)rectangle.position.y, (int)rectangle.size.x, (int)rectangle.size.y);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator ==(RectangleF rect1, RectangleF rect2) => rect1.Equals(rect2);

    ///
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool operator !=(RectangleF rect1, RectangleF rect2) => !rect1.Equals(rect2);
    
    ///<returns> A rectangle whose position is equal to the left argument and size is the sum of the arguments' sizes </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator +(RectangleF rect1, RectangleF rect2) => new(rect1.position, rect1.size + rect2.size);

    ///<returns> A rectangle whose position is equal to the left argument and size is the difference of the arguments' sizes </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator -(RectangleF rect1, RectangleF rect2) => new(rect1.position, rect1.size - rect2.size);

    ///<returns> A rectangle whose position is equal to the left argument and size is the product of the arguments' sizes </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator *(RectangleF rect1, RectangleF rect2) => new(rect1.position, rect1.size * rect2.size);

    ///<returns> A rectangle whose position is equal to the left argument and size is the quotient of the arguments' sizes </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator /(RectangleF rect1, RectangleF rect2) => new(rect1.position, rect1.size / rect2.size);

    ///<returns> A rectangle whose position is the sum of the arguments' positions and size is equal to the left argument </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator &(RectangleF rect1, RectangleF rect2) => new(rect1.position + rect2.position, rect1.size);

    ///<returns> A rectangle whose position is the difference of the arguments' positions and size is equal to the left argument </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator |(RectangleF rect1, RectangleF rect2) => new(rect1.position - rect2.position, rect1.size);

    ///<returns> A rectangle whose position is the product of the arguments' positions and size is equal to the left argument </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator ^(RectangleF rect1, RectangleF rect2) => new(rect1.position * rect2.position, rect1.size);

    ///<returns> A rectangle whose position is the quotient of the arguments' positions and size is equal to the left argument </returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static RectangleF operator %(RectangleF rect1, RectangleF rect2) => new(rect1.position / rect2.position, rect1.size);
}