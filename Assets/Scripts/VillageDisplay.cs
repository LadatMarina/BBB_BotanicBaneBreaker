using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;

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

    private Recollectable potion = null;

    void Start()
    {
        givePotionButton.gameObject.SetActive(false);
        potion = null;
        village= GameManager.Instance.village;
        SetVillage(village);

        Button potionFieldButton = potionField.GetComponent<Button>();
        potionFieldButton.interactable = true;
        potionFieldButton.onClick.AddListener(() => ShowInventoryAndUnableButton(potionFieldButton));
    }

    private void Update()
    {
        if(potion != null)
        {
            RefreshPotionField();
            givePotionButton.gameObject.SetActive(true);
        }
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
        UI_Inventory.Instance.ToggleInventoryButton();
        potionFieldButton.interactable = false;
    }

    public void SetPotion(Recollectable potion)
    {
        this.potion = potion;
    }

    private void RefreshPotionField()
    {
        SpriteRenderer potionFieldSpriteRenderer = potionField.GetComponent<SpriteRenderer>();
        potionFieldSpriteRenderer.sprite = potion.sprite;
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
}
