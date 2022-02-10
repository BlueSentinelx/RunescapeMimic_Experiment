/* Item processor interface for managing items.
 * Author: Corey St-Jacques
 * Date: May 24, 2017
 */


using EasyInventory.Repository;
using EasyInventory.ServicesInterface;
using System;


namespace EasyInventory.Processors
{
    /// <summary>
    /// This interfaces sets all the methods used for manipulating inventory objects.
    /// </summary>
    public interface IItemProcessor
    {
        /// <summary>
        /// Adds an item into the provided inventory object.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="item">The item to add.</param>
        /// <returns>Returns true if the object has been added successfully.</returns>
        bool AddItem(InventoryInfo inventory, Item item);

        /// <summary>
        /// Adds items into the provided inventory object.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns true if the items have been added successfully.</returns>
        bool AddItems(InventoryInfo inventory, Item[] items);

        /// <summary>
        /// Removes an item into the provided inventory object.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="item">The item to remove.</param>
        /// <returns>Returns true if the object has been removed successfully.</returns>
        bool RemoveItem(InventoryInfo inventory, Item item);

        /// <summary>
        /// Removes an item into the provided inventory object.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="index">The index to remove from.</param>
        /// <returns>Returns true if the object has been removed successfully.</returns>
        bool RemoveItem(InventoryInfo inventory, Item item, int index);

        /// <summary>
        /// Removes items into the provided inventory object.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="items">The items to remove.</param>
        /// <returns>Returns true if the items have been removed successfully.</returns>
        bool RemoveItems(InventoryInfo inventory, Item[] items);

        /// <summary>
        /// Checks if the inventory has the item.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="item">The item to check.</param>
        /// <param name="editable">Should the item be editable?</param>
        /// <returns>Returns true if the inventory contains the item.</returns>
        bool HasItem(InventoryInfo inventory, Item item, bool editable = false);


        /// <summary>
        /// Checks if the inventory has the array of items.
        /// </summary>
        /// <param name="inventory">The inventory object to manipulate</param>
        /// <param name="items">The item to check.</param>
        /// <returns>Returns true if the inventory contains the items.</returns>
        bool HasItems(InventoryInfo inventory, Item[] items);
    }
}
