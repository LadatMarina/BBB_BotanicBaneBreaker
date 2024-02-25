using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum SceneIndex //en teoria posar aquests valors o no és lo mateix.
{
    MainMenu = 0,
    GamePlay = 1,
    Witch = 4,
    House = 2,
    Kitchen = 3,
}
public class GameManager : MonoBehaviour
{
    public Village village;
    public bool hasLoaded;

    public bool isPaused = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    public static GameManager Instance { get; private set; }

    public Recollectable pocioQueHaElegit = null;

    private Vector2 lastPlayerPosition;

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
            DontDestroyOnLoad(gameObject);
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

        Debug.Log($"the villager : '{village}' has been set to the load scene '{(int)SceneIndex.House}'");
                
        //hasLoaded = true;

        SceneManager.LoadScene((int)SceneIndex.House);

        Debug.Log(this.village);

    }

    public void LoadKitchen()
    {
        SceneManager.LoadScene((int)SceneIndex.Kitchen);

    }

        private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y))
        {
            LoadHouseScene(village);
        }
    }

    // VULL QUE FUNCIONI AIXÍ, PERÒ DE MENTRES FUNCIONARÀ NOMÉS AMB UN INT
    /*public void LoadScene(SceneIndex index)
    {
        SceneManager.LoadScene((int)index);
    }*/
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void ToggleInventoryButton()
    {
        //PLAY AN SFX WHEN RECOLLECT
        SoundManager.Instance.PlaySFX(SoundManager.Instance.sound3);

        if (UI_Inventory.Instance.panelBackground.activeInHierarchy == true) //if it's true, will be closed so we have to destroy all the elements
        {
            UI_Inventory.Instance.panelBackground.SetActive(false);
            UI_Inventory.Instance.HideAllChildren();
            Debug.Log("all the childrens of the panel background have been destroyed");
        }
        else
        {
            UI_Inventory.Instance.panelBackground.SetActive(true);
            UI_Inventory.Instance.RefreshItems();

            Debug.Log("the inventory has been refreshed with the item list");
        }
    }

    private void OnDisable()
    {
        //pensar a que quant acabi es joc per entregar, he de borrar s'on disable, o almanco l'he de comentar per posar que és només per quant editi es joc 
        GameAssets.Instance.paco.isCured = false;
        GameAssets.Instance.maria.isCured = false;
        GameAssets.Instance.bel.isCured = false;
        GameAssets.Instance.toni.isCured = false;
    }

    public void SetLastPLayerPos(Vector3 pos)
    {
        lastPlayerPosition = pos;
        Debug.Log("the lastPlayerPosition has been setted to " + lastPlayerPosition);
    }

    public Vector3 GetLastPlayerPos()
    {
        return lastPlayerPosition;
    }

}
