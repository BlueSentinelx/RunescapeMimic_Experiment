using EasyInventory.Exceptions;
using EasyInventory.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public static class IntermediateController
{
    public static bool Move(InventoryInfoController start, 
        InventoryInfoController end, 
        int slotIndex, 
        int amount = 1)
    {
        bool result = false;
        Item itemDetails = start.GetItem(slotIndex);
        try
        {
            bool ans = end.MyInventory.AddItem(itemDetails, amount);
            if (ans)
                start.MyInventory.RemoveItem(itemDetails, amount, slotIndex);
        }
        catch (FullItemSlotsException fiseRef)
        {
            Debug.Log(fiseRef);
        }

        return result;
    }
}

