using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDialogue : MonoBehaviour
{
    public GameObject canvas;

    public GameObject wall;
    StatCount statCount;

    FinalDecisionScript finalDecision;

    void Start()
    {
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
        finalDecision = GameObject.FindGameObjectWithTag("WorldUI").GetComponent<FinalDecisionScript>();
        canvas.SetActive(false);
    }

    public void ShowDialogue()
    {
        canvas.SetActive(true);
    }

    public void Yes()
    {
        statCount.completedGame = true;
        canvas.SetActive(false);
        Destroy(wall);
        Destroy(gameObject);
        finalDecision.CheckFinalState();
        Time.timeScale = 1f;
    }
    public void No()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }

   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            Debug.Log("Jugador quiere terminar?");
            ShowDialogue();
            Time.timeScale = 0f;
        }
    }
}
