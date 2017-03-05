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

    public float RadiusFar
    {
        get
        {
            return _radiusFar;
        }
        set
        {
            _radiusFar = value;
        }
    }

    private GameObject _level1Trash;
    private GameObject _level2Trash;
    private GameObject _level3Trash;

    private GameObject _player;
    private GameManager _gm;
    float rate,clock;

    static private float _radiusFar;

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

        GameObject obj = GameObject.FindGameObjectWithTag("GM");
        if (obj)
        {
            _gm = obj.GetComponent<GameManager>();
        }

        if(_radiusFar < 3)
        {
            _radiusFar = 3;
        }
    }
	
	// Update is called once per frame
	void Update ()
    {
        clock += Time.deltaTime;
	}

    /// <summary>
    /// Checks if point is inside cam view
    /// </summary>
    /// <param name="point">point</param>
    /// <returns>true if inside</returns>
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

    void OnDrawGizmos()
    {
        if(_player)
        {
            Gizmos.DrawWireSphere(_player.transform.position, RadiusFar);
        }
    }

    /// <summary>
    /// Checks if point is far from player
    /// </summary>
    /// <param name="point">point</param>
    /// <returns>true if far</returns>
    bool FarFromPlayer(GameObject point)
    {
        Vector2 pos = new Vector2(_player.transform.position.x, _player.transform.position.y);
        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(pos, RadiusFar);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.name == point.name)
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Checks if point is on top of other trash
    /// </summary>
    /// <param name="point">point</param>
    /// <returns>true if on top</returns>
    bool OnTopOfTrash(Vector2 point)
    {
        Vector2 pos = new Vector2(point.x, point.y);
        Collider2D[] hitColliders = Physics2D.OverlapPointAll(pos);

        for (int i = 0; i < hitColliders.Length; i++)
        {
            if (hitColliders[i].gameObject.tag != "spawnpoint" && hitColliders[i].gameObject.tag != "Bounds")
            {
                return true;
            }
        }

        return false;
    }

    /// <summary>
    /// spawns the trash
    /// </summary>
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
            int j = 0, p = 0;
            bool foundPoint = false;
            while(!foundPoint && p < 50)
            {
                while ((insidecamera(spawnpoint) || FarFromPlayer(spawnpoint)) && j < 50)// || OnTopOfTrash(spawnpoint))
                {
                    spawnpoint = spawnpoints[Mathf.FloorToInt(Random.Range(0, spawnpoints.Length - 1))];
                    j++;
                }
                o.transform.position = Random.insideUnitSphere * RadiusMultiplier + spawnpoint.transform.position;
                if (!OnTopOfTrash(o.transform.position))
                {
                    foundPoint = true;
                }
                if(insidecamera(o.gameObject))
                {
                    foundPoint = false;
                }
                p++;
            }

            if(o.gameObject.tag == "Collectable1" && _gm.Level == 3)
            {
                Destroy(o.gameObject);
            }
		}
		if (clock < time)
        {
			rate = Mathf.Floor (rateovertime.Evaluate (clock / time) * num);
			Invoke ("spawn", spawn_rate);
		}
    }
}
