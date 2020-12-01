using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using System;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

public class DB_ManagerTest : MonoBehaviour
{
    public int level = 10;
    public InputField userInput;
    public string username;
    public MySqlConnection conn;

    // Start is called before the first frame update
    void Start()
    {
        userInput = GameObject.Find("InputField").GetComponent<InputField>();

        string connStr = "Server=127.0.0.1; database=CS596; user=Dan; password=danv21";
        conn = new MySqlConnection(connStr);
    }

    public void userEntered()
    {

        username = userInput.text;
        createUser();
        seeStats();
        //updateDB(true, username);

    }

    public void createUser()
    {

        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql = "INSERT INTO Player (username, budget, numWins, numLosses, numPlayed, rank) VALUES ('" + username + "', 10, 0, 0, 0, 1)";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Debug.Log(rdr[0] + " -- " + rdr[1]);
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();
        Debug.Log("Done.");
    }

    public void updateDB(bool winner, string user)
    {

        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql1 = "SELECT * FROM Player WHERE username='" + user + "'";
            MySqlCommand cmd1 = new MySqlCommand(sql1, conn);
            MySqlDataReader rdr1 = cmd1.ExecuteReader();
            rdr1.Read();
            int numWins = (int)rdr1[2];
            int numLosses = (int)rdr1[3];
            int rank = (int)rdr1[5];
            rdr1.Close();

            if (winner)
            {
                numWins++;
                if (numWins % level == 0)
                {
                    rank++;
                }
            }
            else
            {
                numLosses++;
            }
            int numPlayed = numWins + numLosses;

            string sql = "UPDATE Player SET numWins=" + numWins + ", numLosses=" + numLosses + ", numPlayed=" + numPlayed + ", rank=" + rank + " WHERE username='" + user + "'";
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();
            rdr.Read();
            rdr.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }

        conn.Close();
        Debug.Log("Done.");
    }

    public void seeStats()
    {
        string output = "Stats: ";

        try
        {
            Debug.Log("Connecting to MySQL...");
            conn.Open();

            string sql = "SELECT * FROM Player WHERE username='" + username + "'";

            MySqlCommand cmd = new MySqlCommand(sql, conn);
            MySqlDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                output += rdr[0] + " -- " + rdr[2] + " -- " + rdr[3] + " -- " + rdr[4] + " -- " + rdr[5];
            }
            rdr.Close();
        }
        catch (Exception ex)
        {
            Debug.Log(ex.ToString());
        }
        conn.Close();
        Debug.Log("Done.");

        Debug.Log(output);
    }
}