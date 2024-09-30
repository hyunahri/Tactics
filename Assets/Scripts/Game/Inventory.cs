using System;
using System.Collections.Generic;
using System.Linq;
using CoreLib.Complex_Types;
using Items;
using Units;

namespace Game
{
    public class PlayerInventory
    {
        private DefaultDict<string, HashSet<Item>> Items = new(() => new HashSet<Item>(), StringComparer.OrdinalIgnoreCase);
        public IReadOnlyList<Item> GetAllItems() => Items.Values.SelectMany(x => x).ToList();
        public IReadOnlyList<Item> GetAllItems(Func<Item, bool> predicate) => Items.Values.SelectMany(x => x).Where(predicate).ToList();
        
        public bool HasSpecificItem(Item item) => Items[item.Key].Contains(item);
        public bool HasAnyItem(string key) => Items[key].Count > 0;
        public bool HasItems(string key, int count) => Items[key].Count >= count;
        public bool HasItems(ICollection<Item> items) => items.All(HasSpecificItem);
        
        public void AddItem(Item item) => Items[item.Key].Add(item);
    }

    public class UnitInventory
    {
        public UnitInventory(Unit unit)
        {
            this.unit = unit;
        }

        private readonly Unit unit;


        public int SizeLimit()
        { //TODO
            return 1;
        }
    }
}