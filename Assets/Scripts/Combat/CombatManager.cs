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

            var startingEvent = new BattleRound(CurrentBattle, null);
            CurrentBattle.CombatSequence.Add(startingEvent);
            
            var turnQueue = GenerateTurnOrder(R().States.Values);
            
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
                var _nextCharacter = GetNextPrimaryActor(turnQueue);

                //Get the next action
                var _nextAction = _nextCharacter.GetNextPrimaryAction(R());

                //If the action is null, the character is skipped
                if (_nextAction == null)
                {
                    roundsSinceLastAction++;
                    continue;
                }

                //If the action is not null, we reset the rounds since last action
                roundsSinceLastAction = 0;

                //Append the action to the sequence
                AppendEvent(new BattleRound(CurrentBattle, _nextAction, sequence[step]));
                RunActionRound();
            }

            //Finish off with a combat end event
            RunEventRound("combat_end");

            return;

            BattleRound R() => sequence[step];

            void RunEventRound(string eventKey)
            {
                clog.AppendLine(eventKey);
                List<QueuedAction> actions = new List<QueuedAction>();
                foreach (var character in R().States)
                {
                    if (character.Value.ReactToEvent(eventKey, R(), out var action))
                        actions.Add(action);
                }

                //sort the actions
                //actions = OrderActions(actions);


                //generate an action chain from each queued action
            }
            
            void RunActionRound()
            {
                var r = R();
                var seq = r.Actions;
                var root = r.RootAction;
                
                var sameSide = r.States.Values.Where(x => x.Character.GetRootCharacter().Unit == r.RootAction.user.GetRootCharacter().Unit).OrderBy(x => x, new CharacterComparer());
                var otherSide = r.States.Values.Where(x => x.Character.GetRootCharacter().Unit != r.RootAction.user.GetRootCharacter().Unit).OrderBy(x => x, new CharacterComparer());
                
                //State hasn't changed at the time of preparations, so for these we can just pick one prep from each side
                
                //same side prepares
                QueuedAction? sameSidePrep = null;
                foreach (var alias in sameSide)
                {
                    sameSidePrep = alias.GetPreparation(r);
                    if (sameSidePrep == null) continue;
                    //add to r.Actions before root
                    seq.AddBefore(seq.Find(root), sameSidePrep);
                    break;
                }
                
                
                //other side prepares
                QueuedAction? otherSidePrep = null;
                foreach (var alias in otherSide)
                {
                    otherSidePrep = alias.GetPreparation(r);
                    if (otherSidePrep == null) continue;
                    //add to r.Actions before root, but after same side prep
                    seq.AddBefore(seq.Find(root), otherSidePrep);
                    break;
                }
                
                //user executes - STATE CHANGE
                
                //target reacts - For each of the following, we should get a set of possible actions sorted by priority and execute the first option which is valid in the new state.
                
                //other side reacts
                
                //same side reacts
                
                //Build the action chain
            }


            bool IsCombatOver() =>
                step > stepLimit //limit the number of steps to prevent infinite loops
                || roundsSinceLastAction > maxRoundsSinceLastAction
                || CurrentBattle.AllCharacters.All(x => x.IsKnockedOut)
                //|| (CurrentBattle.AllCharacters.All(x => x.GetStat("ap") == 0) && CurrentBattle.AllCharacters.All(x => x.GetStat("pp") == 0))
                ;

            void AppendEvent(BattleRound e)
            {
                sequence.Add(e);
                step++;
            }

            CharacterBattleAlias GetNextPrimaryActor(Queue<CharacterBattleAlias> queue)
            {
                var next = queue.Dequeue();
                queue.Enqueue(next);
                return next;
            }

            Queue<CharacterBattleAlias> GenerateTurnOrder(IReadOnlyCollection<CharacterBattleAlias> set) =>
                set.Count == 1 ?
                    new Queue<CharacterBattleAlias>(set) :
                    new Queue<CharacterBattleAlias>(set.OrderBy(x => x, new CharacterComparer()));
            
            List<QueuedAction> BuildActionChainRecursive(QueuedAction rootAction)
            {
                // build event
                BattleRound e = new BattleRound(CurrentBattle, rootAction, R());

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
                foreach (var character in R().States)
                {
                    if (character.Value.ReactToEvent("pre_action", R(), out var pact))
                        preActions.Add(pact);
                }


                //check for reactions
                foreach (var character in R().States)
                {
                    //if (character.Value.ReactToEvent("action", R(), out var react))
                    //    actions.Add(react);
                }
            }
        }
    }
}