using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatCount : MonoBehaviour
{
    public Text killsText;
    private int kills = 0;
    private bool killed = false;
    public Text timerText; 
    private float timer = 0.0f;
    private bool isTimer = false;
    public Text roundText;
    public Text completedLevel;
    public Text coinCount;
    private bool coinTrigger = false;
    private int coins = 0;
    private int roundN = 1;
    private bool nextRound = false;
   
    void Update()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
            DisplayTime();
        }
        if(killed){

            kills++;
            killsText.text = " MUERTES :   "+kills;
            killed = false;
        }
        
        if(coinTrigger){
            coins++;
            coinCount.text = " "+coins;
            coinTrigger = false;
        }
        if(nextRound)
        {
            roundN++;
            DisplayRound();
            nextRound = false;

        }
    }
    void Start(){

        StartTimer();
    }
    void DisplayTime(){
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer -minutes * 60);
        timerText.text = " TIEMPO :   "+string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void DisplayRound()
    {
        roundText.text = " RONDA :   "+roundN;
    }
    public void StartTimer()
    {
        isTimer = true;
    }
    public void StopTimer(){
        isTimer = false;
    }
    public void ResetTimer()
    
    {
        timer = 0.0f;
    }
    public void AddRound(){
        nextRound = true;
    }
   
    public void AddKill()
    {
        killed = true;
    }
    public void ResetRounds(){
        roundN = 1;
    }
    public int GetRounds()
    {
        return roundN;
    }
    public int GetKills(){
        return kills;
    }
    public void LevelCompleted(){
        StartCoroutine(ShowCongratulations());
    }
    public void AddCoin(){
        coinTrigger = true;
    }
    private IEnumerator ShowCongratulations(){
        completedLevel.text = "NIVEL COMPLETADO";
        yield return new WaitForSeconds(3);
        completedLevel.text = "";

    }
}
