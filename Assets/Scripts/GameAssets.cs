using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameAssets : MonoBehaviour
{
    public Recollectable apple, bean, bluberry, strawberry, defaultIngredient;

    public List<Recollectable> ingredients;

    public Village paco, maria, bel, toni;
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
            default:
                Debug.LogWarning("the default ingredient is being returned");
                return defaultIngredient;
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
