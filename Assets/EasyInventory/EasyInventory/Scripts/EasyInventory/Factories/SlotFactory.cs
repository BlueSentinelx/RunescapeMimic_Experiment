/* Factory class for creating slots.
 * Author: Corey St-Jacques
 * Date: May 24, 2017
 */

using EasyInventory.Repository;

namespace EasyInventory.Factories
{
    /// <summary>
    /// This class creates slots.
    /// </summary>
    public static class SlotFactory
    {
        /// <summary>
        /// Creates a generic slot.
        /// </summary>
        /// <returns>Returns the newly created slot object.</returns>
        public static Slot CreateSlot()
        {
            Slot tmpSlot = null;
            tmpSlot = new Slot();
            return tmpSlot;
        }

        /// <summary>
        /// Creates a slot using an item.
        /// </summary>
        /// <param name="item">The item you would like to fill the slot with.</param>
        /// <returns>Returns the newly created slot object.</returns>
        public static Slot CreateSlotUsingItem(Item item)
        {
            Slot tmpSlot = null;
            tmpSlot = new Slot(item);
            return tmpSlot;
        }
    }
}
