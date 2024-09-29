using System.Collections.Generic;
using System.Linq;
using System.Text;
using Abilities;
using Characters;
using Units;

namespace Combat
{
    /*
     Combat Rules
     . We create a Queue of Initiatives at the start of combat
     . Whenever a character uses a Primary action, they are sent to the end of the initiative queue
        . Non-Primary actions do NOT affect initiative

     . Combat starts with a combat start event
     . Combat ends with a combat end event

     . When a character can act due to Initiative, we query their Primary slotted skills in order and use the first to have a valid target.
     */


    public class CombatManager
    {
        //private static int BonusInitiativeForAttacker = 10;

        public CombatManager(Unit attacker, Unit defender)
        {
            CurrentBattle = new Battle(attacker, defender);
        }

        public Battle CurrentBattle { get; private set; }

        public void BuildBattleSequence()
        {
            StringBuilder clog = new StringBuilder();
            List<BattleRound> sequence = new List<BattleRound>();

            int step = 0;
            int stepLimit = 1000;

            int roundsSinceLastAction = 0;
            int maxRoundsSinceLastAction = CurrentBattle.AllCharacters.Count * 2;

            var startingEvent = new BattleRound();
            startingEvent.Action = null;

            var turnQueue = GenerateTurnOrder(CurrentBattle.AllCharacters.AsReadOnly());
            
            foreach (var character in CurrentBattle.AllCharacters)
                startingEvent.States.Add(character, new CharacterBattleAlias(character));

            sequence.Add(startingEvent);
            
            //A breath before we have to do recursive bullshit
            ////  （＾人＾）

            //First we launch a combat start event
            RunEventRound("combat_start");

            
            
            //And, now, recursive bullshit time
            while (!IsCombatOver())
            {
                //Get the next character to act
                var nextCharacter = GetNextPrimaryActor(GenerateTurnOrder(CurrentBattle.AllCharacters));

                //Get the next action
                var nextAction = nextCharacter.GetNextPrimaryAction();

                //If the action is null, the character is out of actions
                if (nextAction == null)
                {
                    roundsSinceLastAction++;
                    continue;
                }

                //If the action is not null, we reset the rounds since last action
                roundsSinceLastAction = 0;

                //Append the action to the sequence
                AppendEvent(new BattleRound(CurrentBattle, nextAction, sequence[step]));

                //Apply the action
                ApplyAction(nextAction);
            }

            //Finish off with a combat end event
            RunEventRound("combat_end");

            return;

            BattleRound E() => sequence[step];

            void RunEventRound(string eventKey)
            {
                clog.AppendLine(eventKey);
                List<QueuedAction> actions = new List<QueuedAction>();
                foreach (var character in E().States)
                {
                    if (character.Value.ReactToEvent(eventKey, E(), out var action))
                        actions.Add(action);
                }

                //sort the actions
                actions = OrderActions(actions);


                //generate an action chain from each queued action
            }


            bool IsCombatOver() =>
                step > stepLimit //limit the number of steps to prevent infinite loops
                || roundsSinceLastAction > maxRoundsSinceLastAction
                || CurrentBattle.AllCharacters.All(x => x.IsKnockedOut)
                || (CurrentBattle.AllCharacters.All(x => x.GetStat("ap") == 0) && CurrentBattle.AllCharacters.All(x => x.GetStat("pp") == 0)
                );

            void AppendEvent(BattleRound e)
            {
                sequence.Add(e);
                step++;
            }

            ICharacter GetNextPrimaryActor(Queue<ICharacter> queue)
            {
                var next = queue.Dequeue();
                queue.Enqueue(next);
                return next;
            }

            Queue<ICharacter> GenerateTurnOrder(IReadOnlyCollection<ICharacter> set) =>
                set.Count == 1 ?
                    new Queue<ICharacter>(set) :
                    new Queue<ICharacter>(set.OrderBy(x => x, new CharacterComparer()));


            List<QueuedAction> OrderActions(List<QueuedAction> acts)
            {
                //var rng = new System.Random(E().Seed);
                if (acts.Count == 1)
                    return acts;
                //TODO convert to using CharacterComparer
                //return acts.OrderByDescending(x => x.user.GetStat(priority)).ToList(); //todo handle ties
            }


            List<QueuedAction> BuildActionChainRecursive(QueuedAction rootAction)
            {
                // build event
                BattleRound e = new BattleRound(CurrentBattle, rootAction, E());

                //apply action
                //probably use action.trytoexecute, we'll see.

                //TODO
                return new List<QueuedAction>();
            }

            void ApplyAction(QueuedAction act)
            {
                //apply action
                List<string> preEvents = new List<string>(); //events that happen before the action
                List<string> postEvents = new List<string>(); //events that happen after the action

                List<QueuedAction> preActions = new List<QueuedAction>();
                foreach (var character in E().States)
                {
                    if (character.Value.ReactToEvent("pre_action", E(), out var pact))
                        preActions.Add(pact);
                }


                //check for reactions
                foreach (var character in E().States)
                {
                    if (character.Value.ReactToEvent("action", E(), out var react))
                        actions.Add(react);
                }
            }
        }
    }
}