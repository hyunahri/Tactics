using UnityEngine;

namespace Characters
{
    [CreateAssetMenu(fileName = "Character Template", menuName = "Characters/Character")]
    public class CharacterTemplate : ScriptableObject
    {
        public string Key;

        public string Name;

        public Class Class;
    }
}