using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PauseCanvas : MonoBehaviour {

    static private float _value = 1;

	// Use this for initialization
	void Start ()
    {
        GetComponent<Canvas>().worldCamera = Camera.main;
        transform.GetChild(4).GetComponent<Slider>().value = GameObject.FindGameObjectWithTag("GM").GetComponent<AudioSource>().volume;
	}

    public void ChangeValue(float value)
    {
        _value = value;
    }
}
