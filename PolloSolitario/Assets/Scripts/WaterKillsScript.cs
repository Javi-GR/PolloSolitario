using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterKillsScript : MonoBehaviour
{
    ChickenController chicken; 
    void Start(){
        ChickenController chicken = GameObject.FindGameObjectWithTag("Player").GetComponent<ChickenController>();
    }
   void OnCollisionEnter(Collider collider){
       if(collider.gameObject.tag == "Player"){
           Debug.Log("Colliding with the character");
            chicken.ResetGame();
       }
   }
}
