using System.IO;
using IniParser;
using UnityEngine;

namespace Game
{
    /// <summary>
    /// User preferences loaded at runtime.
    /// </summary>
    public static class Preferences
    {
        private static string path = Path.Combine(Application.streamingAssetsPath, "preferences.ini");
        
        public static void FromFile()
        {
            IniParser.FileIniDataParser parser = new IniParser.FileIniDataParser();
            IniParser.Model.IniData data = parser.ReadFile(path);
            //Iterate over ini and use reflection to assign to properties in this class
            foreach (var section in data.Sections)
            {
                foreach (var key in section.Keys)
                {
                    System.Reflection.PropertyInfo prop = typeof(Preferences).GetProperty(key.KeyName);
                    if (prop == null) continue;
                    if (prop.PropertyType == typeof(bool))
                        prop.SetValue(null, bool.Parse(key.Value));
                    else if (prop.PropertyType == typeof(int))
                        prop.SetValue(null, int.Parse(key.Value));
                    else if (prop.PropertyType == typeof(float))
                        prop.SetValue(null, float.Parse(key.Value));
                    else if (prop.PropertyType == typeof(string)) prop.SetValue(null, key.Value);
                }
            }
        }
        
        public static void ToFile()
        {
            IniParser.FileIniDataParser parser = new IniParser.FileIniDataParser();
            IniParser.Model.IniData data = new IniParser.Model.IniData();
            data.Sections.AddSection("Preferences");
            foreach (var prop in typeof(Preferences).GetProperties()) 
                data["Preferences"].AddKey(prop.Name, prop.GetValue(null).ToString());
            parser.WriteFile(path, data);
        }


        public static bool AlwaysRun { get; set; }
        
    }
}