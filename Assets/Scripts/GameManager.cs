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

    public GameObject CreateNewItem(Recollectable itemSO, Vector3 position, int amount)
    {
        GameObject newObject = Instantiate(itemWorld, position, Quaternion.identity);

        Item item = new Item { amount = amount, itemSO = itemSO };

        newObject.tag = "recollectable";

        newObject.GetComponent<RecollectableDisplay>().SetItem(item);

        return newObject;
    }
}
