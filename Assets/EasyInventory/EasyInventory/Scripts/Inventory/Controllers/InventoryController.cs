/* This class manages the current inventory.
 * Author: Corey St-Jacques
 * Date: May 25, 2017
 */

using EasyInventory.ServicesInterface;
using EasyInventory.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Controller for managing the unity inventory view.
/// </summary>
public class InventoryController : InventoryInfoController
{
    public static new InventoryInfoController Instance;

    private InventoryInfo _myInventory;
    /// <summary>
    /// Inventory instance.
    /// </summary>
    public override InventoryInfo MyInventory
    {
        get
        {
            return _myInventory;
        }
        set
        {
            _myInventory = value;
        }
    }

    // Use this for initialization
    private void Start()
    {
        Instance = this;
        MyInventory = new Inventory(transform.childCount);
        LoadInventory();
    }

    /// <summary>
    /// Loads the current inventory.
    /// </summary>
    public override void LoadInventory()
    {
        try
        {
            byte[] fileData = IO.ReadFile(Application.dataPath + "/" + fileName);
            MyInventory = (Inventory)IO.ByteArrayToObject(fileData);
        }
        catch
        {
            SaveInventory();
        }
        UpdateInventory();
    }
}
