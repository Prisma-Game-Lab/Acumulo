using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour
{

    public float Speed;

    void Start()
    {
        Speed = 5;
    }
	
	// Update is called once per frame
	void Update ()
	{
       
	    Vector3 target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
	    target.z = transform.position.z;
	    transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);


	}

    public void Grow()
    {
        Vector3 newSize = new Vector3(transform.localScale.x+1,transform.localScale.y+1,transform.localScale.z);
        transform.localScale = newSize;
    }
}
