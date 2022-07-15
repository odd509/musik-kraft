using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 12f;
    public Animator animator;
    
    
    private Vector2 dir;
    
    private bool jumpPressed;
    private bool isRunning;
    public bool isFacingRight = true;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        isRunning = Input.GetButton("Horizontal");
        
        float y = Input.GetAxis("Vertical");
        dir = new Vector2(x, y);
        
        if (Input.GetButtonDown("Jump"))
        {
            jumpPressed = true;
        }
       
        
        
    }

    private void FixedUpdate()
    {
        
        Walk(dir);
        if (jumpPressed && GetComponent<DetectDirections>().onGround)
        {
            Jump();
            jumpPressed = false;
        }

    }


    private void Walk(Vector2 dir)
    {
        
        rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);
        animator.SetBool("isRunning", isRunning);
        if (dir.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (dir.x < 0 && isFacingRight)
        {
            Flip();
        }


    }

    public void Jump()
    {
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }

    void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;

        isFacingRight = !isFacingRight;
    }

    
}
