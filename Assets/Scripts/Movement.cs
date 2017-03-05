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
    public Player Player;

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

        bool right = false;
        bool up = false;
        bool left = false;
        bool down = false;

        if(_lastDir.normalized == Vector3.right)
        {
            right = true;
        }
        else if(_lastDir.normalized == Vector3.left)
        {
            left = true;
        }

        if(_lastDir.normalized == Vector3.up)
        {
            up = true;
        }
        else if (_lastDir.normalized == Vector3.down)
        {
            down = true;
        }

        if ((h != 0) || (v != 0))
        {
            _targetPosition += inputMove * Time.deltaTime * Speed;
            _lastDir += _targetPosition - transform.position;
        }

        _lastDir = Vector3.ClampMagnitude(_lastDir, 0.05f);

        if ((_lastDir.normalized == Vector3.right && left == true) || (_lastDir.normalized == Vector3.left && right == true))
        {
            Player.DeactivateTrail();
            Player.Invoke("ActivateTrail",1);
        }

        if ((_lastDir.normalized == Vector3.up && down == true) || (_lastDir.normalized == Vector3.down && up == true))
        {
            Player.DeactivateTrail();
            Player.Invoke("ActivateTrail", 1);
        }

        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition + _lastDir, ref _velocity, SmoothTime);
#endif
    }

    public void Slow(float slowTime)
    {
        Speed /= slowfactor;
        Invoke("Fast", slowTime);

    }
    public void Fast()
    {
        Speed *= slowfactor;
    }
}
