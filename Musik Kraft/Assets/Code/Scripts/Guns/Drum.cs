using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public float groundDamage = 40f;
    public float areaDamage = 15f;
    public float knockbackForce = 0.5f;

    bool keyPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown("P")) keyPressed = true; 
        else keyPressed = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Debug.Log("Enemy Spotted");
            GameObject enemy = collision.gameObject;
            // if enemy isGrounded
            enemy.GetComponent<Rigidbody2D>().AddForce(
                ((GetComponent<Transform>().position.x - enemy.GetComponent<Transform>().position.x) > 0 ? Vector2.left : Vector2.right) * knockbackForce, ForceMode2D.Impulse);
        }
    }
}
