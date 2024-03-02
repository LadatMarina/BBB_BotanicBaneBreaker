using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public static CursorManager Instance { get; private set; }
    private void Awake()
    {
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
    }
    // Update is called once per frame
    void Update()
    {
        //int scene = SceneManager.GetActiveScene().buildIndex;
        //switch (scene)
        //{
        //    case (int)SceneIndex.GamePlay:
        //        HideCursor();
        //        break;

        //    case (int)SceneIndex.House:
        //        ShowCursor();
        //        break;

        //    case (int)SceneIndex.Kitchen:
        //        ShowCursor();
        //        break;

        //    case (int)SceneIndex.LoadingScene:
        //        HideCursor();
        //        break;

        //    case (int)SceneIndex.MainMenu:
        //        HideCursor();
        //        break;
        //}
    }

    private void ShowCursor()
    {
        if (Cursor.visible == false)
        {
            //Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void HideCursor()
    {
        if (Cursor.visible == true)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
