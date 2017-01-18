using UnityEngine;

public class Movement : MonoBehaviour
{
    #region variables

    public float Speed;

    #endregion

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

    /// <summary>
    /// Increases the player's size
    /// </summary>
    public void Grow()
    {
        Vector3 newSize = new Vector3(transform.localScale.x+1,transform.localScale.y+1,transform.localScale.z);
        transform.localScale = newSize;
    }
}
