using System;
using System.Collections.Generic;
using CoreLib;
using UnityEngine;

namespace Storage
{
    /// <summary>
    /// Static class for managing all audio resources in the game.
    /// </summary>
    public static class AudioRepo
    {
        private static bool loadedAudio = false;
        public static Dictionary<string, AudioClip> AudioClips = new Dictionary<string, AudioClip>(StringComparer.OrdinalIgnoreCase);
        public static AudioClip GetAudioClip(string key) => AudioClips[key];
        public static void AddAudioClip(string key, AudioClip clip) => AudioClips.Add(key, clip);
        
        public static void LoadAudio()
        {
            if (loadedAudio)
                return;
            loadedAudio = true;
            LoadAudioInternal();
            LoadAudioExternal();
        }
        
        
        private static void LoadAudioInternal()
        {
            var resources = Resources.LoadAll<AudioClip>(DataPaths.InternalAudioPath);
            foreach (var resource in resources)
            {
                AudioClips.Add(resource.name, resource);
            }
        }

        private static void LoadAudioExternal()
        {
            //TODO
            foreach (var path in DataPaths.AudioPathExternal)
            {
                //load from directory and sub directories, mp3 and wav
                var files = System.IO.Directory.GetFiles(path, "*.*", System.IO.SearchOption.AllDirectories);
                foreach (var file in files)
                {
                    if (file.EndsWith(".mp3") || file.EndsWith(".wav"))
                    {
                        string filenameWithoutExtension = System.IO.Path.GetFileNameWithoutExtension(file);
                        using var audio = new WWW("file://" + file);
                        var clip = audio.GetAudioClip();
                        if (clip != null)
                        {
                            AudioClips.Add(filenameWithoutExtension, clip);
                        }
                    }
                }
            }
        }
    }
}