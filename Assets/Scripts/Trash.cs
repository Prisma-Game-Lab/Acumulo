using UnityEngine;

public class Trash : Obstacle
{
    public int value;
    public float deathtime;

    private GameManager _gm;

    void Awake()
    {
        Invoke("die", deathtime);
    }

    void Start()
    {
        gameObject.GetComponent<Animator>().speed = .25f;
        _gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Grow();
            _gm.ScoredTrashTriggered(value);
            die();
        }
    }

    void die()
    {
        Destroy(this.gameObject);
    }
}
