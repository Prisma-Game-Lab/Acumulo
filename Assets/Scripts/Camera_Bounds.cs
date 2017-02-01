using UnityEngine;
using System.Collections;

public class Camera_Bounds : MonoBehaviour {

	//mapX, mapY is size of background image
         private float mapX = 63.7f; 
         private float mapY = 62.6f;
         private float minX;
         private float maxX;
         private float minY;
         private float maxY;
     
         void Update()
            {
            float vertExtent = GetComponent<Camera>().orthographicSize;  
            float  horzExtent = vertExtent * Screen.width / Screen.height;
             
         // Calculations assume map is position at the origin
             minX = horzExtent - mapX / 2.0f;
             maxX = mapX / 2.0f - horzExtent ;
             minY = vertExtent - mapY / 2.0f;
             maxY = mapY / 2.0f - vertExtent;
         }
     
         void LateUpdate() 
         {
             Vector3 v3 = transform.position;
             v3.x = Mathf.Clamp(v3.x, minX, maxX);
             v3.y = Mathf.Clamp(v3.y, minY, maxY);
             transform.position = v3;
         }
}
