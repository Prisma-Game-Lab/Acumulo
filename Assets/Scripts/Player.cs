using UnityEngine;
using System.Collections;
using System.Security.Permissions;

public class Player : MonoBehaviour
{

    private float _score;





	// Use this for initialization
	void Start ()
	{

	    _score = 0;
	}

    public void ScoredTrashTriggered(float score)
    {
        _score += score;
        if (_score >= 100)
        {
            Sprite _image = Resources.Load<Sprite>("Sprites/player2");
            gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _image;
            gameObject.GetComponent<CircleCollider2D>().radius = _image.bounds.extents.x/2;

            Camera.main.GetComponent<CameraFeats>().ZoomOut();
        }
    }

	// Update is called once per frame
	void Update () {
	    
	}
}
