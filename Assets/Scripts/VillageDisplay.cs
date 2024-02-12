using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VillageDisplay : MonoBehaviour
{
    public TextMeshPro nameText;
    public TextMeshPro diseaseText;
    public TextMeshPro potionText;
    public Sprite npcSprite;
    public GameObject background;

    public GameManager gameManager;

    void Start()
    {
        SetVillage(gameManager.village);
    }

    public void SetVillage(Village village)
    {
        nameText.text = village.name;
        diseaseText.text = $"{village.disease}";
        potionText.text = village.potion;
        npcSprite = village.sprite;

        Image imageBackground = background.GetComponent<Image>();
        imageBackground.color = village.backgroundColor;
    }
}
