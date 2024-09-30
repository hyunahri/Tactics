using System.Collections.Generic;
using UnityEngine;
using World;

namespace Characters
{
    /// <summary>
    /// Describes a Rapport Event.
    /// Rapport Events take place between two or more characters at a specified location once certain conditions are met and the player physically travels to the location.
    /// Generally these events should be ordered in chains and require the completion of previous events to unlock, along with a given rapport value between the characters.
    /// Rapport events can add/remove items, bonuses, abilities and make other permanent changes to characters and world state
    /// Consider this the equivalent of 'story' events and cutscenes in other games.
    /// 
    /// Rapport events will need to be tied into the dialogue database later on. 
    /// </summary>
    public class RapportEventTemplate
    {
        /// <summary> Must be unique </summary>
        public string RapportEventKey;

        //Location of the Rapport Event.
        public LocationPointer Location;
        
        [Header("Characters")]
        public List<string> CharacterKeys = new List<string>(); //Characters participating in the event
        
        [Space]
        [Header("Conditions")]
        public List<RapportEventCondition> Conditions;
        public bool EligibleForRapportEvent() => Conditions.TrueForAll(x => x.EligibleForRapportEvent());
        
        
        [Space]
        [Header("Scripted Effects")]
        public List<RapportEffectScripts.RapportScriptedEffect> ScriptedEffects = new List<RapportEffectScripts.RapportScriptedEffect>();
    }
}