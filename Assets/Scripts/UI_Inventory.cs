using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Player player;

    private VillageDisplay villageDisplay;
    public bool hasSelectedAPotion;

    //asignar per inspector
    [SerializeField] private GameObject recollectableButtonPrefab;
    public GameObject panelBackground;


    //[SerializeField] private TextMeshPro amountText;

    private List<Item> itemList;

    public Sprite testingSprite;
    //private Transform containerTemplate;

    public GameObject popUpPanel;

    public IngredientsSpawner ingredientSpawner;


    public static UI_Inventory Instance { get; private set; }

    private void Awake()
    {
        //singleton
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        //-----------------------------

        player = FindAnyObjectByType<Player>();

        inventory = player.GetInventory();
        ingredientSpawner = FindAnyObjectByType<IngredientsSpawner>();

        if(SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Villagers)
        {
            villageDisplay = FindObjectOfType<VillageDisplay>();
            hasSelectedAPotion = false;
        }
    }


    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        //RefreshUiInventory();
    }

    public void FillInventory(Transform parent, List<Item> itemList)
    {
        //canvair mecànica a sa de activar/desactivar es objectes

        if (itemList.Count > 0)
        {
            for (int i = 0; i < itemList.Count; i++)
            {
                //instantiate a child inside the parent
                GameObject instantiation = Instantiate(recollectableButtonPrefab, parent);

                //get the sprite of the just instantiated
                Transform image = instantiation.gameObject.transform.GetChild(0);
                Sprite sprite = image.GetComponent<Sprite>();

                //set that this sprite is the same as the inventory item sprite
                sprite = itemList[i].GetItemSO().sprite;
            }
        }
        else
        {
            Debug.Log("cannot fill the inventory because there's nothing in");
        }
    }



    public void HideAllChildren()
    {
        foreach (Transform child in recollectableButtonPrefab.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void RefreshItems()
    {
        itemList = player.GetInventory().GetItemList();
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < itemList.Count; i++)
        {
            switch (sceneBuildIndex)
            {
                case (int)SceneIndex.Villagers:
                    Item potionItem = itemList[i];
                    if (potionItem.itemSO.recollectableType == RecollectableType.healthPotion)
                    {
                        Transform potionButton = panelBackground.transform.GetChild(i);

                        potionButton.gameObject.SetActive(true);

                        RefreshButton(potionItem, potionButton);
                        Debug.Log("potion has been showed");
                    }
                    else
                    {
                        Debug.Log("there's no potions to show");
                    }
                    break;

                case (int)SceneIndex.GamePlay:
                    Item item = itemList[i];

                    Transform recollectableButton = panelBackground.transform.GetChild(i);
                    //Debug.Log($"element {i} has been set active");
                    recollectableButton.gameObject.SetActive(true);

                    RefreshButton(item, recollectableButton);
                    break;
            }
            
        }
    }

    private void RefreshButton(Item item, Transform recollectableButton)
    {
        //visual
        recollectableButton.transform.GetChild(0).GetComponent<Image>().sprite = item.itemSO.sprite;
        TextMeshProUGUI text = recollectableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text.SetText($"{item.amount}");

        //logic
        Button buttonComponent = recollectableButton.gameObject.GetComponent<Button>();

        //first we save the item reference and then reset the functions the button has
        Item localItem = item;
        buttonComponent.onClick.RemoveAllListeners();
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        switch (buildIndex)
        {
            case (int)SceneIndex.Villagers:
                buttonComponent.onClick.AddListener(() => ChooseThePotionToGive(localItem));
                break;
            case (int)SceneIndex.GamePlay:
                buttonComponent.onClick.AddListener(() => DropItem(localItem, recollectableButton));
                break;
        }
        
    }

    public void DropItem(Item item, Transform recollectableButton)
    {
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };

        foreach (Vector2 dropDirection in directions)
        {
            if (!player.RecollectableInFrontOf(dropDirection)) // si no hi ha res quant dropDirection, instancia i atura es bulce
            {
                //PLAY AN SFX WHEN RECOLLECT
                SoundManager.Instance.PlaySFX(SoundManager.Instance.sound4); 

                // Drop the item to the world
                GameObject newItem = ingredientSpawner.CreateNewItem(item.itemSO, player.transform.position + (Vector3)dropDirection * 2, item.amount);
                newItem.GetComponent<Rigidbody2D>().AddForce(dropDirection, ForceMode2D.Impulse);

                // Remove the item from the list
                player.Remove(item);

                // Refresh the UI inventory by hiding the button where the element was
                recollectableButton.gameObject.SetActive(false);

                break;
            }
        }

        // If no space is available in any direction, log an error message
        Debug.Log("The item could not be instantiated, there's no space");
    }

    public void ChooseThePotionToGive(Item item)
    {
        //if the player has not selected a potion from the inventory, will add the
        //item that the button represents to the field potion in the village display and hide the inventoy
        if(hasSelectedAPotion == false)
        {
            //villageDisplay.potion = item.itemSO;
            villageDisplay.SetPotion(item);
            GameManager.Instance.ToggleInventoryButton();
            hasSelectedAPotion = true;
        }
    }


}
