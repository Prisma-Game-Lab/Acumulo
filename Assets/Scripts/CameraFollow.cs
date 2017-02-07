using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

    public GameObject Player;
    public float smoothSpeed;
    public Vector3 specificVector;
    private PolygonCollider2D _mapEdges;

    // Use this for initialization
    void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        specificVector = new Vector3(Player.transform.position.x, Player.transform.position.y, transform.position.z);

        gameObject.transform.position = Vector3.Lerp(transform.position,specificVector,smoothSpeed*Time.deltaTime);
        _mapEdges = GameObject.FindGameObjectWithTag("Bounds").GetComponent<PolygonCollider2D>();

    }

    private Bounds GetCameraBounds()
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraH = Camera.main.orthographicSize * 2;
        return new Bounds(Camera.main.transform.position, new Vector3(cameraH * screenAspect, cameraH, 0));
    }

    void LateUpdate()
    {
        Vector3 cameraOutOfBounds = CheckCameraBounds(GetCameraBounds(), _mapEdges);
        //this vector contains the distance between the camera bounds and the map bounds, if the camera is outside
        //the map bounds. If it's not, it will be (0,0).

        transform.position += cameraOutOfBounds;
    }
    private Vector3 CheckCameraBounds(Bounds camera, PolygonCollider2D map)
    {
        Vector3 outOfBounds = new Vector3(0, 0, 0);

        if (camera.max.x > map.bounds.max.x)
        {
            outOfBounds.x = map.bounds.max.x - camera.max.x;
        }
        else if (camera.min.x < map.bounds.min.x)
        {
            outOfBounds.x = map.bounds.min.x - camera.min.x;
        }

        if (camera.max.y > map.bounds.max.y)
        {
            outOfBounds.y = map.bounds.max.y - camera.max.y;
        }

        else if (camera.min.y < map.bounds.min.y)
        {
            outOfBounds.y = map.bounds.min.y - camera.min.y;
        }

        return outOfBounds;
    }
}
