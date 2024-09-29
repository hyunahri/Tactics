using CoreLib.Complex_Types;
using UnityEngine;

namespace CoreLib.Events
{
    public static class StaticEvents_Music
    {
        //Music
        public static CoreEvent<string> RequestSong = new CoreEvent<string>();
        public static CoreEvent<string, float> RequestAudioOneShot = new CoreEvent<string, float>();

        //TTS
        public static CoreEvent<AudioClip> RequestTTS = new CoreEvent<AudioClip>();
        public static CoreEvent<string> RequestStreamingTTS = new CoreEvent<string>();

        //UI
        public static CoreEvent OnNotification = new CoreEvent();
        public static CoreEvent OnActionClick = new CoreEvent(); //TODO
        public static CoreEvent OnActionSucceed = new CoreEvent();
        public static CoreEvent OnActionFailed = new CoreEvent();
        
        public static CoreEvent OnCloseMenu = new CoreEvent();
        public static CoreEvent OnOpenMenu = new CoreEvent();
    }
}