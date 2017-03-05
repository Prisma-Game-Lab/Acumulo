using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    #region variables

    public float bioshrinktime = 1;
    public int Size = 1;
    public int[] SizeTriggersNews;
    public int[] SizeTriggersLevel;
    public int[] SizeTriggersAudio;
    public Noticia News;
    public float Level1PixelSizeX;
    public float Level2PixelSizeX;
    public float Level3PixelSizeX;
    public float Level4PixelSizeX;
    public int Level1NumStates;
    public int Level2NumStates;
	public int Level3NumStates;
    public spawner Spawner;
    public TrailRenderer Trail1;
    public TrailRenderer Trail2;
    public TrailRenderer Trail3;
    //public GameObject[] PlayerSprites;

    private int _sizeTriggerCounter;
    [HideInInspector]
    public GameManager _gm;
    private int _subSizeCount;
    private float _growfactor1;
    private float _growfactor2;
    private float _growfactor3;

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

        ComputeGrowFactors();
    }

	// Update is called once per frame
	void Update ()
    {
	    
	}

    void ComputeGrowFactors()
    {
        float xScale;
        float multiplyFactor;
        float newScale;
        _growfactor1 = 0;
        _growfactor2 = 0;
        _growfactor3 = 0;

        xScale = transform.GetChild(0).localScale.x;
        multiplyFactor = Level2PixelSizeX / Level1PixelSizeX;
        newScale = multiplyFactor * xScale;
        _growfactor1 = (newScale - xScale) / Level1NumStates;

        xScale = transform.GetChild(1).localScale.x;
        multiplyFactor = Level3PixelSizeX / Level2PixelSizeX;
        newScale = multiplyFactor * xScale;
        _growfactor2 = (newScale - xScale) / Level2NumStates;

        xScale = transform.GetChild(2).localScale.x;
        multiplyFactor = Level4PixelSizeX / Level3PixelSizeX;
        newScale = multiplyFactor * xScale;
        _growfactor3 = (newScale - xScale) / Level3NumStates;
    }

	/// <summary>
	/// Changes the level
	/// </summary>
	/// <param name="level">Level number</param>
	public void ChangeLevel(int level)
	{
        if (level > 0 && level < 3)
        {
            Animator anim = transform.GetChild(level - 1).gameObject.GetComponent<Animator>();
            anim.SetBool("IsEvol", true);
            _gm.PlayerEvolving = true;
            Camera.main.GetComponent<CameraFeats>().ZoomOut(true);
            if(level == 1)
            {
                GetComponent<Movement>().Speed += 4;
            }
            else if(level == 2)
            {
                GetComponent<Movement>().Speed += 8;
            }

            Spawner.RadiusFar += 1.5f;
            _subSizeCount = 0;
        }
	}

    /// <summary>
    /// Increases the player's size
    /// </summary>
    public void Grow(string tag)
    {
        Vector3 newSize;
        Transform child;
        bool changed = false;
        bool grow = true;

        if (_gm.Level == 1)
        {
            child = transform.GetChild(0);
            newSize = new Vector3(child.localScale.x + _growfactor1, child.localScale.y + _growfactor1, child.localScale.z);
            child.localScale = newSize;
            Size++;
            changed = true;
        }
        else if (_gm.Level == 2)
        {
            child = transform.GetChild(1);
            if (tag == "Collectable1")
            {
                _subSizeCount++;
                if (_subSizeCount < 10)
                {
                    grow = false;
                }
                else
                {
                    _subSizeCount = 0;
                }
            }
            if (grow)
            {
                newSize = new Vector3(child.localScale.x + _growfactor2, child.localScale.y + _growfactor2, child.localScale.z);
                child.localScale = newSize;
                Size++;
                changed = true;
            }
        }
        else if (_gm.Level == 3)
        {
            child = transform.GetChild(2);
            if (tag == "Collectable2")
            {
                _subSizeCount++;
                if (_subSizeCount < 3)
                {
                    grow = false;
                }
                else
                {
                    _subSizeCount = 0;
                }
            }
            if (grow)
            {
                newSize = new Vector3(child.localScale.x + _growfactor3, child.localScale.y + _growfactor3, child.localScale.z);
                child.localScale = newSize;
                Size++;
                changed = true;
            }
        }

        if (changed)
        {
            if (_sizeTriggerCounter < SizeTriggersNews.Length)
            {
                if (Size == SizeTriggersNews[_sizeTriggerCounter])
                {
                    _sizeTriggerCounter++;
                    News.TriggerTextNews();
                }
            }

            Spawner.RadiusFar += 0.04f;
            if(_gm.Level == 1)
            {
                Trail1.time += 0.04f;
            }
            else if(_gm.Level == 2)
            {
                Trail2.time += 0.04f;
            }
            else if (_gm.Level == 3)
            {
                Trail3.time += 0.04f;
            }

            Camera.main.GetComponent<CameraFeats>().ZoomOut(false);
            _gm.CheckLevelChange(Size);
        }
    }

    public void OnCollisionStay2D(Collision2D collision)
    {
        DeactivateTrail();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        ActivateTrail();
    }

    /// <summary>
    /// Deactivates level's respective trail
    /// </summary>
    public void DeactivateTrail()
    {
        if (_gm.Level == 1)
        {
            Trail1.enabled = false;
        }
        else if (_gm.Level == 2)
        {
            Trail2.enabled = false;
        }
        else if (_gm.Level == 3)
        {
            Trail3.enabled = false;
        }
    }

    /// <summary>
    /// Activates level's respective trail
    /// </summary>
    public void ActivateTrail()
    {
        if (_gm.Level == 1)
        {
            Trail1.enabled = true;
        }
        else if (_gm.Level == 2)
        {
            Trail2.enabled = true;
        }
        else if (_gm.Level == 3)
        {
            Trail3.enabled = true;
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
            if(Size == SizeTriggersLevel[i] || Size <= 0)
            {
                shrink = false;
                break;
            }
        }

        if(shrink)
        {
            Transform child;
            Vector3 newSize;
            if (_gm.Level == 1)
            {
                child = transform.GetChild(0);
                newSize = new Vector3(child.localScale.x - _growfactor1, child.localScale.y - _growfactor1, child.localScale.z);
                child.localScale = newSize;
            }
            else if (_gm.Level == 2)
            {
                child = transform.GetChild(1);
                newSize = new Vector3(child.localScale.x - _growfactor2, child.localScale.y - _growfactor2, child.localScale.z);
                child.localScale = newSize;
            }
            else if (_gm.Level == 3)
            {
                child = transform.GetChild(2);
                newSize = new Vector3(child.localScale.x - _growfactor3, child.localScale.y - _growfactor3, child.localScale.z);
                child.localScale = newSize;
            }
            Size--;

            Camera.main.GetComponent<CameraFeats>().ZoomIn();
        }
    }
    public void Bio()
    {
        Invoke("Shrink", bioshrinktime);
    }
}
