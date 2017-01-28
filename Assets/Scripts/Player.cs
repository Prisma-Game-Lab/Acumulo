using UnityEngine;

public class Player : MonoBehaviour
{
    #region variables

    public GameObject obstacle;

    private float _score;

    #endregion

    // Use this for initialization
    void Start ()
	{
	    _score = 0;
	}

    /// <summary>
    /// Called when trash was collected
    /// </summary>
    /// <param name="score"> the trash's value </param>
    public void ScoredTrashTriggered(float score)
    {
        _score += score;
        if (_score >= 1500)
        {
            ChangeLevel("player2");
        }
        else if (_score >= 1000)
        {
            ChangeLevel("player2");
        }
        else if (_score >= 100)
        {
            ChangeLevel("player2");
        }
    }

    void ChangeLevel(string name)
    {
        //Sprite _image = Resources.Load<Sprite>("Sprites/" + name);
        //gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _image;
        //gameObject.GetComponent<CircleCollider2D>().radius = _image.bounds.extents.x / 2;

        Camera.main.GetComponent<CameraFeats>().ZoomOut();
        //obstacle.GetComponent<ReduceSize>().ChangeSize();
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
