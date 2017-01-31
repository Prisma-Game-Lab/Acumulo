using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public float ImageAlphaFactor;

    private Image _logo;
    private bool _fadeIn;

	// Use this for initialization
	void Start ()
    {
        _logo = transform.GetChild(1).GetComponent<Image>();
        _fadeIn = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(_fadeIn)
        {
            Color color = _logo.color;
            color.a += ImageAlphaFactor * Time.deltaTime;
            _logo.color = color;
            if(color.a >= 1.5)
            {
                _fadeIn = false;
            }
        }
        else
        {
            Color color = _logo.color;
            color.a -= ImageAlphaFactor * Time.deltaTime;
            _logo.color = color;
            if (color.a <= 0)
            {
                SceneManager.LoadScene("DevScene");
            }
        }
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("DevScene");
        }
	}
}
