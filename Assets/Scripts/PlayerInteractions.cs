using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


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
        string tag = other.gameObject.tag;

        switch(tag) {
            case "house_1":
                GameManager.Instance.LoadHouseScene(GameAssets.Instance.paco);
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

            case "recollectable":
                Item otherItem = other.gameObject.GetComponent<RecollectableDisplay>().GetItem();

                if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
                {
                    SoundManager.Instance.PlaySFX(SoundManager.Instance.sound1); //PLAY AN SFX WHEN RECOLLECT
                    player.inventory.AddItem(otherItem);
                    Destroy(other.gameObject);
                }
                else
                {
                    Debug.LogError("something gone wrong, the other game object is null");
                }
                break;
        }
    }
}
