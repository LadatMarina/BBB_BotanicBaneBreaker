using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoaderCallBack : MonoBehaviour
{
    private bool firstUpdate = true;


    private void Awake()
    {

    }
    private void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            StartCoroutine(WaitingTime());
        }
    }

    private IEnumerator WaitingTime()
    {

        for (float i = -8; i < 5; i++)
        {

            yield return new WaitForSeconds(0.2f);
        }

        Loader.LoaderCallback();
    }
}
