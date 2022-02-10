/* This class stores Pointer events.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class stores Pointer events.
/// </summary>
public static class PointerEvents
{
    /// <summary>
    /// This event is fired when the mouse has been clicked down.
    /// </summary>
    /// <param name="position">The current mouse position.</param>
    public delegate void OnMouseDownAction(Vector3 position);

    /// <summary>
    /// This event is fired when the mouse has been clicked up.
    /// </summary>
    /// <param name="position">The current mouse position.</param>
    public delegate void OnMouseUpAction(Vector3 position);
}

