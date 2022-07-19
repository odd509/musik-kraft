using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    
    
    [SerializeField] public LayerMask groundLayer;
    public bool onGround;
    
    [Header("Collision")]
    public float collisionRadius = .25f;
    public Vector2 bottomOffset;

    void Update()
    {
        onGround = Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius, groundLayer);
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2) transform.position + bottomOffset, collisionRadius);
    }
}
