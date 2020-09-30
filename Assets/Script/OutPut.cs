using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutPut : MonoBehaviour
{
    Image image;
    Text infoText;
    // Start is called before the first frame update
    void Awake()
    {
        image = GetComponentInChildren<Image>();
        infoText = GetComponentInChildren<Text>();

    }

    public void ChangeImage(Texture2D renderedImage)
    {
        image.sprite = Sprite.Create(renderedImage, new Rect(0,0, renderedImage.width, renderedImage.height), new Vector2(0.5f,0.5f));
    }

    public void UpdateInfo(params float[] infoVal)
    {
        infoText.text = string.Format("Camera Pos {0} {1}", infoVal[0].ToString("#.00"), infoVal[1].ToString("#.00"));
    }

    public void ChangeSize(float value)
    {
        image.rectTransform.sizeDelta = value * new Vector2(640,480); 
    }
}
