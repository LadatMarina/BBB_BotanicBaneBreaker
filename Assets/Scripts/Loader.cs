using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader 
{
    private static Action loaderCallbackAction;

    //set the action while load the loading scene
    public static void Load(SceneIndex scene)
    {
        loaderCallbackAction = () => { SceneManager.LoadScene((int)scene);};
        SceneManager.LoadScene((int)SceneIndex.LoadingScene);
        //DataPersistanceManager.Instance.SaveVillage(GameAssets.Instance.paco);
    }

    //when the action is not null, make it happen and reset it to null
    public static void LoaderCallback()
    {
        if (loaderCallbackAction != null)
        {
            loaderCallbackAction();
            loaderCallbackAction = null;
        }
    }
}
