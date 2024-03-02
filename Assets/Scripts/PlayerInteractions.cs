using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInteractions : MonoBehaviour
{
    public Inventory inventory;

    public Item itemScript;
    public GameInput gameInput;

    private Item otherItem;
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        //inventory = player.GetInventory();
        //gameInput = FindObjectOfType<GameInput>();  
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
                Loader.Load(SceneIndex.House); 
                break;

            case "recollectable":
                Debug.Log(other.gameObject.GetComponent<RecollectableDisplay>().GetItem().itemSO.name);


                otherItem = other.gameObject.GetComponent<RecollectableDisplay>().GetItem();

                //if the list has less than 5 items, can recolect
                if (player.GetInventory().GetItemList().Count <= 4) 
                {
                    Debug.Log("can recolect, the count list is under 4");
                    if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
                    {
                        Debug.Log("HAS recolected");

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
                    if ((player.IsRecollectableInList(otherItem)) && (otherItem.itemSO.stackable))
                    {
                        Debug.Log("can recolect, the count list is = 4, BUT!! --> The item is in the inventory");
                        Debug.Log(other.gameObject.GetComponent<RecollectableDisplay>().GetItem().itemSO.name);

                        RecollectAnItem(other);
                    }
                    else
                    {
                        Debug.Log("can'T recolect, list.count > 4 and the item is not in the inventory");
                        //do not recolect the item --> let the player collision with it
                        Collider2D otherCollider = other.gameObject.GetComponent<Collider2D>();
                        otherCollider.isTrigger = false; 
                    }

                }

                break;
            case "kitchen":
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                Loader.Load(SceneIndex.Kitchen);

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
        PlayerUI playerUI = player.gameObject.GetComponentInChildren<PlayerUI>(); ; //no crec que funcioni, crec que ho hauré de fer primer es parent i dsps get component in child
        playerUI.HideRecollectableNameText();
        Destroy(other.gameObject);

    }


}
