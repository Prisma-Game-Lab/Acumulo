using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {
    public GameObject Player;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 v3 = new Vector3(Player.transform.position.x,Player.transform.position.y,transform.position.z);
        transform.position = v3;
	}
}
