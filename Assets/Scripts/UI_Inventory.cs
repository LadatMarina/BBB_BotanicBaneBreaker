using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    //posar això serialized i llevar es Find
    private Inventory inventory;
    private Transform recollectableContainer;
    private Transform containerTemplate;

    private void Awake()
    {
        //make a constant of this
        recollectableContainer = transform.Find("recollectableContainer");
        recollectableContainer = transform.Find("containerTemplate");
    }
    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryRecollectables();
    }

    public void RefreshInventoryRecollectables()
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
}
