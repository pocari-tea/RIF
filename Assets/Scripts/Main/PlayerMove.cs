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
    private int jumpCount;

    private Rigidbody2D myRIgid;

    private Vector3 moveDirection = Vector3.right;
    

    private bool isJumping = false;

    public float Speed =3f;

    void Start()
    {
        myRIgid = gameObject.GetComponent<Rigidbody2D>();
    }
    
    void Update()
    {

        transform.Translate(Speed * Time.deltaTime, 0, 0);


        if (Input.GetKeyDown(KeyCode.Space) && (jumpCount == 0 || jumpCount == 1))
        {
            isJumping = true;
            jumpCount++;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Land"))
        {
            jumpCount = 0;
        }
    }
}
