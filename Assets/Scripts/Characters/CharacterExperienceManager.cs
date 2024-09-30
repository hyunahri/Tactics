using Game;
using UnityEngine;

namespace Characters
{
    /// <summary>
    /// Quick manager for handling character experience and leveling. Will work on later.
    /// </summary>
    public class CharacterExperienceManager
    {
        public CharacterExperienceManager(Character character) => this.character = character;

        private Character character;
        
        public int Level { get; set; } = 1;
        public int Experience = 0;
        public int ExperienceToNextLevel = 100;
        
        
        private int ApplyExperienceModifier(int experience)
        { //Base modifier of 1, the stat is an integer representing a percent.
            float multiplier = 1f + character.StatsManager.GetStat("xp") / 100f;
            return (int) (experience * multiplier);
        }
        
        public void AddExperience(int amount)
        {
            int modifiedExperience = ApplyExperienceModifier(amount);
            //TODO we'll create a log for the characters so the player can see this.
            Debug.Log($"{character.GetName()} gained {amount} * {100 + character.StatsManager.GetStat("xp")}% = {modifiedExperience} experience."); 
            Experience += modifiedExperience;
            
            while (Experience >= ExperienceToNextLevel) 
                LevelUp();
            character.StatsManager.RebuildClassStats(); //Rebuild stats after leveling.
        }

        public void LevelUp()
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            ExperienceToNextLevel = Formulas.ExperienceToNextLevel(Level + 1);
            
            if(Experience < 0) //In case we directly increased the level without adding experience.
                Experience = 0;
        }
    }
}