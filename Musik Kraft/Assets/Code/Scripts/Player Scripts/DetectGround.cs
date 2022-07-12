using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DetectGround : MonoBehaviour
{

    public LayerMask groundLayer;
    public bool onGround;
    
    [Header("Collision")]
    public float collisionRadius = .25f;
    public Vector2 bottomOffset;
    private Color debugCollisionColor = Color.red;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
