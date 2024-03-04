using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipesManager : MonoBehaviour
{
    public static RecipesManager Instance { get; private set; }
    private int actualPage = 0;
    //left
    public Image potionImageL;
    public TextMeshProUGUI potionNameL;
    public TextMeshProUGUI potionExplanationL;

    //right
    public Image potionImageR;
    public TextMeshProUGUI potionNameR;
    public TextMeshProUGUI potionExplanationR;

    public Transform recipeMenu;
    public Transform pageR;
    public Transform pageL;

    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        pageR.gameObject.SetActive(false);
        pageL.gameObject.SetActive(false);
    }


    public void BeforeButton() { actualPage--; }
    public void AfterButton() { actualPage++; }

    public void ToggleRecipeMenu() 
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        recipeMenu.gameObject.SetActive(!recipeMenu.gameObject.activeInHierarchy);
        ShowRecipes();
         }

    private void ShowRecipes()
    {
        List<Item> unlockedPotionsList = PotionManager.Instance.unlockedPotions;

        if (actualPage < 0) // if it's out of range - lower
        {
            pageL.gameObject.SetActive(false);
        }
        else if (actualPage >= unlockedPotionsList.Count) // if it's out of range - greater or equal
        {
            pageR.gameObject.SetActive(false);
        }
        else // if it's within range
        {
            pageL.gameObject.SetActive(true);
            pageR.gameObject.SetActive(true);

            SetPage("left", PotionManager.Instance.unlockedPotions[actualPage].itemSO);

            // Check if there's a next page available
            if (actualPage + 1 < unlockedPotionsList.Count)
            {
                SetPage("right", PotionManager.Instance.unlockedPotions[actualPage + 1].itemSO);
            }
            else
            {
                ExplanationManagerUI.Instance.ShowAnExplanation("There are not more unlocked potions!", 15);
                pageR.gameObject.SetActive(false);
            }
        }
    }

    public void HideRecipeMenu() { recipeMenu.gameObject.SetActive(false); }
    private void SetPage(string page, Recollectable potion)
    {
        switch (page)
        {
            case "left":
                //
                pageL.gameObject.SetActive(true);
                potionImageL.sprite = potion.sprite;
                potionNameL.text = potion.name;
                potionExplanationL.text = potion.explanation;
                break;

            case "right":
                //
                pageR.gameObject.SetActive(true);
                potionImageR.sprite = potion.sprite;
                potionNameR.text = potion.name;
                potionExplanationR.text = potion.explanation;
                break;
        }
    }

}
