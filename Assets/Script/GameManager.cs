using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum ReconstractionMethod
{
    Naive,
    Advanced
}

public class GameManager : MonoBehaviour
{
    [SerializeField]
    ImageData imageData;
    [SerializeField]
    OutPut outPut;

    const float movingSpeed = .1f;

    float u = 8.0f;
    float v = 8.0f;
    float z = 16.0f;

    Texture2D image;

    Interpolator interpolator = new QuadraInterpolator();

    static GameManager _instance = null;

    ReconstractionMethod curMethod = ReconstractionMethod.Naive;

    public static GameManager Insatnce
    {
        get
        {
            if(_instance != null)
                return _instance;
            return _instance = new GameManager();
        }
    }

    private void Start()
    {
        outPut.ChangeImage(NaiveReconstraction());
        //outPut.ChangeImage(imageData[0, 0]);
    }

    void Update()
    {
        float du = Input.GetAxis("Horizontal");
        float dv = Input.GetAxis("Vertical");
        float dz = Input.GetAxis("Mouse ScrollWheel");
        //Debug.Log(du.ToString() + " " + dv.ToString());
        // We think it move enough
        if (Mathf.Abs(du) > .0f || Mathf.Abs(dv) > .0f || Mathf.Abs(dz) > .0f)
        {
            u -= dv;
            v -= du;
            z += dz;
            u = Mathf.Clamp(u, .0f, 15.0f);
            v = Mathf.Clamp(v, .0f, 15.0f);
            z = Mathf.Clamp(z, .0f, 114.0f);

            switch(curMethod)
            {
                case ReconstractionMethod.Naive: outPut.ChangeImage(NaiveReconstraction()); break;
                case ReconstractionMethod.Advanced: outPut.ChangeImage(AdvancedReconstraction()); break;
            }
            

            // update image
        }
        //outPut.UpdateInfo(u, v);
        //outPut.ChangeImage(imageData[0, 0]);

    }

    Texture2D NaiveReconstraction()
    {
        Texture2D res = new Texture2D(imageData.filmSize.x, imageData.filmSize.y, TextureFormat.RGBA32, false);

        Texture2D[,] nearby = imageData.NearbyCameraData(u, v, 2);

        Debug.Log(new Vector2(u, v));

        for (int s = 0; s < res.width; ++s)
            for (int t = 0; t < res.height; ++t)
            {
                Vector4 queryRay = new Vector4(u, v, s, t);
                Color rayColor = Color.black;
                rayColor += interpolator.Interpolate(queryRay, new Vector4(Mathf.Floor(u), Mathf.Floor(v), s, t), nearby[0, 0]);
                rayColor += interpolator.Interpolate(queryRay, new Vector4(Mathf.Floor(u) + 1.0f, Mathf.Floor(v), s, t), nearby[1, 0]);
                rayColor += interpolator.Interpolate(queryRay, new Vector4(Mathf.Floor(u), Mathf.Floor(v) + 1.0f, s, t), nearby[0, 1]);
                rayColor += interpolator.Interpolate(queryRay, new Vector4(Mathf.Floor(u) + 1.0f, Mathf.Floor(v) + 1.0f, s, t), nearby[1, 1]);
                rayColor.a = 1.0f;

                res.SetPixel(s, t, rayColor);
            }
        res.Apply();
        return res;
    }

    Texture2D AdvancedReconstraction()
    {
        Texture2D res = new Texture2D(imageData.filmSize.x, imageData.filmSize.y, TextureFormat.RGBA32, false);

        Texture2D[,] nearby = imageData.NearbyCameraData(u, v, 2);

        for (int s = 0; s < res.width; ++s)
            for (int t = 0; t < res.height; ++t)
            {

            }
        res.Apply();
        return res;
    }

    public void UsingNaive()
    {
        curMethod = ReconstractionMethod.Naive;
    }

    public void UsingAdvanced()
    {
        curMethod = ReconstractionMethod.Advanced;
    }

}
