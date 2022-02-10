/* This class stores all item details for the view.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class stores all item details for the view.
/// </summary>
[CreateAssetMenu(fileName = "Item", menuName = "Inventory/Item")]
public class InventoryItem : ScriptableObject
{
    /// <summary>
    /// The current item name.
    /// </summary>
    public string itemName = "Item Name";

    /// <summary>
    /// The current item description.
    /// </summary>
    public string itemDescription = "Item Description";

    /// <summary>
    /// Is the item stackable or not?
    /// </summary>
    public bool stackable = false;

    /// <summary>
    /// The item's generic icon.
    /// </summary>
    public Sprite icon;

    /// <summary>
    /// The item's highlighted icon.
    /// </summary>
    public Sprite hover_icon;
}
