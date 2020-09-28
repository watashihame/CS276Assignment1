using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GaussianInterpolator : Interpolator
{

    float[,] weight = null;
    Vector2[,] uvs = null;

    float G(Vector2 center, Vector2 _q)
    {
        return 1.0f / (2.0f * Mathf.PI * (sigmaSqr)) * Mathf.Exp((_q - center).sqrMagnitude / -2.0f / sigmaSqr);
    }

    public override void Update(float _disparaty, float sigma, int _size, Vector2 _uv)
    {
        base.Update(_disparaty, sigma, _size, _uv);
        weight = new float[size, size];
        uvs = new Vector2[size, size];
        int halfSize = size >> 1;
        float weightSum = .0f;

        float lowerX = Mathf.Floor(_uv.x), lowerY = Mathf.Floor(_uv.y);

        for (int i = 0; i < _size; ++i)
            for (int j = 0; j < _size; ++j)
            {
                uvs[i, j] = new Vector2(lowerX + i - (halfSize - 1), lowerY + j - (halfSize - 1));
                weight[i, j] = G(_uv, uvs[i, j]);
                weightSum += weight[i, j];
            }

        for (int i = 0; i < _size; ++i)
            for (int j = 0; j < _size; ++j)
                weight[i, j] /= weightSum;


    }
    public override Color Interpolate(Vector2 uv, Vector2Int st, Texture2D[,] images)
    {
        Color res = Color.black;
        for (int i=0;i<size;++i)
            for (int j=0;j<size;++j)
            {
                Vector2 _st = disparaty * (uvs[i, j] - uv) + st;
                res += weight[i, j] * images[i, j].GetPixelBilinear(_st.x / (float)images[i, j].width, _st.y / (float)images[i, j].height);
            }
        return res;
    }

}
