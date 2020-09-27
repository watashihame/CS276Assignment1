using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class Interpolator
{
    public float disparaty = 0.0f;

    Vector2 error = new Vector2(0,0);
    public virtual Color Interpolate(Vector2 uv, Vector2Int st, Texture2D[,] images)
    { return Color.black; }

    public virtual Color Interpolate(Vector2 uv, Vector2Int st, float size, Texture2D[,] image)
    { return Color.black; }
}
