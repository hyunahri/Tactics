using System.Collections.Generic;
using Characters;
using Game;

namespace Units
{
    //<summary> A unit at a given point in time during combat.
    //Only exists during combat and only needed if we implement abilities that can change the formation during combat.
    //</summary>
    public class UnitState : Unit
    {
        public UnitState(Unit u, UnitState? prior)
        {
            Root = u;
            Prior = prior;
            
            //Clone the formation
            var templateDict = u.GetReadonlyFormation();
            CharactersInFormation = new Character?[Globals.UnitColumns, Globals.UnitRows];
            foreach (var kvp in templateDict)
            {
                CharactersInFormation[kvp.Key.x, kvp.Key.y] = kvp.Value;
                if(kvp.Value is null)
                    continue;
                Characters.Add(kvp.Value);
            }
        }

        public Unit Root;
        public UnitState? Prior;
        public bool IsFirstState => Prior is null;
        
        public List<ICharacter> Characters = new List<ICharacter>();
    }
}