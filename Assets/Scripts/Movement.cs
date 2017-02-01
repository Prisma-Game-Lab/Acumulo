//#define INPUT_MOUSE
#define INPUT_TECLADO_CONTROLE
using System.Reflection;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region variables

    public float Speed;
    public float SmoothTime;
    public float bioshrinktime;
    public float bacteriaslowtime;
    public float slowfactor;
    public float growfactor;
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
    void Update()
    {
#if INPUT_MOUSE
        Vector3 target;
        target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target.z = transform.position.z;
	    transform.position = Vector3.MoveTowards(transform.position, target, Speed * Time.deltaTime);
#elif INPUT_TECLADO_CONTROLE

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 inputMove = new Vector3(h, v, 0);

        _targetPosition = transform.position;

        if ((h != 0) || (v != 0))
        {
            _targetPosition += inputMove * Time.deltaTime * Speed;
            _lastDir += _targetPosition - transform.position;
        }

        _lastDir = Vector3.ClampMagnitude(_lastDir, 0.5f);

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition + _lastDir, ref _velocity, SmoothTime);
#endif
    }

    /// <summary>
    /// Increases the player's size
    /// </summary>
    public void Grow()
    {
        Vector3 newSize = new Vector3(transform.localScale.x * growfactor, transform.localScale.y * growfactor, transform.localScale.z);
        transform.localScale = newSize;
    }
    /// <summary>
    /// Reduce the player's size
    /// </summary>
    public void Shrink()
    {
        Vector3 newSize = new Vector3(transform.localScale.x / growfactor, transform.localScale.y / growfactor, transform.localScale.z);
        transform.localScale = newSize;
    }
    public void Bio()
    {
        Invoke("Shrink", bioshrinktime);
    }
    public void Slow()
    {
        Speed /= slowfactor;
        Invoke("Fast", bacteriaslowtime);

    }
    public void Fast()
    {
        Speed *= slowfactor;
    }
}
