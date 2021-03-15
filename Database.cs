using System;
using MySqlConnector;
using StockApp.Settings;
using StockApp.Settings.Models;
using System.Collections.Generic;
using StockApp;

public class Database : IDisposable
{
    public readonly MySqlConnection Connection;

    public Database()
    {

        /*var test = MainApp.Settings.Database ?? new Dictionary<string, string>();
        test.Add("test", "tast");*/
        // 0 = HOST | 1 = PORT | 2 = USER_ID | 3 = PASSWORD | 4 = DATABASE
        string[] dbCredentials = { "127.0.0.1", "3306", "github", "dev", "csharp" };
        Connection = new MySqlConnection($"host={dbCredentials[0]};port={dbCredentials[1]};user id={dbCredentials[2]};password={dbCredentials[3]};database={dbCredentials[4]};");
    }

    public void Dispose()
    {
        Connection.Close();
    }
}