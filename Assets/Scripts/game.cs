using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;

public class game : MonoBehaviour {
    public float time;
    public Text timeText;
    // Use this for initialization
    void restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    void End()
    {
        Time.timeScale = 0f;
    }
	void Start () {
        Invoke("End", time);

	}
	
	// Update is called once per frame
	void Update () {
        timeText.text = "Time: " +(int)(time- Time.timeSinceLevelLoad);
    }
}
