using UnityEngine;
using System.Collections;

public class CameraFeats : MonoBehaviour
{

    public void ZoomOut()
    {
        float size = gameObject.GetComponent<Camera>().orthographicSize;
        float targetSize = size+10;
        float t = .0f;
        float zoomspeed = 1;

        while (t < 1)
        {
            t += Time.deltaTime / 5;
            print(t);
            gameObject.GetComponent<Camera>().orthographicSize = Mathf.MoveTowards(size, targetSize, t);
            
        }
    }


// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
