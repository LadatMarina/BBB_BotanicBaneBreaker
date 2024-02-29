using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class MainGameUIManager : MonoBehaviour
{
    public Button inventoryButton;
    private GameObject panelBackground;

    private Vector2 playerPosition;
    private bool hasThePlayerPos = false;
    private Player player;


    void Start()
    {
        //set the function of the game manager via code bc the game manager navegate btwn the scenes
        inventoryButton.onClick.AddListener(()=> UI_Inventory.Instance.ToggleInventoryButton());
        inventoryButton.onClick.AddListener(()=> EventSystem.current.SetSelectedGameObject(UI_Inventory.Instance.panelBackground.transform.GetChild(0).gameObject));

        //en teoria no he de posar cap cortafuegos de si és null o si és a sa escena que toca pq
        //sempre hi haurà un panelBackground per mor de que viatge entre escenes, asi que provam
        panelBackground = UI_Inventory.Instance.panelBackground; 

        player = FindObjectOfType<Player>();
    }

    private void Update()
    {
        //when the player moves, the inventory will close
        if (panelBackground.activeInHierarchy == true && hasThePlayerPos == false)
        {
            playerPosition = player.GetPlayerPos();
            hasThePlayerPos = true;

        }
        if ((panelBackground.activeInHierarchy == true) && (player.GetPlayerPos() != playerPosition))
        {
            UI_Inventory.Instance.ToggleInventoryButton();
            hasThePlayerPos = false; //reset the value
            Debug.Log("the inventory is toggled");
        }
    }



}
