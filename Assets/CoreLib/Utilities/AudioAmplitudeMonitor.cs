using CoreLib.Complex_Types;
using UnityEngine;

namespace CoreLib.Utilities
{
    public class AudioAmplitudeMonitor : MonoBehaviour {
 
        public AudioSource audioSource;
        private float updateStep = 0.05f;
        private int sampleDataLength = 4096;
 
        private float currentUpdateTime = 0f;
 
        [SerializeField]private float clipLoudness;
        private float[] clipSampleData;

        public CoreEvent<float> OnAmplitudeChange = new CoreEvent<float>();

        void Awake ()
        {

            audioSource = GetComponent<AudioSource>();
            if (!audioSource) {
                Debug.LogError(GetType() + ".Awake: there was no audioSource set.");
            }
            clipSampleData = new float[sampleDataLength];
 
        }
     
        void Update () {
     
            currentUpdateTime += UnityEngine.Time.deltaTime;
            if (currentUpdateTime >= updateStep) {
                currentUpdateTime = 0f;
                audioSource.clip.GetData(clipSampleData, audioSource.timeSamples); //1024 samples is about 80 ms on a 44khz stereo clip, beginning at the current sample position of the clip.
                clipLoudness = 0f;
                foreach (var sample in clipSampleData) {
                    clipLoudness += Mathf.Abs(sample);
                }
                clipLoudness /= sampleDataLength; 
                OnAmplitudeChange.Invoke(clipLoudness);
            }
 
        }
 
    }
}