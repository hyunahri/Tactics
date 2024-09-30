using System;
using CoreLib.Complex_Types;
using Units;


namespace Characters
{
    /// <summary>
    /// Core class for individual characters. Only modify this outside of combat, when in combat you should be creating CharacterBattleAliases.
    /// At the end of combat, apply the final CharacterBattleAlias to the Character.
    /// </summary>
    [System.Serializable]
    public sealed class Character : ICharacter
    {
        public Character(Class @class)
        {
            if(@class == null)
                throw new ArgumentNullException(nameof(@class));
            ExperienceManager = new CharacterExperienceManager(this);
            StatsManager = new CharacterStatsManager(this);
            EquipmentManager = new CharacterEquipmentManager(this);
            EquipmentManager.OnEquipmentChanged.AddListener(StatsManager.RebuildEquipmentOffsets);
            SetClass(@class);
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
        public int Level => StatsManager.GetStat("level");

        //
        private int currentHP { get; set; }
        public int CurrentHP  //You should usually be accessing this through DamageProcessor
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
        
        public Character GetRootCharacter() => this;
        
        //ICharacter 
        public void AddXP(int amount) => ExperienceManager.AddExperience(amount);  //Let DamageProcessor handle this in combat
        public void Heal(int amount, bool overheal = false) => CurrentHP = overheal ? CurrentHP + amount : Math.Min(MaxHP, CurrentHP + amount);
        public void TakeDamage(int amount, bool overkill = true) => CurrentHP = overkill ? CurrentHP - amount : Math.Max(0, CurrentHP - amount);

        //Class
        private void SetClass(Class @class)
        {
            class_ = @class;
            StatsManager.RebuildClassStats();
        }
        private Class class_;
        public Class Class => class_;
        
        //Experience
        public CharacterExperienceManager ExperienceManager;
        
        //Equipment
        public CharacterEquipmentManager EquipmentManager;
        
        //Stats
        public CharacterStatsManager StatsManager;
        
        //Abilities
        public CharacterAbilityManager AbilityManager;
        
        //On Modify

        
        
    }
}
