using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region variables

    public float bioshrinktime = 1;
    public float growfactor = 1;

    #endregion

    // Use this for initialization
    void Start ()
	{

	}

	// Update is called once per frame
	void Update ()
    {
	    
	}

    /// <summary>
    /// Increases the player's size
    /// </summary>
    public void Grow()
    {
        Vector3 newSize = new Vector3(transform.localScale.x * growfactor, transform.localScale.y * growfactor, transform.localScale.z);
        transform.localScale = newSize;
    }
    /// <summary>
    /// Reduce the player's size
    /// </summary>
    public void Shrink()
    {
        Vector3 newSize = new Vector3(transform.localScale.x / growfactor, transform.localScale.y / growfactor, transform.localScale.z);
        transform.localScale = newSize;
    }
    public void Bio()
    {
        Invoke("Shrink", bioshrinktime);
    }
}
