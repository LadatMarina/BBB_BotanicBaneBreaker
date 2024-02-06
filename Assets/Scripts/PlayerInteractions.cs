using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    public Player player;
    public Inventory inventory;

    public Item itemScript;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        inventory = player.GetInventory();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Item otherItem = other.gameObject.GetComponent<RecollectableDisplay>().GetItem();

        if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
        {
            player.inventory.AddItem(otherItem);
            Destroy(other.gameObject);
        }
        else
        {
            Debug.LogError("something gone wrong, the other game object is null");
        }
        
    }
}
