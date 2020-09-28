using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// fake quadra-interpolate ...
public class QuadraInterpolator : Interpolator
{
    public override Color Interpolate(Vector2 uv, Vector2Int st, Texture2D[,] images)
    {
        float u0 = Mathf.Floor(uv.x);
        float v0 = Mathf.Floor(uv.y);
        float u1 = u0 + 1.0f;
        float v1 = v0 + 1.0f;

        Vector2 uv00 = new Vector2(u0, v0);
        Vector2 uv01 = new Vector2(u0, v1);
        Vector2 uv10 = new Vector2(u1, v0);
        Vector2 uv11 = new Vector2(u1, v1);

        Vector2 st00 = disparaty * (uv00 - uv) + st;
        Vector2 st10 = disparaty * (uv10 - uv) + st;
        Vector2 st01 = disparaty * (uv01 - uv) + st;
        Vector2 st11 = disparaty * (uv11 - uv) + st;

        //Color c000 = Color.Lerp(images[0, 0].GetPixel(Mathf.FloorToInt(st00.x), Mathf.FloorToInt(st00.y)),
        //    images[0, 0].GetPixel(Mathf.FloorToInt(st00.x), Mathf.FloorToInt(st00.y) + 1),
        //    st00.y - Mathf.Floor(st00.y));
        //Color c001 = Color.Lerp(images[0, 0].GetPixel(Mathf.FloorToInt(st00.x) + 1, Mathf.FloorToInt(st00.y)),
        //    images[0, 0].GetPixel(Mathf.FloorToInt(st00.x) + 1, Mathf.FloorToInt(st00.y) + 1),
        //    st00.y - Mathf.Floor(st00.y));
        //Color c00 = Color.Lerp(c000, c001, st00.x - Mathf.Floor(st00.x));

        //Color c010 = Color.Lerp(images[0, 1].GetPixel(Mathf.FloorToInt(st01.x), Mathf.FloorToInt(st01.y)),
        //    images[0, 1].GetPixel(Mathf.FloorToInt(st01.x), Mathf.FloorToInt(st01.y) + 1),
        //    st01.y - Mathf.Floor(st01.y));
        //Color c011 = Color.Lerp(images[0, 1].GetPixel(Mathf.FloorToInt(st01.x) + 1, Mathf.FloorToInt(st01.y)),
        //    images[0, 1].GetPixel(Mathf.FloorToInt(st01.x) + 1, Mathf.FloorToInt(st01.y) + 1),
        //    st01.y - Mathf.Floor(st01.y));
        //Color c01 = Color.Lerp(c010, c011, st01.x - Mathf.Floor(st01.x));

        Color c00 = images[0, 0].GetPixelBilinear(st00.x / (float)images[0, 0].width, st00.y / (float)images[0, 0].height);
        Color c01 = images[0, 1].GetPixelBilinear(st01.x / (float)images[0, 1].width, st01.y / (float)images[0, 1].height);

        Color c0 = Color.Lerp(c00, c01, uv.y - v0);

        //Color c100 = Color.Lerp(images[1, 0].GetPixel(Mathf.FloorToInt(st10.x), Mathf.FloorToInt(st10.y)),
        //    images[1, 0].GetPixel(Mathf.FloorToInt(st10.x), Mathf.FloorToInt(st10.y) + 1),
        //    st10.y - Mathf.Floor(st10.y));
        //Color c101 = Color.Lerp(images[1, 0].GetPixel(Mathf.FloorToInt(st10.x) + 1, Mathf.FloorToInt(st10.y)),
        //    images[1, 0].GetPixel(Mathf.FloorToInt(st10.x) + 1, Mathf.FloorToInt(st10.y) + 1),
        //    st10.y - Mathf.Floor(st10.y));
        //Color c10 = Color.Lerp(c100, c101, st10.x - Mathf.Floor(st10.x));

        //Color c110 = Color.Lerp(images[1, 1].GetPixel(Mathf.FloorToInt(st11.x), Mathf.FloorToInt(st11.y)),
        //    images[1, 1].GetPixel(Mathf.FloorToInt(st11.x), Mathf.FloorToInt(st11.y) + 1),
        //    st11.y - Mathf.Floor(st11.y));
        //Color c111 = Color.Lerp(images[1, 1].GetPixel(Mathf.FloorToInt(st11.x) + 1, Mathf.FloorToInt(st11.y)),
        //    images[1, 1].GetPixel(Mathf.FloorToInt(st11.x) + 1, Mathf.FloorToInt(st11.y) + 1),
        //    st11.y - Mathf.Floor(st11.y));
        //Color c11 = Color.Lerp(c110, c111, st11.x - Mathf.Floor(st11.x));

        Color c10 = images[1, 0].GetPixelBilinear(st10.x / (float)images[1, 0].width, st10.y / (float)images[1, 0].height);
        //Color c10 = Color.black;
        Color c11 = images[1, 1].GetPixelBilinear(st11.x / (float)images[1, 1].width, st11.y / (float)images[1, 1].height);
        Color c1 = Color.Lerp(c10, c11, uv.y - v0);

        Color resColor = Color.Lerp(c0, c1, uv.x - u0);

        return resColor;
    }

}
