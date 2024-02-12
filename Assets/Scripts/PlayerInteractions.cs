using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerInteractions : MonoBehaviour
{
    public Player player;
    public Inventory inventory;

    public Item itemScript;

    public GameManager gameManager;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        inventory = player.GetInventory();
        gameManager = FindObjectOfType<GameManager>();  
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

        string tag = other.gameObject.tag;
        switch(tag) {
            case "house_1":
                gameManager.LoadHouseScene((int)SceneIndex.Villagers, gameManager.village);
                break;
            case "house_2":
                ///
                break;
            case "house_3":
                ///
                break;
            case "house_4":
                ///
                break;
        }
        
    }
}
