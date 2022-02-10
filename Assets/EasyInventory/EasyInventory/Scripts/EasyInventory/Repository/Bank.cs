/* This class manages a banking inventory system.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Processors;
using System;


namespace EasyInventory.ServicesInterface
{
    /// <summary>
    /// This class manages a banking inventory system.
    /// </summary>
    [Serializable]
    public class Bank : InventoryInfo
    {

        /// <summary>
        /// The bank constructor.
        /// </summary>
        /// <param name="size">Specifies the size of the inventory slots.</param>
        public Bank(int size)
            : base(size)
        {
            itemProcessor = new BankProcessor();
        }

        /// <summary>
        /// Overload method for ToString.
        /// </summary>
        /// <returns>Returns the string version of the bank instance.</returns>
        public override string ToString()
        {
            return "Bank" + base.ToString();
        } 

    }
}
