using UnityEngine;

public class ReduceSize : MonoBehaviour
{
    #region variables

    public float ZoomSpeed;

    private Vector3 _initialSize;
    private Vector3 _targetSize;
    private bool _changeSize;
    private float _zoomFactor;

    #endregion
    
    /// <summary>
    /// Performs a zoom out
    /// </summary>
    public void ChangeSize()
    {
        _initialSize = gameObject.transform.localScale;
        _targetSize = new Vector3(_initialSize.x - 1, _initialSize.y - 1, _initialSize.z - 1);
        _zoomFactor = 0f;

        _changeSize = true;
    }


    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (_changeSize)
        {
            _zoomFactor += Time.deltaTime * ZoomSpeed;
            gameObject.transform.localScale = Vector3.Lerp(_initialSize, _targetSize, _zoomFactor);

            if (_zoomFactor == 1)
            {
                _changeSize = false;
            }
        }
    }
}
