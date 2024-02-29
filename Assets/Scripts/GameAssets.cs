using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAssets : MonoBehaviour
{
    public Recollectable apple, bean, bluberry, strawberry, attackPotion1, healthPotion1, healthPotion2, healthPotion3, healthPotion4;
    public Village paco, maria, bel, toni;

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
            case "AttackPotion1":
                return attackPotion1;
            case "HealthPotion1":
                return healthPotion1;
            case "HealthPotion2":
                return healthPotion2;
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

}
