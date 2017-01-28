using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    public Obstacle trash;
    public int num;
    public float time;
    //public AnimationCurve distribution_on_time;
    // Use this for initialization
    float rate,clock;
    void spawn()
    {
        Obstacle o = Instantiate<Obstacle>(trash);
        o.transform.position = Random.insideUnitSphere*10;
        if(clock<time)
            Invoke("spawn", rate);
    }
    void Start () {
        clock = Time.time;
        rate = time / num;
        Invoke("spawn", rate);
	}
	
	// Update is called once per frame
	void Update () {
        clock += Time.deltaTime;
	}
}
