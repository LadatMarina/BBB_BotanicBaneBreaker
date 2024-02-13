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

    public Village villageee;


    void Start()
    {
        //Village myVillage = gameManager.village;
        //no me agafa es village :((((
        villageee = GameManager.Instance.village;
        SetVillage(villageee);
    }

    public void SetVillage(Village village)
    {
        Debug.Log($"en paco ara tocaria sortir : '{village}' ");
        /*nameText.text = village.name;
        diseaseText.text = $"{village.disease}";
        //potionText.text = village.potion;
        npcImage.sprite = village.sprite;

        Image imageBackground = background.GetComponent<Image>();
        imageBackground.color = village.backgroundColor;*/
    }
}
