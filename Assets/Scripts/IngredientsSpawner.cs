using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameAssets;

public class IngredientsSpawner : MonoBehaviour
{
    //private Ingredients ingredientsEnum;
    private GameAssets gameAssets;
    [SerializeField]private GameObject itemWorld;

    private void Awake()
    {
        gameAssets = FindObjectOfType<GameAssets>();
        //ingredientsEnum = gameAssets.Ingredients;
    }
    void Start()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject instantiatedItem = Instantiate(itemWorld, new Vector3(i, 5*i, 0), Quaternion.identity);

            Item item = new Item { amount = 1, itemSO = gameAssets.apple };

            instantiatedItem.GetComponent<RecollectableDisplay>().SetItem(item);

            //------

            GameObject instantiatedItem2 = Instantiate(itemWorld, new Vector3(-2* i, 1 * i, 0), Quaternion.identity);

            Item item2 = new Item { amount = 1, itemSO = gameAssets.bean };

            instantiatedItem2.GetComponent<RecollectableDisplay>().SetItem(item2);
        }

    }
}
