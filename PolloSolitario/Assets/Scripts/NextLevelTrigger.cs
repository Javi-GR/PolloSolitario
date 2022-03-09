using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    public Animator transition;
    void OnTriggerEnter(Collider other){
        Debug.Log("Has superado el tutorial");
        if(other.gameObject.name == ("Player"))
        {
             LoadNextLevel();
        }
       
    }
    public void LoadNextLevel()
    {
       StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex+1));
    }
    IEnumerator LoadLevel(int levelIndx)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndx);
    }
}
