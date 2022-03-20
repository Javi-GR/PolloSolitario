using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LastLevelTrigger : MonoBehaviour
{
    public Text finalText;
    public GameObject dialogue;

    StatCount statCount;

    // Update is called once per frame
    void Start()
    {
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            if(statCount.currentChoice == StatCount.FinalChoice.LIBRE)
            {
                Debug.Log("Triggering with object, LIBRE");
                finalText.text = "HAS CONSEGUIDO EL FINAL\nLIBRE";
                StartCoroutine(LoadLeaderBoard());
            }
            if(statCount.currentChoice == StatCount.FinalChoice.POBRE)
            {
                Debug.Log("Triggering with object, POBRE");
                finalText.text = "HAS CONSEGUIDO EL FINAL\nPOBRE";
                StartCoroutine(LoadLeaderBoard());
            }
            if(statCount.currentChoice == StatCount.FinalChoice.RICO)
            {
                Debug.Log("Triggering with object, RICO");
                finalText.text = "HAS CONSEGUIDO EL FINAL\nRICO";
                StartCoroutine(LoadLeaderBoard());
            }
        }
        Debug.Log("Triggering with object");
    }
    IEnumerator LoadLeaderBoard()
    {
        dialogue.SetActive(true);
        LoadLeaderBoard();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(5);
    }
}
