using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;
    private Vector2 lastMovement;
    private Rigidbody2D _rbPlayer;

    private Vector3 defaultStartPosition = new Vector3(-20, 7, 0);

    private Inventory inventory;

    public LayerMask collisionableLayer;

    public Village savedVillage;

    private void Awake()
    {
        InitializeInventory();

        _rbPlayer = GetComponent<Rigidbody2D>();
        lastMovement = Vector2.down;
    }
    private void Start()
    {
        //menwhile the game manager doesn't store a last player position, the player will appear in defaultStartPosition
        if (GameManager.Instance.GetLastPlayerPos() != Vector3.zero)
        {

            Vector3 lastPos = GameManager.Instance.GetLastPlayerPos();
            transform.position = new Vector3(lastPos.x, lastPos.y - 1, 0);
        }
        else
        {
            transform.position = defaultStartPosition;
        }

        if (GameManager.Instance.isFirstGame)
        {
            ExplanationManagerUI.Instance.ShowAnExplanation("Hi! A witch has set a malefition in Búger. Now, you have to found ingredients to make health potions to each villager.", 30);
        }
    }
    void Update()
    {
        if (GameManager.Instance.isPaused != true)
        {
            isWalking = false;
            Vector2 inputVector = gameInput.GetInputVectorNormalized();
            if ((Vector3)inputVector != Vector3.zero)
            {
                _rbPlayer.velocity = (Vector3)inputVector * moveSpeed;
                isWalking = true;
                lastMovement = inputVector;
            }
        }
    }
    private void LateUpdate()
    {
        if (isWalking == false) { _rbPlayer.velocity = Vector2.zero; }
    }

    public bool SomethingInFrontOf(Vector3 direction) { return Physics2D.Raycast(transform.position, direction, 3f, collisionableLayer); }
    public bool GetIsWalking() { return isWalking; }

    public Vector2 GetLastMovement() { return lastMovement; }
    public Vector2 GetPlayerPos() { return transform.position; }
    public void InitializeInventory() 
    {
        inventory = new Inventory(); 
        if(DataPersistanceManager.Instance.LoadInventory().Count >= 0)
        {
            //there are items saved in the inventory --> load this items to the new inventory.itemList();
            inventory.SetItemList(DataPersistanceManager.Instance.LoadInventory());
        }
        else
        {
            Debug.Log("No list JSON --> new emplty inicialized");
            //there are no items saved in the inventory
        }
    }


    public Inventory GetInventory() {
        if(inventory == null)
        {
            Debug.Log("inventoryNull");
            return null;
        }
        return inventory; 
    }

    public void AddItem(Item item)
    {
        Debug.Log("addItem / player");
        bool itemInInventory = false;

        //if the item is stackable revise the list, if not, add it directly to the inventory
        if (item.itemSO.stackable)
        {
            foreach (Item inventoryItem in inventory.GetItemList())
            {
                if (item.itemSO == inventoryItem.itemSO)
                {
                    //if the item is stackable and was already in the list, amount+ / true
                    inventoryItem.amount = inventoryItem.amount + item.amount;
                    itemInInventory = true;
                }
            }
        }

        if (!itemInInventory)
        {
            inventory.GetItemList().Add(item);
        }
        
        DataPersistanceManager.Instance.SaveInventory(inventory.GetItemList()); //after adding an object, save the inventory to the json file
    }

    public bool IsRecollectableInList(Item itemToCheck)
    {
        foreach (Item item in inventory.GetItemList())
        {
            if (item.itemSO == itemToCheck.itemSO)
            {
                return true;
            }
        }
        return false;
    }

    public void RemoveOneItemFromList(Item itemToRemove)
    {
        if (inventory.GetItemList() != null)
        {
            foreach (Item item in inventory.GetItemList())
            {
                if (item == itemToRemove)
                {
                    if(item.amount >= 1)
                    {
                        item.amount--;
                    }
                    else
                    {
                        inventory.GetItemList().Remove(itemToRemove);
                    }
                    DataPersistanceManager.Instance.SaveInventory(inventory.GetItemList());
                    break;
                }
            }
        }
    }

    public void SaveVillage(Village village) { savedVillage = village; }
    public Village LoadVillage() { return savedVillage; }

    public void RemoveItem(Item item) { 
        inventory.GetItemList().Remove(item);
        DataPersistanceManager.Instance.SaveInventory(inventory.GetItemList());
    }
}

