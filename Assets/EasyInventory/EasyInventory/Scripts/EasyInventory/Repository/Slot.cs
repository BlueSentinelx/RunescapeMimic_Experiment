/* This class manages slot items.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Factories;
using System;


namespace EasyInventory.Repository
{
    /// <summary>
    /// This class manages slot items.
    /// </summary>
    [Serializable]
    public class Slot
    {

        private Item _currentItem;

        /// <summary>
        /// The current attached item.
        /// </summary>
        public Item CurrentItem
        {
            get
            {
                return _currentItem;
            }
            set
            {
                _currentItem = value;
            }
        }

        /// <summary>
        /// The slot constructor.
        /// </summary>
        /// <param name="currentItem">The associated item.</param>
        public Slot(Item currentItem = null)
        {
            _currentItem = currentItem;
        }

        /// <summary>
        /// Adds an item to the current slot.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void Add(Item item)
        {
            if (IsEmpty())
            {
                SetItem(item);
                return;
            }

            if (CurrentItem.ItemId != item.ItemId)
                return;

            CurrentItem.ItemAmount += item.ItemAmount;
        }

        /// <summary>
        /// Sets the current item to the current slot.
        /// </summary>
        /// <param name="item">The current item.</param>
        public void SetItem(Item item)
        {
            _currentItem = item;
        }


        /// <summary>
        /// Clears the current item.
        /// </summary>
        /// <returns>Returns the item to see if it has been cleared successfully.</returns>
        public Item Clear()
        {
            Item item = _currentItem;
            _currentItem = null;
            return item;
        }

        /// <summary>
        /// Checks if the slot is empty or not.
        /// </summary>
        /// <returns>Returns true if the the slot is empty.</returns>
        public bool IsEmpty()
        {
            return (_currentItem == null);
        }

        /// <summary>
        /// Clones the current slot.
        /// </summary>
        /// <returns>Returns the new slot instance.</returns>
        public Slot Clone()
        {
            Slot tmpSlot = SlotFactory.CreateSlot();
            if (!IsEmpty())
                tmpSlot._currentItem =
                    ItemFactory.CreateItem(_currentItem.ItemId,
                        _currentItem.ItemAmount,
                        _currentItem.Stackable);
            else
                tmpSlot._currentItem = null;
            return tmpSlot;
        } 

        /// <summary>
        /// Converts the current instance to a readable string.
        /// </summary>
        /// <returns>The resulting string.</returns>
        public override string ToString()
        {
            string result = string.Empty;
            if (_currentItem != null)
                result += _currentItem;
            else
                result += "NULL";

            return result;
        }

    }
}
