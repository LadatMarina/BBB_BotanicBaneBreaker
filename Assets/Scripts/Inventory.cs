using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private List<Recollectable> recollectableList;

    public Inventory()
    {
        recollectableList = new List<Recollectable>();
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
}
