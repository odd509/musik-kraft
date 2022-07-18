using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class DetectDirections : MonoBehaviour
{

    [SerializeField] public LayerMask groundLayer;
    public bool onGround;
    private bool isFacingRight;
    
    [Header("Collision")]
    public float collisionRadius = .25f;
    public Vector2 bottomOffset;
    public Vector2 frontOffset;
    private Color debugCollisionColor = Color.red;

    private void Awake()
    {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        isFacingRight = GetComponent<Movement>().isFacingRight;
        onGround = Physics2D.OverlapCircle((Vector2) transform.position + bottomOffset, collisionRadius, groundLayer);
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Gizmos.DrawWireSphere((Vector2) transform.position + bottomOffset, collisionRadius);
        
        Gizmos.DrawWireSphere((Vector2) transform.position + frontOffset, collisionRadius);

    }
}
