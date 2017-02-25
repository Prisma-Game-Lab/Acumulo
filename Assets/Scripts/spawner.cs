using UnityEngine;
using System.Collections;

public class spawner : MonoBehaviour {
    public GameObject[] spawnpoints;
    public Obstacle[] trash;
    public int num;
    public float time;
	public float spawn_rate;
	public float initialDelay;
	public AnimationCurve rateovertime;
    public float RadiusMultiplier = 1;

    private GameObject _level1Trash;
    private GameObject _level2Trash;
    private GameObject _level3Trash;

    private GameObject _player;
    float rate,clock;

    //public AnimationCurve distribution_on_time;

    void Start ()
    {
        clock = Time.time;
		rate = Mathf.Floor (rateovertime.Evaluate (clock / time) * num);
		Invoke("spawn", initialDelay);
        _player = GameObject.FindGameObjectWithTag("Player");
        _level1Trash = GameObject.Find("Level1Trash");
        _level2Trash = GameObject.Find("Level2Trash");
        _level3Trash = GameObject.Find("Level3Trash");
        spawnpoints = GameObject.FindGameObjectsWithTag("spawnpoint");
    }
	
	// Update is called once per frame
	void Update ()
    {
        clock += Time.deltaTime;
	}

    bool insidecamera(GameObject point)
    {
        Vector3 topRightPos = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 10.0f));
        Vector3 bottomLeftPos = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 10.0f));

        if((point.transform.position.x > bottomLeftPos.x) && (point.transform.position.x < topRightPos.x)
            && (point.transform.position.y > bottomLeftPos.y) && (point.transform.position.y < topRightPos.y))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void spawn()
    {
		for (int i = 0; i < rate; i++)
        {
			Obstacle o = Instantiate<Obstacle> (trash[Mathf.FloorToInt(Random.Range(0,trash.Length-1))]);
            switch (o.gameObject.tag){
                case "Collectable1":
                    if(_level1Trash)
                    {
                        o.gameObject.transform.SetParent(_level1Trash.transform);
                    }
                    break;
                case "Collectable2":
                    o.gameObject.transform.SetParent(_level2Trash.transform);
                    break;
                case "Collectable3":
                    o.gameObject.transform.SetParent(_level3Trash.transform);
                    break;
                default:
                    break;
            }
            


            GameObject spawnpoint = spawnpoints[Mathf.FloorToInt(Random.Range(0,spawnpoints.Length-1))];
            while(insidecamera(spawnpoint)){
                spawnpoint = spawnpoints[Mathf.FloorToInt(Random.Range(0,spawnpoints.Length-1))];
            }
			o.transform.position = Random.insideUnitSphere * RadiusMultiplier + spawnpoint.transform.position;
			


            //Vector3 newPos;
			//float diff;
			//if (o.transform.position.y < topPos.y && o.transform.position.y > middlePos.y) {
			//	//joga pra cima
			//	diff = topPos.y - o.transform.position.y;
			//	newPos = new Vector3 (o.transform.position.x, o.transform.position.y + 2 * diff, o.transform.position.z);
			//	o.transform.position = newPos;
			//} else if (o.transform.position.y > bottomPos.y && o.transform.position.y < middlePos.y) {
			//	//joga pra baixo
			//	diff = o.transform.position.y - bottomPos.y;
			//	newPos = new Vector3 (o.transform.position.x, o.transform.position.y - 2 * diff, o.transform.position.z);
			//	o.transform.position = newPos;
			//}
		}
		if (clock < time) {
			rate = Mathf.Floor (rateovertime.Evaluate (clock / time) * num);
			Invoke ("spawn", spawn_rate);
		}
    }
}
