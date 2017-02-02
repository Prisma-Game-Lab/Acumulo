//#define INPUT_MOUSE
#define INPUT_TECLADO_CONTROLE
using System.Reflection;
using UnityEngine;

public class Movement : MonoBehaviour
{
    #region variables

    public float Speed;
    public float SmoothTime;
    public float bacteriaslowtime;
    public float slowfactor;
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

        _lastDir = Vector3.ClampMagnitude(_lastDir, 0.2f);

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition + _lastDir, ref _velocity, SmoothTime);
#endif
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
