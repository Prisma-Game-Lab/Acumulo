using UnityEngine;

public class CameraFeats : MonoBehaviour
{
    #region variables

    public float ZoomSpeed;

    private float _initialSize;
    private float _targetSize;
    private bool _changeSize;
    private float _zoomFactor;

    #endregion


    /// <summary>
    /// Performs a zoom out
    /// </summary>
    public void ZoomOut()
    {
        _initialSize = gameObject.GetComponent<Camera>().orthographicSize;
        _targetSize = _initialSize + 10;
        _zoomFactor = 0f;

        _changeSize = true;
    }


// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update ()
    {
        if(_changeSize)
        {
            _zoomFactor += Time.deltaTime * ZoomSpeed;
            Camera.main.orthographicSize = Mathf.MoveTowards(_initialSize, _targetSize, _zoomFactor);

            if(_zoomFactor == 1)
            {
                _changeSize = false;
            }
        }
    }
}
