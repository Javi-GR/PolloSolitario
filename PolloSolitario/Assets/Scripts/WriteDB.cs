using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;


public class WriteDB : MonoBehaviour
{
    StatCount statCount;
    // Start is called before the first frame update
    void Start()
    {
        statCount = GameObject.FindGameObjectWithTag("HUD").GetComponent<StatCount>();
    }
    public void AddScore()
    {
       string conn = "URI=file:"+Application.dataPath+"/scoredb.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        Debug.Log("INSERT INTO score VALUES('"+statCount.currentChoice.ToString()+"','"+statCount.GetTime()+"','" +statCount.GetCoins()+"','"+statCount.GetKills()+"');");
        string query = "INSERT INTO score VALUES('"+statCount.currentChoice.ToString()+"','"+statCount.GetTime()+"','" +statCount.GetCoins()+"','"+statCount.GetKills()+"');";
        dbcmd.CommandText = query;
        IDataReader reader = dbcmd.ExecuteReader();
        
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }
}
