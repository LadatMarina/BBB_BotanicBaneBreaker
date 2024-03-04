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
    Witch = 5,
    House = 2,
    Kitchen = 3,
    LoadingScene = 4,
}
public class GameManager : MonoBehaviour
{
    //public Village village;
    public bool hasLoaded;

    public int numOfGames; //0/false : 1/true

    //setted w the inspector
    [SerializeField] private GameObject itemWorld;


    public bool isPaused = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

    public static GameManager Instance { get; private set; }

    public Recollectable pocioQueHaElegit = null;

    private Vector2 lastPlayerPosition;

    //private List<Recollectable> lockedItemList;

    //inventory
    //public Inventory inventory;

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

        //if (PlayerPrefs.GetInt("numOfGames") == null)
        //{
        //    //if the before game was the first game, 
        //}
        //PlayerPrefs.SetInt("numOfGames", 1);
    }

    //public void DisplayInventoryItemList(List<Item> itemList)
    //{
    //    foreach (Item item in itemList)
    //    {
    //        Debug.Log(itemList.IndexOf(item) + " " + item.itemSO.name + " amount " + item.amount);
    //    }
    //}

# region LOAD THINGS TO DELETE WHEN JSON IS DONE
    //public void LoadHouseScene(Village village)
    //{
    //    this.village = village;

    //    savedItemList = inventory.itemList;

    //    LoadScene((int)SceneIndex.House);
    //}

    //public void LoadKitchen()
    //{
    //    SceneManager.LoadScene((int)SceneIndex.Kitchen);

    //}

    //// VULL QUE FUNCIONI AIXÍ, PERÒ DE MENTRES FUNCIONARÀ NOMÉS AMB UN INT
    ///*public void LoadScene(SceneIndex index)
    //{
    //    SceneManager.LoadScene((int)index);
    //}*/
    //public void LoadScene(int index)
    //{
    //    Debug.Log("LoadScene() / GameManager");
    //    if(SceneManager.GetActiveScene().buildIndex != (int)SceneIndex.MainMenu)
    //    {
    //        if (savedItemList != null)
    //        {
    //            RefreshItemList(savedItemList);

    //        }
    //        else
    //        {
    //            Debug.Log("the saved item list is null");
    //        }
            
    //    }

    //    SceneManager.LoadScene(index);
    //}

    #endregion
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.N))
        //{
        //    DebugItemList(inventory.itemList);
        //}
        
        //if (Input.GetKeyDown(KeyCode.G))
        //{
        //    DebugItemList(savedItemList);
        //}
        
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.Log(DataPersistanceManager.Instance.LoadInventory().Count);
        }
        
        if (Input.GetKeyDown(KeyCode.T))
        {
            DataPersistanceManager.Instance.SaveVillage(GameAssets.Instance.maria);

            Debug.Log("the village is saved, now press I for prove if the list count is under 1");
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



    //    public void DebugItemList(List<Item> list)
    //    {
    //        foreach (Item item in list)
    //        {
    //            Debug.Log(item.itemSO.name);
    //        }
    //    }

    //    public void RefreshItemList(List<Item> list)
    //    {
    //        inventory.itemList = list;

    //        //Debug.Log("item list passed from GM is:");
    //        DebugItemList(list);

    //        //Debug.Log("item list refreshed from inventory is:");
    //        DebugItemList(inventory.itemList);

    //        //Debug.Log("item list refreshed / inventory");
    //    }

    public GameObject CreateNewItem(Recollectable itemSO, Vector3 position, int amount)
    {
        GameObject newObject = Instantiate(itemWorld, position, Quaternion.identity);

        Item item = new Item { amount = amount, itemSO = itemSO };

        newObject.tag = "recollectable";

        newObject.GetComponent<RecollectableDisplay>().SetItem(item);

        return newObject;
    }
}
