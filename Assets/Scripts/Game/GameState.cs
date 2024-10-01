using System;
using CoreLib.Complex_Types;

namespace Game
{
    /// <summary>
    /// Various data and flags that define a campaign. Will serve as root of save file later.
    /// </summary>
    public class GameState
    {
        public static GameState Current = new();

        public GameState()
        { 
            Preferences.FromFile(); //Load preferences from disk
        }
        
        
        //Time
        public int Day;
        
        
        //Player
        public Player Player = new();
        
        
        //World
        public DefaultDict<string, int> WorldValues = new(StringComparer.OrdinalIgnoreCase);
        public DefaultDict<string, bool> WorldFlags = new(StringComparer.OrdinalIgnoreCase);
    }
}