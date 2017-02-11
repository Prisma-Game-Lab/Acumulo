using UnityEngine;
using System.Collections;

public class BarcoFrente : MonoBehaviour {

    private GameManager _gm;
    
    void Awake ()
    {
        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if (gm)
        {
            _gm = gm.GetComponent<GameManager>();
        }
    }
	
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Shrink();
            _gm.ReduceScore(10);
        }
    }

    private void Update()
    {
        transform.position = transform.parent.transform.position;
    }
}
