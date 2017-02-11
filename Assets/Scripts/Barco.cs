using UnityEngine;

public class Barco : Obstacle
{
    public int value;
    public float deathtime;
    public float speed;
    Transform player;

    private GameManager _gm;

    void Awake()
    {
        player = GameObject.FindWithTag("Player").transform;
        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if(gm)
        {
            _gm = gm.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //var targetRotation = Quaternion.LookRotation(player.position - transform.position);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 0.3f * Time.deltaTime);
        transform.LookAt(player.position);
        //transform.RotateAround(player.position, 90);
        //rotacao bugada e precisa de turnspeed baixa p poder contornar
        transform.Rotate(new Vector3(0, -90, 0), Space.Self);
        if (Vector3.Distance(transform.position, player.position) > 0.3f)
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    //ta pegando a colisao do filho tbm aparentemente
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Player>().Grow();
            _gm.ScoredTrashTriggered(value);
            die();
        }
    }

}