using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    
    public static void DisplayInventoryItemList(List<Item> itemList)
    {
        foreach (Item item in itemList)
        {
            Debug.Log(itemList.IndexOf(item) + " " + item.itemSO.name + " amount " + item.amount);
        }
    }

    public void LoadHouseScene(int numberOfTheHouse, Village village)
    {
        SceneManager.LoadScene(numberOfTheHouse);
        //aquí vull fer que se cridi sa funció de "set values of the scene" amb es valor que li digui jo, en plan, amb es nom de sa escena que se vol cridar.
        this.village = village;
    }
}
