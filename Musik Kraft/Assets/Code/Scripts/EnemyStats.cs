using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float damage;
    public float knockbackForce;
    public float maxHealth = 100;
    
    private float health;

    [Header("HealthBar")]
    public HealthBarBehavior healthBar;

    

    private void Awake()
    {
        health = maxHealth;
        healthBar.setHealth(health,maxHealth);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collider = collision.gameObject;
        if (collider.gameObject.tag == "Player")
        {
            collider.GetComponent<PlayerStats>().TakeDamage(damage);
            Rigidbody2D playerRigid = collider.GetComponent<Rigidbody2D>();
            Vector2 difference = (playerRigid.transform.position - transform.position).normalized;
            difference = difference.normalized * knockbackForce;
            playerRigid.AddForce(difference, ForceMode2D.Impulse);
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
        healthBar.setHealth(health,maxHealth);
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    public void Heal(float heal) {
        health = Mathf.Min(maxHealth, (heal + health));
    }
    public float getHP() { return health; }
    
    
}
