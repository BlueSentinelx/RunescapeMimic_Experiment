/* This class manages a generic inventory system.
 * Author: Corey St-Jacques
 * Date: May 24, 2017
 */


using EasyInventory.ServicesInterface;
using System;


namespace EasyInventory.Processors
{
    /// <summary>
    /// This class manages a generic inventory system.
    /// </summary>
    [Serializable]
    public class InventoryProcessor : ItemProcessorInfo
    {
        /// <summary>
        /// Processes the current inventory.
        /// </summary>
        public InventoryProcessor()
        {
            StackableOverride = false;
        }
    }
}
