using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickGun : MonoBehaviour
{
    GameObject chicken;
    ChickenController chickenController;
    public bool gunPicked;
    // Start is called before the first frame update
    void Start()
    {
        chicken = GameObject.FindGameObjectWithTag("Player");
        chickenController = chicken.GetComponent<ChickenController>();
    }

    
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && gunPicked == false)
        {
            gunPicked = true;
            transform.rotation = chickenController.gun.transform.rotation;
            transform.position = chickenController.gun.transform.position;
            chickenController.gun.DropGun();
            gameObject.transform.parent = chicken.transform;
            chickenController.gun = gameObject.GetComponent<Gun>();
        }
    }
}
