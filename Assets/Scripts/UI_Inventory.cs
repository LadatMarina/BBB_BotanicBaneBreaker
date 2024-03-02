using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private Player player;

    private VillageDisplay villageDisplay;

    //asignar per inspector
    public GameObject recollectableButtonPrefab;
    public GameObject panelBackground;

    //asined in the inspector
    public Button inventoryButton;

    private List<Item> itemList;

    public Sprite testingSprite;
    //private Transform containerTemplate;

    public GameObject popUpPanel;
    private GameObject defaultSelectedButton;
    public IngredientsSpawner ingredientSpawner;

    public GameObject prefabInventoryButton;

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

        DestroyAllChildren();

        //inventoryButton.onClick.AddListener(() => EventSystem.current.SetSelectedGameObject(FindFirstButtonActive().gameObject));
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
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.GamePlay)
        {
            if (player == null)
            {
                player = FindAnyObjectByType<Player>();
            }
        }
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryButton();
        }

    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        //RefreshUiInventory();
    }

    //public void HideAllChildren()
    //{
    //    // hide all the buttons
    //    foreach (Transform child in panelBackground.transform)
    //    {
    //        child.gameObject.SetActive(false);
    //        //set active all the button inventory components
    //        foreach (Transform child2 in child)
    //        {
    //            child2.gameObject.SetActive(true);
    //        }
    //    }
    //}
    public void DestroyAllChildren()
    {
        // destroy all the buttons
        foreach (Transform child in panelBackground.transform)
        {
            Destroy(child.gameObject);
        }
    }

    public void RefreshItems()
    {
        DestroyAllChildren();
        //1st acces to the saved inventory
        itemList = player.GetInventory().GetItemList();

        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        for (int i = 0; i < itemList.Count; i++)
        {
            switch (sceneBuildIndex)
            {
                case (int)SceneIndex.House:

                    //show only the health potions in the inventory
                    if (itemList[i].itemSO.recollectableType == RecollectableType.healthPotion)
                    {
                        Transform potionButton = Instantiate(prefabInventoryButton.transform, panelBackground.transform); //create the button

                        RefreshButton(itemList[i], potionButton);
                    }
                    else
                    {
                        Debug.Log("there's no potions to show");
                    }
                    break;

                case (int)SceneIndex.GamePlay:
                    Transform recollectableButton = Instantiate(prefabInventoryButton.transform, panelBackground.transform); //create the button

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

        //first we save the item reference and then reset the functions the button has
        //Item localItem = item;
        buttonComponent.onClick.RemoveAllListeners();
        int buildIndex = SceneManager.GetActiveScene().buildIndex;
        switch (buildIndex)
        {
            case (int)SceneIndex.House:
                buttonComponent.onClick.AddListener(() => villageDisplay.ChooseThePotionToGive(item));
                //buttonComponent.onClick.AddListener(()=> villageDisplay.ProvasionDelete(5));
                break;
            case (int)SceneIndex.GamePlay:
                buttonComponent.onClick.AddListener(() => DropItem(item, recollectableButton)); //aqui anava localItem, mira si funciona
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

                //remove directly bc will drop all the amount and has to save the inventory
                player.RemoveItem(item); 

                // Refresh the UI inventory by hiding the button where the element was 
                //AQUI HAURIA DE FER UN REFRESH A TOT
                //RefreshItems();
                recollectableButton.gameObject.SetActive(false);

                //everytime I drop an item, the selected button has to change
                FindFirstButtonActive();
                //EventSystem.current.SetSelectedGameObject(defaultSelectedButton);

                break;
            }
            else
            {
                i++;
                if(i>= directions.Length)
                {
                    //AQU� HE DE POSAR QUE QUANT DONI FALS A SES 4 DIRECCIONS, ARA ME DIU QUE NO HI HA ESPAI A SA PRIMERA QUE COMPROVA --> textMeshPro
                    // If no space is available in any direction, log an error message
                    Debug.Log("The item could not be instantiated, there's no space");
                }
            }
        }
    }

    public void ToggleInventoryButton()
    {
        //PLAY AN SFX WHEN RECOLLECT
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sound3);

        if (panelBackground.activeInHierarchy) //if it's true, will be closed so we have to destroy all the elements
        {
            GameInput.Instance.EnablePlayerInputActions();
            panelBackground.SetActive(false);
            DestroyAllChildren();
            EventSystem.current.SetSelectedGameObject(null); // reset the selected button
            //Debug.Log("all the childrens of the panel background have been destroyed");
        }
        else
        {
            GameInput.Instance.DisablePlayerInputActions();
            panelBackground.SetActive(true);
            RefreshItems();
            FindFirstButtonActive();
            //EventSystem.current.SetSelectedGameObject(defaultSelectedButton);
            //Debug.Log("the inventory has been refreshed with the item list");
        }
    }
    public void HideInventory()
    {
        panelBackground.SetActive(false);
    }

    private void FindFirstButtonActive()
    {
        foreach (Transform child in panelBackground.GetComponentInChildren<Transform>())
        {
            if (child.gameObject.activeInHierarchy)
            {
                EventSystem.current.SetSelectedGameObject(child.gameObject);
                break;
            }
            else
            {
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

}
