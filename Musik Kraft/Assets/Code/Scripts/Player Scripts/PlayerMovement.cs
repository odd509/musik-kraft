using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public float jumpForce;

    private Rigidbody2D rb;
    private Transform transform;
    private Animator animator;

    // Inputs
    private float moveInput;
    private bool jumpPressed;

    // Bools
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask ground;

    //void atAwake()
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        transform = GetComponent<Transform>();
    }
    void Update()
    {
        moveInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
        {
            jumpPressed = true;
        }

        // Flip
        // BU BURDA YANLI� G�B� FIXED UPDATE E TA�INACAK
        if (moveInput > 0) transform.localScale = Vector3.one;
        else if (moveInput < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FixedUpdate()
    {
        if (jumpPressed && isGrounded())
        {
            Jump();

        }
        else if (moveInput != 0)
        {
            Move();
        }
    }
    private void Jump()
    {
        rb.velocity = Vector2.up * jumpForce;
        jumpPressed = false;
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    }
    public bool isGrounded()
    {

        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
    }
}
