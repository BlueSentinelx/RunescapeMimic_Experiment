/* This class manages a generic inventory system.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Exceptions;
using EasyInventory.Repository;
using EasyInventory.ServicesInterface;
using EasyInventory.Utils;
using System;


namespace EasyInventory.Processors
{
    /// <summary>
    /// This class manages a generic Inventory.
    /// </summary>
    [Serializable]
    public class ItemProcessorInfo : IItemProcessor
    {
        /// <summary>
        /// Should the items be stackable?
        /// </summary>
        protected bool StackableOverride = false;

        /// <inheritdoc />
        public bool AddItem(InventoryInfo inventory, Item item)
        {
            Slot slot = null;
            if (StackableOverride || (!StackableOverride && item.Stackable))
            {
                slot = FindCommonItemSlot(inventory, item);
                if (slot != null)
                {
                    slot.CurrentItem.ItemAmount += item.ItemAmount;
                    return true;
                }
            }
            slot = FindEmptyItemSlot(inventory);
            if (slot != null)
            {
                slot.Add(item);
                return true;
            }
            else
            {
                throw new FullItemSlotsException();
            }
        }

        /// <inheritdoc />
        public bool AddItems(InventoryInfo inventory, Item[] items)
        {
            foreach(Item item in items)
                if (!AddItem(inventory, item))
                    return false;

            return true;
        }

        /// <inheritdoc />
        public bool RemoveItem(InventoryInfo inventory, Item item)
        {
            Slot slot = FindCommonItemSlot(inventory, item);
            return RemoveOperation(slot, item);
        }

        /// <summary>
        /// Removes an item at a given slot index.
        /// </summary>
        /// <param name="inventory">The inventory to manage.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="index">The index to remove from</param>
        /// <returns>Returns true if the changes were successful.</returns>
        public bool RemoveItem(InventoryInfo inventory, Item item, int index)
        {
            Slot slot = inventory.GetSlot(index);
            return RemoveOperation(slot, item);
        }

        // Removes an item given a slot object.
        private bool RemoveOperation(Slot slot, Item item)
        {
            if (slot != null)
                if (!slot.IsEmpty())
                {
                    slot.CurrentItem.ItemAmount -= item.ItemAmount;
                    if (slot.CurrentItem.ItemAmount <= 0)
                    {
                        item.ItemAmount = Math.Abs(slot.CurrentItem.ItemAmount);
                        slot.Clear();
                    }
                    return true;
                }

            return false;
        }

        /// <inheritdoc />
        public bool RemoveItems(InventoryInfo inventory, Item[] items)
        {
            bool result = true;
            foreach (Item item in items)
            {
                if (!RemoveItem(inventory, item))
                    result = false;
            }
            return result;
        }

        /// <inheritdoc />
        public bool HasItem(InventoryInfo inventory, Item item, bool editable = false)
        {
            bool result = false;
            int total = 0;
            foreach (Slot slot in inventory.slots)
            {
                if (!slot.IsEmpty())
                {
                    if (slot.CurrentItem.ItemId == item.ItemId)
                    {
                        total += slot.CurrentItem.ItemAmount;
                        if (editable)
                            slot.CurrentItem.ItemAmount -= item.ItemAmount;
                    }
                    if (total >= item.ItemAmount)
                    {
                        result = true;
                        break;
                    }
                }
            }

            return result;
        }

        /// <inheritdoc />
        public bool HasItems(InventoryInfo inventory, Item[] items)
        {
            InventoryInfo tmpInventory = inventory.Clone();
            Item[] tmpItems = items.CloneItems();
            foreach (Item item in tmpItems)
                if (!HasItem(tmpInventory, item, true))
                    return false;

            return true;
        }

        /// <summary>
        /// Finds a common slot which contains the same item.
        /// </summary>
        /// <param name="inventory">The inventory to manipulate.</param>
        /// <param name="item">The item to compare with.</param>
        /// <returns>Returns the found slot comparison.</returns>
        protected Slot FindCommonItemSlot(InventoryInfo inventory, Item item)
        {
            foreach (Slot slot in inventory.slots)
                if(!slot.IsEmpty())
                    if (slot.CurrentItem.ItemId == item.ItemId)
                        return slot;
            return null;
        }

        /// <summary>
        /// Searches for an empty slot.
        /// </summary>
        /// <param name="inventory">The inventory to manipulate.</param>
        /// <returns>Returns the found inventory slot.</returns>
        protected Slot FindEmptyItemSlot(InventoryInfo inventory)
        {
            foreach (Slot slot in inventory.slots)
                if (slot.IsEmpty())
                    return slot;
            return null;
        }
    }
}
