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

    void Awake()
    {
        player = FindObjectOfType<Player>();
        inventory = GameManager.Instance.GetInventory();
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
                Item otherItem = other.gameObject.GetComponent<RecollectableDisplay>().GetItem();

                if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.sound4); //PLAY AN SFX WHEN RECOLLECT
                    GameManager.Instance.inventory.AddItem(otherItem);
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.LogError("something gone wrong, the other game object is null");
                }
                break;
            case "kitchen":
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                //GameManager.Instance.LoadKitchen();
                break;
        }
    }
}
