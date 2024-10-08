﻿using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ImageData : MonoBehaviour
{
    Texture2D[] images = new Texture2D[256];

    Vector2Int filmRes = Vector2Int.zero;

    void Awake()
    {
        for (int i=0;i<256;++i)
        {
            int imageIdx = ((i >> 4) << 4) + (16 - (i & 15));
            images[i] = Resources.Load<Texture2D>(string.Format("Images/lowtoys{0:D3}", imageIdx));
            //if(images[i] != null)
            //{
            //Debug.Log("Loaded: " + string.Format("Images/lowtoys{0:D3} as Idx {1}", imageIdx, i));
            //}
            filmRes.x = images[i].width;
            filmRes.y = images[i].height;
        }
    }

    public Texture2D this [int i, int j]
    {
        get { return images[15 - i + j * 16]; }
        set { images[15 - i + j * 16] = value; }
    }

    public Vector2Int filmSize
    {
        get { return filmRes; }
        set { filmRes = value; }
    }
    // Return a `size` by `size` matrix of images
    public Texture2D[,] NearbyCameraData(float x, float y, int size)
    {
        if ((size & 1) == 1)
            return null;
        Texture2D[,] res = new Texture2D[size, size];
        int halfSize = size >> 1;

        int lowerX = Mathf.FloorToInt(x), lowerY = Mathf.FloorToInt(y);

        for(int i = 0; i < size; ++i)
            for (int j = 0; j<size;++j)
            {
                int qX = lowerX + i - (halfSize - 1), qY = lowerY + j - (halfSize - 1);
                if (qX >= 0 && qX < 16 && qY >= 0 && qY < 16)
                {
                    res[i, j] = this[qX, qY];
                }
                else
                    res[i, j] = Texture2D.blackTexture;
            }

        return res;
    }

    public Vector2[,] NearbyCameraPos(float x, float y, int size)
    {

        if ((size & 1) == 1)
            return null;
        Vector2[,] res = new Vector2[size, size];
        int halfSize = size >> 1;

        int lowerX = Mathf.FloorToInt(x), lowerY = Mathf.FloorToInt(y);

        for (int i = 0; i < size; ++i)
            for (int j = 0; j < size; ++j)
            {
                int qX = lowerX + i - (halfSize - 1), qY = lowerY + j - (halfSize - 1);
                res[i, j] = new Vector2(qX, qY);
            }

        return res;
    }

}
