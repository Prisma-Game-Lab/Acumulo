using UnityEngine;

[RequireComponent(typeof(ReduceSize))]
public class Trash : Obstacle
{
    public int value;
    public float deathtime;
    public AudioClip firstAudioClip;
    public AudioClip secondAudioClip;
    public AudioClip thirdAudioClip;
    private AudioSource _lixoSound;

    private void GetAudio()
    {
        int number = Random.Range(1, 3);


        if (number == 1) {
            _lixoSound.clip = firstAudioClip;
        }
        else if(number == 2) { 
                _lixoSound.clip = secondAudioClip;
            }
        else if(number == 3)
        {
            _lixoSound.clip = thirdAudioClip;
        }

        Debug.Log(_lixoSound.clip);
        
            

    }

    private GameManager _gm;

    void Awake()
    {
        Invoke("die", deathtime);
        _lixoSound = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    {
        gameObject.GetComponent<Animator>().speed = .25f;
        GameObject obj = GameObject.FindGameObjectWithTag("GM");
        if(obj)
        {
            _gm = obj.GetComponent<GameManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        _lixoSound.volume = _gm.GetVolume();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !_gm.PlayerEvolving)
        {
            other.gameObject.GetComponent<Player>().Grow(gameObject.tag);
            _gm.ScoredTrashTriggered(value);
            GetAudio();
            _lixoSound.Play();
            Destroy(gameObject);
        }
    }

    void die()
    {
        gameObject.GetComponent<ReduceSize>().ChangeSize();
    }
}
