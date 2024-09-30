using CoreLib;

namespace Items
{
    /// <summary>
    /// Describes a slot on a character where equipment can be equipped.
    /// </summary>
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

        public int OrderOffset; //Used to determine the order of equipment slots in the UI for consistency, no functional impact.
    }
}