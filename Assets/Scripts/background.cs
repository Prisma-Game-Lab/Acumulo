﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class background : MonoBehaviour {

	public float vel;
	public bool dir;



	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
		if (dir==true) {
			if (transform.position.x > 10)
				transform.Translate (new Vector3 (-30, 0, 0));
			transform.Translate (new Vector3 (vel*Time.deltaTime, 0, 0));
		} else {
			if (transform.position.x < -10)
				transform.Translate (new Vector3 (30, 0, 0));
			transform.Translate (new Vector3 (-vel*Time.deltaTime, 0, 0));

		}
	}
}
