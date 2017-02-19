using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region variables

    public float bioshrinktime = 1;
    public float growfactor = 0.01f;
    public int Size = 1;
    public int[] SizeTriggers;
    public Noticia News;

    private int _sizeTriggerCounter;

    #endregion

    // Use this for initialization
    void Start ()
	{
        Size = 0;
        _sizeTriggerCounter = 0;
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
        Vector3 newSize = new Vector3(transform.localScale.x + growfactor, transform.localScale.y + growfactor, transform.localScale.z);
        transform.localScale = newSize;
        Size++;
        if(Size == SizeTriggers[_sizeTriggerCounter])
        {
            _sizeTriggerCounter++;
            News.TriggerTextNews();
        }
    }
    /// <summary>
    /// Reduce the player's size
    /// </summary>
    public void Shrink()
    {
        Vector3 newSize = new Vector3(transform.localScale.x - growfactor, transform.localScale.y - growfactor, transform.localScale.z);
        transform.localScale = newSize;
        Size--;
    }
    public void Bio()
    {
        Invoke("Shrink", bioshrinktime);
    }
}
