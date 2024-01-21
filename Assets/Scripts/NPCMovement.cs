using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    private float speed = 2f;
    private Rigidbody2D _rigidbody;
    public bool isWalking = false;
    public float walkTime = 2f;
    private float walkCounter;

    public float waitTime = 4.0f;
    private float waitConuter;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        waitConuter = waitTime;
        walkCounter = walkTime;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
