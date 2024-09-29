using System.Collections.Generic;
using System.Linq;

namespace CoreLib.Utilities
{
    public class MarkovChain<T>
    {
        private Dictionary<T, Dictionary<T, int>> chain;
        private System.Random random => RNG.rng;

        public MarkovChain(List<List<T>> data)
        {
            chain = BuildMarkovChain(data);
        }
    

        private Dictionary<T, Dictionary<T, int>> BuildMarkovChain(List<List<T>> data)
        {
            Dictionary<T, Dictionary<T, int>> newChain = new Dictionary<T, Dictionary<T, int>>();
            foreach (List<T> sequence in data)
            {
                for (int i = 0; i < sequence.Count - 1; i++)
                {
                    T currentElement = sequence[i];
                    T nextElement = sequence[i + 1];
                    if (!newChain.ContainsKey(currentElement))
                    {
                        newChain[currentElement] = new Dictionary<T, int>();
                    }
                    if (!newChain[currentElement].ContainsKey(nextElement))
                    {
                        newChain[currentElement][nextElement] = 0;
                    }
                    newChain[currentElement][nextElement]++;
                }
            }
            return newChain;
        }

        public List<T> GenerateSequence(int maxLength)
        {
            List<T> sequence = new List<T>();
            T currentElement = chain.Keys.ElementAt(random.Next(chain.Count));
            sequence.Add(currentElement);
            while (sequence.Count < maxLength && chain.ContainsKey(currentElement))
            {
                List<T> possibleNextElements = new List<T>(chain[currentElement].Keys);
                T nextElement = possibleNextElements[random.Next(possibleNextElements.Count)];
                sequence.Add(nextElement);
                currentElement = nextElement;
            }
            return sequence;
        }
    }
}
