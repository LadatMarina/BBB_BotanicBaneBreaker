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

    private void Awake()
    {
        //ingredientsEnum = GameAssets.Instance.Ingredients;
    }
    void Start()
    {
        GameManager.Instance.CreateNewItem(GameAssets.Instance.coldir, Vector3.right,1);

        //SpawnTheFruits(beanSpawner, GameAssets.Instance.bean);
        //SpawnTheFruits(appleSpawner, GameAssets.Instance.apple);

        GameManager.Instance.CreateNewItem(GameAssets.Instance.bluberry, Vector3.zero, 1);
        GameManager.Instance.CreateNewItem(GameAssets.Instance.strawberry, new Vector3(-5, -3, 0), 2);
        //CreateNewItem(GameAssets.Instance.healthPotion1, new Vector3(-8, -2, 0), 1);

        //string tag = gameObject.tag;

        /*switch (tag)
        {
            case "BlueberryField":
                for(int x= 0; x < 25; x=+5)
                {
                    for (int y = 0; y < 25; y=+5)
                    {
                        CreateNewItem(GameAssets.Instance.bluberry, new Vector3(transform.position.x + x,transform.position.y + y, 0), 1);
                    }
                }
                break;

            case "Example2":
                //do something
                break;
        }*/
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
        //Debug.Log($"all the {recollectableToSpawn.name} has been spawned");
    }
}
