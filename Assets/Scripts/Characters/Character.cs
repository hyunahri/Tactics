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
        
        //Events
        public CoreEvent OnChanged = new CoreEvent();
        public DefaultDict<string, CoreEvent<Character>> OnEvent = new DefaultDict<string, CoreEvent<Character>>(() => new CoreEvent<Character>(), StringComparer.OrdinalIgnoreCase);
        
        //----------------
        
        public Unit? Unit;
        public bool IsAssignedToUnit => Unit != null;

        public string CharacterKey; //Only for unique characters
        public bool IsUnique => !string.IsNullOrEmpty(CharacterKey);
        
       
        
        //
        public string? OverrideName;
        public string Name = "Default";
        public string GetName() => OverrideName ?? Name;

        public Gender Gender;
        
        public int GetStat(string statName) => StatsManager.GetStat(statName);
        public int Level => StatsManager.GetStat("level");

        //
        private int hp { get; set; }
        public int HP  //You should usually be accessing this through DamageProcessor
        {
            get => hp;
            set
            {
                hp = value;
                OnChanged.Invoke();
            }
        }
        public int MaxHP => StatsManager.GetStat("hp");
        public bool IsKnockedOut => HP <= 0;
        public bool IsDead => MaxHP <= 0;
        
        public Character GetRootCharacter() => this;
        
        //ICharacter 
        public void AddXP(int amount) => ExperienceManager.AddExperience(amount);  //Let DamageProcessor handle this in combat
        public void Heal(int amount, bool overheal = false) => HP = overheal ? HP + amount : Math.Min(MaxHP, HP + amount);
        public void TakeDamage(int amount, bool overkill = true) => HP = overkill ? HP - amount : Math.Max(0, HP - amount);
        public Unit? GetUnit() => Unit;

        public void SetUnit(Unit? u)
        {
            Unit = u;
            OnChanged.Invoke();
        }

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
