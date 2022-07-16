using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1Projectile : MonoBehaviour
{
    public float thrust;
    public float damage;
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null) {
                EnemyStats kill = other.GetComponent<EnemyStats>();
                kill.health -= damage;
                if(kill.health > 0f){
                    Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
                }
            }
        }
    }
}
