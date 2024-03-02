using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using UnityEngine.SceneManagement;
using static UnityEditor.Progress;

public class VillageDisplay : MonoBehaviour
{
    //all the buttons and UI elements are asigned in the inspector
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI diseaseText;
    public TextMeshProUGUI potionText;
    public Image npcImage;
    public Image potionImage;
    public GameObject background;
    public Transform potionField;
    public Transform givePotionButton;
    //public GameObject inventoryPanel;   

    public Village village;
    public bool hasSelectedAPotion;
    Animator npcAnimator;

    void Start()
    {
        SetVillage();
        hasSelectedAPotion = false; //això tampoc se exactament on me conve més, si aqui o a nes potionmanger
    }
    private void SetVillage()
    {
        Debug.Log("SetVillage() / VillageDisplay");

        //village = Player.Instance.LoadVillage();
        village = DataPersistanceManager.Instance.LoadVillage();
        
        nameText.text = village.name;
        diseaseText.text = "Disease: " + village.disease;
        potionText.text = "Potion needed: " + village.potion.name;
        potionImage.sprite = village.potion.sprite;
        npcAnimator = npcImage.GetComponent<Animator>(); 
        npcAnimator.runtimeAnimatorController = village.animator.runtimeAnimatorController;

        Image imageBackground = background.GetComponent<Image>();
        imageBackground.color = village.backgroundColor;

        //set at the begining the value of the animator and the potion field button 
        SetPotionField(potionField, village);
    }

    //això és UI per tant se fa des de aquis
    public void RefreshPotionField(Recollectable recollectableToRefresh)
    {
        Debug.Log($"RefreshPotionField({recollectableToRefresh}) / VillageDisplay");

        Image potionFieldImage = potionField.GetComponent<Image>();
        potionFieldImage.sprite = recollectableToRefresh.sprite;
    }

    public void ShowInventoryAndUnableButton(Button potionFieldButton)
    {
        UI_Inventory.Instance.ToggleInventoryButton();
        potionFieldButton.interactable = false;
    }

    public void SetPotionField(Transform potionField, Village village)
    {
        Debug.Log($"SetPotionField({potionField} , {village}) / Village Display");

        givePotionButton.gameObject.SetActive(false);
        //if the npc is cured the button unables and the image of the button is the correct potion
        if (village.isCured == true)
        {
            npcAnimator.SetBool("isCured", true);

            Button potionFieldButton = potionField.GetComponent<Button>();
            potionFieldButton.interactable = false; //if it's cured, there's no option to choose again the potion

            Image potionFieldImage = potionField.GetComponent<Image>();
            potionFieldImage.sprite = PotionManager.Instance.GetHealthPotionFromDisease(village.disease).sprite; //set the sprite of the potion 
        }
        //if the npc isn't cured the button is able and a function is set to it
        else
        {
            Button potionFieldButton = potionField.GetComponent<Button>();
            potionFieldButton.interactable = true;
            potionFieldButton.onClick.AddListener(() => ShowInventoryAndUnableButton(potionFieldButton));

            npcAnimator.SetBool("isCured", false);
        }
    }

    public void CheckPotion()
    {
        Debug.Log("CheckPotion() / Village Display");

        //if the potion is the one that cures the village's disease,
        if (PotionManager.Instance.GetPotion() == PotionManager.Instance.GetHealthPotionFromDisease(village.disease))
        {
            // play particle system with congrats
            // unlock a new potion recipe 
            village.isCured = true;
            npcAnimator.SetBool("isCured", true);
            Debug.Log($"CONGRAT'S YOU HAVE GIVE {village.name} THE CORRECT POTION");
            givePotionButton.gameObject.SetActive(false);

        }
        else
        {
            //play particle system with death
            // npc animation of dying
            // --> pensar que fer
            Debug.Log($"YOU ALMOST KILL {village.name}!!!");
        }
        UI_Inventory.Instance.HideInventory(); //--> mirar perquè aquí no me funciona, no se tanca quant se pitja es give
        PotionManager.Instance.SetPotion(null);
    }
    public void ChooseThePotionToGive(Item item) 
    {
        //Player.Instance.RemoveOneItemFromList(item);
        DataPersistanceManager.Instance.RemoveOneItem(item); //remove the item directly from the saved list in the json file

        Recollectable recollectableOfThisButton = item.itemSO; //perquè no posar només item.itemSO=
        //if the player has not selected a potion from the inventory, will add the
        //item that the button represents to the field potion in the village display and hide the inventoy
        if (hasSelectedAPotion == false)
        {
            PotionManager.Instance.SetPotion(item.itemSO); //mirar si basta es temps en què transcorre
            RefreshPotionField(item.itemSO); //HO HE CANVIAT AQUI LO DE SA LINIA 127 --> Refresh the img
            givePotionButton.gameObject.SetActive(true);
            UI_Inventory.Instance.ToggleInventoryButton(); //--> close the inventory
            hasSelectedAPotion = true;
        }
    }

    public void BackButton()
    {
        Loader.Load(SceneIndex.GamePlay);
    }
}
