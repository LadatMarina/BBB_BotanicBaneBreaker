using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    public static GameInput Instance;
    private void Awake()
    {
        //singleton
        // If there is an instance, and it's not me, delete myself.

        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

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
    
    public void TogglePlayerInputActions()
    {
        if (playerInputActions.Player.enabled)
        {
            playerInputActions.Player.Disable();
        }
        else
        {
            playerInputActions.Player.Enable();
        }
    }
    //if this function is called, won't disable all the time
    public void DisablePlayerInputActions() {
            Debug.Log("playerInputActions are DISabled");
            playerInputActions.Player.Disable(); }
    public void EnablePlayerInputActions() { 
            Debug.Log("playerInputActions are ENabled");
            playerInputActions.Player.Enable();  }
}
