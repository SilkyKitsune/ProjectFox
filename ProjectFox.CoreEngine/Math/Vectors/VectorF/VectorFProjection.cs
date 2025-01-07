using System;
using System.Runtime.CompilerServices;

namespace ProjectFox.CoreEngine.Math;

public partial struct VectorF
{
    public static float[] OrthographicProjection(VectorF[] values, Vector projectorPosition, int size, float rotation)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));

        rotation -= (int)rotation;
        if (rotation < 0f) rotation = 1f + rotation;
        rotation = -rotation;

        bool rotate = rotation == 0f;

        float s = size;
        VectorF projPos = projectorPosition;

        float[] projections = new float[values.Length];
        for (int i = 0; i < projections.Length; i++)
            projections[i] = (rotate ? values[i].Rotate(rotation, projPos).y : values[i].y) - projPos.y / s;

        return projections;
    }

    public static float[] OrthographicProjection(VectorF[] values, VectorF projectorPosition, float size, float rotation)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));

        rotation -= (int)rotation;
        if (rotation < 0f) rotation = 1f + rotation;
        rotation = -rotation;

        bool rotate = rotation == 0f;

        float s = size;

        float[] projections = new float[values.Length];
        for (int i = 0; i < projections.Length; i++)
            projections[i] = (rotate ? values[i].Rotate(rotation, projectorPosition).y : values[i].y) - projectorPosition.y / s;

        return projections;
    }

    public static float[] PerspectiveProjection(VectorF[] values, Vector projectorPosition, float angle, float rotation)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));

        rotation -= (int)rotation;
        if (rotation < 0f) rotation = 1f + rotation;

        bool pivot = projectorPosition.IsZero();

        VectorF projPos = pivot ? projectorPosition : default;

        float[] projections = new float[values.Length];
        for (int i = 0; i < projections.Length; i++)
        {
            VectorF v = values[i];

            if (pivot)
            {
                v.x -= projPos.x;
                v.y -= projPos.y;
            }

            float f = v.AngleFromRotationOrigin() - rotation;
            projections[i] = (f < 0f ? 1f + f : f) / angle;
        }
        return projections;
    }

    public static float[] PerspectiveProjection(VectorF[] values, VectorF projectorPosition, float angle, float rotation)
    {
        if (values == null || values.Length == 0) throw new ArgumentNullException(nameof(values));

        rotation -= (int)rotation;
        if (rotation < 0f) rotation = 1f + rotation;

        bool pivot = projectorPosition.IsZero();

        float[] projections = new float[values.Length];
        for (int i = 0; i < projections.Length; i++)
        {
            VectorF v = values[i];

            if (pivot)
            {
                v.x -= projectorPosition.x;
                v.y -= projectorPosition.y;
            }

            float f = v.AngleFromRotationOrigin() - rotation;
            projections[i] = (f < 0f ? 1f + f : f) / angle;
        }
        return projections;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float OrthographicProjection(Vector projectorPosition, int size, float rotation) =>
        ((rotation == 0f ? y : Rotate(-rotation, projectorPosition).y) - projectorPosition.y) / size;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public float OrthographicProjection(VectorF projectorPosition, float size, float rotation) =>
        ((rotation == 0f ? y : Rotate(-rotation, projectorPosition).y) - projectorPosition.y) / size;

    public float PerspectiveProjection(Vector projectorPosition, float angle, float rotation)
    {
        rotation -= (int)rotation;
        VectorF v = new(x - projectorPosition.x, y - projectorPosition.y);

        float f = v.AngleFromRotationOrigin() - (rotation < 0f ? 1f + rotation : rotation);

        return (f < 0f ? 1f + f : f) / angle;
    }

    public float PerspectiveProjection(VectorF projectorPosition, float angle, float rotation)
    {
        rotation -= (int)rotation;
        VectorF v = new(x - projectorPosition.x, y - projectorPosition.y);

        float f = v.AngleFromRotationOrigin() - (rotation < 0f ? 1f + rotation : rotation);

        return (f < 0f ? 1f + f : f) / angle;
    }
}