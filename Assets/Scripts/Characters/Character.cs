using System;
using CoreLib.Complex_Types;
using Units;


namespace Characters
{

    public interface ICharacter
    {
        public string GetName();
        public int GetStat(string statName);
        public int Level { get; }
        public Character GetRootCharacter();
    }
    
    
    [System.Serializable]
    public sealed class Character : ICharacter
    {
        public Character(Class @class)
        {
            if(@class == null)
                throw new ArgumentNullException(nameof(@class));
            SetClass(@class);
            StatsManager = new CharacterStatsManager(this);
            EquipmentManager = new CharacterEquipmentManager(this);
            EquipmentManager.OnEquipmentChanged.AddListener(StatsManager.RebuildEquipmentOffsets);
        }
        
        public Unit Unit;
        
        //Events
        public CoreEvent OnChanged = new CoreEvent();
        public DefaultDict<string, CoreEvent<Character>> OnEvent = new DefaultDict<string, CoreEvent<Character>>(() => new CoreEvent<Character>(), StringComparer.OrdinalIgnoreCase);
        
        //
        public string? OverrideName;
        public string Name = "Default";
        public string GetName() => OverrideName ?? Name;

        public Gender Gender;
        
        public int GetStat(string statName) => StatsManager.GetStat(statName);
        
        //
        private int currentHP { get; set; }
        public int CurrentHP
        {
            get => currentHP;
            set
            {
                currentHP = value;
                OnChanged.Invoke();
            }
        }
        public int MaxHP => StatsManager.GetStat("hp");
        public bool IsKnockedOut => CurrentHP <= 0;
        public bool IsDead => MaxHP <= 0;
        
        //Leveling
        public int Level { get; set; } = 1;
        public Character GetRootCharacter() => this;

        public int Experience = 0;
        public int ExperienceToNextLevel = 100;
        
        //Class
        private void SetClass(Class @class)
        {
            class_ = @class;
            StatsManager.RebuildClassStats();
        }
        private Class class_;
        public Class Class => class_;
        //Equipment
        public CharacterEquipmentManager EquipmentManager;
        
        //Stats
        public CharacterStatsManager StatsManager;
        
        
        //On Modify

        
        
    }


    public abstract class StatusEffect
    {
        public string StatusName { get; }
        //public bool CheckValidity(CharacterCombatState state);
        public DefaultDict<string, int> Bonuses { get; }
    }
}
