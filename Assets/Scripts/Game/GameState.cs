using System;
using CoreLib.Complex_Types;

namespace Game
{
    public class GameState
    {
        public string PlayerName;

        public int Day;
        public float Hour;
        
        public DefaultDict<string, int> Inventory = new DefaultDict<string, int>(StringComparer.OrdinalIgnoreCase);
        public int Money => Inventory["f"];
        
        //Preferences
        public bool AlwaysRun = false;
        
        
        //Progression
        public int MaxUnitSize => Values["maxUnitSize"];
        public DefaultDict<string, int> Values = new DefaultDict<string, int>(StringComparer.OrdinalIgnoreCase);
        public DefaultDict<string, bool> Flags = new DefaultDict<string, bool>(StringComparer.OrdinalIgnoreCase);
        
        //Rapport
        public DefaultDict<ReversiblePair<string>, int> Rapport = new DefaultDict<ReversiblePair<string>, int>(() => 0);
    }
}