using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAssets : MonoBehaviour
{
    public Recollectable apple, bean, bluberry, strawberry, defaultRecollectable;

    public List<Recollectable> ingredients;

    public Village paco, maria, bel, toni, defaultVillage;
    public List<Village> villagers;

    public Sprite defaultEmptySprite;
    public static GameAssets Instance { get; private set; }
    private void Awake()
    {   
        //singleton
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        ingredients = new List<Recollectable>() { apple, bean, bluberry, strawberry };
        villagers = new List<Village>() { paco, maria, bel, toni };
    }

    public Recollectable GetRecollectableFromString(string recollectableName)
    {
        switch (recollectableName)
        {
            //ingredients
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
                return PotionManager.Instance.gloombreach;
            case "Blitzir":
                return PotionManager.Instance.blitzir;
            case "ChaosCore":
                return PotionManager.Instance.chaosCore;
            case "Furyfume":
                return PotionManager.Instance.furyfume;

            //healthPotions:
            case "Lifesip":
                return PotionManager.Instance.lifesip;
            case "StomachHealth":
                return PotionManager.Instance.stomachHealth;
            case "Coldir":
                return PotionManager.Instance.coldir;
            case "Voicesip":
                return PotionManager.Instance.voicesip;

            default:
                Debug.Log("anyone matches with: " + recollectableName);
                Debug.LogWarning("the default ingredient is being returned");
                return defaultRecollectable;
        }
    }
    
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
            case "":
                return defaultVillage;
            default:
                Debug.LogWarning("the default is being returned");
                return defaultVillage;
        }
    }

}
