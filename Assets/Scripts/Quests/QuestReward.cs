using CoreLib.Complex_Types;
using CoreLib.Extensions;
using Game;
using Items;

namespace Quests
{
    /// <summary>
    /// Wow, another strategy pattern!
    /// </summary>
    public class QuestReward
    {
        public virtual bool IsHidden => false;
        public virtual string Description { get; } = "";
        public virtual void GiveReward(){}
    }
    
    public class FlatGoldReward : QuestReward
    {
        public int Gold;
        public override string Description => $"Receive {Gold} gold.";
        public override void GiveReward()
        {
            GameState.Current.Player.Gold += Gold;
        }
    }
    
    public class EquipmentReward : QuestReward
    {
        public EquipmentTemplate Equipment;
        public override string Description => $"Receive a {Equipment.Name}.";
        public override void GiveReward()
        {
            GameState.Current.Player.Inventory.AddItem(new Equipment(Equipment));
        }
    }
    
    public class RapportReward : QuestReward
    {
        public ReversiblePair<string> CharacterKeys;
        public int RapportGain;

        public override string Description => $"Improves rapport between {CharacterKeys.Item1.FetchCharacter().GetName()} and {CharacterKeys.Item2.FetchCharacter().GetName()} by {RapportGain}.";

        public override void GiveReward()
        {
            GameState.Current.Player.Rapport[CharacterKeys] += RapportGain;
        }
    }
    
    public class WorldFlagReward : QuestReward
    {
        public override bool IsHidden => true;
        public string FlagKey;
        public bool FlagValue;
        public override string Description => $"Sets world flag {FlagKey} to {FlagValue}.";
        public override void GiveReward()
        {
            GameState.Current.WorldFlags[FlagKey] = FlagValue;
        }
    }
    
    public class WorldValueReward : QuestReward
    {
        public override bool IsHidden => true;
        public string ValueKey;
        public int Value;
        public override string Description => $"Sets world value {ValueKey} to {Value}.";
        public override void GiveReward()
        {
            GameState.Current.WorldValues[ValueKey] = Value;
        }
    }
    
}