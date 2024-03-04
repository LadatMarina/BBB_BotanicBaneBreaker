using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WineScene : MonoBehaviour
{
    private void Start()
    {
        //SoundManager.Instance.PlaySFX();
    }
    public void ReturnToMainMenu()
    {
        SoundManager.Instance.PlaySFX(SoundManager.Instance.toggleButtonSound);
        Loader.Load(SceneIndex.MainMenu);
    }
}
