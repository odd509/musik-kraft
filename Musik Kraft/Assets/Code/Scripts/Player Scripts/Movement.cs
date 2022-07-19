using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Movement : MonoBehaviour
{

    private Rigidbody2D rb;
    public Animator animator;
    public PlayerStats stats;

    [Header("Physics")]
    public float speed = 10f;
    public float jumpForce = 12f;
    public float acceleration = 3.5f;
    public float decceleration = 13f;
    public float velPower = 1f;
    public float dashSpeed;
    public float startDashTime;
    public float fallMultiplier = 7f;
    public float lowJumpMultiplier = 5f;
    
    
    float lastGroundedTime;
    float lastJumpTime; 
    float jumpCoyoteTime = 0.2f;
    float jumpBufferTime = 0.2f;
    private float dashTime;
    
    private Vector2 dir;
    
    private bool jumpPressed;
    private bool isRunning;
    private bool isGrounded;
    public bool isFacingRight = true;
    private bool canDash;
    
    // Start is called before the first frame update
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        stats = GetComponent<PlayerStats>();
        dashTime = startDashTime;
    }

    // Update is called once per frame
    void Update()
    {   
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

        if (Input.GetKeyDown(KeyCode.C) && dashTime < 0f)
        {
            canDash = true;
        }
        else
        {
            dashTime -= Time.deltaTime;
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

        if (canDash)
        {
            Dash();
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
            rb.gravityScale = Input.GetKey(KeyCode.DownArrow) ? fallMultiplier * 1.3f : fallMultiplier;

        }
        else
        {
            animator.SetBool("isFalling", false);
            
            if (!Input.GetButton("Jump"))
            {
                rb.gravityScale = lowJumpMultiplier;
            }
            else
            {
                rb.gravityScale = 2f;
            }
            
        }
    }

    void Dash()
    {
        rb.AddForce(dir * dashSpeed, ForceMode2D.Impulse);
        StartCoroutine(invincibleTime());
        dashTime = startDashTime;
        canDash = false;

    }
    IEnumerator invincibleTime(){
        Physics2D.IgnoreLayerCollision(3,9,true);
        yield return new WaitForSeconds(0.5f);
        Physics2D.IgnoreLayerCollision(3,9,false);
        
    }


}
