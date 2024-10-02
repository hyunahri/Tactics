using UnityEngine;

namespace Dialogue
{
    [System.Serializable]
    public class Line
    {
        [SerializeField]public Emotions Emotion;
        [SerializeField]public string Text;
    }
}