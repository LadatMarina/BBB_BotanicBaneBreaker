using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;

    private Player player;

    //asignar per inspector
    [SerializeField] private GameObject recollectableButtonPrefab;
    [SerializeField] private GameObject panelBackground;
    [SerializeField] private Text amountText;

    private List<Item> itemList;

    //private Transform containerTemplate;

    private void Awake()
    {
        player = FindAnyObjectByType<Player>();

        inventory = player.GetInventory();

        itemList = inventory.GetItemList();
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshUiInventory();
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
        DestroyAllChildren(panelBackground);
        if (inventory.GetItemList().Count >0)
        {
            if (panelBackground.activeInHierarchy==true) //if it's true, will be closed so we have to destroiy all the elements
            {
                DestroyAllChildren(panelBackground);
                Debug.Log("all the childrens og the panel background have been destroyed");
            }
            else
            {
                //FillInventory(panelBackground.transform, inventory.GetItemList());
                Debug.Log("the inventory has been refreshed with the item list");
            }

            panelBackground.SetActive(!panelBackground.activeInHierarchy);
        }
        else
        {
            Debug.Log("the inventory is empty");
        }
    }
}
