using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    private GameAssets gameAssets;
    [SerializeField]private GameObject itemWorld;

    private void Awake()
    {
        gameAssets = FindObjectOfType<GameAssets>();
    }
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            GameObject instantiatedItem = Instantiate(itemWorld, new Vector3(2*i, 5*i, 0), Quaternion.identity);

            RecollectableDisplay recollectableDisplay = instantiatedItem.AddComponent<RecollectableDisplay>();

            //Item item = new Item { };
            // Item itemScript = instantiatedItem.AddComponent<Item>();
            //itemScript.ResetAmount();
            //Debug.Log(itemScript.GetAmount());
            //li deim quin scriptable object ha d'agafar sa referència
            recollectableDisplay.scriptableObject = gameAssets.bean;
        }
        

        

        //ara mateix no me importa que totes ses instancies siguins beans,
        //perquè en un futur lo que faré serà instanciar ses que jo digui on vulgui,
        //per tant ara mateix basta que siguin ses mateixes
        /*for(int i = 0; i < 2; i++) 
        {
            Item item = new Item();

            GameObject fruit = Instantiate(itemWorld, new Vector3(2 * i, 5 * i, 0), Quaternion.identity);

            /*GameObject fruit = new GameObject();
            fruit.transform.position = new Vector3(2*i, 5*i, 0);

            RecollectableDisplay recollectableDisplay = fruit.AddComponent<RecollectableDisplay>();
            recollectableDisplay.scriptableObject = gameAssets.bean;
            fruit.name = recollectableDisplay.scriptableObject.name;
            Debug.Log($"ingredient 1 has been created with name {recollectableDisplay.scriptableObject.name}");
        }*/
    }
}
