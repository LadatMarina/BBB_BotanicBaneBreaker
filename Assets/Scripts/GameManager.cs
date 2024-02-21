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
    Witch = 3,
    Villagers = 2
}
public class GameManager : MonoBehaviour
{
    public Village village;
    public bool hasLoaded;

    public bool isPaused = false;
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject pauseButton;

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
            DontDestroyOnLoad(gameObject);
        }

        SetUI();
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

    // VULL QUE FUNCIONI AIXÍ, PERÒ DE MENTRES FUNCIONARÀ NOMÉS AMB UN INT
    /*public void LoadScene(SceneIndex index)
    {
        SceneManager.LoadScene((int)index);
    }*/
    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        SetUI();
    }

    public void TogglePauseButton()
    {
        if (pausePanel.activeInHierarchy)
        {
            pausePanel.SetActive(false);
            isPaused = false;
        }
        else
        {
            pausePanel.SetActive(true);
            isPaused = true;
        }
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

    public void SetUI()
    {
        //quant es carrega sa escena s'asignen es panels i es botons i se li posa al pause button sa funció corresponent
        //no me serveix fer-ho des de s'inspector, perquè jo vull que se faci un toggle
        pausePanel = GameObject.Find("PausePanel");

        /*
        pauseButton = GameObject.Find("PauseButton");
        Button pausebuttonButtonComponent = pauseButton.GetComponent<Button>();
        pausebuttonButtonComponent.onClick.RemoveAllListeners();
        pausebuttonButtonComponent.onClick.AddListener(() => TogglePauseButton());
        */
        Debug.Log("the pausePanel has been founded and set to the game manager. Now the pause button should work");

        //asignar aquí es pause panel i tal per poder fer es pause. de totes formes
        //me queda pendent mirar es projecte meu de l'any passat amb en jerson o es
        //projecte de s'snake per veure com s'ha fet es pause, que segur és algo súper fàcil
    }


}
