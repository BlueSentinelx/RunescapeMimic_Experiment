/* Exception class used for throwing inventory exceptions.
 * Author: Corey St-Jacques
 * Date: May 24, 2017
 */

using System;
using System.Runtime.Serialization;

namespace EasyInventory.Exceptions
{
    /// <summary>
    /// Exception is called when all the slots are full.
    /// </summary>
    [Serializable]
    internal class FullItemSlotsException : Exception
    {
        /// <summary>
        /// Exception is called when all the slots are full.
        /// </summary>
        public FullItemSlotsException()
             : base("Slots are full.")
        {
        }

        /// <summary>
        /// Custom slot message exception constructor.
        /// </summary>
        /// <param name="message">Your message to throw.</param>
        public FullItemSlotsException(string message) : base(message)
        {
        }
    }
}