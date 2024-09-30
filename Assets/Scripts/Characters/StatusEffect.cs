using CoreLib.Complex_Types;

namespace Characters
{
    //Mostly placeholder for now. Intent is to use this for in-combat buffs and debuffs which can be applied and removed dynamically. Low priority for now.
    public abstract class StatusEffect
    {
        public string StatusName { get; }
        public abstract bool CheckValidity(ICharacter state);
        public DefaultDict<string, int> Bonuses { get; }
    }
}