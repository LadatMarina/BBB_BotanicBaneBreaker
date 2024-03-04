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

    [SerializeField] private Transform recipeButon;


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
        RecipesManager.Instance.HideRecipeMenu();
        ExplanationManagerUI.Instance.ShowAnExplanation("Click on each field to decide what ingredient put of what to do with the result.", 20);
    }

    public void IngredientFieldButton(int bttn)
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        UI_Inventory.Instance.ToggleInventoryButton();
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

    public void ChooseIngredient(Item item) 
    {
        ExplanationManagerUI.Instance.HideExplanation();

        UI_Inventory.Instance.ToggleInventoryButton();
        IngredientHolder ingredientHolder = actualIngredientField.GetComponent<IngredientHolder>();

        //set the button ingredient as the field --> visuals and the item that represents
        Button ingredientFieldButton = actualIngredientField.GetComponent<Button>();

        //was already an ingredient in the button --> return this item to the list
        if (ingredientHolder.ingredient.itemSO != null)
        {
            //if the button had an intem, return to the inventory the item --> returnPreviousItem()
            DataPersistanceManager.Instance.AddOneItem(new Item { amount = 1, itemSO = ingredientHolder.ingredient.itemSO }); //create a new item w/amount 1
            ingredientFieldButton.image.sprite = GameAssets.Instance.defaultEmptySprite;
        }
         //if there wasn't an ingredient asigned, remove from the list the item we want to use (parameter one)
        DataPersistanceManager.Instance.RemoveOneItem(item); 

        //set the button ingredient as the field --> visuals and the item that represents
        ingredientFieldButton.image.sprite = item.itemSO.sprite;
        ingredientHolder.ingredient = item;
        CanMix(); //everytime the field is changed, check if can mix
    }

    private bool CanMix()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
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
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        mixButton.gameObject.SetActive(false); //hide the button
        HideRecipeButton(); //after mix, hide the recipe book button
        Recollectable resultPotion = PotionManager.Instance.GetPotionFromIngredients(ingredientHolder1.ingredient.itemSO, ingredientHolder2.ingredient.itemSO);
        if(resultPotion != PotionManager.Instance.defaultPotion)
        {
            //it's an existing potion
            PlayParticles();
            SoundManager.Instance.PlaySFX(SoundManager.Instance.mainMenuMusic);
            potionHolder.potion = resultPotion;
            potionHolderButton.image.sprite = resultPotion.sprite;
            potionHolderButton.interactable = true;

            //if the potion was not unlocked yet, unlock
            if (!PotionManager.Instance.CheckIfPotionUnlocked(resultPotion))
            {
                PotionManager.Instance.UnlockPotion(resultPotion);
            }
        }
        else
        {
            ExplanationManagerUI.Instance.ShowAnExplanation("There are not potions with this combination. Try new ingredients.", 20);
            potionHolderButton.image.sprite = resultPotion.sprite;
        }
    }

    public void PotionFieldButton() {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        whatToDoPanel.gameObject.SetActive(true); }
    public void SavePotion()
    {   
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        DataPersistanceManager.Instance.AddOneItem(new Item { amount = 1, itemSO = potionHolder.potion });
        PotionManager.Instance.UnlockPotion(potionHolder.potion);
        ResetAllFields();
        whatToDoPanel.gameObject.SetActive(false);
        Loader.Load(SceneIndex.GamePlay); // return to the game after doing a potion
    }

    public void ThrowPotion()
    {
        ResetAllFields();
        whatToDoPanel.gameObject.SetActive(false);
        Loader.Load(SceneIndex.GamePlay); // return to the game after doing a potion
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
    public void BackButton() {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        RecipesManager.Instance.HideRecipeMenu();
        Loader.Load(SceneIndex.GamePlay); }
    public void ToggleRecipeButton() {  
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        RecipesManager.Instance.ToggleRecipeMenu(); }
 
    private void HideRecipeButton() { 
        recipeButon.gameObject.SetActive(false); }
}
