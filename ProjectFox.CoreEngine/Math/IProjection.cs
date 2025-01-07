namespace ProjectFox.CoreEngine.Math;

public interface IProjection<T, P, Vi, Vf, Si, Sf, A>
{
    public static abstract P[] OrthographicProjection(T[] values, Vi projectorPosition, Si size, A rotation);

    public static abstract P[] OrthographicProjection(T[] values, Vf projectorPosition, Sf size, A rotation);

    public static abstract P[] PerspectiveProjection(T[] values, Vi projectorPosition, A angle, A rotation);

    public static abstract P[] PerspectiveProjection(T[] values, Vf projectorPosition, A angle, A rotation);

    public abstract P OrthographicProjection(Vi projectorPosition, Si size, A rotation);

    public abstract P OrthographicProjection(Vf projectorPosition, Sf size, A rotation);

    //public abstract P OrthographicProjection(Rectangle projector, float rotation);

    //public abstract P OrthographicProjection(Line2D projector);
    
    public abstract P PerspectiveProjection(Vi projectorPosition, A angle, A rotation);

    public abstract P PerspectiveProjection(Vf projectorPosition, A angle, A rotation);
}