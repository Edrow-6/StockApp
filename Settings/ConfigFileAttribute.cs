using System;

namespace StockApp.Settings
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