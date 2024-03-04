using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class IngredientsSpawner : MonoBehaviour
{
    //private Ingredients ingredientsEnum;

    [SerializeField] private Vector2[] appleSpawner;
    [SerializeField] private Vector2[] beanSpawner;
    [SerializeField] private Vector2[] bluberrySpawner;
    [SerializeField] private Vector2[] strawberrySpawner;


    void Start()
    {
        //create two initial ingredients to help the player
        GameManager.Instance.CreateNewItem(GameAssets.Instance.bluberry, Vector3.zero, 1);
        GameManager.Instance.CreateNewItem(GameAssets.Instance.strawberry, new Vector3(-5, -3, 0), 1);
    }

    public void Spawn(string fruit)
    {
        switch (fruit)
        {
            case "bean":
                SpawnTheFruits(beanSpawner, GameAssets.Instance.bean);
                break;
            case "apple":
                SpawnTheFruits(appleSpawner, GameAssets.Instance.apple);
                break;
            case "blueberry":
                SpawnTheFruits(bluberrySpawner, GameAssets.Instance.bluberry);
                break;
            case "strawberry":
                SpawnTheFruits(strawberrySpawner, GameAssets.Instance.strawberry);
                break;
        }
    }

    private void SpawnTheFruits(Vector2[] arrayOfPositions, Recollectable recollectableToSpawn)
    {
        int i = 0;
        foreach(Vector2 pos in arrayOfPositions)
        {
            GameManager.Instance.CreateNewItem(recollectableToSpawn, arrayOfPositions[i], 1);
            i++;
        }
    }
}
