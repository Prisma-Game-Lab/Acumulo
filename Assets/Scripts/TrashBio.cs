using UnityEngine;

[RequireComponent(typeof(ReduceSize))]
public class TrashBio : Obstacle
{
    public int value;
    public float deathtime;
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
    public AudioClip thirdAudioClip;
    private AudioSource _lixoBioSound;

    
    private GameManager _gm;

    void Start()
    {
        gameObject.GetComponentInChildren<Animator>().speed = 0.25f;
        _gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
        _lixoBioSound = GetComponent<AudioSource>();

    }

    private void GetAudio()
    {
        int number = Random.Range(1, 3);


        if (number == 1)
        {
            _lixoBioSound.clip = firstAudioClip;
        }
        else if (number == 2)
        {
            _lixoBioSound.clip = secondAudioClip;
        }
        else if (number == 3)
        {
            _lixoBioSound.clip = thirdAudioClip;
        }

        Debug.Log(_lixoBioSound.clip);

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !_gm.PlayerEvolving)
        {
            other.gameObject.GetComponent<Player>().Grow(gameObject.tag);
            other.gameObject.GetComponent<Player>().Bio();
            _gm.ScoredTrashTriggered(value);
            GetAudio();
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
        _lixoBioSound.volume = _gm.GetVolume();
    }
}
