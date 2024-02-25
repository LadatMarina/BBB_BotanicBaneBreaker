using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Video;
using System;
using UnityEngine.SceneManagement;

public class PotionManager : MonoBehaviour
{
    private Recollectable potion = null;
    public bool hasSelectedAPotion;

    Animator npcAnimator;

    public static PotionManager Instance { get; private set; }
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

        potion = null;
    }




    public Recollectable GetHealthPotionFromDisease(Diseases disease)
    {
        switch (disease)
        {
            case Diseases.cold:
                return GameAssets.Instance.healthPotion1;
            case Diseases.constipated:
                return GameAssets.Instance.healthPotion2;
            case Diseases.diarrea:
                return GameAssets.Instance.healthPotion3;
            case Diseases.stomachAge:
                return GameAssets.Instance.healthPotion4;
        }
        return null;
    }

    public Recollectable GetPotion() { return potion; }
    public void SetPotion(Recollectable potionToSet) { potion = potionToSet; }
   
}
