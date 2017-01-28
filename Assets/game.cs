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
	void Start () {
        Invoke("restart", time);

	}
	
	// Update is called once per frame
	void Update () {
        timeText.text = "Time: " +(time- Time.timeSinceLevelLoad);
    }
}
