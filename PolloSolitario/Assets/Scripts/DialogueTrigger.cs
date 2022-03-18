using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueTrigger : MonoBehaviour
{
    
    public GameObject canvas;

    private bool canvasIsActive;

    void Start()
    {
        canvas.SetActive(false);
        canvasIsActive = false;
    }
    void Update(){
        if(Input.GetKeyDown(KeyCode.Mouse0) && canvasIsActive == true){
            canvas.SetActive(false);
        }
    }
    void OnTriggerEnter(Collider other){
        if(other.tag=="Player"){
            StartCoroutine(TimeToRead());
        }
    }

    IEnumerator TimeToRead(){
        
        
        canvas.SetActive(true);
        canvasIsActive = true;

        yield return new WaitForSeconds(4);
        
        Destroy(gameObject);
        canvas.SetActive(false);
        canvasIsActive = false;
        
    }
}
