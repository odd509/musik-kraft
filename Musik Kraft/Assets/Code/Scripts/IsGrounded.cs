using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public Transform groundCheck;
    public LayerMask ground;
    public float checkRadius;

    public bool Check()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, ground);
    }
}
