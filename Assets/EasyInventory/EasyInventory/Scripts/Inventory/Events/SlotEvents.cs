/* This class stores all slot events.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


/// <summary>
/// This class stores all slot events.
/// </summary>
public static class SlotEvents
{
    /// <summary>
    /// This event is fired once a slot has been clicked on.
    /// </summary>
    /// <param name="slot">The slot controller.</param>
    /// <param name="index">The slot index.</param>
    public delegate void OnSlotMouseDownAction(SlotController slot, int index);

    /// <summary>
    /// This event is fired once a slot hass been released.
    /// </summary>
    /// <param name="slot">The slot controller.</param>
    /// <param name="index">The slot index.</param>
    public delegate void OnSlotMouseUpAction(SlotController slot, int index);
}

