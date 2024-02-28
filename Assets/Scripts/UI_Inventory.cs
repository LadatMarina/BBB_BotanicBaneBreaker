using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Player player;

    private VillageDisplay villageDisplay;

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

        HideAllChildren();
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.House)
        {
            if (villageDisplay == null)
            {
                villageDisplay = FindObjectOfType<VillageDisplay>();
            }
        }
        //Debug.Log("the recollectableSavedForUsing is not longer null");
        //villageDisplay.potion = recollectableSavedForUsing;
        //villageDisplay.RefreshPotionField();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        //RefreshUiInventory();
    }

    public void HideAllChildren()
    {
        // hide all the buttons
        foreach (Transform child in panelBackground.transform)
        {
            child.gameObject.SetActive(false);
            //set active all the button inventory components
            foreach (Transform child2 in child)
            {
                child2.gameObject.SetActive(true);
            }
        }
    }

    public void RefreshItems()
    {
        //1st acces to the saved inventory
        itemList = DataPersistanceManager.Instance.LoadInventory();
        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;
        for (int i = 0; i < itemList.Count; i++)
        {
            switch (sceneBuildIndex)
            {
                case (int)SceneIndex.House:
                    //Item potionItem = itemList[i];

                    if (itemList[i].itemSO.recollectableType == RecollectableType.healthPotion)
                    {
                        Transform potionButton = panelBackground.transform.GetChild(i);

                        potionButton.gameObject.SetActive(true);

                        RefreshButton(itemList[i], potionButton);

                        //Debug.Log("the potion has been showed");
                    }
                    else
                    {
                        Debug.Log("there's no potions to show");
                    }
                    break;

                case (int)SceneIndex.GamePlay:
                    //Item item = itemList[i];

                    Transform recollectableButton = panelBackground.transform.GetChild(i);

                    recollectableButton.gameObject.SetActive(true);

                    RefreshButton(itemList[i], recollectableButton);
                    break;
            }
            
        }
    }

    private void RefreshButton(Item item, Transform recollectableButton)
    {
        //visual
        recollectableButton.transform.GetChild(0).GetComponent<Image>().sprite = item.itemSO.sprite; //set the sprite
        TextMeshProUGUI text = recollectableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>(); //set the amount text
        text.SetText($"{item.amount}");

        //logic
        Button buttonComponent = recollectableButton.gameObject.GetComponent<Button>(); //get the button fot the add listener
        //delete inventoryButtonItem = recollectableButton.GetComponent<delete>();

        //this can be deleted, it's only for knowing if the button has the correct recollectable asigned
        //inventoryButtonItem.recollectable = item.itemSO;

        //first we save the item reference and then reset the functions the button has
        Item localItem = item;
        buttonComponent.onClick.RemoveAllListeners();
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        switch (buildIndex)
        {
            case (int)SceneIndex.House:
                buttonComponent.onClick.AddListener(() => villageDisplay.ChooseThePotionToGive(item));
                //buttonComponent.onClick.AddListener(()=> villageDisplay.ProvasionDelete(5));
                break;
            case (int)SceneIndex.GamePlay:
                buttonComponent.onClick.AddListener(() => DropItem(localItem, recollectableButton));
                break;
        }
        
    }

    public void DropItem(Item item, Transform recollectableButton)
    {
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        int i = 0;
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
                player.GetInventory().GetItemList().Remove(item); //remove directly from the player bc only can drop an item if the player is in the scene

                // Refresh the UI inventory by hiding the button where the element was
                recollectableButton.gameObject.SetActive(false);

                break;
            }
            else
            {
                i++;
                if(i>= directions.Length)
                {
                    //AQUÍ HE DE POSAR QUE QUANT DONI FALS A SES 4 DIRECCIONS, ARA ME DIU QUE NO HI HA ESPAI A SA PRIMERA QUE COMPROVA
                    // If no space is available in any direction, log an error message
                    Debug.Log("The item could not be instantiated, there's no space");
                }
            }
        }
        //after a change is made, the inventory is saved to the Json file
        DataPersistanceManager.Instance.SaveInventory(player.GetInventory().GetItemList());
    }

    public void ToggleInventoryButton()
    {
        //PLAY AN SFX WHEN RECOLLECT
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sound3);

        //GameObject inventoryPanelBackground = UI_Inventory.Instance.panelBackground;

        if (panelBackground.activeInHierarchy) //if it's true, will be closed so we have to destroy all the elements
        {
            panelBackground.SetActive(false);
            HideAllChildren();
            //Debug.Log("all the childrens of the panel background have been destroyed");
        }
        else
        {
            panelBackground.SetActive(true);
            RefreshItems();
            //Debug.Log("the inventory has been refreshed with the item list");
        }
    }
    public void HideInventory()
    {
        panelBackground.SetActive(false);
    }

}
