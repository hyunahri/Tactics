using System;
using System.Collections.Generic;
using Characters;
using CoreLib.Complex_Types;
using UnityEngine;

namespace Game
{
    public class Player
    {
        public Character? Leader; //The character which represents the player
        
        //Gold - Basic currency
        [SerializeField] private int gold;
        public int Gold { get => gold; set => gold = value; }
        public bool TrySpendGold(int amount)
        {
            if (Gold < amount) return false;
            Gold -= amount;
            return true;
        }

        //Renown - Gates various mechanics and is relatively slow to grow. 
        public int Renown;
        public int RenownLevel
        {
            get
            {
                int level;
                for (level = 0; level < 10; level++)
                    if (Renown < 1000 * (level + 1)) break;
                return level;
            }
        }
        
        //Progression
        public PlayerCharacterManager GlobalCharacterManager = new();
        
        //Inventory
        public PlayerInventory Inventory = new();
        
        //Rapport - Relationship between pairs of characters, affecting quests and interactions.
        public DefaultDict<ReversiblePair<string>, int> Rapport = new(() => 0); //Dictionary of character pairs and their rapport with each other. The key is reversible and rapport is mirrored.
        public Dictionary<string, RapportEventResult> RapportEventResults = new(StringComparer.OrdinalIgnoreCase); //Dictionary of rapport events and their results, a non-null value means the event has been completed.
        public bool CompletedRapportEvent(RapportEventTemplate template) => CompletedRapportEvent(template.RapportEventKey);
        public bool CompletedRapportEvent(string key) => RapportEventResults.ContainsKey(key);
    }
}