using System.Collections.Generic;
using Characters;

namespace Units
{
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