using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public float time;
    public AudioSource[] AudioSources;

    private Text _scoreText;
    private Text _timeText;
    private float _score;
    private GameObject _playerObj;
    private Player _player;
    private int _level;
	private GameObject _pause;
    static private GameObject _pauseCanvas;
    static private float _volume;

    public int Level
    {
        get
        {
            return _level;
        }
    }

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
		_pause = Resources.Load<GameObject> ("Prefabs/PauseCanvas");
		_score = 0;
        _volume = AudioSources[0].volume;
        AudioSources[0].Play();
        for(int i = 0; i < AudioSources.Length; i++)
        {
            AudioSources[i].volume = _volume;
        }
        _level = 1;
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
        for (int i = 0; i < AudioSources.Length; i++)
        {
            AudioSources[i].volume = _volume;
        }
    }

    /// <summary>
    /// Pause/Unpause game
    /// </summary>
    public void Pause()
    {
        if(SceneManager.GetActiveScene().name == "DevScene")
        {
            if (!_pauseCanvas)
            {
                _pauseCanvas = Instantiate(_pause);
                _pauseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
            else
            {
                _pauseCanvas.SetActive(!_pauseCanvas.activeSelf);
                Time.timeScale = (_pauseCanvas.activeSelf) ? 0 : 1;
            }
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

        if(!_playerObj)
        {
            _playerObj = GameObject.FindGameObjectWithTag("Player");
        }

        if(_playerObj)
        {
            _player = _playerObj.GetComponent<Player>();
			_player.ChangeLevel(0);
        }
        if (SceneManager.GetActiveScene().name == "DevScene")
        {
            AudioSources[1].Play();
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
    }

    public void CheckLevelChange(int size)
    {
        if (size == _player.SizeTriggersLevel[2] && _level == 3)
        {
            SceneManager.LoadScene("EndingDestruction");
        }
        else if (size == _player.SizeTriggersLevel[1] && _level == 2)
        {
			_player.ChangeLevel(2);
            _level++;
        }
        else if (size == _player.SizeTriggersLevel[0] && _level == 1)
        {
			_player.ChangeLevel(1);
            _level++;
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
        SceneManager.LoadScene("Menu");
    }
}
