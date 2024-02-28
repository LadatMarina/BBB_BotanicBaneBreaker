using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;
    private Vector2 lastMovement;
    private Rigidbody2D _rbPlayer;

    private Vector3 defaultStartPosition = new Vector3(-20 , 7 , 0);

    private Inventory inventory;

    //public bool listInitialized = false;

    public LayerMask collisionableLayer;

    private void Awake()
    {
        _rbPlayer = GetComponent<Rigidbody2D>();
        lastMovement = Vector2.down;

        InitializeInventory();
        
    }
    private void Start()
    {
        //mirar per a què ho emplei o algo perquè ara ns pq tenc aquesta variable
        //if (inventory != null)
        //{
        //    listInitialized = true;
        //}

        //menwhile the game manager doesn't store a last player position, the player will appear in defaultStartPosition
        if (GameManager.Instance.GetLastPlayerPos() != Vector3.zero) {

            Vector3 lastPos = GameManager.Instance.GetLastPlayerPos();
            transform.position = new Vector3(lastPos.x , lastPos.y -1, 0   );
            //Debug.Log("because the gameManager was storing a last player position, the player has moved to that last position " + transform.position);
        }
        else
        {
            transform.position = defaultStartPosition; 
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
        }
    }
    private void LateUpdate()
    {
        //other ways to write it down: _rbPlayer.velocity = isWalking ? _rbPlayer.velocity : Vector2.zero;
        if (isWalking == false) {_rbPlayer.velocity = Vector2.zero; }
    }

    public bool RecollectableInFrontOf(Vector3 direction) { return Physics2D.Raycast(transform.position, direction, 3f, collisionableLayer); }
    public bool GetIsWalking() { return isWalking; }

    public Vector2 GetLastMovement() { return lastMovement; }
    public Vector2 GetPlayerPos() { return transform.position; }
    public void InitializeInventory() { inventory = new Inventory(); }


    public Inventory GetInventory() { return inventory; }

    public void Remove(Item item) { inventory.RemoveItemFromList(item); }
}

