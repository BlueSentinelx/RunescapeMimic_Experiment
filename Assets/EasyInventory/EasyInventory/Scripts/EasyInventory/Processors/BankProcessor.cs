/* Processes all item elements using a bank protocol;
 * Author: Corey St-Jacques
 * Date: May 24, 2017
 */


using System;
namespace EasyInventory.Processors
{
    /// <summary>
    /// Processes all item elements using a bank protocol;
    /// </summary>
    [Serializable]
    public class BankProcessor : ItemProcessorInfo
    {
        /// <summary>
        /// Bank constructor for processing bank objects.
        /// </summary>
        public BankProcessor()
        {
            StackableOverride = true;
        }
    }
}
