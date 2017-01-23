//#define INPUT_MOUSE
#define INPUT_TECLADO
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region variables

    public float Speed;
    public float SmoothTime;
    public float ExtraMovement;

    private Vector3 _velocity;
    private Vector3 _targetPosition;
    private Vector3 _lastDir;

    #endregion

    void Start()
    {
        //Speed = 5;
        //SmoothTime = 5;
        _velocity = Vector3.zero;
        _targetPosition = transform.position;
    }
	
	// Update is called once per frame
	void Update ()
	{
#if INPUT_MOUSE
        Vector3 target;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
	    transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
#elif INPUT_TECLADO
        if (Input.GetKey(KeyCode.W))
        {
            _targetPosition += Vector3.up * Time.deltaTime * Speed;//transform.Translate(Vector3.up * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.A))
        {
            _targetPosition += Vector3.left * Time.deltaTime * Speed;//transform.Translate(Vector3.left * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _targetPosition += Vector3.down * Time.deltaTime * Speed;//transform.Translate(Vector3.down * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _targetPosition += Vector3.right * Time.deltaTime * Speed;//transform.Translate(Vector3.right * Time.deltaTime * Speed);
        }
        //_lastDir *= ExtraMovement;
        _lastDir = _targetPosition;
        //transform.position = Vector3.Lerp(transform.position, _targetPosition, Speed * Time.deltaTime);
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocity, SmoothTime);
        //transform.Translate(_lastDir * Time.deltaTime * Speed);
#endif
    }

    /// <summary>
    /// Increases the player's size
    /// </summary>
    public void Grow()
    {
        Vector3 newSize = new Vector3(transform.localScale.x+1,transform.localScale.y+1,transform.localScale.z);
        transform.localScale = newSize;
    }
}
