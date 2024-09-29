using CoreLib;

namespace Items
{
    public class EquipmentSlot
    {
        public EquipmentSlot(EquipmentSlotTypes slotType)
        {
            OrderOffset = RNG.rng.Next(1, 50);
            SlotType = slotType;
        }
        
        public EquipmentSlotTypes SlotType { get; set; }
        public Equipment? Equipment { get; set; }
        public bool IsEmpty => Equipment == null;

        public int OrderOffset;
    }
}