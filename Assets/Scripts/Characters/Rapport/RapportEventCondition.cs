using System;
using CoreLib.Complex_Types;
using Game;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Strategy pattern for determining if a rapport event should be triggered.
    /// </summary>
    [Serializable]
    public class RapportEventCondition
    {
        public virtual bool EligibleForRapportEvent() => true;
    }
    
    public class RapportCondition : RapportEventCondition
    {
        [SerializeField]public ReversiblePair<string> CharacterKeys;
        public int MinRapport = 0;
        public override bool EligibleForRapportEvent() => GameState.Current.Player.Rapport[CharacterKeys] >= MinRapport;
    }
    
    public class PreviousEventCondition : RapportEventCondition
    {
        public string PreviousEventKey;
        public override bool EligibleForRapportEvent() => GameState.Current.Player.CompletedRapportEvent(PreviousEventKey);
    }
    
    public class PlayerLevelCondition : RapportEventCondition
    {
        public int Level;
        public override bool EligibleForRapportEvent() => GameState.Current.Player.Leader.Level >= Level;
    }
    
    public class RenownLevelCondition : RapportEventCondition
    {
        public int Level;
        public override bool EligibleForRapportEvent() => GameState.Current.Player.RenownLevel >= Level;
    }
    
    public class CharacterLevelCondition : RapportEventCondition
    {
        public string CharacterKey;
        public int Level;
        public override bool EligibleForRapportEvent() => GameState.Current.Player.Leader.Level >= Level;
    }
    
    public class WorldFlagCondition : RapportEventCondition
    {
        public string Flag;
        public bool ExpectedValue = true;
        public override bool EligibleForRapportEvent() => GameState.Current.WorldFlags[Flag] == ExpectedValue;
    }
    
    public class WorldValueCondition : RapportEventCondition
    {
        public string Key;
        public int MinValue = -1;
        public int MaxValue = 99;
        public override bool EligibleForRapportEvent() => GameState.Current.WorldValues[Key] >= MinValue && GameState.Current.WorldValues[Key] <= MaxValue;
    }
    
    public class ItemCondition : RapportEventCondition
    {
        public string ItemKey;
        public int MinQuantity = 1;
        public override bool EligibleForRapportEvent() => GameState.Current.Player.Inventory.HasItems(ItemKey, MinQuantity);
    }

}