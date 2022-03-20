using UnityEngine;
using Mono.Data.Sqlite;
using System.Data;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ReadDB : MonoBehaviour
{
    //Represents the final stored in the database per run
    public Text finalDB;
    //Represents the amount of coins stored in db per run
    public Text coinsDB;
    //Represents the time that has taken to complete the game
    public Text timeDB;
    //Represents the amount of kills achieved in the game
    public Text killsDB;

    void Start()
    {
        ReadDatabase();
    }
    //Function that reads the database in the assets folder and puts the data in the text fields 
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
    //Funciton to go back to the main menu
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
   
}
