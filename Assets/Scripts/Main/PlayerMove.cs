using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{ 
    [Header("이동")]
    [SerializeField]
    private float moveSpeed;
    
    [SerializeField]
    private float jumpPower;

    private Rigidbody2D myRIgid;

    private Vector3 moveDirection = Vector3.right;
    

    private bool isJumping = false;
    
    void Start()
    {
        myRIgid = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
        }
    }

    void FixedUpdate()
    {
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
        if (!isJumping)
            return;
        
        myRIgid.AddForce(new Vector2(0, jumpPower));
        isJumping = false;
    }
}
