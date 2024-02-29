using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        //construct the input actions of the player
        playerInputActions = new PlayerInputActions();
        //enable the player input actions
        playerInputActions.Player.Enable();
        //playerInputActions.UI_nav.Enable();
    }
    public Vector2 GetInputVectorNormalized()
    {
        //our input vector is the value that the input system recieves
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        
        inputVector = inputVector.normalized;

        return inputVector;
    }
//    public bool HasClickedTheInventoryButton()
//    {
//        if(playerInputActions.UI_nav.OpenInventory();)
//        return true;
//    } 
}
