using System.Collections.Generic;
using System.Text;
using Characters;
using CoreLib;
using Units;

namespace Combat
{
    public class BattleRound
    {
        public BattleRound(int i = 0) => Index = i;

        public BattleRound(Battle battle, QueuedAction? action, BattleRound? prior = null)
        {
            Index = prior is null ? 0 : prior.Index + 1;
            Battle = battle;
            
            RootAction = action;
            Actions = new LinkedList<QueuedAction>();
            if (RootAction != null)
                Actions.AddFirst(RootAction);
            
            States = new Dictionary<Character, CharacterBattleAlias>();
            UnitStates = new Dictionary<Unit, UnitState>();

            if (prior == null) BuildFromBattle(battle);
            else BuildFromPrior(prior);
        }

        private void BuildFromBattle(Battle battle)
        {
            //Create clones of characters
            foreach (var character in battle.AllCharacters)
                States.Add(character, new CharacterBattleAlias(character));
            
            //Create clones of units
            foreach (var unit in new []{battle.AttackerUnit, battle.DefenderUnit})
                UnitStates.Add(unit, new UnitState(unit, null));
        }
        
        private void BuildFromPrior(BattleRound prior)
        {
            //Clone prior character states 
            foreach (var pair in prior.States) 
                States.Add(pair.Key, new CharacterBattleAlias(pair.Value));
            
            //Clone prior unit states
            foreach (var pair in prior.UnitStates) 
                UnitStates.Add(pair.Key, new UnitState(pair.Key, pair.Value));
        }


        
        //Core
        public readonly int Index;
        public readonly Battle Battle;
        public readonly int Seed = RNG.rng.Next();
        
        //Actions
        public readonly QueuedAction RootAction; //Primary action taken during this round
        public readonly LinkedList<QueuedAction> Actions; //All actions taken during this round, including Root, in order of execution
        
        //State at end of round
        public readonly Dictionary<Character, CharacterBattleAlias> States; //State of each character at the conclusion of round
        public readonly Dictionary<Unit, UnitState> UnitStates; //State of each unit at the conclusion of round
        
        //Logging
        public readonly StringBuilder RoundLog = new StringBuilder();
    }
}