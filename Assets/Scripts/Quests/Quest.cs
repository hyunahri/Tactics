using System.Collections.Generic;

namespace Quests
{
    public class Quest
    {
        public string QuestName; //Generate via markov chain
        public LinkedList<QuestStage> Stages { get; set; } = new LinkedList<QuestStage>();
        
        public List<QuestReward> Rewards { get; set; } = new List<QuestReward>();
        //Categorization
        
    }
    
    public enum QuestTypes
    {
        Story,
        Character, 
        Side, //Manually created
        Radiant, //Randomly generated
    }

    //Used to control the generation of radiant quests
    public enum QuestCategories
    {
        Exploration,
        Gathering,
        Battle,
        Escort,
        
    }
}