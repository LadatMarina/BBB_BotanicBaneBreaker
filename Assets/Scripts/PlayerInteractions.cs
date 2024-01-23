using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    const string ATTACK_POTION = "attackPotion";
    const string HEALTH_POTION = "healthPotion";
    const string INGREDIENT = "ingredient";

    //RecollectableDisplay recollectableDisplay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other != null) 
        {
            //add this scriptable object to the inventory
            //recollectableDisplay = GetComponent<RecollectableDisplay>();
            switch (other.gameObject.GetComponent<RecollectableDisplay>().type)
            {
                case ATTACK_POTION:
                    Debug.Log($"you have recollected an attack potion named {other.gameObject.name}");
                    break;
                case HEALTH_POTION:
                    Debug.Log($"you have recollected an health potion named {other.gameObject.name}");
                    break;
                case INGREDIENT:
                    Debug.Log($"you have recollected an ingredient named {other.gameObject.name}");
                    break;

            }
            Destroy(other.gameObject);
        }
        else
        {
            Debug.LogError("something gone wrong, the other game object is null");
        }
        
    }
}
