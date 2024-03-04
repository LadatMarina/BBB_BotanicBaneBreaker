using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerInteractions : MonoBehaviour
{
    private Item otherItem;
    Player player;

    void Awake()
    {
        player = FindObjectOfType<Player>();
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
                    //but the item to recolect is in the list
                    if ((player.IsRecollectableInList(otherItem)) && (otherItem.itemSO.stackable))
                    {
                        Debug.Log(other.gameObject.GetComponent<RecollectableDisplay>().GetItem().itemSO.name);

                        RecollectAnItem(other);
                    }
                    else 
                    {
                        ExplanationManagerUI.Instance.ShowAnExplanation("Your inventory is full, can't recollect more new objects.", 10);
                        //do not recolect the item --> let the player collision with it
                        Collider2D otherCollider = other.gameObject.GetComponent<Collider2D>();
                        otherCollider.isTrigger = false; 
                    }

                }

                break;
            case "kitchen":
                if(player.GetInventory().GetItemList().Count >= 5)
                {
                    ExplanationManagerUI.Instance.ShowAnExplanation("Your inventory is full. Try to drop something in order to make space.", 15);
                }
                else
                {
                    GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                    Loader.Load(SceneIndex.Kitchen);
                }
                break;
        }
    }

    private void RecollectAnItem(Collider2D other)
    {
        Collider2D otherCollider = other.gameObject.GetComponent<Collider2D>();
        otherCollider.isTrigger = true;

        SoundManager.Instance.PlaySFX(SoundManager.Instance.recolectSound); //PLAY AN SFX WHEN RECOLLECT
        player.AddItem(otherItem);
        Destroy(other.gameObject);
    }
}
