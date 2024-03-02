using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private Transform savedIngredientField;
    [SerializeField] private Transform ingredientField2;

    //private IngredientHolder ingredientHolder1

    // Start is called before the first frame update
    void Start()
    {
        //IngredientHolder 
        //ingredientField1
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IngredientFieldButton(Transform transform)
    {
        UI_Inventory.Instance.ToggleInventoryButton();
        //passar es transform de s'ingredient field perquè al chooseIngredient s'hi pugui accedir
    }

    public void ChooseIngredient(Item item, Transform inventoryButton) //només se n'ha de restar 1 i afegir 1!!
    {
        IngredientHolder ingredientHolder = inventoryButton.GetComponent<IngredientHolder>(); //malament --> es qui te aqeust script és ingredient field, es qui fa toggle

        if(ingredientHolder.ingredient != null)
        {
            //was already an ingredient in the button
            DataPersistanceManager.Instance.RemoveOneItem(ingredientHolder.ingredient);
        }

        //set the button ingredient as the field --> visuals and the item that represents
        Button ingredientFieldButton = inventoryButton.GetComponent<Button>();
        ingredientFieldButton.image.sprite = item.itemSO.sprite;
        ingredientHolder.ingredient = item;

        //create a new item with amount 1 to ensure not to take a more than 1
        DataPersistanceManager.Instance.AddOneItem(new Item { amount = 1, itemSO = item.itemSO }); 
    }
}
