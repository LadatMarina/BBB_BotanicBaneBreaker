using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class Inventory
{
    private List<Recollectable> recollectableList;
    private List<Item> itemList;


    public Inventory()
    {
        recollectableList = new List<Recollectable>();
        
        itemList = new List<Item>();

        //AddRecollectableToTheInventory()
        Debug.Log("the inventory has been initialized");
        //Debug.Log(recollectableList.Count);
    }

    public void AddRecollectableToTheInventory(Recollectable recollectable)
    {
        recollectableList.Add(recollectable);
        
        Debug.Log($"you have added a {recollectable} to the inventory");
    }

    public void AddItemToTheInventory(Item item)
    {
        itemList.Add(item);

        Debug.Log($"you have added a {item} to the inventory");
    }

    public List<Recollectable> GetRecolletableList()
    {
        return recollectableList;
    }

    public void AddItem(Item item)
    {
        foreach( Item inventoryItem in itemList)
        {
            if (item.itemSO.recollectableType == inventoryItem.itemSO.recollectableType)
            {
                inventoryItem.amount = inventoryItem.amount + item.amount;
                Debug.Log("due to the item was already in the list, we onliy have increased the sum of the amount");
            }
            else
            {
                itemList.Add(item);
                Debug.Log("the item was not in the list. We have added the item to the list");
            }
        }

        itemList.Add(item);
        Debug.Log("the item was not in the list. We have added the item to the list");
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
