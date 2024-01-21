using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string H_AXIS = "Horizontal";
    private const string V_AXIS = "Vertical";
    
    private Animator _animator;
    private GameInput gameInput;
    public Player playerScript;
    private void Awake()
    {
        playerScript = GetComponentInParent<Player>();
        _animator = GetComponent<Animator>();
        gameInput = FindObjectOfType<GameInput>();
    }

    public void Update()
    {
        _animator.SetFloat(H_AXIS, gameInput.GetInputVectorNormalized().x);
        _animator.SetFloat(V_AXIS, gameInput.GetInputVectorNormalized().y);
        _animator.SetBool("isWalking", playerScript.GetIsWalking());
        _animator.SetFloat("Last_V", playerScript.GetLastMovement().y);
        _animator.SetFloat("Last_H", playerScript.GetLastMovement().x);
    }
}
