using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public List<Recollectable> GetRecolletableList()
    {
        return recollectableList;
    }

    public void AfegirItem(Item item)
    {
        itemList.Add(item);
    }
    public List<Item> ObtenirLlistaItems()
    {
        return itemList;
    }
}
