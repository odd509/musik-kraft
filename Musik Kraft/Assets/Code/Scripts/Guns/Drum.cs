using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drum : MonoBehaviour
{
    public float groundDamage = 40f;
    public float areaDamage = 15f;
    public float knockbackForce = 2f;
    public float radius = 10f;

    bool keyPressed = false;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Collider2D[] results = Physics2D.OverlapCircleAll(GetComponent<Transform>().position, radius, LayerMask.NameToLayer("Enemy"));
            foreach (Collider2D collider in results) {
                Debug.Log("Enemy Spotted");
                GameObject enemy = collider.gameObject;

                enemy.GetComponent<EnemyStats>().TakeDamage(enemy.GetComponent<IsGrounded>().Check() ? groundDamage : areaDamage);


                Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
                Vector2 difference = (rb.transform.position - transform.position).normalized;
                difference = difference.normalized * knockbackForce;
                rb.AddForce(difference, ForceMode2D.Impulse);

            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(GetComponent<Transform>().position, radius);
    }
}
