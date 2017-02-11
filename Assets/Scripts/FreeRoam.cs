using UnityEngine;
using System.Collections;

public class FreeRoam : MonoBehaviour
{
    public float maxX = 13f;
    public float minX = -14f;
    public float maxY = 9f;
    public float minY = -9f;
    public float moveSpeed = 0.4f;

    private float tChange = 0;
    private float randomX;
    private float randomY;

    private bool _insideXBounds;
    private bool _insideYBounds;

    private void Start()
    {
        maxX = 13f;
        minX = -14f;
        maxY = 9f;
        minY = -9f;

        _insideXBounds = true;
        _insideYBounds = true;
    }


    void Update()
    {
        if (Time.time >= tChange)
        {
            randomX = Random.Range(-2.0f, 2.0f);
            randomY = Random.Range(-2.0f, 2.0f);
            tChange = Time.time + 5;
        }

        gameObject.transform.Translate(new Vector3(randomX,randomY,0)* moveSpeed * Time.deltaTime);

        if (gameObject.transform.position.x >= maxX || gameObject.transform.position.x <= minX)
        {
            if(_insideXBounds)
            {
                Debug.Log("X");
                _insideXBounds = false;
                randomX = -randomX;
            }
        }
        else
        {
            _insideXBounds = true;
        }

        if (gameObject.transform.position.y >= maxY || gameObject.transform.position.y <= minY)
        {
            if (_insideYBounds)
            {
                Debug.Log("Y");
                _insideYBounds = false;
                randomY = -randomY;
            }
        }
        else
        {
            _insideYBounds = true;
        }
    }
}
