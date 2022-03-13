using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevelTrigger : MonoBehaviour
{
    StatCount statCount;
    public Animator transition;

    void Start()
    {
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();

    }
    void OnTriggerEnter(Collider other){
        
        if(other.gameObject.name == ("Player"))
        {
            Debug.Log("Has superado el nivel");
            DontDestroyOnLoad(GameObject.FindGameObjectWithTag("HUD"));
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
        statCount.ResetRounds();
        statCount.DisplayRound();
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(levelIndx);
    }
}
