using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    //The canvas that displays after triggering the collider 
    public GameObject canvas;
    //Check if the canvas is active or not
    private bool canvasIsActive;
    //On start sets the canvas to false just in case
    void Start()
    {
        canvas.SetActive(false);
        canvasIsActive = false;
    }
    //If the canvas is displayed and the player clicks a button the canvas dissapears
    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && canvasIsActive == true){
            canvas.SetActive(false);
        }
    }
    //When colliding with the gameobject the canvas displays and starts the TimeToRead() coroutine
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            StartCoroutine(TimeToRead());
        }
    }
    // Here is where we let time for the user to read what's displayed
    IEnumerator TimeToRead(){
        
        
        canvas.SetActive(true);
        canvasIsActive = true;

        yield return new WaitForSeconds(5);
        
        Destroy(gameObject);
        canvas.SetActive(false);
        canvasIsActive = false;
        
    }
}
