using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using UnityEngine.SceneManagement;

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

    Animator npcAnimator;

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
    public void SetVillage(Village village)
    {
        nameText.text = village.name;
        diseaseText.text = $"{village.disease}";
        potionText.text = village.potion;
        npcImage.sprite = village.sprite;
        npcAnimator = npcImage.GetComponent<Animator>();
        npcAnimator.runtimeAnimatorController = village.animator.runtimeAnimatorController;

        Image imageBackground = background.GetComponent<Image>();
        imageBackground.color = village.backgroundColor;
    }

    private void ShowInventoryAndUnableButton(Button potionFieldButton)
    {
        GameManager.Instance.ToggleInventoryButton();
        potionFieldButton.interactable = false;
    }

    //public void SetPotion(Recollectable potion)
    //{
    //    this.potion = potion;
    //}

    public void RefreshPotionField(Recollectable recollectableToRefresh)
    {
        Debug.Log("has entrat dins sa funció RefreshPotionField de dins VillageDisplay");
        Image potionFieldImage = potionField.GetComponent<Image>();
        potionFieldImage.sprite = recollectableToRefresh.sprite;
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
            village.isCured = true;
            npcAnimator.SetBool("isCured", true);
            Debug.Log($"CONGRAT'S YOU HAVE GIVE {village.name} THE CORRECT POTION");
            
        }
        else
        {
            //play particle system with death
            // npc animation of dying
            // --> pensar que fer
            Debug.Log($"YOU ALMOST KILL {village.name}!!!");
        }
        potion = null;
        Debug.Log("now the potion variable of the village display is null");
    }


    public Recollectable GetHealthPotionFromDisease(Diseases disease)
    {
        switch (disease)
        {
            case Diseases.cold:
                return GameAssets.Instance.healthPotion1;
            case Diseases.constipated:
                return GameAssets.Instance.healthPotion2;
            case Diseases.diarrea:
                return GameAssets.Instance.healthPotion3;
            case Diseases.stomachAge:
                return GameAssets.Instance.healthPotion4;
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
            potion = itemSO; //mirar si basta es temps en què transcorre
            RefreshPotionField(recollectableOfThisButton);
            givePotionButton.gameObject.SetActive(true);
            GameManager.Instance.ToggleInventoryButton();
            hasSelectedAPotion = true;
        }
    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
}
