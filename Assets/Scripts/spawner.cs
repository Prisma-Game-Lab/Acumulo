using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    public Obstacle trash;
    public int num;
    public float time;
    public float RadiusMultiplier = 1;

    private GameObject _player;
    float rate,clock;

    //public AnimationCurve distribution_on_time;

    void Start ()
    {
        clock = Time.time;
        rate = time / num;
        Invoke("spawn", rate);
        _player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        clock += Time.deltaTime;
	}

    void spawn()
    {
        Vector3 topPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 10.0f));
        Vector3 middlePos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0.5f, 10.0f));
        Vector3 bottomPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10.0f));

        Obstacle o = Instantiate<Obstacle>(trash);
        o.transform.position = Random.insideUnitSphere * RadiusMultiplier + _player.transform.position;
        Vector3 newPos;
        float diff;
        if (o.transform.position.y < topPos.y && o.transform.position.y > middlePos.y)
        {
            //joga pra cima
            diff = topPos.y - o.transform.position.y;
            newPos = new Vector3(o.transform.position.x, o.transform.position.y + 2*diff, o.transform.position.z);
            o.transform.position = newPos;
        }
        else if(o.transform.position.y > bottomPos.y && o.transform.position.y < middlePos.y)
        {
            //joga pra baixo
            diff = o.transform.position.y - bottomPos.y;
            newPos = new Vector3(o.transform.position.x, o.transform.position.y - 2*diff, o.transform.position.z);
            o.transform.position = newPos;
        }
        if (clock < time)
            Invoke("spawn", rate);
    }
}
