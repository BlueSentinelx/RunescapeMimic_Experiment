/* This is a helper class for managing slots.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Repository;


namespace EasyInventory.Utils
{
    /// <summary>
    /// This is a helper class for managing slots.
    /// </summary>
    public static class SlotsHelper
    {
        /// <summary>
        /// Creates a deep copy of the current slot array.
        /// </summary>
        /// <param name="slots">The slots to copy.</param>
        /// <returns>Returns a deep copy of the current slots.</returns>
        public static Slot[] CloneSots(this Slot[] slots)
        {
            Slot[] tmpSlots = new Slot[slots.Length];
            for (int i = 0; i < tmpSlots.Length; i++)
            {
                tmpSlots.SetValue(slots[i].Clone(), i);
            }
            return tmpSlots;
        }
    }
}
