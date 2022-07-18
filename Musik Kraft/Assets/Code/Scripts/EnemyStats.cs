using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float damage;
    public float maxHealth = 100;
    
    float health;

    private void Awake()
    {
        health = maxHealth;
    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            Movement player = collider.GetComponent<Movement>();
            player.stats.TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage) {
        health -= damage;
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
