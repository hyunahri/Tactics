using System;
using System.Collections.Generic;
using Characters;
using CoreLib;

namespace Units
{
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

    //<summary> A unit at a given point in time during combat.
    // Used mainly to manage changes to the formation that occur during combat.</summary>
    public class UnitState : Unit
    {
        public UnitState(Unit u, UnitState? prior)
        {
            Root = u;
            Prior = prior;
            
            //Setup character array
            var template = prior != null ? prior.CharactersInFormation : u.CharactersInFormation;
            CharactersInFormation = new Character?[template.GetLength(0), template.GetLength(1)];
            for (var i = 0; i < template.GetLength(0); i++)
            {
                for (var j = 0; j < template.GetLength(1); j++)
                {
                    CharactersInFormation[i, j] = template[i, j];
                }
            }
            
            foreach (var c in CharactersInFormation)
            {
                if (c is null)
                    continue;
                Characters.Add(c);
            }
        }

        public Unit Root;
        public UnitState? Prior;
        public bool IsFirstState => Prior is null;
        
        public List<Character> Characters = new List<Character>();
    }
}