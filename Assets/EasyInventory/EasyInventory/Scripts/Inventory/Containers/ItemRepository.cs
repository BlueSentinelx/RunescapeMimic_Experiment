using EasyInventory.Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRepository : MonoBehaviour {

    public static ItemRepository Instance;

    public InventoryItem[] inventoryItems;

	// Use this for initialization
	void Start ()
    {
        Instance = this;
	}

    /// <summary>
    /// retrieves an inventory item provided the item index.
    /// </summary>
    /// <param name="index">The item index.</param>
    public InventoryItem GetInventoryItem(int index)
    {
        return inventoryItems[index];
    }


    /// <summary>
    /// retrieves an inventory item provided the item.
    /// </summary>
    /// <param name="item">The item.</param>
    public InventoryItem GetInventoryItem(Item item)
    {
        if (item == null)
            return null;

        if (item.ItemId >= 0)
            return inventoryItems[item.ItemId];
        else
            return null;
    }

    /// <summary>
    /// Retrieves the inventory items length.
    /// </summary>
    /// <returns>Returns the inventory items length.</returns>
    public int InventoryItemSize()
    {
        return inventoryItems.Length;
    }
}
