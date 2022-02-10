/* This class is a mock object class which allows the parent class to instantiate itself.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using System;
namespace EasyInventory.ServicesInterface
{
    /// <summary>
    /// This class is a mock object class which allows the parent class to instantiate itself.
    /// </summary>
    [Serializable]
    internal class BaseService : InventoryInfo
    {
        /// <summary>
        /// A constructor of the base service.
        /// </summary>
        /// <param name="slotSize">The size of the current inventory.</param>
        public BaseService(int slotSize) : base(slotSize)
        {
        }
    }
}
