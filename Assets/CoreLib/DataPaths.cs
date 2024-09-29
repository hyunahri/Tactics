using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using UnityEngine;

namespace CoreLib
{
    public static class DataPaths
    {
        public const string SECTION_DELIMITER = ";"; // ComponentName|propertyName:100|propertyName:200; ComponentName|propertyName:100|propertyName:200
        public const string PAIR_DELIMITER = ":";  // minerals:100
        public const char KEY_VALUE_DELIMITER = ':';
        public const char STAT_MODIFIER_DELIMITER = KEY_VALUE_DELIMITER;

        public const string LIST_DELIMITER = "|"; // a|b|c
        
        
        
        
        //public static JsonSerializerSettings JSONSettings;
        public static string BasePath = Application.streamingAssetsPath;
        public static List<string> ModPaths = new List<string>(){BasePath};
        public static string DataPath = "Data/";
        public static string PortraitPath = "Portraits/";
        public static List<string> SpritePaths = new() { "Sprites/" };
        public static List<string> ShipsetsPath = new(){"Shipsets/"};
        public  static string InternalAudioPath = "Audio";
        public static string POIRootPath = "POI/";
        
        public static string CoreRecipePath = "Recipes/";

        //public static string ParticleDataPathInternal = "Data/Particles";
        //public static List<string> ParticleDataPathExternal = new List<string>(){Path.Combine(BasePath, "Data", "Particles")};
        
        public static string GetStreamingPath(string path) => Path.Combine(BasePath, path);
        
        
        
        
        
        
        
        //SHATTERED STAR IMPORTS
        public const string AudioPath = "Audio/";
        public static List<string> AudioPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/Sounds"};
        
        public const string HouseDataPath = "GameData/HouseData/";
        public static List<string> HouseDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/HouseData"};
        
        public const string RaceDataPath = "GameData/RaceData/";
        public static List<string> RaceDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/RaceData"};
        
        public const string FactionDataPath = "GameData/FactionData/";
        public static List<string> FactionDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/FactionData"};
        
        public const string ReligionDataPath = "GameData/ReligionData/";
        public static List<string> ReligionDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/ReligionData"};
        
        public const string ReligionSectDataPath = "GameData/ReligionSectData/";
        public static List<string> ReligionSectDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/ReligionSectData"};
        
        public const string ReligionHolySiteDataPath = "GameData/ReligionHolySiteData/";
        public static List<string> ReligionHolySiteDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/ReligionHolySiteData"};
        
        public const string TechDataPathInternal = "GameData/TechData/";
        public static List<string> TechDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/TechData"};

        public static string[] SpritePathsInternal = new string[]{"Sprites/UnitIcons", "Sprites/HouseIcons", "Sprites/ReligionIcons", "Sprites/ResourceIcons", "Sprites/StructureIcons"};
        public static List<string> SpritePathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/Sprites"};

        public static string StructureDataPathInternal = "GameData/StructureData/";
        public static List<string> StructureDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/StructureData/"};

        public static string[] RecipePathsInternal = new[] { "GameData/Recipes" };
        public static List<string> RecipePathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/Recipes/"};
        
        public static string MapPathSpaceExternal = $"{Application.streamingAssetsPath}/Maps/Space/";
        public static List<string> MapPathGroundExternal = new List<string>(){$"{Application.streamingAssetsPath}/Maps/Ground/"};
        
        public static string CulturePathInternal = "GameData/CultureData/";
        public static List<string> CulturePathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/CultureData/"};
        
        public static string HarvestPathInternal = "GameData/HarvestData/";
        public static List<string> HarvestPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/HarvestData/"};
        
        public const string UnitDataPath = "GameData/UnitData/";
        public static List<string> UnitDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/UnitData/"};
        
        public static string ResourceDataPathInternal ="GameData/ResourceData/";
        public static List<string> ResourceDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/ResourceData/"};
        
        public static string ParticleDataPathInternal = "GameData/ParticleData/";
        public static List<string> ParticleDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/ParticleData/"};

        public static string CharacterDataPathInternal = "GameData/CharacterData/";
        public static List<string> CharacterDataPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/CharacterData/"};
        
        public static string CharacterEventPathInternal = "GameData/EventData/";
        public static List<string> CharacterEventPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/EventData/"};
        
        public static string CharacterStoryPathInternal = "GameData/StoryData/";
        public static List<string> CharacterStoryPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/StoryData/"};
        
        public static string PortraitPathInternal = "Portraits/";
        public static List<string> PortraitPathsExternal = new List<string>(){ $"{Application.streamingAssetsPath}/Portraits/"};
        
        public static string CharacterTraitPathInternal = "GameData/CharacterTraits/";
        public static List<string> CharacterTraitPathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/CharacterTraits/"};
        
        public static string CharacterNamePathInternal = "GameData/CharacterNames/";
        public static List<string> CharacterNamePathExternal = new List<string>(){$"{Application.streamingAssetsPath}/GameData/CharacterNames/"};
        
        public static string TilePrefabPathInternal = "Prefabs/Tiles/";
        
        public static string ModdingHelpPathExternal = $"{Application.streamingAssetsPath}/!Modding/";
        
        public static string PersistentSettingsPathExternal = $"{Application.streamingAssetsPath}/Profile/";
        
        public static JsonSerializerSettings JSONSettings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new StringEnumConverter() },
            Formatting = Formatting.Indented,
            NullValueHandling = NullValueHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Auto
        };
        
        
        
        //[RuntimeInitializeOnLoadMethod(LoadTypeAfter)]
        public static void CreateAllExternalDirectories()
        {
            FieldInfo[] fields = typeof(DataPaths).GetFields(BindingFlags.Public | BindingFlags.Static);
            foreach (FieldInfo field in fields)
            {
                if (field.Name.EndsWith("External") && field.FieldType == typeof(string))
                {
                    string path = (string)field.GetValue(null);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
            }

            try
            {
                //   InternalCSVHelper.Write(ModdingHelper.GetRepoKeysWithDescriptorsForModding(), Path.Combine(ModdingHelpPathExternal, "RepoKeys.csv"));
            }
            catch
            {
                Debug.LogWarning("Couldn't write RepoKeys.csv, file is probably open.");
            }
            //string documentation = ModdingHelper.GenerateDocumentationForConsole();
            try
            {
                //    File.WriteAllText(Path.Combine(ModdingHelpPathExternal, "ConsoleCommands.txt"), documentation);
            }
            catch
            {
                Debug.LogWarning("Couldn't write ConsoleCommands.txt, file is probably open.");
            }
        }
        
        
        
        
        
        
    }
}