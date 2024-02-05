using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public int amount;
    public Recollectable itemSO; //sa meva info esta`tica
    
    public void ResetAmount()
    {
        amount = 0;
    }

    public int GetAmount()
    {
        return amount;
    }

    public void AddOneToAmount()
    {
        amount++;
        Debug.Log($"THE AMOUNT HAS BEEN CHANGED!! the amount now is {amount}");
    }

    public Item GetItem() 
    {
        return this;
    }

    public Recollectable GetItemSO() 
    {
        return itemSO;
    }
}
