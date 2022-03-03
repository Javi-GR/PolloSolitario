using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    
    void OnTriggerEnter(Collider other){
        Debug.Log("Has superado el tutorial");
        if(other.gameObject.name == ("Player"))
        {
             SceneManager.LoadScene(1);
        }
       
    }
}
