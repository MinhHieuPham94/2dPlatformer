using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public GameObject player;
    public float minX, maxX, minY, maxY;
    public Vector3 distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position - distance;

        if(transform.position.x <= minX)
        {
            Vector3 cameraLimit = transform.position;
            cameraLimit.x = minX;
            transform.position = cameraLimit;
        } else if(transform.position.x >maxX)
        {
            Vector3 cameraLimit = transform.position;
            cameraLimit.x = maxX;
            transform.position = cameraLimit;
        }

        if(transform.position.y <= minY)
        {
            Vector3 cameraLimit = transform.position;
            cameraLimit.y = minY;
            transform.position = cameraLimit;
        } else if(transform.position.y >maxY)
        {
            Vector3 cameraLimit = transform.position;
            cameraLimit.y = maxY;
            transform.position = cameraLimit;
        }        

        
    }
}
