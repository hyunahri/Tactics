namespace Items
{
    public abstract class Item
    {
        public string Name;
        public string Description;

        public int Size = 1;
        public int Value = 100;
        public virtual bool IsStackable => true;
    }
}