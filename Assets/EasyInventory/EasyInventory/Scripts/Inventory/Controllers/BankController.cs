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
public class BankController : InventoryInfoController
{
    
    public new static InventoryInfoController Instance;

    private InventoryInfo _myInventory;
    /// <summary>
    /// Bank instance.
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
        MyInventory = new Bank(transform.childCount);
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
            MyInventory = (Bank)IO.ByteArrayToObject(fileData);
        }
        catch
        {
            SaveInventory();
        }
        UpdateInventory();
    }

}
