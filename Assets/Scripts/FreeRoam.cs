using UnityEngine;
using System.Collections;

public class FreeRoam : MonoBehaviour
{
    public float maxX = 10f;
    public float minX = -10f;
    public float maxY = 10;
    public float minY = -10f;
    public float moveSpeed = 0.4f;

    private float tChange = 0;
    private float randomX;
    private float randomY;


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
            randomX = -randomX;
        }
        if (gameObject.transform.position.y >= maxY || gameObject.transform.position.y <= minY)
        {
            randomY = -randomY;
        }
    }
}
