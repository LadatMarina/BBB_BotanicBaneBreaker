using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public enum SceneIndex //en teoria posar aquests valors o no és lo mateix.
{
    MainMenu = 0,
    GamePlay = 1,
    Witch = 3,
    Villagers = 2
}

public class GameManager : MonoBehaviour
{
    public Village village;
    public bool hasLoaded;

    public static GameManager Instance { get; private set; }
    private void Awake()
    {
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null )
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }
    public void DisplayInventoryItemList(List<Item> itemList)
    {
        foreach (Item item in itemList)
        {
            Debug.Log(itemList.IndexOf(item) + " " + item.itemSO.name + " amount " + item.amount);
        }
    }

    public void LoadHouseScene(Village village)
    {
        this.village = village;

        Debug.Log($"the villager : '{village}' has been set to the load scene '{(int)SceneIndex.Villagers}'");
                
        //hasLoaded = true;

        SceneManager.LoadScene((int)SceneIndex.Villagers);

        Debug.Log(this.village);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            LoadHouseScene(village);
        }
    }
}
