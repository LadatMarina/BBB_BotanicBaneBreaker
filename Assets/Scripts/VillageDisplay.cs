using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System;

public class VillageDisplay : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI diseaseText;
    public TextMeshProUGUI potionText;
    public Image npcImage;
    public GameObject background;

    public Transform potionField;
    public Transform givePotionButton;
    public GameObject inventoryPanel;   

    public Village village;

    public Recollectable potion = null;
    public bool hasSelectedAPotion;


    void Start()
    {
        givePotionButton.gameObject.SetActive(false);
        potion = null;
        village= GameManager.Instance.village;
        SetVillage(village);

        Button potionFieldButton = potionField.GetComponent<Button>();
        potionFieldButton.interactable = true;
        potionFieldButton.onClick.AddListener(() => ShowInventoryAndUnableButton(potionFieldButton));
        hasSelectedAPotion = false;
    }
    bool booleanCanviarNom = false;
    private void Update()
    {
        //if (GameManager.Instance.pocioQueHaElegit != null)
        //{
        //    Debug.Log("SA POSION NO ES NULA SENIORES");
        //    potion = GameManager.Instance.pocioQueHaElegit;

        //    if (booleanCanviarNom == false)
        //    {
                
        //        //givePotionButton.gameObject.SetActive(true);
        //        RefreshPotionField();
        //        booleanCanviarNom = true;
        //    }
        //}

        //if(UI_Inventory.Instance.recollectableSavedForUsing == null)
        //{
        //    Debug.Log("SA POSION NO ES NULA SENIORES - villageDisplay");
        //    potion = UI_Inventory.Instance.recollectableSavedForUsing;
        //    if(booleanCanviarNom == false)
        //    {
        //        givePotionButton.gameObject.SetActive (true);   
        //        RefreshPotionField();
        //        booleanCanviarNom= true;
        //    }
        //}

        //if(UI_Inventory.Instance.hasSelectedAPotion != false)
        //{
        //    if (booleanCanviarNom == false)
        //    {
        //        booleanCanviarNom = true;

        //        //RefreshPotionField();
        //        givePotionButton.gameObject.SetActive(true);

        //    }
        //}
    }

    public void SetVillage(Village village)
    {
        Debug.Log($"en paco ara tocaria sortir : '{village}' ");
        nameText.text = village.name;
        diseaseText.text = $"{village.disease}";
        //potionText.text = village.potion;
        npcImage.sprite = village.sprite;

        Image imageBackground = background.GetComponent<Image>();
        imageBackground.color = village.backgroundColor;
    }

    private void ShowInventoryAndUnableButton(Button potionFieldButton)
    {
        GameManager.Instance.ToggleInventoryButton();
        potionFieldButton.interactable = false;
    }

    public void SetPotion(Recollectable potion)
    {
        this.potion = potion;
    }

    public void RefreshPotionField(Recollectable recollectableToRefresh)
    {
        Debug.Log("has entrat dins sa funció RefreshPotionField de dins VillageDisplay");

        SpriteRenderer potionFieldSpriteRenderer = potionField.GetComponent<SpriteRenderer>();
        potionFieldSpriteRenderer.sprite = recollectableToRefresh.sprite;
        potion = null;
    }

    public void CheckPotion()
    {
        //if the potion is the one that cures the village's disease,
        
        if (potion == GetHealthPotionFromDisease(village.disease))
        {
            // play particle system with congrats
            // npc animation of "huryay!"
            // return to main game
            // unlock a new potion recipe 
            Debug.Log($"CONGRAT'S YOU HAVE GIVE {village.name} THE CORRECT POTION");
        }
        else
        {
            //play particle system with death
            // npc animation of dying
            // --> pensar que fer
            Debug.Log($"YOU ALMOST KILL {village.name}!!!");
        }
    }


    public Recollectable GetHealthPotionFromDisease(Diseases disease)
    {
        switch (disease)
        {
            case Diseases.cold:
                return GameAssets.Instance.healthPotion1;
            
        }
        return null;
    }

    public void ChooseThePotionToGive(Recollectable itemSO) //pensar a posar sa lògica de restar-n'hi un si té més d'una poción!!
    {

        Debug.Log(itemSO.name + " this is the recollectable that this button is making reference of");

        
        Recollectable recollectableOfThisButton = itemSO;
        //if the player has not selected a potion from the inventory, will add the
        //item that the button represents to the field potion in the village display and hide the inventoy
        if(hasSelectedAPotion == false)
        {
            //potion = itemSO;
            Debug.Log("ha entrat dins sa funció ChoosePotionToGive, aquí és on s'hauria de actualitzar s'input Field");
            RefreshPotionField(recollectableOfThisButton);
            GameManager.Instance.ToggleInventoryButton();
            hasSelectedAPotion = true;
        }
    }

    public void ProvasionDelete(int n )
    {
        Debug.Log($"the recollectable {n} has arrived here");
    }
}
