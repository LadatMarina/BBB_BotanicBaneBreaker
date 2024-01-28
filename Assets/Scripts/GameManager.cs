using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private Inventory inventory;
    public UI_Inventory uiInventory;

    private void Awake()
    {
        inventory = new Inventory();
        ///i am making here because now the inspector don't let 
        ///me asign by hand the script,as soon as i can, i will set 
        ///the inventory in the awake 
        uiInventory = FindAnyObjectByType<UI_Inventory>(); 
    }
    private void Start()
    {
        uiInventory.SetInventory(inventory); //send the inventory just created
    }
}
