using UnityEngine;

public class FloatLogo : MonoBehaviour
{
    public float movefactor;
    public float moveDistance;

    private bool _moveUp;
    public Vector3 _targetPositionUp;
    public Vector3 _targetPositionDown;
	
    // Use this for initialization
	void Start ()
	{
	    _moveUp = true;
        _targetPositionUp = new Vector3(transform.position.x, transform.position.y + moveDistance, transform.position.z);
        _targetPositionDown = new Vector3(transform.position.x, transform.position.y - moveDistance, transform.position.z);
	}
	
	// Update is called once per frame
	void Update () 
    {
	    if (_moveUp)
	    {
	        transform.Translate(Vector3.up * Time.deltaTime * movefactor);
	        if (Mathf.Abs(transform.position.y - _targetPositionUp.y) < 0.1)
	        {
	            _moveUp = false;
	        }
	    }
	    else
	    {
            transform.Translate(Vector3.down * Time.deltaTime * movefactor);
            if (Mathf.Abs(transform.position.y - _targetPositionDown.y) < 0.1)
            {
                _moveUp = true;
            }
	    }
	}
}
