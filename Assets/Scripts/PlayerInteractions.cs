using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    //RecollectableDisplay recollectableDisplay;
    // Start is called before the first frame update

    public GameManager gameManager;
    public Inventory inventory;

    public Item itemScript;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        inventory = gameManager.GetInventory();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        //si a s'inventario hi ha s'element que jo he colisionat
        if(inventory.GetRecolletableList().Contains(other.gameObject.GetComponent<RecollectableDisplay>().scriptableObject))
        {
            Debug.Log($"previous amount = {itemScript.GetAmount()}");
            //itemScript.AddOneToAmount();

            // inventory.AfegirItem()

            //accedir a s'element de sa llista i Item.AddAmount();
            //Debug.Log("li falta afegir un amount a s'objecte perquè ja està dins sa llista");
            //other.gameObject.GetComponent<Item>().AddOneToAmount();
            //Debug.Log($"the object was already in the inventory, so now the amount has increased to {other.gameObject.GetComponent<Item>().GetAmount()}");
        }
        //si no està a sa llista simplement li afegesc a sa llista i li sum 1 de amount
        else
        {
            gameManager.GetInventory().AddRecollectableToTheInventory(other.gameObject.GetComponent<RecollectableDisplay>().scriptableObject);
            //other.gameObject.GetComponent<Item>().AddOneToAmount();
            itemScript.AddOneToAmount();
        }

        if (other != null) //&& (Input.GetKeyDown(KeyCode.X)))
        {
            //add this scriptable object to the inventory
            //recollectableDisplay = GetComponent<RecollectableDisplay>();
            switch (other.gameObject.GetComponent<RecollectableDisplay>().GetRecollectableType())
            {
                case RecollectableType.attackPotion:
                    Debug.Log($"you have recollected an attack potion named {other.gameObject.name}");
                    break;
                case RecollectableType.healthPotion:
                    Debug.Log($"you have recollected an health potion named {other.gameObject.name}");
                    break;
                case RecollectableType.ingredients:
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
