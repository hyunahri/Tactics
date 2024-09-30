using System;
using System.Collections.Generic;
using Characters;
using CoreLib;

namespace Units
{
    /// <summary>
    /// A group of characters arranged in a Formation.
    /// The Unit is the basic building block of combat and the smallest movable entity on the tactical map.
    /// Combat will always be between two units.
    /// </summary>
    public class Unit
    {
        public Unit()
        {
            CharactersInFormation = new Character?[2,3];
        }
        
        public Character?[,] CharactersInFormation = new Character?[2,3];
        public List<Character> GetRow(int i) => new List<Character> {CharactersInFormation[i, 0], CharactersInFormation[i, 1], CharactersInFormation[i, 2]};
        public int GetRow(Character c) => c == CharactersInFormation[0, 0] || c == CharactersInFormation[0, 1] || c == CharactersInFormation[0, 2] ? 0 : 1;
        
        //Generate a seed for every enemy unit that shows up on the tactical map. Reroll after attacking that enemy or using a skill against it.
        public Dictionary<Unit, int> Seeds = new Dictionary<Unit, int>();
        public int GenerateSeed() => RNG.rng.Next(0, Int32.MaxValue);
    }


}