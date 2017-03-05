using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class acheivementbutton : MonoBehaviour {

	public string url;
	public int index;
	public GameObject locked;

	// Use this for initialization
	void Start ()
    {
		if (PlayerPrefs.HasKey ("counter") && PlayerPrefs.GetInt ("counter") >= index)
        {
            enable();
        }	
	}

	void enable()
    {
		Button btn = GetComponent<Button> ();
		btn.onClick.AddListener (onClick);
		locked.SetActive (false);
        GetComponent<Image>().color = new Color(GetComponent<Image>().color.r, GetComponent<Image>().color.g, GetComponent<Image>().color.b, 250);
	}

	void onClick()
    {
		Application.OpenURL (url);
	}
}
