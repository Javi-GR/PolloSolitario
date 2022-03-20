using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCollision : MonoBehaviour
{
    //Reference to the player controller
    ChickenController chicken;

    void Start()
    {
        chicken = GameObject.FindGameObjectWithTag("Player").GetComponent<ChickenController>();
    }
    //If  the bullet that has this script attached hits a player, this will deal 5 damage (Snake enemies)
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
