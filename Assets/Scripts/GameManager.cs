using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public float time;
    private Text _timeText;

    private Text _scoreText;
    private float _score;
    private GameObject _player;
    private AudioSource _audio;
    public Sprite[] _Playerimages;
    static private GameObject _pauseCanvas;
    static private float _volume;

    // Singleton
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
        _audio = GetComponent<AudioSource>();
        _volume = _audio.volume;
    }

	void End()
    {
        SceneManager.LoadScene("EndingPlanetSaved");
    }

	// Update is called once per frame
	void Update ()
    {
	    if(Input.GetButtonDown("Pause"))
        {
            Pause();
        }
        if(_timeText)
        {
            _timeText.text = "Time: " + (int)(time - Time.timeSinceLevelLoad);
        }
        if (((int)(time- Time.timeSinceLevelLoad) == 0) && (SceneManager.GetActiveScene().name == "DevScene"))
        {
            End();
        }
        _audio.volume = _volume;
    }

    /// <summary>
    /// Pause/Unpause game
    /// </summary>
    public void Pause()
    {
        if (!_pauseCanvas)
        {
            _pauseCanvas = Instantiate(Resources.Load<GameObject>("Prefabs/PauseCanvas"));
            _pauseCanvas.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
            Time.timeScale = (_pauseCanvas.activeSelf) ? 0: 1;
        }
    }

    /// <summary>
    /// Changes the game's volume
    /// </summary>
    /// <param name="value">new volume</param>
    public void ChangeVolume(float value)
    {
        _volume = value;
    }

    /// <summary>
    /// Scene load callback
    /// </summary>
    void OnLevelWasLoaded()
    {
        GameObject ui = GameObject.FindGameObjectWithTag("UI");
        if (ui)
        {
            _scoreText = ui.transform.GetChild(0).GetComponent<Text>();
            _timeText  = ui.transform.GetChild(1).GetComponent<Text>();

        }
        Time.timeScale = 1;

        if(!_player)
        {
            _player = GameObject.FindGameObjectWithTag("Player");
        }

        if(_player)
        {
            ChangeLevel(0);
        }
    }

    /// <summary>
    /// Called when trash was collected
    /// </summary>
    /// <param name="score"> the trash's value </param>
    public void ScoredTrashTriggered(float score)
    {
        _score += score;
        _scoreText.text = "Score: " + _score;

        if (_score == 500)
        {
            SceneManager.LoadScene("EndingDestruction");
        }
        else if (_score == 300)
        {
            ChangeLevel(3);
        }
        else if (_score == 200)
        {
            ChangeLevel(2);
        }
        else if (_score == 100)
        {
            ChangeLevel(1);
        }
    }

    /// <summary>
    /// Reduces player's score
    /// </summary>
    /// <param name="score">amount to reduce</param>
    public void ReduceScore(float score)
    {
        _score -= score;
        _scoreText.text = "Score: " + _score;
    }

    /// <summary>
    /// Changes the level
    /// </summary>
    /// <param name="_level">Level number</param>
    void ChangeLevel(int _level)
    {
        Sprite _image = _Playerimages[_level];
        _player.GetComponentInChildren<SpriteRenderer>().sprite = _image;
        _player.GetComponent<CircleCollider2D>().radius = _image.bounds.extents.x / 2;

        if(_level != 0)
        {
            Camera.main.GetComponent<CameraFeats>().ZoomOut();
        }
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
