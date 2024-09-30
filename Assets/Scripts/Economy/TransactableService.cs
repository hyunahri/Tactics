using Game;

namespace Economy
{
    /// <summary>
    /// Wrapper for services that can be performed.
    /// </summary>
    public abstract class TransactableService : ITransactable
    {
        public string Name { get; set; } = "Service";
        public string Description { get; set; } = "A service that can be performed.";
        public int Value { get; set; } = 100;
        public string Category { get; set; } = "Service";
        
        IService Service;
        public virtual bool PerformService(object input)
        {
            //TODO check for money and take some and retrn false if not enough
            Service.PerformService(input);
            return true;
        }
    }
}