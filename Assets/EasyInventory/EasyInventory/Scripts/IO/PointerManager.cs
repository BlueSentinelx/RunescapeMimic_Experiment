/* This class manages all pointer events.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class manages all pointer events.
/// </summary>
public class PointerManager : MonoBehaviour
{
    /// <summary>
    /// The event fired on mouse down.
    /// </summary>
    public static event PointerEvents.OnMouseDownAction OnMouseDown;

    /// <summary>
    /// The event fired on mouse up.
    /// </summary>
    public static event PointerEvents.OnMouseUpAction OnMouseUp;

    /// <summary>
    /// The update method is automatically called by the unity engine.
    /// </summary>
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            FireOnMouseDown();
        }
        if(Input.GetMouseButtonUp(0))
        {
            FireOnMouseUp();
        }
    }

    /// <summary>
    /// This method invokes the on mouse down event.
    /// </summary>
    private void FireOnMouseDown()
    {
        if (OnMouseDown != null)
            OnMouseDown(Input.mousePosition);
    }

    /// <summary>
    /// This method invokes the on mouse up event.
    /// </summary>
    private void FireOnMouseUp()
    {
        if (OnMouseUp != null)
            OnMouseUp(Input.mousePosition);
    }

}
