using UnityEngine;

public class Trash : MonoBehaviour
{

    void OnCollisionEnter2D(Collision2D other)
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
