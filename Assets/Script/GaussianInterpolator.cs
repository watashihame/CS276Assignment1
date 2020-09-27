using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianInterpolator : Interpolator
{
    public override Color Interpolate(Vector2 uv, Vector2Int st, Texture2D[,] images)
    { return Interpolate(uv, st, 2.0f, images); }

    public override Color Interpolate(Vector2 uv, Vector2Int st, float size, Texture2D[,] image)
    { return Color.black; }
}
