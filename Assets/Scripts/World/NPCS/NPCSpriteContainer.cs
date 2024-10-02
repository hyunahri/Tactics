using Dialogue;
using UnityEngine;

namespace World.NPCS
{
    [CreateAssetMenu(fileName = "NPCSpriteContainer", menuName = "NPC", order = 0)]
    public class NPCSpriteContainer : ScriptableObject
    {
        [SerializeField]public Sprite Normal;
        [SerializeField]public Sprite Happy;
        [SerializeField]public Sprite Sad;
        [SerializeField]public Sprite Angry;
        
        public Sprite GetSprite(Emotions emotion)
        {
            switch (emotion)
            {
                case Emotions.NEUTRAL:
                    return Normal;
                case Emotions.HAPPY:
                    return Happy;
                case Emotions.SAD:
                    return Sad;
                case Emotions.ANGRY:
                    return Angry;
                default:
                    throw new System.ArgumentOutOfRangeException(emotion.ToString());
            }
        }
    }
}