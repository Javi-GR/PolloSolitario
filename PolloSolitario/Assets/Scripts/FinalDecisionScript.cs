using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FinalDecisionScript : MonoBehaviour
{
    public GameObject truckL;
    public GameObject truckR;
    public GameObject truckU;
    public Image crossR;
    public Image crossL;
    public Image crossU;
    public Image arrowR;
    public Image arrowL;
    public Image arrowU;
    StatCount statCount;
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

    public void CheckFinalState()
    {
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
