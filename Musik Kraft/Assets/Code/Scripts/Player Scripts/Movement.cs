using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    public float speed = 10f;
    public float jumpForce = 12f;
    public Animator animator;
    public float acceleration = 3.5f;
    public float decceleration = 13f;
    public float velPower = 1f;
    float lastGroundedTime;
    float lastJumpTime; 
    float jumpCoyoteTime = 0.2f;
    float jumpBufferTime = 0.2f;
    public float playerHP;
    private Vector2 dir;
    
    private bool jumpPressed;
    private bool isRunning;
    private bool isGrounded;
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
        if(playerHP <= 0f){
            Destroy(gameObject);
        }
        
        float x = Input.GetAxisRaw("Horizontal");
        isRunning = Input.GetButton("Horizontal");

        float y = Input.GetAxis("Vertical");
        dir = new Vector2(x, y);

        isGrounded = GetComponent<DetectDirections>().onGround;
        if (isGrounded != animator.GetBool("isGrounded"))
        {
            animator.SetBool("isGrounded", isGrounded);
        }
        
        
        if (Input.GetButtonDown("Jump"))
        {
            lastJumpTime = jumpBufferTime; //jump coyote timers
            jumpPressed = true;
        }
        else
        { 
            lastJumpTime -= Time.deltaTime;
        }

        if (Input.GetButtonUp("Jump"))
        {
            lastGroundedTime = 0f;
        }
        
        #region Timer
        
        if (isGrounded)
        {
            lastGroundedTime = jumpCoyoteTime;
        }
        else
        {
            lastGroundedTime -= Time.deltaTime;
        }
        
        

        #endregion
        
    }

    private void FixedUpdate()
    {
        
        
        
        
        Run(dir);
        
        if (lastGroundedTime > 0 && lastJumpTime > 0)
        {
            Jump();
            jumpPressed = false;
        }
        
        FallDetection();
        
    }


    private void Run(Vector2 dir)
    {
        animator.SetBool("isRunning", isRunning);
        //rb.velocity = new Vector2(dir.x * speed, rb.velocity.y);

        #region Run

        float targetSpeed = dir.x * speed;
        float speedDif = targetSpeed - rb.velocity.x;
        float accelRate = (Mathf.Abs(targetSpeed) > 0.01f) ? acceleration : decceleration;
        float movement = Mathf.Pow(Mathf.Abs(speedDif) * accelRate, velPower) * Mathf.Sign(speedDif);

        rb.AddForce(movement * Vector2.right);
        
        #endregion

        #region Flip

        if (dir.x > 0 && !isFacingRight)
        {
            Flip();
        }
        else if (dir.x < 0 && isFacingRight)
        {
            Flip();
        }

        #endregion
        
        


    }

    public void Jump()
    {
        animator.SetTrigger("jump");
        rb.velocity = new Vector2(rb.velocity.x, 0);
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        lastJumpTime = 0f;
    }

    void Flip()
    {
        transform.Rotate(0f, 180f, 0f);

        isFacingRight = !isFacingRight;
    }

    void FallDetection()
    {
        if (rb.velocity.y < 0 )
        {
            animator.SetBool("isFalling", true);
        }
        else
        {
            animator.SetBool("isFalling", false);
        }
    }

    
}
