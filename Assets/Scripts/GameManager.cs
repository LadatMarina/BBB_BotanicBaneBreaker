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
    House = 2,
    Kitchen = 3,
    LoadingScene = 4,
    WinScene = 5
}

public class GameManager : MonoBehaviour
{
    //setted w the inspector
    [SerializeField] private GameObject itemWorld;
    public bool isPaused = false;

    public static GameManager Instance { get; private set; }

    private Vector2 lastPlayerPosition;

    //this variable is duplicated for store it's value during the game, for not being all the time saving and load
    public bool isFirstGame; 

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

        isFirstGame = DataPersistanceManager.Instance.LoadIsFirstGame();
    }

    private void Start()
    {
        //if it's true, turn it false
        if (isFirstGame)
        {
            isFirstGame = false;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(GoToWinScene());
        }
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

    public GameObject CreateNewItem(Recollectable itemSO, Vector3 position, int amount)
    {
        GameObject newObject = Instantiate(itemWorld, position, Quaternion.identity);

        Item item = new Item { amount = amount, itemSO = itemSO };

        newObject.tag = "recollectable";

        newObject.GetComponent<RecollectableDisplay>().SetItem(item);

        return newObject;
    }

    public bool HasWon()
    {
        //check if all the villagers are cured --> if goes out of the foreach and enter in the if, are all cured
        foreach(Village villager in GameAssets.Instance.villagers)
        {
            if (!villager.isCured) { return false; }
        }
        //check if all the potions has been unlocked (8)
        if(DataPersistanceManager.Instance.LoadUnlockedList().Count >= 8)
        {
            StartCoroutine(GoToWinScene());
            return true;
        }
        return false;
    }

    private IEnumerator GoToWinScene()
    {
        yield return new WaitForSeconds(2);
        Loader.Load(SceneIndex.WinScene);
        //
    }
}
