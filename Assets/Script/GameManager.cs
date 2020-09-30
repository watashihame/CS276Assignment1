using System.Collections;
using System.Collections.Generic;
//using System.Diagnostics;
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
    [SerializeField]
    float uvTost = 1.0f;

    const float movingSpeed = 0.1f;

    [SerializeField]
    float u = 8.0f;
    [SerializeField]
    float v = 8.0f;
    [SerializeField]
    float z = 2.0f;
    [SerializeField]
    float imageSize = 1.0f;
    [SerializeField]
    float aperture = 2.0f;

    //Texture2D image;

    Interpolator interpolator = new QuadraInterpolator();

    static GameManager _instance = null;
    [SerializeField]
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
        //outPut.ChangeImage(NaiveReconstraction());
        //outPut.ChangeImage(imageData[0, 0]);
    }

    void Update()
    {
        float du = Input.GetAxis("Horizontal");
        float dv = Input.GetAxis("Vertical");
        float dz = Input.GetAxis("Mouse ScrollWheel");
        float da = Input.GetAxis("Aprture");
        float ds = Input.GetAxis("Size");
        if (Input.GetButtonDown("Naive"))
            UsingNaive();
        if (Input.GetButtonDown("FocalPlane"))
            UsingAdvanced();
        if (Input.GetButtonDown("Qudra"))
            interpolator = new QuadraInterpolator();
        if (Input.GetButtonDown("Gaussian"))
            interpolator = new GaussianInterpolator();
        //Debug.Log(du.ToString() + " " + dv.ToString());
        // We think it move enough
        if (Mathf.Abs(du) > .0f || Mathf.Abs(dv) > .0f || Mathf.Abs(da) > .0f || Mathf.Abs(dz) > .0f || Mathf.Abs(ds) > .0f)
        {
            u += du;
            v += dv;
            z += dz;
            aperture += da;
            imageSize += ds;
            u = Mathf.Clamp(u, 0.0f, 15.0f);
            v = Mathf.Clamp(v, 0.0f, 15.0f);
            z = Mathf.Clamp(z, -320.0f, 320.0f);
            aperture = Mathf.Clamp(aperture, 0.5f, 2.0f);
            imageSize = Mathf.Clamp(imageSize, 1.0f, 16.0f);

            switch(curMethod)
            {
                case ReconstractionMethod.Naive: outPut.ChangeImage(NaiveReconstraction()); break;
                case ReconstractionMethod.Advanced: outPut.ChangeImage(AdvancedReconstraction()); break;
            }
            outPut.ChangeSize(imageSize);

            // update image
        }
        //outPut.UpdateInfo(u, v);
        //outPut.ChangeImage(imageData[0, 0]);

    }

    Texture2D NaiveReconstraction()
    {
        Texture2D res = new Texture2D(imageData.filmSize.x, imageData.filmSize.y, TextureFormat.RGBA32, false);

        Texture2D[,] nearby = imageData.NearbyCameraData(u, v, 2);
        Vector4 queryRay = new Vector2(u, v);

        interpolator.Update(.0f, 0.5f, 2, queryRay);

        //Debug.Log(new Vector2(u, v));

        for (int s = 0; s < res.width; ++s)
            for (int t = 0; t < res.height; ++t)
            {
                Color rayColor = interpolator.Interpolate(queryRay, new Vector2Int(s, t), nearby);
                rayColor.a = 1.0f;

                res.SetPixel(s, t, rayColor);
            }
        res.Apply();
        return res;
    }

    Texture2D AdvancedReconstraction()
    {
        Texture2D res = new Texture2D(imageData.filmSize.x, imageData.filmSize.y, TextureFormat.RGBA32, false);
        Texture2D[,] nearby = null;
        Vector2 queryRay = new Vector2(u, v);
        if (interpolator.GetType() == typeof(QuadraInterpolator))
        {
            nearby = imageData.NearbyCameraData(u, v, 2);
            interpolator.Update(z, aperture, 2, queryRay);
        }
        else
        {
            nearby = imageData.NearbyCameraData(u, v, 4);
            interpolator.Update(z, aperture, 4, queryRay);
        }

        // notice z is focal dis
        //Debug.Log(z);

        for (int s = 0; s < res.width; ++s)
            for (int t = 0; t < res.height; ++t)
            {
                Color rayColor = interpolator.Interpolate(queryRay, new Vector2Int(s, t), nearby);
                rayColor.a = 1.0f;
                res.SetPixel(s, t, rayColor);
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
