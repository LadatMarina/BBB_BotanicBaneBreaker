using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class RecipesManager : MonoBehaviour
{
    public static RecipesManager Instance { get; private set; }

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

        // Start is called before the first frame update
    void Start()
    {
        pageR.gameObject.SetActive(false);
        pageL.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void BeforeButton() { }
    public void AfterButton() { }

    public void ToggleRecipeMenu() { recipeMenu.gameObject.SetActive(!recipeMenu.gameObject.activeInHierarchy); }

    public void SetPage(string page, Recollectable potion)
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
