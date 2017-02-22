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
    public float Level1PixelSizeX;
    public float Level2PixelSizeX;
    public float Level3PixelSizeX;
    public float Level4PixelSizeX;
    public int Level1NumStates;
    public int Level2NumStates;
	public int Level3NumStates;
	//public GameObject[] PlayerSprites;

    private int _sizeTriggerCounter;
    private GameManager _gm;
    private int _subSizeCount;

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
        _subSizeCount = 0;

		Vector3 scale = transform.GetChild(0).localScale;
		float multiplyFactor = Level1PixelSizeX / Level2PixelSizeX;
		float newScale = multiplyFactor * scale.x;
		Vector3 newSize = new Vector3(newScale, newScale, scale.z);
		transform.GetChild(0).localScale = newSize;

		scale = transform.GetChild(1).localScale;
		multiplyFactor = Level2PixelSizeX / Level3PixelSizeX;
		newScale = multiplyFactor * scale.x;
		newSize = new Vector3(newScale, newScale, scale.z);
		transform.GetChild(1).localScale = newSize;

		scale = transform.GetChild(2).localScale;
		multiplyFactor = Level3PixelSizeX / Level4PixelSizeX;
		newScale = multiplyFactor * scale.x;
		newSize = new Vector3(newScale, newScale, scale.z);
		transform.GetChild(2).localScale = newSize;
    }

	// Update is called once per frame
	void Update ()
    {
	    
	}

	/// <summary>
	/// Changes the level
	/// </summary>
	/// <param name="level">Level number</param>
	public void ChangeLevel(int level)
	{
		//GameObject _image = PlayerSprites[level];
		for(int i = 0; i < transform.childCount; i++)
		{
			transform.GetChild(i).gameObject.SetActive(false);
		}
		transform.GetChild(level).gameObject.SetActive(true);
		//GameObject obj = Instantiate(_image);
		//obj.transform.parent = transform;
		//obj.transform.localRotation = Quaternion.identity;
		//obj.transform.localPosition = Vector3.zero;
		//obj.transform.localScale = Vector3.one;

		if (level != 0)
		{
			Camera.main.GetComponent<CameraFeats>().ZoomOut();
		}
	}

    /// <summary>
    /// Increases the player's size
    /// </summary>
    public void Grow(string tag)
    {
        float xScale = gameObject.transform.localScale.x;
        float multiplyFactor;
        float newScale;
        growfactor = 0;
        
        if (_gm.Level == 1)
        {
            multiplyFactor = Level2PixelSizeX / Level1PixelSizeX;
            newScale = multiplyFactor * xScale;
            growfactor = (newScale-xScale)/Level1NumStates;
        }
        else if(_gm.Level == 2)
        {
            multiplyFactor = Level3PixelSizeX / Level2PixelSizeX;
            newScale = multiplyFactor * xScale;
            growfactor = (newScale - xScale) / Level2NumStates;
            if (tag == "Collectibles1")
            {
                _subSizeCount++;
                if(_subSizeCount < 10)
                {
                    growfactor = 0;
                }
                else
                {
                    _subSizeCount = 0;
                }
            }
        }
        else if(_gm.Level == 3)
        {
            multiplyFactor = Level4PixelSizeX / Level3PixelSizeX;
            newScale = multiplyFactor * xScale;
            growfactor = (newScale - xScale) / Level3NumStates;
            if (tag == "Collectibles2")
            {
                _subSizeCount++;
                if (_subSizeCount < 3)
                {
                    growfactor = 0;
                }
                else
                {
                    _subSizeCount = 0;
                }
            }
        }

        if(growfactor>0)
        {
            Vector3 newSize = new Vector3(transform.localScale.x + growfactor, transform.localScale.y + growfactor, transform.localScale.z);
            transform.localScale = newSize;
            Size++;

            if (_sizeTriggerCounter < SizeTriggersNews.Length)
            {
                if (Size == SizeTriggersNews[_sizeTriggerCounter])
                {
                    _sizeTriggerCounter++;
                    News.TriggerTextNews();
                }
            }

            _gm.CheckLevelChange(Size);
        }
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
