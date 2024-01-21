using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private const string H_AXIS = "Horizontal";
    private const string V_AXIS = "Vertical";
    
    private Animator _animator;
    private GameInput gameInput;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        gameInput = FindObjectOfType<GameInput>();
    }

    public void Update()
    {
        _animator.SetInteger(H_AXIS, (int)gameInput.GetInputVectorNormalized().x);
        _animator.SetInteger(V_AXIS, (int)gameInput.GetInputVectorNormalized().y);
    }
}
