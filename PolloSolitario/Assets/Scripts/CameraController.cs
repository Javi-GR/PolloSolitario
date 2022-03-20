using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Transform of the player
    private Transform target;
    //Vector3 that stores the target transform
    private Vector3 cameraTarget;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Follows the player aroound the map
    void Update()
    {
        cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 12);
    }
}
