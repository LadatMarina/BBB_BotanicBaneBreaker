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
    public GameObject inventoryPanel;   

    public Village villageee;

    private Recollectable potion;

    void Start()
    {
        //Village myVillage = gameManager.village;
        //no me agafa es village :((((
        villageee = GameManager.Instance.village;
        SetVillage(villageee);

        Button potionFieldButton = potionField.GetComponent<Button>();
        potionFieldButton.interactable = true;
        potionFieldButton.onClick.AddListener(() => ShowInventoryAndUnableButton(potionFieldButton));
    }

    private void Update()
    {
        if(potion != null)
        {
            RefreshPotionField();
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
}
