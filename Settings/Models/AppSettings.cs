using System.Collections.Generic;

namespace StockApp.Settings.Models
{
    [ConfigFile("settings.json")]
    public class AppSettings : Config
    {
        public Dictionary<string, string> Database { get; set; }

        public AppSettings()
        {
            Database = new Dictionary<string, string>();
        }
    }
}