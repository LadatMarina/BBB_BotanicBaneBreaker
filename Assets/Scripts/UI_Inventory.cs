using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    //posar això serialized i llevar es Find
    private Inventory inventory;


    //no estic segura de necessitar això:
    [SerializeField] private Transform UiInventory;
    private GameManager gameManager;

    //asignar per inspector
    [SerializeField] private GameObject recollectableButtonPrefab;
    [SerializeField] private GameObject panelBackground;

    private List<Item> itemList;

    //private Transform containerTemplate;

    private void Awake()
    {
        gameManager = FindAnyObjectByType<GameManager>();

        inventory = gameManager.GetInventory();

        itemList = inventory.GetItemList();
        /*make a constant of this
        recollectableContainer = transform.Find("recollectableContainer");
        recollectableContainer = transform.Find("containerTemplate");*/

    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        //RefreshInventoryRecollectables();
        RefreshUiInventory();
    }

    /*public void RefreshInventoryRecollectables()
    {
        //update all the components of the list with a foreach, to update one by one
        foreach (Recollectable recollectable in inventory.GetRecolletableList())
        {
            int x = 0;
            int y = 0;
            int recollectableSize = 85; //here's 5points bigger in order to separate a little bit the items each other
            //syntax --> Instantiate(Object original, Transform parent);
            // in evry intantiation of a new containerTemplate we get it's rect transform to modify it's position and set the element active
            RectTransform recollectableRectTransform = Instantiate(containerTemplate, recollectableContainer).GetComponent<RectTransform>();
            recollectableRectTransform.gameObject.SetActive(true);
             
            recollectableRectTransform.anchoredPosition = new Vector2(x * recollectableSize, y * recollectableSize);
            x++;
            if (x > 4)
            {
                x = 0;
                y++;
            }
        }
    }

    public void Refrescar()
    {
        List<Item> itemlsit2 = inventory.ObtenirLlistaItems();
        foreach(Item item in itemlsit2)
        {
            //
            //Sprite spriteOfTheScroptableObject = item.GetItemSO().sprite;
        }
    }*/

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
        for (int i = 0; parent.transform.GetChild(0) != null; i++)
        {
            Transform child = parent.transform.GetChild(i);
            Destroy(child);
        }
    }

    public void FillInventory(Transform parent, List<Item> itemList)
    {
        for(int i = 0; i== itemList.Count; i++)
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

    public void ToggleInventoryButton()
    {
        panelBackground.SetActive(!panelBackground.activeInHierarchy);
    }
}
