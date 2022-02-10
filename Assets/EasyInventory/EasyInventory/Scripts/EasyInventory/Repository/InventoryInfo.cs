/* This class is the base service class for managing your inventory items.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */


using EasyInventory.Repository;
using EasyInventory.Factories;

using EasyInventory.Processors;
using EasyInventory.Utils;
using System;

namespace EasyInventory.ServicesInterface
{
    /// <summary>
    /// This class is the base service class for managing your inventory items.
    /// </summary>
    [Serializable]
    public abstract class InventoryInfo
    {
        /// <summary>
        /// The item processor attached to this service.
        /// </summary>
        protected IItemProcessor itemProcessor;

        /// <summary>
        /// The inventory slots containing items.
        /// </summary>
        public Slot[] slots;

        /// <summary>
        /// The slot size of the slot array.
        /// </summary>
        private readonly int _slotSize;

        /// <summary>
        /// The slot size of the slot array.
        /// </summary>
        public int SlotSize
        {
            get
            {
                return _slotSize;
            }
        }

        /// <summary>
        /// The base constructor for the inventory service.
        /// </summary>
        /// <param name="slotSize">The size of the inventory.</param>
        public InventoryInfo(int slotSize)
        {
            _slotSize = slotSize;
            slots = new Slot[_slotSize];
            InitializeSlots();
        }

        /// <summary>
        /// Initializes the inventory slots.
        /// </summary>
        private void InitializeSlots()
        {
            for (int i = 0; i < _slotSize; i++)
                slots[i] = SlotFactory.CreateSlot();
        }

        /// <summary>
        /// Swaps two items from one slot index to another.
        /// </summary>
        /// <param name="start">The start index.</param>
        /// <param name="end">The end index.</param>
        public void SwapItems(int start, int end)
        {
            Item start_item = null;
            Item end_item = null;

            Slot slot_start = GetSlot(start);
            Slot slot_end = GetSlot(end);

            if (!slot_start.IsEmpty())
                start_item = slot_start.CurrentItem;

            if (!slot_end.IsEmpty())
                end_item = slot_end.CurrentItem;

            slot_start.SetItem(end_item);
            slot_end.SetItem(start_item);
        }

        /// <summary>
        /// Moves an item from one slot to another.
        /// </summary>
        /// <param name="start">The start slot index.</param>
        /// <param name="end">The end slot index.</param>
        public void Move(int start, int end)
        {
            SwapItems(start, end);
        }

        /// <summary>
        /// Removes an item from the current inventory.
        /// </summary>
        /// <param name="index">The slot index where to remove the item from.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool RemoveItem(int index)
        {
            Slot slot = GetSlot(index);
            return itemProcessor.RemoveItem(this, slot.CurrentItem);
        }

        /// <summary>
        /// Removes an item from the current inventory.
        /// </summary>
        /// <param name="item">The item at which to remove from.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool RemoveItem(Item item)
        {
            return itemProcessor.RemoveItem(this, item);
        }

        /// <summary>
        /// Removes an item from the current inventory.
        /// </summary>
        /// <param name="item">The item at which to remove from.</param>
        /// <param name="amt">The item amt to remove.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool RemoveItem(Item item, int amt)
        {
            Item tmpItem = ItemFactory.CreateItem(item.ItemId, amt, item.Stackable);
            return itemProcessor.RemoveItem(this, tmpItem);
        }

        /// <summary>
        /// Removes an item from the current inventory given an index.
        /// </summary>
        /// <param name="item">The item at which to remove from.</param>
        /// <param name="amt">The item amt to remove.</param>
        /// <param name="index">The index to remove from.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool RemoveItem(Item item, int amt, int index)
        {
            Item tmpItem = ItemFactory.CreateItem(item.ItemId, amt, item.Stackable);
            return itemProcessor.RemoveItem(this, tmpItem, index);
        }

        /// <summary>
        /// Removes an item from the current inventory.
        /// </summary>
        /// <param name="index">The index at which to remove from.</param>
        /// <param name="amt">The item amt to remove.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool RemoveItem(int index, int amt)
        {
            Item item = GetItem(index);
            Item tmpItem = ItemFactory.CreateItem(item.ItemId, amt, item.Stackable);
            return itemProcessor.RemoveItem(this, tmpItem, index);
        }

