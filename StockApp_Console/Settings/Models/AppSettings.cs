using System.Collections.Generic;

namespace StockApp_Console.Settings.Models
{
    [ConfigFile("settings.json")]
    public class AppSettings : Config
    {
        public Dictionary<string, string> Database { get; set; }
        public string dbHost { get; set; }
        public string dbPort { get; set; }
        public string dbUser { get; set; }
        public string dbPassword { get; set; }
        public string dbName { get; set; }

        public AppSettings()
        {
            Database = new Dictionary<string, string>();
        }
    }
}