/* This class manages the current slot controller.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.Repository;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// This class manages the current slot controller.
/// </summary>
public class SlotController : MonoBehaviour
     , IPointerEnterHandler
     , IPointerExitHandler
     , IPointerDownHandler
     , IPointerUpHandler
{
    /// <summary>
    /// Event is fired when the mouse is down.
    /// </summary>
    public static event SlotEvents.OnSlotMouseDownAction OnSlotMouseDown;

    /// <summary>
    /// Event is fired when the mouse is up.
    /// </summary>
    public static event SlotEvents.OnSlotMouseUpAction OnSlotMouseUp;


    /// <summary>
    /// The current slot controller.
    /// </summary>
    public static SlotController currentSlot;

    /// <summary>
    /// The current slot index.
    /// </summary>
    public int slotIndex;

    /// <summary>
    /// The current parent inventory.
    /// </summary>
    public InventoryInfoController inventoryController;

    /// <summary>
    /// Start is fired automatically.
    /// </summary>
    private void Start()
    {
        FindIndex();
        inventoryController = transform.parent.GetComponent<InventoryInfoController>();
    }

    /// <summary>
    /// Event is fired once the pointer has entered the controller.
    /// </summary>
    /// <param name="eventData">The associated eventData.</param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        InfoManager.Instance.SetDetails(inventoryController.MyInventory.GetItem(slotIndex));
        currentSlot = this;
    }

    /// <summary>
    /// Event is fired once the pointer has exited the controller.
    /// </summary>
    /// <param name="eventData">The associated eventData.</param>
    public void OnPointerExit(PointerEventData eventData)
    {
        InfoManager.Instance.SetDetails(null);
        currentSlot = null;
    }

    /// <summary>
    /// Event is fired once the pointer has been released.
    /// </summary>
    /// <param name="eventData">The associated eventData.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        if (currentSlot == null)
            return;

		if (eventData.button == 0)
		{
			FireOnSlotMouseDown ();
		}
    }

    /// <summary>
    /// Event is fired once the pointer has been activated.
    /// </summary>
    /// <param name="eventData">The associated eventData.</param>
    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.button == 0)
            FireOnSlotMouseUp();
    }

    /// <summary>
    /// Fires the OnSlotMouseDown event.
    /// </summary>
    public void FireOnSlotMouseDown()
    {
        if (OnSlotMouseDown != null)
            OnSlotMouseDown(this, slotIndex);
				
    }

    /// <summary>
    /// Fires the OnSlotMouseUp event.
    /// </summary>
    public void FireOnSlotMouseUp()
    {
        if (OnSlotMouseUp != null)
            OnSlotMouseUp(this, slotIndex);
    }

    /// <summary>
    /// Finds the current slot index and applies it to "slotIndex".
    /// </summary>
    private void FindIndex()
    {
        int i = 0;
        foreach(Transform child in transform.parent)
        {
            if (child.Equals(transform))
            {
                slotIndex = i;
                return;
            }
            i++;
        }
    }

    /// <summary>
    /// highlights the current item slot.
    /// </summary>
    public void HighlightItem()
    {
        if(transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.GetComponent<Image>().sprite = GetItemInfo(this).hover_icon;
        }
    }

    /// <summary>
    /// Disables the current slot highlight selection.
    /// </summary>
    public void RestHighlightItem()
    {
        if (transform.childCount > 0)
        {
            Transform child = transform.GetChild(0);
            child.GetComponent<Image>().sprite = GetItemInfo(this).icon;
        }
    }

    /// <summary>
    /// Retrieves the item given an index.
    /// </summary>
    /// <param name="slotIndex">The inventory slot index.</param>
    /// <returns>Returns the item.</returns>
    public Item GetItem(int slotIndex)
    {
        return inventoryController.MyInventory.GetItem(slotIndex);
    }

    /// <summary>
    /// Retrieves the item given a slot controller.
    /// </summary>
    /// <param name="slotController">The related slot controller.</param>
    /// <returns>Returns the retrieved item.</returns>
    public Item GetItem(SlotController slotController)
    {
        return inventoryController.MyInventory.GetItem(slotController.slotIndex);
    }

    /// <summary>
    /// Retrieves the item.
    /// </summary>
    /// <returns>Returns the item.</returns>
    public Item GetItem()
    {
        return inventoryController.MyInventory.GetItem(slotIndex);
    }

    /// <summary>
    /// Retrieves the item information based on an idex.
    /// </summary>
    /// <param name="itemIndex">The item index.</param>
    /// <returns>Returns the item information.</returns>
    public InventoryItem GetItemInfo(int itemIndex)
    {
        return ItemRepository.Instance.GetInventoryItem(itemIndex);
    }

    /// <summary>
    /// Retrieves item slot information.
    /// </summary>
    /// <param name="slotController">The current slot to check.</param>
    /// <returns>Returns the inventory item information.</returns>
    public InventoryItem GetItemInfo(SlotController slotController)
    {
        Item myItem = GetItem(slotController.slotIndex);
        return ItemRepository.Instance.GetInventoryItem(myItem);
    }

    /// <summary>
    /// Retrieves the inventory item information.
    /// </summary>
    /// <returns>Returns the inventory item information.</returns>
    public InventoryItem GetItemInfo()
    {
        Item myItem = GetItem(slotIndex);
        return ItemRepository.Instance.GetInventoryItem(myItem);
    }
}
