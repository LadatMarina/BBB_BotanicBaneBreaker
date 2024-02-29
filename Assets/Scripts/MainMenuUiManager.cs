using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.EventSystems;

public class MainMenuUiManager : MonoBehaviour
{
    public GameObject quitCheckerPanel;
    public GameObject backButton;
    public GameObject playButton;

    private void Awake()
    {
        quitCheckerPanel.SetActive(false);
    }

    public void PlayButton()
    {
        Loader.Load(SceneIndex.GamePlay);
    }

    public void QuitButton()
    {
        if (!quitCheckerPanel.activeInHierarchy)
        {
            quitCheckerPanel.SetActive(true);
            EventSystem.current.SetSelectedGameObject(backButton);
        }
        else
        {
            Application.Quit();
        }
    }

    public void BackButton()
    {
        quitCheckerPanel.SetActive(false);
        EventSystem.current.SetSelectedGameObject(playButton);
    }
}
