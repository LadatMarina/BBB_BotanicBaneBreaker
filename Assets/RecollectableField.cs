using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RecollectableField : MonoBehaviour
{
    Inventory inventory;
    List<Item> itemList;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            //inventory = Player.Instance.GetInventory();
            //itemList = Player.Instance.GetInventory().GetItemList();
            int count = Player.Instance.GetInventory().GetItemList().Count;
            Debug.Log(count);
            Debug.Log("DELETE --> count saved as player.itemlist.count");
        }
    }

}
