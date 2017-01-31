using UnityEngine;

public class TrashBio : Obstacle
{
    public int value;
    public float deathtime;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().Grow();
            other.gameObject.GetComponent<Movement>().Bio();
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
    void Update()
    {

    }
}
