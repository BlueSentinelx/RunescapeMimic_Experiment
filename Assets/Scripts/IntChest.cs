using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyInventory.Repository;
using EasyInventory.ServicesInterface;
using EasyInventory.Utils;
using UnityEngine.UI;

public class IntChest : Interactable
{
	private bool IsNotOpen = false;
	public ChatterManager chatManager;
	private int itemIndex;
	private int itemCount = 5;
	private int tempCount = 0;
	private Item myItem;
	public InventoryController myInv;

	public override void Interact(GameObject player)
	{	

		itemIndex = Random.Range(0, 12);
		IsNotOpen = !IsNotOpen;

		if(IsNotOpen == true)IsNotOpen = false;

		if (IsNotOpen == false && interactableName == "Chest") {
			
			if (tempCount <= itemCount) {
				tempCount++;
				playerAnimator.SetTrigger ("Open");
				myItem = new Item(itemIndex, 1, true);
				myInv.MyInventory.AddItem (myItem);
				myInv.UpdateInventory ();
				GameObject newMessage = Instantiate (chatManager.chatMessage, chatManager.chatContent);
				Text content = newMessage.GetComponent<Text> ();

				content.text = "<color=#FFCC30>You found some stuff in this chest!</color>";
			} else {
				GameObject newMessage = Instantiate (chatManager.chatMessage, chatManager.chatContent);
				Text content = newMessage.GetComponent<Text> ();
				content.text = "<color=#FFCC30>There is nothing left</color>";
			}
		}

	}

}

