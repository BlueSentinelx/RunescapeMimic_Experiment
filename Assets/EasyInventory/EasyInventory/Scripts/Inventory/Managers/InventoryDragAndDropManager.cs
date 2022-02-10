/* This class manages all drag and drop events.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// This class manages all drag and drop events.
/// </summary>
public class InventoryDragAndDropManager : MonoBehaviour
{
    /// <summary>
    /// The current drag and drop instance.
    /// </summary>
    public static InventoryDragAndDropManager Instance;

    /// <summary>
    /// Are we currently dragging an item?
    /// </summary>
    public bool dragging = false;

    /// <summary>
    /// The current item transform we are dragging.
    /// </summary>
    public Transform itemDragging;

    /// <summary>
    /// The initial index slot from which we started dragging from.
    /// </summary>
    public int initialDragIndex;

    /// <summary>
    /// The initial mouse offset from the object center. Later used for dragging offsets.
    /// </summary>
    public Vector3 initialClickOffset;

    /// <summary>
    /// The current dragging slot we are managing.
    /// </summary>
    public Transform itemDragSlot;

    /// <summary>
    /// The current selected slot controller.
    /// </summary>
    public SlotController currentSelectedItem;

	public GameObject bankUI;
	/// <summary>
	/// The unity start method.
	/// </summary>
	/// 
	void Awake(){

		bankUI = GameObject.Find ("BankUI");
	}
	void Start ()
    {
        Instance = this;
		bankUI.SetActive (false);
	}

    /// <summary>
    /// This method is automatically called once per frame.
    /// </summary>
    private void Update()
    {
        if (itemDragging == null)
            return;

        if (dragging)
            itemDragging.position = Input.mousePosition + initialClickOffset;
    }

    /// <summary>
    /// Subscribing all events when the object has been enabled.
    /// </summary>
    private void OnEnable()
    {
        SlotController.OnSlotMouseDown += SlotMouseDown;
        SlotController.OnSlotMouseUp += SlotMouseUp;
    }

    /// <summary>
    /// Unsubscribing all events once the object has been disabled.
    /// </summary>
    private void OnDisable()
    {
        SlotController.OnSlotMouseDown -= SlotMouseDown;
        SlotController.OnSlotMouseUp -= SlotMouseUp;
    }

    /// <summary>
    /// Event method is fired once a slot has been released.
    /// </summary>
    /// <param name="slot">The current selected slot.</param>
    /// <param name="index">The current sot index.</param>
    private void SlotMouseUp(SlotController slot, int index)
    {
        if (slot == null)
            return;

        if (SlotController.currentSlot == null)
        {
            ClearDragging(slot);
            Debug.Log("Invalid drag.");
            return;
        }

        if (currentSelectedItem == null)
            return;

        bool sameSlot = (currentSelectedItem.inventoryController.GetType() 
            == SlotController.currentSlot.inventoryController.GetType());

        if (SlotController.currentSlot == null || !sameSlot)
        {
            //Debug.Log("Wrong drag destination: " + slot.GetItemInfo().itemName);
            ClearDragging(slot);
        }

        else if (dragging && sameSlot)
        {
            if (initialDragIndex != SlotController.currentSlot.slotIndex && slot.GetItemInfo() != null)
                //Debug.Log("Dragged: " + slot.GetItemInfo().itemName + " to slot " + SlotController.currentSlot.slotIndex + ".");
            slot.inventoryController.MyInventory.SwapItems(initialDragIndex, SlotController.currentSlot.slotIndex);
            ClearDragging(slot);
        }
    }

    /// <summary>
    /// Clears all dragging components and resets for the next drag.
    /// </summary>
    /// <param name="slot">The current dragging slot to clear.</param>
    public void ClearDragging(SlotController slot)
    {
        if (itemDragging != null)
            Destroy(itemDragging.gameObject);
        slot.inventoryController.UpdateInventory();
        CheckSelected(slot);
        dragging = false;
        initialDragIndex = -1;
    }

    /// <summary>
    /// Event method is fired once a slot has been clicked on.
    /// </summary>
    /// <param name="slot">The current selected slot.</param>
    /// <param name="index">The current sot index.</param>
    private void SlotMouseDown(SlotController slot, int index)
    {
        if (slot == null)
            return;
        dragging = true;
        itemDragging = slot.transform.GetChild(0);
        initialDragIndex = index;
        initialClickOffset = itemDragging.position - Input.mousePosition;
        itemDragging.SetParent(itemDragSlot);
        itemDragging.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        currentSelectedItem = SlotController.currentSlot;
    }

    /// <summary>
    /// Checks if we are selecting an item, or dragging it.
    /// </summary>
    /// <param name="slot"></param>
    private void CheckSelected(SlotController slot)
    {
        if (SlotController.currentSlot == null)
            return;

        if (currentSelectedItem != null)
            currentSelectedItem.RestHighlightItem();

        if (initialDragIndex == SlotController.currentSlot.slotIndex)
        {
            currentSelectedItem = SlotController.currentSlot;
            slot.HighlightItem();
            //ItemPreviewManager.Instance.ApplyItemPreview(slot.GetItem(initialDragIndex));
			if(bankUI.activeInHierarchy){
	            if (SlotController.currentSlot.inventoryController.GetType() == typeof(BankController))
	                IntermediateController.Move(BankController.Instance, InventoryController.Instance, initialDragIndex);
	            else
	                IntermediateController.Move(InventoryController.Instance, BankController.Instance, initialDragIndex);

	            
	            BankController.Instance.UpdateInventory();
	            InventoryController.Instance.UpdateInventory();
	            //Debug.Log("Selected: " + slot.GetItemInfo().itemName + " at slot " + slot.slotIndex + ".");
			}
        }
        else
        {
            currentSelectedItem = null;
        }
    }
}
