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
    }
    public List<Item> GetItemList() { return itemList; }

    public void SetItemList(List<Item> listToSave) { itemList = listToSave; }

    public void AddItem(Item item)
    {
        if (itemList != null)
        {
            if (itemList.Count < 0)
            {
                Debug.Log("itemList.of the inventory is less than 0");
            }
            Debug.Log("addItem / inventory");
            for (int i = 1; i <= itemList.Count; i++)
            {
                //if the item was already, amount+ / true
                if (item.itemSO == itemList[i].itemSO)
                {
                    itemList[i].amount = itemList[i].amount + item.amount;

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
        }
        else
        {
            Debug.Log("itemList NULL /inventory class");
        }

        //foreach (Item inventoryItem in itemList)
        //{
        //if the item was already, amount+ / true
        //if (item.itemSO == inventoryItem.itemSO)
        //{
        //    inventoryItem.amount = inventoryItem.amount + item.amount;

        //    //Debug.Log("due to the item was already in the list, we only have increased the sum of the amount");
        //    //Debug.Log($"you have added a {inventoryItem.itemSO.name} / {item.itemSO.name} to the inventory with amount {inventoryItem.amount}");
        //}
        //else
        //{
        //    itemList.Add(item);
        //    //Debug.Log("the item was not in the list. we have added the item to the list");
        //    //Debug.Log($"you have added a {item} to the inventory with amount {item.amount}");
        //}
        //}   
        //}

        Debug.Log("the list in the inventory now is: ");
        foreach (Item itemK in itemList)
        {
            Debug.Log(itemK.itemSO.name);
        }
    }

    public void Alternative(Item item, List<Item> itemListAlternative)
    {
        itemListAlternative.Add(item);
    }
}
