namespace Abilities
{
    public enum AbilityCoreTypes
    {
        ACTIVE,
        REACTIVE,
        PASSIVE,
    }
    
    public enum AbilityTypes
    {
        Attack,
        Buff,
        Debuff,
        Heal,
        Standby
    }

    public enum Scope
    {
        SELF,
        ALLY,
        ENEMY,
        ANY
    }

    public enum ActionTypes
    {
        Preparation,
        Primary,
        Reaction
    }
    
    public enum AbilityTags
    {
        BlockPreActions,
        BlockPostActions,
        Offensive,
        Defensive,
        Physical,
        Magic,
        Healing,
    }
} 