        /// <summary>
        /// Removes mutliple items from the current inventory.
        /// </summary>
        /// <param name="items">The items to remove.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool RemoveItems(Item[] items)
        {
            return itemProcessor.RemoveItems(this, items);
        }

        /// <summary>
        /// Adds an item to the current inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool AddItem(Item item)
        {
            return itemProcessor.AddItem(this, item);
        }

        /// <summary>
        /// Adds an item to the current inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        /// <param name="amt">The item amount to add.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool AddItem(Item item, int amt)
        {
            //if (item == null)
            //    return false;
            Item tmpItem = ItemFactory.CreateItem(item.ItemId, amt, item.Stackable);
            return itemProcessor.AddItem(this, tmpItem);
        }

        /// <summary>
        /// Adds an array of items to the current inventory system.
        /// </summary>
        /// <param name="items">The items to add.</param>
        /// <returns>Returns true if the change has been made successfully.</returns>
        public bool AddItems(Item[] items)
        {
            return itemProcessor.AddItems(this, items);
        }

        /// <summary>
        /// Checks whether or not the item exist within the inventory.
        /// </summary>
        /// <param name="item">The item which to compare with.</param>
        /// <returns>Returns true if the item exists within the inventory.</returns>
        public bool HasItem(Item item)
        {
            return itemProcessor.HasItem(this, item);
        }

        /// <summary>
        /// Checks whether or not the items exist within the inventory.
        /// </summary>
        /// <param name="items">The items which to compare with.</param>
        /// <returns>Returns true if the items exist within the inventory.</returns>
        public bool HasItems(Item[] items)
        {
            return itemProcessor.HasItems(this, items);
        }

        /// <summary>
        /// Checks if the current inventory slot is occupied with an item or not.
        /// </summary>
        /// <param name="index">The slot index of which to check.</param>
        /// <returns>Returns true if the index slot is occupied.</returns>
        public bool IsOccupied(int index)
        {
            return !GetSlot(index).IsEmpty();
        }

        /// <summary>
        /// Checks if the inventory is completely empty.
        /// </summary>
        /// <returns>Returns true if the inventory is empty.</returns>
        public bool IsEmpty()
        {
            foreach (Slot slot in slots)
                if (!slot.IsEmpty())
                    return false;
            return true;
        }

        /// <summary>
        /// Retrieves a slot object from the current inventory.
        /// </summary>
        /// <param name="index">The slot index to retrieve from.</param>
        /// <returns>Returns the found slot.</returns>
        public Slot GetSlot(int index)
        {
            index = (index < 0) ? 0 : index;
            return slots[index];
        }

        /// <summary>
        /// Retrieves an item within the current inventory system.
        /// </summary>
        /// <param name="index">The current slot of index.</param>
        /// <returns>Returns the found item.</returns>
        public Item GetItem(int index)
        {
            return GetSlot(index).CurrentItem;
        }

        /// <summary>
        /// Clears the current inventory of items.
        /// </summary>
        public void Clear()
        {
            foreach(Slot slot in slots)
                if (!slot.IsEmpty())
                    slot.Clear();
        }

        /// <summary>
        /// Clones the current inventory.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public InventoryInfo Clone()
        {
            InventoryInfo tmpTnventoryInfo = new BaseService(_slotSize);
            tmpTnventoryInfo.itemProcessor = itemProcessor;
            tmpTnventoryInfo.slots = slots.CloneSots();
            return tmpTnventoryInfo;
        }

        /// <summary>
        /// Converts the current inventory object to a string reference.
        /// </summary>
        /// <returns>Returns the string reference.</returns>
        public override string ToString()
        {
            string result = string.Empty;

            result += " size: " + _slotSize + ", ";
            result += "{";
            bool first = true;
            foreach(Slot slot in slots)
            {
                if (!first)
                    result += ", ";
                result += slot + "";
                first = false;
            }
            result += "}";

            return result;
        }
    }
}
