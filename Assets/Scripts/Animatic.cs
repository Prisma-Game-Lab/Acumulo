using UnityEngine;
using UnityEngine.SceneManagement;

public class Animatic : MonoBehaviour
{
    private MovieTexture _animatic;

	// Use this for initialization
	void Start ()
    {
        Renderer r = GetComponent<Renderer>();
        _animatic = (MovieTexture)r.material.mainTexture;
        _animatic.Play();
    }

    private void Update()
    {
        if(Input.anyKeyDown)
        {
            SceneManager.LoadScene("DevScene");
        }
    }
}
