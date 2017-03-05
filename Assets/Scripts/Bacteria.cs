using UnityEngine;

public class Bacteria : Obstacle
{
    public int value;
    public float deathtime;

    private GameManager _gm;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player" && !_gm.PlayerEvolving)
        {
            other.gameObject.GetComponent<Movement>().Slow(1.5f);
            die();
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
    void Awake()
    {
        //Invoke("die", deathtime);
        GameObject obj = GameObject.FindGameObjectWithTag("GM");
        if (obj)
        {
            _gm = obj.GetComponent<GameManager>();
        }
    }
    void Update()
    {

    }
}
