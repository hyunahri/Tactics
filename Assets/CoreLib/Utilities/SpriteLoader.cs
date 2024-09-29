using System.IO;
using System.Threading.Tasks;
using UnityEngine;

namespace CoreLib.Utilities
{
    public static class SpriteLoader
    {
        public static Sprite LoadSpriteFromStreamingAssets(string filePath, FilterMode mode = FilterMode.Point)
        {
            if (File.Exists(filePath))
            {
                byte[] fileData = File.ReadAllBytes(filePath);
                Texture2D texture = new Texture2D(2, 2) // Dummy size, will be replaced by loaded data
                {
                    filterMode = mode
                };  
                if (texture.LoadImage(fileData))
                {
                    // Create a new sprite using the loaded texture
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
                else
                {
                    FLog.LogError("Failed to load image as texture. " + filePath);
                    return null;
                }
            }
            else
            {
                FLog.LogError($"File not found: {filePath}");
                return null;
            }
        }
    
        public static async Task<Sprite> LoadSpriteFromStreamingAssetsAsync(string filePath)
        {
            if (File.Exists(filePath))
            {
                byte[] fileData = await Task.Run(() => File.ReadAllBytes(filePath));
                Texture2D texture = new Texture2D(2, 2);  // Dummy size, will be replaced by loaded data
                if (texture.LoadImage(fileData))
                {
                    // Create a new sprite using the loaded texture
                    return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                }
                else
                {
                    FLog.LogError("Failed to load image as texture. " + filePath);
                    return null;
                }
            }
            else
            {
                FLog.LogError($"File not found: {filePath}");
                return null;
            }
        }
    }
}