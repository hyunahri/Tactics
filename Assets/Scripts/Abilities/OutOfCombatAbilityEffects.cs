using Characters;
using Combat;
using UnityEngine;

namespace Abilities
{
    /// <summary> Special effects for permanently changing characters.
    /// Mainly intended for use with consumables. </summary>
    
    //EXPERIENCE
    
    [System.Serializable]
    public class AddExperienceToCharacterFlat : AbilityEffect
    {
        [SerializeField] public int experience;
        
        //Description
        public override string GetDescription() => $"Adds {experience} experience to the target character.";
        public override string GetDescriptionForUser(ICharacter user) => $"Adds {experience} experience to {user.GetName()}.";
        
        //Application
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => target.GetRootCharacter().ExperienceManager.AddExperience(experience);
    }
    
    public class AddExperienceScaled : AbilityEffect
    {
        [SerializeField] public int Experience;
        [SerializeField] public int LevelCutoff;
        [SerializeField] public int MultiplierUnderCutoff;
        
        //Description
        public override string GetDescription() => $"Adds {Experience} experience to the target character, or {MultiplierUnderCutoff}x that if the character is level {LevelCutoff} or lower.";
        public override string GetDescriptionForUser(ICharacter user)
        {
            int level = user.GetRootCharacter().Level;
            if(level <= LevelCutoff)
                return $"Adds {Experience * MultiplierUnderCutoff} experience to {user.GetName()}.";
            else
                return $"Adds {Experience} experience to {user.GetName()}.";
        }

        //Application
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target) => target.GetRootCharacter().ExperienceManager.AddExperience(Experience);
    }
    
    [System.Serializable]
    public class LevelUpCharacter : AbilityEffect
    {
        public int NumberOfLevels = 1;
        
        //Description
        public override string GetDescription() => $"Advances the target character's level by {NumberOfLevels}.";
        public override string GetDescriptionForUser(ICharacter user) => $"Advances {user.GetName()} by {NumberOfLevels} levels.";
        
        //Application
        public override void ApplyEffect(BattleRound e, ICharacter user, ICharacter target)
        {
            int experience = target.GetRootCharacter().ExperienceManager.Experience;
            for (int i = 0; i < NumberOfLevels; i++)
                target.GetRootCharacter().ExperienceManager.LevelUp();
            target.GetRootCharacter().ExperienceManager.Experience = experience; //Restore experience
        }
    }
    
}