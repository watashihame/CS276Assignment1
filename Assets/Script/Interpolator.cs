using System.Collections;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class Interpolator
{
    public virtual Color Interpolate(Vector4 query, Vector4 ori, Texture2D image)
    { return Color.black; }
}
