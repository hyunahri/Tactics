using Characters;
using Game;

namespace CoreLib.Extensions
{
    public static class Extensions_Fetch
    { 
        public static Character FetchCharacter(this string key) => GlobalCharacterManager.GetCharacter(key);
    }
}