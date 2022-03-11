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
    void LoadData(){

    }
    void SaveData(){

    }

    void Update()
    {
        if(isTimer)
        {
            timer += Time.deltaTime;
            DisplayTime();
        }
        if(killed){

            kills++;
            killsText.text = "MUERTES :   "+kills;
            killed = false;
        }
    }
    void Start(){

        StartTimer();
    }
    void DisplayTime(){
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer -minutes * 60);
        timerText.text = "TIEMPO :   "+string.Format("{0:00}:{1:00}", minutes, seconds);
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
    public void AddKill()
    {
        killed = true;
    }
}
