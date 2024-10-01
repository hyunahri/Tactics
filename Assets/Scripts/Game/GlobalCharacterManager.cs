using System.Collections.Generic;
using Characters;

namespace Game
{
    /// <summary>
    /// Used for storing and managing all characters in the game.
    /// Mainly for retrieval.
    /// 
    /// </summary>
    public static class GlobalCharacterManager
    {
        //TODO implement
        public static Dictionary<string, Character> AllCharacters = new Dictionary<string, Character>();
        public static Character GetCharacter(string key) => null;
        public static Character GetCharacter(CharacterTemplate template) => null;
    }
}