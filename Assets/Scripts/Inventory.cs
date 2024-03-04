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
        if ((itemList != null) && (itemList.Count >= 0))
        {
            for (int i = 1; i <= itemList.Count; i++)
            {
                //if the item was already, amount+ / true
                if (item.itemSO == itemList[i].itemSO)
                {
                    itemList[i].amount = itemList[i].amount + item.amount;
                }
                else
                {
                    itemList.Add(item);
                }
            }
        }
        else
        {
            Debug.LogWarning("Item list null, can't add the item");
        }
    }
}
