using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField]private int amount;
    //[SerializeField]private Recollectable itemSO; //sa meva info esta`tica
    
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
        Debug.Log($"the amount now is {amount}");
    }

    /*public Recollectable GetItemSO() 
    {

        return itemSO;
    }*/
}
