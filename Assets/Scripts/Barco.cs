using UnityEngine;

public class Barco : Obstacle
{
    public int value;
    public float speed;
    public float CoolDown;
    public float BoostSpeed;

    private float _timer;
    private AudioSource _boatSound;
    private Transform _player;
    private GameManager _gm;
    private Vector3 _targetPos;

    void Awake()
    {
        //gameObject.GetComponentInChildren<Animator>().speed = .25f;
        _player = GameObject.FindWithTag("Player").transform;
        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if(gm)
        {
            _gm = gm.GetComponent<GameManager>();
        }

        _boatSound = gameObject.GetComponent<AudioSource>();

        _timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        _boatSound.volume = _gm.GetVolume();

        _timer += Time.deltaTime;
        if(_timer > CoolDown)
        {
            _targetPos = _player.position;
            var diff = _player.position - transform.position;
            diff.Normalize();

            float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z), 2.5f * Time.deltaTime);

            if (Vector3.Distance(transform.position, _targetPos) > 0.3f)
            {
                transform.Translate(new Vector3(speed * BoostSpeed * Time.deltaTime, 0, 0));
            }
            else
            {
                _timer = 0;
            }

            if(_timer > 2*CoolDown)
            {
                _timer = 0;
            }
        }
        else
        {
            transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }

        //var diff = _player.position - transform.position;
        //diff.Normalize();

        //float rot_z = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        //transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0f, 0f, rot_z), 1.2f * Time.deltaTime);

        //if (Vector3.Distance(transform.position, _player.position) > 0.3f)
        //{
        //    transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        //}
    }

    void die()
    {
        Destroy(this.gameObject);
    }

    //ta pegando a colisao do filho tbm aparentemente
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !_gm.PlayerEvolving)
        {
            other.gameObject.GetComponent<Player>().Grow(gameObject.tag);
            _gm.ScoredTrashTriggered(value);
            die();
        }
    }

}