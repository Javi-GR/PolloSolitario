using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDecisionScript : MonoBehaviour
{
    //Left truck blocking the road
    public GameObject truckL;
    //right truck blocking the road
    public GameObject truckR;
    //Upper truck blocking the road 
    public GameObject truckU;
    //The right cross image in the canvas to set the available directions visually
    public Image crossR;
    //The left cross image in the canvas to set the available directions visually
    public Image crossL;
    //The upper cross image in the canvas to set the available directions visually
    public Image crossU;
    //The right arrow image in the canvas to set the available directions visually
    public Image arrowR;
    //The left arrow image in the canvas to set the available directions visually
    public Image arrowL;
    //The upper arrow image in the canvas to set the available directions visually
    public Image arrowU;
    //Reference to the statCount in HUD
    StatCount statCount;
    //Reference to the database script, so it saves the progress made while finishing the game
    WriteDB writeDB;

    // Start is called before the first frame update
    void Start()
    {
        writeDB = GameObject.FindGameObjectWithTag("db").GetComponent<WriteDB>();
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
        arrowL.enabled = false;
        arrowU.enabled = false;
        arrowR.enabled = false;
        crossL.enabled = false;
        crossR.enabled = false;
        crossU.enabled = false;
    }

    //Called when pressing YES on the final dialogue
    public void CheckFinalState()
    {
        //If the game has been completed and the coins the player has are 0, the road to the pawn shop is open 
        if(statCount.completedGame == true && statCount.GetCoins() == 0)
        {
            statCount.StopTimer();
            statCount.currentChoice = StatCount.FinalChoice.POBRE;
            Debug.Log("No has conseguido monedas");
            arrowR.enabled = true;
            Destroy(truckR);
            crossL.enabled = true;
            crossU.enabled = true;
            writeDB.AddScore();
        }
        // If the player has gotten some coins, but not all of them, they go back to the woods 
        if(statCount.completedGame == true && statCount.GetCoins()>0 && statCount.GetCoins() <8)
        {
            statCount.StopTimer();
            statCount.currentChoice = StatCount.FinalChoice.LIBRE;
            Debug.Log("Has conseguido algunas monedas");
            arrowU.enabled = true;
            Destroy(truckU);
            crossR.enabled = true;
            crossL.enabled = true;
            writeDB.AddScore();
        }
        //if the player has gotten 8 coins and comleted the game the road that leads to the bank opens 
        if(statCount.completedGame == true && statCount.GetCoins() >=8)
        {
            statCount.StopTimer();
            statCount.currentChoice = StatCount.FinalChoice.RICO;
            Debug.Log("Has conseguido todas las monedas");
            crossU.enabled = true;
            crossR.enabled = true;
            Destroy(truckL);
            arrowL.enabled = true;
            writeDB.AddScore();
        }
    }
}
