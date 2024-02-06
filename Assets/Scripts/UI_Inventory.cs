using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Player player;

    //asignar per inspector
    [SerializeField] private GameObject recollectableButtonPrefab;
    [SerializeField] private GameObject panelBackground;
    //[SerializeField] private TextMeshPro amountText;

    private List<Item> itemList;

    public Sprite testingSprite;
    //private Transform containerTemplate;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();

        inventory = player.GetInventory();
    }
    
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        //RefreshUiInventory();
    }

    public void RefreshUiInventory()
    {
        //fist destroy all the childs in the panel backround
        DestroyAllChildren(panelBackground);
        //then instantiate a new version of the prefab with the information of the adquired item (with the set item
        //FillInventory();
    }

    public void DestroyAllChildren(GameObject parent)
    {
        //while the parent has some child, they will be destroyed.
        for (int i = 0; parent.transform.GetChild(0) == null; i++)
        {
            Transform child = parent.transform.GetChild(i);
            Destroy(child.gameObject);
            Debug.Log("destroyed!");
        }
    }

    public void FillInventory(Transform parent, List<Item> itemList)
    {
        //canvair mecànica a sa de activar/desactivar es objectes

        if(itemList.Count > 0)
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


    
    public void ToggleInventoryButton()
    {
        if (player.GetInventory().GetItemList().Count > 0)
        {
            if (panelBackground.activeInHierarchy == true) //if it's true, will be closed so we have to destroy all the elements
            {
                panelBackground.SetActive(false);
                HideAllChildren();
                Debug.Log("all the childrens of the panel background have been destroyed");
            }
            else
            {
                panelBackground.SetActive(true);
                RefreshItems();
                    
                Debug.Log("the inventory has been refreshed with the item list");
            }
        }
        else
        {
            Debug.Log("the inventory is empty");
        }

    }

    private void HideAllChildren()
    {
        foreach (Transform child in recollectableButtonPrefab.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    private void RefreshItems()
    {
        // HideAllChildren();
        itemList = player.GetInventory().GetItemList();

        for (int i = 0; i < itemList.Count; i++)
        {
            Item item = itemList[i];

            Transform recollectableButton = panelBackground.transform.GetChild(i);
            Debug.Log($"element {i} has been set active");
            recollectableButton.gameObject.SetActive(true);

            RefreshButton(item, recollectableButton);
        }
    }

    private void RefreshButton(Item item, Transform recollectableButton)
    {
        //visual
        recollectableButton.transform.GetChild(0).GetComponent<Image>().sprite = item.itemSO.sprite;
        TextMeshProUGUI text = recollectableButton.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        text.SetText($"{item.amount}");
        //= $"{item.amount}"
        //logic
        Button buttonComponent = recollectableButton.gameObject.GetComponent<Button>();
        buttonComponent.onClick.AddListener(() => ShowPopUp(item.itemSO));
    }

    public void ShowPopUp(Recollectable itemSO)
    {
        //show the pop up panel --> with the name of the recollectable that
        //the button represents --> we need the reference to the item because
        // the buttons of the pop up will drop the element (like a direct reference to not complicate all) or show the description 
        Debug.Log("now the pop up should shown");

    }
}
