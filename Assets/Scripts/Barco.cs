using UnityEngine;

public class Barco : Obstacle
{
    public int value;
    public float deathtime;
    public float speed;
    Transform player;
    //ta pegando a colisao do filho tbm aparentemente
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //other.gameObject.GetComponent<Movement>().Grow();
            other.gameObject.GetComponent<Player>().ScoredTrashTriggered(value);
            //die();
        }
    }
    
    
    void die()
    {
        Destroy(this.gameObject);
    }
    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
    }
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.position);
        //transform.RotateAround(player.position, 90);
        //rotacao bugada e precisa de turnspeed baixa p poder contornar
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        if (Vector3.Distance(transform.position, player.position) > 0.3f)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }
}
