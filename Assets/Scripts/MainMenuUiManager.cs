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
    public GameObject creditsPanel;
    public GameObject controllsPanel;
    public ParticleSystem blueExplosion;

    private void Awake()
    {
        quitCheckerPanel.SetActive(false);
        creditsPanel.SetActive(false);
        controllsPanel.SetActive(false);
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
    public void ToggleCreditsButton() { creditsPanel.SetActive(!creditsPanel.activeInHierarchy); }
    public void ToggleControllsButton() { controllsPanel.SetActive(!controllsPanel.activeInHierarchy); }
    public void PlayParticles() { blueExplosion.Play(); }
}
