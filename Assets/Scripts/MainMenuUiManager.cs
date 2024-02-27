using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUiManager : MonoBehaviour
{
    public GameObject quitCheckerPanel;

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
        if (quitCheckerPanel.activeInHierarchy)
        {
            Application.Quit();
        }
        else
        {
            quitCheckerPanel.SetActive(true);
        }
    }

    public void BackButton()
    {
        quitCheckerPanel.SetActive(false);
    }
}
