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
        CreateNewItem(gameAssets.bean, new Vector3(0, 3, 0), 1);
        CreateNewItem(gameAssets.bean, new Vector3(0, 4, 0), 1);
        CreateNewItem(gameAssets.apple, new Vector3(-5, 3, 0), 3);
        CreateNewItem(gameAssets.apple, new Vector3(-5, 5, 0), 3);
        CreateNewItem(gameAssets.bluberry, Vector3.zero, 1);
        CreateNewItem(gameAssets.strawberry, new Vector3(-5, -3, 0), 2);

        string tag = gameObject.tag;

        switch (tag)
        {
            case "BlueberryField":
                for(int x= 0; x < 25; x=+5)
                {
                    for (int y = 0; y < 25; y=+5)
                    {
                        CreateNewItem(gameAssets.bluberry, new Vector3(transform.position.x + x,transform.position.y + y, 0), 1);
                    }
                }
                break;

            case "Example2":
                //do something
                break;
        }
    }

    public GameObject CreateNewItem(Recollectable itemSO, Vector3 position, int amount)
    {
        GameObject newObject = Instantiate(itemWorld, position, Quaternion.identity);

        Item item = new Item { amount = amount, itemSO = itemSO };

        newObject.GetComponent<RecollectableDisplay>().SetItem(item);

        return newObject;
    }
}
