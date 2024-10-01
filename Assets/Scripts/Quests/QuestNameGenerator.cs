using System.Collections.Generic;
using CoreLib.Utilities;

namespace Quests
{
    public class QuestNameGenerator
    {
        public QuestNameGenerator(List<QuestStage> stages, List<string> trainingData)
        {
            // Split each quest name into a list of words


            // Initialize the Markov Chain with the training data
            //markovChain = new MarkovChain<string>(trainingData);
        }
        
        private MarkovChain<string> markovChain;

    }
}