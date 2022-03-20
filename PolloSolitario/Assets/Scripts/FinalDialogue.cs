using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDialogue : MonoBehaviour
{
    //Dialogue canvas 
    public GameObject canvas;
    //Wall blocking the final area
    public GameObject wall;
    //Reference to the statcount in hud
    StatCount statCount;
    //Referenece to the finalDecision so it can check the final state of the game when pressing yes on dialogue
    FinalDecisionScript finalDecision;

    void Start()
    {
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
        finalDecision = GameObject.FindGameObjectWithTag("WorldUI").GetComponent<FinalDecisionScript>();
        canvas.SetActive(false);
    }
    //Function that shows the canvas
    public void ShowDialogue()
    {
        canvas.SetActive(true);
    }
    //Function called while pressing the YES button, sets the game as completed and lets the player go through the wall
    public void Yes()
    {
        statCount.completedGame = true;
        canvas.SetActive(false);
        Destroy(wall);
        Destroy(gameObject);
        finalDecision.CheckFinalState();
        Time.timeScale = 1f;
    }
    //Hides the canvas and starts time again
    public void No()
    {
        canvas.SetActive(false);
        Time.timeScale = 1f;
    }
    // When the player collides with the dialogue colider, it stops the time and shows the canvas to capture the players decision
   private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player" && statCount.roundsCompleted == true)
        {
            Debug.Log("Jugador quiere terminar?");
            ShowDialogue();
            Time.timeScale = 0f;
        }
    }
}
