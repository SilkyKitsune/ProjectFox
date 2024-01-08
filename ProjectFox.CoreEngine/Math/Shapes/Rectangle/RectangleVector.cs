using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct Rectangle
{
    [MethodImpl(MethodImplOptions.AggressiveInlining)]//are these okay?
    public Vector.Direction DirectionFromZero() => CenterPoint.DirectionFromZero();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionToPoint(Vector value) => CenterPoint.DirectionToPoint(value);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public Vector.Direction DirectionToShape(Rectangle shape) => CenterPoint.DirectionToPoint(shape.CenterPoint);

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Distance(Vector value)
    {
        //this might suck
        /*Vector[] points = Points;
        float[] distances = new float[]
        {
            points[0].Distance(value),
            points[1].Distance(value),
            points[2].Distance(value),
            points[3].Distance(value),
        };

        float smallestDistance = distances[0] < distances[1] ? distances[0] : distances[1];
        if (distances[2] < smallestDistance) smallestDistance = distances[2];
        if (distances[3] < smallestDistance) smallestDistance = distances[3];

        return smallestDistance;*/
        //throw new NotImplementedException();
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float Distance(Rectangle value)
    {
        return default;

        /*
Rectangle.distance(rectangle)
Probably use direction for these checks?
  If (intersecting) return 0

  If (x intersecting)
    Return y distance

  If (y intersecting)
    Return x distance

  Return distance of closest corners

Int I = betweenbounds(posx, rect.posx, rect.endx) || (rect.posx, posx, endx) ? 0b10 : 0b00

I |= betweenbounds(posy, rect.posy, rect.endy) || (rect.posy, posy, endy) ? 0b01 : 0b00

Switch
  0b00
    ?
  0b01 => posx < rect.posx ? rect.posx - endx : posx - rect.endx
  0b10 => ydist
  0b11 => 0```
        */
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceFromZero()
    {
        //this might suck
        /*switch (CenterPoint.DirectionFromZero())
        {
            case Vector.Direction.Equal:
                return 0f;
            case Vector.Direction.YPos:
                {
                    float dist0 = position.DistanceFromZero();
                    float dist1 = new Vector(position.x + size.x, position.y).DistanceFromZero();
                    return dist0 < dist1 ? dist0 : dist1;
                }
            case Vector.Direction.PosQuad:
                return position.DistanceFromZero();
            case Vector.Direction.XPos:
                {
                    float dist0 = position.DistanceFromZero();
                    float dist3 = new Vector(position.x, position.y + size.y).DistanceFromZero();
                    return dist0 < dist3 ? dist0 : dist3;
                }
            case Vector.Direction.PosNegQuad:
                return new Vector(position.x, position.y + size.y).DistanceFromZero();
            case Vector.Direction.YNeg:
                {
                    Vector end = EndPoint;
                    float dist2 = end.DistanceFromZero();
                    float dist3 = new Vector(position.x, end.y).DistanceFromZero();
                    return dist2 < dist3 ? dist2 : dist3;
                }
            case Vector.Direction.NegQuad:
                return EndPoint.DistanceFromZero();
            case Vector.Direction.XNeg:
                {
                    Vector end = EndPoint;
                    float dist1 = new Vector(end.x, position.y).DistanceFromZero();
                    float dist2 = end.DistanceFromZero();
                    return dist1 < dist2 ? dist1 : dist2;
                }
            case Vector.Direction.NegPosQuad:
                return new Vector(position.x + size.x, position.y).DistanceFromZero();
            default:
                throw new Exception($"Direction of {nameof(CenterPoint)} {CenterPoint} was {/*CenterPoint.GetDirection*//*FromZero*(new Vector(0, 0))}");
        }*/
        //throw new NotImplementedException();
        return default;
    }

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceFromZeroSquared() => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceSquared(Vector value) => default;

    /// <summary> Not Yet Implemented </summary>
    /// <returns> default </returns>
    public float DistanceSquared(Rectangle value) => default;
}