using System;
using MySqlConnector;

public class Database : IDisposable
{
    public readonly MySqlConnection Connection;

    public Database()
    {
        // 0 = HOST | 1 = PORT | 2 = USER_ID | 3 = PASSWORD | 4 = DATABASE
        string[] dbCredentials = { "127.0.0.1", "3306", "github", "dev", "csharp" };
        Connection = new MySqlConnection($"host={dbCredentials[0]};port={dbCredentials[1]};user id={dbCredentials[2]};password={dbCredentials[3]};database={dbCredentials[4]};");
    }

    public void Dispose()
    {
        Connection.Close();
    }
}