using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class Item
{
    public int amount;
    public Recollectable itemSO; 
    
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
    }

    public void IncreaseDrecreaseAmount(int amountToUse)
    {
        amount = amountToUse + amount;
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
