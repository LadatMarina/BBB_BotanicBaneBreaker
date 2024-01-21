using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking = false;
    private Vector2 lastMovement;

    private void Awake()
    {
        lastMovement = Vector2.down;
    }
    void Update()
    {
        isWalking = false;
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        if ((Vector3)inputVector != Vector3.zero)
        {
            //Vector3 moveDir = new Vector3(inputVector.x, inputVector.y, 0);
            transform.position += (Vector3)inputVector * moveSpeed * Time.deltaTime;
            isWalking = true;
            lastMovement = inputVector;
        }
    }

    public bool GetIsWalking()
    {
        return isWalking;
    }
    public Vector2 GetLastMovement()
    {
        return lastMovement;
    }
}

