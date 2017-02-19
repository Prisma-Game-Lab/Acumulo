using UnityEngine;

public class Bacteria : Obstacle
{
    public int value;
    public float deathtime;

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<Movement>().Slow();
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
    }
    void Update()
    {

    }
}
