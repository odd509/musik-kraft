using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class BetterJump : MonoBehaviour
{
    public float fallMultiplier = 7f;
    public float lowJumpMultiplier = 5f;

    Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    public void FixedUpdate()
    {
        if (rb.velocity.y < 0)
        {
            rb.gravityScale = fallMultiplier;
        }
        else if (rb.velocity.y > 0 && !Input.GetButton("Jump"))

        {
            rb.gravityScale = lowJumpMultiplier;
            Debug.Log(Input.GetButton("Jump"));
        }
        else
        {
            rb.gravityScale = 2f;
        }
        
    }

    
}
