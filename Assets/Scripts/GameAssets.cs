using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAssets : MonoBehaviour
{
    public Recollectable apple, bean, bluberry, strawberry, gloombreach, blitzir, chaosCore, furyfume, lifesip, stomachHealth, coldir, voicesip, defaultPotion;
    public List<Recollectable> potions;

    public Village paco, maria, bel, toni;
    public Sprite defaultEmptySprite;
    public static GameAssets Instance { get; private set; }
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

        potions = new List<Recollectable>() { gloombreach, blitzir, chaosCore, furyfume, lifesip, stomachHealth, coldir, voicesip };
    }

    public Recollectable GetRecollectableFromString(string recollectableName) 
    {
        switch (recollectableName)
        {
            case "Apple":
                return apple;
            case "Bean":
                return bean;
            case "Blueberry":
                return bluberry;
            case "Strawberry":
                return strawberry;

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
                return apple;
        }
    }

    //******molt important; revisar que tots es noms dels villagers comencin en majúscula, perquè si no són iguals mos donarà null
    public Village GetVillageFromString(string villagerName)
    {
        switch (villagerName)
        {
            case "Paco":
                return paco;
            case "Maria":
                return maria;
            case "Bel":
                return bel;
            case "Toni":
                return toni;
            default:
                Debug.Log("the default is being returned");
                return null;
        }
    }

    public Recollectable GetPotionFromIngredients(Recollectable ingredient1, Recollectable ingredient2)
    {
        foreach(Recollectable potion in potions)
        {
            Debug.Log("checking if there's a recipe compatible...");

            if(ingredient1 == potion.ingredientsForMakeThePotion[0] && ingredient2 == potion.ingredientsForMakeThePotion[1])
            {
                Debug.Log("recipe founded!");
                return potion;
            }
        }
        Debug.Log(" NO recipe found");

        //if there's not a recipe for this values, return null --> canviar a un simbol que sigui --> no possible recipe
        return null;
    }

    public Recollectable GetHealthPotionFromDisease(Diseases disease)
    {
        Debug.Log($"GetHealthPotionFromDisease({disease}) / PotionManager");

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
}
