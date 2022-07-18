using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public float damage;
    public float maxHealth;
    
    float health;
    
    void OnTriggerEnter2D(Collider2D collider){
        if (collider.gameObject.tag == "Player"){
            Movement player = collider.GetComponent<Movement>();
            player.playerHP -= damage;
        }
    }

    void TakeDamage(float damage) {
        health -= damage;
        if (health <= 0f)
        {
            Destroy(gameObject);
        }
    }
    void Heal(float heal) {
        health = Mathf.Min(maxHealth, (heal + health));
    }
}
