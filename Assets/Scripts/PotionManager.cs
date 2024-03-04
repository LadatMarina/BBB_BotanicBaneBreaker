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
    public List<Item> unlockedPotions;

    public Recollectable    gloombreach, blitzir, chaosCore, furyfume, lifesip, stomachHealth, coldir, voicesip, defaultPotion;
    public List<Recollectable> attackPotions;
    public List<Recollectable> healthPotions;

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

        unlockedPotions = DataPersistanceManager.Instance.LoadUnlockedList();

        attackPotions = new List<Recollectable>() { gloombreach, blitzir, chaosCore, furyfume };
        healthPotions = new List<Recollectable>() { lifesip, stomachHealth, coldir, voicesip };
    }

    public void UnlockPotion(Recollectable potion)
    {
        unlockedPotions.Add(new Item { amount = 1, itemSO = potion });
    }


    public Recollectable GetPotionFromIngredients(Recollectable ingredient1, Recollectable ingredient2)
    {
        List<Recollectable> allPotions = new List<Recollectable>();
        allPotions.AddRange(healthPotions);
        allPotions.AddRange(attackPotions);

        foreach (Recollectable potion in allPotions)
        {
            Debug.Log("checking if there's a recipe compatible...");

            if (ingredient1 == potion.ingredientsForMakeThePotion[0] && ingredient2 == potion.ingredientsForMakeThePotion[1])
            {
                Debug.Log("recipe founded!");
                return potion;
            }
        }
        Debug.Log(" NO recipe found");

        //if there's not a recipe for this values, return null --> canviar a un simbol que sigui --> no possible recipe
        return defaultPotion;
    }

    public Recollectable GetHealthPotionFromDisease(Diseases disease)
    {
        switch (disease)
        {
            case Diseases.cold:
                return coldir;
            case Diseases.constipated:
                return lifesip;
            case Diseases.diarrea:
                return voicesip;
            case Diseases.stomachAge:
                return stomachHealth;
        }
        return null;
    }

    public Recollectable GetAttackPotionFromHealthPotion(Recollectable healthPotion)
    {
        if (healthPotion.Equals(lifesip))
        {
            return gloombreach;
        }
        else if (healthPotion.Equals(stomachHealth))
        {
            return blitzir;
        }
        else if (healthPotion.Equals(coldir))
        {
            return chaosCore;
        }
        else if (healthPotion.Equals(voicesip))
        {
            return furyfume;
        }
        else
        {
            return defaultPotion;
        }
    }

    public Recollectable GetPotionFromString(string potionName)
    {
        switch (potionName)
        {
            //attackPotions:
            case "Gloombreach":
                return gloombreach;
            case "Blitzir":
                return blitzir;
            case "ChaosCore":
                return chaosCore;
            case "Furyfume":
                return furyfume;

            //healthPotions:
            case "Lifesip":
                return lifesip;
            case "StomachHealth":
                return stomachHealth;
            case "Coldir":
                return coldir;
            case "Voicesip":
                return voicesip;

            default:
                Debug.Log("the default is being returned");
                return defaultPotion;
        }
    }

    public bool CheckIfPotionUnlocked(Recollectable potion)
    {
        if(unlockedPotions.Count != 0)
        {
            foreach (Item item in unlockedPotions)
            {
                if (item.itemSO == potion)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
