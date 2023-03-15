using MySql.Data.MySqlClient;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConnectionInfo
{
    public static string ip = "127.0.0.1";
    public static string uid = "root";
    public static string pwd = "12345";
    public static string database = "gamedb";
}

public class DbManager : MonoBehaviour
{
    static string connectionString = $"server = {ConnectionInfo.ip}; uid = {ConnectionInfo.uid}; pwd = {ConnectionInfo.pwd}; Database = {ConnectionInfo.database}; SSLMode = none";

    static MySqlConnection con;
    private void Awake()
    {
        con = new MySqlConnection(connectionString);
        try
        {
            con.Open();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }
    }
    private void OnApplicationQuit()
    {
        con.Close();
    }

    public void InsertToPlayers(string Name, int balance)
    {
        string query = $"insert into gamedb.players (name, balance) values ('{Name}',{balance})";

        var command = new MySqlCommand(query,con);
        try
        {
            command.ExecuteNonQuery();
        }
        catch (System.Exception ex)
        {
            Debug.LogError(ex.Message);
        }        
        command.Dispose();
    }

    public List<string> SelectFromPlayers()
    {
        string query = $"select name from gamedb.players";
        List<string> nickList = new List<string>();
        MySqlCommand command = new MySqlCommand(query,con);

        try
        {
            var reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    nickList.Add(reader.GetString("name"));
                }
               /* reader.Read();*/
                /*Debug.Log(reader.GetString("name"));*/
                command.Dispose();
                return nickList;
            }
            else
            {
                command.Dispose();
                return nickList;
            }
        }
        catch (System.Exception ex)
        {
            command.Dispose();
            Debug.LogError(ex.Message);
            return nickList;
        }
        
    }
}
