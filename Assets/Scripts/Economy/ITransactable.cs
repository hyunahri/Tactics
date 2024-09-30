namespace Economy
{
    /// <summary>
    /// Base interface for all transactable objects, i.e. items, services, etc.
    /// </summary>
    public interface ITransactable
    {
        public string Name { get; }
        public string Description { get; }
        public int Value { get; }
        public string Category { get; }
    }
}