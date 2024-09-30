using System.Collections.Generic;
using System.Linq;
using CoreLib.Complex_Types;
using Items;

namespace Characters
{
    /// <summary>
    ///  Manages the equipment of a character, including which equipment they have equipped and the order of that equipment.
    /// </summary>
    public class CharacterEquipmentManager
    {
        public CharacterEquipmentManager(Character character)
        {
            this.character = character;
        }
        
        private Character character;
        public CoreEvent OnEquipmentChanged = new CoreEvent();
        
        
        //Equipment
        private List<EquipmentSlot> Slots = new List<EquipmentSlot>() {new EquipmentSlot(EquipmentSlotTypes.WEAPON)};
        public IReadOnlyList<EquipmentSlot> GetSlots() => Slots.AsReadOnly();
        private void SortSlots() => Slots.OrderBy(x => (int)x.SlotType + x.OrderOffset);
        
        public void AddSlot(EquipmentSlotTypes slotType)
        {
            Slots.Add(new EquipmentSlot(slotType));
            SortSlots();
        }
        
        public bool TryEquip(Equipment equipment)
        {
            if (equipment.IsEquipped) //Remove if already equipped by someone else
                if (!equipment.EquippedCharacter.EquipmentManager.TryRemoveEquip(equipment))
                    return false;
            
            var slot = Slots.FirstOrDefault(x => x.SlotType == equipment.Data.Slot && x.Equipment == null);
            if (slot == null) return false;
            
            slot.Equipment = equipment;
            equipment.EquippedCharacter = character;
            OnEquipmentChanged.Invoke();
            return true;
        }

        public bool TryRemoveEquip(Equipment equipment)
        {
            var slot = Slots.FirstOrDefault(x => x.Equipment == equipment);
            if (slot == null)
            {
                if (equipment.IsEquipped && equipment.EquippedCharacter == character)
                {
                    equipment.EquippedCharacter = null;
                    return false;
                }
                else
                    return false;
            }

            slot.Equipment = null;
            equipment.EquippedCharacter = null;
            OnEquipmentChanged.Invoke();
            return true;
        }
    }
}