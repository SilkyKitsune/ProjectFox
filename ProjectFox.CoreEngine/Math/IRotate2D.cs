namespace ProjectFox.CoreEngine.Math;

/// <summary> Interface for implementing 2D rotation function </summary>
/// <typeparam name="S"></typeparam>
/// <typeparam name="V"></typeparam>
/// <typeparam name="F"></typeparam>
/// <typeparam name="Vf"></typeparam>
public interface IRotate2D<S, V, F, Vf>
{
    public abstract float AngleFromRotationOrigin();

    //public abstract float Angle(V value);
    //public abstract float Angle(Vf value);

    public abstract float Angle(V value, V pivot = default);///////////////
    public abstract float Angle(Vf value, Vf pivot = default);

    //public abstract F Rotate(float amount);//combine?
    //public abstract F Rotate(float amount, V pivot = default);//combine?
    public abstract F Rotate(float amount, Vf pivot = default);

    //public abstract F RotateByRadians(float radians);
    //public abstract F RotateByRadians(V pivot, float radians);
    public abstract F RotateByRadians(float radians, Vf pivot = default);

    //public abstract F RotateByDegrees(float degrees);
    //public abstract F RotateByDegrees(V pivot, float degrees);
    public abstract F RotateByDegrees(float degrees, Vf pivot = default);

    public abstract S RotateByRightAngles(int rightAngles);
    public abstract S RotateByRightAngles(V pivot, int rightAngles);
    public abstract S/*F?*/ RotateByRightAngles(Vf pivot, int rightAngles);
}