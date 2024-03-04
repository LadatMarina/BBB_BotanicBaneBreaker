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
    private KitchenManager kitchenManager;
    private GameInput gameInput;

    //asignar per inspector
    public GameObject recollectableButtonPrefab;
    public GameObject panelBackground;

    //asined in the inspector
    public Button inventoryButton;

    private List<Item> itemList;

    public Sprite testingSprite;
    //private Transform containerTemplate;

    public GameObject popUpPanel;
    public IngredientsSpawner ingredientSpawner;

    public GameObject prefabInventoryButton;

    public static UI_Inventory Instance { get; private set; }

    private void Awake()
    {
        //singleton
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        player = FindAnyObjectByType<Player>();
        inventory = player.GetInventory();
        DestroyAllChildren();
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
        if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.Kitchen)
        {
            if (kitchenManager == null)
            {
                kitchenManager = FindObjectOfType<KitchenManager>();
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
        if(gameInput == null)
        {
            gameInput = FindObjectOfType<GameInput>();
        }
        if(ingredientSpawner == null)
        {
            ingredientSpawner = FindAnyObjectByType<IngredientsSpawner>();
        }

    }

    public void SetInventory(Inventory inventory) { this.inventory = inventory; }
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

        int sceneBuildIndex = SceneManager.GetActiveScene().buildIndex;

        //1st acces to the saved inventory
        if (sceneBuildIndex != (int)SceneIndex.GamePlay)
        {
            itemList = DataPersistanceManager.Instance.LoadInventory();
        }
        else
        {
            itemList = player.GetInventory().GetItemList();
        }

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

                case (int)SceneIndex.Kitchen:
                    if (itemList[i].itemSO.recollectableType == RecollectableType.ingredients) //show only the ingredients avaliables
                    {
                        Transform potionButton = Instantiate(prefabInventoryButton.transform, panelBackground.transform); //create the button
                        RefreshButton(itemList[i], potionButton);
                    }
                    else
                    {
                        Debug.LogWarning("there's no ingredients to show");
                    }
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
                break;

            case (int)SceneIndex.GamePlay:
                buttonComponent.onClick.AddListener(() => DropItem(item, recollectableButton)); 
                break;

            case (int)SceneIndex.Kitchen:
                buttonComponent.onClick.AddListener(() => kitchenManager.ChooseIngredient(item));
                break;
        }
        
    }

    public void DropItem(Item item, Transform recollectableButton)
    {
        Vector2[] directions = { Vector2.up, Vector2.right, Vector2.down, Vector2.left };
        int i = 0;
        foreach (Vector2 dropDirection in directions)
        {
            if (!player.SomethingInFrontOf(dropDirection)) 
            {
                //PLAY AN SFX WHEN RECOLLECT
                SoundManager.Instance.PlaySFX(SoundManager.Instance.dropSound); 

                // Drop the item to the world
                GameObject newItem = GameManager.Instance.CreateNewItem(item.itemSO, player.transform.position + (Vector3)dropDirection * 2, item.amount);
                newItem.GetComponent<Rigidbody2D>().AddForce(dropDirection, ForceMode2D.Impulse);

                //remove directly bc will drop all the amount and has to save the inventory
                player.RemoveItem(item); 

                // Refresh the UI inventory by hiding the button where the element was 
                recollectableButton.gameObject.SetActive(false);

                //everytime I drop an item, the selected button has to change
                FindFirstButtonActive();

                break;
            }
            else
            {
                i++;
                if(i>= directions.Length)
                {
                    // If no space is available in any direction, log an error message
                    Debug.LogWarning("The item could not be instantiated, there's no space");
                }
            }
        }
    }

    public void ToggleInventoryButton()
    {
        //PLAY AN SFX WHEN RECOLLECT
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);

        if (panelBackground.activeInHierarchy) //if it's true, will be closed so we have to destroy all the elements
        {
            if (SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.GamePlay) { gameInput.EnablePlayerInputActions(); }

            panelBackground.SetActive(false);
            DestroyAllChildren();
            EventSystem.current.SetSelectedGameObject(null); // reset the selected button
            //Debug.Log("all the childrens of the panel background have been destroyed");
        }
        else
        {   
            if(SceneManager.GetActiveScene().buildIndex == (int)SceneIndex.GamePlay) { gameInput.DisablePlayerInputActions(); }
            
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
