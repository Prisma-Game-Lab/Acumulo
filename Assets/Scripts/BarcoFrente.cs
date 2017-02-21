using UnityEngine;
using System.Collections;

public class BarcoFrente : MonoBehaviour {

    private GameManager _gm;
	public float hit_time;
	private float timer;
    void Awake ()
    {
		timer = 0;
		GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if (gm)
        {
            _gm = gm.GetComponent<GameManager>();
        }
    }
	
    void OnCollisionEnter2D(Collision2D other)
    {
		if (other.gameObject.tag == "Player" && timer>=hit_time)
        {
			timer = 0;
            other.gameObject.GetComponent<Player>().Shrink();
            _gm.ReduceScore(10);
        }
    }

    private void Update()
    {
		timer += Time.deltaTime;
		transform.position = transform.parent.transform.position;
    }
}
