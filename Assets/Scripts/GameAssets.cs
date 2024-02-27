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
            case "apple":
                return apple;
            case "bean":
                return bean;
            case "bluberry":
                return bluberry;
            case "strawberry":
                return strawberry;
            case "attackPotion1":
                return attackPotion1;
            case "healthPotion1":
                return healthPotion1;
            case "healthPotion2":
                return healthPotion2;
        }
        return null;
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
        }
        return null;
    }

}
