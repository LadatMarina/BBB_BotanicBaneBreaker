using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerHandle : MonoBehaviour
{
    public int timesThePlayerPassed = 0;
    private IngredientsSpawner ingredientsSpawner;

    // Start is called before the first frame update
    void Start()
    {
        ingredientsSpawner = FindObjectOfType<IngredientsSpawner>();
    }


    //everytime the player goes by the field, sum. And when times>4, spawn the fruits
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if(timesThePlayerPassed > 4)
            {
                ingredientsSpawner.Spawn(this.gameObject.name);
                timesThePlayerPassed = 0;
            }
            else
            {
                timesThePlayerPassed++;
            }
        }
    }
}
