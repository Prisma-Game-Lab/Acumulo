using UnityEngine;

public class Barreira : Obstacle
{
    public int value;
    public float deathtime;
    private AudioSource boiaSound;
    private GameManager _gm;
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            boiaSound.Play();
            other.gameObject.GetComponent<Movement>().Slow(3);
        }
    }
    /*
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().Fast();
            //Destroy(this.gameObject);
        }
    }
    */
    // Update is called once per frame
    void die()
    {
        Destroy(this.gameObject);
    }
    void Start()
    {
        //Invoke("die", deathtime);
        boiaSound = gameObject.GetComponent<AudioSource>();
        GameObject obj = GameObject.FindGameObjectWithTag("GM");
        if (obj)
        {
            _gm = obj.GetComponent<GameManager>();
        }
    }
    void Update()
    {
        boiaSound.volume = _gm.GetVolume();
    }
}
