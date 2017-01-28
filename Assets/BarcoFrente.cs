using UnityEngine;
using System.Collections;

public class BarcoFrente : MonoBehaviour {

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().Shrink();
            //other.gameObject.GetComponent<Player>().ScoredTrashTriggered(value);
            //die();
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
