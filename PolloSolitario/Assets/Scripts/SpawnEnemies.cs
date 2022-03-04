using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{
    public GameObject chaserEnemies;
    public Transform player;
    private bool hasSpawned = false;

    void Spawn()
    {
        if(checkIfSpawned()!= true){
            Invoke("enemySpawn", 0f);
        }
            
    }

    // Start is called before the first frame update
    void enemySpawn()
    {
            GameObject chaserEnemy =Instantiate(chaserEnemies, gameObject.transform.position, Quaternion.identity);
            
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position, gameObject.transform.position)<3f){
            Spawn();
            hasSpawned = true;
        }
    }
    private bool checkIfSpawned(){
        if(hasSpawned==true){
            CancelInvoke("enemySpawn");
        }
        return hasSpawned;
    }
   
}
