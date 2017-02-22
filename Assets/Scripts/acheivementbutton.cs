using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class acheivementbutton : MonoBehaviour {

	public string url;

	// Use this for initialization
	void Start () {
		Button btn = GetComponent<Button> ();
		btn.onClick.AddListener (onClick);
	}

	void onClick(){
		Application.OpenURL (url);
	}

	// Update is called once per frame
	void Update () {
		
	}
}
