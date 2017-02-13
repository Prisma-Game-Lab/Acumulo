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
        gameObject.GetComponentInChildren<Animator>().speed = .25f;
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
        var diff = player.position - transform.position;
        diff.Normalize();

        float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z), 1.2f * Time.deltaTime); 

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