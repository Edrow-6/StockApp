using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace StockApp.Settings
{
    public static class ConfigFileManager
    {
        public static void LoadConfigFiles(Program app)
        {
            var configs = Assembly.GetExecutingAssembly().GetTypes().Where(x => x.IsClass && x.Namespace == "StockApp.Settings.Models" && x.IsSubclassOf(typeof(Config)));
            foreach (var property in app.GetType().GetProperties().Where(x => x.PropertyType.IsSubclassOf(typeof(Config))))
            {
                string filePath = Config.BasePath + property.PropertyType.GetCustomAttribute<ConfigFileAttribute>().FileName;

                if (!File.Exists(filePath)) continue;
                object c = JsonConvert.DeserializeObject(File.ReadAllText(filePath), property.PropertyType) ?? Activator.CreateInstance(property.PropertyType);
                property.SetValue(app, c);
            }
        }
    }
}