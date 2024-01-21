using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private GameInput gameInput;

    private bool isWalking;
    
    void Update()
    {
        Vector2 inputVector = gameInput.GetInputVectorNormalized();
        
        Vector3 moveDir = new Vector3(inputVector.x, inputVector.y,0);

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }
}

