using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    private GameAssets gameAssets;

    private void Awake()
    {
        gameAssets = FindObjectOfType<GameAssets>();
    }
    void Start()
    {
        for(int i = 0; i < 2; i++) 
        {
            GameObject fruit = new GameObject();
            fruit.transform.position = new Vector3(2*i, 5*i, 0);
            RecollectableDisplay recollectableDisplay = fruit.AddComponent<RecollectableDisplay>();
            recollectableDisplay.scriptableObject = gameAssets.ingredients[i];
            Debug.Log("ingredient 1 has been created.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
