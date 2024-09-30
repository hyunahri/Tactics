using System.Collections.Generic;
using System.Diagnostics;

namespace Characters.RapportEffectScripts
{
    /// <summary>
    /// Scripted effects for particular rapport events that need more complicated processing than the ability effects can handle.
    /// </summary>
    /// 
    public class RapportScriptedEffect
    {
        public virtual void Process(List<Character> characters)
        {
            return;
        }
    }
}