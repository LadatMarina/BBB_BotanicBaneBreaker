using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanelUI : MonoBehaviour
{
    public void TogglePauseButton()
    {
        if (this.gameObject.activeInHierarchy)
        {
            this.gameObject.SetActive(false);
            GameManager.Instance.isPaused = false;
        }
        else
        {
            this.gameObject.SetActive(true);
            GameManager.Instance.isPaused = true;
        }
    }
}
