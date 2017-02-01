using UnityEngine;

public class TrashBio : Obstacle
{
    public int value;
    public float deathtime;

    private GameManager _gm;

    void Start()
    {
        gameObject.GetComponentInChildren<Animator>().speed = 0.25f;
        _gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Grow();
            other.gameObject.GetComponent<Player>().Bio();
            _gm.ScoredTrashTriggered(value);
            die();
        }
    }
    void die()
    {
        Destroy(this.gameObject);
    }
    void Awake()
    {
        Invoke("die", deathtime);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
