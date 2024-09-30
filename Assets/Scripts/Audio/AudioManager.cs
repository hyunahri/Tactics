using System.Collections;
using Events;
using Storage;
using UnityEngine;

namespace Audio
{
    /// <summary>
    /// Singleton for managing all audio in the game.
    /// </summary>
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;

        //public bool amInstance = false;

        private bool playMenuHum = false;
        private float menuHumVolume = 1f;
        
        //Sources
        AudioSource BackgroundMusicAudioSource1;
        AudioSource BackgroundMusicAudioSource2;
        AudioSource VoiceAudioSource;
        AudioSource MenuEffectsAudioSource;
        AudioSource OneShotAudioSource;
        private AudioSource MenuHumAudioSource;

        public int activeAudioSourceIndex = 1;
        AudioSource activeSource => activeAudioSourceIndex == 1 ? BackgroundMusicAudioSource1 : BackgroundMusicAudioSource2;
        AudioSource inactiveSource => activeAudioSourceIndex == 1 ? BackgroundMusicAudioSource2 : BackgroundMusicAudioSource1;
        

        //BakedClips
        AudioClip successClip => AudioRepo.GetAudioClip("Success");
        AudioClip failureClip => AudioRepo.GetAudioClip("Failure");

        //Pausing
        bool isPaused = false;

        //Volumes
        static float MasterVolume = .5f;

        private void Awake() => OnEnable();


        public static void SetMasterVolume(float val)
        {
            MasterVolume = val;
            if (Instance)
                Instance.ConfigureVolumes();
        }

        public static float GetMasterVolume() => MasterVolume;

        static float MusicVolume = .6f;

        public static void SetMusicVolume(float val)
        {
            MusicVolume = val;
            if (Instance)
                Instance.ConfigureVolumes();
        }

        public static float GetMusicVolume() => MusicVolume;

        static float OneShotVolume = 1f;

        public static void SetOneShotVolume(float val)
        {
            OneShotVolume = val;
            if (Instance)
                Instance.ConfigureVolumes();
        }

        public static float GetOneShotVolume() => OneShotVolume;

        static float VoiceVolume = 1f;

        public static void SetVoiceVolume(float val)
        {
            VoiceVolume = val;
            if (Instance)
                Instance.ConfigureVolumes();
        }

        public static float GetVoiceVolume() => VoiceVolume;

        static float MenuVolume = .4f;

        public static void SetMenuVolume(float val)
        {
            MenuVolume = val;
            if (Instance)
                Instance.ConfigureVolumes();
        }

        public static float GetMenuVolume() => MenuVolume;

        //Methods

        private void OnEnable()
        {
            if (Instance != this && Instance != null) //Duplicate handling
            {
                this.enabled = false;
                return;
            }
            Instance = this;
            AudioRepo.LoadAudio();
            CreateAudioSources();
            ConfigureVolumes();
            AudioEvents.RequestSong.AddListener(PlaySong);
            AudioEvents.OnActionFailed.AddListener(PlayFailure);
            AudioEvents.OnActionSucceed.AddListener(PlaySuccess);
            AudioEvents.RequestAudioOneShot.AddListener(PlayOneShot);
            AudioEvents.RequestTTS.AddListener(PlayVoice);
            AudioEvents.OnCloseMenu.AddListener(OnCloseMenu);
            AudioEvents.OnOpenMenu.AddListener(OnOpenMenu);
            AudioEvents.RequestStreamingTTS.AddListener(PlayStreamingVoice);
            //AudioEvents.UIPanelsStateChanged.AddListener(OnUIStateChanged);
            DontDestroyOnLoad(gameObject);
        }
        
        private void OnDisable()
        {
            AudioEvents.RequestSong.RemoveListener(PlaySong);
            AudioEvents.OnActionFailed.RemoveListener(PlayFailure);
            AudioEvents.OnActionSucceed.RemoveListener(PlaySuccess);
            AudioEvents.RequestAudioOneShot.RemoveListener(PlayOneShot);
            AudioEvents.RequestTTS.RemoveListener(PlayVoice);
            AudioEvents.OnCloseMenu.RemoveListener(OnCloseMenu);
            AudioEvents.OnOpenMenu.RemoveListener(OnOpenMenu);
            AudioEvents.RequestStreamingTTS.RemoveListener(PlayStreamingVoice);
            //AudioEvents.UIPanelsStateChanged.RemoveListener(OnUIStateChanged);
        }

        /*
        private void OnUIStateChanged()
        {
            bool toPlay = UIPanels.Instance.AnyPanelIsActive;
            if (toPlay)
            {
                if (!MenuHumAudioSource.isPlaying)
                    MenuHumAudioSource.Play();
            }
            else
            {
                if (MenuHumAudioSource.isPlaying)
                    MenuHumAudioSource.Stop();
            }
        }
        */
        
        

