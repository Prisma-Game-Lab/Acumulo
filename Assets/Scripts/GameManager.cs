using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;

public class GameManager : MonoBehaviour {

    public float time;
    public AudioSource[] AudioSources;
    //[HideInInspector]
    public bool[] ActiveAudios;

    private Text _scoreText;
    private Text _timeText;
    private float _score;
    private GameObject _playerObj;
    private Player _player;
    private int _level;
	private GameObject _pause;
    static private GameObject _pauseCanvas;
    static private float _volume;
    private GameObject _spawner;
    private bool _playerEvolving;

    public bool PlayerEvolving
    {
        get
        {
            return _playerEvolving;
        }
        set
        {
            _playerEvolving = value;
        }
    }

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

        _volume = AudioSources[0].volume;
    }

    // Use this for initialization
    void Start ()
    {
		_pause = Resources.Load<GameObject> ("Prefabs/PauseCanvas");
		_score = 0;
        //_volume = AudioSources[0].volume;
        _level = 1;
        _playerEvolving = false;

        for (int i = 1; i < ActiveAudios.Length; i++)
        {
            ActiveAudios[i] = false;
        }

        // PARA PODER RODAR DE QUALQUER CENA ////////////////////////////////
        GameObject ui = GameObject.FindGameObjectWithTag("UI");            //
        if (ui)                                                            //
        {                                                                  //
            _scoreText = ui.transform.GetChild(0).GetComponent<Text>();    //
            _timeText = ui.transform.GetChild(1).GetComponent<Text>();     //
                                                                           //
        }                                                                  //
        Time.timeScale = 1;                                                //  
                                                                           //
        if (!_playerObj)                                                   //  
        {                                                                  //
            _playerObj = GameObject.FindGameObjectWithTag("Player");       //
        }                                                                  //
                                                                           //
        if (_playerObj)                                                    //
        {                                                                  //
            _player = _playerObj.GetComponent<Player>();                   //
            _player.ChangeLevel(0);                                        //
        }                                                                  //
                                                                           //
        if (!_spawner)                                                     //
        {                                                                  //
            _spawner = GameObject.Find("spawners");                        //
        }                                                                  //
                                                                           //
        for (int i = 1; i < ActiveAudios.Length; i++)                      //
        {                                                                  //
            ActiveAudios[i] = false;                                       //
            AudioSources[i].volume = 0;                                    //
        }                                                                  //
        /////////////////////////////////////////////////////////////////////
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
            _timeText.text = "Tempo restante: " + (int)(time - Time.timeSinceLevelLoad);
        }
        if (((int)(time- Time.timeSinceLevelLoad) == 0) && (SceneManager.GetActiveScene().name == "DevScene"))
        {
            End();
        }
        for (int i = 0; i < AudioSources.Length; i++)
        {
            if(ActiveAudios[i])
            {
                AudioSources[i].volume = _volume;
            }
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
    /// Returns the volume
    /// </summary>
    /// <returns>volume level</returns>
    public float GetVolume()
    {
        return _volume;
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

        if (!_spawner)
        {
            _spawner = GameObject.Find("spawners");
        }

        for (int i = 1; i < ActiveAudios.Length; i++)
        {
            ActiveAudios[i] = false;
            AudioSources[i].volume = 0;
        }
    }

    /// <summary>
    /// Called when trash was collected
    /// </summary>
    /// <param name="score"> the trash's value </param>
    public void ScoredTrashTriggered(float score)
    {
        _score += score;
        _scoreText.text = "Pontuação: " + _score;
    }

    public void CheckLevelChange(int size)
    {
        if (size == _player.SizeTriggersLevel[2] && _level == 3) //end game
        {
            SceneManager.LoadScene("EndingDestruction");
        }
        else if (size == _player.SizeTriggersLevel[1] && _level == 2)//finished level 2
        {
            _spawner.transform.FindChild("spawner Barco").gameObject.SetActive(true);
            _spawner.transform.FindChild("spawner Carro").gameObject.SetActive(true);
            _spawner.transform.FindChild("spawner Plataforma").gameObject.SetActive(true);
            _spawner.transform.FindChild("spawner Sacola").gameObject.SetActive(false);
            _spawner.transform.FindChild("spawner Latinha").gameObject.SetActive(false);
            _spawner.transform.FindChild("spawner Garrafa").gameObject.SetActive(false);
            Destroy(GameObject.Find("Level1Trash"));
            GameObject[] trashs = GameObject.FindGameObjectsWithTag("Collectable1");
            foreach(GameObject trash in trashs)
            {
                Destroy(trash);
            }

            _player.ChangeLevel(2);
            _level++;
        }
        else if (size == _player.SizeTriggersLevel[0] && _level == 1)//finished level 1
        {
            _spawner.transform.FindChild("spawner Roda").gameObject.SetActive(true);
            _spawner.transform.FindChild("spawner Tortuga").gameObject.SetActive(true);
            _spawner.transform.FindChild("spawner Wash Machine").gameObject.SetActive(true);
            _player.ChangeLevel(1);
            _level++;
        }

        for(int i = 0; i < 8; i++)
        {
            if (size == _player.SizeTriggersAudio[i])
            {
                ActiveAudios[i+1] = true;
            }
        }
    } 

    /// <summary>
    /// Reduces player's score
    /// </summary>
    /// <param name="score">amount to reduce</param>
    public void ReduceScore(float score)
    {
        _score -= score;
        _scoreText.text = "Pontuação: " + _score;
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
