/* Factory class for generating items.
 * Author: Corey St-Jacques
 * Date: May 24, 2017
 */


using EasyInventory.Repository;

namespace EasyInventory.Factories
{
    /// <summary>
    /// This factory class creates items.
    /// </summary>
    public static class ItemFactory
    {
        /// <summary>
        /// Creates an item given a few formal parameters.
        /// </summary>
        /// <param name="itemId">The current item id.</param>
        /// <param name="amount">The item amount.</param>
        /// <param name="stackable">Should the item be stackable or not.</param>
        /// <returns>Returns the created item object.</returns>
        public static Item CreateItem(int itemId, int amount, bool stackable)
        {
            Item tmpItem = null;
            tmpItem = new Item(itemId, amount, stackable);
            return tmpItem;
        }
    }
}
