using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    ChickenController chicken;

    void Start()
    {
        chicken = GameObject.FindGameObjectWithTag("Player").GetComponent<ChickenController>();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            chicken.TakeDamage(5);
            if(gameObject!=null)
            {
                Destroy(gameObject);
            }
            
        }
    }
}
