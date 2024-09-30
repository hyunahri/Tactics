using System.Collections.Generic;
using Economy;
using Units;

namespace World
{
    public class Town
    {
        //Identity
        public string Name;
        
        //Economy
        public int ValuePerMonth; //Amount of gold given to owner each month.
        public List<TransactableService> Services { get; private set; } = new List<TransactableService>();

        
        //--------------------------------------------------
        
        //Example of how to add a service to a town
        //Just as an example
        public void AddHealerService()
        {
            Services ??= new List<TransactableService>();
            HealerService healerService = new HealerService
            {
                Name = "Todd's Herbal Remedies",
                Value = 11*11*11
            };
        }

        public void UseHealerService(Unit unit)
        {
            var  healerService = Services.Find(service => service is HealerService) as HealerService;
            healerService?.PerformService(unit);
        }
    }
}