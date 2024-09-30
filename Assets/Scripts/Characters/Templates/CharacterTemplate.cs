using UnityEngine;

namespace Characters
{
    //Placeholder for now.
    
    [CreateAssetMenu(fileName = "Character Template", menuName = "Characters/Character")]
    public class CharacterTemplate : ScriptableObject
    {
        public string Key;
        public string Name;

        public Class Class;
    }
}