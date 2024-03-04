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
    public List<Recollectable> lockedPotionsList;
    public List<Recollectable> unlockedHealthPotions;
    public List<Recollectable> unlockedAttackPotions;
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

        lockedPotionsList = new List<Recollectable>();
        unlockedAttackPotions = new List<Recollectable>();
        unlockedHealthPotions = new List<Recollectable>();
    }

    private void InicializeLockedPotionsList()
    {
        //if( firstGame)
        lockedPotionsList = GameAssets.Instance.potions;
    }

}
