using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngredientsSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 3; i++) { 

        GameObject fruit = new GameObject();
            fruit.AddComponent<RecollectableDisplay>();
            
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
