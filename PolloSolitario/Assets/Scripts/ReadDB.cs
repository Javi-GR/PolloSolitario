using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadDB : MonoBehaviour
{
    public Text finalDB;
    public Text coinsDB;
    public Text timeDB;
    public Text killsDB;

    void Start()
    {
        ReadDatabase();
    }
    void ReadDatabase()
    {
        string conn = "URI=file:"+Application.dataPath+"/scoredb.db";
        IDbConnection dbconn;
        dbconn = (IDbConnection)new SqliteConnection(conn);
        dbconn.Open();
        IDbCommand dbcmd = dbconn.CreateCommand();
        string query = "SELECT final, time, coins, kills FROM score";
        dbcmd.CommandText = query;
        IDataReader reader = dbcmd.ExecuteReader();
        while(reader.Read())
        {
            
            finalDB.text += reader["final"]+"\n";
            coinsDB.text += reader["coins"]+"\n";
            timeDB.text += reader["time"]+"\n";
            killsDB.text += reader["kills"]+"\n";

        }
        reader.Close();
        reader = null;
        dbcmd.Dispose();
        dbcmd = null;
        dbconn.Close();
        dbconn = null;
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
   
}
