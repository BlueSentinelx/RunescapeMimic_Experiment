/* This class manages the current inventory.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using EasyInventory.Processors;
using EasyInventory.Utils;
using EasyInventory.Repository;
using EasyInventory.ServicesInterface;
using UnityEngine.UI;
using EasyInventory.Factories;
using EasyInventory.Exceptions;

/// <summary>
/// Controller for managing the unity inventory view.
/// </summary>
public abstract class InventoryInfoController : MonoBehaviour
{
    public static InventoryInfoController Instance;

    abstract public InventoryInfo MyInventory
    {
        get;
        set;
    }
    public string fileName = "myInventory.inv";
    public Transform UIItemPrefab;

    /// <summary>
    /// Loads the current inventory.
    /// </summary>
    public abstract void LoadInventory();

    /// <summary>
    /// Saves all inventory changes.
    /// </summary>
    public void SaveInventory()
    {
        byte[] fileData = IO.ObjectToByteArray(MyInventory);
        IO.SaveToFile(Application.dataPath + "/" + fileName, fileData);

    }

    /// <summary>
    /// Refreshes the graphical interface of the current inventory.
    /// </summary>
    public void UpdateInventory()
    {
        DestroyGraphics();
        Slot slot;
        for (int i = 0; i < transform.childCount; i++)
        {
            slot = MyInventory.GetSlot(i);
            SpawnSlot(slot, transform.GetChild(i));
        }
    }

    /// <summary>
    /// Spawns an existing item inside the inventory to be displayed within the designated slot.
    /// </summary>
    /// <param name="slot">The slot to spawn an item into.</param>
    /// <param name="parent">The Transform parent of the slot.</param>
    public void SpawnSlot(Slot slot, Transform parent)
    {
        if (!slot.IsEmpty())
            SpawnItem(slot.CurrentItem, parent);
    }

    /// <summary>
    /// Spawns an item within the inventory view.
    /// </summary>
    /// <param name="item">The item to spawn.</param>
    /// <param name="parent">The parent </param>
    /// <returns>Returns the instantiated transform.</returns>
    public Transform SpawnItem(Item item, Transform parent)
    {
        Transform tmp = Instantiate(UIItemPrefab);
        tmp.SetParent(parent);
        tmp.localPosition = Vector3.zero;
        tmp.localScale = Vector3.one;
        ApplyGraphics(tmp, item);
        return tmp;
    }

    /// <summary>
    /// Applies all graphical changes to the current item object.
    /// </summary>
    /// <param name="itemUI">The UI to modify.</param>
    /// <param name="item">The item object to modify with.</param>
    public void ApplyGraphics(Transform itemUI, Item item)
    {
        InventoryItem inventoryItem = ItemRepository.Instance.GetInventoryItem(item);
        itemUI.GetComponent<Image>().sprite = inventoryItem.icon;
        Text uiText = itemUI.GetChild(0).GetComponent<Text>();
        uiText.text = ToFormattedNumber(item.ItemAmount);
    }

    /// <summary>
    /// Destroys all graphical items related to the inventory view.
    /// </summary>
    public void DestroyGraphics()
    {
        foreach (Transform slot in transform)
            if (slot.childCount > 0)
                foreach (Transform child in slot)
                    Destroy(child.gameObject);
    }

    /// <summary>
    /// Clears the current inventory of all items.
    /// </summary>
    public void ClearInventory()
    {
        MyInventory.Clear();
        UpdateInventory();
    }

    /// <summary>
    /// Populates the current inventory with random items.
    /// </summary>
    public void PopulateInventory()
    {
        int itemIndex = Random.Range(0, ItemRepository.Instance.InventoryItemSize());
        InventoryItem inventoryItem = ItemRepository.Instance.GetInventoryItem(itemIndex);
        bool stackable = inventoryItem.stackable;

        int iterations = (stackable) ? 1 : Random.Range(1, 5);

        for (int i = 0; i < iterations; i++)
        {
            itemIndex = Random.Range(0, ItemRepository.Instance.InventoryItemSize());
            inventoryItem = ItemRepository.Instance.GetInventoryItem(itemIndex);
            stackable = inventoryItem.stackable;
            AddItem(itemIndex, (!stackable) ? 1 : Random.Range(1, 9999));
        }
        
        UpdateInventory();
    }

    /// <summary>
    /// Adds an item tot he current inventory.
    /// </summary>
    /// <param name="itemIndex">The item index where to add the item to.</param>
    /// <param name="amount">The amount of the specified item to add.</param>
    public bool AddItem(int itemIndex, int amount)
    {
        InventoryItem inventoryItem = ItemRepository.Instance.GetInventoryItem(itemIndex);
        Item myItem = ItemFactory.CreateItem(itemIndex, amount, inventoryItem.stackable);

        try
        {
            return MyInventory.AddItem(myItem);
        }
        catch(FullItemSlotsException fiseRef)
        {
            Debug.Log("Inventory is full Exception. " + fiseRef);
            return false;
        }
    }

    /// <summary>
    /// Converts an integer to a numercial string.
    /// </summary>
    /// <param name="value">The integer to convert.</param>
    /// <returns>Returns the converted numerical string.</returns>
    public static string ToFormattedNumber(int value)
    {
        string result = value.ToString();

        if (value <= 1)
            result = string.Empty;

        else if (result.Length > 3 && result.Length <= 6)
            result = result.Substring(0, result.Length - 3) + "K";

        else if (result.Length > 6 && result.Length <= 9)
            result = result.Substring(0, result.Length - 6) + "M";

        else if (result.Length > 9)
            result = result.Substring(0, result.Length - 9) + "T";

        return result;
    }

    /// <summary>
    /// Retrieves the item given an index.
    /// </summary>
    /// <param name="slotIndex">The inventory slot index.</param>
    /// <returns>Returns the item.</returns>
    public Item GetItem(int slotIndex)
    {
        return MyInventory.GetItem(slotIndex);
    }

    /// <summary>
    /// Retrieves the item given a slot controller.
    /// </summary>
    /// <param name="slotController">The related slot controller.</param>
    /// <returns>Returns the retrieved item.</returns>
    public Item GetItem(SlotController slotController)
    {
        return MyInventory.GetItem(slotController.slotIndex);
    }
}
