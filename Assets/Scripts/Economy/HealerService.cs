using System.Linq;
using Characters;
using Units;

namespace Economy
{
    /// <summary>
    /// Quick sample of a service that heals all the members of a Unit.
    /// </summary>
    public class HealerService : TransactableService, IService<Unit>
    {
        bool IService.EligibleForService(object input) => EligibleForService((Unit)input);
        void IService.PerformService(object input) => PerformService((Unit)input);
        
        public bool EligibleForService(Unit input) =>
            input.GetICharacters() is { Count: > 0 } chars && chars.Any(x => x.HP < x.GetStat("maxHP"));
        
        public void PerformService(Unit input)
        {
            foreach (ICharacter character in input.GetICharacters()) 
                character.Heal(9999);
        }
    }
}