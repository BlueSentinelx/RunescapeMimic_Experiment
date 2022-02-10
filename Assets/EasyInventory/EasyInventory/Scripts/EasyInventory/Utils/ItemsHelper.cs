/* This is a helper class for managing items.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */


using EasyInventory.Repository;


namespace EasyInventory.Utils
{
    /// <summary>
    /// This is a helper class for managing items.
    /// </summary>
    public static class ItemsHelper
    {
        /// <summary>
        /// Creates a deep copy of the current Item array.
        /// </summary>
        /// <param name="items">The Items to copy.</param>
        /// <returns>Returns a deep copy of the current Items.</returns>
        public static Item[] CloneItems(this Item[] items)
        {
            Item[] tmpItems = new Item[items.Length];
            for (int i = 0; i < tmpItems.Length; i++)
            {
                tmpItems.SetValue(items[i].Clone(), i);
            }
            return tmpItems;
        }
    }
}
