using UnityEngine;

public class ReduceSize : MonoBehaviour
{ 

	// NIKO CHANGE - Now reduces alpha and fade out maybe it's better to change the name of functions and script


    #region variables

    public float ChangeSpeed;

    private Vector3 _initialSize;
    private Vector3 _targetSize;
    private bool _changeSize;
    private float _sizeFactor;

    #endregion
    
    /// <summary>
    /// Config the size change
    /// </summary>
    public void ChangeSize()
    {
        _initialSize = gameObject.transform.localScale;
        _targetSize = new Vector3(_initialSize.x - 1, _initialSize.y - 1, _initialSize.z - 1);
        _sizeFactor = 0f;

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
            _sizeFactor += Time.deltaTime * ChangeSpeed;
			Color color = GetComponent<SpriteRenderer>().color;
			color.a = Mathf.Lerp(color.a, 0f, 0.07f);
			GetComponent<SpriteRenderer>().color = color;

			//gameObject.transform.localScale = Vector3.Lerp(_initialSize, _targetSize, _sizeFactor);

            if (GetComponent<SpriteRenderer>().color.a == 0)
            {
                _changeSize = false;
                Destroy(gameObject);
            }
        }
    }
}
