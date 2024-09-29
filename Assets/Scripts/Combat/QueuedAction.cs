using Abilities;
using Characters;

namespace Combat
{
    public class QueuedAction
    {
        public QueuedAction(CharacterBattleAlias user, CharacterBattleAlias target, Ability ability)
        {
            this.user = user;
            this.target = target;
            this.ability = ability;
        }
        
        public CharacterBattleAlias user;
        public CharacterBattleAlias target;
        
        public CharacterBattleAlias? forcedTarget; //Overrides target if not null, used by abilities that force or alter targets like ally guard.
        public CharacterBattleAlias GetTarget() => forcedTarget ?? target;
        
        public Ability ability;
    }
}