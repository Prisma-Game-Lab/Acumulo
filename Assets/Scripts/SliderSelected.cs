using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSelected : MonoBehaviour {

    public Image Image;
    public Image FillImage;
    public Sprite SelectedImage;
    public Sprite DeselectedImage;
    public Sprite FillSelectedImage;
    public Sprite FillDeselectedImage;

    void Start()
    {
        GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>().GetVolume();
    }

    public void ChangeSprite(bool selected)
    {
        if(selected)
        {
            Image.sprite = SelectedImage;
            FillImage.sprite = FillSelectedImage;
        }
        else
        {
            Image.sprite = DeselectedImage;
            FillImage.sprite = FillDeselectedImage;
        }
    }
}
