namespace Items
{
    public abstract class Item
    {
        public string Name;
        public string Description;

        public int Size = 1; //weight of the item when given to a unit
        public int Value = 100; //Base cost of the item, modified by various factors
        public virtual bool IsStackable => true;
    }
}