namespace Characters
{
    public interface ICharacter
    {
        public string GetName();
        public int GetStat(string statName);
        public int Level { get; }
        public Character GetRootCharacter();
        
        //Exp
        public void AddXP(int amount);
        public void Heal(int amount, bool overheal = false);
        public void TakeDamage(int amount, bool overkill = true);

    }
}