using UnityEngine;

[RequireComponent(typeof(ReduceSize))]
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
            other.gameObject.GetComponent<Player>().Grow(gameObject.tag);
            other.gameObject.GetComponent<Player>().Bio();
            _gm.ScoredTrashTriggered(value);
            Destroy(gameObject);
        }
    }
    void die()
    {
        gameObject.GetComponent<ReduceSize>().ChangeSize();
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
