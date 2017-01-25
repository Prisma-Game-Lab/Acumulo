using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;
    public float smoothSpeed;
    public Vector3 specificVector;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        specificVector = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);

        gameObject.transform.position = Vector3.Lerp(transform.position,specificVector,smoothSpeed*Time.deltaTime);
    }
}
