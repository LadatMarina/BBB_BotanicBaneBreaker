using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();

        //Debug.Log("the inventory has been initialized");
    }
    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void RemoveItemFromList(Item itemToRemove)
    {
        if(itemList != null)
        {
            foreach (Item item in itemList)
            {
                if (item == itemToRemove)
                {
                    itemList.Remove(itemToRemove);
                    //Debug.Log($"the item {itemToRemove} has been deleted of the list");
                    //ara me dona error, però si jo pos aquí un brake, quan ho hagui trobat sortirà
                    //des loop, no m'hauria de donar problemes en teoria
                    break;
                }
            }
        }
    }

    public void AddItem(Item item)
    {
        Debug.Log("addItem / player");
        foreach (Item inventoryItem in itemList)
        {
            //if the item was already, amount+ / true
            if (item.itemSO == inventoryItem.itemSO)
            {
                inventoryItem.amount = inventoryItem.amount + item.amount;

                //Debug.Log("due to the item was already in the list, we only have increased the sum of the amount");
                //Debug.Log($"you have added a {inventoryItem.itemSO.name} / {item.itemSO.name} to the inventory with amount {inventoryItem.amount}");
            }
            else 
            {
                itemList.Add(item);
                //Debug.Log("the item was not in the list. we have added the item to the list");
                //Debug.Log($"you have added a {item} to the inventory with amount {item.amount}");
            }
        }
        
        Debug.Log("the list in the inventory now is: ");
        foreach (Item itemK in itemList)
        {
            Debug.Log(itemK.itemSO.name);
        }
    }

    public bool IsRecollectableInList(Item itemToCheck)
    {
        foreach (Item item in itemList)
        {
            if(item.itemSO == itemToCheck.itemSO)
            {
                //item.amount += itemToCheck.amount;
                return true;
            }
        }
        return false;
    }
}
