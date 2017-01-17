using UnityEngine;
using System.Collections;

public class Trash : MonoBehaviour {

	// Use this for initialization

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().Grow();
            other.gameObject.GetComponent<Player>().ScoredTrashTriggered(50);
            Destroy(this.gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
