/* This class manages an inventory based system.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Factories;
using EasyInventory.Processors;
using EasyInventory.Repository;
using System;
using System.Diagnostics;


namespace EasyInventory.ServicesInterface
{
    /// <summary>
    /// This class manages an inventory based system.
    /// </summary>
    [Serializable]
    public class Inventory : InventoryInfo
    {
        /// <summary>
        /// The inventory base constructor.
        /// </summary>
        /// <param name="size">The size of the inventory.</param>
        public Inventory(int size)
            : base(size)
        {
            itemProcessor = new InventoryProcessor();
        }

        /// <summary>
        /// Adds an item tot he current inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Returns true if the item has been added successfully.</returns>
        public new bool AddItem(Item item)
        {
            Item myItem = null;
            if (!item.Stackable)
            {
                int amtLength = item.ItemAmount;
                for (int i = 0; i < amtLength; i++)
                {
                    Debug.WriteLine(i);
                    myItem = ItemFactory.CreateItem(item.ItemId, 1, item.Stackable);
                    if (!itemProcessor.AddItem(this, myItem))
                        return false;
                    else
                        item.ItemAmount--;
                }
            }
            else
            {
                if (!itemProcessor.AddItem(this, item))
                    return false;
            }
            return true;
        }

        /// <summary>
        /// ToString overload method for displaying the current inventory.
        /// </summary>
        /// <returns>Returns the string result.</returns>
        public override string ToString()
        {
            return "Inventory" + base.ToString();
        } 

    }
}
