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


}
