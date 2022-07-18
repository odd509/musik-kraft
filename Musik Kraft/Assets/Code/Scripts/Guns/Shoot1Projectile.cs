using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot1Projectile : MonoBehaviour
{
    public float thrust;
    public float damage;
    void Awake(){
        StartCoroutine(DestroyProjectile());
    }
    private void OnTriggerEnter2D(Collider2D other){
        if (other.gameObject.CompareTag("Enemy")){
            Rigidbody2D enemy = other.GetComponent<Rigidbody2D>();
            if (enemy != null) {
                EnemyStats kill = other.GetComponent<EnemyStats>();
                kill.health -= damage;
                Vector2 difference = (enemy.transform.position - transform.position).normalized;
                difference = difference.normalized * thrust;
                enemy.AddForce(difference, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
    IEnumerator DestroyProjectile(){
        yield return new WaitForSeconds(1f);
        Destroy(gameObject);
    }
}
