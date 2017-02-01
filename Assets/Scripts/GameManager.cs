using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

    private Text _scoreText;
    private float _score;

    static public GameManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        _score = 0;
    }
	
	// Update is called once per frame
	void Update ()
    {
	
	}

    void OnLevelWasLoaded()
    {
        _scoreText = GameObject.FindGameObjectWithTag("UI").transform.GetChild(0).GetComponent<Text>();
    }

    /// <summary>
    /// Called when trash was collected
    /// </summary>
    /// <param name="score"> the trash's value </param>
    public void ScoredTrashTriggered(float score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;
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

    /// <summary>
    /// Changes the level 
    /// </summary>
    /// <param name="name">Sprite name</param>
    void ChangeLevel(string name)
    {
        //Sprite _image = Resources.Load<Sprite>("Sprites/" + name);
        //gameObject.GetComponentInChildren<SpriteRenderer>().sprite = _image;
        //gameObject.GetComponent<CircleCollider2D>().radius = _image.bounds.extents.x / 2;

        Camera.main.GetComponent<CameraFeats>().ZoomOut();
        //obstacle.GetComponent<ReduceSize>().ChangeSize();
    }

    /// <summary>
    /// Changes the scene
    /// </summary>
    /// <param name="name">Scene name</param>
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    /// <summary>
    /// Quits the application
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    /// <summary>
    /// Resets the scene
    /// </summary>
    public void Reset()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
