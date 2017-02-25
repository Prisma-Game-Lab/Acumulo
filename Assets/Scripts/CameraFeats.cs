using UnityEngine;

public class CameraFeats : MonoBehaviour
{
    #region variables

    public float ZoomSpeed;
    public float LevelZoom;
    public float TrashZoom;


    private float _initialSize;
    private float _targetSize;
    private bool _changeSize;
    private float _zoomFactor;

    #endregion

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

    /// <summary>
    /// Performs a zoom out
    /// </summary>
    /// <param name="changeLevel">true if level change</param>
    public void ZoomOut(bool changeLevel)
    {
        //if (!_changeSize || changeLevel)
        //{
        //    _initialSize = gameObject.GetComponent<Camera>().orthographicSize;
        //    _targetSize = _initialSize + ((changeLevel) ? LevelZoom : TrashZoom);
        //    _zoomFactor = 0f;

        //    _changeSize = true;
        //}

        _initialSize = gameObject.GetComponent<Camera>().orthographicSize;

        if (_changeSize)
        {
            _targetSize += ((changeLevel) ? LevelZoom : TrashZoom);
        }
        else
        {
            _targetSize = _initialSize + ((changeLevel) ? LevelZoom : TrashZoom);
        }
        _zoomFactor = 0f;

        _changeSize = true;
    }

    /// <summary>
    /// Performs a zoom in
    /// </summary>
    public void ZoomIn()
    {
        _initialSize = gameObject.GetComponent<Camera>().orthographicSize;

        if (_changeSize)
        {
            _targetSize -= TrashZoom;
        }
        else
        {
            _targetSize = _initialSize - TrashZoom;
        }
        _zoomFactor = 0f;

        _changeSize = true;
    }
}
