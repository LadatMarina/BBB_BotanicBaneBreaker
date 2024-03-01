using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInteractions : MonoBehaviour
{
    public Player player;
    public Inventory inventory;

    public Item itemScript;
    public GameInput gameInput;

    private Item otherItem;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        inventory = player.GetInventory();
        gameInput = FindObjectOfType<GameInput>();  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;

        switch(tag) {
            case "house_1":
                //save the position of the player
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                DataPersistanceManager.Instance.SaveVillage(GameAssets.Instance.paco);
                Loader.Load(SceneIndex.House);
                
                break;
            case "house_2":
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                DataPersistanceManager.Instance.SaveVillage(GameAssets.Instance.maria);
                Loader.Load(SceneIndex.House);
                break;

            case "house_3":
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                DataPersistanceManager.Instance.SaveVillage(GameAssets.Instance.bel);
                Loader.Load(SceneIndex.House);
                break;
            
            case "house_4":
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                DataPersistanceManager.Instance.SaveVillage(GameAssets.Instance.toni);
                Loader.Load(SceneIndex.House); ;
                break;

            case "recollectable":

                otherItem = other.gameObject.GetComponent<RecollectableDisplay>().GetItem();

                //if the list has less than 5 items, can recolect
                if (player.GetInventory().GetItemList().Count <= 4) 
                {
                    if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
                    {
                        RecollectAnItem(other);
                    }
                    else
                    {
                        Debug.LogError("something gone wrong, the other game object is null");
                    }
                }
                //if the list has 5...
                else 
                {
                    //but the item I want to recolect is in the list
                    if (!player.GetInventory().IsRecollectableInList(otherItem))
                    {
                        RecollectAnItem(other);
                    }
                    else
                    {
                        //do not recolect the item --> let the player collision with it
                        Collider2D otherCollider = other.gameObject.GetComponent<Collider2D>();
                        otherCollider.isTrigger = false; 
                    }

                }

                break;
            case "kitchen":
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                //GameManager.Instance.LoadKitchen();
                break;
        }
    }

    private void RecollectAnItem(Collider2D other)
    {
        Collider2D otherCollider = other.gameObject.GetComponent<Collider2D>();
        otherCollider.isTrigger = true;

        SoundManager.Instance.PlaySFX(SoundManager.Instance.sound4); //PLAY AN SFX WHEN RECOLLECT
        player.AddItem(otherItem);
        Destroy(other.gameObject);
    }
}
