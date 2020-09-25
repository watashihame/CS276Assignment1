using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuadraInterpolator : Interpolator
{
    public override Color Interpolate(Vector4 query, Vector4 ori, Texture2D image)
    {
        Color resColor = Color.black;

        float w = (1.0f - Mathf.Abs(query.x - ori.x)) * (1.0f - Mathf.Abs(query.y - ori.y));

        resColor = w * image.GetPixel(Mathf.RoundToInt(ori.z), Mathf.RoundToInt(ori.w));

        return resColor;
    }
}
