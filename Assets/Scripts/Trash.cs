using UnityEngine;

public class Trash : Obstacle
{
    public int value;
    public float deathtime;

    void Start()
    {
        gameObject.GetComponent<Animator>().speed = .25f;
    }

void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().Grow();
            other.gameObject.GetComponent<Player>().ScoredTrashTriggered(value);
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
    void Update ()
    {

    }
}
