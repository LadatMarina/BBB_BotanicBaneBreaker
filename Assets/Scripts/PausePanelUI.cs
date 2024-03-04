using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelUI : MonoBehaviour
{
    public Transform pausePanel;
    public Transform howToPlayPanel;

    public void TogglePauseButton()
    {
        if (pausePanel.gameObject.activeInHierarchy)
        {
            pausePanel.gameObject.SetActive(false);
            GameManager.Instance.isPaused = false;
        }
        else
        {
            pausePanel.gameObject.SetActive(true);
            GameManager.Instance.isPaused = true;
        }
    }
    public void GoToMainMenuButton() { Loader.Load(SceneIndex.MainMenu); }
}
