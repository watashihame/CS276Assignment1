using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class Interpolator
{
    protected float disparaty = 0.0f;
    protected float sigmaSqr = 0.25f;
    protected int size;
    public virtual Color Interpolate(Vector2 uv, Vector2Int st, Texture2D[,] images)
    { return Color.black; }


    public virtual void Update(float _disparaty, float sigma, int _size, Vector2 _uv)
    {
        disparaty = _disparaty;
        sigmaSqr = sigma * sigma;
        size = _size;
    }
}
