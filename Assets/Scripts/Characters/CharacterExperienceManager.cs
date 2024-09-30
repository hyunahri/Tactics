using Game;

namespace Characters
{
    public class CharacterExperienceManager
    {
        public CharacterExperienceManager(Character character)
        {
            this.character = character;
        }
        
        private Character character;
        
        public int Level { get; set; } = 1;
        public int Experience = 0;
        public int ExperienceToNextLevel = 100;
        
        public void AddExperience(int amount)
        {
            Experience += amount;
            while (Experience >= ExperienceToNextLevel)
            {
                LevelUp();
            }
        }

        public void LevelUp()
        {
            Experience -= ExperienceToNextLevel;
            Level++;
            ExperienceToNextLevel = Formulas.ExperienceToNextLevel(Level + 1);
            
            if(Experience < 0) //In case we directly increased the level without adding experience.
                Experience = 0;
                
            
            character.StatsManager.RebuildClassStats(); //Rebuild stats after leveling.
        }
    }
}