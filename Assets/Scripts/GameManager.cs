using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    private List<Item> savedItemList;

    //inventory
    public Inventory inventory;

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

        //savedItemList = new List<Item>();
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

        savedItemList = inventory.itemList;

        LoadScene((int)SceneIndex.House);


    }

    public void LoadKitchen()
    {
        SceneManager.LoadScene((int)SceneIndex.Kitchen);

    }

    // VULL QUE FUNCIONI AIXÍ, PERÒ DE MENTRES FUNCIONARÀ NOMÉS AMB UN INT
    /*public void LoadScene(SceneIndex index)
    {
        SceneManager.LoadScene((int)index);
    }*/
    public void LoadScene(int index)
    {
        Debug.Log("LoadScene() / GameManager");
        if(SceneManager.GetActiveScene().buildIndex != (int)SceneIndex.MainMenu)
        {
            if (savedItemList != null)
            {
                RefreshItemList(savedItemList);

            }
            else
            {
                Debug.Log("the saved item list is null");
            }
            
        }

        SceneManager.LoadScene(index);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            DebugItemList(inventory.itemList);
        }
        
        if (Input.GetKeyDown(KeyCode.G))
        {
            DebugItemList(savedItemList);
        }
    }

    private void OnDisable()
    {
        //pensar a que quant acabi es joc per entregar, he de borrar s'on disable, o almanco l'he de comentar per posar que és només per quant editi es joc
        //GameAssets.Instance.paco.isCured = false;
        //GameAssets.Instance.maria.isCured = false;
        //GameAssets.Instance.bel.isCured = false;
        //GameAssets.Instance.toni.isCured = false;
        //lastPlayerPosition = Vector3.zero;
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

    public void InitializeInventory()
    {
        //initialize the inventory
        inventory = new Inventory();
    }

    public Inventory GetInventory(){ return inventory; }

    public void Remove(Item item) {inventory.RemoveItemFromList(item);}

    public void DebugItemList(List<Item> list)
    {
        foreach (Item item in list)
        {
            Debug.Log(item.itemSO.name);
        }
    }

    public void RefreshItemList(List<Item> list)
    {
        inventory.itemList = list;

        Debug.Log("item list passed from GM is:");
        GameManager.Instance.DebugItemList(list);

        Debug.Log("item list refreshed from inventory is:");
        GameManager.Instance.DebugItemList(inventory.itemList);

        Debug.Log("item list refreshed / inventory");
    }
}
