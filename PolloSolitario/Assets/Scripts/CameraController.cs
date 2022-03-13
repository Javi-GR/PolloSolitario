using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private Transform target;
    private Vector3 cameraTarget;
    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        cameraTarget = new Vector3(target.position.x, transform.position.y, target.position.z);
        //transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 8);
        transform.position = Vector3.Lerp(transform.position, target.position, Time.deltaTime * 15);
    }
}
