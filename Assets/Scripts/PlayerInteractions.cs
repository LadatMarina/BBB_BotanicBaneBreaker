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
        inventory = player.GetInventory();
        gameInput = FindObjectOfType<GameInput>();  
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        string tag = other.gameObject.tag;

        switch(tag) {
            case "house_1":
                Debug.Log(gameInput.GetInputVectorNormalized());
                //save the position of the player and the last facing (the last facing ara no és necessari pq totes ses portes per poder entrar estaràs mirant cap adalt)
                GameManager.Instance.SetLastPLayerPos(player.GetPlayerPos());
                
                GameManager.Instance.LoadHouseScene(GameAssets.Instance.paco);
                
                break;
            case "house_2":
                GameManager.Instance.LoadHouseScene(GameAssets.Instance.maria);
                Debug.Log(gameInput.GetInputVectorNormalized());

                break;
            case "house_3":
                GameManager.Instance.LoadHouseScene(GameAssets.Instance.bel);
                Debug.Log(gameInput.GetInputVectorNormalized());

                break;
            case "house_4":
                GameManager.Instance.LoadHouseScene(GameAssets.Instance.toni);
                Debug.Log(gameInput.GetInputVectorNormalized());

                break;

            case "recollectable":
                Item otherItem = other.gameObject.GetComponent<RecollectableDisplay>().GetItem();

                if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.sound4); //PLAY AN SFX WHEN RECOLLECT
                    player.inventory.AddItem(otherItem);
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.LogError("something gone wrong, the other game object is null");
                }
                break;
            case "kitchen":
                GameManager.Instance.LoadKitchen();
                Debug.Log(gameInput.GetInputVectorNormalized());

                break;
        }
    }
}
