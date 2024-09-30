using System.Collections.Generic;
using System.Linq;
using Characters;
using Units;

namespace Combat
{
    //<summary> A battle between two units
    // This class is a container for the various states and includes helper functionality for managing characters.
    // Actual combat logic is handled by the CombatManager class.
    //</summary>
    public class Battle
    {
        public Battle(Unit attackerUnit, Unit defenderUnit)
        {
            AttackerUnit = attackerUnit;
            DefenderUnit = defenderUnit;
            foreach (Character? c in attackerUnit.GetICharacters())
            {
                if(c is null)
                    continue;
                AllCharacters.Add(c);
                AttackerCharacters.Add(c);
            }
            foreach (Character? c in defenderUnit.GetICharacters())
            {
                if(c is null)
                    continue;
                AllCharacters.Add(c);
                DefenderCharacters.Add(c);
            }
            RollInitiative();
        }
        
        public System.Random rng = new System.Random(100);
        
        public Unit AttackerUnit;
        public Unit DefenderUnit;
        
        public List<Character> AllCharacters = new List<Character>();
        
        public HashSet<Character> AttackerCharacters = new HashSet<Character>();
        public HashSet<Character> DefenderCharacters = new HashSet<Character>();
        
        public Dictionary<Character, int> InitiativeOrder = new Dictionary<Character, int>();
        public List<BattleRound> CombatSequence = new List<BattleRound>();
        
        public bool IsAttacker(Character character) => AttackerCharacters.Contains(character);
        public bool IsDefender(Character character) => DefenderCharacters.Contains(character);
        
        public Unit GetOwnUnit(ICharacter character) => AttackerCharacters.Contains(character.GetRootCharacter()) ? AttackerUnit : DefenderUnit;
        public Unit GetEnemyUnit(ICharacter character) => AttackerCharacters.Contains(character.GetRootCharacter()) ? DefenderUnit : AttackerUnit;
        
        private void RollInitiative()
        {
            var initiatives = new Dictionary<Character, int>();
            foreach (var character in AllCharacters)
            {
                initiatives.Add(character, character.StatsManager.GetStat("initiative"));
            }
        }
        
        
        
    }

    //<summary> A state of the battle at a given point in time</summary>
}