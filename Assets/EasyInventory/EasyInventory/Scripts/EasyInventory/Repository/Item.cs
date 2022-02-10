/* This class manages an item instance.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Factories;
using EasyInventory.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EasyInventory.Repository
{
    /// <summary>
    /// This class manages an item instance.
    /// </summary>
    [Serializable]
    public class Item : IDraggable
    {
        private int _itemId;
        private int _itemAmount;
        private bool _stackable;

        /// <summary>
        /// Is the item stackable or not.
        /// </summary>
        public bool Stackable
        {
            get
            {
                return _stackable;
            }
            set
            {
                _stackable = value;
            }
        }

        /// <summary>
        /// How many of the current do we have?
        /// </summary>
        public int ItemAmount
        {
            get
            {
                return _itemAmount;
            }
            set
            {
                _itemAmount = value;
            }
        }

        /// <summary>
        /// The current item id.
        /// </summary>
        public int ItemId
        {
            get 
            { 
                return _itemId; 
            }
            set 
            { 
                _itemId = value; 
            }
        }
        
        /// <summary>
        /// The item constructor.
        /// </summary>
        /// <param name="id">The current item id.</param>
        /// <param name="amt">The current item amount.</param>
        /// <param name="stackable">Is the item stackable or not.</param>
        public Item(int id, int amt, bool stackable)
        {
            _itemId = id;
            _itemAmount = amt;
            _stackable = stackable;
        }

        /// <summary>
        /// Clones the current instance.
        /// </summary>
        /// <returns>Returns the cloned instance.</returns>
        public Item Clone()
        {
            Item tmpItem = 
                ItemFactory.CreateItem(ItemId, 
                    ItemAmount, 
                    Stackable);

            return tmpItem;
        }

        /// <summary>
        /// Converts the current instance toa  readable string.
        /// </summary>
        /// <returns>Returns the instance as a string.</returns>
        public override string ToString()
        {
            return "[id: " + _itemId
                + ", amt: " + _itemAmount + "]";
        }

    }
}
