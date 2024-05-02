namespace ProjectFox.CoreEngine.Math;

/// <summary> Interface for implementing 2D rotation function </summary>
/// <typeparam name="S"></typeparam>
/// <typeparam name="V"></typeparam>
/// <typeparam name="F"></typeparam>
/// <typeparam name="Vf"></typeparam>
public interface IRotate2D<S, V, F, Vf>
{
    public abstract float Angle(V value, V pivot = default);//should pivot be vf for both?

    public abstract float Angle(Vf value, Vf pivot = default);//could these be combined?

    public abstract float AngleFromRotationOrigin();

    public abstract F Rotate(float amount, Vf pivot = default);

    public abstract S RotateByRightAngles(int rightAngles);

    public abstract F RotateByRightAngles(int rightAngles, Vf pivot = default);
}