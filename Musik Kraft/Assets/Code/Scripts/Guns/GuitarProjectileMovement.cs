using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuitarProjectileMovement : MonoBehaviour
{

    public Vector2 velocityVector;
    private Rigidbody2D rb;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = velocityVector;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

}
