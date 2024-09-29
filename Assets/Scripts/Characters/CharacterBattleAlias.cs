using System;
using System.Collections.Generic;
using System.Linq;
using Combat;
using CoreLib.Complex_Types;

namespace Characters
{
    //<summary>The state of a character at a given point in time during combat</summary>
    public class CharacterBattleAlias : ICharacter
    {
        public CharacterBattleAlias(Character character)
        {
            Character = character;
            HP = character.CurrentHP;
            
            AP = MaxAP;
            PP = MaxPP;
        }

        public CharacterBattleAlias(CharacterBattleAlias prior)
        {
            this.Character = prior.Character;
            //clone prior
            HP = prior.HP;
            AP = prior.AP;
            PP = prior.PP;

            Statuses.AddRange(prior.Statuses);
            RebuildStatusOffets();
        }
        
        public Character Character;
        
        //
        public int HP = 0;
        public int MaxHP => GetStat("hp");
        public bool IsKnockedOut => HP <= 0;
        public bool IsDead => MaxHP <= 0;        
        
        public int AP = 0;
        public int MaxAP => GetStat("ap");
        public int PP = 0;
        public int MaxPP => GetStat("pp");

        public string GetName() => Character.GetName();
        public int Level => Character.Level;
        public Character GetRootCharacter() => Character.GetRootCharacter();

        public int GetStat(string stat) => Character.StatsManager.ClassStats[stat] + Character.StatsManager.EquipmentOffsets[stat] + Character.StatsManager.PermanentOffsets[stat] + StatusOffsets[stat];
        public DefaultDict<string, int> StatusOffsets = new DefaultDict<string, int>(() => 0, StringComparer.OrdinalIgnoreCase); //Offsets from status effects
        
        //
        public List<StatusEffect> Statuses = new List<StatusEffect>();

        public void AddStatus(StatusEffect statusEffect)
        {
            Statuses.Add(statusEffect);
            RebuildStatusOffets();
        }

        public void RemoveStatus(StatusEffect statusEffect)
        {
            Statuses.Remove(statusEffect);
            RebuildStatusOffets();
        }

        /*
        public void CheckStatusValidity()
        {
            bool changed = false;
            foreach(var status in Statuses.ToList())
                if (status.CheckValidity(this) == false)
                {
                    Statuses.Remove(status);
                    changed = true;
                }
            if(changed)
                RebuildStatusOffets();
        }
        */
        
        public void RebuildStatusOffets()
        {  //Rebuild offsets after changing status effects
            StatusOffsets.Clear();
            foreach (var status in Statuses)
            {
                foreach (var pair in status.Bonuses)
                    StatusOffsets[pair.Key] += pair.Value;
            }
            
        }
        
        public StatusEffect? GetStatus(string key) => null;

        public bool CanAct() => !IsKnockedOut && !IsFrozen && !IsStunned;
        public bool CanCast() => !IsSilenced;
        
        //Status helpers
        public bool IsFrozen => GetStatus("frozen") != null;
        public bool IsStunned => GetStatus("stunned") != null;
        public bool IsSilenced => GetStatus("silenced") != null;
        public bool IsBlinded => GetStatus("blinded") != null;
        
        
        
        
        
        //Action Logic, 
        public bool ReactToEvent(string eventKey, BattleRound currentEvent, out QueuedAction action)
        {
            action = new QueuedAction();
            
            return false;
        }
    }
}