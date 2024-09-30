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
            Preferences.FromFile();
        }
        
        public CharacterManager CharacterManager = new();
        
        //Time
        public int Day;
        
        
        //Player
        public Player Player = new();
        
        
        //Preferences
        public bool AlwaysRun = false;
        
        
        
        public DefaultDict<string, int> WorldValues = new(StringComparer.OrdinalIgnoreCase);
        public DefaultDict<string, bool> WorldFlags = new(StringComparer.OrdinalIgnoreCase);
        

    }
}