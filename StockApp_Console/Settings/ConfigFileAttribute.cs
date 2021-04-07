using System;

namespace StockApp_Console.Settings
{
    public class ConfigFileAttribute : Attribute
    {
        public string FileName { get; }

        public ConfigFileAttribute(string fileName)
        {
            FileName = fileName;
        }
    }
}