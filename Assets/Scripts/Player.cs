using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;
    private Vector2 lastMovement;
    private Rigidbody2D _rbPlayer;

    //inventory
    public Inventory inventory;

    public bool listInitialized = false;

    public LayerMask collisionableLayer;

    private void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        lastMovement = Vector2.down;

        //initialize the inventory
        inventory = new Inventory();

    }
    private void Start()
    {
        if(inventory != null)
        {
            listInitialized = true;
        }
    }
    void Update()
    {
        if(GameManager.Instance.isPaused != true)
        {
            isWalking = false;
            Vector2 inputVector = gameInput.GetInputVectorNormalized();
            if ((Vector3)inputVector != Vector3.zero)
            {
                //Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0);
                //transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;
                _rbPlayer.velocity = (Vector3)inputVector * moveSpeed;
                isWalking = true;
                lastMovement = inputVector;
            }

            if (Input.GetKeyUp(KeyCode.J))
            {
                GameManager.Instance.DisplayInventoryItemList(inventory.GetItemList());
            }
        }
    }
    private void LateUpdate()
    {
        //other ways to write it down: _rbPlayer.velocity = isWalking ? _rbPlayer.velocity : Vector2.zero;
        if (isWalking == false) {_rbPlayer.velocity = Vector2.zero; }
    }

    public bool RecollectableInFrontOf(Vector3 direction)
    {
        return Physics2D.Raycast(transform.position, direction ,3f,collisionableLayer);
    }


    public bool GetIsWalking()
    {
        return isWalking;
    }
    public Vector2 GetLastMovement()
    {
        return lastMovement;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    public void Remove(Item item)
    {
        inventory.RemoveItemFromList(item);        
    }

    public Vector2 GetPlayerPos()
    {
        return transform.position;
    }
}