        void CreateAudioSources()
        {
            BackgroundMusicAudioSource1 ??= gameObject.AddComponent<AudioSource>();
            BackgroundMusicAudioSource1.loop = true;
            BackgroundMusicAudioSource2 ??= gameObject.AddComponent<AudioSource>();
            BackgroundMusicAudioSource2.loop = true;
            MenuEffectsAudioSource ??= gameObject.AddComponent<AudioSource>();
            OneShotAudioSource ??= gameObject.AddComponent<AudioSource>();
            VoiceAudioSource ??= gameObject.AddComponent<AudioSource>();
            MenuHumAudioSource ??= gameObject.AddComponent<AudioSource>();
            MenuHumAudioSource.loop = true;
            MenuHumAudioSource.clip = AudioRepo.GetAudioClip("MenuHum");
        }

        private void ConfigureVolumes()
        {
            BackgroundMusicAudioSource1.volume = MusicVolume * MasterVolume;
            BackgroundMusicAudioSource2.volume = MusicVolume * MasterVolume;
            VoiceAudioSource.volume = VoiceVolume * MasterVolume;
            MenuEffectsAudioSource.volume = MenuVolume * MasterVolume;
            OneShotAudioSource.volume = OneShotVolume * MasterVolume;
            MenuHumAudioSource.volume = menuHumVolume * MasterVolume;
        }

        private void PlaySong(string audio)
        {
            var clip = AudioRepo.GetAudioClip(audio);
            if (isPaused)
                activeSource.clip = clip;
            else
            {
                if (activeSource.isPlaying)
                    StartCoroutine(FadeTracks(activeSource, inactiveSource, AudioRepo.GetAudioClip(audio), 2f));
                else
                {
                    activeSource.clip = clip;
                    activeSource.Play();
                    activeSource.loop = true; //CHECK THIS LATER
                }
            }
        }

        private void PlaySuccess() => MenuEffectsAudioSource.PlayOneShot(successClip, MenuVolume);
        private void PlayFailure()
        {
           // Debug.LogWarning("Action Failure Log");
            MenuEffectsAudioSource.PlayOneShot(failureClip, MenuVolume);
        }

        private void PlayVoice(AudioClip clip)
        {
            if (VoiceAudioSource.isPlaying)
                VoiceAudioSource.Stop();
            VoiceAudioSource.PlayOneShot(clip);
        }
        
        
        private void PlayStreamingVoice(string url) => StartCoroutine(StreamVoice(url));
        
        private IEnumerator StreamVoice(string url)
        {
            WWW www = new WWW(url);
            yield return www;
            VoiceAudioSource.clip = www.GetAudioClip(false, true, AudioType.WAV);
            VoiceAudioSource.Play();
        }

        private IEnumerator FadeTracks(AudioSource oldAudio, AudioSource newAudio, AudioClip newClip, float fadeTime)
        {
            // If the old audio is playing, fade it out.
            if (oldAudio.isPlaying)
            {
                float oldAudioStartVolume = oldAudio.volume;
                while (oldAudio.volume > 0)
                {
                    oldAudio.volume -= oldAudioStartVolume * (Time.deltaTime / fadeTime);
                    yield return null;
                }

                oldAudio.Stop();
            }

            // If there's new audio to fade in, start it at zero volume and fade it in.
            if (newClip != null)
            {
                newAudio.clip = newClip;
                newAudio.volume = 0;
                newAudio.Play();

                while (newAudio.volume < MusicVolume)
                {
                    newAudio.volume += Time.deltaTime / fadeTime;
                    yield return null;
                }
            }

            activeAudioSourceIndex = activeAudioSourceIndex == 1 ? 2 : 1;
        }

        private void PlayOneShot(string clipName, float volume)
        {
            AudioClip clip = AudioRepo.GetAudioClip(clipName);
            if (clip == null)
                return;
            OneShotAudioSource.PlayOneShot(clip, volume);
        }

        /*
        public void TogglePause()
        {
            isPaused = !isPaused;
            if (isPaused)
            {
                activeSource.Pause();
                inactiveSource.Pause();
                if (MusicIcon)
                    MusicIcon.sprite = MusicOff;
            }
            else
            {
                activeSource.UnPause();
                if (MusicIcon)
                    MusicIcon.sprite = MusicOn;
            }
        }
        */
        
        public void OnOpenMenu() => PlayOneShot("OpenMenu", MenuVolume);
        public void OnCloseMenu() => PlayOneShot("CloseMenu", MenuVolume);
    }
}