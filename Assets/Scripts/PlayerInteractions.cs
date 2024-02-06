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

            /*switch (otherItem.itemSO.recollectableType)
            {
                case RecollectableType.attackPotion:
                    Debug.Log($"you have recollected an attack potion named {other.gameObject.name}");
                    break;
                case RecollectableType.healthPotion:
                    Debug.Log($"you have recollected an health potion named {other.gameObject.name}");
                    break;
                case RecollectableType.ingredients:
                    Debug.Log($"you have recollected an ingredient named {other.gameObject.name}");
                    break;

            }*/
            Destroy(other.gameObject);
        }
        else
        {
            Debug.LogError("something gone wrong, the other game object is null");
        }
        
    }
}
