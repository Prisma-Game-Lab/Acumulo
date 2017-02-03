using UnityEngine;
using System.Collections;

public class wobble : MonoBehaviour {

    
    public float speed, intensity;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.forward, Mathf.Sin(speed * Time.time) * intensity * Random.value );
    }
}
