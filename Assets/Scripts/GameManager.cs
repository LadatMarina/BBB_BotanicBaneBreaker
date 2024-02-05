using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Inventory inventory;
    public UI_Inventory uiInventory;

    public Recollectable recollectableTesting;
    public Recollectable recollectableTesting1;

    private void Awake()
    {
        inventory = new Inventory();

        ///i am making here because now the inspector don't let 
        ///me asign by hand the script,as soon as i can, i will set 
        ///the inventory in the awake 
        
        //uiInventory = FindAnyObjectByType<UI_Inventory>(); 
    }
    private void Start()
    {
        inventory.AddItem(new Item { amount = 10, itemSO = recollectableTesting });
        inventory.AddItem(new Item { amount = 2, itemSO = recollectableTesting1 });

        Debug.Log("the count of the list now is " + inventory.GetItemList().Count);
    }

    public Inventory GetInventory()
    {
        return inventory;
    }
}
