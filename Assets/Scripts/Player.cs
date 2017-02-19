using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region variables

    public float bioshrinktime = 1;
    public float growfactor = 0.01f;
    public int Size = 1;
    public int[] SizeTriggersNews;
    public int[] SizeTriggersLevel;
    public Noticia News;

    private int _sizeTriggerCounter;
    private GameManager _gm;

    #endregion

    // Use this for initialization
    void Start ()
	{
        Size = 0;
        _sizeTriggerCounter = 0;
        GameObject gm = GameObject.FindGameObjectWithTag("GM");
        if (gm)
        {
            _gm = gm.GetComponent<GameManager>();
        }
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

        if(_sizeTriggerCounter < SizeTriggersNews.Length)
        {
            if (Size == SizeTriggersNews[_sizeTriggerCounter])
            {
                _sizeTriggerCounter++;
                News.TriggerTextNews();
            }
        }
        
        _gm.CheckLevelChange(Size);
    }
    /// <summary>
    /// Reduce the player's size
    /// </summary>
    public void Shrink()
    {
        bool shrink = true;

        for(int i = 0; i < SizeTriggersLevel.Length; i++)
        {
            if(Size == SizeTriggersLevel[i])
            {
                shrink = false;
                break;
            }
        }

        if(shrink)
        {
            Vector3 newSize = new Vector3(transform.localScale.x - growfactor, transform.localScale.y - growfactor, transform.localScale.z);
            transform.localScale = newSize;
            Size--;
        }
    }
    public void Bio()
    {
        Invoke("Shrink", bioshrinktime);
    }
}
