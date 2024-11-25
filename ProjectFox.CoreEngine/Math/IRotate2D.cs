namespace ProjectFox.CoreEngine.Math;

public interface IRotate2D<V, Vf, A, R>
{
    public static abstract A Angle(V a, V b, V pivot = default);

    public abstract A AngleFromRotationOrigin(/*pivot?*/);

    public abstract Vf Rotate(A amount, Vf pivot = default);

    public abstract V RotateByRightAngles(R rightAngles);

    public abstract Vf RotateByRightAngles(R rightAngles, Vf pivot = default);
}