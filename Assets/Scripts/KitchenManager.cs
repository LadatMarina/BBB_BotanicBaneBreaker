using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KitchenManager : MonoBehaviour
{
    [SerializeField] private Transform whatToDoPanel;

    [SerializeField] private Transform ingredientField1;
    [SerializeField] private Transform ingredientField2;
    private Transform actualIngredientField;

    [SerializeField] private Transform potionField;
    [SerializeField] private Transform mixButton;
    [SerializeField] private ParticleSystem blueExplosion;

    private IngredientHolder ingredientHolder1;
    private IngredientHolder ingredientHolder2;
    private PotionHolder potionHolder;
    private Button potionHolderButton;

    private void Awake()
    {
        ingredientHolder1 = ingredientField1.GetComponent<IngredientHolder>();
        ingredientHolder2 = ingredientField2.GetComponent<IngredientHolder>();
        potionHolder = potionField.GetComponent<PotionHolder>();
        potionHolderButton = potionField.GetComponent<Button>();

    }

    void Start()
    {
        mixButton.gameObject.SetActive(false);
        whatToDoPanel.gameObject.SetActive(false);
        potionHolderButton.interactable = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            IngredientHolder ingredientHolder = ingredientField1.GetComponent<IngredientHolder>();
            if(ingredientHolder.ingredient == null){
                Debug.Log("ingredientHolder.ingredient == null");
            }
            else
            {
                Debug.Log("ingredientHolder.ingredient != null");
            }
        }
    }

    public void IngredientFieldButton(int bttn)
    {
        Debug.Log("ingredientFieldButton() / kitchenManager");

        UI_Inventory.Instance.ToggleInventoryButton();
        
        //passar es transform de s'ingredient field perquè al chooseIngredient s'hi pugui accedir
        switch (bttn)
        {
            case 1:
                actualIngredientField = ingredientField1;
                break;
            case 2:
                actualIngredientField = ingredientField2;
                break;
        }
    }

    public void ChooseIngredient(Item item) //només se n'ha de restar 1 i afegir 1!!
    {
        Debug.Log("ChooseIngredient() / KM");

        UI_Inventory.Instance.ToggleInventoryButton();
        IngredientHolder ingredientHolder = actualIngredientField.GetComponent<IngredientHolder>();

        //set the button ingredient as the field --> visuals and the item that represents
        Button ingredientFieldButton = actualIngredientField.GetComponent<Button>();

        //was already an ingredient in the button --> return this item to the list
        if (ingredientHolder.ingredient.itemSO != null)
        {
            Debug.Log("there was already an ingredient, intercambio --> return the item to the inventory");
            //if the button had an intem, return to the inventory the item --> returnPreviousItem()
            DataPersistanceManager.Instance.AddOneItem(new Item { amount = 1, itemSO = ingredientHolder.ingredient.itemSO }); //create a new item w/amount 1
            ingredientFieldButton.image.sprite = GameAssets.Instance.defaultEmptySprite;
        }
         //if there wasn't an ingredient asigned, remove from the list the item we want to use (parameter one)
        
        Debug.Log("hi hagués asignat o no un item, hem de llevar de sa llista s'actual");
        DataPersistanceManager.Instance.RemoveOneItem(item); 

        //set the button ingredient as the field --> visuals and the item that represents
        ingredientFieldButton.image.sprite = item.itemSO.sprite;
        ingredientHolder.ingredient = item;
        CanMix(); //everytime the field is changed, check if can mix
    }

    private bool CanMix()
    {
        Debug.Log("canMix() / kitchenManager");

        if (ingredientHolder1.ingredient.itemSO != null  && ingredientHolder2.ingredient.itemSO != null)
        {
            mixButton.gameObject.SetActive(true);
            return true;
        }
        else
        {
            mixButton.gameObject.SetActive(false);
            return false;
        }
    }

    public void MixButton()
    {
        mixButton.gameObject.SetActive(false); //hide the button
        Debug.Log("mixButton() / kitchenManager");
        Recollectable resultPotion = GameAssets.Instance.GetPotionFromIngredients(ingredientHolder1.ingredient.itemSO, ingredientHolder2.ingredient.itemSO);
        if(resultPotion != GameAssets.Instance.defaultPotion)
        {
            //it's an existing potion
            PlayParticles();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.sound1);
            potionHolder.potion = resultPotion;
            potionHolderButton.image.sprite = resultPotion.sprite;
            potionHolderButton.interactable = true;
            //show explanation text of "you can click here to decide what to do
            //check if the potion is in the lockedPotionList --> RecipeManager
        }
        else
        {
            //there's not a recipe for this combination --> change the ingredients
            potionHolderButton.image.sprite = resultPotion.sprite;
        }

    }

    public void PotionFieldButton() { whatToDoPanel.gameObject.SetActive(true); }
    public void SavePotion()
    {
        DataPersistanceManager.Instance.AddOneItem(new Item { amount = 1, itemSO = potionHolder.potion });
        whatToDoPanel.gameObject.SetActive(false);
        //ResetAllFields();
    }

    public void ThrowPotion()
    {
        ResetAllFields();
        whatToDoPanel.gameObject.SetActive(false);
    }

    public void PlayParticles() { blueExplosion.Play(); }
    private void ResetAllFields()
    {
        //reset all the fields
        ingredientHolder1.ingredient = null;
        Button ingredientFieldButton1 = ingredientField1.GetComponent<Button>();
        ingredientFieldButton1.image.sprite = GameAssets.Instance.defaultEmptySprite;

        ingredientHolder2.ingredient = null;
        Button ingredientFieldButton2 = ingredientField2.GetComponent<Button>();
        ingredientFieldButton2.image.sprite = GameAssets.Instance.defaultEmptySprite;

        potionHolder.potion = null;
        potionHolderButton.image.sprite = GameAssets.Instance.defaultEmptySprite;
    }
    public void BackButton() { Loader.Load(SceneIndex.GamePlay); }
    public void ToggleRecipeButton() { RecipesManager.Instance.ToggleRecipeMenu(); }
    
}
