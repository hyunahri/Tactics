using System.Collections.Generic;

namespace Quests
{
    public class QuestStage
    {
        public List<string> SeedPhrases; //Used for MarkovChain
        public List<QuestReward> StageRewards;
        //todo Quest failure penalty system? 
        
        public void OnComplete(bool success)
        {
            if (!success) return;
            foreach (var reward in StageRewards)
                reward.GiveReward();
        }
    }
}