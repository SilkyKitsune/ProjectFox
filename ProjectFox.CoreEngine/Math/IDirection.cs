namespace ProjectFox.CoreEngine.Math;

public interface IDirection<T>
{
    public abstract Vector.Direction DirectionFromZero();

    public abstract Vector.Direction DirectionToPoint(T value);

    //public abstract WeightedDirectionFromZero();?
}
