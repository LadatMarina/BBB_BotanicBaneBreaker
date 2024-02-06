using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        Debug.Log("the inventory has been initialized");
    }

    public void AddItem(Item item)
    {
        bool itemInInventory = false;
        foreach (Item inventoryitem in itemList)
        {
            //if theitem was already amount+ / true
            if (item.itemSO.recollectableType == inventoryitem.itemSO.recollectableType)
            {
                inventoryitem.amount = inventoryitem.amount + item.amount;
                itemInInventory = true;
                Debug.Log("due to the item was already in the list, we only have increased the sum of the amount");
                Debug.Log($"you have added a {inventoryitem.itemSO.name} / {item.itemSO.name} to the inventory with amount {inventoryitem.amount}");
            }
            
        }
        if(!itemInInventory)
        {
            itemList.Add(item);
            Debug.Log("the item was not in the list. we have added the item to the list");
            Debug.Log($"you have added a {item} to the inventory with amount {item.amount}");
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
